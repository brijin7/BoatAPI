using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using TTDCAPIv1.Models;

namespace TTDCAPIv1.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class RowerController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_BoatHouse"].ConnectionString);

        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);
        /*Rower Settlement Summary*/

        //List
        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Rower"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerSettlementSummary")]
        public IHttpActionResult RowerSummary([FromBody] RowerSettlement Rower)
        {
            try
            {
                string squery = string.Empty;

                if (Rower.RowerId != "" && Rower.TripDate != "" && Rower.BoatHouseId != "")
                {
                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = @BoatHouseId AND A.Status IN ('B', 'R') AND B.Status IN ('B', 'R', 'P') "
                        + " AND CAST(A.TripStartTime AS DATE) = @TripDate "
                        + " AND A.ActualRowerCharge IS NOT NULL AND A.ActualRowerCharge > 0 ";

                    if (Rower.RowerId != "0")
                    {
                        condition += " AND A.RowerId = @RowerId ";
                    }
                    squery = " SELECT RowerId, RowerName, BookingDate, SUM(TripsCount) as 'TripCount', SUM(TotalAmount) as 'TotalAmount', "
                        + " SUM(PaidAmount) as 'PaidAmount',SUM(Balance) as 'BalanceAmount' FROM ( "
                        + " SELECT C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 103) as 'BookingDate', COUNT(*) as 'TripsCount', "
                        + " SUM(ISNULL(ActualRowerCharge, 0)) as 'TotalAmount', "
                        + " CASE WHEN A.SettlementId IS NOT NULL THEN SUM(ISNULL(ActualRowerCharge, 0)) ELSE 0 END AS 'PaidAmount', "
                        + " CASE WHEN A.SettlementId IS NULL THEN SUM(ISNULL(ActualRowerCharge, 0)) ELSE 0 END AS 'Balance' "
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN RowerMaster AS C ON A.RowerId = C.RowerId AND A.BoatHouseId = C.BoatHouseId "
                        + " " + condition + " "
                        + " GROUP BY C.RowerId, C.RowerName, A.TripStartTime, A.SettlementId "
                        + " ) AS A WHERE Balance <> 0 GROUP BY RowerId, RowerName, BookingDate";

                    List<RowerSettlement> li = new List<RowerSettlement>();
                    SqlCommand cmd = new SqlCommand(squery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@TripDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Rower.BoatHouseId.Trim();
                    cmd.Parameters["@TripDate"].Value = DateTime.Parse(Rower.TripDate.Trim(), objEnglishDate);
                    cmd.Parameters["@RowerId"].Value = Rower.RowerId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RowerSettlement RowerSum = new RowerSettlement();

                            RowerSum.RowerId = dt.Rows[i]["RowerId"].ToString();
                            RowerSum.RowerName = dt.Rows[i]["RowerName"].ToString();
                            RowerSum.TripCount = dt.Rows[i]["TripCount"].ToString();
                            RowerSum.TripDate = dt.Rows[i]["BookingDate"].ToString();
                            RowerSum.TotalCharge = dt.Rows[i]["TotalAmount"].ToString();
                            RowerSum.PaidCharge = dt.Rows[i]["PaidAmount"].ToString();
                            RowerSum.BalanceCharge = dt.Rows[i]["BalanceAmount"].ToString();

                            li.Add(RowerSum);
                        }

                        RowerSettlementList BoatRate = new RowerSettlementList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        RowerSettlementStr BoatRate = new RowerSettlementStr
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RowerSettlementStr Vehicle = new RowerSettlementStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        // Rower Settlement List

        /// <summary>
        /// Modified By : Prethika
        /// Modified Date : 13-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="boatType"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerSettledGrid")]
        public IHttpActionResult RowerSettledGrid([FromBody] SettlementAmount boatType)
        {
            try
            {
                if (boatType.FromDate != "" && boatType.BoatHouseId != "" && boatType.RowerId != "")
                {
                    List<SettlementAmount> li = new List<SettlementAmount>();
                    con.Open();
                    string condition = string.Empty;
                    if (boatType.RowerId != "0")
                    {
                        condition += " AND A.RowerId= @RowerId ";
                    }

                    SqlCommand cmd = new SqlCommand(" SELECT A.SettlementId, "
                        + " CONVERT(VARCHAR, A.SettlementDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.SettlementDate, 100),7) AS 'SettleDate', "
                        + " A.RowerId, B.RowerName, A.SettlementAmount FROM RowerSettlement AS A "
                        + " INNER JOIN RowerMaster AS B ON A.RowerId = B.RowerId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId "
                        + " " + condition + " AND "
                        + " CAST(A.SettlementDate AS DATE) BETWEEN @FromDate AND "
                        + " @ToDate ORDER BY SettleDate", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(boatType.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(boatType.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@RowerId"].Value = boatType.RowerId.Trim();


                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SettlementAmount amt = new SettlementAmount();
                            amt.SettlementId = dt.Rows[i]["SettlementId"].ToString();
                            amt.SettlementDate = dt.Rows[i]["SettleDate"].ToString();
                            amt.RowerId = dt.Rows[i]["RowerId"].ToString();
                            amt.RowerName = dt.Rows[i]["RowerName"].ToString();
                            amt.SettlementAmt = dt.Rows[i]["SettlementAmount"].ToString();
                            li.Add(amt);
                        }

                        SettlementAmountList ConfList = new SettlementAmountList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        SettlementAmountStr ConfRes = new SettlementAmountStr
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    SettlementAmountStr Vehicle = new SettlementAmountStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }


        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="boatType"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerSettlement/SettlementAmount")]
        public IHttpActionResult PaymentAmount([FromBody] SettlementAmount boatType)
        {
            try
            {
                if (boatType.RowerId != "" && boatType.TripDate != "")
                {
                    List<SettlementAmount> li = new List<SettlementAmount>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand(" SELECT A.BookingId, A.BookingPin, A.BoatTypeId, C.BoatType, A.BoatSeaterId, "
                        + " D.SeaterType, A.BoatReferenceNo, E.RowerName, B.CustomerMobile, "
                        + " CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS TripStartTime, "
                        + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime,"
                        + " DATEDIFF(MINUTE, A.TripStartTime ,A.TripEndTime) AS 'TravelledMinutes',"
                        + " ISNULL(A.ActualRowerCharge, 0) AS  'SettlementAmt' FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseName = B.BoatHouseName "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND A.BoatHouseName = C.BoatHouseName "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND A.BoatHouseName = D.BoatHouseName "
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId AND A.BoatHouseName = E.BoatHouseName "
                        + " WHERE A.RowerId = @RowerId AND CAST(A.TripStartTime AS DATE) = @TripDate "
                        + " AND A.BoatHouseId = @BoatHouseId AND A.Status IN ('B', 'R') AND B.Status IN ('B', 'R', 'P') "
                        + " AND A.SettlementId IS NULL AND A.ActualRowerCharge IS NOT NULL AND A.ActualRowerCharge > 0  ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@TripDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
                    cmd.Parameters["@TripDate"].Value = DateTime.Parse(boatType.TripDate.Trim(), objEnglishDate);
                    cmd.Parameters["@RowerId"].Value = boatType.RowerId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SettlementAmount amt = new SettlementAmount();
                            amt.BookingId = dt.Rows[i]["BookingId"].ToString();
                            amt.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            amt.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            amt.BoatType = dt.Rows[i]["BoatType"].ToString();
                            amt.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            amt.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            amt.RowerName = dt.Rows[i]["RowerName"].ToString();
                            amt.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            amt.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            amt.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            amt.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            amt.TravelledMinutes = dt.Rows[i]["TravelledMinutes"].ToString();
                            amt.SettlementAmt = dt.Rows[i]["SettlementAmt"].ToString();
                            li.Add(amt);
                        }

                        SettlementAmountList ConfList = new SettlementAmountList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        SettlementAmountStr ConfRes = new SettlementAmountStr
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    SettlementAmountStr Vehicle = new SettlementAmountStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }


        // Rower Settlement Insert

        [HttpPost]
        [AllowAnonymous]
        [Route("RowerSettlement")]
        public IHttpActionResult InsRowerSettlement([FromBody] RowerSettlement enq)
        {
            try
            {
                if (enq.QueryType != "" && enq.RowerId != "" && enq.TripDate != "" && enq.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_RowerSettlement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", enq.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@RowerId", enq.RowerId.ToString());
                    cmd.Parameters.AddWithValue("@TripDate", enq.TripDate.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", enq.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", enq.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", enq.CreatedBy.Trim());

                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con.Close();

                    if (sResult[0].Trim() == "Success")
                    {
                        RowerSettlementStr InsCE = new RowerSettlementStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        RowerSettlementStr InsCE = new RowerSettlementStr
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    RowerSettlementStr InsCE = new RowerSettlementStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }


        // SettledId List

        /// <summary>
        /// Modified By : Prethika
        /// Modified Date: 13-10-2021
        /// Modified By : Prethika
        /// Modified Date: 19-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="boatType"></param>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        [Route("RowerSettledIdGrid")]
        public IHttpActionResult RowerSettledIdGrid([FromBody] SettlementAmount boatType)
        {
            try
            {
                if (boatType.SettlementId != "" && boatType.BoatHouseId != "")
                {
                    List<SettlementAmount> li = new List<SettlementAmount>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand(" SELECT  BookingId,BookingPin,  BookingDate,BoatType,  SeaterType, "
                     + " ActualRowerCharge, SUM(TripsCount) as 'TripCount', RowerName,MobileNo,SettleDate,TripDate FROM "
                     + " (SELECT A.BookingId,A.BookingPin, CONVERT(VARCHAR, B.BookingDate, 103) + ' ' + "
                     + " RIGHT(CONVERT(VARCHAR, B.BookingDate, 100), 7) AS 'BookingDate', C.BoatType, D.SeaterType, "
                     + " ISNULL(A.ActualRowerCharge, 0) AS 'ActualRowerCharge', COUNT(*) as 'TripsCount',E.RowerName, "
                     + " E.MobileNo,CONVERT(VARCHAR, S.SettlementDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, S.SettlementDate, 100),7) AS 'SettleDate' ,"
                     + " CONVERT(NVARCHAR(50), A.TripStartTime, 103) + ' ' +RIGHT(CONVERT(VARCHAR, B.BookingDate, 100), 7) AS  'TripDate' from "
                     + " BookingDtl AS A "
                     + " Inner JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                     + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                     + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                     + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = C.BoatHouseId "
                     + " INNER JOIN RowerSettlement AS S ON S.SettlementId = A.SettlementId "
                     + " WHERE A.BoatHouseId = @BoatHouseId AND A.Status IN('B', 'R') AND B.Status IN('B', 'R', 'P') "
                     + " AND A.SettlementId = @SettlementId AND S.SettlementId = @SettlementId AND A.ActualRowerCharge IS NOT NULL AND A.ActualRowerCharge > 0 "
                     + " GROUP BY A.BookingId,A.BookingPin, A.ActualRowerCharge, B.BookingDate, C.BoatType, D.SeaterType, A.ActualRowerCharge,E.RowerName,E.MobileNo,S.SettlementDate,A.TripStartTime)AS A "
                     + " GROUP BY BookingId, ActualRowerCharge, BookingDate, BoatType, SeaterType,BookingPin, ActualRowerCharge,RowerName,MobileNo,SettleDate,TripDate ", con);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@SettlementId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
                    cmd.Parameters["@SettlementId"].Value = boatType.SettlementId.Trim();
                 
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SettlementAmount amt = new SettlementAmount();
                            amt.BookingId = dt.Rows[i]["BookingId"].ToString();
                            amt.NoOfTrips = dt.Rows[i]["TripCount"].ToString();
                            amt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            amt.BoatType = dt.Rows[i]["BoatType"].ToString();
                            amt.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            amt.RowerName = dt.Rows[i]["RowerName"].ToString();
                            amt.CustomerMobile = dt.Rows[i]["MobileNo"].ToString();
                            amt.SettlementDate = dt.Rows[i]["SettleDate"].ToString();
                            amt.TripDate = dt.Rows[i]["TripDate"].ToString();
                            amt.ActualRowerCharge = dt.Rows[i]["ActualRowerCharge"].ToString();
                            li.Add(amt);
                        }

                        SettlementAmountList ConfList = new SettlementAmountList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        SettlementAmountStr ConfRes = new SettlementAmountStr
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    SettlementAmountStr Vehicle = new SettlementAmountStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }



        /*Rower Boat Assign */

        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerBoatAssign")]
        public IHttpActionResult InsRowerBoatAssign([FromBody] BH_RowerBoatAssign enq)
        {
            try
            {
                if (enq.QueryType != "" && enq.RowerId != "" && enq.BoatTypeId != ""
                    && enq.SeaterId != "" && enq.SeaterName != "" && enq.BoatHouseId != "" && enq.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrRowerBoatAssign", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", enq.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", enq.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@RowerId", enq.RowerId.ToString());
                    cmd.Parameters.AddWithValue("@RowerName", enq.RowerName.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", enq.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatType", enq.BoatType.ToString());
                    cmd.Parameters.AddWithValue("@SeaterId", enq.SeaterId.ToString());
                    cmd.Parameters.AddWithValue("@SeaterName", enq.SeaterName.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", enq.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", enq.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", enq.CreatedBy.Trim());
                    cmd.Parameters.AddWithValue("@MultipleTripRights", enq.MultipleTripRights.Trim());

                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con.Close();

                    if (sResult[0].Trim() == "Success")
                    {
                        BH_RowerBoatAssignRes InsCE = new BH_RowerBoatAssignRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        BH_RowerBoatAssignRes InsCE = new BH_RowerBoatAssignRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    BH_RowerBoatAssignRes InsCE = new BH_RowerBoatAssignRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                BH_RowerBoatAssignRes ConfRes = new BH_RowerBoatAssignRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        /// <summary>
        /// Created By : Subalakshmi
        /// Created Date : 2022-04-21
        /// Version : V2
        /// </summary>
        /// <param name="PinDet"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerBoatAssignV2")]
        public IHttpActionResult RowerBoatAssignV2([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                int endcount = Int32.Parse(PinDet.Input1.Trim()) + 9;
                if (PinDet.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("GetReportBasicDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", PinDet.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@Input1", PinDet.Input1.Trim());
                    cmd.Parameters.AddWithValue("@Input2", endcount.ToString());
                    cmd.Parameters.AddWithValue("@Input3", PinDet.Input3.Trim());
                    cmd.Parameters.AddWithValue("@Input4", PinDet.Input4.Trim());
                    cmd.Parameters.AddWithValue("@Input5", PinDet.Input5.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();

                    if (ds != null)
                    {
                        return Ok(ds);
                    }
                    else
                    {
                        return Ok("No Records Found.");
                    }
                }
                else
                {
                    return Ok("Must Pass All Parameters");
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString());
            }
        }


        /// <summary>
        /// Created By : Subalakshmi
        /// Created Date : 2022-04-21
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="PinDet"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerBoatAssignSearchV2")]
        public IHttpActionResult RowerBoatAssignSearchV2([FromBody] CommonAPIMethod PinDet)
        {
            try
            {                
                if (PinDet.BoatHouseId != "" && PinDet.Input1.Trim() != "")
                {
                    List<CommonAPIMethod> li = new List<CommonAPIMethod>();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY B.RowerId) 'RowNumber', * FROM ("
                     + " SELECT UniqueId, RowerId, RowerName, BoatTypeId, BoatType, SeaterId, SeaterName, "
                     + " BoatHouseId, BoatHouseName, ActiveStatus, MultiTripRights, ROW_NUMBER() OVER (ORDER BY RowerId) 'RowNumbers' "
                     + " FROM RowerBoatAssignDetails WITH(NOLOCK) WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId ) AS B "
                     + " Where (B.RowerName LIKE @Input3 or ( B.SeaterName LIKE @Input3 or B.SeaterName LIKE @SeaterName ) " 
                     + " or B.BoatType LIKE @Input3) ) AS A WHERE A.RowNumber BETWEEN @Input1 AND @EndCount", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Input1", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Input3", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@SeaterName", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@EndCount", System.Data.SqlDbType.Int));


                    cmd.Parameters["@BoatHouseId"].Value = PinDet.BoatHouseId.Trim();
                    cmd.Parameters["@Input1"].Value = PinDet.Input1.Trim();
                    cmd.Parameters["@Input3"].Value = PinDet.Input3.Trim() + "%";
                    cmd.Parameters["@SeaterName"].Value = "%" + PinDet.Input3.Trim();
                    cmd.Parameters["@EndCount"].Value = Int32.Parse(PinDet.Input1.Trim()) + 9;

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();

                    if (ds != null)
                    {
                        return Ok(ds);
                    }

                    else
                    {
                        CommonAPIMethodres BoatType = new CommonAPIMethodres
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatType);
                    }
                }
                else
                {
                    CommonAPIMethodres Vehicle = new CommonAPIMethodres
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString());
            }
        }

        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tx"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerBoatAssign/GetRower")]
        public IHttpActionResult RowerBoatAssignGetRower([FromBody] Rower tx)
        {            
            try
            {
                string sQuery = string.Empty;
                if (tx.BoatHouseId != "" && tx.BoatTypeId != "")
                {
                    List<Rower> li = new List<Rower>();
                    con.Open();

                    sQuery = "SELECT RowerId,RowerName FROM RowerMaster "
                    + " WHERE RowerId NOT IN (SELECT RowerId FROM RowerBoatAssignDetails WHERE BoatHouseId = @BoatHouseId  AND "
                    + " BoatTypeId = @BoatTypeId AND ActiveStatus = 'A') AND ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    
                    cmd.Parameters["@BoatHouseId"].Value = tx.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = tx.BoatTypeId.Trim();
                   
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();

                    if (ds != null)
                    {
                        return Ok(ds);
                    }
                    else
                    {
                        return Ok("No Records Found.");
                    }
                }
                else
                {
                    RowerRes Vehicle = new RowerRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                RowerRes ConfRes = new RowerRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }

        }

    }
}

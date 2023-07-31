using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http;
using TTDCAPIv1.Models;

namespace TTDCAPIv1.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class TripSheetController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_BoatHouse"].ConnectionString);

        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);
        // Change Trip sheet Refund Amount

        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetRefundAmt")]
        public IHttpActionResult TripSheetRefundAmt([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.QueryType != "" && Trip.BookingId != "" && Trip.BoatReferenceNo != ""
                    && Trip.ActualBoatId != "" && Trip.RowerId != "" && Trip.BookingMedia != ""
                    && Trip.BoatHouseId != "" && Trip.BoatHouseName != "")
                {
                    string sReturn = string.Empty;

                    SqlCommand cmd = new SqlCommand("ChangeTripSheetWeb", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Trip.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", Trip.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", Trip.BoatReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Trip.BoatHouseName.ToString());

                    cmd.Parameters.AddWithValue("@BoatId", Trip.ActualBoatId.ToString());
                    cmd.Parameters.AddWithValue("@RowerId", Trip.RowerId.ToString());
                    cmd.Parameters.AddWithValue("@BookingMedia", Trip.BookingMedia.ToString());
                    cmd.Parameters.AddWithValue("@Input1", DateTime.Parse(Trip.TripStartTime.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@Input2", DateTime.Parse(Trip.TripEndTime.ToString(), objEnglishDate));

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
                        TripSheetWebString ConMstr = new TripSheetWebString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        TripSheetWebString ConMstr = new TripSheetWebString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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

        //Trip Sheet Web Update
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetWeb/Update")]
        public IHttpActionResult Tripsheetweb([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.QueryType != "" && Trip.BookingId != "" && Trip.BoatReferenceNo != ""
                    && Trip.ActualBoatId != "" && Trip.RowerId != "" && Trip.BookingMedia != ""
                    && Trip.BoatHouseId != "" && Trip.BoatHouseName != "")
                {
                    string sReturn = string.Empty;

                    SqlCommand cmd = new SqlCommand("TripSheetWeb", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Trip.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", Trip.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", Trip.BoatReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Trip.BoatHouseName.ToString());

                    cmd.Parameters.AddWithValue("@BoatId", Trip.ActualBoatId.ToString());
                    cmd.Parameters.AddWithValue("@RowerId", Trip.RowerId.ToString());

                    cmd.Parameters.AddWithValue("@BookingMedia", Trip.BookingMedia.ToString());

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
                        TripSheetWebString ConMstr = new TripSheetWebString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        TripSheetWebString ConMstr = new TripSheetWebString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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

        //Trip Sheet Web Trip End
        /// <summary>
        /// Modified By : Imran
        /// Modified Date : 17-09-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripStart")]
        public IHttpActionResult gettripsheetStart([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();


                    string sQuery = "SELECT * FROM  "
                                    + " (SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                                    + "  C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "  CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "  WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                                    + "  A.Status IN('B') AND A.TripStartTime IS NULL "
                                    + "  UNION ALL "
                                    + "  Select  D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                                    + "  C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + "  CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "   CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' "
                                    + "   FROM BookingDtl AS A  WITH(NOLOCK) "
                                    + "  LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "  where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A')  "
                                    + "   AND A.TripStartTime IS NULL AND A.PremiumStatus != 'I' AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A "
                                    + "  ORDER BY ExpectedTime ASC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Silambarasu D for Booking Pin
        /// Modified Date : 29-03-2022
        /// Modified By : imran for including Booking ID
        /// Modified Date : 2022-04-19  
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripStartSingleV2")]
        public IHttpActionResult gettripsheetStartWithPin([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.BoatHouseId != "" && bHMstr.BookingPin != null && bHMstr.BookingPin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();


                    string sQuery = "SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) as  'RowNumber',* FROM  "
                                    + " (SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                                    + "  C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "  CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "  WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "

                                    + "  A.Status IN('B') AND A.TripStartTime IS NULL AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin ) "

                                    + "  UNION ALL "
                                    + "  Select  D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                                    + "  C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + "  CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "   CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' "
                                    + "   FROM BookingDtl AS A  WITH(NOLOCK) "
                                    + "  LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "  where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A' "

                                    + "  AND (BookingPin = @BookingPin OR BookingId = @BookingPin) )  "

                                    + "  AND A.TripStartTime IS NULL AND A.PremiumStatus != 'I' AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId "

                                    + "  AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin )) AS A "

                                    + "  ORDER BY ExpectedTime ASC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = bHMstr.BookingPin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Imran
        /// Modified Date : 17-09-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripEnd")]
        public IHttpActionResult gettripsheetEnd([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM "
                                    + "  (SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                    + "   A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration,"
                                    + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                    + "   C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + "   CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                    + "   CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                    + "   INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "   LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                    + "   on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "    WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                    + "   AND A.Status IN('B') AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL "
                                    + "  UNION ALL "
                                    + "  SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                    + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                    + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                    + "     C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + "    CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                    + "    CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                    + "    INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "   LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                    + "   on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "   where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                    + "  AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Silambarasu D for Booking Pin
        /// Modified Date : 29-03-2022
        /// Modified By : imran for including Booking ID
        /// Modified Date : 2022-04-19  
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripEndSingleV2")]
        public IHttpActionResult gettripsheetEndWithPin([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.BoatHouseId != "" && bHMstr.BookingPin != null && bHMstr.BookingPin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) as  'RowNumber',* FROM "
                                    + " (SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration,"
                                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                    + " C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                    + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                    + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                    + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                    + " AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin ) "
                                    + " AND A.Status IN('B') AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL "
                                    + " UNION ALL "
                                    + " SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                    + " C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                    + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                    + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                    + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + " where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId "
                                    + " AND (BookingPin = @BookingPin OR BookingId = @BookingPin)"
                                    + " AND ActiveStatus='A') "
                                    + " AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId "
                                    + " AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin )) AS A ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = bHMstr.BookingPin.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Imran
        /// Modified Date : 17-09-2021
        /// Modified By : Silambarasu
        /// Modified Date : 06-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query

        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripClosed")]
        public IHttpActionResult TripClosedGrid([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != null && Trip.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM  "
                                        + " (SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum,  "
                                        + " E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime,  "
                                        + "  A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                                        + "   DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A  "
                                        + "   INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "   INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                                        + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "  LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                                        + "   A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                                        + "   A.BoatHouseId = F.BoatHouseId WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                                        + "   CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND A.PremiumStatus != 'I' AND  "
                                        + "   A.BoatHouseId = @BoatHouseId AND A.Status IN('B')  "
                                        + "  UNION ALL  "
                                        + " SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum,  "
                                        + "   E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime,  "
                                        + "   A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                                        + "   DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A  "
                                        + "  INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "    INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                                        + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "   LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                                        + "   A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                                        + "    A.BoatHouseId = F.BoatHouseId WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                                        + "   A.PremiumStatus != 'I' AND A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK)  "
                                        + "  where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                        + " AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId ) AS A  "
                                        + "   ORDER BY A.TripEndTime DESC ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();
                            ShowTrip.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        /// Modified By : Imran.
        /// Modified Date : 17-09-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripStart")]
        public IHttpActionResult NewgettripsheetStart([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.BoatHouseId != "" && bHMstr.UserId != null && bHMstr.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = " SELECT * FROM  "
                                    + "     (SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + "     A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + "   A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                    + "    G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + "    CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "    CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                    + "     LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "      LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "     INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "    INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "    INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "     INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "     LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "     AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                    + "     AND A.BoatHouseId = H.BoatHouseId "
                                    + "     WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                                    + "    A.Status IN('B') AND A.TripStartTime IS NULL "
                                    + "    UNION ALL "
                                    + "   SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + "   A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + "   A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                    + "   G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + "    CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "    CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                    + "    LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "   LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "    INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "    INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "   INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "   LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "   AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                    + "   AND A.BoatHouseId = H.BoatHouseId "
                                    + "   where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                    + "   AND A.TripStartTime IS NULL AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.PremiumStatus != 'I' AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A "
                                    + "   ORDER BY ExpectedTime ASC ";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.CommandTimeout = 900000;
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Silambarasu D for Booking Pin
        /// Modified Date : 29-03-2022
        /// Modified By : imran for including Booking ID
        /// Modified Date : 2022-04-19
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripStartSingleV2")]
        public IHttpActionResult NewgettripsheetStartWithPin([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BoatHouseId != null && bHMstr.BookingPin != "" && bHMstr.BookingPin != null
                    && bHMstr.UserId != "" && bHMstr.UserId != null)
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = " SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) as  'RowNumber',* FROM  "
                                       + "  (SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                       + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                       + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                       + "  G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                       + "  CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                       + "  CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                       + "  LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                       + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                       + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                       + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                       + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                       + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                       + "  LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                       + "  AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                       + "  AND A.BoatHouseId = H.BoatHouseId "
                                       + "  WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId "

                                       + "  AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin ) "

                                       + "  AND H.ActiveStatus = 'A' AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                                       + "  A.Status IN('B') AND A.TripStartTime IS NULL "
                                       + "   UNION ALL "
                                       + "  SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                       + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                       + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                       + "  G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                       + "  CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                       + "  CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                       + "  LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                       + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                       + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                       + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                       + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                       + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                       + "  LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                       + "  AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                       + "  AND A.BoatHouseId = H.BoatHouseId "
                                       + "  where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                       + "  where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId "

                                       + "  AND (BookingPin = @BookingPin OR BookingId = @BookingPin) "

                                       + "  AND ActiveStatus='A') "
                                       + "  AND A.TripStartTime IS NULL AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.PremiumStatus != 'I' "

                                       + "  AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin ) "

                                       + "  AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A "
                                       + "  ORDER BY ExpectedTime ASC ";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.CommandTimeout = 900000;
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = bHMstr.BookingPin.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Imran.
        /// Modified Date : 17-09-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripEnd")]
        public IHttpActionResult NewgettripsheetEnd([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BoatHouseId != null && bHMstr.UserId != "" && bHMstr.UserId != null)
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand(" SELECT * FROM "
                                                + "   (SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                                + "    A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                                + "     A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                                + "    C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                                + "   CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                                + "    CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                                + "    INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                                + "    LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                                + "     on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                                + "      INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                                + "     LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                                + "     AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                                + "     AND A.BoatHouseId = H.BoatHouseId "
                                                + "     WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' "
                                                + "     AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                                + "    AND A.Status IN('B') AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL "
                                                + "    UNION ALL "
                                                + "    SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                                + "     A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                                + "     A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                                + "     C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                                + "     CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                                + "     CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                                + "    INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                                + "    LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                                + "    on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                                + "    INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                                + "    LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                                + "    AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                                + "    AND A.BoatHouseId = H.BoatHouseId "
                                                + "    WHERE   A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                                + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                                + "   AND H.UserId = @UserId AND H.ActiveStatus = 'A' "
                                                + "    AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();


                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Created By : Vediyappan.V
        /// Created Date : 2022-04-19
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripEndV2")]
        public IHttpActionResult NewgettripsheetEndV2([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.UserId.Trim() != null && bHMstr.CountStart.Trim() != null
                    && bHMstr.BoatHouseId != "" && bHMstr.UserId.Trim() != "" && bHMstr.CountStart.Trim() != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM  "
                                                + "  (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                                                + "  (SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                                + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                                + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                                + "  C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                                + "  CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                                + "  CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                                + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                                + "  LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                                + "  on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                                + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                                + "  LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                                + "  AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                                + "  AND A.BoatHouseId = H.BoatHouseId "
                                                + "  WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' "
                                                + "  AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                                + "  AND A.Status IN('B') AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL "
                                                + "  UNION ALL "
                                                + "  SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                                + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                                + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                                + "  C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                                + "  CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                                + "  CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                                + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                                + "  LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                                + "  on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                                + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                                + "  LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                                + "  AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                                + "  AND A.BoatHouseId = H.BoatHouseId "
                                                + "  WHERE   A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                                + "  where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                                + "  AND H.UserId = @UserId AND H.ActiveStatus = 'A' "
                                                + "  AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL AND A.Status IN('R') "
                                                + "  AND A.BoatHouseId = @BoatHouseId ) AS A ) AS B WHERE B.RowNumber BETWEEN @CountStart AND @CountEnd ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();
                    cmd.Parameters["@CountStart"].Value = bHMstr.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(bHMstr.CountStart.Trim()) + 9;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();

                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Silambarasu D for Booking Pin
        /// Modified Date : 29-03-2022
        /// Modified By : imran for including Booking ID
        /// Modified Date : 2022-04-19      
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripEndSingleV2")]
        public IHttpActionResult NewgettripsheetEndWithPin([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.BookingPin != null && bHMstr.UserId != null &&
                    bHMstr.BoatHouseId != "" && bHMstr.BookingPin != "" && bHMstr.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand(" SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) as  'RowNumber',* FROM "
                                                + " (SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                                + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                                + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                                + " C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                                + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                                + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                                + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                                + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                                + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                                + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                                + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                                + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                                + " AND A.BoatHouseId = H.BoatHouseId "
                                                + " WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' "

                                                + "  AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin ) "

                                                + " AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                                + " AND A.Status IN('B') AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL "
                                                + " UNION ALL "
                                                + " SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime, "
                                                + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                                + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                                + " C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                                + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                                + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A "
                                                + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                                + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                                + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                                + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                                + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                                + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                                + " AND A.BoatHouseId = H.BoatHouseId "
                                                + " WHERE   A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                                + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A' "

                                                + " AND (BookingPin = @BookingPin OR BookingId = @BookingPin) ) "

                                                + " AND H.UserId = @UserId AND H.ActiveStatus = 'A' "
                                                + " AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId "

                                                + "  AND (A.BookingPin = @BookingPin OR A.BookingId = @BookingPin ) ) AS A ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = bHMstr.BookingPin.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Imran.
        /// Modified Date : 17-09-2021
        /// Modified By : Silambarasu.
        /// Modified Date : 06-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripClosed")]
        public IHttpActionResult NewTripClosedGrid([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != null && Trip.BoatHouseId != "" && Trip.UserId != null && Trip.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM "
                                    + "  (SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum, "
                                    + "   E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + "  A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                    + "  DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                                    + "   INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                                    + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                                    + "   LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                                    + "   A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                                    + "  A.BoatHouseId = F.BoatHouseId "
                                    + "  LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "  AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                    + "   AND A.BoatHouseId = H.BoatHouseId "
                                    + "  WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND A.PremiumStatus != 'I' AND "
                                    + "  CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND "
                                    + "   A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.Status IN('B') "
                                    + "   UNION ALL "
                                    + "  SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum, "
                                    + "  E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + "  A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                    + "   DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                                    + "   INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                                    + "  INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                                    + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                                    + "   LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                                    + "  A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                                    + "   A.BoatHouseId = F.BoatHouseId "
                                    + "   LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "   AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value)"
                                    + "   AND A.BoatHouseId = H.BoatHouseId "
                                    + "   WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND A.PremiumStatus != 'I' AND "
                                    + "   A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                    + "   AND A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.Status IN('R')) AS A "
                                    + "   ORDER BY A.TripEndTime DESC ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = Trip.UserId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();
                            ShowTrip.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        /// Created By : Vediyappan.V
        /// Created Date : 2022-04-19
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripClosedV2")]
        public IHttpActionResult NewTripClosedGridV2([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != null && Trip.BoatHouseId != "" && Trip.UserId != null && Trip.UserId != ""
                    && Trip.CountStart != null && Trip.CountStart != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    con.Open();

                    SqlCommand cmd = new SqlCommand(" SELECT * FROM "
                                    + " (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                                    + " (SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum, "
                                    + " E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + " A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                    + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus'"
                                    + " FROM BookingDtl AS A "
                                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                                    + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                                    + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                                    + " A.BoatHouseId = F.BoatHouseId "
                                    + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                    + " AND A.BoatHouseId = H.BoatHouseId "
                                    + " WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND A.PremiumStatus != 'I' AND "
                                    + " CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND "
                                    + " A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.Status IN('B') "
                                    + " UNION ALL "
                                    + " SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum, "
                                    + " E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + " A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                    + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' "
                                    + " FROM BookingDtl AS A "
                                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                                    + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                                    + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                                    + " A.BoatHouseId = F.BoatHouseId "
                                    + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value)"
                                    + " AND A.BoatHouseId = H.BoatHouseId "
                                    + " WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND A.PremiumStatus != 'I' AND "
                                    + " A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                    + " AND A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.Status IN('R')) AS A )"
                                    + " AS B WHERE B.RowNumber BETWEEN @CountStart AND @CountEnd ORDER BY RowNumber ASC, B.TripEndTime DESC ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = Trip.UserId.Trim();
                    cmd.Parameters["@CountStart"].Value = Trip.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(Trip.CountStart.Trim()) + 9;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();
                            ShowTrip.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            ShowTrip.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="rower"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/Rower")]
        public IHttpActionResult getRower([FromBody] Rower rower)
        {
            try
            {
                if (rower.BoatHouseId != "" && rower.BoatHouseId != null)
                {
                    List<Rower> li = new List<Rower>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT RowerId,RowerName FROM RowerMaster WHERE BoatHouseId = @BoatHouseId AND ActiveStatus='A'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = rower.BoatHouseId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Rower row = new Rower();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();

                            li.Add(row);
                        }

                        RowerList ConfList = new RowerList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        RowerRes ConfRes = new RowerRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
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
                TripSheetWebString ConfRes = new TripSheetWebString
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

        //Trip Sheet Settlement Details on Boat House

        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDet")]
        public IHttpActionResult getBookingDet([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BoatHouseId != null)
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = " SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate', A.BookingDuration, A.BookingId, A.BoatReferenceNo, "
                        + " A.BookingPin, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, "
                        + " ISNULL(E.RowerName,'-') AS 'RowerName', "
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile, A.RefundRePrint"
                        + " FROM BookingDtl AS A"
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId "
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N'";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.RStatus = dt.Rows[i]["RefundRePrint"].ToString();

                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Created By : Silambarasu
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetV2")]
        public IHttpActionResult getBookingDetV2([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.CountStart != "" && tripBook.BoatHouseId != null
                    && tripBook.CountStart != null)
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();
                    string sQuery = string.Empty;

                    sQuery = "SELECT * FROM "
                        + " (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                        + " (SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate', A.BookingDuration, A.BookingId, A.BoatReferenceNo, "
                        + " A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile,A.RefundRePrint"
                        + " FROM BookingDtl AS A"
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId"
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N' ) AS A)"
                        + " AS B WHERE B.RowNumber BETWEEN @CountStart AND @CountEnd";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@CountStart"].Value = tripBook.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(tripBook.CountStart.Trim()) + 9;


                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.RStatus = dt.Rows[i]["RefundRePrint"].ToString();
                            row.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="rower"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/RowerBoatAssign")]
        public IHttpActionResult getRowerType([FromBody] Rower rower)
        {
            try
            {

                if (rower.BoatHouseId != "" && rower.BoatTypeId != "" && rower.BoatSeaterId != "" &&
                    rower.BoatHouseId != null && rower.BoatTypeId != null && rower.BoatSeaterId != null)
                {
                    List<Rower> li = new List<Rower>();
                    con.Open();

                    string sQuery = "SELECT AutoEndForNoDeposite FROM BHMaster WHERE BoatHouseId = @BoatHouseId ";
                    DataTable Dtck = new DataTable();
                    SqlCommand cmdck = new SqlCommand(sQuery, con);
                    cmdck.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmdck.Parameters["@BoatHouseId"].Value = rower.BoatHouseId.Trim();

                    SqlDataAdapter dachk = new SqlDataAdapter(cmdck);
                    dachk.Fill(Dtck);
                    SqlCommand cmd = new SqlCommand();
                    if (Dtck.Rows.Count > 0)
                    {
                        if (Dtck.Rows[0]["AutoEndForNoDeposite"].ToString().Trim() == "Y")
                        {
                            cmd = new SqlCommand("SELECT RowerId, RowerName FROM RowerBoatAssignDetails WHERE EXISTS "
                                               + " (SELECT * FROM STRING_SPLIT(SeaterId, ',') WHERE value IN(@BoatSeaterId)) "
                                               + " AND BoatTypeId IN(@BoatTypeId) AND BoatHouseId = @BoatHouseId", con);
                        }
                        else
                        {
                            //cmd = new SqlCommand("SELECT RowerId, RowerName FROM RowerBoatAssignDetails WHERE EXISTS "
                            //   + " (SELECT * FROM STRING_SPLIT(SeaterId, ',') WHERE value IN('" + rower.BoatSeaterId.Trim() + "')) "
                            //   + " AND BoatTypeId IN('" + rower.BoatTypeId.Trim() + "') AND BoatHouseId = '" + rower.BoatHouseId.Trim() + "'"
                            //   + " AND RowerId NOT IN (SELECT RowerId FROM BookingDtl AS A INNER JOIN BookingHdr AS B "
                            //   + " ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                            //   + " WHERE CAST(B.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND TripStartTime IS NOT NULL AND "
                            //   + " TripEndTime IS NULL AND RowerId IS NOT NULL)", con);

                            //Change by Imran on 2021-11-08 
                            cmd = new SqlCommand("SELECT DISTINCT(RowerId),RowerName FROM "
                                       + " (SELECT RowerId, RowerName FROM RowerBoatAssignDetails WHERE EXISTS "
                                       + " (SELECT * FROM STRING_SPLIT(SeaterId, ',') WHERE value IN(@BoatSeaterId)) "
                                       + " AND BoatTypeId IN(@BoatTypeId) AND BoatHouseId = @BoatHouseId "
                                       + " AND RowerId NOT IN(SELECT RowerId FROM BookingDtl AS A INNER JOIN BookingHdr AS B "
                                       + " ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                                       + " WHERE CAST(B.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND TripStartTime IS NOT NULL AND "
                                       + " TripEndTime IS NULL AND RowerId IS NOT NULL) "
                                       + " UNION ALL "
                                       + " SELECT RowerId, RowerName FROM RowerBoatAssignDetails WHERE EXISTS "
                                       + " (SELECT * FROM STRING_SPLIT(SeaterId, ',') WHERE value IN(@BoatSeaterId)) "
                                       + " AND BoatTypeId IN(@BoatTypeId) AND BoatHouseId =@BoatHouseId "
                                       + " AND RowerId IN(SELECT RowerId FROM RowerBoatAssignDetails WHERE "
                                       + " BoatHouseId = @BoatHouseId AND MultiTripRights = 'Y' "
                                       + " and RowerId not in "
                                       + " (select RowerId from BookingDtl where CAST(BDate AS DATE) =  CAST(GETDATE() AS DATE) AND "
                                       + " TripStartTime IS NOT NULL AND "
                                       + " TripEndTime IS NULL AND BoatTypeId NOT IN (SELECT DISTINCT(BoatTypeId) FROM RowerBoatAssignDetails WHERE "
                                       + " BoatHouseId = @BoatHouseId AND MultiTripRights = 'Y')  ))) AS A", con);
                        }
                    }

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = rower.BoatHouseId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = rower.BoatSeaterId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = rower.BoatTypeId.Trim();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Rower row = new Rower();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();

                            li.Add(row);
                        }

                        RowerList ConfList = new RowerList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        RowerRes ConfRes = new RowerRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="rower"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/RowerBoatBasedAssign")]
        public IHttpActionResult getRowerBoatBasedType([FromBody] Rower rower)
        {
            try
            {
                if (rower.BoatHouseId != "" && rower.BoatTypeId != "" && rower.BoatSeaterId != "" && rower.RowerId != "" &&
                   rower.BoatHouseId != null && rower.BoatTypeId != null && rower.BoatSeaterId != null && rower.RowerId != null)
                {
                    List<Rower> li = new List<Rower>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT RowerId, RowerName FROM RowerBoatAssignDetails WHERE EXISTS "
                                   + " (SELECT * FROM STRING_SPLIT(SeaterId, ',') WHERE value IN(@BoatSeaterId)) "
                                   + " AND BoatTypeId IN(@BoatTypeId) AND BoatHouseId = @BoatHouseId"
                                   + " AND RowerId = @RowerId", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = rower.BoatHouseId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = rower.BoatSeaterId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = rower.BoatTypeId.Trim();
                    cmd.Parameters["@RowerId"].Value = rower.RowerId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Rower row = new Rower();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();

                            li.Add(row);
                        }

                        RowerList ConfList = new RowerList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        RowerRes ConfRes = new RowerRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPin")]
        public IHttpActionResult getBookingDetByPin([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingPin != "" && tripBook.BookingId != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    //sQuery = " SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BoatReferenceNo, A.BookingPin,A.BoatTypeId, B.BoatType,"
                    //    + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                    //    + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                    //    + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                    //    + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                    //    + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                    //    + " D.CustomerMobile FROM BookingDtl AS A"
                    //    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                    //    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                    //    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                    //    + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                    //    + " WHERE A.BoatHouseId = '" + tripBook.BoatHouseId + "' "
                    //    + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL"
                    //    + " AND A.ActualBoatCharge IS NOT NULL AND A.ActualRowerCharge IS NOT NULL AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE)"
                    //    + " AND ISNULL(A.BoatDeposit,0) > 0 AND A.BookingPin = '" + tripBook.BookingPin + "'";

                    sQuery = " SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, "
                        + " A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile,A.RefundRePrint"
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId "
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND D.Status IN ('B', 'R', 'P') "
                        + " AND A.Status IN ('B', 'R') AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND "
                        + " (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N' "
                        + " AND A.BookingPin = @BookingPin AND A.BookingId = @BookingId "
                        + " AND D.BookingId = @BookingId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingId"].Value = tripBook.BookingId.Trim();
                    cmd.Parameters["@BookingPin"].Value = tripBook.BookingPin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.RStatus = dt.Rows[i]["RefundRePrint"].ToString();
                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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

        //Trip Sheet Settlement Details
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/Details")]
        public IHttpActionResult getDetails([FromBody] BoatCharges enq)
        {
            try
            {

                if (enq.QueryType != "" && enq.BoatReferenceNo != "" && enq.BoatHouseId != "" && enq.BookingPin != "" && enq.BookingId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ViewTripSheetSettelement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", enq.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", enq.BoatReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", enq.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", enq.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", enq.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BookingPin", enq.BookingPin.ToString());

                    List<BoatCharges> li = new List<BoatCharges>();
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (enq.QueryType == "GetBoatCharge")
                            {
                                BoatCharges row = new BoatCharges();
                                row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                                row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                                row.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();

                                row.BoatMinCharge = dt.Rows[i]["BoatCharge"].ToString();

                                row.BoatExtnTime = dt.Rows[i]["BoatExtnTime"].ToString();
                                row.BoatGraceTime = dt.Rows[i]["BoatGraceTime"].ToString();
                                row.Totaltime = dt.Rows[i]["Totaltime"].ToString();

                                row.ActualNetAmt = dt.Rows[i]["ActualNetAmt"].ToString();

                                row.ExtensionChargePerMin = dt.Rows[i]["ExtensionChargePerMin"].ToString();

                                row.TimeDiff = dt.Rows[i]["TimeDiff"].ToString();
                                row.ExtraTime = dt.Rows[i]["ExtraTime"].ToString();
                                row.ExtraExtCharge = dt.Rows[i]["ExtraExtCharge"].ToString();
                                row.TotalNetAmount = dt.Rows[i]["TotalNetAmount"].ToString();
                                row.ExtensionMsg = dt.Rows[i]["ExtensionMsg"].ToString();

                                li.Add(row);
                            }
                            else if (enq.QueryType == "GetOfferAmount")
                            {
                                BoatCharges row = new BoatCharges();
                                row.OfferAmount = dt.Rows[i]["ActualOfferAmount"].ToString();

                                li.Add(row);
                            }
                            else if (enq.QueryType == "GetNetAmount")
                            {
                                BoatCharges row = new BoatCharges();
                                row.ActualNetAmt = dt.Rows[i]["InitNetAmount"].ToString();

                                li.Add(row);
                            }
                            else if (enq.QueryType == "GetTexboxDetails")
                            {
                                BoatCharges row = new BoatCharges();

                                row.BookingDate = dt.Rows[i]["BookingDate"].ToString();

                                row.BookingId = dt.Rows[i]["BookingId"].ToString();
                                row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                                row.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                                row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                                row.BoatNumber = dt.Rows[i]["ActualBoatNum"].ToString();
                                row.RowerId = dt.Rows[i]["RowerId"].ToString();

                                row.InitBoatCharge = dt.Rows[i]["InitialBoatCharge"].ToString();
                                row.InitRowerCharge = dt.Rows[i]["InitialRowerCharge"].ToString();
                                row.BoatDeposit = dt.Rows[i]["Deposit"].ToString();

                                row.InitOfferAmount = dt.Rows[i]["InitialOfferAmount"].ToString();
                                row.InitNetAmount = dt.Rows[i]["InitialNetAmount"].ToString();

                                row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                                row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();

                                row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                                row.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                                row.CustomerAddress = dt.Rows[i]["CustomerAddress"].ToString();
                                row.PaymentTypeId = dt.Rows[i]["PaymentTypeId"].ToString();
                                row.PaymentType = dt.Rows[i]["PaymentTypeName"].ToString();

                                li.Add(row);
                            }
                            else if (enq.QueryType == "GetCustomerDetails")
                            {
                                BoatCharges row = new BoatCharges();
                                row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                                row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                                row.PaymentType = dt.Rows[i]["ConfigName"].ToString();

                                li.Add(row);
                            }
                            else if (enq.QueryType == "GetRefund")
                            {
                                BoatCharges row = new BoatCharges();

                                row.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();
                                row.MinutesTaken = dt.Rows[i]["MinutesTaken"].ToString();
                                row.ExtraMinutes = dt.Rows[i]["ExtraMinutes"].ToString();
                                row.Deposit = dt.Rows[i]["Deposit"].ToString();
                                row.TotalDeduction = dt.Rows[i]["TotalDeduction"].ToString();
                                row.BoatDeduction = dt.Rows[i]["BoatDeduction"].ToString();
                                row.Rowerdeduction = dt.Rows[i]["Rowerdeduction"].ToString();
                                row.TaxDeduction = dt.Rows[i]["TaxDeduction"].ToString();
                                row.RefundAmount = dt.Rows[i]["RefundAmount"].ToString();
                                row.TimeExtension = dt.Rows[i]["TimeExtension"].ToString();
                                row.ExtensionSlabId = dt.Rows[i]["ExtensionSlabId"].ToString();
                                row.ExtensionMsg = dt.Rows[i]["ExtensionMsg"].ToString();
                                row.CollectedBalance = dt.Rows[i]["CollectedBalance"].ToString();

                                li.Add(row);
                            }
                        }

                        BoatChargesList ConfList = new BoatChargesList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        string msg = string.Empty;
                        if (enq.QueryType == "GetRefund")
                        {
                            msg = "Boat Charges Not Available.";
                        }
                        else if (enq.QueryType == "GetRowerCharge")
                        {
                            msg = "Rower Charges Not Available.";
                        }
                        else
                        {
                            msg = "No Records Found.";
                        }
                        BoatChargesRes ConfRes = new BoatChargesRes
                        {
                            Response = msg,
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatChargesRes InsCE = new BoatChargesRes
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

        //Trip Sheet Settlement Update
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/Update")]
        public IHttpActionResult UpdateTripSheet([FromBody] UpdateSheet enq)
        {
            try
            {

                if (enq.QueryType != "" && enq.BoatHouseId != "" && enq.BookingId != "" && enq.BoatReferenceNo != "" && enq.BookingPin != ""
                    && enq.ActualNetAmountExtn != "" && enq.ActualBoatChargeExtn != "" && enq.ActualRowerChargeExtn != "" && enq.ActualOfferAmountExtn != ""
                    && enq.ActualTaxExtn != "" && enq.DepRefundAmount != "" && enq.RePaymentType != "" && enq.CreatedBy != "")

                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("DepositRefundCalculation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", enq.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", enq.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", enq.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", enq.BoatReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@BookingPin", enq.BookingPin.ToString());

                    cmd.Parameters.AddWithValue("@ActualNetAmountExtn", enq.ActualNetAmountExtn.ToString());
                    cmd.Parameters.AddWithValue("@ActualBoatChargeExtn", enq.ActualBoatChargeExtn.ToString());
                    cmd.Parameters.AddWithValue("@ActualRowerChargeExtn", enq.ActualRowerChargeExtn.ToString());
                    cmd.Parameters.AddWithValue("@ActualOfferAmountExtn", enq.ActualOfferAmountExtn.ToString());
                    cmd.Parameters.AddWithValue("@ActualTaxExtn", enq.ActualTaxExtn.ToString());

                    cmd.Parameters.AddWithValue("@DepRefundAmount", enq.DepRefundAmount.ToString());
                    cmd.Parameters.AddWithValue("@RePaymentType", enq.RePaymentType.ToString());

                    cmd.Parameters.AddWithValue("@CustomerMobile", enq.CustomerMobile.ToString());

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
                        UpdateSheetRes InsCE = new UpdateSheetRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        UpdateSheetRes InsCE = new UpdateSheetRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    UpdateSheetRes InsCE = new UpdateSheetRes
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

        //Trip Sheet Settlement List
        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="enq"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/BindListAll")]
        public IHttpActionResult TripSheetSettelementGrid([FromBody] UpdateSheet enq)
        {
            try
            {
                if (enq.BoatHouseId != "" && enq.FromDate != "" && enq.ToDate != "")
                {
                    List<UpdateSheet> li = new List<UpdateSheet>();
                    con.Open();
                    string sQuery = string.Empty;

                    sQuery = " SELECT A.BookingId, A.BoatReferenceNo, A.BookingPin, A.ActualBoatId, A.ActualBoatNum, B.BoatName, A.RowerId, ISNULL(C.RowerName,'-') AS 'RowerName', "
                        + " ISNULL(convert(varchar(10), TripStartTime, 103) + right(convert(varchar(32), TripStartTime, 100), 8), '-')   AS 'TripStartTime',"
                        + " ISNULL(convert(varchar(10), TripEndTime, 103) + right(convert(varchar(32), TripEndTime, 100), 8), '-')   AS 'TripEndTime', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference',  "
                        + " A.ActualNetAmount, ISNULL(A.DepRefundAmount,0) as 'DepRefundAmount', A.RePaymentType, D.ConfigName as 'RepaymentName' , "
                        + " ISNULL(convert(varchar(10), A.DepRefundDate, 103) + right(convert(varchar(32), A.DepRefundDate, 100), 8), '-')   AS 'DepRefundDate' "
                        + " FROM BookingDtl AS A INNER JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS C ON C.RowerId = A.RowerId AND C.BoatHouseId = A.BoatHouseId"
                        + " INNER JOIN ConfigurationMaster AS D ON A.RePaymentType = D.ConfigID AND TypeID='20'"
                        + " WHERE BoatReferenceNo IS NOT NULL "
                        + " AND  TripStartTime IS NOT NULL AND TripEndTime IS NOT NULL AND A.BoatHouseId = @BoatHouseId AND "
                        + " B.BoatHouseId = @BoatHouseId AND A.Status IN ('B', 'R') "
                        + " AND CAST(TripStartTime AS DATE) BETWEEN @FromDate AND @ToDate";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = enq.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(enq.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(enq.ToDate, objEnglishDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UpdateSheet row = new UpdateSheet();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.BoatName = dt.Rows[i]["BoatName"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.ActualNetAmount = dt.Rows[i]["ActualNetAmount"].ToString();
                            row.DepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                            row.RePaymentType = dt.Rows[i]["RepaymentName"].ToString();
                            row.DepRefundDate = dt.Rows[i]["DepRefundDate"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();

                            li.Add(row);
                        }

                        UpdateSheetList ConfList = new UpdateSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        UpdateSheetRes ConfRes = new UpdateSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    UpdateSheetRes Vehicle = new UpdateSheetRes
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
        /// Created By : Silambarasu
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified By : Silambarasu
        /// Modified Date : 2022-04-27
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="enq"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/BindListAllV2")]
        public IHttpActionResult TripSheetSettelementGridV2([FromBody] UpdateSheet enq)
        {
            try
            {
                if (enq.BoatHouseId != "" && enq.FromDate != "" && enq.ToDate != "" && enq.CountStart != "")
                {
                    List<UpdateSheet> li = new List<UpdateSheet>();
                    con.Open();
                    string sQuery = string.Empty;

                    sQuery = "SELECT * FROM "
                        + " (SELECT ROW_NUMBER() OVER(ORDER BY OrderDate DESC) 'RowNumber', * FROM  "
                        + " ( SELECT A.BookingId, A.BoatReferenceNo, A.BookingPin, A.ActualBoatId, A.ActualBoatNum, B.BoatName, A.RowerId, ISNULL(C.RowerName,'-') AS 'RowerName', "
                        + " ISNULL(convert(varchar(10), TripStartTime, 103) + right(convert(varchar(32), TripStartTime, 100), 8), '-')   AS 'TripStartTime',"
                        + " ISNULL(convert(varchar(10), TripEndTime, 103) + right(convert(varchar(32), TripEndTime, 100), 8), '-')   AS 'TripEndTime', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference',  "
                        + " A.ActualNetAmount, ISNULL(A.DepRefundAmount,0) as 'DepRefundAmount', A.RePaymentType, D.ConfigName as 'RepaymentName' , "
                        + " ISNULL(convert(varchar(10), A.DepRefundDate, 103) + right(convert(varchar(32), A.DepRefundDate, 100), 8), '-')   AS 'DepRefundDate', A.DepRefundDate as 'OrderDate' "
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS C ON C.RowerId = A.RowerId AND C.BoatHouseId = A.BoatHouseId"
                        + " INNER JOIN ConfigurationMaster AS D ON A.RePaymentType = D.ConfigID AND TypeID='20'"
                        + " WHERE BoatReferenceNo IS NOT NULL "
                        + " AND TripStartTime IS NOT NULL AND TripEndTime IS NOT NULL AND A.BoatHouseId = @BoatHouseId AND "
                        + " B.BoatHouseId = @BoatHouseId AND A.Status IN ('B', 'R') "
                        + " AND CAST(TripStartTime AS DATE) BETWEEN @FromDate AND @ToDate ) AS A)"
                        + " AS B WHERE B.RowNumber BETWEEN @CountStart AND @CountEnd";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = enq.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(enq.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(enq.ToDate, objEnglishDate);
                    cmd.Parameters["@CountStart"].Value = enq.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(enq.CountStart.Trim()) + 9;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UpdateSheet row = new UpdateSheet();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.BoatName = dt.Rows[i]["BoatName"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.ActualNetAmount = dt.Rows[i]["ActualNetAmount"].ToString();
                            row.DepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                            row.RePaymentType = dt.Rows[i]["RepaymentName"].ToString();
                            row.DepRefundDate = dt.Rows[i]["DepRefundDate"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(row);
                        }

                        UpdateSheetList ConfList = new UpdateSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        UpdateSheetRes ConfRes = new UpdateSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    UpdateSheetRes Vehicle = new UpdateSheetRes
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



        //Trip Sheet Web Trip Start
        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ScanTripStart")]
        public IHttpActionResult GetTripStart([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = "SELECT D.BookingId,A.BookingSerial,D.UserId,CONVERT(NVARCHAR,CAST(D.BookingDate AS TIME),100) AS 'BoardingTime', "
                    + " A.BoatHouseId,A.BoatHouseName,A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType,B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                    + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100) AS 'TripStartTime', "
                    + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                    + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime'  FROM BookingDtl AS A "
                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                    + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                    + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                    + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                    + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND "
                    + " A.Status IN('B', 'R') AND A.BookingPin= @BarcodePin ORDER BY ExpectedTime ASC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ScanTripEnd")]
        public IHttpActionResult GetScanEnd([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand(" SELECT D.BookingId,A.BookingSerial,D.UserId,convert(nvarchar,CAST(D.BookingDate as time),100) as BoardingTime, "
                            + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                            + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType,B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                            + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100) AS 'TripStartTime', "
                            + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                            + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime'  FROM BookingDtl as A "
                            + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                            + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                            + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                            + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                            + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                            + " AND A.Status IN('B', 'R') AND A.BookingPin = @BarcodePin AND A.TripStartTime != '' AND A.TripEndTime IS NULL  ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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

        /**** Deposit Refund Report ****/
        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="enq"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("DepositRefundCash/RPT")]
        public IHttpActionResult DepositRefundcashRpt([FromBody] UpdateSheet enq)
        {
            try
            {
                if (enq.BoatHouseId != "" && enq.FromDate != "" && enq.ToDate != "")
                {
                    List<UpdateSheet> li = new List<UpdateSheet>();
                    con.Open();
                    string sQuery = string.Empty;

                    sQuery = " SELECT A.BookingId,A.BoatHouseName,E.SeaterType,SUBSTRING(F.BoatType, 1, CHARINDEX(' ', F.BoatType)) as 'BoatType', "
                        + " ISNULL(SUM(A.DepRefundAmount), 0) as 'DepRefundAmount' "
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId AND B.BoatHouseId = A.BoatHouseId "
                        + " LEFT JOIN RowerMaster AS C ON C.RowerId = A.RowerId AND C.BoatHouseId = A.BoatHouseId "
                        + " INNER JOIN ConfigurationMaster AS D ON A.RePaymentType = D.ConfigID AND TypeID = '20' "
                        + " INNER JOIN BoatSeat as E On A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId and E.ActiveStatus = 'A' "
                        + " INNER JOIN BoatTypes AS F on A.BoatTypeId = F.BoatTypeId "
                        + " WHERE BoatReferenceNo IS NOT NULL AND A.Status IN ('B', 'R') AND TripStartTime IS NOT NULL "
                        + " AND TripEndTime IS NOT NULL AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId "
                        + " AND CAST(TripStartTime AS DATE) BETWEEN @FromDate AND @ToDate "
                        + " GROUP BY A.BookingId, A.BoatHouseName, E.SeaterType, F.BoatType ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = enq.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(enq.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(enq.ToDate, objEnglishDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UpdateSheet row = new UpdateSheet();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.DepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            li.Add(row);
                        }

                        UpdateSheetList ConfList = new UpdateSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        UpdateSheetRes ConfRes = new UpdateSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    UpdateSheetRes Vehicle = new UpdateSheetRes
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

        //public IHttpActionResult DepositRefundcashRpt([FromBody] UpdateSheet enq)
        //{
        //    try
        //    {
        //        if (enq.BoatHouseId != "" && enq.FromDate != "" && enq.ToDate != "")
        //        {
        //            List<UpdateSheet> li = new List<UpdateSheet>();
        //            con.Open();
        //            string sQuery = string.Empty;

        //            sQuery = " SELECT A.BookingId,A.BoatHouseName,E.SeaterType,SUBSTRING(F.BoatType, 1, CHARINDEX(' ', F.BoatType)) as 'BoatType',    "
        //                     + " ISNULL(SUM(A.DepRefundAmount), 0) as 'DepRefundAmount' "
        //                     + " FROM BookingDtl AS A INNER JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
        //                     + "LEFT JOIN RowerMaster AS C ON C.RowerId = A.RowerId AND C.BoatHouseId = A.BoatHouseId INNER JOIN ConfigurationMaster "
        //                     + " AS D ON A.RePaymentType = D.ConfigID AND TypeID = '20'  Inner Join BoatSeat as E On A.BoatSeaterId = E.BoatSeaterId "
        //                     + " And A.BoatHouseId = E.BoatHouseId and E.ActiveStatus = 'A'  Inner Join BoatTypes AS F on A.BoatTypeId = F.BoatTypeId "
        //                     + " WHERE BoatReferenceNo IS NOT NULL AND  TripStartTime IS NOT NULL AND TripEndTime AND A.Status IN ('B', 'R') "
        //                     + " IS NOT NULL AND A.BoatHouseId = '" + enq.BoatHouseId.Trim() + "' AND B.BoatHouseId = '" + enq.BoatHouseId.Trim() + "'  AND "
        //                     + " CAST(TripStartTime AS DATE)BETWEEN '" + DateTime.Parse(enq.FromDate, objEnglishDate) + "'  AND  '" + DateTime.Parse(enq.FromDate, objEnglishDate) + "' "
        //                     + " GROUP BY A.BookingId,A.BoatHouseName,E.SeaterType,F.BoatType ";

        //            SqlCommand cmd = new SqlCommand(sQuery, con);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    UpdateSheet row = new UpdateSheet();
        //                    row.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    row.DepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
        //                    row.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                    row.SeaterType = dt.Rows[i]["SeaterType"].ToString();
        //                    row.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                    li.Add(row);
        //                }

        //                UpdateSheetList ConfList = new UpdateSheetList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(ConfList);
        //            }

        //            else
        //            {
        //                UpdateSheetRes ConfRes = new UpdateSheetRes
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(ConfRes);
        //            }
        //        }
        //        else
        //        {
        //            UpdateSheetRes Vehicle = new UpdateSheetRes
        //            {
        //                Response = "Must Pass All Parameters",
        //                StatusCode = 0
        //            };
        //            return Ok(Vehicle);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        //            StatusCode = 0
        //        };
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return Ok(ConfRes);
        //    }
        //}

        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="enq"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("DepositRefundCash/NetTotal")]
        public IHttpActionResult DepositRefundNetTotal([FromBody] UpdateSheet enq)
        {
            try
            {
                if (enq.BoatHouseId != "" && enq.FromDate != "" && enq.ToDate != "")
                {
                    List<UpdateSheet> li = new List<UpdateSheet>();
                    con.Open();
                    string sQuery = string.Empty;

                    sQuery = " SELECT F.BoatTypeId,SUBSTRING(F.BoatType, 1, CHARINDEX(' ', F.BoatType)) as 'BoatType', "
                        + " ISNULL(SUM(A.ActualNetAmount),0) as 'NetAmount' "
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                        + " LEFT JOIN RowerMaster AS C ON C.RowerId = A.RowerId AND C.BoatHouseId = A.BoatHouseId "
                        + " INNER JOIN ConfigurationMaster AS D ON A.RePaymentType = D.ConfigID AND TypeID = '20' "
                        + " INNER JOIN BoatTypes AS F on A.BoatTypeId = F.BoatTypeId  And A.BoatHouseId = F.BoatHouseId "
                        + " AND F.ActiveStatus = 'A' WHERE BoatReferenceNo IS NOT NULL AND A.Status IN ('B', 'R') AND "
                        + " TripStartTime IS NOT NULL AND TripEndTime IS NOT NULL AND A.BoatHouseId = @BoatHouseId AND "
                        + " B.BoatHouseId = @BoatHouseId AND "
                        + " CAST(TripStartTime AS DATE) BETWEEN @FromDate  AND @ToDate GROUP BY  F.BoatTypeId, F.BoatType";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = enq.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(enq.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(enq.ToDate, objEnglishDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UpdateSheet row = new UpdateSheet();

                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                            li.Add(row);
                        }

                        UpdateSheetList ConfList = new UpdateSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        UpdateSheetRes ConfRes = new UpdateSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    UpdateSheetRes Vehicle = new UpdateSheetRes
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="enq"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("DepositRefundCashInHand")]
        public IHttpActionResult DepositRefundTotalCalculation([FromBody] UpdateSheet enq)
        {
            try
            {
                if (enq.BoatHouseId != "" && enq.FromDate != "" && enq.ToDate != "")
                {
                    List<UpdateSheet> li = new List<UpdateSheet>();
                    con.Open();
                    string sQuery = string.Empty;

                    sQuery = " SELECT ISNULL(SUM(A.ActualNetAmount),0) AS 'TotalNetAmount', ISNULL(Sum(A.DepRefundAmount),0) as 'TotalDeRefund', "
                            + " ISNULL(SUM(A.ActualNetAmount), 0) AS 'CashonHand', A.BoatHouseName, A.GSTNumber "
                            + " FROM ( SELECT A.BookingId, A.BoatHouseId, A.ActualNetAmount, A.BoatHouseName, G.GSTNumber, "
                            + " ISNULL(A.DepRefundAmount, 0) as 'DepRefundAmount'  FROM BookingDtl AS A INNER JOIN BoatMaster "
                            + " AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId  LEFT JOIN RowerMaster AS C ON "
                            + " C.RowerId = A.RowerId AND C.BoatHouseId = A.BoatHouseId INNER JOIN ConfigurationMaster   AS "
                            + " D ON A.RePaymentType = D.ConfigID AND TypeID = '20'  Inner Join BHMaster AS G on A.BoatHouseId = G.BoatHouseId "
                            + " And G.ActiveStatus = 'A' WHERE BoatReferenceNo IS NOT NULL AND TripStartTime "
                            + " IS NOT NULL AND TripEndTime IS NOT NULL  AND A.BoatHouseId = @BoatHouseId "
                            + " AND B.BoatHouseId = @BoatHouseId  AND "
                            + " CAST(TripStartTime AS DATE)BETWEEN @FromDate "
                            + " AND @ToDate AND A.Status IN ('B', 'R') ) AS A "
                            + " Group By A.BoatHouseName, A.GSTNumber";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = enq.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(enq.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(enq.ToDate, objEnglishDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UpdateSheet row = new UpdateSheet();

                            row.TotalNetAmount = dt.Rows[i]["TotalNetAmount"].ToString();
                            row.TotalDefundAmount = dt.Rows[i]["TotalDeRefund"].ToString();
                            row.CashOnHand = dt.Rows[i]["CashonHand"].ToString();
                            row.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            row.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();
                            li.Add(row);
                        }

                        UpdateSheetList ConfList = new UpdateSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        UpdateSheetRes ConfRes = new UpdateSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    UpdateSheetRes Vehicle = new UpdateSheetRes
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
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ScanTripStartedList")]
        public IHttpActionResult GetTripStartedList([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = "SELECT Top 5 * FROM ( "
                    + " SELECT Top 5 D.BookingId,A.BookingSerial,D.UserId,CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime',  "
                    + " A.BoatHouseId,A.BoatHouseName,A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge,  "
                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType,B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                    + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100)  AS 'TripStartTime',  "
                    + " CONVERT(nvarchar, CAST(A.TripEndTime as time), 100)  AS 'TripEndTime',  "
                    + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime'  FROM BookingDtl AS A "
                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                    + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                    + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                    + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                    + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                    + " A.Status IN('B') AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NULL ORDER BY A.TripStartTime DESC "
                    + " UNION ALL "
                    + " SELECT Top 5 D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                    + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100)  AS 'TripStartTime', "
                    + " CONVERT(nvarchar, CAST(A.TripEndTime as time), 100)  AS 'TripEndTime', "
                    + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime'  FROM BookingDtl AS A "
                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                    + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                    + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                    + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                    + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId "
                    + " AND A.PremiumStatus != 'I' AND "
                    + " A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                    + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and "
                    + " BoatHouseId = @BoatHouseId) AND "
                    + " A.Status IN('R') AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NULL ORDER BY A.TripStartTime DESC ) AS A "
                    + " ORDER BY A.TripStartTime DESC  ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ScanTripEndDuration")]
        public IHttpActionResult GetScanEndDuration([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("SELECT D.BookingId,A.BookingSerial,D.UserId,CONVERT(nvarchar,CAST(D.BookingDate as time),100) as BoardingTime, "
                                   + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                   + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                   + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                   + " CONVERT(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                   + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TraveledTime', "
                                   + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime' "
                                   + " FROM BookingDtl as A "
                                   + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                   + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                   + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                   + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                   + " WHERE A.BoatHouseId = @BoatHouseId AND "
                                   + " CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                   + " AND A.Status IN('B', 'R') AND A.BookingPin = @BarcodePin AND A.TripStartTime != '' AND A.TripEndTime IS NOT NULL  ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.TraveledTime = dt.Rows[i]["TraveledTime"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListTop5TripClosed")]
        public IHttpActionResult TripEndedList([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT Top 5 * FROM("
                             + " SELECT Top 5 A.BookingId,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType, A.BookingDuration,A.ActualBoatNum, "
                             + " E.BoatNum,A.RowerId,F.RowerName,Convert(nvarchar,CAST(A.TripStartTime as time),100) AS TripStartTime,  "
                             + " A.BookingPin,convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                             + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes' FROM BookingDtl AS A  "
                             + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                             + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                             + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                             + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                             + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                             + " A.BoatHouseId = F.BoatHouseId WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                             + " A.Status IN('B') AND "
                             + " CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND  "
                             + " A.BoatHouseId = @BoatHouseId ORDER BY A.TripEndTime DESC  "
                             + " UNION ALL "
                             + " SELECT Top 5 A.BookingId,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType, A.BookingDuration,A.ActualBoatNum,  "
                             + " E.BoatNum,A.RowerId,F.RowerName,Convert(nvarchar,CAST(A.TripStartTime as time),100) AS TripStartTime,  "
                             + " A.BookingPin,convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                             + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes' FROM BookingDtl AS A  "
                             + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                             + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                             + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                             + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                             + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                             + " A.BoatHouseId = F.BoatHouseId "
                             + " WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                             + " A.PremiumStatus != 'I' AND  "
                             + " A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK)  "
                             + " WHERE  CAST(BookingNewDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) and "
                             + " BoatHouseId = @BoatHouseId ) AND "
                             + " A.Status IN('R') AND "
                             + " A.BoatHouseId = @BoatHouseId ORDER BY A.TripEndTime DESC ) AS A "
                             + " ORDER BY A.TripEndTime DESC ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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

        // New Trip Sheet Implementations

        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetWeb/Update")]
        public IHttpActionResult NewTripsheetweb([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.QueryType != "" && Trip.BookingId != "" && Trip.BoatReferenceNo != ""
                    && Trip.ActualBoatId != "" && Trip.RowerId != "" && Trip.BookingMedia != ""
                    && Trip.BoatHouseId != "" && Trip.BoatHouseName != "")
                {
                    string sReturn = string.Empty;

                    SqlCommand cmd = new SqlCommand("TripSheetWebNew", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Trip.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", Trip.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", Trip.BoatReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Trip.BoatHouseName.ToString());

                    cmd.Parameters.AddWithValue("@BoatId", Trip.ActualBoatId.ToString());
                    cmd.Parameters.AddWithValue("@RowerId", Trip.RowerId.ToString());

                    if (Trip.SSUserBy.Trim() == "")
                    {
                        cmd.Parameters.AddWithValue("@SSUserBy", Trip.SSUserBy).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SSUserBy", Trip.SSUserBy.ToString());
                    }

                    if (Trip.SDUserBy.Trim() == "")
                    {
                        cmd.Parameters.AddWithValue("@SDUserBy", Trip.SDUserBy).Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SDUserBy", Trip.SDUserBy.ToString());
                    }

                    cmd.Parameters.AddWithValue("@BookingMedia", Trip.BookingMedia.ToString());

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
                        TripSheetWebString ConMstr = new TripSheetWebString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        TripSheetWebString ConMstr = new TripSheetWebString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ScanTripStartedList")]
        public IHttpActionResult NewGetTripStartedList([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = " SELECT Top 5 * FROM ( "
                  + " SELECT Top 5 D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                  + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                  + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                  + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100)  AS 'TripStartTime', "
                  + " CONVERT(nvarchar, CAST(A.TripEndTime as time), 100)  AS 'TripEndTime', "
                  + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime'  FROM BookingDtl AS A "
                  + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                  + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                  + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                  + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                  + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                  + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                  + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                  + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                  + " AND A.BoatHouseId = H.BoatHouseId "
                  + " WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND "
                  + " H.ActiveStatus = 'A' AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND  A.PremiumStatus != 'I' AND "
                  + " A.Status IN('B') AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NULL ORDER BY A.TripStartTime DESC "
                  + " UNION ALL "
                  + " SELECT Top 5 D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                  + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                  + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                  + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100)  AS 'TripStartTime', "
                  + " CONVERT(nvarchar, CAST(A.TripEndTime as time), 100)  AS 'TripEndTime', "
                  + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime'  FROM BookingDtl AS A "
                  + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                  + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                  + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                  + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                  + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                  + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                  + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                  + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                  + " AND A.BoatHouseId = H.BoatHouseId "
                  + " WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND "
                  + "  H.ActiveStatus = 'A' AND  A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                  + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and "
                  + " BoatHouseId = @BoatHouseId ) AND A.PremiumStatus != 'I' AND "
                  + " A.Status IN('R') AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NULL ORDER BY A.TripStartTime DESC ) AS A "
                  + " ORDER BY A.TripStartTime DESC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListTop5TripClosed")]
        public IHttpActionResult NewTripEndedList([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != "" && Trip.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT Top 5 * FROM ( "
                            + " SELECT Top 5 A.BookingId,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType, A.BookingDuration,A.ActualBoatNum, "
                            + " E.BoatNum,A.RowerId,F.RowerName,Convert(nvarchar,CAST(A.TripStartTime as time),100) AS TripStartTime, "
                            + " A.BookingPin,convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                            + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes' FROM BookingDtl AS A "
                            + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                            + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                            + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                            + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                            + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                            + " A.BoatHouseId = F.BoatHouseId "
                            + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                            + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I "
                            + " WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                            + " AND A.BoatHouseId = H.BoatHouseId "
                            + " WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND "
                            + " CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND "
                            + " A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND  A.Status IN('B') "
                            + " AND H.ActiveStatus='A' AND A.PremiumStatus != 'I' ORDER BY A.TripEndTime DESC "
                            + " UNION ALL"
                            + " SELECT Top 5 A.BookingId,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType, A.BookingDuration,A.ActualBoatNum, "
                            + " E.BoatNum,A.RowerId,F.RowerName,Convert(nvarchar,CAST(A.TripStartTime as time),100) AS TripStartTime, "
                            + " A.BookingPin,convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                            + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes' FROM BookingDtl AS A "
                            + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                            + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                            + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                            + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                            + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                            + " A.BoatHouseId = F.BoatHouseId "
                            + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                            + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I "
                            + " WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                            + " AND A.BoatHouseId = H.BoatHouseId "
                            + " WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL "
                            + " AND A.PremiumStatus != 'I' AND "
                            + " A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                            + "  where  CAST(BookingNewDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) and "
                            + "  BoatHouseId = @BoatHouseId) AND "
                            + " A.Status IN('R') AND "
                            + " A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId "
                            + " AND H.ActiveStatus='A' ORDER BY A.TripEndTime DESC ) AS A "
                            + " ORDER BY A.TripEndTime DESC  ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = Trip.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ScanTripStart")]
        public IHttpActionResult NewGetTripStart([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BarcodePin != "" && bHMstr.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = " SELECT D.BookingId,A.BookingSerial,D.UserId,CONVERT(NVARCHAR,CAST(D.BookingDate AS TIME),100) AS 'BoardingTime', "
                                  + " A.BoatHouseId,A.BoatHouseName,A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                  + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType,B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                                  + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100) AS 'TripStartTime', "
                                  + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                  + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime'  FROM BookingDtl AS A "
                                  + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                  + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                  + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                  + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                  + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                  + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                  + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                  + " AND A.BoatSeaterId IN (SELECT Value FROM [dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                  + " AND A.BoatHouseId = H.BoatHouseId "
                                  + " WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND "
                                  + " A.Status IN('B', 'R') AND A.BookingPin = @BarcodePin ORDER BY ExpectedTime ASC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ScanTripEndDuration")]
        public IHttpActionResult NewGetScanEndDuration([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BarcodePin != "" && bHMstr.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("SELECT D.BookingId,A.BookingSerial,D.UserId,CONVERT(nvarchar,CAST(D.BookingDate as time),100) as BoardingTime, "
                                   + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                   + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                   + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                   + " CONVERT(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                   + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TraveledTime', "
                                   + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime' "
                                   + " FROM BookingDtl as A "
                                   + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                   + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                   + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                   + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                   + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                   + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                   + " AND A.BoatHouseId = H.BoatHouseId "
                                   + " WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND "
                                   + " CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                   + " AND A.Status IN('B', 'R') AND A.BookingPin = @BarcodePin AND A.TripStartTime != '' AND A.TripEndTime IS NOT NULL  ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.TraveledTime = dt.Rows[i]["TraveledTime"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ScanTripEnd")]
        public IHttpActionResult NewGetScanEnd([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BarcodePin != "" && bHMstr.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand(" SELECT D.BookingId,A.BookingSerial,D.UserId,convert(nvarchar,CAST(D.BookingDate as time),100) as BoardingTime, "
                            + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                            + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType,B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                            + " C.RowerName, CONVERT(nvarchar, CAST(A.TripStartTime as time), 100) AS 'TripStartTime', "
                            + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                            + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime'  FROM BookingDtl as A "
                            + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                            + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                            + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                            + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                            + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                            + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                            + " AND A.BoatHouseId = H.BoatHouseId "
                            + " WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                            + " AND A.Status IN('B', 'R') AND A.BookingPin= @BarcodePin AND A.TripStartTime != '' AND A.TripEndTime IS NULL  ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPinEnd")]
        public IHttpActionResult getBookingDetByPinEnd([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingId != "" && tripBook.BookingPin != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = " SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile"
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId"
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N' "
                        + " AND A.BookingPin = @BookingPin AND A.BookingId = @BookingId "
                        + " AND D.BookingId = @BookingId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = tripBook.BookingPin.Trim();
                    cmd.Parameters["@BookingId"].Value = tripBook.BookingId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Modified by: subalakshmi
        /// Modified Date: 2022-05-26
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPinAlreadyRefunded")]
        public IHttpActionResult getBookingDetByPinAlreadyRefunded([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingId != "" && tripBook.BookingPin != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = " SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile, A.DepRefundAmount, "
                        + " CONVERT(VARCHAR(50), A.DepRefundDate, 103) + ' ' + FORMAT(CAST(A.DepRefundDate AS DATETIME), 'hh:mm tt') AS 'DepRefundDate',"
                        + " G.UserName AS 'DepositRefundBy',A.DepRefundBy FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " INNER JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS G ON  A.BoatHouseId = G.BranchId AND A.DepRefundBy = G.UserId"
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId "
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) "
                        + " AND A.BookingPin = @BookingPin AND A.BookingId = @BookingId "
                        + " AND D.BookingId = @BookingId AND A.DepRefundStatus = 'Y' ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = tripBook.BookingPin.Trim();
                    cmd.Parameters["@BookingId"].Value = tripBook.BookingId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.DepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                            row.DepositRefundBy = dt.Rows[i]["DepositRefundBy"].ToString();
                            row.DepRefundDate = dt.Rows[i]["DepRefundDate"].ToString();
                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPinNotStarted")]
        public IHttpActionResult getBookingDetByPinNotStarted([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingId != "" && tripBook.BookingPin != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = " SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile"
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId"
                        + " AND A.TripStartTime IS NULL AND A.TripEndTime IS NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N' "
                        + " AND A.BookingPin = @BookingPin AND A.BookingId = @BookingId "
                        + " AND D.BookingId = @BookingId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = tripBook.BookingPin.Trim();
                    cmd.Parameters["@BookingId"].Value = tripBook.BookingId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Created By : Silambarasu
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified By : Silambarasu
        /// Modified Date : 2022-04-26
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPinORIdV2")]
        public IHttpActionResult getBookingDetByPinORId([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingIdORPin != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = "SELECT * FROM  "
                        + "  (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                        + "  ( SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile,A.RefundRePrint"
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId "
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N' "
                        + " AND (A.BookingPin = @BookingIdORPin OR A.BookingId = @BookingIdORPin )) AS A ) AS B";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingIdORPin", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingIdORPin"].Value = tripBook.BookingIdORPin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.RStatus = dt.Rows[i]["RefundRePrint"].ToString();
                            row.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Created By : Silambarasu
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified By : Silambarasu
        /// Modified Date : 2022-04-26
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPinORIdEndV2")]
        public IHttpActionResult getBookingDetByPinORIdEnd([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingPin != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = "SELECT * FROM  "
                        + "  (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                        + "  ( SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile"
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId"
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N' "
                        + " AND (A.BookingPin = @BookingIdORPin OR A.BookingId = @BookingIdORPin )) AS A) AS B";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingIdORPin", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingIdORPin"].Value = tripBook.BookingIdORPin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Created By : Silambarasu
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified By : Silambarasu
        /// Modified Date : 2022-04-26
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPinORIdNotStartedV2")]
        public IHttpActionResult getBookingDetByPinORIdNotStarted([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingIdORPin != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = "SELECT * FROM  "
                        + " (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                        + " ( SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile"
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId"
                        + " AND A.TripStartTime IS NULL AND A.TripEndTime IS NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND ISNULL(A.BoatDeposit,0) > 0 AND A.DepRefundStatus = 'N' "
                        + " AND (A.BookingPin = @BookingIdORPin OR A.BookingId = @BookingIdORPin )) AS A ) AS B";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingIdORPin", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingIdORPin"].Value = tripBook.BookingIdORPin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// Created By : Silambarasu
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified By : Silambarasu
        /// Modified Date : 2022-04-26
        /// Version : V2
        /// Modified By : Subalakshmi
        /// Modified Date : 2022-05-26
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-26
        /// Modified into parameterised query
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getBookingDetByPinORAlreadyRefundedV2")]
        public IHttpActionResult getBookingDetByPinORAlreadyRefunded([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "" && tripBook.BookingIdORPin != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = "SELECT * FROM "
                        + " (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                        + " ( SELECT CONVERT(VARCHAR(50), D.BookingDate, 103) as 'BookingDate',  A.BookingId, A.BookingDuration, A.BoatReferenceNo, A.BookingPin, A.BoatTypeId, B.BoatType,"
                        + " A.BoatSeaterId, C.SeaterType, A.ActualBoatId, A.ActualBoatNum, A.RowerId, ISNULL(E.RowerName,'-') AS 'RowerName',"
                        + " CONVERT(VARCHAR(50), A.TripStartTime, 103) + ' ' + FORMAT(CAST(A.TripStartTime AS DATETIME), 'hh:mm tt') AS 'TripStartTime',"
                        + " CONVERT(VARCHAR(50), A.TripEndTime, 103) + ' ' + FORMAT(CAST(A.TripEndTime AS DATETIME), 'hh:mm tt') AS 'TripEndTime',"
                        + " D.PremiumStatus, A.InitNetAmount, ISNULL(A.BoatDeposit,0) as 'Deposit', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDifference', "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8) as 'Duration', "
                        + " ISNULL(CAST(DATEDIFF(MINUTE, right(convert(nvarchar(32), A.TripEndTime, 100), 8), "
                        + " right(convert(nvarchar(32), GETDATE(), 100), 8)) AS nvarchar), 0) AS 'TravelDuration', F.RefundDuration, D.CustomerMobile, A.DepRefundAmount ,"
                        + " CONVERT(VARCHAR(50), A.DepRefundDate, 103) + ' ' + FORMAT(CAST(A.DepRefundDate AS DATETIME), 'hh:mm tt') AS 'DepRefundDate', "
                        + " G.UserName AS 'DepositRefundBy',A.DepRefundBy FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId"
                        + " LEFT JOIN RowerMaster AS E ON A.BoatHouseId = E.BoatHouseId AND A.RowerId = E.RowerId"
                        + " INNER JOIN BHMaster AS F ON  A.BoatHouseId = F.BoatHouseId "
                        + " INNER JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS G ON  A.BoatHouseId = G.BranchId AND A.DepRefundBy = G.UserId"
                        + " WHERE A.BoatHouseId = @BoatHouseId AND F.BoatHouseId = @BoatHouseId "
                        + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') "
                        + " AND (A.ActualBoatCharge IS NULL OR A.ActualBoatCharge IS NOT NULL) AND (A.ActualRowerCharge IS NULL OR A.ActualRowerCharge IS NOT NULL) "
                        + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.DepRefundStatus = 'Y' "
                        + " AND (A.BookingPin = @BookingIdORPin OR A.BookingId = @BookingIdORPin ))AS A ) AS B";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingIdORPin", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@BookingIdORPin"].Value = tripBook.BookingIdORPin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            row.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            row.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            row.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.RowerName = dt.Rows[i]["RowerName"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            row.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            row.Deposit = dt.Rows[i]["Deposit"].ToString();
                            row.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            row.TravelDifference = dt.Rows[i]["TravelDifference"].ToString();
                            row.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                            row.DepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                            row.DepositRefundBy = dt.Rows[i]["DepositRefundBy"].ToString();
                            row.DepRefundDate = dt.Rows[i]["DepRefundDate"].ToString();
                            row.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(row);
                        }

                        TripSheetList ConfList = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripSheetRes ConfRes = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripSheetRes Vehicle = new TripSheetRes
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
        /// //Created By : Abhinaya
        /// Created Date : 22-08-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/GetPremiumStatus")]
        public IHttpActionResult GetPremiumStatus([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BarcodePin != "")
                {
                    con.Open();
                    string sQuery = "SELECT PremiumStatus FROM BookingDtl WHERE BookingPin = @BarcodePin " +
                        "AND BoatHouseId = @BoatHouseId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result.ToString() == "I")
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "Individual Tickets Not Allowed",
                            StatusCode = 1
                        };
                        con.Close();
                        return Ok(BoatHouse);
                    }
                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = result.ToString(),
                            StatusCode = 0

                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString BoatHouse = new TripSheetWebString
                    {
                        Response = "No Records Found.",
                        StatusCode = 2
                    };
                    return Ok(BoatHouse);
                }
            }
            catch (Exception ex)
            {
                TripSheetWebString BoatHouse = new TripSheetWebString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(BoatHouse);
            }
        }

        /// <summary>
        /// Created By Abhinaya
        /// Date:21.08.2021
        /// This Method is used for get Trip Details
        /// </summary>
        /// <param name="SmartScan"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("IndividualSmartTripStart")]
        public IHttpActionResult IndividualSmartTripStart([FromBody] IndividualScanTripSheet SmartScan)
        {
            try
            {
                if (SmartScan.BoatHouseId != "" || SmartScan.BarcodePin != "")
                {
                    List<IndividualScanTripSheet> li = new List<IndividualScanTripSheet>();
                    SqlCommand cmd = new SqlCommand("sp_ScanIndividualTripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "IndividualTripStart");
                    cmd.Parameters.AddWithValue("@BookingPin", SmartScan.BarcodePin);
                    cmd.Parameters.AddWithValue("@BoatHouseId", SmartScan.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", SmartScan.UserId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            IndividualScanTripSheet tripsheets = new IndividualScanTripSheet();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        IndividualScanTripSheetList tripsheet = new IndividualScanTripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        IndividualScanTripSheetString BoatHouse = new IndividualScanTripSheetString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    IndividualScanTripSheetString Vehicle = new IndividualScanTripSheetString
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
        /// Created By Abhinaya
        /// Date:21.08.2021
        /// This Method is used for get Trip Details for user based
        /// </summary>
        /// <param name="SmartScan"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UserBasedNewTripScanTripSheetStart")]
        public IHttpActionResult UserBasedNewTripScanTripSheetStart([FromBody] IndividualScanTripSheet SmartScan)
        {
            try
            {
                if (SmartScan.BoatHouseId != "" || SmartScan.BarcodePin != "")
                {
                    List<IndividualScanTripSheet> li = new List<IndividualScanTripSheet>();
                    SqlCommand cmd = new SqlCommand("sp_ScanIndividualTripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "UserBasedNewTripScanTripSheetStart");
                    cmd.Parameters.AddWithValue("@BookingPin", SmartScan.BarcodePin);
                    cmd.Parameters.AddWithValue("@BoatHouseId", SmartScan.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", SmartScan.UserId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            IndividualScanTripSheet tripsheets = new IndividualScanTripSheet();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        IndividualScanTripSheetList tripsheet = new IndividualScanTripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        IndividualScanTripSheetString BoatHouse = new IndividualScanTripSheetString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    IndividualScanTripSheetString Vehicle = new IndividualScanTripSheetString
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
        /// Created By Abhinaya
        /// Date:21.08.2021
        /// </summary>
        /// <param name="Individual"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("InividualNewSmartTripSheetWeb")]
        public async Task<IHttpActionResult> InividualNewSmartTripSheetWeb([FromBody] IndividualTripSheetWeb Individual)
        {
            var i = 0;
            if (Individual.BoatReferenceNo == null)
                return NotFound();

            var result = new List<string>();
            foreach (var id in Individual.BoatReferenceNo)
            {
                result.Add(await IndSmartNewTripSheetWeb(id, Individual.QueryType, Individual.BookingId[i], Individual.BoatHouseId, Individual.BoatHouseName,
                    Individual.ActualBoatId, Individual.RowerId, Individual.SSUserBy, Individual.SDUserBy, Individual.BookingMedia));
                i++;
            }
            IndividualTripSheetWebResponse BookingSlot = new IndividualTripSheetWebResponse
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }

        /// <summary>
        /// Created By Abhinaya
        /// Date:21.08.2021
        /// </summary>
        /// <param name="BoatReferenceNo"></param>
        /// <param name="QueryType"></param>
        /// <param name="BookingId"></param>
        /// <param name="BoatHouseId"></param>
        /// <param name="BoatHouseName"></param>
        /// <param name="ActualBoatId"></param>
        /// <param name="RowerId"></param>
        /// <param name="SSUserBy"></param>
        /// <param name="SDUserBy"></param>
        /// <param name="BookingMedia"></param>
        /// <returns></returns>
        public async Task<dynamic> IndSmartNewTripSheetWeb(int BoatReferenceNo, string QueryType, string BookingId, string BoatHouseId, string BoatHouseName,
           string ActualBoatId, string RowerId, string SSUserBy, string SDUserBy, string BookingMedia)
        {
            try
            {
                string sReturn = string.Empty;

                SqlCommand cmd = new SqlCommand("sp_TripSheetStart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;

                cmd.Parameters.AddWithValue("@QueryType", QueryType.ToString());
                cmd.Parameters.AddWithValue("@BookingId", BookingId.ToString());
                cmd.Parameters.AddWithValue("@BoatReferenceNo", BoatReferenceNo.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.ToString());

                cmd.Parameters.AddWithValue("@BoatId", ActualBoatId.ToString());
                cmd.Parameters.AddWithValue("@RowerId", RowerId.ToString());

                if (SSUserBy.Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@SSUserBy", SSUserBy).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SSUserBy", SSUserBy.ToString());
                }

                if (SDUserBy.Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@SDUserBy", SDUserBy).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SDUserBy", SDUserBy.ToString());
                }

                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.ToString());

                SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                RuturnValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(RuturnValue);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                string[] sResult = sReturn.Split('~');
                con.Close();
                return sReturn;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
        }

        /// <summary>
        /// Created By : Jaya Suriya
        /// Created Date : 26-08-2021
        /// Modified By : Abhinaya
        /// Modified Date : 27-09-2021
        /// </summary>
        /// <param name="SmartScan"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripEnd/Individual")]
        public IHttpActionResult TripEnd([FromBody] IndividualScanTripSheet SmartScan)
        {
            try
            {
                if (SmartScan.BoatHouseId != "" && SmartScan.BarcodePin != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_IndividualTripEnd", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", SmartScan.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", SmartScan.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@Bookingpin", SmartScan.BarcodePin);
                    cmd.Parameters.AddWithValue("@BookingId", SmartScan.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@BoatRefNo", SmartScan.BoatReferenceNo.Trim());
                    cmd.Parameters.AddWithValue("@ActualBoatId", SmartScan.ActualBoatId.Trim());
                    cmd.Parameters.AddWithValue("@BookingMedia", SmartScan.BookingMedia.Trim());
                    cmd.Parameters.AddWithValue("@SDUserBy", SmartScan.SDUserBy.Trim());

                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 100);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con.Close();

                    if (sResult[0].Trim() == "Success")
                    {
                        IndividualScanTripSheetResponse SlotMstr = new IndividualScanTripSheetResponse
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(SlotMstr);
                    }
                    else
                    {
                        IndividualScanTripSheetResponse SlotMstr = new IndividualScanTripSheetResponse
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(SlotMstr);
                    }
                }
                else
                {
                    IndividualScanTripSheetResponse SlotMstr = new IndividualScanTripSheetResponse
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(SlotMstr);
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
        /// Created By :Jaya Suriya A,
        /// Date :21-Aug-2021
        /// Modified By : Jaya Suriya A,
        /// Modified Date : 31-Aug-2021
        /// </summary>
        /// <param name="IndTripSheet"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("IndividualTripSheet/View")]
        public IHttpActionResult ViewIndividualtripSheet([FromBody] IndividualTripSheet IndTripSheet)
        {
            try
            {
                if (IndTripSheet.QueryType == "" || IndTripSheet.BoatHouseId == "")
                {
                    return Ok("Must Pass All Parameters");
                }

                SqlCommand cmd = new SqlCommand("sp_IndividualTripSheetWeeb", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@QueryType", SqlDbType.NVarChar, 100).Value = IndTripSheet.QueryType.Trim();
                cmd.Parameters.Add("@BoatHouseId", SqlDbType.Int).Value = IndTripSheet.BoatHouseId.Trim();
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = IndTripSheet.UserId.Trim();
                cmd.Parameters.Add("@UserRole", SqlDbType.NVarChar, 100).Value = IndTripSheet.UserRole.Trim();
                cmd.Parameters.Add("@RowerId", SqlDbType.Int).Value = IndTripSheet.RowerId.Trim();
                cmd.Parameters.Add("@BoatTypeId", SqlDbType.Int).Value = IndTripSheet.BoatTypeId.Trim();
                cmd.Parameters.Add("@BoatSeaterId", SqlDbType.Int).Value = IndTripSheet.BoatSeaterId.Trim();
                cmd.Parameters.Add("@TripUser", SqlDbType.VarChar).Value = IndTripSheet.TripUser.Trim();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    var IndTripSheetList = new IndividualTripSheetList()
                    {
                        DataTable = dt,
                        StatusCode = 1,
                        ResponseMsg = "Success"
                    };
                    return Ok(IndTripSheetList);
                }
                else
                {
                    var IndTripSheetList = new IndividualTripSheetList()
                    {
                        DataTable = null,
                        StatusCode = 0,
                        ResponseMsg = "No Records Found !!!."
                    };
                    return Ok(IndTripSheetList);
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
        /// Created By : JayaSuriya
        /// Created Date : 26-08-2021       
        /// </summary>
        /// <param name="Individual"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("InividualNewTripSheetWeb")]
        public async Task<IHttpActionResult> IndividualTripSheetWebNew([FromBody] IndividualTripSheetWeb Individual)
        {
            var i = 0;
            if (Individual.BoatReferenceNo == null)
                return NotFound();

            var result = new List<string>();
            foreach (var id in Individual.BoatReferenceNo)
            {
                result.Add(await IndNewTripSheetWeb(id, Individual.QueryType, Individual.BookingId[i], Individual.BoatHouseId, Individual.BoatHouseName,
                    Individual.ActualBoatId, Individual.RowerId, Individual.SSUserBy, Individual.SDUserBy, Individual.BookingMedia));
                i++;
            }
            IndividualTripSheetWebResponse BookingSlot = new IndividualTripSheetWebResponse
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }


        /// <summary>
        ///  Created By : JayaSuriya
        ///  Modified Date : 20-09-2021
        /// </summary>
        /// <param name="BoatReferenceNo"></param>
        /// <param name="QueryType"></param>
        /// <param name="BookingId"></param>
        /// <param name="BoatHouseId"></param>
        /// <param name="BoatHouseName"></param>
        /// <param name="ActualBoatId"></param>
        /// <param name="RowerId"></param>
        /// <param name="SSUserBy"></param>
        /// <param name="SDUserBy"></param>
        /// <param name="BookingMedia"></param>
        /// <returns></returns>
        public async Task<dynamic> IndNewTripSheetWeb(int BoatReferenceNo, string QueryType, string BookingId, string BoatHouseId, string BoatHouseName,
          string ActualBoatId, string RowerId, string SSUserBy, string SDUserBy, string BookingMedia)
        {
            try
            {
                string sReturn = string.Empty;

                SqlCommand cmd = new SqlCommand("TripSheetWebNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;

                cmd.Parameters.AddWithValue("@QueryType", QueryType.ToString());
                cmd.Parameters.AddWithValue("@BookingId", BookingId.ToString());


                if (QueryType.ToString() == "IndividualTripEnd")
                {
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", "");
                    cmd.Parameters.AddWithValue("@BoatId", BoatReferenceNo.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", BoatReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatId", ActualBoatId.ToString());
                }
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.ToString());

                cmd.Parameters.AddWithValue("@RowerId", RowerId.ToString());

                if (SSUserBy.Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@SSUserBy", SSUserBy).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SSUserBy", SSUserBy.ToString());
                }

                if (SDUserBy.Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@SDUserBy", SDUserBy).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SDUserBy", SDUserBy.ToString());
                }

                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.ToString());

                SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                RuturnValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(RuturnValue);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                string[] sResult = sReturn.Split('~');
                con.Close();
                return sReturn;
            }
            catch (Exception ex)
            {

                //catch (Exception ex)
                //{
                //    return ex.Message;
                //}

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
        /// Created By : Silambarasu
        /// Created Date : 16-09-2021       
        /// </summary>
        /// <param name="Individual"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("IndividualNewTripSheetWebEnd")]
        public async Task<IHttpActionResult> IndividualNewTripSheetWebEnd([FromBody] IndividualTripSheetWeb Individual)
        {
            var i = 0;
            if (Individual.BoatReferenceNo == null)
                return NotFound();

            var result = new List<string>();
            foreach (var id in Individual.BoatReferenceNo)
            {
                result.Add(await IndNewTripSheetWeb(id, Individual.QueryType, Individual.BookingId[i], Individual.BoatHouseId, Individual.BoatHouseName,
                    Individual.BoatId[i], Individual.RowerIds[i], Individual.SSUserBy, Individual.SDUserBy, Individual.BookingMedia));
                i++;
            }
            IndividualTripSheetWebResponse BookingSlot = new IndividualTripSheetWebResponse
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }

        /// <summary>
        /// Created By : Abhi
        /// Created Date : 08-10-2021
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripStartResch")]
        public IHttpActionResult TripStartResch([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" || bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("sp_TripStartEndResch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", bHMstr.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", bHMstr.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BookingPin", bHMstr.BarcodePin.Trim());
                    cmd.Parameters.AddWithValue("@UserId", bHMstr.UserId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Created By : Abhi
        /// Created Date : 08-10-2021
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripEndResch")]
        public IHttpActionResult TripEndResch([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" || bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("sp_TripStartEndResch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", bHMstr.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", bHMstr.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BookingPin", bHMstr.BarcodePin.Trim());
                    cmd.Parameters.AddWithValue("@UserId", bHMstr.UserId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Created By : Abhi
        /// Created Date : 08-10-2021
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripEndDurationResch")]
        public IHttpActionResult TripEndDurationResch([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" || bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("sp_TripStartEndResch", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", bHMstr.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", bHMstr.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BookingPin", bHMstr.BarcodePin.Trim());
                    cmd.Parameters.AddWithValue("@UserId", bHMstr.UserId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.TraveledTime = dt.Rows[i]["TraveledTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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

        ///======================================== For Test Validations.

        /// /// <summary>
        /// Created By : Abhinaya
        /// Created Date : 27-10-2021
        /// </summary>
        /// <param name="SmartScan"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetTripStartEndTimeScan")]
        public IHttpActionResult GetTripStartEndTimeScan([FromBody] IndividualScanTripSheet SmartScan)
        {
            try
            {
                if (SmartScan.BoatHouseId != "" || SmartScan.BarcodePin != "")
                {
                    List<IndividualScanTripSheet> li = new List<IndividualScanTripSheet>();
                    SqlCommand cmd = new SqlCommand("sp_ScanIndividualTripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", SmartScan.QueryType);
                    cmd.Parameters.AddWithValue("@BookingPin", SmartScan.BarcodePin);
                    cmd.Parameters.AddWithValue("@BoatHouseId", SmartScan.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", SmartScan.UserId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            IndividualScanTripSheet tripsheets = new IndividualScanTripSheet();


                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString(); ;
                            li.Add(tripsheets);
                        }

                        IndividualScanTripSheetList tripsheet = new IndividualScanTripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        IndividualScanTripSheetString BoatHouse = new IndividualScanTripSheetString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    IndividualScanTripSheetString Vehicle = new IndividualScanTripSheetString
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
        /// //Created By : Abhinaya
        /// Created Date : 25-08-2021
        /// Modified By : Abhinaya
        /// Modified Date : 25-10-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// Not Used
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/GetPremiumStatus_Test")]
        public IHttpActionResult GetPremiumStatus_Test([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BookingId != "" && bHMstr.BarcodePin != "")
                {
                    con.Open();
                    string sQuery = "SELECT PremiumStatus FROM BookingDtl WHERE BookingPin = @BarcodePin AND " +
                        "BookingId= @BookingId AND BoatHouseId = @BoatHouseId";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@BarcodePin", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BookingId"].Value = bHMstr.BookingId.Trim();
                    cmd.Parameters["@BarcodePin"].Value = bHMstr.BarcodePin.Trim();

                    object result = cmd.ExecuteScalar();

                    if (result != null && result.ToString() == "I")
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "Individual Tickets Not Allowed",
                            StatusCode = 1
                        };
                        con.Close();
                        return Ok(BoatHouse);
                    }
                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = result.ToString(),
                            StatusCode = 0

                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString BoatHouse = new TripSheetWebString
                    {
                        Response = "No Records Found.",
                        StatusCode = 2
                    };
                    return Ok(BoatHouse);
                }
            }
            catch (Exception ex)
            {
                TripSheetWebString BoatHouse = new TripSheetWebString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return null;
            }
        }


        /// <summary>
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        ///  Modified By : Abhi
        /// Modified Date : 22-10-2021
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListTop5TripClosed_Test")]
        public IHttpActionResult TripEndedList_Test([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("sp_ScanIndividualTripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "TripSheetweb/ListTop5TripClosed");
                    cmd.Parameters.AddWithValue("@BookingPin", "0");
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", "0");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            //ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            // ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        ///  Modified By : Abhi
        /// Modified Date : 25-10-2021
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListTop5TripClosed_Test")]
        public IHttpActionResult NewTripEndedList_Test([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("sp_ScanIndividualTripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "NewTripSheetweb/ListTop5TripClosed");
                    cmd.Parameters.AddWithValue("@BookingPin", "0");
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", Trip.UserId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            //ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            // ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        /// Modified By : Abhi
        /// Modified Date : 25-10-2021
        /// ENDPoint:TripSheetweb/ScanTripStartedList
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ScanTripStartedList_Test")]
        public IHttpActionResult GetTripStartedList_Test([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" || bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("sp_ScanIndividualTripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "TripSheetweb/ScanTripStartedList");
                    cmd.Parameters.AddWithValue("@BookingPin", "0");
                    cmd.Parameters.AddWithValue("@BoatHouseId", bHMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", "0");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Abhi
        /// Modified Date : 08-10-2021
        ///  Modified By : Abhi
        /// Modified Date : 25-10-2021
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ScanTripStartedList_Test")]
        public IHttpActionResult NewGetTripStartedList_Test([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" || bHMstr.BarcodePin != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();


                    SqlCommand cmd = new SqlCommand("sp_ScanIndividualTripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "NewTripSheetweb/ScanTripStartedList");
                    cmd.Parameters.AddWithValue("@BookingPin", "0");
                    cmd.Parameters.AddWithValue("@BoatHouseId", bHMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", bHMstr.UserId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Imran
        /// Modified Date : 28-03-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// Not used
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripStartMaxCount")]
        public IHttpActionResult gettripsheetStartMaxCount([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();


                    string sQuery = "SELECT TOP (select Maxcount from MaxiumCount WHERE BoatHouseId= @BoatHouseId ) * FROM  "
                                    + " (SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + "  A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                                    + "  C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "  CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                                    + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "  WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                                    + "  A.Status IN('B') AND A.TripStartTime IS NULL "
                                    + "  UNION ALL "
                                    + "  Select  D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                                    + "  C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + "  CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "   CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' "
                                    + "   FROM BookingDtl AS A  WITH(NOLOCK) "
                                    + "  LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "  LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "  INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "  INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "  where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A')  "
                                    + "   AND A.TripStartTime IS NULL AND A.PremiumStatus != 'I' AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A "
                                    + "  ORDER BY ExpectedTime ASC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified By : Imran.
        /// Modified Date : 28-03-2021
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// Not Used
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripStartMaxCount")]
        public IHttpActionResult NewgettripsheetStartMaxCount([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = " SELECT TOP (select Maxcount from MaxiumCount WHERE BoatHouseId= @BoatHouseId ) * FROM  "
                                    + "     (SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + "     A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + "   A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                    + "    G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + "    CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "    CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                    + "     LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "      LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "     INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "    INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "    INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "     INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "     LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "     AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                    + "     AND A.BoatHouseId = H.BoatHouseId "
                                    + "     WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                                    + "    A.Status IN('B') AND A.TripStartTime IS NULL "
                                    + "    UNION ALL "
                                    + "   SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                    + "   A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                    + "   A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                    + "   G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                    + "    CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                    + "    CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                    + "    LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                    + "   LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                    + "    INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                    + "    INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                    + "   INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                    + "   LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "   AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                    + "   AND A.BoatHouseId = H.BoatHouseId "
                                    + "   where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                    + "   AND A.TripStartTime IS NULL AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.PremiumStatus != 'I' AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A "
                                    + "   ORDER BY ExpectedTime ASC ";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.CommandTimeout = 900000;

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Created By : Vediyappan.
        /// Created Date : 16-04-2022 Mid Night From Home.
        /// Version : V2
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripStartV2")]
        public IHttpActionResult NewListAllTripStartV2([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = " SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM "
                                      + " (SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                      + "   A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                      + "   A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                      + "   G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                      + "   CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                      + "   CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                      + "   LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                      + "   LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                      + "   INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                      + "   INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                      + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                      + "   INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                      + "   LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                      + "   AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                      + "   AND A.BoatHouseId = H.BoatHouseId "
                                      + "   WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                                      + "   A.Status IN('B') AND A.TripStartTime IS NULL "
                                      + "   UNION ALL "
                                      + "   SELECT D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                                      + "   A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                                      + "   A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', "
                                      + "   G.SelfDrive, C.RowerId, C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                                      + "   CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                                      + "   CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus'  FROM BookingDtl AS A "
                                      + "   LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                                      + "   LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                                      + "   INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                      + "   INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                      + "   INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                      + "   INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                                      + "   LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                      + "   AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                      + "   AND A.BoatHouseId = H.BoatHouseId "
                                      + "   where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH (NOLOCK) "
                                      + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A') "
                                      + "   AND A.TripStartTime IS NULL AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND "
                                      + "   A.PremiumStatus != 'I' AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A "
                                      + "   ) AS B WHERE B.RowNumber BETWEEN @CountStart AND @CountEnd ORDER BY RowNumber ASC, ExpectedTime ASC ";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.CommandTimeout = 900000;
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();
                    cmd.Parameters["@CountStart"].Value = bHMstr.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(bHMstr.CountStart.Trim()) + 9;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();

                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        //**************************New changes did by suriya on 04-04-2022*******************************//

        //***************************************************************************//
        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripStartV2")]
        public IHttpActionResult gettripsheetStartV2([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    string sQuery = "SELECT * FROM "
                             + " (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM "
                             + " (SELECT  D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                             + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                             + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                             + " C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                             + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                             + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                             + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                             + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                             + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                             + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                             + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                             + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                             + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.PremiumStatus != 'I' AND "
                             + " A.Status IN('B') AND A.TripStartTime IS NULL "
                             + " UNION ALL "
                             + " Select  D.BookingId, A.BookingSerial, D.UserId, CONVERT(NVARCHAR, CAST(D.BookingDate AS TIME), 100) AS 'BoardingTime', "
                             + " A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.InitRowerCharge, "
                             + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName AS 'BoatName', G.SelfDrive, C.RowerId, "
                             + " C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS 'TripStartTime', "
                             + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS 'TripEndTime', "
                             + " CONVERT(NVARCHAR(15), CAST(A.ExpectedTime AS TIME), 100) AS 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' "
                             + " FROM BookingDtl AS A  WITH(NOLOCK) "
                             + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  AND B.BoatHouseId = A.BoatHouseId "
                             + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId AND C.BoatHouseId = A.BoatHouseId "
                             + "INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                             + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                             + "INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                             + " INNER JOIN BoatRateMaster AS G ON A.BoatTypeId = G.BoatTypeId AND A.BoatSeaterId = G.BoatSeaterId AND A.BoatHouseId = G.BoatHouseId "
                             + " where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                             + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus = 'A') "
                             + " AND A.TripStartTime IS NULL AND A.PremiumStatus != 'I' AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A "
                             + " ) AS B where B.RowNumber BETWEEN @CountStart AND @CountEnd ORDER BY RowNumber ASC, ExpectedTime ASC ";


                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@CountStart"].Value = bHMstr.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(bHMstr.CountStart.Trim()) + 9;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();

                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripEndV2")]
        public IHttpActionResult gettripsheetEndV2([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM  "
                                            + "  (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                                            + "  (SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime,  "
                                            + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.BoatTypeId, E.BoatType, A.BoatSeaterId,  "
                                            + "  F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100)  "
                                            + "  AS TripStartTime, CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime,  "
                                            + "  CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A  "
                                            + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId    LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  "
                                            + "  LEFT JOIN RowerMaster As C    on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId  "
                                            + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId WHERE A.BoatHouseId = @BoatHouseId AND  "
                                            + "  CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) AND A.Status IN('B') AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND  "
                                            + "  A.TripEndTime IS NULL  "
                                            + "  UNION ALL  "
                                            + "  SELECT D.BookingId, A.BookingSerial, D.UserId, convert(nvarchar, CAST(D.BookingDate as time), 100) as BoardingTime,  "
                                            + "  A.BoatHouseId, A.BoatHouseName, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, A.BoatTypeId, E.BoatType, A.BoatSeaterId,  "
                                            + "  F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100)  "
                                            + "  AS TripStartTime, CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime,  "
                                            + "  CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl as A  "
                                            + "  INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId    LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId  "
                                            + "  LEFT JOIN RowerMaster As C    on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId  "
                                            + "  INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId  "
                                            + "  where A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK)  where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE)  "
                                            + "  and BoatHouseId = @BoatHouseId AND ActiveStatus = 'A') AND A.TripStartTime != '' AND A.PremiumStatus != 'I' AND A.TripEndTime IS NULL AND  "
                                            + "  A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A) AS B WHERE B.RowNumber BETWEEN @CountStart AND @CountEnd ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@CountStart"].Value = bHMstr.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(bHMstr.CountStart.Trim()) + 9;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            tripsheets.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripClosedV2")]
        public IHttpActionResult TripClosedGridV2([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand(" SELECT * FROM "
                                        + " (SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber', * FROM  "
                                        + " (SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum,  "
                                        + " E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime,  "
                                        + " A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                                        + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A  "
                                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId  "
                                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                                        + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                                        + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                                        + " A.BoatHouseId = F.BoatHouseId WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                                        + " CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND A.PremiumStatus != 'I' AND  "
                                        + " A.BoatHouseId = @BoatHouseId AND A.Status IN('B')  "
                                        + " UNION ALL  "
                                        + " SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum,  "
                                        + " E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime,  "
                                        + " A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                                        + " DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A  "
                                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId  "
                                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                                        + " LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                                        + " A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                                        + " A.BoatHouseId = F.BoatHouseId WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                                        + " A.PremiumStatus != 'I' AND A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK)  "
                                        + " where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus = 'A') "
                                        + " AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId) AS A)  "
                                        + " AS B WHERE B.RowNumber BETWEEN @CountStart AND @CountEnd"
                                        + " ORDER BY RowNumber ASC,  B.TripEndTime DESC ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmd.Parameters["@CountStart"].Value = Trip.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(Trip.CountStart.Trim()) + 9;

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();
                            ShowTrip.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            ShowTrip.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        ///  Created By : Imran
        ///  Created Date : 2022-04-20
        ///  Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>       
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripClosedSingleV2")]
        public IHttpActionResult TripClosedGridWithPin([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber',* FROM  "
                                        + " (SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum,  "
                                        + " E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime,  "
                                        + "  A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                                        + "   DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A  "
                                        + "   INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "   INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                                        + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "  LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                                        + "   A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                                        + "   A.BoatHouseId = F.BoatHouseId WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                                        + "   CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND A.PremiumStatus != 'I' AND  "
                                        + "   A.BoatHouseId = @BoatHouseId AND A.Status IN('B') AND (A.BookingId = @BookingPin OR A.BookingPin = @BookingPin )  "
                                        + "  UNION ALL  "
                                        + " SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum,  "
                                        + "   E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime,  "
                                        + "   A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime,  "
                                        + "   DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A  "
                                        + "  INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "    INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId  "
                                        + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId  "
                                        + "   LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND  "
                                        + "   A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND  "
                                        + "    A.BoatHouseId = F.BoatHouseId WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND  "
                                        + "   A.PremiumStatus != 'I' AND A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK)  "
                                        + "  where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A' "
                                        + " AND (BookingPin = @BookingPin OR BookingId = @BookingPin)) "
                                        + " AND A.Status IN('R') AND A.BoatHouseId = @BoatHouseId "
                                        + " AND (A.BookingId = @BookingPin OR A.BookingPin = @BookingPin ) ) AS A  "
                                        + "   ORDER BY A.TripEndTime DESC ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmd.Parameters["@BookingPin"].Value = Trip.BookingPin.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();
                            ShowTrip.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            ShowTrip.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        ///  Created By : Imran
        ///  Created Date : 2022-04-20
        ///  Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripClosedSingleV2")]
        public IHttpActionResult NewTripClosedGridWithPin([FromBody] TripSheetWeb Trip)
        {
            try
            {
                if (Trip.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY A.BookingId) 'RowNumber',* FROM "
                                    + "  (SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum, "
                                    + "   E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + "  A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                    + "  DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                                    + "   INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                                    + "   INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                                    + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                                    + "   LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                                    + "   A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                                    + "  A.BoatHouseId = F.BoatHouseId "
                                    + "  LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "  AND A.BoatSeaterId IN (SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                    + "   AND A.BoatHouseId = H.BoatHouseId "
                                    + "  WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND A.PremiumStatus != 'I' AND "
                                    + "  CAST(D.BookingDate AS DATE) BETWEEN CONVERT(date, GETDATE(), 103) AND CONVERT(date, GETDATE(), 103) AND "
                                    + "   A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' AND A.Status IN('B')"
                                    + "  AND (A.BookingId = @BookingPin OR A.BookingPin=@BookingPin )  "
                                    + "   UNION ALL "
                                    + "  SELECT A.BookingId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BookingDuration, A.ActualBoatNum, "
                                    + "  E.BoatNum, A.RowerId, F.RowerName, Convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                    + "  A.BookingPin, convert(nvarchar, CAST(A.TripEndTime as time), 100) AS TripEndTime, "
                                    + "   DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS 'TravelledMinutes', A.PremiumStatus AS 'BDtlPremiumStatus' FROM BookingDtl AS A "
                                    + "   INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                                    + "  INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                                    + "   INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = B.BoatHouseId "
                                    + "   LEFT JOIN BoatMaster AS E ON A.ActualBoatId = E.BoatId AND A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND "
                                    + "  A.BoatHouseId = E.BoatHouseId LEFT JOIN RowerMaster AS F ON A.RowerId = F.RowerId AND "
                                    + "   A.BoatHouseId = F.BoatHouseId "
                                    + "   LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                    + "   AND A.BoatSeaterId IN (SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value)"
                                    + "   AND A.BoatHouseId = H.BoatHouseId "
                                    + "   WHERE A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL AND A.PremiumStatus != 'I' AND "
                                    + "   A.BookingPin in (SELECT BookingPin FROM BookingResched  WITH(NOLOCK) "
                                    + "   where CAST(BookingNewDate AS DATE) = CAST(GETDATE() AS DATE) and BoatHouseId = @BoatHouseId AND ActiveStatus='A' "
                                    + " AND (BookingPin = @BookingPin OR BookingId = @BookingPin)) AND A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId "
                                    + "  AND H.ActiveStatus = 'A' AND (A.BookingId =@BookingPin OR A.BookingPin=@BookingPin )   AND A.Status IN('R')) AS A "
                                    + "   ORDER BY A.TripEndTime DESC ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = Trip.UserId.Trim();
                    cmd.Parameters["@BookingPin"].Value = Trip.BookingPin.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TripSheetWeb ShowTrip = new TripSheetWeb();
                            ShowTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowTrip.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowTrip.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowTrip.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowTrip.RowerName = dt.Rows[i]["RowerName"].ToString();
                            ShowTrip.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowTrip.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowTrip.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            ShowTrip.TraveledTime = dt.Rows[i]["TravelledMinutes"].ToString();
                            ShowTrip.BDtlPremiumStatus = dt.Rows[i]["BDtlPremiumStatus"].ToString();
                            ShowTrip.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                            li.Add(ShowTrip);
                        }
                        TripSheetWebList ConfList = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        TripSheetWebString ConfRes1 = new TripSheetWebString
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    TripSheetWebString ConfRes = new TripSheetWebString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                TripSheetWebString ConfRes = new TripSheetWebString
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
        ///  Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAllTripEndDefault")]
        public IHttpActionResult gettripsheetEndDefault([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand(" SELECT D.BookingId,A.BookingSerial,D.UserId,convert(nvarchar,CAST(D.BookingDate as time),100) as BoardingTime, "
                            + " A.BoatHouseId, A.BoatHouseName,A.BoatDeposit, A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                            + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType,B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                            + " C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                            + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                            + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', "
                            + "  DATEADD(mi, A.BookingDuration,CAST(A.TripStartTime AS TIME)) as 'DefaultEndTime1',"
                            + " CONVERT(varchar(15),CAST( DATEADD(mi, A.BookingDuration,CAST(A.TripStartTime AS TIME)) AS TIME),100) AS 'DefaultEndTime'"
                            + " FROM BookingDtl as A "
                            + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                            + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                            + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                            + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                            + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                            + " AND A.Status IN('B', 'R') AND A.TripStartTime != '' AND A.TripEndTime IS NULL  and A.BoatDeposit= '0.00' ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.DefaultEndTime = dt.Rows[i]["DefaultEndTime"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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
        ///  Modified BY  : Silambarasu
        /// Modified Date : 2023-04-27
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetweb/ListAllTripEndDefault")]
        public IHttpActionResult NewgettripsheetEndDefault([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.UserId != null && bHMstr.UserId != "")
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand(" SELECT D.BookingId,A.BookingSerial,D.UserId,convert(nvarchar,CAST(D.BookingDate as time),100) as BoardingTime, "
                                   + " A.BoatHouseId, A.BoatHouseName,A.BoatDeposit ,A.BoatReferenceNo, A.BookingPin, D.PremiumStatus, A.BookingDuration, "
                                   + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType, B.BoatId, B.BoatNum, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                                   + " C.RowerName, convert(nvarchar, CAST(A.TripStartTime as time), 100) AS TripStartTime, "
                                   + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime, "
                                   + " CONVERT(varchar(15), CAST(A.ExpectedTime AS TIME), 100) as 'ExpectedTime', "
                                   + "  DATEADD(mi, A.BookingDuration,CAST(A.TripStartTime AS TIME)) as 'DefaultEndTime1',"
                                   + " CONVERT(varchar(15),CAST( DATEADD(mi, A.BookingDuration,CAST(A.TripStartTime AS TIME)) AS TIME),100) AS 'DefaultEndTime'"
                                   + " FROM BookingDtl as A "
                                   + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                                   + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId LEFT JOIN RowerMaster As C "
                                   + " on A.RowerId = C.RowerId INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                                   + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                                   + " LEFT JOIN ScanUserRightsDetails AS H ON A.BoatTypeId = H.BoatTypeId "
                                   + " AND A.BoatSeaterId IN(SELECT Value FROM[dbo].[CSVToList](H.SeaterId) AS I WHERE Value <> '' AND A.BoatSeaterId = I.Value) "
                                   + " AND A.BoatHouseId = H.BoatHouseId "
                                   + " WHERE A.BoatHouseId = @BoatHouseId AND H.UserId = @UserId AND H.ActiveStatus = 'A' "
                                   + " AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) "
                                   + " AND A.Status IN('B', 'R') AND A.TripStartTime != '' AND A.TripEndTime IS NULL  and A.BoatDeposit= '0.00' ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            tripsheets.UserId = dt.Rows[i]["UserId"].ToString();
                            tripsheets.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            tripsheets.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            tripsheets.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();
                            tripsheets.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            tripsheets.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            tripsheets.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            tripsheets.DefaultEndTime = dt.Rows[i]["DefaultEndTime"].ToString();

                            li.Add(tripsheets);
                        }

                        TripSheetWebList tripsheet = new TripSheetWebList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(tripsheet);
                    }

                    else
                    {
                        TripSheetWebString BoatHouse = new TripSheetWebString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
                    }
                }
                else
                {
                    TripSheetWebString Vehicle = new TripSheetWebString
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

        [HttpPost]
        [AllowAnonymous]
        [Route("NewTripSheetWeb/UpdateEndDeafult")]
        public IHttpActionResult NewTripsheetwebEndDeafult([FromBody] TripSheetWeb Trip)
        {
            try
            {


                string sReturn = string.Empty;

                SqlCommand cmd = new SqlCommand("TripSheetWebNewDefaultEnd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;


                cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.ToString());


                if (Trip.SDUserBy.Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@SDUserBy", Trip.SDUserBy).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SDUserBy", Trip.SDUserBy.ToString());
                }



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
                    TripSheetWebString ConMstr = new TripSheetWebString
                    {
                        Response = sResult[1].Trim(),
                        StatusCode = 1
                    };
                    return Ok(ConMstr);
                }
                else
                {
                    TripSheetWebString ConMstr = new TripSheetWebString
                    {
                        Response = sResult[1].Trim(),
                        StatusCode = 0
                    };
                    return Ok(ConMstr);
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
    }
}

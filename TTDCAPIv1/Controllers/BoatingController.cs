using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
using System.Web.Http;
using TTDCAPIv1.Models;

namespace TTDCAPIv1.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class BoatingController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_BoatHouse"].ConnectionString);
        SqlConnection con_Common = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_Common"].ConnectionString);

        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

        /***Department Boating***/
        //Get Boat Category List
        [HttpPost]
        [AllowAnonymous]
        [Route("NewBoatBookingDet")]
        public IHttpActionResult NewBoatBookingDet([FromBody] NewBoatBooked btBook)
        {
            try
            {
                if (btBook.BoatHouseId != "")
                {
                    List<NewBoatBooked> li = new List<NewBoatBooked>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "NewBoatBooking");
                    cmd.Parameters.AddWithValue("@BoatHouseId", btBook.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(btBook.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(btBook.BookingDate.Trim(), objEnglishDate));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            NewBoatBooked bt = new NewBoatBooked();
                            bt.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            bt.BoatType = dt.Rows[i]["BoatType"].ToString();
                            bt.NormalAvailable = dt.Rows[i]["NormalAvailable"].ToString();
                            bt.NormalExpTripTime = dt.Rows[i]["NormalExpTripTime"].ToString();
                            bt.NormalMaxFare = dt.Rows[i]["NormalMaxFare"].ToString();
                            bt.PremiumAvailable = dt.Rows[i]["PremiumAvailable"].ToString();
                            bt.PremiumExpTripTime = dt.Rows[i]["PremiumExpTripTime"].ToString();
                            bt.PremiumMaxFare = dt.Rows[i]["PremiumMaxFare"].ToString();
                            bt.NormalWaitingTrip = dt.Rows[i]["NormalWaitingTrip"].ToString();
                            bt.PremiumWaitingTrip = dt.Rows[i]["PremiumWaitingTrip"].ToString();
                            bt.BookedNormalOnline = dt.Rows[i]["BookedNormalOnline"].ToString();
                            bt.BookedPremiumOnline = dt.Rows[i]["BookedPremiumOnline"].ToString();
                            bt.BookedNormalBoatHouse = dt.Rows[i]["BookedNormalBoatHouse"].ToString();
                            bt.BookedPremiumBoatHouse = dt.Rows[i]["BookedPremiumBoatHouse"].ToString();
                            bt.TotalNormalTrip = dt.Rows[i]["TotalNormalTrip"].ToString();
                            bt.TotalPremiumTrip = dt.Rows[i]["TotalPremiumTrip"].ToString();
                            bt.NormalTripCompleted = dt.Rows[i]["NormalTripCompleted"].ToString();
                            bt.PremiumTripCompleted = dt.Rows[i]["PremiumTripCompleted"].ToString();
                            li.Add(bt);
                        }

                        NewBoatBookedList btl = new NewBoatBookedList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(btl);
                    }
                    else
                    {
                        NewBoatBookedString Bts = new NewBoatBookedString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(Bts);
                    }
                }
                else
                {

                    NewBoatBookedString Vehicle = new NewBoatBookedString
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
        /// MODIFIED BY   : VINITHA M
        /// MODIFIED DATE : 23-SEP-2021
        /// </summary>
        /// <param name="btBook"></param>
        /// <returns></returns>        
        [HttpPost]
        [AllowAnonymous]
        [Route("NewBoatBookingRights")]
        public IHttpActionResult NewBoatBookingRights([FromBody] NewBoatBooked btBook)
        {
            try
            {
                if (btBook.BoatHouseId != "")
                {
                    List<NewBoatBooked> li = new List<NewBoatBooked>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "NewBoatBookingRights");
                    cmd.Parameters.AddWithValue("@BoatHouseId", btBook.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(btBook.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(btBook.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@UserId", btBook.UserId);
                    cmd.Parameters.AddWithValue("@ServiceType", btBook.UserRole);
                    cmd.Parameters.AddWithValue("@PremiumStatus", btBook.PremiumStatus);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            NewBoatBooked bt = new NewBoatBooked();
                            bt.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            bt.BoatType = dt.Rows[i]["BoatType"].ToString();
                            bt.NormalAvailable = dt.Rows[i]["NormalAvailable"].ToString();
                            bt.NormalExpTripTime = dt.Rows[i]["NormalExpTripTime"].ToString();
                            bt.NormalMaxFare = dt.Rows[i]["NormalMaxFare"].ToString();
                            bt.PremiumAvailable = dt.Rows[i]["PremiumAvailable"].ToString();
                            bt.PremiumExpTripTime = dt.Rows[i]["PremiumExpTripTime"].ToString();
                            bt.PremiumMaxFare = dt.Rows[i]["PremiumMaxFare"].ToString();
                            bt.NormalWaitingTrip = dt.Rows[i]["NormalWaitingTrip"].ToString();
                            bt.PremiumWaitingTrip = dt.Rows[i]["PremiumWaitingTrip"].ToString();
                            bt.BookedNormalOnline = dt.Rows[i]["BookedNormalOnline"].ToString();
                            bt.BookedPremiumOnline = dt.Rows[i]["BookedPremiumOnline"].ToString();
                            bt.BookedNormalBoatHouse = dt.Rows[i]["BookedNormalBoatHouse"].ToString();
                            bt.BookedPremiumBoatHouse = dt.Rows[i]["BookedPremiumBoatHouse"].ToString();
                            bt.TotalNormalTrip = dt.Rows[i]["TotalNormalTrip"].ToString();
                            bt.TotalPremiumTrip = dt.Rows[i]["TotalPremiumTrip"].ToString();
                            bt.NormalTripCompleted = dt.Rows[i]["NormalTripCompleted"].ToString();
                            bt.PremiumTripCompleted = dt.Rows[i]["PremiumTripCompleted"].ToString();
                            bt.TotalNoofTrips = dt.Rows[i]["TotalNoofTrips"].ToString();
                            bt.TotalNoofBoats = dt.Rows[i]["TotalNoofBoats"].ToString();
                            li.Add(bt);
                        }

                        NewBoatBookedList btl = new NewBoatBookedList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(btl);
                    }
                    else
                    {
                        NewBoatBookedString Bts = new NewBoatBookedString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(Bts);
                    }
                }
                else
                {

                    NewBoatBookedString Vehicle = new NewBoatBookedString
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
        /// Modified By : Vinitha
        /// Modified Date : 18-10-2021
        /// </summary>
        /// <param name="BoatAvList"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatBooking/BoatAvailableList")]
        public IHttpActionResult BoatAvailableList([FromBody] BoatAvailable BoatAvList)
        {
            try
            {
                if (BoatAvList.BoatHouseId != "" && BoatAvList.PremiumStatus != "")
                {
                    List<BoatAvailable> li = new List<BoatAvailable>();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("BoatAvailableDetails", con);
                    con.Open();
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Clear();
                    da.SelectCommand.CommandTimeout = 500000;
                    da.SelectCommand.Parameters.Add("@BoatHouseId", SqlDbType.VarChar).Value = BoatAvList.BoatHouseId.Trim();
                    da.SelectCommand.Parameters.Add("@PremiumStatus", SqlDbType.VarChar).Value = BoatAvList.PremiumStatus.Trim();
                    da.SelectCommand.Parameters.Add("@BookingDate", SqlDbType.VarChar).Value = DateTime.Parse(BoatAvList.BookingDate.Trim(), objEnglishDate);
                    da.SelectCommand.Parameters.Add("@BoatTypeId", SqlDbType.VarChar).Value = BoatAvList.BoatTypeId;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatAvailable Botlst = new BoatAvailable();

                            Botlst.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            Botlst.BoatType = dt.Rows[i]["BoatType"].ToString();
                            Botlst.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            Botlst.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            Botlst.NoOfSeats = dt.Rows[i]["NoOfSeats"].ToString();

                            Botlst.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            Botlst.DepositType = dt.Rows[i]["DepositType"].ToString();
                            Botlst.Deposit = dt.Rows[i]["Deposit"].ToString();
                            Botlst.TimeExtension = dt.Rows[i]["TimeExtension"].ToString();
                            Botlst.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();

                            Botlst.BoatExtnTime = dt.Rows[i]["BoatExtnTime"].ToString();
                            Botlst.BoatGraceTime = dt.Rows[i]["BoatGraceTime"].ToString();
                            Botlst.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            Botlst.BoatTotalCharge = dt.Rows[i]["BoatTotalCharge"].ToString();
                            Botlst.BoatMinCharge = dt.Rows[i]["BoatMinCharge"].ToString();

                            Botlst.BoatExtnCharge = dt.Rows[i]["BoatExtnCharge"].ToString();
                            Botlst.RowerMinCharge = dt.Rows[i]["RowerMinCharge"].ToString();
                            Botlst.RowerExtnCharge = dt.Rows[i]["RowerExtnCharge"].ToString();
                            Botlst.BoatExtnTotalCharge = dt.Rows[i]["BoatExtnTotalCharge"].ToString();
                            Botlst.BoatTaxCharge = dt.Rows[i]["BoatTaxCharge"].ToString();

                            Botlst.BoatExtnTaxCharge = dt.Rows[i]["BoatExtnTaxCharge"].ToString();
                            Botlst.BookedTrips = dt.Rows[i]["BookedTrips"].ToString();
                            Botlst.RemainTrips = dt.Rows[i]["RemainTrips"].ToString();
                            Botlst.MaxTripsPerDay = dt.Rows[i]["MaxTripsPerDay"].ToString();
                            Botlst.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();

                            Botlst.BoatImageLink = dt.Rows[i]["BoatImageLink"].ToString();
                            Botlst.BHShortCode = dt.Rows[i]["BHShortCode"].ToString();
                            Botlst.TotalNoofBoats = dt.Rows[i]["TotalNoofBoats"].ToString();

                            li.Add(Botlst);
                        }

                        BoatAvailableList Bolist = new BoatAvailableList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(Bolist);

                    }
                    else
                    {
                        UserLoginRes ConfRes = new UserLoginRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    UserLoginRes Vehicle = new UserLoginRes
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Add Parameterised Query for SQl Injection
        /// </summary>
        /// <param name="BoatHouseId"></param>
        /// <param name="BookingId"></param>
        public void GetCurrentBookingId(string BoatHouseId, string BookingId)
        {
            string sReturn = string.Empty;
            DataTable dt = new DataTable();

            try
            {
                string sQuery = string.Empty;

                sQuery = "SELECT Count(*) AS 'Count' FROM BookingDtl WHERE BoatHouseId = @BoatHouseId AND BookingId = @BookingId AND ExpectedTime IS NULL ";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                cmd.Parameters["@BoatHouseId"].Value = BoatHouseId.Trim();
                cmd.Parameters["@BookingId"].Value = BookingId.Trim();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    int iCount = Convert.ToInt32(dt.Rows[0]["Count"].ToString());

                    if (iCount > 0)
                    {
                        sQuery = "EXECUTE spGetNextBoatDetail @BoatHouseId, @BookingId";

                        SqlCommand cmd1 = new SqlCommand(sQuery, con);
                        cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                        cmd1.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                        cmd1.Parameters["@BoatHouseId"].Value = BoatHouseId.Trim();
                        cmd1.Parameters["@BookingId"].Value = BookingId.Trim();

                        con.Open();
                        int iReturn = cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                dt.Dispose();
            }
        }

        // Credit Boat Booking
        [HttpPost]
        [AllowAnonymous]
        [Route("CreditBoatBookingService")]
        public IHttpActionResult CreditBoatBookingService([FromBody] BoatBooking BB)
        {
            try
            {
                if (BB.QueryType != "" && BB.BookingDate != "" && BB.BoatHouseId != "" && BB.BoatHouseName != "" && BB.PremiumStatus != ""
                    && BB.BoatTypeId != "" && BB.BoatSeaterId != "" && BB.BookingDuration != ""
                    && BB.InitBoatCharge != "" && BB.InitNetAmount != "" && BB.CreatedBy != "" && BB.BookingMedia != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("InsertCreditBoatBooking", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    //Boat Booking
                    cmd.Parameters.AddWithValue("@QueryType", BB.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", BB.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BB.BookingDate.ToString(), objEnglishDate));

                    cmd.Parameters.AddWithValue("@BookingPin", BB.BookingPin.ToString());
                    cmd.Parameters.AddWithValue("@CustomerMobileNo", BB.CustomerMobileNo.ToString());
                    cmd.Parameters.AddWithValue("@PremiumStatus", BB.PremiumStatus.ToString());

                    cmd.Parameters.AddWithValue("@BoatTypeId", BB.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", BB.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BookingDuration", BB.BookingDuration.ToString());

                    cmd.Parameters.AddWithValue("@ActualBoatCharge", BB.InitBoatCharge.ToString());
                    cmd.Parameters.AddWithValue("@ActualRowerCharge", BB.InitRowerCharge.ToString());
                    cmd.Parameters.AddWithValue("@ActualNetAmount", BB.InitNetAmount.ToString());

                    cmd.Parameters.AddWithValue("@BookingMedia", BB.BookingMedia.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", BB.CreatedBy.ToString());

                    cmd.Parameters.AddWithValue("@BoatHouseId", BB.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", BB.BoatHouseName.ToString());

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
                        BoatBookingStr TxMstr = new BoatBookingStr
                        {
                            Response = sResult[1].Trim() + '~' + sResult[2].Trim(),
                            StatusCode = 1
                        };
                        return Ok(TxMstr);
                    }
                    else
                    {
                        BoatBookingStr TxMstr = new BoatBookingStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(TxMstr);
                    }
                }
                else
                {
                    BoatBookingStr TxMstr = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(TxMstr);
                }
            }
            catch (Exception ex)
            {
                BoatBookingStr TxMstr = new BoatBookingStr
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(TxMstr);
            }
        }

        // Boat Booking Count & Amount
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Add Parameterised Query for SQl Injection
        /// </summary>
        /// <param name="dtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CreditBookingDetailsCount")]
        public IHttpActionResult CreditBookingDetailsCount([FromBody] BoatingReport dtl)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string sBookingMedia = string.Empty;

                squery = "SELECT COUNT(*) AS 'Count', SUM(ActualNetAmount) AS 'NetAmount' FROM CreditBoatBooking WHERE "
                    + " CAST(BookingDate AS DATE) = @BookingDate "
                    + " AND BStatus IN ('B') AND BoatHouseId = @BoatHouseId ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = dtl.BoatHouseId.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(dtl.BookingDate.Trim(), objEnglishDate);

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
            catch (Exception ex)
            {
                BoatingReportres ConfRes = new BoatingReportres
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Add Parameterised Query for SQl Injection
        /// </summary>
        /// <param name="dtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CreditBookedDetails")]
        public IHttpActionResult CreditBookedDetails([FromBody] BoatingReport dtl)
        {
            try
            {
                string squery = string.Empty;

                squery = "SELECT BookingId, CONVERT(VARCHAR, BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, BookingDate, 100),7) AS 'BookingDate', "
                    + " BookedPin, COUNT(*) AS 'BoatCount', CASE WHEN PremiumStatus = 'P' THEN 'Premium' ELSE 'Normal' END AS 'BookingStatus', "
                    + " SUM(ActualNetAmount) AS 'NetAmount', BookingDate AS 'OrderDate' FROM CreditBoatBooking "
                    + " WHERE CAST(BookingDate AS DATE) BETWEEN @FromDate AND @ToDate AND BoatHouseId = @BoatHouseId AND BStatus = 'B' "
                    + " GROUP BY BookingId, BookedPin, BookingDate, PremiumStatus ORDER BY OrderDate";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = dtl.BoatHouseId.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(dtl.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(dtl.ToDate.Trim(), objEnglishDate);
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
            catch (Exception ex)
            {
                BoatingReportres ConfRes = new BoatingReportres
                {
                    Response = Convert.ToString(ex),
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
        [Route("CreditBoatBookedTicket")]
        public IHttpActionResult CreditBoatBookedTicket([FromBody] CreditBoatTicket Bt)
        {
            try
            {
                List<CreditBoatTicket> li = new List<CreditBoatTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "CreditBoatPrintTicket");
                cmd.Parameters.AddWithValue("@BoatHouseId", Bt.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BookingId", Bt.BookingId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CreditBoatTicket bt = new CreditBoatTicket();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();

                        bt.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        bt.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                        bt.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();

                        bt.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        bt.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();

                        bt.BoatType = dt.Rows[i]["BoatType"].ToString();
                        bt.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                        bt.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();

                        bt.ActualBoatCharge = dt.Rows[i]["ActualBoatCharge"].ToString();
                        bt.ActualRowerCharge = dt.Rows[i]["ActualRowerCharge"].ToString();
                        bt.ActualNetAmount = dt.Rows[i]["ActualNetAmount"].ToString();

                        bt.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();

                        bt.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                        bt.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        bt.BookingMedia = dt.Rows[i]["BookingMedia"].ToString();

                        bt.BoatTypeSno = dt.Rows[i]["BoatTypeSno"].ToString();

                        li.Add(bt);
                    }

                    CreditBoatTicketlist btl = new CreditBoatTicketlist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(btl);
                }
                else
                {
                    CreditBoatTicketStr Bts = new CreditBoatTicketStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(Bts);
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Add Parameterised Query for SQl Injection 
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RprCreditTripWiseDetailsDateAndMonthWise")]
        public IHttpActionResult RprCreditTripWiseDetailsDateAndMonthWise([FromBody] TripSheet tripBook)
        {
            try
            {
                string sQuery = string.Empty;

                if (tripBook.BoatTypeId == "0")
                {
                    sQuery = "SELECT A.BookingId,A.BookingDate,B.BoatType+' '+C.SeaterType AS 'BoatName', "
                           + "A.BoatCount,A.BoatCharge,A.RowerCharge,A.NetAmount,A.BoatTypeId,A.BoatSeaterId FROM "
                           + "( "
                           + "SELECT BookingId, CONVERT(NVARCHAR, BookingDate,103) AS 'BookingDate',COUNT(BookingId) AS BoatCount, "
                           + "SUM(ActualBoatCharge)AS 'BoatCharge', SUM(ActualRowerCharge) AS 'RowerCharge', "
                           + "SUM(ActualNetAmount) AS 'NetAmount',BoatTypeId, BoatSeaterId, BoatHouseId "
                           + "FROM CreditBoatBooking WHERE BoatHouseId = @BoatHouseId AND "
                           + "BStatus IN ('B') "
                           + "AND CAST(BookingDate AS DATE) BETWEEN @FromDate "
                           + "AND @ToDate "
                           + "GROUP BY BookingId, BoatTypeId, BoatSeaterId, BookingDate, BoatHouseId "
                           + ") AS A "
                           + "LEFT JOIN "
                           + "( "
                           + "SELECT * FROM BoatTypes WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId "
                           + ") AS B ON A.BoatHouseId = B.BoatHouseId AND A.BoatTypeId = B.BoatTypeId "
                           + "LEFT JOIN "
                           + "( "
                           + "SELECT * FROM BoatSeat WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId "
                           + ") AS C ON A.BoatHouseId = C.BoatHouseId AND A.BoatSeaterId = C.BoatSeaterId ORDER BY A.BookingId";
                }
                else
                {
                    sQuery = "SELECT A.BookingId,A.BookingDate,B.BoatType+' '+C.SeaterType AS 'BoatName', "
                            + "A.BoatCount,A.BoatCharge,A.RowerCharge,A.NetAmount,A.BoatTypeId,A.BoatSeaterId FROM "
                            + "( "
                            + "SELECT BookingId, CONVERT(NVARCHAR, BookingDate,103) AS 'BookingDate',COUNT(BookingId) AS BoatCount, "
                            + "SUM(ActualBoatCharge)AS 'BoatCharge', SUM(ActualRowerCharge) AS 'RowerCharge', "
                            + "SUM(ActualNetAmount) AS 'NetAmount',BoatTypeId, BoatSeaterId, BoatHouseId "
                            + "FROM CreditBoatBooking WHERE BoatHouseId = @BoatHouseId AND "
                            + "BStatus IN ('B') "
                            + "AND CAST(BookingDate AS DATE) BETWEEN @FromDate "
                            + "AND @ToDate AND "
                            + "BoatTypeId=@BoatTypeId "
                            + "GROUP BY BookingId, BoatTypeId, BoatSeaterId, BookingDate, BoatHouseId "
                            + ") AS A "
                            + "LEFT JOIN "
                            + "( "
                            + "SELECT * FROM BoatTypes WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId "
                            + ") AS B ON A.BoatHouseId = B.BoatHouseId AND A.BoatTypeId = B.BoatTypeId "
                            + "LEFT JOIN "
                            + "( "
                            + "SELECT * FROM BoatSeat WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId "
                            + ") AS C ON A.BoatHouseId = C.BoatHouseId AND A.BoatSeaterId = C.BoatSeaterId ORDER BY A.BookingId";
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = tripBook.BoatTypeId.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(tripBook.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(tripBook.ToDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    return Ok(dt);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        //Boat booking Print instructions
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Add Parameterised Query for SQl Injection
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PrintInstrucSvc")]
        public IHttpActionResult PrintingInstructionSvc([FromBody] PrintingInstruction Trip)
        {
            try
            {
                List<PrintingInstruction> li = new List<PrintingInstruction>();
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT UniqueId, ServiceType, "
                    + " CASE WHEN ServiceType = '1' THEN 'Boating' WHEN ServiceType = '2' THEN 'Other Services' "
                    + " WHEN ServiceType = '3' THEN 'Restaurant' END AS 'ServiceName', InstructionDtl, BoatHouseName, ActiveStatus FROM "
                    + " PrintingInstruction WHERE BoatHouseId = @BoatHouseId AND ServiceType = @ServiceType AND ActiveStatus = 'A' ORDER BY UniqueId ", con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ServiceType", System.Data.SqlDbType.NVarChar, 50));

                cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                cmd.Parameters["@ServiceType"].Value = Trip.ServiceType.Trim();


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PrintingInstruction ShowBoatid = new PrintingInstruction();
                        ShowBoatid.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        ShowBoatid.ServiceType = dt.Rows[i]["ServiceType"].ToString();
                        ShowBoatid.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        ShowBoatid.InstructionDtl = dt.Rows[i]["InstructionDtl"].ToString();
                        ShowBoatid.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ShowBoatid.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowBoatid);
                    }

                    PrintingInstructionList ConfList = new PrintingInstructionList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    PrintingInstructionres ConfRes = new PrintingInstructionres
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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

        // Boat booked list on Grid
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Btl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatBookedList")]
        public IHttpActionResult GetBoatBookedList([FromBody] BoatTicketDtl Btl)
        {
            try
            {

                List<BoatTicketDtl> li = new List<BoatTicketDtl>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BoatBookedDetails");
                cmd.Parameters.AddWithValue("@BoatHouseId", Btl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Btl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Btl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Btl.UserId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTicketDtl bt = new BoatTicketDtl();
                        bt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                        bt.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                        bt.BookingStatus = dt.Rows[i]["BookingStatus"].ToString();
                        bt.InitBillAmount = dt.Rows[i]["InitBillAmount"].ToString();
                        bt.OtherService = dt.Rows[i]["OtherService"].ToString();
                        bt.CustomerEmailId = dt.Rows[i]["CustomerEmailId"].ToString();
                        bt.CustomerGSTNo = dt.Rows[i]["CustomerGSTNo"].ToString();
                        bt.Status = dt.Rows[i]["Status"].ToString();
                        bt.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                        li.Add(bt);
                    }

                    BoatTicketDtllist Bdl = new BoatTicketDtllist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Bdl);
                }
                else
                {
                    BoatTicketDtlStr FBRes = new BoatTicketDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(FBRes);
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
        /// Created By : Abhinaya
        /// Created On : 2022-04-18
        /// Version : V2
        /// </summary>
        /// <param name="Btl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatBookedListV2")]
        public IHttpActionResult GetBoatBookedListV2([FromBody] BoatTicketDtl Btl)
        {
            try
            {
                int endcount = Int32.Parse(Btl.CountStart.Trim()) + 9;

                List<BoatTicketDtl> li = new List<BoatTicketDtl>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BoatBookedDetailsV2");
                cmd.Parameters.AddWithValue("@BoatHouseId", Btl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Btl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Btl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Btl.UserId.Trim());
                cmd.Parameters.AddWithValue("@Input1", Btl.CountStart.Trim());
                cmd.Parameters.AddWithValue("@Input2", endcount.ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTicketDtl bt = new BoatTicketDtl();
                        bt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                        bt.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                        bt.BookingStatus = dt.Rows[i]["BookingStatus"].ToString();
                        bt.InitBillAmount = dt.Rows[i]["InitBillAmount"].ToString();
                        bt.OtherService = dt.Rows[i]["OtherService"].ToString();
                        bt.CustomerEmailId = dt.Rows[i]["CustomerEmailId"].ToString();
                        bt.CustomerGSTNo = dt.Rows[i]["CustomerGSTNo"].ToString();
                        bt.Status = dt.Rows[i]["Status"].ToString();
                        bt.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                        bt.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                        li.Add(bt);
                    }

                    BoatTicketDtllist Bdl = new BoatTicketDtllist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Bdl);
                }
                else
                {
                    BoatTicketDtlStr FBRes = new BoatTicketDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(FBRes);
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
        /// Created By : Abhinaya
        /// Created On : 2022-04-18
        /// Version : V2
        /// Modified By : Abhinaya
        /// Modified Date : 2022-04-25
        /// Version : V2
        /// </summary>
        /// <param name="Btl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatBookedListPinV2")]
        public IHttpActionResult GetBoatBookedListPinV2([FromBody] BoatTicketDtl Btl)
        {
            try
            {
                int endcount = Int32.Parse(Btl.CountStart.Trim()) + 9;
                List<BoatTicketDtl> li = new List<BoatTicketDtl>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BoatBookedDetailsPinV2");
                cmd.Parameters.AddWithValue("@BoatHouseId", Btl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Btl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Btl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Btl.UserId.Trim());
                cmd.Parameters.AddWithValue("@Input1", Btl.CountStart.Trim());
                cmd.Parameters.AddWithValue("@Input2", endcount);
                cmd.Parameters.AddWithValue("@Input3", Btl.Search.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTicketDtl bt = new BoatTicketDtl();
                        bt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                        bt.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                        bt.BookingStatus = dt.Rows[i]["BookingStatus"].ToString();
                        bt.InitBillAmount = dt.Rows[i]["InitBillAmount"].ToString();
                        bt.OtherService = dt.Rows[i]["OtherService"].ToString();
                        bt.CustomerEmailId = dt.Rows[i]["CustomerEmailId"].ToString();
                        bt.CustomerGSTNo = dt.Rows[i]["CustomerGSTNo"].ToString();
                        bt.Status = dt.Rows[i]["Status"].ToString();
                        bt.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                        bt.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                        li.Add(bt);
                    }

                    BoatTicketDtllist Bdl = new BoatTicketDtllist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Bdl);
                }
                else
                {
                    BoatTicketDtlStr FBRes = new BoatTicketDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(FBRes);
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
        /// Modified Date : 13-10-2021
        /// Modified By : Vediyappan.V
        /// Modified Date : 15-10-2021
        /// Add New Field BankName for Return Value.
        /// Modified By Abhinaya K
        /// </summary>
        /// <param name="Bt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatBookedTicket")]
        public IHttpActionResult GetBoatTicket([FromBody] BoatTicket Bt)
        {
            try
            {
                List<BoatTicket> li = new List<BoatTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BoatPrintTicket");
                cmd.Parameters.AddWithValue("@BoatHouseId", Bt.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BookingId", Bt.BookingId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTicket bt = new BoatTicket();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        bt.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                        bt.CustomerName = dt.Rows[i]["CustomerName"].ToString();

                        bt.CustomerAddress = dt.Rows[i]["CustomerAddress"].ToString();
                        bt.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                        bt.NoOfPass = dt.Rows[i]["NoOfPass"].ToString();
                        bt.NoOfChild = dt.Rows[i]["NoOfChild"].ToString();
                        bt.NoofInfant = dt.Rows[i]["NoofInfant"].ToString();

                        bt.OfferName = dt.Rows[i]["OfferName"].ToString();
                        bt.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();
                        bt.InitBillAmount = dt.Rows[i]["InitBillAmount"].ToString();
                        bt.BoatType = dt.Rows[i]["BoatType"].ToString();
                        bt.SeaterType = dt.Rows[i]["SeaterType"].ToString();

                        bt.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                        bt.InitBoatCharge = dt.Rows[i]["InitBoatCharge"].ToString();
                        bt.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
                        bt.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                        bt.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();

                        bt.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                        bt.InitOfferAmount = dt.Rows[i]["InitOfferAmount"].ToString();
                        bt.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                        bt.CustomerId = dt.Rows[i]["CustomerId"].ToString();
                        bt.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();

                        bt.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        bt.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();
                        bt.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                        bt.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                        bt.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                        bt.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                        bt.BookingMedia = dt.Rows[i]["BookingMedia"].ToString();
                        bt.CheckDate = dt.Rows[i]["CheckDate"].ToString();
                        bt.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                        bt.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();

                        bt.TripFlag = dt.Rows[i]["TripFlag"].ToString();
                        bt.BoatTypeSno = dt.Rows[i]["BoatTypeSno"].ToString();
                        bt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        bt.BHShortCode = dt.Rows[i]["BHShortCode"].ToString();
                        bt.OrderId = dt.Rows[i]["OrderId"].ToString();

                        bt.TrackingId = dt.Rows[i]["TrackingId"].ToString();
                        bt.BankReferenceNo = dt.Rows[i]["BankReferenceNo"].ToString();
                        bt.TimeSlot = dt.Rows[i]["TimeSlot"].ToString();
                        bt.RescheduledDate = dt.Rows[i]["RescheduledDate"].ToString();
                        bt.RescheduledAmount = dt.Rows[i]["RescheduledAmount"].ToString();
                        bt.BankName = dt.Rows[i]["BankName"].ToString();

                        //Newly added by imran - 13-10-2021, Public booking
                        //bt.TrackingId = dt.Rows[i]["TrackingId"].ToString();
                        //bt.BankReferenceNo = dt.Rows[i]["BankReferenceNo"].ToString();

                        li.Add(bt);
                    }

                    BoatTicketlist btl = new BoatTicketlist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(btl);
                }

                else
                {
                    return GetHistoryBoatBookedTicket(Bt);
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
        /// Created BY Abhinaya K
        /// </summary>
        /// <param name="BoatHouseId"></param>
        /// <param name="BookingId"></param>
        public IHttpActionResult GetHistoryBoatBookedTicket([FromBody] BoatTicket Bt)
        {
            try
            {
                List<BoatTicket> li = new List<BoatTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BoatPrintTicketHistory");
                cmd.Parameters.AddWithValue("@BoatHouseId", Bt.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BookingId", Bt.BookingId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTicket bt = new BoatTicket();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        bt.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
                        bt.CustomerName = dt.Rows[i]["CustomerName"].ToString();

                        bt.CustomerAddress = dt.Rows[i]["CustomerAddress"].ToString();
                        bt.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                        bt.NoOfPass = dt.Rows[i]["NoOfPass"].ToString();
                        bt.NoOfChild = dt.Rows[i]["NoOfChild"].ToString();
                        bt.NoofInfant = dt.Rows[i]["NoofInfant"].ToString();

                        bt.OfferName = dt.Rows[i]["OfferName"].ToString();
                        bt.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();
                        bt.InitBillAmount = dt.Rows[i]["InitBillAmount"].ToString();
                        bt.BoatType = dt.Rows[i]["BoatType"].ToString();
                        bt.SeaterType = dt.Rows[i]["SeaterType"].ToString();

                        bt.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                        bt.InitBoatCharge = dt.Rows[i]["InitBoatCharge"].ToString();
                        bt.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
                        bt.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                        bt.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();

                        bt.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                        bt.InitOfferAmount = dt.Rows[i]["InitOfferAmount"].ToString();
                        bt.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                        bt.CustomerId = dt.Rows[i]["CustomerId"].ToString();
                        bt.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();

                        bt.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        bt.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();
                        bt.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                        bt.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                        bt.BookingPin = dt.Rows[i]["BookingPin"].ToString();

                        bt.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                        bt.BookingMedia = dt.Rows[i]["BookingMedia"].ToString();
                        bt.CheckDate = dt.Rows[i]["CheckDate"].ToString();
                        bt.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                        bt.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();

                        bt.TripFlag = dt.Rows[i]["TripFlag"].ToString();
                        bt.BoatTypeSno = dt.Rows[i]["BoatTypeSno"].ToString();
                        bt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        bt.BHShortCode = dt.Rows[i]["BHShortCode"].ToString();
                        bt.OrderId = dt.Rows[i]["OrderId"].ToString();

                        bt.TrackingId = dt.Rows[i]["TrackingId"].ToString();
                        bt.BankReferenceNo = dt.Rows[i]["BankReferenceNo"].ToString();
                        bt.TimeSlot = dt.Rows[i]["TimeSlot"].ToString();
                        bt.RescheduledDate = dt.Rows[i]["RescheduledDate"].ToString();
                        bt.RescheduledAmount = dt.Rows[i]["RescheduledAmount"].ToString();
                        bt.BankName = dt.Rows[i]["BankName"].ToString();


                        li.Add(bt);
                    }

                    BoatTicketlist btl = new BoatTicketlist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(btl);
                }

                else
                {
                    BoatTicketStr Bts = new BoatTicketStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(Bts);
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
        /// Created By Abhinaya K on 2022-05-20
        /// </summary>
        /// <param name="Ot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatOtherTicket")]
        public IHttpActionResult BoatOtherTicket([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BoatPrintOtherTicket");
                cmd.Parameters.AddWithValue("@BoatHouseId", Ot.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BookingId", Ot.BookingId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicket ot = new OtherTicket();

                        ot.BookingId = dt.Rows[i]["BookingId"].ToString();
                        ot.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                        ot.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        ot.BookingType = dt.Rows[i]["BookingType"].ToString();
                        ot.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();

                        ot.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        ot.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                        ot.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        ot.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        ot.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();

                        ot.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        ot.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();
                        ot.CheckDate = dt.Rows[i]["CheckDate"].ToString();

                        ot.OrderId = dt.Rows[i]["OrderId"].ToString();
                        ot.TrackingId = dt.Rows[i]["TrackingId"].ToString();
                        ot.BankReferenceNo = dt.Rows[i]["BankReferenceNo"].ToString();
                        ot.BankName = dt.Rows[i]["BankName"].ToString();
                        ot.RescheduledDate = dt.Rows[i]["RescheduledDate"].ToString();

                        li.Add(ot);
                    }

                    OtherTicketlist otl = new OtherTicketlist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(otl);
                }

                else
                {
                    return GetBoatOtherTicketHistory(Ot);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        public IHttpActionResult GetBoatOtherTicketHistory([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BoatPrintOtherTicketHistory");
                cmd.Parameters.AddWithValue("@BoatHouseId", Ot.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BookingId", Ot.BookingId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicket ot = new OtherTicket();

                        ot.BookingId = dt.Rows[i]["BookingId"].ToString();
                        ot.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                        ot.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        ot.BookingType = dt.Rows[i]["BookingType"].ToString();
                        ot.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();

                        ot.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        ot.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                        ot.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        ot.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        ot.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();

                        ot.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        ot.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();
                        ot.CheckDate = dt.Rows[i]["CheckDate"].ToString();

                        ot.OrderId = dt.Rows[i]["OrderId"].ToString();
                        ot.TrackingId = dt.Rows[i]["TrackingId"].ToString();
                        ot.BankReferenceNo = dt.Rows[i]["BankReferenceNo"].ToString();
                        ot.BankName = dt.Rows[i]["BankName"].ToString();
                        ot.RescheduledDate = dt.Rows[i]["RescheduledDate"].ToString();

                        li.Add(ot);
                    }

                    OtherTicketlist otl = new OtherTicketlist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(otl);
                }

                else
                {
                    OtherTicketStr ots = new OtherTicketStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ots);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        //Pin Transaction
        [HttpPost]
        [AllowAnonymous]
        [Route("CheckPinTransaction")]
        public IHttpActionResult CheckPinTransaction([FromBody] BookedPin ChkBP)
        {
            try
            {
                string sQuery = string.Empty;

                List<BookedPin> li = new List<BookedPin>();
                SqlCommand cmd = new SqlCommand("CheckPinTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@PinTran", ChkBP.BookingPin.Trim());
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(ChkBP.BookingDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@BoatHouseId", ChkBP.BoatHouseId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BookedPin ShowConfMstr = new BookedPin();
                        ChkBP.UserId = dt.Rows[i]["UserId"].ToString();
                        ChkBP.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        ChkBP.MailId = dt.Rows[i]["MailId"].ToString();
                        ChkBP.Name = dt.Rows[i]["Name"].ToString();
                        ChkBP.ResponseCode = dt.Rows[i]["ResponseCode"].ToString();
                        ChkBP.ResponseMsg = dt.Rows[i]["ResponseMsg"].ToString();
                        li.Add(ChkBP);
                    }

                    BookedPinList ConfList = new BookedPinList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    BookedPinStr ConfRes = new BookedPinStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                BookedPinStr ConfRes = new BookedPinStr
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

        //Save Pin Transaction
        [HttpPost]
        [AllowAnonymous]
        [Route("SavePinTransaction")]
        public IHttpActionResult SavePinTransaction([FromBody] BookedPin InsBP)
        {
            try
            {
                if (InsBP.UserId != "" && InsBP.BookingDate != "" && InsBP.BoatHouseId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("SavePinTransactionDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "Insert");
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(InsBP.BookingDate, objEnglishDate));
                    cmd.Parameters.AddWithValue("@UserId", InsBP.UserId.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", InsBP.MobileNo.Trim());
                    cmd.Parameters.AddWithValue("@BookingType", InsBP.BookingType.Trim());
                    cmd.Parameters.AddWithValue("@BookingMedia", InsBP.BookingMedia.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBP.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBP.BoatHouseName.Trim());

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
                        CompanyMasterRes InAppCar = new CompanyMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        CompanyMasterRes InAppCar = new CompanyMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    CompanyMasterRes InAppCar = new CompanyMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
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

        // Get Offer
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified into Parameterised Query 
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlOffer/BHId")]
        public IHttpActionResult ddlOfferBhId([FromBody] OfferMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<OfferMaster> li = new List<OfferMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT OfferId, OfferName FROM OfferMaster "
                        + " WHERE ActiveStatus = 'A' AND EffectiveFrom <= CAST(GETDATE()AS DATE) "
                        + " and EffectiveTill >= CAST(GETDATE() AS DATE) AND BoatHouseId LIKE  @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BoatHouseId"].Value = "%" + bHMstr.BoatHouseId.Trim() + "%";


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OfferMaster ShowTaxMaster = new OfferMaster();
                            ShowTaxMaster.OfferId = dt.Rows[i]["OfferId"].ToString();
                            ShowTaxMaster.OfferName = dt.Rows[i]["OfferName"].ToString();

                            li.Add(ShowTaxMaster);
                        }

                        OfferMasterList ConfList = new OfferMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        OfferMasterString ConfRes = new OfferMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatHouseMasterString Vehicle = new BoatHouseMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BoatHouseMasterString Vehicle = new BoatHouseMasterString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }

        //Offer Details by OfferId
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified into Parameterised Query 
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("OfferMstr/OfferEdit")]
        public IHttpActionResult OfferMstrId([FromBody] OfferMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.OfferId != "")
                {
                    List<OfferMaster> li = new List<OfferMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT OfferId, OfferType, OfferName, AmountType, Offer, MinBillAmount, "
                        + " MinNoOfTickets,Convert(Nvarchar(20),EffectiveFrom,105) 'EffectiveFrom', "
                        + " Convert(Nvarchar(20), EffectiveTill, 105) 'EffectiveTill', ActiveStatus, Createdby, Convert(Nvarchar(20), CreatedDate, 105) 'CreatedDate', "
                        + " Updatedby, Convert(Nvarchar(20), UpdatedDate, 105) 'UpdatedDate' from OfferMaster Where ActiveStatus IN ('A','D') "
                        + " AND BoatHouseId LIKE @BoatHouseId  AND OfferId = @OfferId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@OfferId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = "%" + bHMstr.BoatHouseId.Trim() + "%";
                    cmd.Parameters["@OfferId"].Value = bHMstr.OfferId.ToString().Trim();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OfferMaster OfferMasters = new OfferMaster();

                            OfferMasters.OfferId = dt.Rows[i]["OfferId"].ToString();
                            OfferMasters.OfferName = dt.Rows[i]["OfferName"].ToString();
                            OfferMasters.OfferType = dt.Rows[i]["OfferType"].ToString();
                            OfferMasters.AmountType = dt.Rows[i]["AmountType"].ToString();
                            OfferMasters.Offer = dt.Rows[i]["Offer"].ToString();
                            OfferMasters.MinBillAmount = dt.Rows[i]["MinBillAmount"].ToString();
                            OfferMasters.MinNoOfTickets = dt.Rows[i]["MinNoOfTickets"].ToString();
                            OfferMasters.EffectiveFrom = dt.Rows[i]["EffectiveFrom"].ToString();
                            OfferMasters.EffectiveTill = dt.Rows[i]["EffectiveTill"].ToString();
                            OfferMasters.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OfferMasters.CreatedBy = dt.Rows[i]["Createdby"].ToString();
                            OfferMasters.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();
                            OfferMasters.UpdatedBy = dt.Rows[i]["Updatedby"].ToString();
                            OfferMasters.UpdatedDate = dt.Rows[i]["UpdatedDate"].ToString();

                            li.Add(OfferMasters);

                        }

                        OfferMasterList OfferMaster = new OfferMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(OfferMaster);
                    }
                    else
                    {
                        OfferMasterString OfferMaster = new OfferMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(OfferMaster);
                    }
                }
                else
                {
                    BoatHouseMasterString Vehicle = new BoatHouseMasterString
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

        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("PrintingInstruction")]
        public IHttpActionResult PrintingInstruction([FromBody] PrintingInstruction InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.UniqueId != ""
                    && InsConfMstr.ServiceType != "" && InsConfMstr.InstructionDtl != ""
                    && InsConfMstr.BoatHouseId != "" && InsConfMstr.BoatHouseName != ""
                    && InsConfMstr.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("SP_PrintInstruction", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsConfMstr.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@ServiceType", InsConfMstr.ServiceType.ToString());
                    cmd.Parameters.AddWithValue("@InstructionDtl", InsConfMstr.InstructionDtl.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsConfMstr.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsConfMstr.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@Createdby", InsConfMstr.Createdby.Trim());

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
                        PrintingInstructionres ConMstr = new PrintingInstructionres
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        PrintingInstructionres ConMstr = new PrintingInstructionres
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    PrintingInstructionres Vehicle = new PrintingInstructionres
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                PrintingInstructionres ConfRes = new PrintingInstructionres
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

        //Printing Instruction Grid
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PrintingInstructionGrid")]
        public IHttpActionResult PrintingInstructionGrid([FromBody] PrintingInstruction Trip)
        {
            try
            {
                List<PrintingInstruction> li = new List<PrintingInstruction>();
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT UniqueId, ServiceType, "
                    + " CASE WHEN ServiceType = '1' THEN 'Boating' WHEN ServiceType = '2' THEN 'Other Services' "
                    + " WHEN ServiceType = '3' THEN 'Restaurant' END AS 'ServiceName', InstructionDtl, BoatHouseName, ActiveStatus FROM "
                    + " PrintingInstruction WHERE BoatHouseId = @BoatHouseId ", con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PrintingInstruction ShowBoatid = new PrintingInstruction();
                        ShowBoatid.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        ShowBoatid.ServiceType = dt.Rows[i]["ServiceType"].ToString();
                        ShowBoatid.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        ShowBoatid.InstructionDtl = dt.Rows[i]["InstructionDtl"].ToString();
                        ShowBoatid.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ShowBoatid.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowBoatid);
                    }

                    PrintingInstructionList ConfList = new PrintingInstructionList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    PrintingInstructionres ConfRes = new PrintingInstructionres
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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

        //View Reschedule Details  
        /// <summary>
        /// Modified By : Jsuriya
        /// Modified Date : 27-09-2021
        /// Modified By : Jayasuriya
        /// Modified Date : 13-10-2021,Added stgBookingPin Parameter
        /// Modified By : Jayasuriya
        /// Modified Date : 18-10-2021, Added RescheduleOldDate And RescheduleNewDate Parameter
        /// </summary>
        /// <param name="BooDet"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BookingReschedule/RescheduleDetails")]
        public IHttpActionResult RescheduleDetails([FromBody] BookingReScheduling BooDet)
        {
            try
            {
                if (BooDet.QueryType != "" && BooDet.BookingId != "" && BooDet.BoatHouseId != "" && BooDet.UserId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ViewRescheduleDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", BooDet.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", BooDet.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", BooDet.BoatHouseId.ToString());
                    if (BooDet.FromDate != "" && BooDet.ToDate != "")
                    {
                        cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(BooDet.FromDate, objEnglishDate));
                        cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(BooDet.ToDate, objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FromDate", "");
                        cmd.Parameters.AddWithValue("@ToDate", "");
                    }
                    cmd.Parameters.AddWithValue("@UserId", BooDet.UserId.ToString());
                    cmd.Parameters.AddWithValue("@MobileNo", BooDet.MobileNo.ToString());
                    cmd.Parameters.AddWithValue("@RescheduleType", BooDet.RescheduleType.ToString());
                    cmd.Parameters.AddWithValue("@BookingPin", BooDet.stgBookingPin.ToString());

                    if (BooDet.RescheduleOldDate != "" && BooDet.RescheduleNewDate != "")
                    {
                        cmd.Parameters.AddWithValue("@RescheduleOldDate", DateTime.Parse(BooDet.RescheduleOldDate.ToString().Trim(), objEnglishDate));
                        cmd.Parameters.AddWithValue("@RescheduleNewDate", DateTime.Parse(BooDet.RescheduleNewDate.ToString().Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RescheduleOldDate", "");
                        cmd.Parameters.AddWithValue("@RescheduleNewDate", "");
                    }
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Table");
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
                    PriceComparisonRes InsCE = new PriceComparisonRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return Ok(ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString());
            }
        }

        /// <summary>
        /// Created By : Jayasuriya
        /// Created Date : 13-09-2021
        /// Modified By :  Jayasuriya
        /// Modified Date :  13-10-2021
        /// Modified By :  Jayasuriya
        /// Modified Date :  22-10-2021, Added OthersRescheduleDate
        ///Added RescheduleReason Parameter
        /// </summary>  
        /// <param name="InsCityMap"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BookingReschedule")]
        public async Task<IHttpActionResult> BookingRescheduleSlot([FromBody] BookingReScheduling InsCityMap)
        {
            var i = 0;
            if (InsCityMap.SlotId == null)
                return NotFound();

            var result = new List<string>();
            foreach (var id in InsCityMap.SlotId)
            {
                result.Add(await BookingRescheduleSlotid(id, InsCityMap.QueryType, InsCityMap.BookingId, InsCityMap.BoatHouseId,
                    InsCityMap.BoatHouseName, InsCityMap.BookingDate[i], InsCityMap.BookingMedia, InsCityMap.CreatedBy,
                    InsCityMap.RescheduledTotalcharge, InsCityMap.CGST, InsCityMap.SGST, InsCityMap.RescheduledCharge,
                    InsCityMap.PaymentType, InsCityMap.Hour, InsCityMap.Minute, InsCityMap.ActivityId,
                    InsCityMap.BookingPin[i], InsCityMap.ChargeType, InsCityMap.ChargeAmount, InsCityMap.RescheduleReason,
                    InsCityMap.OthersRescheduleDate));
                i++;
            }
            BookingReSchedulingResponse BookingSlot = new BookingReSchedulingResponse
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }


        public async Task<dynamic> BookingRescheduleSlotid(int SlotId, string QueryType, string BookingId, string BoatHouseId, string BoatHouseName,
          string BookingDate, string BookingMedia, string CreatedBy, string RescheduledTotalcharge, string CGST,
          string SGST, string RescheduledCharge, string PaymentType, string Hour, string Minute, string ActivityId,
          string BookingPin, string ChargeType, string ChargeAmount, string RescheduleReason,
          string OthersRescheduleDate)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BookingReschedule", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;

                cmd.Parameters.AddWithValue("@QueryType", QueryType.ToString());
                cmd.Parameters.AddWithValue("@BookingId", BookingId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.ToString());
                cmd.Parameters.AddWithValue("@BookingNewDate", DateTime.Parse(BookingDate.ToString(), objEnglishDate));
                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy.Trim());
                cmd.Parameters.AddWithValue("@RescheduledTotalcharge", RescheduledTotalcharge.Trim());
                cmd.Parameters.AddWithValue("@CGST", CGST.Trim());
                cmd.Parameters.AddWithValue("@SGST", SGST.Trim());
                cmd.Parameters.AddWithValue("@RescheduledCharge", RescheduledCharge.Trim());
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType.Trim());
                cmd.Parameters.AddWithValue("@Hour", Hour.Trim());
                cmd.Parameters.AddWithValue("@Minute", Minute.Trim());
                cmd.Parameters.AddWithValue("@ActivityId", ActivityId.Trim());
                cmd.Parameters.AddWithValue("@BookingPin", BookingPin.Trim());
                cmd.Parameters.AddWithValue("@SlotId", SlotId);
                cmd.Parameters.AddWithValue("@ChargeType", ChargeType.Trim());
                cmd.Parameters.AddWithValue("@Charges", ChargeAmount.Trim());
                cmd.Parameters.AddWithValue("@RescheduleReason", RescheduleReason.Trim());
                if (OthersRescheduleDate == "")
                {
                    cmd.Parameters.AddWithValue("@OthersRescheduleDate", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@OthersRescheduleDate", DateTime.Parse(OthersRescheduleDate.ToString().Trim(), objEnglishDate));
                }
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CancellationNoteDetails")]
        public IHttpActionResult CancellationNoteDetails([FromBody] CancelReschedMstr Cancel)
        {
            try
            {
                if (Cancel.BoatHouseId != "")
                {
                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select ApplicableBefore ,ChargeType,Charges  from CancelReschedMstr "
                        + " Where ActivityType='C' AND ActiveStatus='A' AND BoatHouseId=@BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            CancelReschedMstr CancelBooking = new CancelReschedMstr();

                            CancelBooking.Charges = dt.Rows[i]["Charges"].ToString();
                            CancelBooking.ApplicableBefore = dt.Rows[i]["ApplicableBefore"].ToString();
                            CancelBooking.ChargeType = dt.Rows[i]["ChargeType"].ToString();

                            li.Add(CancelBooking);
                        }
                        CancelReschedMstrList ConfList = new CancelReschedMstrList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        CancelReschedMstrRes ConfRes1 = new CancelReschedMstrRes
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

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

        //Cancellation Details
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CancellationDetails")]
        public IHttpActionResult CancellationDetails([FromBody] CancelReschedMstr Cancel)
        {
            try
            {
                if (Cancel.BoatHouseId != "")
                {
                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT A.BookingId,A.BookingPin,A.BoatReferenceNo,B.CustomerMobile,ISNULL(A.InitNetAmount,0) 'InitNetAmount', "
                    + " ISNULL(A.InitBoatCharge, 0) + ISNULL(A.InitRowerCharge, 0) + (ISNULL(SUM(A.CGSTTaxAmount), 0)+ISNULL(SUM(A.SGSTTaxAmount), 0)) AS 'BoatCharge', "
                    + " ISNULL(A.BoatDeposit, 0) 'DepositAmount', ISNULL(A.CancelCharges, 0) 'CancelCharges', ISNULL(A.CancelRefund, 0) 'CancelRefund', C.ConfigName 'PaymentType', "
                    + " convert(varchar(10), B.BookingDate, 101) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate', "
                    + " convert(varchar(10), B.UpdatedDate, 101) + right(convert(varchar(32), B.UpdatedDate, 100), 8) AS 'CancellationDate' FROM BookingDtl AS A "
                    + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN ConfigurationMaster AS C ON A.RePaymentType = C.ConfigID AND C.TypeID = 20 "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND A.Status = 'C' AND A.TripStartTime IS NULL AND CAST(B.BookingDate AS DATE) = CAST(GETDATE() AS DATE) "
                    + " GROUP BY A.BookingId, A.BookingPin, A.BoatReferenceNo, B.CustomerMobile, A.InitNetAmount, "
                    + " A.InitBoatCharge, A.InitRowerCharge, A.BoatDeposit, A.CancelCharges, A.CancelRefund, C.ConfigName, "
                    + " B.BookingDate, B.UpdatedDate"
                    + " ORDER BY B.UpdatedDate DESC", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            CancelReschedMstr CancelBooking = new CancelReschedMstr();

                            CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
                            CancelBooking.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            CancelBooking.BoatCharge = dt.Rows[i]["BoatCharge"].ToString();
                            CancelBooking.DepositAmount = dt.Rows[i]["DepositAmount"].ToString();
                            CancelBooking.DeductedCharges = dt.Rows[i]["CancelCharges"].ToString();
                            CancelBooking.Refund = dt.Rows[i]["CancelRefund"].ToString();
                            CancelBooking.MobileNo = dt.Rows[i]["CustomerMobile"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.CancellationDate = dt.Rows[i]["CancellationDate"].ToString();

                            li.Add(CancelBooking);
                        }
                        CancelReschedMstrList ConfList = new CancelReschedMstrList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        CancelReschedMstrRes ConfRes1 = new CancelReschedMstrRes
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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

        //Cancellation Boooking Details
        /// <summary>
        /// Modified By : Imran
        /// Modified By : 13-10-2021
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>       
        [HttpPost]
        [AllowAnonymous]
        [Route("BookingDetailsBasedOnBookingId")]
        public IHttpActionResult BookingDetailsBasedOnBookingId([FromBody] BoatBooking Cancel)
        {
            try
            {
                if (Cancel.BoatHouseId != "" && Cancel.BookingId != "")
                {
                    List<BoatBooking> li = new List<BoatBooking>();
                    con.Open();

                    //SqlCommand cmd = new SqlCommand("SELECT A.BookingId,A.BookingPin,A.BoatReferenceNo,A.initNetAmount,A.BookingDuration, "
                    //    + " A.BoatDeposit,A.initBoatCharge,A.InitRowerCharge, B.CustomerName, B.CustomerMobile, "
                    //    + " convert(varchar(10), B.BookingDate, 103) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate',B.PaymentType, "
                    //    + " CASE WHEN B.PremiumStatus='P' THEN 'Premium' ELSE 'Normal' END AS PremiumStatus,"
                    //    + " SUM(InitBoatCharge+InitRowerCharge+CGSTTaxAmount+SGSTTaxAmount) AS 'BoatCharges'"
                    //    + " FROM BookingDtl AS A INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId  AND A.BoatHouseId=B.BoatHouseId "
                    //    + " WHERE A.BookingId ='" + Cancel.BookingId.ToString() + "' AND A.Status IN('B','R') "
                    //    + " AND A.BoatHouseId='" + Cancel.BoatHouseId.ToString() + "' AND A.TripStartTime IS NULL"
                    //    + " GROUP BY "
                    //    + " A.BookingId, A.BookingPin, A.BoatReferenceNo, A.initNetAmount, A.BookingDuration,"
                    //    + " A.BoatDeposit, A.initBoatCharge, A.InitRowerCharge, B.CustomerName, B.CustomerMobile,B.BookingDate, B.PaymentType,B.PremiumStatus", con);

                    SqlCommand cmd = new SqlCommand("SELECT A.BookingId,A.BookingPin,A.BoatReferenceNo,A.initNetAmount,A.BookingDuration, "
                                + "   A.BoatDeposit, A.initBoatCharge, A.InitRowerCharge, B.CustomerName, B.CustomerMobile,  "
                                + "   convert(varchar(10), B.BookingDate, 103) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate', B.PaymentType,  "
                                + "   CASE WHEN B.PremiumStatus = 'P' THEN 'Premium' ELSE 'Normal' END AS PremiumStatus,  "
                                + "   SUM(InitBoatCharge + InitRowerCharge + CGSTTaxAmount + SGSTTaxAmount) AS 'BoatCharges', "
                                + "   BT.BoatType, BS.SeaterType, BHM.BoatHouseName, STUFF(RIGHT(' ' + CONVERT(VARCHAR(30), BSM.SlotStartTime, 100), 7), 6, 0, ' ') AS 'TimeSlot'  "
                                + "   FROM BookingDtl AS A  "
                                + "   INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId  AND A.BoatHouseId = B.BoatHouseId  "
                                + "   INNER JOIN BoatTypes AS BT ON BT.BoatTypeId = A.BoatTypeId AND BT.BoatHouseId = A.BoatHouseId  "
                                + "   INNER JOIN BoatSeat AS BS ON BS.BoatSeaterId = A.BoatSeaterId AND BS.BoatHouseId = A.BoatHouseId  "
                                + "   INNER JOIN BHMaster AS BHM ON B.BoatHouseId = BHM.BoatHouseId  "
                                + "   INNER JOIN BoatSlotMaster AS BSM ON A.BoatHouseId = BSM.BoatHouseId AND A.TimeSlotId = BSM.SlotId AND BSM.ActiveStatus = 'A'  "
                                + "   WHERE A.BookingId =@BookingId  AND A.Status IN('B', 'R')  "
                                + "   AND A.BoatHouseId =@BoatHouseId AND A.TripStartTime IS NULL  "
                                + "   GROUP BY A.BookingId, A.BookingPin, A.BoatReferenceNo, A.initNetAmount, A.BookingDuration,  "
                                + "   A.BoatDeposit, A.initBoatCharge, A.InitRowerCharge, B.CustomerName, B.CustomerMobile, B.BookingDate,  "
                                + "   B.PaymentType, B.PremiumStatus, BT.BoatType, BS.SeaterType, BHM.BoatHouseName, BSM.SlotStartTime", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.ToString().Trim();
                    cmd.Parameters["@BookingId"].Value = Cancel.BookingId.ToString().Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatBooking CancelBooking = new BoatBooking();
                            CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
                            CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            CancelBooking.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.CustomerMobileNo = dt.Rows[i]["CustomerMobile"].ToString();
                            CancelBooking.InitNetAmount = dt.Rows[i]["initNetAmount"].ToString();
                            CancelBooking.InitBoatCharge = dt.Rows[i]["initBoatCharge"].ToString();
                            CancelBooking.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            CancelBooking.BoatCharges = dt.Rows[i]["BoatCharges"].ToString();
                            CancelBooking.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            CancelBooking.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.TimeSlot = dt.Rows[i]["TimeSlot"].ToString();

                            li.Add(CancelBooking);
                        }
                        BoatBookingList ConfList = new BoatBookingList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        CancelReschedMstrRes ConfRes1 = new CancelReschedMstrRes
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                    {
                        Response = "Must Pass Boat House Id And Booking Id",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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
        /// Modified Date : 22-09-2021
        /// </summary>
        /// <param name="Booking"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PublicAndDepartmentBookingCancellationDetails")]
        public IHttpActionResult PublicBookingCancellation([FromBody] CancelReschedMstr Booking)
        {
            try
            {
                List<CancelReschedMstr> li = new List<CancelReschedMstr>();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                if (Booking.BookingId != "" && Booking.BoatHouseId != ""
                    && Booking.Cgst != "" && Booking.Sgst != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrPublicCancelBooking", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "BookingIdBasedCharges");
                    cmd.Parameters.AddWithValue("@ActivityType", 'C');
                    cmd.Parameters.AddWithValue("@BookingId", Booking.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Booking.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@CGST", Booking.Cgst.ToString());
                    cmd.Parameters.AddWithValue("@SGST", Booking.Sgst.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", Booking.BoatReferenceNo.ToString());

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
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                CancelReschedMstr CancelBooking = new CancelReschedMstr();
                                CancelBooking.ChargeType = dt.Rows[i]["ChargeType"].ToString();
                                CancelBooking.Charges = dt.Rows[i]["Charges"].ToString();
                                CancelBooking.TotalAmount = dt.Rows[i]["TotalAmount"].ToString();
                                CancelBooking.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                                CancelBooking.DepositAmount = dt.Rows[i]["DepositAmount"].ToString();
                                CancelBooking.ChargesAmount = dt.Rows[i]["ChargesAmount"].ToString();
                                CancelBooking.Refund = dt.Rows[i]["RefundAmount"].ToString();
                                CancelBooking.Cgst = dt.Rows[i]["CancelCgst"].ToString();
                                CancelBooking.Sgst = dt.Rows[i]["CancelSgst"].ToString();
                                CancelBooking.CancelBoatCharges = dt.Rows[i]["CancelBoatCharge"].ToString();


                                li.Add(CancelBooking);
                            }
                        }


                        CancelReschedMstrList ConMstr = new CancelReschedMstrList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else if (sResult[0].Trim() == "Success1")
                    {
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                CancelReschedMstr CancelBooking = new CancelReschedMstr();
                                CancelBooking.TotalAmount = dt.Rows[i]["TotalAmount"].ToString();
                                CancelBooking.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                                CancelBooking.DepositAmount = dt.Rows[i]["DepositAmount"].ToString();
                                CancelBooking.ChargesAmount = dt.Rows[i]["ChargesAmount"].ToString();
                                CancelBooking.Refund = dt.Rows[i]["RefundAmount"].ToString();

                                li.Add(CancelBooking);
                            }
                        }

                        CancelReschedMstrList ConMstr = new CancelReschedMstrList
                        {
                            Response = li,
                            StatusCode = 2
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        CancelReschedMstrRes ConMstr = new CancelReschedMstrRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    CancelReschedMstrRes Vehicle = new CancelReschedMstrRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BulkCancellationDetails")]
        public IHttpActionResult BulkCancellationDetails([FromBody] BoatBooking Book)
        {
            try
            {
                if (Book.BoatHouseId != "" && Book.PaymentTypeId != "")
                {
                    string sCondition = string.Empty;

                    if (Book.PaymentTypeId == "0")
                    {
                        sCondition = string.Empty;
                    }
                    else
                    {
                        sCondition = " AND B.PaymentType=@PaymentTypeId";
                    }

                    List<BoatBooking> li = new List<BoatBooking>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT A.BoatReferenceNo,A.BookingPin, A.BookingId, "
                      + " A.BoatHouseName, A.BookingDuration, A.BoatDeposit, A.initNetAmount,B.PaymentType,C.ConfigName 'PaymentTypeName', "
                      + " A.initBoatCharge, B.InitBillAmount, A.initRowerCharge,  "
                      + " convert(varchar(10), B.BookingDate, 103) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate', "
                      + " B.CustomerName FROM BookingDtl AS A INNER JOIN BookingHdr AS B ON A.BoatHouseId = B.BoatHouseId AND A.BookingId = B.BookingId "
                      + " INNER JOIN ConfigurationMaster AS C ON B.PaymentType=C.ConfigID AND C.TypeID=20 "
                      + " WHERE A.TripStartTime IS NULL AND CAST(B.BookingDate AS DATE) BETWEEN CAST( @FromDate AS DATE) "
                      + " AND CAST( @ToDate AS DATE)  "
                      + " AND A.Status IN('B', 'R') AND A.BoatHouseId = @BoatHouseId " + sCondition.Trim() + " Order by A.BookingId ASC ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = Book.BoatHouseId.ToString().Trim();
                    cmd.Parameters["@PaymentTypeId"].Value = Book.PaymentTypeId.ToString().Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Book.Fromdate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Book.Todate.Trim(), objEnglishDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            BoatBooking CancelBooking = new BoatBooking();
                            CancelBooking.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
                            CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.InitBillAmount = dt.Rows[i]["InitBillAmount"].ToString();
                            CancelBooking.InitBoatCharge = dt.Rows[i]["InitBoatCharge"].ToString();
                            CancelBooking.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            CancelBooking.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
                            CancelBooking.PaymentTypeId = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentTypeName"].ToString();

                            li.Add(CancelBooking);
                        }
                        BoatBookingList ConfList = new BoatBookingList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        BoatBookingStr ConfRes1 = new BoatBookingStr
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    BoatBookingStr ConfRes = new BoatBookingStr
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0

                    };
                    return Ok(ConfRes);

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


        //Cancellation Update
        /// <summary>
        /// Modifiesd By : Imran
        /// Modified Date : 22-09-2021
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CancellationUpdate")]
        public IHttpActionResult CancellationUpdate([FromBody] CancelReschedMstr Cancel)
        {
            try
            {
                if (Cancel.QueryType != "" && Cancel.ActivityType != "" && Cancel.BoatHouseId != "" && Cancel.BookingId != ""
                    && Cancel.Charges != "" && Cancel.ChargeType != "" && Cancel.Comments != "" && Cancel.CancelledBy != "" && Cancel.CancelledMedia != ""
                    && Cancel.CreatedBy != "" && Cancel.Cgst != "" && Cancel.Sgst != "" && Cancel.CancellationHours != "")
                {
                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();


                    SqlCommand cmd1 = new SqlCommand("EXECUTE [dbo].[MstrCancelBooking]   @QueryType, @BookingId, "
                       + " @BoatHouseId, @ActivityType, @Charges, @ChargeType, @Comments,"
                       + " @CancelledBy, @CancelledMedia, @CreatedBy, @Cgst, "
                       + " @Sgst, @PaymentType,@BookingIdList, @BoatReferenceNoList ,@CancellationHours , "
                       + " @CancellationType,@CancelOtherService,@OtherCancelledBy ", con);

                    cmd1.Parameters.Add(new SqlParameter("@QueryType", System.Data.SqlDbType.NVarChar, 25));
                    cmd1.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd1.Parameters.Add(new SqlParameter("@ActivityType", System.Data.SqlDbType.Char, 1));
                    cmd1.Parameters.Add(new SqlParameter("@Charges", System.Data.SqlDbType.Decimal));
                    cmd1.Parameters.Add(new SqlParameter("@ChargeType", System.Data.SqlDbType.NVarChar, 20));
                    cmd1.Parameters.Add(new SqlParameter("@Comments", System.Data.SqlDbType.NVarChar, 250));
                    cmd1.Parameters.Add(new SqlParameter("@CancelledBy", System.Data.SqlDbType.NVarChar, 30));
                    cmd1.Parameters.Add(new SqlParameter("@CancelledMedia", System.Data.SqlDbType.NVarChar, 30));
                    cmd1.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.NVarChar, 20));
                    cmd1.Parameters.Add(new SqlParameter("@Cgst", System.Data.SqlDbType.Decimal));
                    cmd1.Parameters.Add(new SqlParameter("@Sgst", System.Data.SqlDbType.Decimal));
                    cmd1.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.NVarChar, 10));
                    cmd1.Parameters.Add(new SqlParameter("@BookingIdList", System.Data.SqlDbType.NVarChar));
                    cmd1.Parameters.Add(new SqlParameter("@BoatReferenceNoList", System.Data.SqlDbType.NVarChar));
                    cmd1.Parameters.Add(new SqlParameter("@CancellationHours", System.Data.SqlDbType.NVarChar, 50));
                    cmd1.Parameters.Add(new SqlParameter("@CancellationType", System.Data.SqlDbType.NVarChar, 50));
                    cmd1.Parameters.Add(new SqlParameter("@CancelOtherService", System.Data.SqlDbType.VarChar, 10));
                    cmd1.Parameters.Add(new SqlParameter("@OtherCancelledBy", System.Data.SqlDbType.VarChar));


                    cmd1.Parameters["@QueryType"].Value = Cancel.QueryType.ToString().Trim();
                    cmd1.Parameters["@BookingId"].Value = Cancel.BookingId.ToString().Trim();
                    cmd1.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.ToString().Trim();
                    cmd1.Parameters["@ActivityType"].Value = Cancel.ActivityType.ToString().Trim();
                    cmd1.Parameters["@Charges"].Value = Cancel.Charges.ToString().Trim();
                    cmd1.Parameters["@ChargeType"].Value = Cancel.ChargeType.ToString().Trim();
                    cmd1.Parameters["@Comments"].Value = Cancel.Comments.ToString().Trim();
                    cmd1.Parameters["@CancelledBy"].Value = Cancel.CancelledBy.ToString().Trim();
                    cmd1.Parameters["@CancelledMedia"].Value = Cancel.CancelledMedia.ToString().Trim();
                    cmd1.Parameters["@CreatedBy"].Value = Cancel.CreatedBy.ToString().Trim();
                    cmd1.Parameters["@Cgst"].Value = Cancel.Cgst.ToString().Trim();
                    cmd1.Parameters["@Sgst"].Value = Cancel.Sgst.ToString().Trim();
                    cmd1.Parameters["@PaymentType"].Value = Cancel.PaymentType.ToString().Trim();
                    cmd1.Parameters["@BookingIdList"].Value = Cancel.BookingIdList.ToString().Trim();
                    cmd1.Parameters["@BoatReferenceNoList"].Value = Cancel.BoatReferenceNoList.ToString().Trim();
                    cmd1.Parameters["@CancellationHours"].Value = Cancel.CancellationHours.ToString().Trim();
                    cmd1.Parameters["@CancellationType"].Value = Cancel.CancellationType.ToString().Trim();
                    cmd1.Parameters["@CancelOtherService"].Value = Cancel.CancelOtherService.ToString().Trim();
                    cmd1.Parameters["@OtherCancelledBy"].Value = Cancel.OtherCancelledBy.ToString().Trim();


                    con.Open();
                    int i = cmd1.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                        {
                            Response = "Success",
                            StatusCode = 1
                        };
                        return Ok(ConfRes);
                    }
                    else if (i == -1)
                    {
                        CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                        {
                            Response = "Already This Trip is Started Cannot Cancel This Trip",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                    else
                    {
                        CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                        {
                            Response = "Failure",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BulkCancellationUpdate")]
        public IHttpActionResult BulkCancellationUpdate([FromBody] CancelReschedMstr Cancel)
        {
            try
            {

                if (Cancel.QueryType != "" && Cancel.ActivityType != "" && Cancel.BoatHouseId != "" && Cancel.CancelledBy != "" && Cancel.CancelledMedia != "")
                {
                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();

                    SqlCommand cmd1 = new SqlCommand("EXECUTE [dbo].[MstrCancelBooking]   @QueryType, @BookingId, "
                       + " @BoatHouseId, @ActivityType, @Charges, @ChargeType, @Comments,"
                       + " @CancelledBy, @CancelledMedia, @CreatedBy, @Cgst, "
                       + " @Sgst, @PaymentType,@BookingIdList, @BoatReferenceNoList ,@CancellationHours , "
                       + " @CancellationType,@CancelOtherService,@OtherCancelledBy ", con);

                    cmd1.Parameters.Add(new SqlParameter("@QueryType", System.Data.SqlDbType.NVarChar, 25));
                    cmd1.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd1.Parameters.Add(new SqlParameter("@ActivityType", System.Data.SqlDbType.Char, 1));
                    cmd1.Parameters.Add(new SqlParameter("@Charges", System.Data.SqlDbType.Decimal));
                    cmd1.Parameters.Add(new SqlParameter("@ChargeType", System.Data.SqlDbType.NVarChar, 20));
                    cmd1.Parameters.Add(new SqlParameter("@Comments", System.Data.SqlDbType.NVarChar, 250));
                    cmd1.Parameters.Add(new SqlParameter("@CancelledBy", System.Data.SqlDbType.NVarChar, 30));
                    cmd1.Parameters.Add(new SqlParameter("@CancelledMedia", System.Data.SqlDbType.NVarChar, 30));
                    cmd1.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.NVarChar, 20));
                    cmd1.Parameters.Add(new SqlParameter("@Cgst", System.Data.SqlDbType.Decimal));
                    cmd1.Parameters.Add(new SqlParameter("@Sgst", System.Data.SqlDbType.Decimal));
                    cmd1.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.NVarChar, 10));
                    cmd1.Parameters.Add(new SqlParameter("@BookingIdList", System.Data.SqlDbType.NVarChar));
                    cmd1.Parameters.Add(new SqlParameter("@BoatReferenceNoList", System.Data.SqlDbType.NVarChar));
                    cmd1.Parameters.Add(new SqlParameter("@CancellationHours", System.Data.SqlDbType.NVarChar, 50));
                    cmd1.Parameters.Add(new SqlParameter("@CancellationType", System.Data.SqlDbType.NVarChar, 50));
                    cmd1.Parameters.Add(new SqlParameter("@CancelOtherService", System.Data.SqlDbType.VarChar, 10));
                    cmd1.Parameters.Add(new SqlParameter("@OtherCancelledBy", System.Data.SqlDbType.VarChar));


                    cmd1.Parameters["@QueryType"].Value = Cancel.QueryType.ToString().Trim();
                    cmd1.Parameters["@BookingId"].Value = Cancel.BookingId.ToString().Trim();
                    cmd1.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.ToString().Trim();
                    cmd1.Parameters["@ActivityType"].Value = Cancel.ActivityType.ToString().Trim();
                    cmd1.Parameters["@Charges"].Value = Cancel.Charges.ToString().Trim();
                    cmd1.Parameters["@ChargeType"].Value = Cancel.ChargeType.ToString().Trim();
                    cmd1.Parameters["@Comments"].Value = Cancel.Comments.ToString().Trim();
                    cmd1.Parameters["@CancelledBy"].Value = Cancel.CancelledBy.ToString().Trim();
                    cmd1.Parameters["@CancelledMedia"].Value = Cancel.CancelledMedia.ToString().Trim();
                    cmd1.Parameters["@CreatedBy"].Value = Cancel.CreatedBy.ToString().Trim();
                    cmd1.Parameters["@Cgst"].Value = Cancel.Cgst.ToString().Trim();
                    cmd1.Parameters["@Sgst"].Value = Cancel.Sgst.ToString().Trim();
                    cmd1.Parameters["@PaymentType"].Value = Cancel.PaymentType.ToString().Trim();
                    cmd1.Parameters["@BookingIdList"].Value = Cancel.BookingIdList.ToString().Trim();
                    cmd1.Parameters["@BoatReferenceNoList"].Value = Cancel.BoatReferenceNoList.ToString().Trim();
                    cmd1.Parameters["@CancellationHours"].Value = Cancel.CancellationHours.ToString().Trim();
                    cmd1.Parameters["@CancellationType"].Value = Cancel.CancellationType.ToString().Trim();
                    cmd1.Parameters["@CancelOtherService"].Value = Cancel.CancelOtherService.ToString().Trim();
                    cmd1.Parameters["@OtherCancelledBy"].Value = Cancel.OtherCancelledBy.ToString().Trim();
                    con.Open();
                    int i = cmd1.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                        {
                            Response = "Success",
                            StatusCode = 1
                        };
                        return Ok(ConfRes);
                    }
                    else if (i == -1)
                    {
                        CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                        {
                            Response = "Already This Trip is Started Cannot Cancel This Trip",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                    else
                    {
                        CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                        {
                            Response = "Failure",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

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


        //ReEntry Trip Details
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="RETrip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("REEntrpTripdtls/BookingId/BookingPin")]
        public IHttpActionResult REEntrpTripdtlsBookingIdBookingPIN([FromBody] RETripEntryDtls RETrip)
        {
            try
            {
                if (RETrip.BoatHouseId != "" && RETrip.BookingId != "" && RETrip.BookingPin != "")
                {
                    List<RETripEntryDtls> li = new List<RETripEntryDtls>();
                    con.Open();
                    string sQuery = " Select A.BookingId, A.BookingPin, A.BoatTypeId, B.BoatType, A.BoatSeaterId, A.BoatHouseId, C.SeaterType, "
                                    + " A.BoatHouseName,A.BoatReferenceNo, Convert(Nvarchar, E.BookingDate, 103) as 'BookedDate', "
                                    + " A.ActualBoatNum,A.ActualBoatId, convert(nvarchar, CAST(A.TripStartTime as time), 108) as 'TripStartTime' , "
                                    + " convert(nvarchar, CAST(A.TripEndTime as time), 108) as 'TripEndTime' ,A.RowerId,D.RowerName from BookingDtl AS A "
                                    + " left Join BoatTypes as B On A.BoatTypeId = B.BoatTypeId  and B.ActiveStatus = 'A' "
                                    + " left Join RowerMaster as D on A.RowerId = D.RowerId and D.ActiveStatus = 'A' "
                                    + " left Join BoatSeat as C on A.BoatSeaterId = C.BoatSeaterId and C.ActiveStatus = 'A' "
                                    + " inner Join BookingHdr as E on E.BookingId = A.BookingId where A.BoatHouseId = @BoatHouseId and "
                                    + " A.BookingId = @BookingId and A.BookingPin = @BookingPin and "
                                    + " A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL ";
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 10));


                    cmd.Parameters["@BoatHouseId"].Value = RETrip.BoatHouseId.Trim();
                    cmd.Parameters["@BookingId"].Value = RETrip.BookingId.Trim();
                    cmd.Parameters["@BookingPin"].Value = RETrip.BookingPin.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RETripEntryDtls ShowREEntry = new RETripEntryDtls();
                            ShowREEntry.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowREEntry.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowREEntry.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowREEntry.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowREEntry.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowREEntry.BoatSeat = dt.Rows[i]["SeaterType"].ToString();
                            ShowREEntry.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            ShowREEntry.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            ShowREEntry.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            ShowREEntry.BookedDate = dt.Rows[i]["BookedDate"].ToString();
                            ShowREEntry.BoatNumber = dt.Rows[i]["ActualBoatNum"].ToString();
                            ShowREEntry.BoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            ShowREEntry.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowREEntry.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowREEntry.Rower = dt.Rows[i]["RowerName"].ToString();
                            ShowREEntry.RowerId = dt.Rows[i]["RowerId"].ToString();


                            li.Add(ShowREEntry);
                        }

                        RETripEntryDtlsStringResList REEntry = new RETripEntryDtlsStringResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(REEntry);
                    }
                    else
                    {
                        RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(REEntryRes);
                    }
                }
                else
                {
                    RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(REEntryRes);
                }
            }
            catch (Exception ex)
            {
                RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(REEntryRes);
            }
        }

        //ReEntry Trip Details based on Booking Id
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="RETrip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("REEntrpTripdtls/BookingId")]
        public IHttpActionResult REEntrpTripdtlsBookingId([FromBody] RETripEntryDtls RETrip)
        {

            try
            {

                if (RETrip.BoatHouseId != "" && RETrip.BookingId != "")
                {
                    List<RETripEntryDtls> li = new List<RETripEntryDtls>();
                    con.Open();
                    string sQuery = "Select  A.BookingId,A.BookingPin,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType, A.BoatReferenceNo, "
                                    + " Convert(Nvarchar, D.BookingDate, 103) as 'BookedDate'  from BookingDtl AS A "
                                    + " left Join BoatTypes as B On A.BoatTypeId = B.BoatTypeId  and B.ActiveStatus = 'A' "
                                    + " left Join BoatSeat as C on A.BoatSeaterId = C.BoatSeaterId and C.ActiveStatus = 'A' "
                                    + " inner Join BookingHdr as D on D.BookingId = A.BookingId "
                                    + " where A.BoatHouseId = @BoatHouseId and A.BookingId = @BookingId "
                                    + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND D.Status IN ('B','R','P') AND A.Status IN ('B','R') ";
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = RETrip.BoatHouseId.Trim();
                    cmd.Parameters["@BookingId"].Value = RETrip.BookingId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RETripEntryDtls ShowREEntry = new RETripEntryDtls();
                            ShowREEntry.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowREEntry.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowREEntry.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowREEntry.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowREEntry.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowREEntry.BoatSeat = dt.Rows[i]["SeaterType"].ToString();
                            ShowREEntry.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            ShowREEntry.BookedDate = dt.Rows[i]["BookedDate"].ToString();

                            li.Add(ShowREEntry);
                        }

                        RETripEntryDtlsStringResList REEntry = new RETripEntryDtlsStringResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(REEntry);
                    }

                    else
                    {
                        RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(REEntryRes);
                    }
                }
                else
                {
                    RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(REEntryRes);
                }
            }
            catch (Exception ex)
            {
                RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(REEntryRes);
            }
        }

        //ReEntry Trip Details Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("REEntrpTripdtls/Insert")]
        public IHttpActionResult ReentryInsert([FromBody] RETripEntryDtls RETrip)
        {
            try
            {
                if (RETrip.Querytype != "" && RETrip.BookingId != "" && RETrip.BookingPin != "" && RETrip.BoatTypeId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("REEntryTripDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", RETrip.Querytype.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", RETrip.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BookingPin", RETrip.BookingPin.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", RETrip.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatType", RETrip.BoatType.Trim());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", RETrip.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeat", RETrip.BoatSeat.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", RETrip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", RETrip.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@BoatId", RETrip.BoatId.ToString());
                    cmd.Parameters.AddWithValue("@BoatNumber", RETrip.BoatNumber.Trim());
                    cmd.Parameters.AddWithValue("@Reason", RETrip.Reason.Trim());
                    if (RETrip.TripStartTime != "")
                    {
                        cmd.Parameters.AddWithValue("@TripStartTime", DateTime.Parse(RETrip.TripStartTime.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TripStartTime", "");
                    }

                    if (RETrip.TripEndTime != "")
                    {
                        cmd.Parameters.AddWithValue("@TripEndTime", DateTime.Parse(RETrip.TripEndTime.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@TripEndTime", "");
                    }
                    if (RETrip.ReTripStartTime != "")
                    {
                        cmd.Parameters.AddWithValue("@ReTripStartTime", DateTime.Parse(RETrip.ReTripStartTime.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ReTripStartTime", "");
                    }

                    if (RETrip.ReTripEndTime != "")
                    {
                        cmd.Parameters.AddWithValue("@ReTripEndTime", DateTime.Parse(RETrip.ReTripEndTime.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ReTripEndTime", "");
                    }

                    if (RETrip.BookedDate != "")
                    {
                        cmd.Parameters.AddWithValue("@BookedDate", DateTime.Parse(RETrip.BookedDate.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BookedDate", "");

                    }

                    cmd.Parameters.AddWithValue("@RowerId", RETrip.RowerId.ToString());
                    cmd.Parameters.AddWithValue("@Rower", RETrip.Rower.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", RETrip.CreatedBy.Trim());

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
                        RETripEntryDtlsString InAppCar = new RETripEntryDtlsString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        RETripEntryDtlsString InAppCar = new RETripEntryDtlsString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    RETripEntryDtlsString InAppCar = new RETripEntryDtlsString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
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

        //ReEntry Trip List
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="RETrip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BindReEnrty")]
        public IHttpActionResult BindReEntry([FromBody] RETripEntryDtls RETrip)
        {

            try
            {
                if (RETrip.BoatHouseId != "")
                {
                    List<RETripEntryDtls> li = new List<RETripEntryDtls>();
                    con.Open();
                    string sQuery = " select BookingId, BookingPin, BoatTypeId, BoatType, BoatSeaterId, BoatSeat, BoatHouseId, BoatHouseName, "
                        + " BoatNumber, BoatId, "
                       + " Rower, RowerId, Convert(Nvarchar, BookedDate, 103) as 'BookedDate', convert(varchar(8), TripStartTime,108) as 'TripStartTime' , "
                       + " convert(varchar(8), TripEndTime,108) as 'TripEndTime',convert(varchar(8), ReTripStartTime,108) as 'ReTripStartTime', convert(varchar(8), ReTripEndTime,108) as 'ReTripEndTime', Reason , ActiveStatus from REEntryTripDtls "
                       + " where BoatHouseId = @BoatHouseId AND CAST(CreatedDate AS DATE) = CAST (GETDATE() AS DATE)";
                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = RETrip.BoatHouseId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RETripEntryDtls ShowREEntry = new RETripEntryDtls();
                            ShowREEntry.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowREEntry.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            ShowREEntry.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowREEntry.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowREEntry.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowREEntry.BoatSeat = dt.Rows[i]["BoatSeat"].ToString();
                            ShowREEntry.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            ShowREEntry.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            ShowREEntry.BoatNumber = dt.Rows[i]["BoatNumber"].ToString();
                            ShowREEntry.BoatId = dt.Rows[i]["BoatId"].ToString();
                            ShowREEntry.Rower = dt.Rows[i]["Rower"].ToString();
                            ShowREEntry.RowerId = dt.Rows[i]["RowerId"].ToString();
                            ShowREEntry.BookedDate = dt.Rows[i]["BookedDate"].ToString();
                            ShowREEntry.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ShowREEntry.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ShowREEntry.ReTripStartTime = dt.Rows[i]["ReTripStartTime"].ToString();
                            ShowREEntry.ReTripEndTime = dt.Rows[i]["ReTripEndTime"].ToString();
                            ShowREEntry.Reason = dt.Rows[i]["Reason"].ToString();
                            ShowREEntry.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            li.Add(ShowREEntry);
                        }

                        RETripEntryDtlsStringResList REEntry = new RETripEntryDtlsStringResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(REEntry);
                    }
                    else
                    {
                        RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(REEntryRes);
                    }
                }
                else
                {
                    RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(REEntryRes);
                }
            }
            catch (Exception ex)
            {
                RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(REEntryRes);
            }
        }

        // Checking Booking Id RetripEntry
        [HttpPost]
        [AllowAnonymous]
        [Route("REEntrpTripdtls/GetBookingId")]
        public IHttpActionResult REEntrpTripdtlsGetBookingId([FromBody] RETripEntryDtls RETrip)
        {

            try
            {
                if (RETrip.BoatHouseId != "" && RETrip.BookingId != "")
                {
                    List<RETripEntryDtls> li = new List<RETripEntryDtls>();
                    con.Open();
                    string sQuery = "SELECT BookingId FROM BookingHdr WHERE BoatHouseId = @BoatHouseId "
                                    + " AND BookingId = @BookingId ";
                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters["@BoatHouseId"].Value = RETrip.BoatHouseId.Trim();
                    cmd.Parameters["@BookingId"].Value = RETrip.BookingId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        RETripEntryDtls ShowREEntry = new RETripEntryDtls();
                        ShowREEntry.BookingId = dt.Rows[0]["BookingId"].ToString();

                        li.Add(ShowREEntry);


                        RETripEntryDtlsStringResList REEntry = new RETripEntryDtlsStringResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(REEntry);
                    }
                    else
                    {
                        RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                        {
                            Response = "Invalid Booking Id",
                            StatusCode = 0
                        };
                        return Ok(REEntryRes);
                    }
                }
                else
                {
                    RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                    {
                        Response = "Enter Valid Booking Id",
                        StatusCode = 0
                    };
                    return Ok(REEntryRes);
                }
            }
            catch (Exception ex)
            {
                RETripEntryDtlsString REEntryRes = new RETripEntryDtlsString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(REEntryRes);
            }
        }


        //Change Trip Sheet Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("RescheduleBoardingPass")]
        public IHttpActionResult RescheduleBoardingLog([FromBody] BookingBoardingPass Pass)
        {
            try
            {

                if (Pass.QueryType != "" && Pass.BookingId != "" && Pass.BoatHouseId != ""
                   && Pass.BoatHouseName != "" && Pass.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("SP_BoardingLog", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Pass.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", Pass.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BookingPin", Pass.BookingPin.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", Pass.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", Pass.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", Pass.BoatReferenceNum.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Pass.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Pass.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@OldBoatId", Pass.OldBoatId.ToString());
                    cmd.Parameters.AddWithValue("@OldBoatNum", Pass.OldBoatNum.ToString());
                    cmd.Parameters.AddWithValue("@OldExpectedTime", Pass.OldExpectedTime.ToString());
                    cmd.Parameters.AddWithValue("@OldRowerId", Pass.OldRowerId.ToString());

                    cmd.Parameters.AddWithValue("@NewBoatNum", Pass.NewBoatNum.ToString());
                    cmd.Parameters.AddWithValue("@NewExpectedTime", Pass.NewExpectedTime.ToString());
                    cmd.Parameters.AddWithValue("@NewRowerId", Pass.NewRowerId.ToString());

                    if (Pass.OldTripStartTime.ToString() != "" && Pass.OldTripEndTime.ToString() != "")
                    {
                        cmd.Parameters.AddWithValue("@OldTripStartTime", DateTime.Parse(Pass.OldTripStartTime.ToString(), objEnglishDate));
                        cmd.Parameters.AddWithValue("@OldTripEndTime", DateTime.Parse(Pass.OldTripEndTime.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OldTripStartTime", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@OldTripEndTime", DateTime.Now.ToString());
                    }

                    cmd.Parameters.AddWithValue("@NewTripStartTime", DateTime.Parse(Pass.NewTripStartTime.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@NewTripEndTime", DateTime.Parse(Pass.NewTripEndTime.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@NewBoatId", Pass.NewBoatId.ToString());
                    cmd.Parameters.AddWithValue("@OldDepRefundAmount", Pass.OldDepRefundAmount.ToString());
                    cmd.Parameters.AddWithValue("@NewDepRefundAmount", Pass.NewDepRefundAmount.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", Pass.CreatedBy.Trim());
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
                        CityMappingRes ConMstr = new CityMappingRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        CityMappingRes ConMstr = new CityMappingRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    CityMappingRes Vehicle = new CityMappingRes
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

        //Change Trip Sheet Get Boat Status
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="BM"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlBoatNumStatus/BHId")]
        public IHttpActionResult ddlBoatNum([FromBody] BoatMstr BM)
        {
            try
            {
                if (BM.BoatHouseId != "" && BM.BoatTypeId != "" && BM.BoatSeaterId != "")
                {
                    List<BoatMstr> li = new List<BoatMstr>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT BoatId, BoatName, BoatNum FROM BoatMaster WHERE BoatStatus = 1 "
                                + " AND BoatHouseId = @BoatHouseId AND BoatTypeId = @BoatTypeId "
                                + " AND BoatSeaterId = @BoatSeaterId AND BoatNature = @BoatNature ;", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatNature", System.Data.SqlDbType.Char, 1));
                    cmd.Parameters["@BoatHouseId"].Value = BM.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = BM.BoatTypeId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = BM.BoatSeaterId.Trim();
                    cmd.Parameters["@BoatNature"].Value = BM.BoatStatus.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatMstr ShowBoatTypes = new BoatMstr();
                            ShowBoatTypes.BoatId = dt.Rows[i]["BoatId"].ToString();
                            ShowBoatTypes.BoatName = dt.Rows[i]["BoatName"].ToString();
                            ShowBoatTypes.BoatNum = dt.Rows[i]["BoatNum"].ToString();

                            li.Add(ShowBoatTypes);
                        }

                        BoatMsrList ConfList = new BoatMsrList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BoatMsrString ConfRes = new BoatMsrString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatMsrString Vehicle = new BoatMsrString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BoatMsrString Vehicle = new BoatMsrString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }

        //Change Trip Sheet List
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ChangeBoatBooking")]
        public IHttpActionResult ChangeBoatBooking([FromBody] BookingBoardingPass Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    //Change by Imran on 2021-11-08 
                    string sQuery = "SELECT A.BookingId, A.BoatReferenceNo,A.BookingPin,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType,"
                    + " A.ActualBoatId, A.ActualBoatNum, E.RowerId,  E.RowerName, "
                    + " F.BoatMinTime + F.BoatGraceTime AS 'BoatDuration', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDuration', "
                    + " CONVERT(NVARCHAR(50), D.BookingDate, 103) AS 'BookingDate', "
                    + " CONVERT(VARCHAR, A.TripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripStartTime, 100),7) as TripStartTime, "
                    + " CONVERT(VARCHAR, A.TripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripEndTime, 100),7) as TripEndTime, "
                    + " RIGHT(CONVERT(VARCHAR, ExpectedTime, 100), 7) AS 'ExpectedTime', "
                    + " CASE when D.PremiumStatus = 'P' then 'Permium' else 'Normal' end as PremiumStatus, A.BoatDeposit, A.DepRefundAmount "
                    + " FROM BookingDtl AS A"
                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS E on A.RowerId = E.RowerId and A.BoatHouseId = E.BoatHouseId "
                    + " INNER JOIN BoatRateMaster AS F ON A.BoatHouseId = F.BoatHouseId AND A.BoatTypeId = F.BoatTypeId "
                    + " AND A.BoatSeaterId = F.BoatSeaterId AND F.ActiveStatus='A' "
                    + " WHERE A.BoatHouseId = @BoatHouseId "
                    + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.Status IN('B','R') AND D.Status IN ('B','R','P') "
                    + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL and A.PremiumStatus not in ('I') ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Pass.BoatHouseId.Trim();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BookingBoardingPass Boardingpass = new BookingBoardingPass();
                            Boardingpass.BookingId = dt.Rows[i]["BookingId"].ToString();
                            Boardingpass.BoatReferenceNum = dt.Rows[i]["BoatReferenceNo"].ToString();
                            Boardingpass.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            Boardingpass.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            Boardingpass.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            Boardingpass.BoatType = dt.Rows[i]["BoatType"].ToString();
                            Boardingpass.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            Boardingpass.SeatType = dt.Rows[i]["SeaterType"].ToString();
                            Boardingpass.ActualBoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            Boardingpass.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            Boardingpass.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            Boardingpass.Status = dt.Rows[i]["PremiumStatus"].ToString();
                            Boardingpass.RowerId = dt.Rows[i]["RowerId"].ToString();
                            Boardingpass.RowerName = dt.Rows[i]["RowerName"].ToString();
                            Boardingpass.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            Boardingpass.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            Boardingpass.BoatDuration = dt.Rows[i]["BoatDuration"].ToString();
                            Boardingpass.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            Boardingpass.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            Boardingpass.OldDepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();

                            li.Add(Boardingpass);
                        }

                        BookingBoardingPassList AvailList = new BookingBoardingPassList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(AvailList);
                    }

                    else
                    {
                        BookingBoardingPassRes availRes = new BookingBoardingPassRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(availRes);
                    }
                }
                else
                {
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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
        /// CREATED BY : JayaSuriya
        /// Created Date : 21-Apr-2022
        /// Version : V2
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ChangeBoatBookingV2")]
        public IHttpActionResult ChangeBoatBookingV2([FromBody] BookingBoardingPassV2 Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPassV2> li = new List<BookingBoardingPassV2>();
                    SqlCommand cmd = new SqlCommand("SP_ChangeTripSheetNew", con);

                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1000000;

                    cmd.Parameters.AddWithValue("@QueryType", Pass.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Pass.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BookingId", Pass.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@BookingPin", Pass.BookingPin.Trim());
                    cmd.Parameters.AddWithValue("@FirstPageNo", Pass.FirstPageNo.Trim());
                    cmd.Parameters.AddWithValue("@LastPageNo", Pass.LastPageNo.Trim());
                    cmd.Parameters.AddWithValue("@date", DateTime.Parse(Pass.Date.Trim(), objEnglishDate));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                        if (Pass.QueryType.Trim() == "getCount" ||
                            Pass.QueryType.Trim() == "ViewLogCount" ||
                            Pass.QueryType.Trim() == "getCountSadmin" ||
                            Pass.QueryType.Trim() == "ViewLogCountSadmin")
                        {
                            BookingBoardingPassV2 Boardingpass = new BookingBoardingPassV2();
                            Boardingpass.Count = dt.Rows[0]["Count"].ToString();
                            li.Add(Boardingpass);

                            BookingBoardingPassV2List AvailList = new BookingBoardingPassV2List
                            {
                                Response = li,
                                StatusCode = 1
                            };
                            return Ok(AvailList);
                        }
                        else if (Pass.QueryType.Trim() == "ViewLogDetails" ||
                                 Pass.QueryType.Trim() == "BookinPinBasedFilterLog")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                BookingBoardingPassV2 Boardingpass = new BookingBoardingPassV2();
                                Boardingpass.BookingId = dt.Rows[i]["BookingId"].ToString();
                                Boardingpass.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                                Boardingpass.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                                Boardingpass.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                                Boardingpass.BoatReferenceNum = dt.Rows[i]["BoatReferenceNo"].ToString();
                                Boardingpass.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                                Boardingpass.BoatType = dt.Rows[i]["BoatType"].ToString();
                                Boardingpass.SeatType = dt.Rows[i]["SeaterType"].ToString();
                                Boardingpass.RowerName = dt.Rows[i]["RowerName"].ToString();
                                Boardingpass.OldTravelDuration = dt.Rows[i]["OldTravelDuration"].ToString();
                                Boardingpass.NewTravelDuration = dt.Rows[i]["NewTravelDuration"].ToString();
                                Boardingpass.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                                Boardingpass.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                                Boardingpass.NewTripStartTime = dt.Rows[i]["NewTripStartTime"].ToString();
                                Boardingpass.NewTripEndTime = dt.Rows[i]["NewTripEndTime"].ToString();
                                Boardingpass.NewRowerId = dt.Rows[i]["NewRowerId"].ToString();
                                Boardingpass.NewExpectedTime = dt.Rows[i]["NewExpectedTime"].ToString();
                                Boardingpass.NewBoatNum = dt.Rows[i]["NewBoatNum"].ToString();
                                Boardingpass.NewBoatId = dt.Rows[i]["NewBoatId"].ToString();
                                Boardingpass.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                                li.Add(Boardingpass);
                            }
                            BookingBoardingPassV2List AvailList = new BookingBoardingPassV2List
                            {
                                Response = li,
                                StatusCode = 1
                            };
                            return Ok(AvailList);
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                BookingBoardingPassV2 Boardingpass = new BookingBoardingPassV2();
                                Boardingpass.BookingId = dt.Rows[i]["BookingId"].ToString();
                                Boardingpass.BoatReferenceNum = dt.Rows[i]["BoatReferenceNo"].ToString();
                                Boardingpass.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                                Boardingpass.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                                Boardingpass.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                                Boardingpass.BoatType = dt.Rows[i]["BoatType"].ToString();
                                Boardingpass.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                                Boardingpass.SeatType = dt.Rows[i]["SeaterType"].ToString();
                                Boardingpass.ActualBoatId = dt.Rows[i]["ActualBoatId"].ToString();
                                Boardingpass.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                                Boardingpass.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                                Boardingpass.Status = dt.Rows[i]["PremiumStatus"].ToString();
                                Boardingpass.RowerId = dt.Rows[i]["RowerId"].ToString();
                                Boardingpass.RowerName = dt.Rows[i]["RowerName"].ToString();
                                Boardingpass.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                                Boardingpass.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                                Boardingpass.BoatDuration = dt.Rows[i]["BoatDuration"].ToString();
                                Boardingpass.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                                Boardingpass.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                                Boardingpass.OldDepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                                Boardingpass.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                                li.Add(Boardingpass);
                            }
                            BookingBoardingPassV2List AvailList = new BookingBoardingPassV2List
                            {
                                Response = li,
                                StatusCode = 1
                            };
                            return Ok(AvailList);
                        }
                    }
                    else
                    {
                        BookingBoardingPassV2Res availRes = new BookingBoardingPassV2Res
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(availRes);
                    }
                }
                else
                {
                    BookingBoardingPassV2Res availRes1 = new BookingBoardingPassV2Res
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassV2Res ConfRes = new BookingBoardingPassV2Res
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

        //Change Boat Log Details
        /// <summary>
        /// Modified By : Silambarasu
        /// Modified Date : 07-09-2021
        ///Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ViewBoatLogDetails")]
        public IHttpActionResult ViewBoatLogDetails([FromBody] BookingBoardingPass Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    string sQuery = " SELECT A.BookingId, A.BookingPin, A.NewBoatTypeId, B.BoatType, A.NewBoatSeaterId, C.SeaterType, A.BoatReferenceNo, "
                    + " A.NewBoatId, A.NewBoatNum, A.NewBoatCharge, A.NewRowerCharge,  A.NewDeposit, A.NewNetAmount, A.NewTaxAmount,  "
                    + " CONVERT(NVARCHAR(50), D.BookingDate, 103) AS 'BookingDate', "
                    + " CASE WHEN A.PremiumStatus = 'P' THEN 'Express' WHEN A.PremiumStatus = 'N' THEN 'Normal' WHEN A.PremiumStatus = 'I' THEN 'Individual' ELSE '' END AS 'PremiumStatus', "
                    + " A.ExtraRefundAmount, A.ExtraCharge, A.BoatHouseId, A.BoatHouseName FROM ChangeBoatDetailsLog AS A "
                    + " INNER JOIN BoatTypes AS B ON A.NewBoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.NewBoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId ORDER BY A.UpdatedDate DESC";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = Pass.BoatHouseId.ToString().Trim();

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
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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
        /// Created Date : 2022-04-19
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// Version : V2
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ViewBoatLogDetailsV2")]
        public IHttpActionResult ViewBoatLogDetailsV2([FromBody] BookingBoardingPass Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();

                    int endcount = Int32.Parse(Pass.CountStart.Trim()) + 9;

                    con.Open();
                    string sQuery = "SELECT * FROM "
                    + " (SELECT ROW_NUMBER() OVER(ORDER BY A.UpdatedDate DESC) 'RowNumber', * FROM  "
                    + " (SELECT A.BookingId, A.BookingPin, A.NewBoatTypeId, B.BoatType, A.NewBoatSeaterId, C.SeaterType, A.BoatReferenceNo, "
                    + " A.NewBoatId, A.NewBoatNum, A.NewBoatCharge, A.NewRowerCharge,  A.NewDeposit, A.NewNetAmount, A.NewTaxAmount, "
                    + " CONVERT(NVARCHAR(50), D.BookingDate, 103) AS 'BookingDate', A.UpdatedDate,"
                    + " CASE WHEN A.PremiumStatus = 'P' THEN 'Express' WHEN A.PremiumStatus = 'N' THEN 'Normal' WHEN A.PremiumStatus = 'I' THEN 'Individual' ELSE '' END AS 'PremiumStatus', "
                    + " A.ExtraRefundAmount, A.ExtraCharge, A.BoatHouseId, A.BoatHouseName FROM ChangeBoatDetailsLog AS A "
                    + " INNER JOIN BoatTypes AS B ON A.NewBoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.NewBoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId ) AS A)"
                    + " AS B WHERE B.RowNumber BETWEEN @CountStart AND   @endcount ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@endcount", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = Pass.BoatHouseId.ToString().Trim();
                    cmd.Parameters["@CountStart"].Value = Pass.CountStart.Trim();
                    cmd.Parameters["@endcount"].Value = endcount;

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
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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

        //Change Trip Sheet Log Details
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ViewLogDetails")]
        public IHttpActionResult ViewLogDetails([FromBody] BookingBoardingPass Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    string sQuery = " SELECT A.BookingId, A.BookingPin, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BoatReferenceNo, "
                    + " A.NewBoatId, A.NewBoatNum, ISNULL(RIGHT(CONVERT(VARCHAR, A.NewExpectedTime, 100), 7), '00:00') AS 'NewExpectedTime', "
                    + " CONVERT(NVARCHAR(50), E.BookingDate, 103) AS 'BookingDate', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.OldTripStartTime, A.OldTripEndTime) AS NVARCHAR), 0) AS 'OldTravelDuration', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.NewTripStartTime, A.NewTripEndTime) AS NVARCHAR), 0) AS 'NewTravelDuration', "
                    + " A.NewRowerId, D.RowerName, CONVERT(VARCHAR, A.NewTripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.NewTripStartTime, 100), 7) AS 'NewTripStartTime', "
                    + " CONVERT(VARCHAR, A.NewTripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.NewTripEndTime, 100), 7) AS 'NewTripEndTime',  "
                    + " A.BoatHouseId, A.BoatHouseName FROM BoardingPassLog AS A "
                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS D on A.NewRowerId = D.RowerId and A.BoatHouseId = D.BoatHouseId "
                    + " INNER JOIN BookingHdr AS E ON A.BookingId = E.BookingId AND A.BoatHouseId = E.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId ORDER BY A.CreatedDate DESC";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Pass.BoatHouseId.ToString().Trim();

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
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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

        //Change Trip Sheet Log Details More
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="Pass"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ViewPopLogDetails")]
        public IHttpActionResult ViewPopLogDetails([FromBody] BookingBoardingPass Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    string sQuery = " SELECT A.BookingId, A.BookingPin, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BoatReferenceNo, "
                    + " A.OldBoatId, A.OldBoatNum, ISNULL(RIGHT(CONVERT(VARCHAR, A.OldExpectedTime, 100), 7), '00:00') AS 'OldExpectedTime',  "
                    + " A.OldRowerId, E.RowerName AS 'OldRowerName', CONVERT(VARCHAR, A.OldTripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.OldTripStartTime, 100), 7) AS 'OldTripStartTime',  "
                    + " CONVERT(VARCHAR, A.OldTripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.OldTripEndTime, 100), 7) AS 'OldTripEndTime', "
                    + " A.NewBoatId, A.NewBoatNum, ISNULL(RIGHT(CONVERT(VARCHAR, A.NewExpectedTime, 100), 7), '00:00') AS 'NewExpectedTime',  "
                    + " A.NewRowerId, D.RowerName, CONVERT(VARCHAR, A.NewTripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.NewTripStartTime, 100), 7) AS 'NewTripStartTime', "
                    + " CONVERT(VARCHAR, A.NewTripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.NewTripEndTime, 100), 7) AS 'NewTripEndTime',  "
                    + " A.BoatHouseId, A.BoatHouseName, ISNULL(A.OldDepRefundAmount,0) AS 'OldDepRefundAmount', ISNULL(A.NewDepRefundAmount,0) AS 'NewDepRefundAmount' FROM BoardingPassLog AS A "
                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS D on A.NewRowerId = D.RowerId and A.BoatHouseId = D.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS E on A.OldRowerId = E.RowerId and A.BoatHouseId = E.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND A.BookingId = @BookingId "
                    + " AND A.BookingPin = @BookingPin ORDER BY A.CreatedDate DESC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@BoatHouseId"].Value = Pass.BoatHouseId.ToString().Trim();
                    cmd.Parameters["@BookingId"].Value = Pass.BookingId.ToString().Trim();
                    cmd.Parameters["@BookingPin"].Value = Pass.BookingPin.ToString().Trim();

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
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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

        //Change Boat Details Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("ChangeBoatDetails/Insert")]
        public IHttpActionResult ChangeBoatLog([FromBody] ChangeBoatDetail ChangeBoat)
        {
            try
            {
                if (
                    ChangeBoat.QueryType != ""
                    && ChangeBoat.BookingId != ""
                    && ChangeBoat.BookingDate != ""
                    && ChangeBoat.BookingPin != ""
                    && ChangeBoat.BoatReferenceNo != ""
                    && ChangeBoat.OldBoatTypeId != ""
                    && ChangeBoat.OldBoatSeaterId != ""
                    && ChangeBoat.OldBoatCharge != ""
                    && ChangeBoat.OldRowerCharge != ""
                    && ChangeBoat.OldDeposit != ""
                    && ChangeBoat.OldNetAmount != ""
                    && ChangeBoat.OldTaxAmount != ""
                    && ChangeBoat.NewBoatTypeId != ""
                    && ChangeBoat.NewBoatSeaterId != ""
                    && ChangeBoat.NewBoatCharge != ""
                    && ChangeBoat.NewRowerCharge != ""
                    && ChangeBoat.NewDeposit != ""
                    && ChangeBoat.NewNetAmount != ""
                    && ChangeBoat.ExtraCharge != ""
                    && ChangeBoat.BoatHouseId != ""
                    && ChangeBoat.BoatHouseName != ""
                    && ChangeBoat.UpdatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("SP_ChangeBoatDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", ChangeBoat.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", ChangeBoat.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(ChangeBoat.BookingDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BookingPin", ChangeBoat.BookingPin.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", ChangeBoat.BoatReferenceNo.Trim());
                    cmd.Parameters.AddWithValue("@PremiumStatus", ChangeBoat.PremiumStatus.Trim());

                    cmd.Parameters.AddWithValue("@OldBoatTypeId", ChangeBoat.OldBoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@OldBoatSeaterId", ChangeBoat.OldBoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@OldBoatId", ChangeBoat.OldBoatId.ToString());
                    cmd.Parameters.AddWithValue("@OldBoatNum", ChangeBoat.OldBoatNum.Trim());
                    cmd.Parameters.AddWithValue("@OldBoatCharge", ChangeBoat.OldBoatCharge.Trim());
                    cmd.Parameters.AddWithValue("@OldRowerCharge", ChangeBoat.OldRowerCharge.Trim());
                    cmd.Parameters.AddWithValue("@OldBoatDeposit", ChangeBoat.OldDeposit.Trim());
                    cmd.Parameters.AddWithValue("@OldOfferAmount", ChangeBoat.OldOfferAmount.Trim());
                    cmd.Parameters.AddWithValue("@OldNetAmount", ChangeBoat.OldNetAmount.Trim());
                    cmd.Parameters.AddWithValue("@OldTaxAmount", ChangeBoat.OldTaxAmount.ToString().Trim());

                    cmd.Parameters.AddWithValue("@NewBoatTypeId", ChangeBoat.NewBoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@NewBoatSeaterId", ChangeBoat.NewBoatSeaterId.Trim());
                    cmd.Parameters.AddWithValue("@NewBoatCharge", ChangeBoat.NewBoatCharge.ToString());
                    cmd.Parameters.AddWithValue("@NewRowerCharge", ChangeBoat.NewRowerCharge.ToString());
                    cmd.Parameters.AddWithValue("@NewBoatDeposit", ChangeBoat.NewDeposit.ToString());
                    cmd.Parameters.AddWithValue("@NewNetAmount", ChangeBoat.NewNetAmount.ToString());
                    cmd.Parameters.AddWithValue("@NewTaxAmount", ChangeBoat.NewTaxAmount.Trim());

                    cmd.Parameters.AddWithValue("@ExtraRefundAmount", ChangeBoat.ExtraRefundAmount.ToString());
                    cmd.Parameters.AddWithValue("@ExtraCharge", ChangeBoat.ExtraCharge.Trim());
                    cmd.Parameters.AddWithValue("@PaymentModeId", ChangeBoat.PaymentModeId.ToString());
                    cmd.Parameters.AddWithValue("@PaymentModeType", ChangeBoat.PaymentModeType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", ChangeBoat.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", ChangeBoat.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@UpdatedBy", ChangeBoat.UpdatedBy.Trim());

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
                        ChangeBoatDetailRes EmMstr = new ChangeBoatDetailRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(EmMstr);
                    }
                    else
                    {
                        ChangeBoatDetailRes EmMstr = new ChangeBoatDetailRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(EmMstr);
                    }
                }
                else
                {
                    ChangeBoatDetailRes EmMstr = new ChangeBoatDetailRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
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

        /***Online Boating***/
        //Online Before Transaction

        /// <summary>
        /// Modified By : Vediyappan
        /// Modified Date : 09-08-2021
        /// </summary>
        /// <param name="TransactionNo"></param>
        /// <param name="BookingDate"></param>
        /// <param name="BookingPin"></param>
        /// <param name="UserId"></param>
        /// <param name="MobileNo"></param>
        /// <param name="EmailId"></param>
        /// <param name="Amount"></param>
        /// <param name="BoatHouseId"></param>
        /// <param name="BoatHouseName"></param>
        /// <param name="BookingMedia"></param>
        /// <param name="BookingType"></param>
        /// <param name="PremiumStatus"></param>
        /// <param name="NoOfPass"></param>
        /// <param name="NoOfChild"></param>
        /// <param name="NoOfInfant"></param>
        /// <param name="BoatTypeId"></param>
        /// <param name="BoatSeaterId"></param>
        /// <param name="BookingDuration"></param>
        /// <param name="InitBoatCharge"></param>
        /// <param name="InitRowerCharge"></param>
        /// <param name="BoatDeposit"></param>
        /// <param name="InitOfferAmount"></param>
        /// <param name="InitNetAmount"></param>
        /// <param name="TaxDetails"></param>
        /// <param name="OthServiceStatus"></param>
        /// <param name="OthServiceId"></param>
        /// <param name="OthChargePerItem"></param>
        /// <param name="OthNoOfItems"></param>
        /// <param name="OthNetAmount"></param>
        /// <param name="OthTaxDetails"></param>
        /// <param name="BFDInitBoatCharge"></param>
        /// <param name="BFDInitNetAmount"></param>
        /// <param name="BFDGST"></param>
        /// <param name="ModuleType"></param>
        /// <param name="CustomerGSTNo"></param>
        /// <param name="BookingTimeSlotId"></param>
        /// <param name="BookingBlockId"></param>
        /// <returns></returns>
        public string SaveOnlineBoatBooking(string TransactionNo, string BookingDate, string BookingPin, string UserId,
                string MobileNo, string EmailId, string Amount, string BoatHouseId, string BoatHouseName,
                string BookingMedia, string BookingType, string PremiumStatus, string NoOfPass, string NoOfChild, string NoOfInfant, string BoatTypeId,
                string BoatSeaterId, string BookingDuration, string InitBoatCharge, string InitRowerCharge, string BoatDeposit, string InitOfferAmount,
                string InitNetAmount, string TaxDetails, string OthServiceStatus, string OthServiceId, string OthChargePerItem, string OthNoOfItems,
                string OthNetAmount, string OthTaxDetails, string BFDInitBoatCharge, string BFDInitNetAmount, string BFDGST, string ModuleType,
                string CustomerGSTNo, string BookingTimeSlotId, string BookingBlockId)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BoatBookingNew", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;

                //Boat Booking
                cmd.Parameters.AddWithValue("@QueryType", "Insert");
                cmd.Parameters.AddWithValue("@BookingId", "0");
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.Trim());

                cmd.Parameters.AddWithValue("@BookingPin", BookingPin);
                cmd.Parameters.AddWithValue("@CustomerId", UserId.Trim());
                cmd.Parameters.AddWithValue("@CustomerMobileNo", MobileNo.Trim());
                cmd.Parameters.AddWithValue("@CustomerName", "OnlinePayment");
                cmd.Parameters.AddWithValue("@CustomerAddress", "OnlinePayment");

                if (CustomerGSTNo == "")
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString());
                }

                cmd.Parameters.AddWithValue("@PremiumStatus", PremiumStatus.Trim());

                cmd.Parameters.AddWithValue("@NoOfPass", NoOfPass.Trim());
                cmd.Parameters.AddWithValue("@NoOfChild", NoOfChild.Trim());
                cmd.Parameters.AddWithValue("@NoOfInfant", NoOfInfant.Trim());

                cmd.Parameters.AddWithValue("@OfferId", "0");

                cmd.Parameters.AddWithValue("@InitBillAmount", Amount.Trim());
                cmd.Parameters.AddWithValue("@PaymentType", "3");
                cmd.Parameters.AddWithValue("@ActualBillAmount", "0");

                cmd.Parameters.AddWithValue("@Status", "B");

                cmd.Parameters.AddWithValue("@BoatTypeId", BoatTypeId.Trim());
                cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSeaterId.Trim());
                cmd.Parameters.AddWithValue("@BookingDuration", BookingDuration.Trim());
                cmd.Parameters.AddWithValue("@InitBoatCharge", InitBoatCharge.Trim());
                cmd.Parameters.AddWithValue("@InitRowerCharge", InitRowerCharge.Trim());
                cmd.Parameters.AddWithValue("@BoatDeposit", BoatDeposit.Trim());

                cmd.Parameters.AddWithValue("@TaxDetails", TaxDetails.Trim());
                cmd.Parameters.AddWithValue("@InitOfferAmount", InitOfferAmount.Trim());
                cmd.Parameters.AddWithValue("@InitNetAmount", InitNetAmount.Trim());

                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", UserId.Trim());

                // Other Service Booking
                cmd.Parameters.AddWithValue("@OthServiceStatus", OthServiceStatus.Trim());
                cmd.Parameters.AddWithValue("@OthServiceId", OthServiceId.Trim());
                cmd.Parameters.AddWithValue("@OthChargePerItem", OthChargePerItem.Trim());
                cmd.Parameters.AddWithValue("@OthNoOfItems", OthNoOfItems.Trim());

                cmd.Parameters.AddWithValue("@OthTaxDetails", OthTaxDetails.Trim());
                cmd.Parameters.AddWithValue("@OthNetAmount", OthNetAmount.Trim());

                cmd.Parameters.AddWithValue("@BFDInitBoatCharge", BFDInitBoatCharge.Trim());
                cmd.Parameters.AddWithValue("@BFDInitNetAmount", BFDInitNetAmount.Trim());
                cmd.Parameters.AddWithValue("@BFDGST", BFDGST.Trim());

                cmd.Parameters.AddWithValue("@CollectedAmount", "0");
                cmd.Parameters.AddWithValue("@BalanceAmount", "0");

                cmd.Parameters.AddWithValue("@BookingTimeSlotId", BookingTimeSlotId.Trim());
                cmd.Parameters.AddWithValue("@BookingBlockId", BookingBlockId.Trim());

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
                    SaveTransactionHistroryDetails(TransactionNo.Trim(), sResult[2].Trim(), BookingDate.Trim(), Amount.Trim(), BookingType.Trim(),
                         UserId.Trim(), MobileNo.Trim(), EmailId.Trim(), BoatHouseId.Trim(), BoatHouseName.Trim(), BookingMedia.Trim(),
                         ModuleType.Trim());
                }

                return sReturn;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw ex;
            }
        }

        public string SaveOnlineOtherServiceBooking(string TransactionNo, string Amount, string BookingDate, string UserId,
            string MobileNo, string EmailId, string BoatHouseId, string BoatHouseName, string BookingMedia, string OthServiceStatus,
            string OthServiceId, string OthChargePerItem, string OthNoOfItems, string OthNetAmount, string CGSTOthTaxAmount, string SGSTOthTaxAmount, string ModuleType)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BookingOtherServices", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;

                cmd.Parameters.AddWithValue("@QueryType", "Insert");
                cmd.Parameters.AddWithValue("@BookingId", "0");
                cmd.Parameters.AddWithValue("@ServiceId", OthServiceId.Trim());
                cmd.Parameters.AddWithValue("@BookingType", "I");
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.Trim());
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.Trim(), objEnglishDate));

                cmd.Parameters.AddWithValue("@ChargePerItem", OthChargePerItem.Trim());
                cmd.Parameters.AddWithValue("@NoOfItems", OthNoOfItems.Trim());
                cmd.Parameters.AddWithValue("@CGSTTaxAmount", CGSTOthTaxAmount.Trim());
                cmd.Parameters.AddWithValue("@SGSTTaxAmount", SGSTOthTaxAmount.Trim());
                cmd.Parameters.AddWithValue("@NetAmount", OthNetAmount.Trim());
                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.Trim());
                cmd.Parameters.AddWithValue("@CustomerMobileNo", MobileNo.Trim());
                cmd.Parameters.AddWithValue("@PaymentType", "3");
                cmd.Parameters.AddWithValue("@CreatedBy", UserId.Trim());

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
                    SaveTransactionHistroryDetails(TransactionNo.Trim(), sResult[2].Trim(), BookingDate.Trim(), Amount.Trim(), "I",
                        UserId.Trim(), MobileNo.Trim(), EmailId.Trim(), BoatHouseId.Trim(), BoatHouseName.Trim(), BookingMedia.Trim(),
                        ModuleType.Trim());
                }

                return sReturn;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw ex;
            }
        }

        /// <summary>
        /// Modified By:Jaya Suriya
        /// Modified Date:25-10-2021, Added OthersRescheduleDate And RescheduleDescription Parameter
        /// </summary>
        public string SaveOnlineReSheduleBooking(string TransactionNo, string Amount, string UserId,
            string MobileNo, string EmailId, string BoatHouseId, string BoatHouseName, string BookingMedia,
            string BRSBookingId, string BRSNewDate, string BRSCharge, string BRSCGST, string BRSSGST,
            string BRSRuleId, string ModuleType, string BRSBookingPin, string TimeSlot, string BRSChargeType,
            string RescheduleDescription, string OthersRescheduleDate)
        {
            try
            {
                string BookingDate = DateTime.Now.ToString("dd/MM/yyyy");

                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BookingReschedule", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;

                cmd.Parameters.AddWithValue("@QueryType", "PublicReschedule");
                cmd.Parameters.AddWithValue("@BookingId", BRSBookingId.Trim());
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.Trim());
                cmd.Parameters.AddWithValue("@BookingNewDate", BRSNewDate);
                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", UserId.Trim());
                cmd.Parameters.AddWithValue("@RescheduledTotalcharge", Amount.Trim());
                cmd.Parameters.AddWithValue("@CGST", BRSCGST.Trim());
                cmd.Parameters.AddWithValue("@SGST", BRSSGST.Trim());
                cmd.Parameters.AddWithValue("@RescheduledCharge", BRSCharge.Trim());
                cmd.Parameters.AddWithValue("@PaymentType", "3");
                cmd.Parameters.AddWithValue("@Hour", "0");
                cmd.Parameters.AddWithValue("@Minute", "0");
                cmd.Parameters.AddWithValue("@ActivityId", BRSRuleId.Trim());
                cmd.Parameters.AddWithValue("@BookingPin", BRSBookingPin.Trim());
                cmd.Parameters.AddWithValue("@SlotId", TimeSlot.Trim());
                cmd.Parameters.AddWithValue("@ChargeType", BRSChargeType.Trim());
                if (OthersRescheduleDate.Trim() == "-")
                {
                    cmd.Parameters.AddWithValue("@OthersRescheduleDate", "");
                    cmd.Parameters.AddWithValue("@RescheduleDescription", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@OthersRescheduleDate", OthersRescheduleDate.Trim());
                    cmd.Parameters.AddWithValue("@RescheduleDescription", RescheduleDescription.Trim());
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
                    SaveTransactionHistroryDetails(TransactionNo.Trim(), BRSBookingId.Trim(), BookingDate.Trim(), Amount.Trim(), "R",
                        UserId.Trim(), MobileNo.Trim(), EmailId.Trim(), BoatHouseId.Trim(), BoatHouseName.Trim(), BookingMedia.Trim(),
                        ModuleType.Trim());
                }

                return sReturn;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw ex;
            }
        }

        public void SaveTransactionHistroryDetails(string TransactionNo, string BookingId, string BookingDate, string Amount, string BookingType,
            string UserId, string MobileNo, string EmailId, string BoatHouseId, string BoatHouseName, string BookingMedia, string ModuleType)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("SaveOnlineBookingAfterTransaction", con_Common);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;

                //Boat Booking
                cmd.Parameters.AddWithValue("@QueryType", "History");
                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo.Trim());
                cmd.Parameters.AddWithValue("@UserId", UserId.Trim());
                cmd.Parameters.AddWithValue("@MobileNo", MobileNo.Trim());
                cmd.Parameters.AddWithValue("@EmailId", EmailId.Trim());

                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.Trim());
                cmd.Parameters.AddWithValue("@BookingType", BookingType.Trim());
                cmd.Parameters.AddWithValue("@BookingId", BookingId.Trim());
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@Amount", Amount.Trim());
                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.Trim());
                cmd.Parameters.AddWithValue("@ModuleType", ModuleType.Trim());

                cmd.Parameters.AddWithValue("@BankReferenceNo", "");
                cmd.Parameters.AddWithValue("@OrderStatus", "");

                SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                RuturnValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(RuturnValue);
                con_Common.Open();

                cmd.ExecuteNonQuery();
                sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                string[] sResult = sReturn.Split('~');
                con_Common.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw ex;
            }
        }

        //Get Boat Seat In Boat Rate based on Boat Type
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="tx"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRate/SeaterIn")]
        public IHttpActionResult BoatRateSeat([FromBody] BoatSeatMaster tx)
        {
            try
            {
                List<BoatSeatMaster> li = new List<BoatSeatMaster>();
                con.Open();

                SqlCommand cmd = new SqlCommand(" SELECT * FROM BoatSeat WHERE BoatHouseId = @BoatHouseId AND ActiveStatus = 'A' "
                    + " AND BoatSeaterId IN (SELECT BoatSeaterId FROM BoatRateMaster WHERE BoatTypeId = @BoatTypeId "
                    + " AND BoatHouseId = @BoatHouseId AND ActiveStatus = 'A')", con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = tx.BoatHouseId.ToString().Trim();
                cmd.Parameters["@BoatTypeId"].Value = tx.BoatTypeId.ToString().Trim();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatSeatMaster Tax1 = new BoatSeatMaster();
                        Tax1.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        Tax1.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                        li.Add(Tax1);
                    }

                    BoatSeatMasterList Tax = new BoatSeatMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Tax);
                }

                else
                {
                    BoatSeatMasterString Tax = new BoatSeatMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(Tax);
                }
            }
            catch (Exception ex)
            {
                BoatSeatMasterString ConfRes = new BoatSeatMasterString
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RegPrinter")]
        public IHttpActionResult PrinterRegistrationTest([FromBody] PrinterReg Ip)
        {
            try
            {
                if (Ip.QueryType != "" && Ip.PrinterRegNo != "" && Ip.BoatHouseId != "" && Ip.PrinterName != "" && Ip.ComputerName != "" && Ip.RegistrationStatus != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_Printers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Ip.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@PrinterRegNo", Ip.PrinterRegNo.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Ip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@PrinterName", Ip.PrinterName.Trim());
                    cmd.Parameters.AddWithValue("@ComputerName", Ip.ComputerName.Trim());
                    cmd.Parameters.AddWithValue("@RegistrationStatus", Ip.RegistrationStatus.Trim());

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
                        List<PrinterReg> li = new List<PrinterReg>();
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("SELECT * FROM MstrPrinters WHERE PrinterRegNo=@PrinterRegNo ", con);
                        cmd.Parameters.Add(new SqlParameter("@PrinterRegNo", System.Data.SqlDbType.VarChar, 150));
                        cmd.Parameters["@PrinterRegNo"].Value = Ip.PrinterRegNo.Trim();

                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PrinterReg PrSt = new PrinterReg();
                                PrSt.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                                PrSt.PrinterRegNo = dt.Rows[i]["PrinterRegNo"].ToString();
                                PrSt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                                PrSt.PrinterName = dt.Rows[i]["PrinterName"].ToString();
                                PrSt.PrinterMake = dt.Rows[i]["PrinterMake"].ToString();
                                PrSt.RegisteredDate = dt.Rows[i]["RegisteredDate"].ToString();
                                PrSt.PrinterStatus = dt.Rows[i]["PrinterStatus"].ToString();
                                PrSt.RegistrationStatus = dt.Rows[i]["RegistrationStatus"].ToString();
                                PrSt.ComputerName = dt.Rows[i]["ComputerName_Mac"].ToString();
                                PrSt.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                                PrSt.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();
                                PrSt.AuthorisedBy = dt.Rows[i]["AuthorisedBy"].ToString();

                                li.Add(PrSt);
                            }

                            PrinterRegList ConfList = new PrinterRegList
                            {
                                Response = li,
                                StatusCode = 1
                            };
                            return Ok(ConfList);
                        }
                        else
                        {
                            PrinterRegStr InAppCar = new PrinterRegStr
                            {
                                Response = sResult[1].Trim(),
                                StatusCode = 0
                            };
                            return Ok(InAppCar);
                        }
                    }
                    else
                    {
                        PrinterRegStr InAppCar = new PrinterRegStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    PrinterRegStr InAppCar = new PrinterRegStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetPrintJobs")]
        public IHttpActionResult GetPrintJobs([FromBody] PrinterReg Ip)
        {
            try
            {
                if (Ip.PrinterRegNo != "" && Ip.BoatHouseId != "" && Ip.ComputerName != "" && Ip.PassCode != "")
                {
                    List<PrinterJob> li = new List<PrinterJob>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT LogNo,PrinterRegNo,BoatHouseId,BookingId,WithOthers,ComputerName_Mac,JobID,"
                        + " Print_Date,Job_Status,Job_CreateDateTime FROM PrintJobs"
                        + " WHERE PrinterRegNo= @PrinterRegNo AND BoatHouseId = @BoatHouseId "
                        + " AND  ComputerName_Mac =@ComputerName AND JobPassCode = @PassCode "
                        + " AND Job_Status='P'", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PrinterRegNo", System.Data.SqlDbType.VarChar, 150));
                    cmd.Parameters.Add(new SqlParameter("@ComputerName", System.Data.SqlDbType.VarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@PassCode", System.Data.SqlDbType.VarChar, 20));

                    cmd.Parameters["@BoatHouseId"].Value = Ip.BoatHouseId.ToString().Trim();
                    cmd.Parameters["@PrinterRegNo"].Value = Ip.PrinterRegNo.ToString().Trim();
                    cmd.Parameters["@ComputerName"].Value = Ip.ComputerName.ToString().Trim();
                    cmd.Parameters["@PassCode"].Value = Ip.PassCode.ToString().Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PrinterJob Pr = new PrinterJob();
                            Pr.LogNo = dt.Rows[i]["LogNo"].ToString();
                            Pr.PrinterRegNo = dt.Rows[i]["PrinterRegNo"].ToString();
                            Pr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            Pr.BookingId = dt.Rows[i]["BookingId"].ToString();
                            Pr.WithOthers = dt.Rows[i]["WithOthers"].ToString();
                            Pr.ComputerName_Mac = dt.Rows[i]["ComputerName_Mac"].ToString();
                            Pr.JobID = dt.Rows[i]["JobID"].ToString();
                            Pr.Print_Date = dt.Rows[i]["Print_Date"].ToString();
                            Pr.Job_Status = dt.Rows[i]["Job_Status"].ToString();
                            Pr.Job_CreateDateTime = dt.Rows[i]["Job_CreateDateTime"].ToString();
                            li.Add(Pr);
                        }
                        PrinterJobLst ConfList = new PrinterJobLst
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        PrinterJobStr ConfRes = new PrinterJobStr
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    PrinterJobStr Vehicle = new PrinterJobStr
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CreatePrinterPair")]
        public IHttpActionResult CreatePrinterPair([FromBody] PrinterPairs Ip)
        {
            try
            {
                if (Ip.QueryType != "" && Ip.BoatHouseId != "")
                {
                    var random = new Random();
                    string WebKey = string.Empty;

                    //for (int i = 0; i < 8; i++)
                    //{
                    //    WebKey = String.Concat(WebKey, random.Next(10).ToString());
                    //}

                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    WebKey = new string(Enumerable.Repeat(chars, 10)
                      .Select(s => s[random.Next(s.Length)]).ToArray());


                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_PrinterPair", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Ip.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Ip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@WebKey", WebKey.Trim());

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
                        List<PrinterPairs> li = new List<PrinterPairs>();
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("SELECT PairLogId, PrinterRegNo, BoatHouseId, ComputerName_Mac, WebKey, ApplicationKey, IsPaired,"
                            + " PairedOn, PairedRemarks, LogCreatedOn, LogSystemIP FROM PrinterPair WHERE WebKey= @WebKey", con);

                        cmd.Parameters.Add(new SqlParameter("@WebKey", System.Data.SqlDbType.VarChar, 20));
                        cmd.Parameters["@WebKey"].Value = WebKey;
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PrinterPairs PrSt = new PrinterPairs();
                                PrSt.PairLogId = dt.Rows[i]["PairLogId"].ToString();
                                PrSt.PrinterRegNo = dt.Rows[i]["PrinterRegNo"].ToString();
                                PrSt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                                PrSt.ComputerName_Mac = dt.Rows[i]["ComputerName_Mac"].ToString();
                                PrSt.WebKey = dt.Rows[i]["WebKey"].ToString();

                                PrSt.ApplicationKey = dt.Rows[i]["ApplicationKey"].ToString();
                                PrSt.IsPaired = dt.Rows[i]["IsPaired"].ToString();
                                PrSt.PairedOn = dt.Rows[i]["PairedOn"].ToString();
                                PrSt.PairedRemarks = dt.Rows[i]["PairedRemarks"].ToString();
                                PrSt.LogCreatedOn = dt.Rows[i]["LogCreatedOn"].ToString();
                                PrSt.LogSystemIP = dt.Rows[i]["LogSystemIP"].ToString();

                                li.Add(PrSt);
                            }

                            PrinterPairslst ConfList = new PrinterPairslst
                            {
                                Response = li,
                                StatusCode = 1
                            };
                            return Ok(ConfList);
                        }
                        else
                        {
                            PrinterPairsStr InAppCar = new PrinterPairsStr
                            {
                                Response = sResult[1].Trim(),
                                StatusCode = 0
                            };
                            return Ok(InAppCar);
                        }
                    }
                    else
                    {
                        PrinterPairsStr InAppCar = new PrinterPairsStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    PrinterPairsStr InAppCar = new PrinterPairsStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ConnectPrinterPair")]
        public IHttpActionResult ConnectPrinterPair([FromBody] PrinterPairs Ip)
        {
            try
            {
                if (Ip.QueryType != "" && Ip.PrinterRegNo != "" && Ip.BoatHouseId != "" && Ip.ComputerName_Mac != ""
                    && Ip.ApplicationKey != "" && Ip.LogSystemIP != "" && Ip.PublicIP != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_PrinterPair", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Ip.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@PrinterRegNo", Ip.PrinterRegNo.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Ip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@ComputerName_Mac", Ip.ComputerName_Mac.Trim());
                    cmd.Parameters.AddWithValue("@WebKey", Ip.ApplicationKey.Trim());
                    cmd.Parameters.AddWithValue("@LogSystemIP", Ip.LogSystemIP.Trim());
                    cmd.Parameters.AddWithValue("@PublicIP", Ip.PublicIP.Trim());

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
                        List<PrinterPairs> li = new List<PrinterPairs>();
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("SELECT PairLogId, PrinterRegNo, BoatHouseId, ComputerName_Mac, WebKey, ApplicationKey, IsPaired,"
                            + " PairedOn, PairedRemarks, LogCreatedOn, LogSystemIP FROM PrinterPair WHERE WebKey=@ApplicationKey ", con);

                        cmd.Parameters.Add(new SqlParameter("@ApplicationKey", System.Data.SqlDbType.VarChar, 20));
                        cmd.Parameters["@ApplicationKey"].Value = Ip.ApplicationKey.Trim();
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        con.Close();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PrinterPairs PrSt = new PrinterPairs();
                                PrSt.PairLogId = dt.Rows[i]["PairLogId"].ToString();
                                PrSt.PrinterRegNo = dt.Rows[i]["PrinterRegNo"].ToString();
                                PrSt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                                PrSt.ComputerName_Mac = dt.Rows[i]["ComputerName_Mac"].ToString();
                                PrSt.WebKey = dt.Rows[i]["WebKey"].ToString();

                                PrSt.ApplicationKey = dt.Rows[i]["ApplicationKey"].ToString();
                                PrSt.IsPaired = dt.Rows[i]["IsPaired"].ToString();
                                PrSt.PairedOn = dt.Rows[i]["PairedOn"].ToString();
                                PrSt.PairedRemarks = dt.Rows[i]["PairedRemarks"].ToString();
                                PrSt.LogCreatedOn = dt.Rows[i]["LogCreatedOn"].ToString();
                                PrSt.LogSystemIP = dt.Rows[i]["LogSystemIP"].ToString();

                                li.Add(PrSt);
                            }

                            PrinterPairslst ConfList = new PrinterPairslst
                            {
                                Response = li,
                                StatusCode = 1
                            };
                            return Ok(ConfList);
                        }
                        else
                        {
                            PrinterPairsStr InAppCar = new PrinterPairsStr
                            {
                                Response = sResult[1].Trim(),
                                StatusCode = 0
                            };
                            return Ok(InAppCar);
                        }
                    }
                    else
                    {
                        PrinterPairsStr InAppCar = new PrinterPairsStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    PrinterPairsStr InAppCar = new PrinterPairsStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified By Jaya Suriya A
        /// Modified Date 27-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetPrinterPairStatus")]
        public IHttpActionResult GetPrinterPairStatus([FromBody] PrinterPairs Ip)
        {
            try
            {
                if (Ip.BoatHouseId != "" && Ip.ApplicationKey != "")
                {
                    List<PrinterPairs> li = new List<PrinterPairs>();
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT PairLogId, PrinterRegNo, BoatHouseId, ComputerName_Mac, WebKey, ApplicationKey, IsPaired,"
                        + " PairedOn, PairedRemarks, LogCreatedOn, LogSystemIP FROM PrinterPair WHERE WebKey=@ApplicationKey AND"
                        + " BoatHouseId=@BoatHouseId", con);

                    cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd1.Parameters.Add(new SqlParameter("@ApplicationKey", System.Data.SqlDbType.VarChar, 20));

                    cmd1.Parameters["@BoatHouseId"].Value = Ip.BoatHouseId.Trim();
                    cmd1.Parameters["@ApplicationKey"].Value = Ip.ApplicationKey.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PrinterPairs PrSt = new PrinterPairs();
                            PrSt.PairLogId = dt.Rows[i]["PairLogId"].ToString();
                            PrSt.PrinterRegNo = dt.Rows[i]["PrinterRegNo"].ToString();
                            PrSt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            PrSt.ComputerName_Mac = dt.Rows[i]["ComputerName_Mac"].ToString();
                            PrSt.WebKey = dt.Rows[i]["WebKey"].ToString();

                            PrSt.ApplicationKey = dt.Rows[i]["ApplicationKey"].ToString();
                            PrSt.IsPaired = dt.Rows[i]["IsPaired"].ToString();
                            PrSt.PairedOn = dt.Rows[i]["PairedOn"].ToString();
                            PrSt.PairedRemarks = dt.Rows[i]["PairedRemarks"].ToString();
                            PrSt.LogCreatedOn = dt.Rows[i]["LogCreatedOn"].ToString();
                            PrSt.LogSystemIP = dt.Rows[i]["LogSystemIP"].ToString();

                            li.Add(PrSt);
                        }

                        PrinterPairslst ConfList = new PrinterPairslst
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        PrinterPairsStr InAppCar = new PrinterPairsStr
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    PrinterJobStr Vehicle = new PrinterJobStr
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
        [Route("BillPrintStatus")]
        public IHttpActionResult BillPrintStatus([FromBody] BillPrinter Ip)
        {
            try
            {
                if (Ip.LogNo != "" && Ip.PrinterRegNo != "" && Ip.BoatHouseId != "" && Ip.BookingId != "" && Ip.ComputerName != "" && Ip.Job_Status != "")
                {

                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_BillPrinterStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Ip.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@LogNo", Ip.LogNo.ToString());
                    cmd.Parameters.AddWithValue("@PrinterRegNo", Ip.PrinterRegNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Ip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", Ip.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@ComputerName", Ip.ComputerName.ToString());
                    cmd.Parameters.AddWithValue("@Job_Status", Ip.Job_Status.ToString());

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
                        BillPrinterStr InAppCar = new BillPrinterStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        BillPrinterStr InAppCar = new BillPrinterStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    BillPrinterStr InAppCar = new BillPrinterStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
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
        [Route("RePrintBill")]
        public IHttpActionResult RePrintBill([FromBody] RePrints Ip)
        {
            try
            {
                if (Ip.QueryType != "" && Ip.BoatHouseId != "" && Ip.BookingId != "" && Ip.@WebKey != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_RePrint", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Ip.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Ip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BookingId", Ip.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@WebKey", Ip.WebKey.Trim());

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
                        RePrintsStr InAppCar = new RePrintsStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        RePrintsStr InAppCar = new RePrintsStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    RePrintsStr InAppCar = new RePrintsStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
                }
            }
            catch (Exception ex)
            {
                RePrintsStr ConfRes = new RePrintsStr
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("chkOfferApplicable")]
        public IHttpActionResult chkOfferApplicable([FromBody] OfferMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.Input1 != "")
                {
                    List<OfferMaster> li = new List<OfferMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT  * FROM OfferMaster WHERE ActiveStatus = 'A' AND EffectiveFrom <= CAST(GETDATE()AS DATE)  and EffectiveTill >= CAST(GETDATE() AS DATE) "
                    + " AND BoatHouseId LIKE @BoatHouseId AND MinBIllAmount <= @Input1 "
                    + " ORDER BY MinBIllAmount DESC", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Input1", System.Data.SqlDbType.Decimal));

                    cmd.Parameters["@BoatHouseId"].Value = "%" + bHMstr.BoatHouseId + "%";
                    cmd.Parameters["@Input1"].Value = bHMstr.Input1;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Table");
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
                    BoatHouseMasterString Vehicle = new BoatHouseMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BoatHouseMasterString Vehicle = new BoatHouseMasterString
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }

        // Update GST No & Email Id in Boat Booking Details
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="UpGST"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateGST/BookingHdr")]
        public IHttpActionResult UpdateGST([FromBody] BoatBooking UpGST)
        {
            try
            {
                if (UpGST.BookingId != "" && UpGST.CustomerGSTNo != "" && UpGST.BoatHouseId != "" && UpGST.UserId != "")
                {
                    string sReturn = string.Empty;
                    string sQuery = string.Empty;

                    sQuery = "Update BookingHdr set CustomerMobile = @CustomerMobileNo, CustomerGSTNo = @CustomerGSTNo , "
                        + " Updatedby = @CreatedBy , UpdatedDate = GETDATE() , CustomerEmailId = @CustomerEmailId "
                           + " Where BookingId = @BookingId  and BoatHouseId = @BoatHouseId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CustomerMobileNo", System.Data.SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@CustomerGSTNo", System.Data.SqlDbType.NVarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CustomerEmailId", System.Data.SqlDbType.NVarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.VarChar, 50));

                    cmd.Parameters["@BoatHouseId"].Value = UpGST.BoatHouseId;
                    cmd.Parameters["@CustomerMobileNo"].Value = UpGST.CustomerMobileNo;
                    cmd.Parameters["@CustomerGSTNo"].Value = UpGST.CustomerGSTNo;
                    cmd.Parameters["@CreatedBy"].Value = UpGST.CreatedBy;
                    cmd.Parameters["@CustomerEmailId"].Value = UpGST.CustomerEmailId;
                    cmd.Parameters["@BookingId"].Value = UpGST.BookingId;

                    con.Open();
                    int sResult = cmd.ExecuteNonQuery();
                    con.Close();

                    if (sResult > 0)
                    {
                        BoatBookingStr InAppCar = new BoatBookingStr
                        {
                            Response = "Updated Sucessfully",

                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        BoatBookingStr InAppCar = new BoatBookingStr
                        {
                            Response = "Error In Updating",
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    BoatBookingStr InAppCar = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
                }
            }
            catch (Exception ex)
            {
                BoatBookingStr ConfRes = new BoatBookingStr
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
        [Route("Refund/INS")]
        public IHttpActionResult InsRefund([FromBody] Refund InsRefund)
        {
            try
            {
                if (InsRefund.QueryType != "" && InsRefund.UserId != ""
                    && InsRefund.UserName != ""
                    /*&& InsRefund.RequestStatus != ""*/
                    && InsRefund.BoatHouseId != "" && InsRefund.BoatHouseName != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("Refund", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsRefund.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsRefund.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", InsRefund.UserId.ToString());
                    cmd.Parameters.AddWithValue("@UserName", InsRefund.UserName.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsRefund.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeName", InsRefund.BoatTypeName.ToString());
                    cmd.Parameters.AddWithValue("@RequestedAmount", InsRefund.RequestedAmount.ToString());
                    cmd.Parameters.AddWithValue("@RequestedBy", InsRefund.RequestedBy.ToString());
                    cmd.Parameters.AddWithValue("@PaidAmount", InsRefund.PaidAmount.ToString());
                    cmd.Parameters.AddWithValue("@PaidBy", InsRefund.PaidBy.ToString());
                    cmd.Parameters.AddWithValue("@PaidDate", InsRefund.PaidDate.ToString());
                    cmd.Parameters.AddWithValue("@PaymentStatus", InsRefund.PaymentStatus.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsRefund.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsRefund.BoatHouseName.ToString());


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
                        RefundRes InsCE = new RefundRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        RefundRes InsCE = new RefundRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    RefundRes InsCE = new RefundRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                RefundRes ConfRes = new RefundRes
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
        [Route("AllBoatHousesList")]
        public IHttpActionResult AllBoatHouses([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                string sQuery = string.Empty;
                if (PinDet.Input1 == "Sadmin" || PinDet.Input1.ToUpper() == "SADMIN")
                {
                    sQuery = "SELECT BoatHouseId , BoatHouseName FROM BHMaster WHERE ActiveStatus IN ('A') AND BoatHouseId NOT IN ('72');";
                }
                else
                {
                    sQuery = "SELECT BoatHouseId , BoatHouseName FROM BHMaster WHERE ActiveStatus IN ('A')";
                }
                List<getBoatHouseMaster> li = new List<getBoatHouseMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Table");
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
            catch (Exception ex)
            {
                getBoatHouseMasterString ConfRes = new getBoatHouseMasterString
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

        /*** Chalan Register **/
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="RptChallan"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlUserName")]
        public IHttpActionResult ddlUserName([FromBody] Challan RptChallan)
        {
            try
            {
                string sCondtion = string.Empty;

                if (RptChallan.BoatHouseId != "")
                {
                    sCondtion = " AND BranchId=@BranchId ";
                }

                List<EmployeeMaster> li = new List<EmployeeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserId,EmpFirstName+' '+EmpLastName AS 'UserName' FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster  "
                    + " WHERE ActiveStatus='A' " + sCondtion, con);

                cmd.Parameters.Add(new SqlParameter("@BranchId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BranchId"].Value = RptChallan.BoatHouseId;

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
            catch (Exception ex)
            {
                EmployeeMasterRes ConfRes = new EmployeeMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("AbstractChalanRegister")]
        public IHttpActionResult AbstractChalanRegister([FromBody] CommonAPIMethod drc)
        {
            try
            {
                if (drc.QueryType != null)
                {
                    SqlCommand cmd = new SqlCommand("ViewAbstractChalanRegister", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", drc.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", drc.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@ChalanDate", DateTime.Parse(drc.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@UserId", drc.UserId.Trim());
                    cmd.Parameters.AddWithValue("@UserRole", drc.Input1.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", drc.ServiceType);
                    cmd.Parameters.AddWithValue("@PaymentType", drc.Input2.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Table");
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
                return Ok(ex.ToString());
            }
        }

        /*** DAY WISE REVENUE COMPARISON ***/
        [HttpPost]
        [AllowAnonymous]
        [Route("DayWiseRevenueComparison")]
        public IHttpActionResult DayWiseRevenueComparison([FromBody] CommonAPIMethod drc)
        {
            try
            {
                if (drc.QueryType != null)
                {
                    SqlCommand cmd = new SqlCommand("ViewDayWiseRevenueComparison", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", drc.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", drc.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", drc.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", drc.BoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", drc.BoatSeaterId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(drc.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(drc.ToDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatStatus", drc.BoatStatus.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Table");
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
                return Ok(ex.ToString());
            }

        }

        //*** Service Wise Collection ***/
        /// <summary>
        /// Modified By : Silambarasu
        /// Modified Date : 27 OCT 2021,16 Nov
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Boating"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptServiceWisePayment")]
        public IHttpActionResult RptServiceWisePayment([FromBody] BoatingReport Boating)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(PaidDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND BoatHouseId = @BoatHouseId "
                       + " AND PaymentStatus = 'P' ";

                string conditions1 = " WHERE  CAST(SettlementDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND BoatHouseId = @BoatHouseId "
                       + " AND ActiveStatus = 'A' ";

                string conditions2 = " WHERE  CAST(DepRefundDate AS DATE) "
                      + " BETWEEN @BookingDate "
                      + " AND @BookingDate "
                      + " AND BoatHouseId = @BoatHouseId "
                      + " AND DepRefundStatus = 'Y' ";

                if (Boating.BoatTypeId != "0")
                {
                    conditions += " AND BoatTypeId = @BoatTypeId ";
                    conditions2 += " AND BoatTypeId = @BoatTypeId ";
                }
                if (Boating.CreatedBy != "0")
                {
                    conditions += " AND PaidBy = @CreatedBy ";
                    conditions1 += " AND CreatedBy = @CreatedBy ";
                    conditions2 += " AND DepRefundBy = @CreatedBy ";
                }

                squery = "  SELECT PaidDate, Account AS 'Particulars', SUM(TotalCount) AS 'Count', SUM(Amount) AS 'Amount' "
                          + " FROM"
                          + " ("
                          + " SELECT CONVERT(NVARCHAR(20), CAST(PaidDate AS DATE), 103) AS 'PaidDate', COUNT(UniqueId) AS 'TotalCount', "
                          + " 'Refund Counter' AS 'Account', "
                          + " ISNULL(CAST(SUM(PaidAmount) AS DECIMAL(18, 2)), 0) AS 'Amount' "
                         + " FROM  BH_RefundCounter "
                         + " " + conditions + " "
                         + "GROUP BY UniqueId, CAST(PaidDate AS DATE) "
                         + ") "
                         + "AS A "
                         + "GROUP BY PaidDate, Account "
                         + "UNION ALL "
                        + " SELECT PaidDate, Account AS 'Particulars', SUM(TotalCount) AS 'Count', SUM(Amount) AS 'Amount' "
                         + "FROM "
                         + "( "
                         + " SELECT CONVERT(NVARCHAR(20), CAST(SettlementDate AS DATE), 103) AS 'PaidDate', COUNT(SettlementId) AS 'TotalCount', "
                         + " 'Rower Settlement' AS 'Account', "
                         + " ISNULL(CAST(SUM(SettlementAmount) AS DECIMAL(18, 2)), 0) AS 'Amount' "
                         + " FROM RowerSettlement "
                        + " " + conditions1 + " "
                        + "GROUP BY CAST(SettlementDate AS DATE) "
                        + ") "
                        + "AS B "
                        + "GROUP BY PaidDate, Account "
                        + "UNION ALL "
                        + " SELECT PaidDate, Account AS 'Particulars', SUM(TotalCount) AS 'Count', SUM(Amount) AS 'Amount' "
                         + "FROM "
                         + "( "
                         + "SELECT CONVERT(NVARCHAR(20), CAST(DepRefundDate AS DATE), 103) AS 'PaidDate', COUNT(BookingId) AS 'TotalCount', "
                         + "'Deposit Refund' AS 'Account', "
                         + " ISNULL(CAST(SUM(DepRefundAmount) AS DECIMAL(18, 2)), 0) AS 'Amount' "
                         + "FROM BookingDtl "
                        + " " + conditions2 + " "
                        + "GROUP BY CAST(DepRefundDate AS DATE)  "
                        + ") "
                        + "AS C "
                        + "GROUP BY PaidDate, Account ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = Boating.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = Boating.BoatTypeId.Trim();
                cmd.Parameters["@CreatedBy"].Value = Boating.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Boating.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Boating"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractServiceWiseCollection")]
        public IHttpActionResult BoatwiseTripAbs([FromBody] BoatingReport Boating)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                string conditions1 = " WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                if (Boating.BoatTypeId != "0")
                {
                    conditions += " AND B.BoatTypeId = @BoatTypeId ";
                    conditions1 += " AND B.BoatTypeId = @BoatTypeId ";
                }
                if (Boating.PaymentType != "0")
                {
                    conditions += " AND C.PaymentType = @PaymentType ";
                    conditions1 += " AND C.PaymentType = @PaymentType ";
                }
                if (Boating.CreatedBy != "0")
                {
                    conditions += "AND C.CreatedBy = @CreatedBy ";
                    conditions1 += "AND C.CreatedBy = @CreatedBy";
                }

                squery = " DECLARE @TAX DECIMAL(18, 2) "
                        + "SET @TAX = (SELECT TOP 1 TaxPercentage AS 'TAX' FROM TaxMaster WHERE ServiceName = '1' AND BoatHouseId = @BoatHouseId )  "
                        + "SELECT BookingDate, SUM(TotalCount)AS 'Count', Account AS 'Particulars', SUM(Amount)AS 'Amount', "
                        + "SUM(CGST) AS 'CGST',  "
                        + "SUM(SGST) AS 'SGST',  "
                        + "SUM(TotalAmount)AS 'TotalAmount' "
                        + "FROM "
                        + "(SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate', "
                        + " COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                        + "   ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                        + "  ((ISNULL(SUM(B.CGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'CGST', "
                        + "   ((ISNULL(SUM(B.SGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'SGST', "
                        + "   ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) + "
                        + "  ((ISNULL(SUM(B.CGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) + "
                        + "   ((ISNULL(SUM(B.SGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) + "
                        + "   ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) + "
                        + "   ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'TotalAmount' "
                        + "   FROM BookingDtl AS B   "
                        + "INNER JOIN  BookingHdr AS C ON B.BookingId = C.BookingId "
                        + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                        + " " + conditions + " "
                        + " GROUP BY B.BookingId, CAST(C.BookingDate AS DATE))  AS A "
                        + "GROUP BY BookingDate, Account "
                        + " UNION ALL "
                        + "SELECT BookingDate, 0 AS 'Count', Account, SUM(Amount), SUM(CGST), SUM(SGST), SUM(TotalAmount) "
                        + "FROM "
                        + " ( "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate', "
                        + "COUNT(B.BookingId) AS 'TotalCount', 'Rower Charge' AS 'Account', "
                        + " ISNULL(CAST(SUM(B.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                        + " ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'CGST', "
                        + "ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'SGST', "
                        + " ISNULL(CAST(SUM(B.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                        + " FROM  BookingDtl AS B "
                        + "INNER JOIN  BookingHdr AS C ON B.BookingId = C.BookingId "
                        + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                        + " " + conditions1 + " "
                        + "GROUP BY CAST(C.BookingDate AS DATE) "
                        + ") "
                        + " AS B "
                        + " GROUP BY BookingDate, Account ";



                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = Boating.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = Boating.BoatTypeId.Trim();
                cmd.Parameters["@PaymentType"].Value = Boating.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Boating.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Boating.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="UserBased"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptUserBasedServiceListPrint")]
        public IHttpActionResult UserBasedServicePrint([FromBody] BoatingReport UserBased)
        {
            try
            {
                string squery = string.Empty;
                string sCondition = string.Empty;

                if (UserBased.BoatHouseId != "" && UserBased.FromDate != "" && UserBased.ToDate != ""
                    && UserBased.CreatedBy != "")
                {
                    sCondition = " WHERE CAST(B.BookingDate AS DATE) BETWEEN  @FromDate "
                            + " AND @ToDate "
                            + " AND B.Status = 'B'  AND A.BoatHouseId = @BoatHouseId "
                            + " AND B.BoatHouseId = @BoatHouseId "
                            + " AND C.BoatHouseId = @BoatHouseId "
                            + " AND D.BoatHouseId = @BoatHouseId  AND B.Createdby = @CreatedBy ";

                    if (UserBased.BoatTypeId != "0")
                    {
                        sCondition += " AND A.BoatTypeId = @BoatTypeId ";
                    }

                    squery = " SELECT CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate',  C.BoatType AS 'BoatType', "
                            + " D.SeaterType AS 'BoatSeater',  COUNT(A.BookingId) AS 'Count',  "
                            + " ISNULL(CAST(SUM(A.InitNetAmount) AS DECIMAL(18, 2)), 0) AS 'Amount'  FROM BookingDtl AS A "
                            + " INNER JOIN  BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                            + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                            + " AND B.BoatHouseId = C.BoatHouseId  INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId "
                            + " AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
                            + sCondition
                            + " GROUP BY CAST(B.BookingDate AS DATE), C.BoatType, D.SeaterType ";


                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = UserBased.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = UserBased.BoatTypeId.Trim();
                    cmd.Parameters["@CreatedBy"].Value = UserBased.CreatedBy.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(UserBased.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(UserBased.ToDate.Trim(), objEnglishDate);
                    List<BoatingReport> li = new List<BoatingReport>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatingReport UserBasedList = new BoatingReport();

                            UserBasedList.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            UserBasedList.BoatType = dt.Rows[i]["BoatType"].ToString();
                            UserBasedList.BoatSeater = dt.Rows[i]["BoatSeater"].ToString();
                            UserBasedList.Count = dt.Rows[i]["Count"].ToString();
                            UserBasedList.Amount = dt.Rows[i]["Amount"].ToString();

                            li.Add(UserBasedList);
                        }

                        BoatingReportList BoatRate = new BoatingReportList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        BoatingReportres BoatRate = new BoatingReportres
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    BoatingReportres Vehicle = new BoatingReportres
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
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="CuTotal"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UserCountTotal/BoatBooking")]
        public IHttpActionResult UserCountTotal([FromBody] BoatBooking CuTotal)
        {
            try
            {
                if (CuTotal.BoatHouseId != "" && CuTotal.Fromdate != "" && CuTotal.CreatedBy != "")
                {
                    con.Open();
                    string squery = string.Empty;
                    string squery2 = string.Empty;
                    string OtherserviceTotal = string.Empty;
                    squery = "SELECT UserId, UserName, Count, TotalAmount  as 'Total' "
                    + " FROM "
                    + "  (  "
                    + "    SELECT A.Createdby AS 'UserId', C.EmpFirstName + ' ' + C.EmpLastName AS 'UserName', "
                    + "      COUNT(*) as 'Count', sum(B.InitNetAmount) as 'TotalAmount' "
                    + "    FROM BookingHdr AS A INNER JOIN BookingDtl AS B ON B.BookingId = A.BookingId "
                    + "    INNER JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS C ON C.UserId = A.Createdby "
                    + "    WHERE A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND A.Createdby = @CreatedBy "
                    + "  AND CAST(A.BookingDate AS DATE) BETWEEN @Fromdate AND @Fromdate AND A.Status = 'B' "
                    + "     AND BookingMedia IN('DW', 'DA', 'DI') "
                    + "   GROUP BY A.Createdby, C.EmpFirstName, C.EmpLastName "
                    + "     UNION ALL "
                    + " SELECT 0 AS 'UserId', 'Online' AS 'UserName', "
                    + "    COUNT(*) AS 'Count', SUM(B.InitNetAmount) AS 'TotalAmount' "
                    + "  FROM BookingDtl AS B "
                    + "  INNER JOIN BookingHdr AS A ON B.BookingId = A.BookingId "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND A.Createdby = @Createdby "
                    + " AND CAST(A.BookingDate AS DATE) BETWEEN @Fromdate AND @Fromdate AND A.Status = 'B' "
                    + " AND A.BookingMedia IN('PW', 'PA', 'PI') "
                    + " ) AS A WHERE TotalAmount > 0  ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = CuTotal.BoatHouseId.Trim();
                    cmd.Parameters["@CreatedBy"].Value = CuTotal.CreatedBy.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(CuTotal.Fromdate.Trim(), objEnglishDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    squery2 = "select ISNULL(SUM(NetAmount),0) as 'TotalOthrs'  "
                       + " From BookingOthers where Createdby = @CreatedBy and BoatHouseId = @BoatHouseId and "
                       + " CAST(CreatedDate AS DATE)BETWEEN @Fromdate  AND  @Fromdate and BookingType = 'B' ";

                    SqlCommand cmd1 = new SqlCommand(squery2, con);
                    cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd1.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                    cmd1.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));

                    cmd1.Parameters["@BoatHouseId"].Value = CuTotal.BoatHouseId.Trim();
                    cmd1.Parameters["@CreatedBy"].Value = CuTotal.CreatedBy.Trim();
                    cmd1.Parameters["@FromDate"].Value = DateTime.Parse(CuTotal.Fromdate.Trim(), objEnglishDate);

                    SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        OtherserviceTotal = dt2.Rows[0]["TotalOthrs"].ToString();
                    }

                    con.Close();
                    List<BoatBooking> li = new List<BoatBooking>();
                    if (dt.Rows.Count > 0)
                    {
                        BoatBooking row = new BoatBooking();

                        if (OtherserviceTotal != "")
                        {
                            decimal TotalAmount = Convert.ToDecimal(row.Total = dt.Rows[0]["Total"].ToString()) + Convert.ToDecimal(OtherserviceTotal.ToString());
                            row.Total = TotalAmount.ToString();
                        }
                        else
                        {
                            row.Total = dt.Rows[0]["Total"].ToString();
                        }
                        row.Count = dt.Rows[0]["Count"].ToString();
                        li.Add(row);

                        BoatBookingList ConfList = new BoatBookingList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BoatBookingStr ConfRes = new BoatBookingStr
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatBookingStr Vehicle = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }

            catch (Exception ex)
            {
                BoatBookingStr ConfRes = new BoatBookingStr
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="CuTotal"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UserCountTotal/BookingOtherService")]
        public IHttpActionResult OtherserviceUserCountTotal([FromBody] BookingOtherServices CuTotal)
        {
            try
            {
                if (CuTotal.BoatHouseId != "" && CuTotal.FromDate != "" && CuTotal.CreatedBy != "")
                {
                    con.Open();
                    string squery = string.Empty;

                    squery = " SELECT ISNULL(SUM(NoOfItems),0) as 'OSCount', ISNULL(Sum(NetAmount),0) as 'Total' from BookingOthers  where  "
                     + "  Createdby = @CreatedBy and BoatHouseId = @BoatHouseId  and CAST(CreatedDate AS DATE)  "
                     + " BETWEEN @FromDate  AND  @FromDate AND BookingType = 'I' ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = CuTotal.BoatHouseId.Trim();
                    cmd.Parameters["@CreatedBy"].Value = CuTotal.CreatedBy.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(CuTotal.FromDate.Trim(), objEnglishDate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    List<BookingOtherServices> li = new List<BookingOtherServices>();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BookingOtherServices row = new BookingOtherServices();

                            row.Total = dt.Rows[i]["Total"].ToString();
                            row.Count = dt.Rows[i]["OSCount"].ToString();
                            li.Add(row);
                        }

                        BookingOtherServicesList ConfList = new BookingOtherServicesList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        BookingOtherServicesRes ConfRes = new BookingOtherServicesRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BookingOtherServicesRes Vehicle = new BookingOtherServicesRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }

            catch (Exception ex)
            {
                BookingOtherServicesRes ConfRes = new BookingOtherServicesRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="RowerList"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptRowerList")]
        public IHttpActionResult AllRowerList([FromBody] BoatingReport RowerList)
        {
            try
            {
                string squery = string.Empty;

                squery = " SELECT A.RowerName, B.MobileNo, B.Address1 + ' , ' + B.City AS 'Address' "
                       + " FROM RowerBoatAssignDetails AS A "
                       + " INNER JOIN RowerMaster AS B ON A.RowerId = B.RowerId AND A.BoatHouseId = B.BoatHouseId "
                       + " WHERE A.BoatTypeId = @BoatTypeId AND "
                       + " A.BoatHouseId = @BoatHouseId AND "
                       + " B.BoatHouseId = @BoatHouseId ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = RowerList.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = RowerList.BoatTypeId.Trim();

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
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified BY SUBA on 12-05-2022
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="TripWise"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptTripWise")]
        public IHttpActionResult BoatTripWise([FromBody] BoatingReport TripWise)
        {
            try
            {
                string squery = string.Empty;
                string squery1 = string.Empty;
                squery1 = "SELECT BookingId  FROM BookingDtl WHERE BoatHouseId = @BoatHouseId AND  CAST(BDate AS Date) = @BookingDate UNION  "
                          + " SELECT BookingId FROM BookingHdr Where BoatHouseId =@BoatHouseId AND CAST(BookingDate AS Date) = @BookingDate";
                SqlCommand cmd1 = new SqlCommand(squery1, con);
                cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd1.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));

                cmd1.Parameters["@BoatHouseId"].Value = TripWise.BoatHouseId.Trim();
                cmd1.Parameters["@BookingDate"].Value = DateTime.Parse(TripWise.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                con.Close();
                if (dt1.Rows.Count > 0)
                {
                    squery = " SELECT B.BoatType + ' ' + C.SeaterType AS 'Boat Name', E.BoatNum AS 'Boat No', "
                           + " ISNULL(COUNT(A.BookingId),0) AS 'No.of.Trips', "
                           + " ISNULL(CAST((SUM(A.ActualBoatCharge)) AS DECIMAL(18,2)),0)  AS 'Boat Charge', "
                           + " ISNULL(CAST((SUM(A.ActualRowerCharge)) AS DECIMAL(18, 2)), 0) AS 'Rower Charge', "
                           + " ISNULL(CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)) + CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)), 0) AS 'Tax' , "
                           + " ISNULL(CAST((SUM(A.ActualBoatCharge)) AS DECIMAL(18, 2)), 0) + ISNULL(CAST((SUM(A.ActualRowerCharge)) AS DECIMAL(18, 2)), 0) + "
                           + " ISNULL(CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)) + CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)), 0) AS 'Total' "
                           + " FROM BookingDtl AS A "
                           + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                           + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                           + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                           + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId "
                           + " AND E.BoatNum=A.ActualBoatNum "
                           + " WHERE A.BoathouseId = @BoatHouseId AND "
                           + " B.BoatHouseId = @BoatHouseId AND "
                           + " C.BoatHouseId = @BoatHouseId AND "
                           + " D.BoatHouseId = @BoatHouseId AND "
                           + " E.BoatHouseId = @BoatHouseId AND "
                           + " D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND "
                           + " CAST(D.BookingDate AS DATE) BETWEEN "
                           + " @BookingDate AND "
                           + " @BookingDate "
                           + " AND  A.BoatTypeId = @BoatTypeId  AND A.TripEndTime IS NOT NULL "
                           + " GROUP BY B.BoatType, C.SeaterType, E.BoatNum, E.BoatName ";
                }
                else
                {
                    squery = " SELECT B.BoatType + ' ' + C.SeaterType AS 'Boat Name', E.BoatNum AS 'Boat No', "
                           + " ISNULL(COUNT(A.BookingId),0) AS 'No.of.Trips', "
                           + " ISNULL(CAST((SUM(A.ActualBoatCharge)) AS DECIMAL(18,2)),0)  AS 'Boat Charge', "
                           + " ISNULL(CAST((SUM(A.ActualRowerCharge)) AS DECIMAL(18, 2)), 0) AS 'Rower Charge', "
                           + " ISNULL(CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)) + CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)), 0) AS 'Tax' , "
                           + " ISNULL(CAST((SUM(A.ActualBoatCharge)) AS DECIMAL(18, 2)), 0) + ISNULL(CAST((SUM(A.ActualRowerCharge)) AS DECIMAL(18, 2)), 0) + "
                           + " ISNULL(CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)) + CAST(SUM(A.CGSTActualTaxAmount) AS DECIMAL(18, 2)), 0) AS 'Total' "
                           + " FROM BookingDtlHistory AS A "
                           + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                           + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                           + " INNER JOIN BookingHdrHistory AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                           + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId "
                           + " AND E.BoatNum=A.ActualBoatNum "
                           + " WHERE A.BoathouseId = @BoatHouseId AND "
                           + " B.BoatHouseId = @BoatHouseId AND "
                           + " C.BoatHouseId = @BoatHouseId AND "
                           + " D.BoatHouseId = @BoatHouseId AND "
                           + " E.BoatHouseId = @BoatHouseId AND "
                           + " D.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND "
                           + " CAST(D.BookingDate AS DATE) BETWEEN "
                           + " @BookingDate AND "
                           + " @BookingDate "
                           + " AND  A.BoatTypeId = @BoatTypeId  AND A.TripEndTime IS NOT NULL "
                           + " GROUP BY B.BoatType, C.SeaterType, E.BoatNum, E.BoatName ";
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = TripWise.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = TripWise.BoatTypeId.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(TripWise.BookingDate.Trim(), objEnglishDate);
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
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By : Abhi.
        /// Modified Date : 03-Sep-2021.
        ///  Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="BookingIdList"></param>
        /// <returns></returns>
        /// Modified By: Brijin 
        /// Date : 12-05-2022
        [HttpPost]
        [AllowAnonymous]
        [Route("RptUserBasedBookingIdList")]
        public IHttpActionResult BookingIdList([FromBody] BoatingReport BookingIdList)
        {
            try
            {
                string squery = string.Empty;
                string sQuery1 = string.Empty;

                string conditions = " WHERE CAST(B.BookingDate AS DATE) BETWEEN "
                                  + " @BookingDate "
                                  + " AND @BookingDate "
                                  + " AND B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R')  "
                                  + " AND A.BoatHouseId = @BoatHouseId "
                                  + " AND B.BoatHouseId = @BoatHouseId "
                                  + " AND C.BoatHouseId = @BoatHouseId "
                                  + " AND D.BoatHouseId = @BoatHouseId ";

                if (BookingIdList.CreatedBy != "0")
                {
                    conditions += " AND B.Createdby = @CreatedBy ";
                }
                con.Open();
                sQuery1 = "SELECT BOOKINGID  FROM BookingDtl WHERE BoatHouseId = @BoatHouseId AND  CAST(BDate AS Date) = @BookingDate "
                    + " UNION SELECT BOOKINGID  FROM BookingHdr Where BoatHouseId =@BoatHouseId AND CAST(BookingDate AS Date) =@BookingDate ";

                SqlCommand cmd1 = new SqlCommand(sQuery1, con);
                cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd1.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd1.Parameters["@BoatHouseId"].Value = BookingIdList.BoatHouseId.Trim();
                cmd1.Parameters["@BookingDate"].Value = DateTime.Parse(BookingIdList.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                con.Close();
                if (dt1.Rows.Count > 0)
                {
                    squery = " SELECT B.UniqueId, B.CreatedBy, A.BookingId AS 'BookingId', "
                       + " CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate', "
                       + " ISNULL(CAST(SUM(A.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'BoatCharge', "
                       + " ISNULL(CAST(SUM(A.BoatDeposit) AS DECIMAL(18, 2)), 0) AS 'DepositCharge', "
                       + " ISNULL(CAST(SUM(A.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'RowerCharge', "
                       + " ISNULL(CAST(SUM(A.CGSTTaxAmount) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                       + " ISNULL(CAST(SUM(A.CGSTTaxAmount) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                       + " ISNULL(CAST(SUM(A.InitNetAmount) AS DECIMAL(18, 2)), 0) AS 'NetAmount', "
                       + " ISNULL(CAST(SUM(A.DepRefundAmount) AS DECIMAL(18, 2)), 0) AS 'RefundAmount' FROM BookingDtl AS A "
                       + " INNER JOIN  BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                       + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND B.BoatHouseId = C.BoatHouseId "
                       + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
                       + " " + conditions + ""
                       + " GROUP BY A.BookingId, CAST(B.BookingDate AS DATE), B.UniqueId, B.CreatedBy "
                       + " ORDER BY B.CreatedBy, B.UniqueId ASC ";
                }
                else
                {

                    squery = " SELECT B.UniqueId, B.CreatedBy, A.BookingId AS 'BookingId', "
                      + " CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate', "
                      + " ISNULL(CAST(SUM(A.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'BoatCharge', "
                      + " ISNULL(CAST(SUM(A.BoatDeposit) AS DECIMAL(18, 2)), 0) AS 'DepositCharge', "
                      + " ISNULL(CAST(SUM(A.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'RowerCharge', "
                      + " ISNULL(CAST(SUM(A.CGSTTaxAmount) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                      + " ISNULL(CAST(SUM(A.CGSTTaxAmount) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                      + " ISNULL(CAST(SUM(A.InitNetAmount) AS DECIMAL(18, 2)), 0) AS 'NetAmount', "
                      + " ISNULL(CAST(SUM(A.DepRefundAmount) AS DECIMAL(18, 2)), 0) AS 'RefundAmount' FROM BookingDtlHistory AS A "
                      + " INNER JOIN  BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                      + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND B.BoatHouseId = C.BoatHouseId "
                      + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
                      + " " + conditions + ""
                      + " GROUP BY A.BookingId, CAST(B.BookingDate AS DATE), B.UniqueId, B.CreatedBy "
                      + " ORDER BY B.CreatedBy, B.UniqueId ASC ";

                }

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = BookingIdList.BoatHouseId.Trim();
                cmd.Parameters["@CreatedBy"].Value = BookingIdList.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(BookingIdList.BookingDate.Trim(), objEnglishDate);
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
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UserName")]
        public IHttpActionResult UserNameService([FromBody] RefundCounter Username)
        {
            try
            {
                string squery = string.Empty;

                if (Username.BoatHouseId != ""
                    && Username.CreatedBy != "")
                {


                    squery = " Select C.EmpFirstName + ' ' + C.EmpLastName AS 'UserName',C.UserId as 'UserId', "
                        + " D.BoatType As 'BoatType',D.BoatTypeId as 'BoatTypeId',Sum(B.InitNetAmount) as 'AMOUNT' from BookingHdr as A "
                        + " inner Join BookingDtl as B on A.BoatHouseId = B.BoatHouseId and A.BookingId = B.BookingId "
                        + " inner Join "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster as C on C.BranchId = B.BoatHouseId and A.Createdby = C.UserId "
                        + " inner Join BoatTypes As D on B.BoatHouseId = D.BoatHouseId and B.BoatTypeId = D.BoatTypeId "
                        + " where A.BoatHouseId = @BoatHouseId and B.BoatDeposit IS NOT NULL  "
                        + " and Cast(A.BookingDate as Date) = @BookingDate "
                        + " and C.ActiveStatus = 'A'  and B.BoatHouseId = @BoatHouseId "
                        + " AND C.BranchId = @BoatHouseId and D.BoatHouseId = @BoatHouseId  "
                        + " AND B.BoatDeposit > 0 AND A.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') "
                        + " Group by C.EmpFirstName + ' ' + C.EmpLastName, C.UserId, D.BoatType,D.BoatTypeId ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@BoatHouseId"].Value = Username.BoatHouseId.Trim();
                    cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Username.BookingDate.Trim(), objEnglishDate);
                    List<RefundCounter> li = new List<RefundCounter>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RefundCounter UsernameList = new RefundCounter();
                            UsernameList.UserId = dt.Rows[i]["UserId"].ToString();
                            UsernameList.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            UsernameList.UserName = dt.Rows[i]["UserName"].ToString();

                            UsernameList.BoatType = dt.Rows[i]["BoatType"].ToString();
                            UsernameList.Amount = dt.Rows[i]["AMOUNT"].ToString();

                            li.Add(UsernameList);
                        }

                        RefundCounterList User = new RefundCounterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(User);
                    }
                    else
                    {
                        RefundCounterRes BoatRate = new RefundCounterRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(User);
                    }
                }
                else
                {
                    RefundCounterRes Vehicle = new RefundCounterRes
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
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023       
        /// Modified By Jaya Suriya A
        /// Modified Date 27-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="rower"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlRower/RowerSettle")]
        public IHttpActionResult getRowerSettle([FromBody] Rower rower)
        {
            try
            {
                string sQuery = string.Empty;
                if (rower.BoatHouseId != "")
                {
                    List<Rower> li = new List<Rower>();
                    con.Open();
                    sQuery = " SELECT RowerId, RowerName FROM RowerMaster WHERE BoatHouseId =@BoatHouseId AND ActiveStatus='A' "
                        + " AND RowerId IN(SELECT RowerId FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId "
                        + " AND CAST(B.BookingDate AS DATE) = @TripDate) "
                        + " ORDER BY RowerName ASC";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@TripDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@BoatHouseId"].Value = rower.BoatHouseId.Trim();
                    cmd.Parameters["@TripDate"].Value = DateTime.Parse(rower.TripDate.Trim(), objEnglishDate);
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
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetPaymentStatus")]
        public IHttpActionResult GetPaymentStatus([FromBody] UserProfile bHMstr)
        {
            try
            {
                string sQuery = string.Empty;


                if (bHMstr.UserId != "")
                {
                    List<UserProfile> li = new List<UserProfile>();

                    sQuery = " SELECT DISTINCT(UserId), PaymentStatus, BoatHouseId "
                              + " FROM "+ ConfigurationManager.AppSettings["BoatingDB"] +".dbo.BH_RefundCounter "
                              + " WHERE UserId = @UserId AND PaymentStatus = 'U' AND RequestStatus = 'R' "
                              + " AND cast(RequestedDate as Date) = CAST(GETDATE() AS DATE) ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@UserId"].Value = bHMstr.UserId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        UserProfile User1 = new UserProfile();

                        User1.UserId = dt.Rows[0]["UserId"].ToString();
                        User1.PaymentStatus = dt.Rows[0]["PaymentStatus"].ToString();
                        li.Add(User1);

                        UserProfileList ConfList = new UserProfileList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }
                    else
                    {
                        UserProfileRes ConfRes = new UserProfileRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    UserProfileRes Vehicle = new UserProfileRes
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
        /// Sillu 03 NOV 2021
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="rcr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RefundCashFromReport")]
        public IHttpActionResult RefundCashReport([FromBody] CashReport rcr)
        {
            try
            {
                if (rcr.BoatHouseId != "" || rcr.FromDate != "" || rcr.CashflowTypes != "")
                {
                    List<CashReport> li = new List<CashReport>();
                    con.Open();

                    string sQuery = string.Empty;

                    if (rcr.CashflowTypes == "CashFrom")
                    {
                        sQuery = " SELECT  bt.BoatType as 'Particulars', SUM(ISNULL(PaidAmount,0)) as 'Amount' FROM BH_RefundCounter AS rc"
                       + " inner join BoatTypes as bt on rc.BoatTypeId = bt.BoatTypeId and rc.BoatHouseId = bt.BoatHouseId"
                       + " WHERE rc.BoatHouseId = @BoatHouseId and cast(RequestedDate as date) = @FromDate and PaymentStatus = 'P'"
                       + " Group by bt.BoatType";
                    }
                    else if (rcr.CashflowTypes == "CashReceived")
                    {
                        sQuery = " SELECT  bt.BoatType as 'Particulars', SUM(ISNULL(PaidAmount,0)) as 'Amount' FROM BH_RefundCounter AS rc"
                       + " inner join BoatTypes as bt on rc.BoatTypeId = bt.BoatTypeId and rc.BoatHouseId = bt.BoatHouseId"
                       + " WHERE rc.BoatHouseId = @BoatHouseId and cast(RequestedDate as date) = @FromDate and PaymentStatus = 'P' and rc.RequestedBy = @RequestedBy "
                       + " Group by bt.BoatType";
                    }
                    else
                    {
                        sQuery = " select cast(bt.BoatType as varchar) + ' ' + bs.SeaterType as 'Particulars', SUM(ISNULL(DepRefundAmount, 0)) as Amount from BookingDtl as bdl"
                            + " inner join BoatTypes as bt on bdl.BoatTypeId = bt.BoatTypeId and bdl.BoatHouseId = bt.BoatHouseId"
                            + " inner join BoatSeat as bs on bdl.BoatSeaterId = bs.BoatSeaterId and bdl.BoatHouseId = bs.BoatHouseId"
                            + " where bdl.BoatHouseId = @BoatHouseId and cast(bdl.DepRefundDate  as date) = @FromDate and bdl.DepRefundStatus = 'Y'"
                            + " Group by bt.BoatType, bs.SeaterType";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@RequestedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@BoatHouseId"].Value = rcr.BoatHouseId.Trim();
                    cmd.Parameters["@RequestedBy"].Value = rcr.RequestedBy.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(rcr.FromDate.Trim(), objEnglishDate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CashReport cs = new CashReport();
                            cs.Particulars = dt.Rows[i]["Particulars"].ToString();
                            cs.Amount = dt.Rows[i]["Amount"].ToString();
                            li.Add(cs);
                        }

                        CashReportlst Cashrpt = new CashReportlst
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(Cashrpt);
                    }
                    else
                    {
                        CashReportStr Cashrpt = new CashReportStr
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(Cashrpt);
                    }
                }
                else
                {
                    CashReportStr Cashrpt = new CashReportStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Cashrpt);
                }
            }
            catch (Exception ex)
            {
                CashReportStr Cashrpt = new CashReportStr
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Cashrpt);
            }
        }
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query 
        /// </summary>
        /// <param name="dtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BookingDetailsCount")]
        public IHttpActionResult BookingDetailsCount([FromBody] BoatingReport dtl)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string sBookingMedia = string.Empty;

                if (dtl.CreatedBy != "0")
                {
                    conditions = " AND A.Createdby=@CreatedBy ";
                    sBookingMedia = " AND A.BookingMedia NOT IN ('PW', 'PA', 'PI')";
                }

                //squery = " SELECT SUM(NetAmount)-SUM(Deposit) AS 'Amount', SUM(Deposit) AS 'Deposit', SUM(NetAmount) AS 'NetAmount', SUM(Count) AS 'Count' FROM "
                //+ " ( "
                //+ " SELECT ISNULL(SUM(ISNULL(B.InitNetAmount, 0) - ISNULL(B.BoatDeposit, 0)), 0) AS 'Amount', ISNULL(SUM(ISNULL(B.BoatDeposit, 0)), 0) "
                //+ " AS 'Deposit', "
                //+ " ISNULL(SUM(ISNULL(B.InitNetAmount, 0)), 0) AS 'NetAmount', COUNT(*) AS 'Count' FROM BookingHdr AS A "
                //+ " INNER JOIN BookingDtl AS B ON A.BoatHouseId = B.BoatHouseId AND A.BookingId = B.BookingId "
                //+ " WHERE CAST(A.BookingDate AS DATE) = '" + DateTime.Parse(dtl.BookingDate.Trim(), objEnglishDate) + "' AND A.BoatHouseId = '" + dtl.BoatHouseId + "' AND B.BoatHouseId = '" + dtl.BoatHouseId + "' "
                //+ " AND A.Status = 'B' " + conditions + " " + sBookingMedia + ""

                //+ " UNION "
                //+ " SELECT SUM(ISNULL(B.ChargePerItem, 0)) AS 'Amount', '0' AS 'Deposit', "
                //+ " ISNULL(SUM(ISNULL(B.NetAmount, 0)), 0) AS 'NetAmount', 0 AS 'Count' FROM BookingHdr AS A "
                //+ " INNER JOIN BookingOthers AS B ON A.BoatHouseId = B.BoatHouseId AND A.BookingId = B.BookingId "
                //+ " WHERE CAST(A.BookingDate AS DATE) = '" + DateTime.Parse(dtl.BookingDate.Trim(), objEnglishDate) + "' AND A.BoatHouseId = '" + dtl.BoatHouseId + "' AND B.BoatHouseId = '" + dtl.BoatHouseId + "' "
                //+ " AND A.Status = 'B' " + conditions + "  AND B.BookingType = 'B' " + sBookingMedia + " "
                //+ " ) AS A";


                squery = " SELECT SUM(NetAmount)-SUM(Deposit) AS 'Amount', SUM(Deposit) AS 'Deposit', SUM(NetAmount) AS 'NetAmount', SUM(Count) AS 'Count' FROM "
               + " ( "
               + " SELECT ISNULL(SUM(ISNULL(B.InitNetAmount, 0) - ISNULL(B.BoatDeposit, 0)), 0) AS 'Amount', ISNULL(SUM(ISNULL(B.BoatDeposit, 0)), 0) "
               + " AS 'Deposit', "
               + " ISNULL(SUM(ISNULL(B.InitNetAmount, 0)), 0) AS 'NetAmount', COUNT(*) AS 'Count' FROM BookingHdr AS A "
               + " INNER JOIN BookingDtl AS B ON A.BoatHouseId = B.BoatHouseId AND A.BookingId = B.BookingId "
               + " WHERE CAST(A.BookingDate AS DATE) = @BookingDate AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId "
               + " AND A.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') " + conditions + " " + sBookingMedia + ""
               + " ) AS A";


                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters["@BoatHouseId"].Value = dtl.BoatHouseId.Trim();
                cmd.Parameters["@CreatedBy"].Value = dtl.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(dtl.BookingDate.Trim(), objEnglishDate);
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
            catch (Exception ex)
            {
                BoatingReportres ConfRes = new BoatingReportres
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="bHMstrAddChrg"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRateMstrAddChrg/BHId")]
        public IHttpActionResult getBoatRateMstrAddChrg([FromBody] BoatRateMaster bHMstrAddChrg)
        {
            try
            {
                if (bHMstrAddChrg.BoatHouseId != "")
                {
                    List<BoatRateMaster> li = new List<BoatRateMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT A.UniqueId, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, "
                                   + " A.Charges, A.Amount, A.GST, A.ActiveStatus FROM BoatRateMasterAdditionalCharges AS A "
                                   + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                                   + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                                   + " WHERE A.BoatHouseId = @BoatHouseId AND "
                                   + " B.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstrAddChrg.BoatHouseId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatRateMaster BoatRateMaster = new BoatRateMaster();

                            BoatRateMaster.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            BoatRateMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatRateMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            BoatRateMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatRateMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            BoatRateMaster.Charges = dt.Rows[i]["Charges"].ToString();
                            BoatRateMaster.Amount = dt.Rows[i]["Amount"].ToString();
                            BoatRateMaster.GST = dt.Rows[i]["GST"].ToString();

                            BoatRateMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            li.Add(BoatRateMaster);
                        }

                        BoatRateMasterList BoatRate = new BoatRateMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        BoatRateMasterString BoatRate = new BoatRateMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    BoatHouseMasterString Vehicle = new BoatHouseMasterString
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
        [Route("BoatRateMstrAddChrgs/Insert")]
        public IHttpActionResult BoatRateAddInsert([FromBody] BoatRateMaster BRMstrAddChrg)
        {
            try
            {
                if (BRMstrAddChrg.QueryType != "" && BRMstrAddChrg.BoatTypeId != "" && BRMstrAddChrg.BoatSeaterId != ""
                    && BRMstrAddChrg.Charges != "" && BRMstrAddChrg.Amount != "" && BRMstrAddChrg.GST != "" &&
                    BRMstrAddChrg.BoatHouseId != ""
                    && BRMstrAddChrg.BoatHouseName != "" && BRMstrAddChrg.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("BoatRateMstrAdditionalCharges", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", BRMstrAddChrg.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", BRMstrAddChrg.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", BRMstrAddChrg.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", BRMstrAddChrg.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@Charges", BRMstrAddChrg.Charges.Trim());
                    cmd.Parameters.AddWithValue("@Amount", BRMstrAddChrg.Amount.ToString());
                    cmd.Parameters.AddWithValue("@GST", BRMstrAddChrg.GST.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", BRMstrAddChrg.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", BRMstrAddChrg.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", BRMstrAddChrg.Createdby.Trim());

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
                        ConfigurationMasterRes ConMstr = new ConfigurationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        ConfigurationMasterRes ConMstr = new ConfigurationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    ConfigurationMasterRes Vehicle = new ConfigurationMasterRes
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
        [Route("BookingCancel")]
        public IHttpActionResult BookingCancel([FromBody] CancelReschedMstr InsCityMap)
        {
            try
            {
                if (InsCityMap.QueryType != "" && InsCityMap.BookingId != "" && InsCityMap.BoatHouseId != ""
                    && InsCityMap.BookingPin != "" && InsCityMap.BoatReferenceNo != ""
                    && InsCityMap.BoatTypeId != "" && InsCityMap.BoatSeaterId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("BookingCancel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", InsCityMap.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", InsCityMap.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsCityMap.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BookingPin", InsCityMap.BookingPin.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", InsCityMap.BoatReferenceNo.Trim());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsCityMap.BoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", InsCityMap.BoatSeaterId.Trim());

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
                        CityMappingRes ConMstr = new CityMappingRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        CityMappingRes ConMstr = new CityMappingRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    CityMappingRes Vehicle = new CityMappingRes
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
        [Route("BoatBooking")]
        public IHttpActionResult GetBoatBooking([FromBody] BoatTicketDtl Btl)
        {
            try
            {
                List<BoatTicketDtl> li = new List<BoatTicketDtl>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "BookingCount");
                cmd.Parameters.AddWithValue("@BoatHouseId", Btl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Btl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Btl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Btl.UserId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTicketDtl bt = new BoatTicketDtl();

                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.ActualBillAmount = dt.Rows[i]["ActualBillAmount"].ToString();
                        bt.OtherServiceAmount = dt.Rows[i]["OtherServiceAmount"].ToString();
                        bt.BoatType = dt.Rows[i]["BoatType"].ToString();
                        bt.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                        bt.BookingType = dt.Rows[i]["BookingType"].ToString();

                        li.Add(bt);
                    }

                    BoatTicketDtllist Bdl = new BoatTicketDtllist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Bdl);
                }

                else
                {
                    BoatTicketDtlStr FBRes = new BoatTicketDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(FBRes);
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

        //Info Display
        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="BoatDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatListDisplay/BoatType")]
        public IHttpActionResult BoatListDeparture([FromBody] BoatBooking BoatDtl)
        {
            try
            {
                List<BoatBooking> li = new List<BoatBooking>();
                string squery = string.Empty;

                squery = "  select DISTINCT(BDL.BoatTypeId),BDL.BoatHouseName,BDL.BoatHouseId,  "
                         + " (select BoatType from BoatTypes BTY where BTY.BoatTypeId = BDL.BoatTypeId and "
                         + " BTY.BoatHouseId = BDL.BoatHouseId)  BoatType "
                         + " from BookingDtl BDL inner join BookingHdr BHD on BDL.BookingId = BHD.BookingId and "
                         + " BDL.BoatHouseId = BHD.BoatHouseId where BDL.BoatHouseId = @BoatHouseId "
                         + " and cast(BHD.BookingDate  AS DATE) = CAST(GETDATE() as DATE) AND BDL.TripStartTime IS  NULL";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = BoatDtl.BoatHouseId.Trim();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatBooking ts = new BoatBooking();


                        ts.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        ts.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ts.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ts.BoatTypeName = dt.Rows[i]["BoatType"].ToString();

                        li.Add(ts);
                    }
                    BoatBookingList ItemMasters = new BoatBookingList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ItemMasters);
                }
                else
                {
                    BoatBookingStr ItemMasters1 = new BoatBookingStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
                }

            }
            catch (Exception ex)
            {
                BoatBookingStr ConfRes = new BoatBookingStr
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="BoatDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatListDisplay/Departure/TypeId")]
        public IHttpActionResult BoatListTypeId([FromBody] BoatBooking BoatDtl)
        {
            try
            {
                List<BoatBooking> li = new List<BoatBooking>();
                string squery = string.Empty;
                squery = " select top 10 BDL.BoatSeaterId,BDL.BoatTypeId,BDL.BoatHouseName,BDL.BoatHouseId,  "
                       + " (select BoatType from BoatTypes BTY where BTY.BoatTypeId = BDL.BoatTypeId and "
                       + " BTY.BoatHouseId = BDL.BoatHouseId)  BoatType,  "
                       + " (select SeaterType from BoatSeat BS  where BS.BoatSeaterId = BDL.BoatSeaterId and "
                       + " BS.BoatHouseId = BDL.BoatHouseId) BoatSeater , BDL.BookingPin , "
                       + "  BDL.BookingPin ,BDL.ExpectedTime ,BHD.PremiumStatus ,BDL.BookingId "
                       + " from BookingDtl BDL inner join BookingHdr BHD on BDL.BookingId = BHD.BookingId and "
                       + " BDL.BoatHouseId = BHD.BoatHouseId where BDL.BoatHouseId = @BoatHouseId AND BHD.PremiumStatus = @PremiumStatus "
                       + " and cast(BHD.BookingDate  AS DATE) = CAST(GETDATE() as DATE )   AND BDL.TripStartTime IS  NULL ANd BDL.BoatTypeId =@BoatTypeId  "
                       + " ORDER BY BDL.BoatTypeId";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PremiumStatus", System.Data.SqlDbType.Char, 1));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = BoatDtl.BoatHouseId.Trim();
                cmd.Parameters["@PremiumStatus"].Value = BoatDtl.PremiumStatus.Trim();
                cmd.Parameters["@BoatTypeId"].Value = BoatDtl.BoatTypeId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatBooking ts = new BoatBooking();

                        ts.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        ts.BookingId = dt.Rows[i]["BookingId"].ToString();
                        ts.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        ts.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ts.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ts.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                        ts.BoatSeaterName = dt.Rows[i]["BoatSeater"].ToString();
                        ts.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                        ts.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                        ts.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                        li.Add(ts);
                    }
                    BoatBookingList ItemMasters = new BoatBookingList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ItemMasters);
                }
                else
                {
                    BoatBookingStr ItemMasters1 = new BoatBookingStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
                }

            }
            catch (Exception ex)
            {
                BoatBookingStr ConfRes = new BoatBookingStr
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified By Jaya Suriya A
        /// Modified Date 27-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="dtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("getUserName")]
        public IHttpActionResult getUserName([FromBody] BoatTicketDtl dtl)
        {
            try
            {
                string squery = string.Empty;
                List<BoatTicketDtl> li = new List<BoatTicketDtl>();

                squery = "select EmpFirstName +' '+ EmpLastName as 'EmpName' from "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster where UserId=@UserId AND BranchId =@BoatHouseId";
                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = dtl.BoatHouseId.Trim();
                cmd.Parameters["@UserId"].Value = dtl.UserId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                con.Close();
                if (ds.Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        BoatTicketDtl ShowTaxMaster = new BoatTicketDtl();
                        ShowTaxMaster.UserName = ds.Rows[i]["EmpName"].ToString();
                        li.Add(ShowTaxMaster);
                    }

                    BoatTicketDtllist ConfList = new BoatTicketDtllist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    BoatTicketDtlStr ConfRes = new BoatTicketDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified By Abhinaya K
        /// Modified Date 26-Apr-2023
        /// Modified Into Parameterised Query  
        /// </summary>
        /// <param name="Btl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ReprintReason")]
        public IHttpActionResult GetReprintReason([FromBody] BoatTicketDtl Btl)
        {
            try
            {
                string sQuery = string.Empty;
                sQuery = "INSERT INTO ReprintReason (BoatHouseId, BoatHouseName, UserId, UserName, ServiceType, BookingId, Reason, CreatedBy,CreatedDate)"
                       + " VALUES(@BoatHouseId,@BoatHouseName,@UserId, "
                       + " @UserName,@ServiceType,@BookingId,@Reason,@CreatedBy,GETDATE())";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseName", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Reason", System.Data.SqlDbType.NVarChar, 250));
                cmd.Parameters.Add(new SqlParameter("@ServiceType", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = Btl.BoatHouseId.Trim();
                cmd.Parameters["@BoatHouseName"].Value = Btl.BoatHouseName.Trim();
                cmd.Parameters["@UserId"].Value = Btl.UserId.Trim();
                cmd.Parameters["@UserName"].Value = Btl.UserName.Trim();
                cmd.Parameters["@Reason"].Value = Btl.Reason.Trim();
                cmd.Parameters["@BookingId"].Value = Btl.BookingId.Trim();
                cmd.Parameters["@ServiceType"].Value = Btl.ServiceType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Btl.CreatedBy.Trim();
                con.Open();
                int No = cmd.ExecuteNonQuery();
                con.Close();

                if (No > 0)
                {
                    BoatTicketDtlStr ConfList = new BoatTicketDtlStr
                    {
                        Response = "Inserted Successfully",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    BoatTicketDtlStr ConfRes = new BoatTicketDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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
        [Route("BoatRunningStatus")]
        public IHttpActionResult BoatRunningStatus([FromBody] BoatBooking BRS)
        {
            try
            {
                if (BRS.QueryType != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ViewBoatRunningStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", BRS.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", BRS.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", BRS.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", BRS.BoatSeaterId.ToString());

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    return Ok(ds);
                }
                else
                {
                    BoatBookingStr InsCE = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("BookingClosedDetails")]
        public IHttpActionResult BookingClosedDetails([FromBody] ServiceWiseReportHistory ServiceWise)
        {
            try
            {
                if (ServiceWise.BookingDate != "" && ServiceWise.BoatHouseId != "" && ServiceWise.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ServicewiseCollectionReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", ServiceWise.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@UserId", ServiceWise.UserId.Trim());
                    cmd.Parameters.AddWithValue("@UserName", ServiceWise.UserName.Trim());
                    cmd.Parameters.AddWithValue("@ServiceId", ServiceWise.ServiceId.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", ServiceWise.Services.Trim());
                    cmd.Parameters.AddWithValue("@CategoryId", ServiceWise.CategoryId.Trim());
                    cmd.Parameters.AddWithValue("@CategoryName", ServiceWise.Category.Trim());
                    cmd.Parameters.AddWithValue("@TypeId", ServiceWise.TypeId.Trim());
                    cmd.Parameters.AddWithValue("@TypeName", ServiceWise.Types.Trim());
                    cmd.Parameters.AddWithValue("@PaymentTypeId", ServiceWise.PaymentTypeId.Trim());
                    cmd.Parameters.AddWithValue("@PaymentType", ServiceWise.PaymentType.Trim());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(ServiceWise.BookingDate, objEnglishDate));
                    cmd.Parameters.AddWithValue("@ServiceTotal", ServiceWise.ServiceTotal.Trim());
                    cmd.Parameters.AddWithValue("@ReferenceId", ServiceWise.ReferenceId.Trim());
                    cmd.Parameters.AddWithValue("@Denomination", ServiceWise.Denomination.Trim());
                    cmd.Parameters.AddWithValue("@DenominationCount", ServiceWise.DenominationCount.Trim());
                    cmd.Parameters.AddWithValue("@DenominationAmount", ServiceWise.DenominationAmount.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", ServiceWise.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", ServiceWise.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", ServiceWise.CreatedBy.Trim());



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
                        ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Report);
                    }
                    else
                    {
                        ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Report);
                    }
                }
                else
                {
                    ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Report);
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
        [Route("BookingClosedService")]
        public IHttpActionResult BookingClosedService([FromBody] ServiceWiseReportHistory ServiceWise)
        {
            try
            {
                string squery = string.Empty;
                string condition = string.Empty;



                squery = " SELECT UniqueId,UserId,UserName,ServiceId,ServiceName,BookingDate "
                         + " FROM BookingClosedDetails WHERE UserId =  @UserId  "
                         + " and BoatHouseId =  @BoatHouseId  "
                         + " and ServiceId =  @ServiceId "
                         + " and BookingDate = cast(Getdate() as date) ";


                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@ServiceId"].Value = ServiceWise.ServiceId.Trim();
                cmd.Parameters["@UserId"].Value = ServiceWise.UserId.Trim();
                cmd.Parameters["@BoatHouseId"].Value = ServiceWise.BoatHouseId.Trim();
                List<ServiceWiseReportHistory> li = new List<ServiceWiseReportHistory>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServiceWiseReportHistory OthDtl = new ServiceWiseReportHistory();

                        OthDtl.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        OthDtl.UserId = dt.Rows[i]["UserId"].ToString();
                        OthDtl.UserName = dt.Rows[i]["Username"].ToString();
                        OthDtl.Services = dt.Rows[i]["ServiceId"].ToString();
                        OthDtl.Category = dt.Rows[i]["ServiceName"].ToString();
                        OthDtl.Types = dt.Rows[i]["BookingDate"].ToString();

                        li.Add(OthDtl);
                    }

                    ServiceWiseReportHistoryList BoatRate = new ServiceWiseReportHistoryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }
                else
                {
                    ServiceWiseReportHistoryRes BoatRate = new ServiceWiseReportHistoryRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatRate);
                }

            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("ServiceWiseReportHistory")]
        public IHttpActionResult ServiceWiseReportHistory([FromBody] ServiceWiseReportHistory ServiceWise)
        {
            try
            {
                if (ServiceWise.BookingDate != "" && ServiceWise.BoatHouseId != "" && ServiceWise.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ServicewiseCollectionReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", ServiceWise.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@UserId", ServiceWise.UserId.Trim());
                    cmd.Parameters.AddWithValue("@UserName", ServiceWise.UserName.Trim());
                    cmd.Parameters.AddWithValue("@ServiceId", ServiceWise.ServiceId.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", ServiceWise.Services.Trim());
                    cmd.Parameters.AddWithValue("@CategoryId", ServiceWise.CategoryId.Trim());
                    cmd.Parameters.AddWithValue("@CategoryName", ServiceWise.Category.Trim());
                    cmd.Parameters.AddWithValue("@TypeId", ServiceWise.TypeId.Trim());
                    cmd.Parameters.AddWithValue("@TypeName", ServiceWise.Types.Trim());
                    cmd.Parameters.AddWithValue("@PaymentTypeId", ServiceWise.PaymentTypeId.Trim());
                    cmd.Parameters.AddWithValue("@PaymentType", ServiceWise.PaymentType.Trim());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(ServiceWise.BookingDate, objEnglishDate));
                    cmd.Parameters.AddWithValue("@ServiceTotal", ServiceWise.ServiceTotal.Trim());
                    cmd.Parameters.AddWithValue("@ReferenceId", ServiceWise.ReferenceId.Trim());
                    cmd.Parameters.AddWithValue("@Denomination", ServiceWise.Denomination.Trim());
                    cmd.Parameters.AddWithValue("@DenominationCount", ServiceWise.DenominationCount.Trim());
                    cmd.Parameters.AddWithValue("@DenominationAmount", ServiceWise.DenominationAmount.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", ServiceWise.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", ServiceWise.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", ServiceWise.CreatedBy.Trim());



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
                        ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Report);
                    }
                    else
                    {
                        ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Report);
                    }
                }
                else
                {
                    ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Report);
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
        [Route("DenominationHistory")]
        public IHttpActionResult Denomination([FromBody] ServiceWiseReportHistory ServiceWise)
        {
            try
            {
                if (ServiceWise.BookingDate != "" && ServiceWise.BoatHouseId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ServicewiseCollectionReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", ServiceWise.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ReferenceId", ServiceWise.ReferenceId.Trim());
                    cmd.Parameters.AddWithValue("@Denomination", ServiceWise.Denomination.Trim());
                    cmd.Parameters.AddWithValue("@DenominationCount", ServiceWise.DenominationCount.Trim());
                    cmd.Parameters.AddWithValue("@DenominationAmount", ServiceWise.DenominationAmount.Trim());
                    cmd.Parameters.AddWithValue("@UserId", ServiceWise.UserId.Trim());
                    cmd.Parameters.AddWithValue("@UserName", ServiceWise.UserName.Trim());
                    cmd.Parameters.AddWithValue("@ServiceId", ServiceWise.ServiceId.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", ServiceWise.Services.Trim());
                    cmd.Parameters.AddWithValue("@CategoryId", ServiceWise.CategoryId.Trim());
                    cmd.Parameters.AddWithValue("@CategoryName", ServiceWise.Category.Trim());
                    cmd.Parameters.AddWithValue("@TypeId", ServiceWise.TypeId.Trim());
                    cmd.Parameters.AddWithValue("@TypeName", ServiceWise.Types.Trim());
                    cmd.Parameters.AddWithValue("@PaymentTypeId", ServiceWise.PaymentTypeId.Trim());
                    cmd.Parameters.AddWithValue("@PaymentType", ServiceWise.PaymentType.Trim());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(ServiceWise.BookingDate, objEnglishDate));
                    cmd.Parameters.AddWithValue("@ServiceTotal", ServiceWise.ServiceTotal.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", ServiceWise.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", ServiceWise.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", ServiceWise.CreatedBy.Trim());


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
                        ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Report);
                    }
                    else
                    {
                        ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(Report);
                    }
                }
                else
                {
                    ServiceWiseReportHistoryRes Report = new ServiceWiseReportHistoryRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Report);
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
        [Route("GetUniqueIdServiceWise")]
        public IHttpActionResult GetUniqueId([FromBody] ServiceWiseReportHistory ServiceWise)
        {
            try
            {
                string squery = string.Empty;
                string condition = string.Empty;

                condition = " WHERE BookingDate = @BookingDate "
                          + " AND UserId = @UserId AND BoatHouseId = @BoatHouseId ";


                if (ServiceWise.ServiceId != "0")
                {
                    condition += " AND ServiceId = @ServiceId ";
                }


                if (ServiceWise.CategoryId != "0")
                {
                    condition += " AND CategoryId = @CategoryId ";
                }
                if (ServiceWise.CategoryId == "0")
                {
                    condition += " AND CategoryId = @CategoryId ";
                }

                if (ServiceWise.TypeId != "0")
                {
                    condition += " AND TypeId =@TypeId ";
                }
                if (ServiceWise.TypeId == "0")
                {
                    condition += " AND TypeId =@TypeId ";
                }


                if (ServiceWise.PaymentTypeId != "0")
                {
                    condition += " AND PaymentTypeId =@PaymentTypeId ";
                }
                if (ServiceWise.PaymentTypeId == "0")
                {
                    condition += " AND PaymentTypeId =@PaymentTypeId ";
                }
                squery = "SELECT UniqueId FROM ServiceWiseHistory " + condition + " ";


                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@TypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(ServiceWise.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@PaymentTypeId"].Value = ServiceWise.PaymentTypeId.Trim();
                cmd.Parameters["@TypeId"].Value = ServiceWise.TypeId.Trim();
                cmd.Parameters["@CategoryId"].Value = ServiceWise.CategoryId.Trim();
                cmd.Parameters["@ServiceId"].Value = ServiceWise.ServiceId.Trim();
                cmd.Parameters["@UserId"].Value = ServiceWise.UserId.Trim();
                cmd.Parameters["@BoatHouseId"].Value = ServiceWise.BoatHouseId.Trim();

                List<ServiceWiseReportHistory> li = new List<ServiceWiseReportHistory>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServiceWiseReportHistory OthDtl = new ServiceWiseReportHistory();

                        OthDtl.ReferenceId = dt.Rows[i]["UniqueId"].ToString();

                        li.Add(OthDtl);
                    }

                    ServiceWiseReportHistoryList BoatRate = new ServiceWiseReportHistoryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }
                else
                {
                    ServiceWiseReportHistoryRes BoatRate = new ServiceWiseReportHistoryRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatRate);
                }

            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("ServiceWiseGrid")]
        public IHttpActionResult ServiceWiseGrid([FromBody] ServiceWiseReportHistory ServiceWise)
        {
            try
            {
                string squery = string.Empty;
                string condition = string.Empty;

                condition = " WHERE BookingDate BETWEEN @BookingDate AND @ToDate "
                          + " AND UserId = @UserId AND BoatHouseId = @BoatHouseId ";


                if (ServiceWise.ServiceId != "0")
                {
                    condition += " AND ServiceId = @ServiceId ";
                }


                if (ServiceWise.CategoryId != "0")
                {
                    condition += " AND CategoryId = @CategoryId ";
                }
                if (ServiceWise.CategoryId == "0")
                {
                    condition += " AND CategoryId = @CategoryId ";
                }

                if (ServiceWise.TypeId != "0")
                {
                    condition += " AND TypeId =@TypeId ";
                }
                if (ServiceWise.TypeId == "0")
                {
                    condition += " AND TypeId =@TypeId ";
                }


                if (ServiceWise.PaymentTypeId != "0")
                {
                    condition += " AND PaymentTypeId =@PaymentTypeId ";
                }
                if (ServiceWise.PaymentTypeId == "0")
                {
                    condition += " AND PaymentTypeId =@PaymentTypeId ";
                }
                squery = " SELECT UniqueId,UserId,Username,ServiceType,CategoryName,TypeName,PaymentType, "
                       + " CONVERT(NVARCHAR,BookingDate,103) AS 'BookingDate',ServiceTotal FROM ServiceWiseHistory " + condition + " ORDER BY BookingDate DESC";


                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@TypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(ServiceWise.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(ServiceWise.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@PaymentTypeId"].Value = ServiceWise.PaymentTypeId.Trim();
                cmd.Parameters["@TypeId"].Value = ServiceWise.TypeId.Trim();
                cmd.Parameters["@CategoryId"].Value = ServiceWise.CategoryId.Trim();
                cmd.Parameters["@ServiceId"].Value = ServiceWise.ServiceId.Trim();
                cmd.Parameters["@UserId"].Value = ServiceWise.UserId.Trim();
                cmd.Parameters["@BoatHouseId"].Value = ServiceWise.BoatHouseId.Trim();
                List<ServiceWiseReportHistory> li = new List<ServiceWiseReportHistory>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServiceWiseReportHistory OthDtl = new ServiceWiseReportHistory();

                        OthDtl.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        OthDtl.UserId = dt.Rows[i]["UserId"].ToString();
                        OthDtl.UserName = dt.Rows[i]["Username"].ToString();
                        OthDtl.Services = dt.Rows[i]["ServiceType"].ToString();
                        OthDtl.Category = dt.Rows[i]["CategoryName"].ToString();
                        OthDtl.Types = dt.Rows[i]["TypeName"].ToString();
                        OthDtl.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                        OthDtl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        OthDtl.ServiceTotal = dt.Rows[i]["ServiceTotal"].ToString();
                        li.Add(OthDtl);
                    }

                    ServiceWiseReportHistoryList BoatRate = new ServiceWiseReportHistoryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }
                else
                {
                    ServiceWiseReportHistoryRes BoatRate = new ServiceWiseReportHistoryRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatRate);
                }

            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("ServiceWiseDenoGrid")]
        public IHttpActionResult ServiceWiseDenoGrid([FromBody] ServiceWiseReportHistory ServiceWise)
        {
            try
            {
                string squery = string.Empty;

                squery = "SELECT * FROM ServiceWiseDenomination where ReferenceId =@ReferenceId"
                       + " AND BoatHouseId = @BoatHouseId ";


                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@ReferenceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@ReferenceId"].Value = ServiceWise.ReferenceId.Trim();
                cmd.Parameters["@BoatHouseId"].Value = ServiceWise.BoatHouseId.Trim();
                List<ServiceWiseReportHistory> li = new List<ServiceWiseReportHistory>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServiceWiseReportHistory OthDtl = new ServiceWiseReportHistory();

                        OthDtl.Denomination = dt.Rows[i]["Denomination"].ToString();
                        OthDtl.DenominationCount = dt.Rows[i]["DenominationCount"].ToString();
                        OthDtl.DenominationAmount = dt.Rows[i]["DenominationAmount"].ToString();

                        li.Add(OthDtl);
                    }

                    ServiceWiseReportHistoryList BoatRate = new ServiceWiseReportHistoryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }
                else
                {
                    ServiceWiseReportHistoryRes BoatRate = new ServiceWiseReportHistoryRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatRate);
                }

            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("BoatListDisplaynew/BoatTypenew")]
        public IHttpActionResult BoatListDepartureNew([FromBody] BoatBooking BoatDtl)
        {
            try
            {
                List<BoatBooking> li = new List<BoatBooking>();
                string squery = string.Empty;


                squery = "select BoatTypeId , BoatType , BoatHouseId , BoatHouseName from BoatTypes where BoatHouseId = @BoatHouseId  and ActiveStatus = 'A' ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = BoatDtl.BoatHouseId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatBooking ts = new BoatBooking();


                        ts.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        ts.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ts.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ts.BoatTypeName = dt.Rows[i]["BoatType"].ToString();

                        li.Add(ts);
                    }
                    BoatBookingList ItemMasters = new BoatBookingList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ItemMasters);
                }
                else
                {
                    BoatBookingStr ItemMasters1 = new BoatBookingStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
                }

            }
            catch (Exception ex)
            {
                BoatBookingStr ConfRes = new BoatBookingStr
                {
                    Response = Convert.ToString(ex),
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
        [Route("BoatListDisplaynew/Departurenew/TypeIdnew")]
        public IHttpActionResult BoatListTypeIdNew([FromBody] BoatBooking BoatDtl)
        {
            try
            {
                List<BoatBooking> li = new List<BoatBooking>();
                string squery = string.Empty;
                squery = " select top 5 BDL.BoatSeaterId,BDL.BoatTypeId,BDL.BoatHouseName,BDL.BoatHouseId,  BRM.BoatImageLink, "
                       + " (select BoatType from BoatTypes BTY where BTY.BoatTypeId = BDL.BoatTypeId and "
                       + " BTY.BoatHouseId = BDL.BoatHouseId)  BoatType,  "
                       + " (select SeaterType from BoatSeat BS  where BS.BoatSeaterId = BDL.BoatSeaterId and "
                       + " BS.BoatHouseId = BDL.BoatHouseId) BoatSeater , BDL.BookingPin , "
                       + "  BDL.BookingPin ,BDL.ExpectedTime ,BHD.PremiumStatus ,BDL.BookingId "
                       + " from BookingDtl BDL inner join BookingHdr BHD on BDL.BookingId = BHD.BookingId and "
                       + " BDL.BoatHouseId = BHD.BoatHouseId  INNER JOIN BoatRateMaster AS BRM ON BDL.BoatHouseId=BRM.BoatHouseId AND "
                       + " BDL.BoatSeaterId = BRM.BoatSeaterId AND BDL.BoatTypeId = BRM.BoatTypeId "
                       + " where BDL.BoatHouseId = @BoatHouseId AND BHD.PremiumStatus = @PremiumStatus "
                       + " and cast(BHD.BookingDate  AS DATE) = CAST(GETDATE() as DATE )   AND BDL.TripStartTime IS  NULL ANd BDL.BoatTypeId =@BoatTypeId  "
                       + " ORDER BY BDL.BoatTypeId";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PremiumStatus", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@PremiumStatus"].Value = BoatDtl.PremiumStatus.Trim();
                cmd.Parameters["@BoatHouseId"].Value = BoatDtl.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = BoatDtl.BoatTypeId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatBooking ts = new BoatBooking();

                        ts.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        ts.BookingId = dt.Rows[i]["BookingId"].ToString();
                        ts.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        ts.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ts.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ts.BoatImageLink = dt.Rows[i]["BoatImageLink"].ToString();
                        ts.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                        ts.BoatSeaterName = dt.Rows[i]["BoatSeater"].ToString();
                        ts.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                        ts.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                        ts.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

                        li.Add(ts);
                    }
                    BoatBookingList ItemMasters = new BoatBookingList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ItemMasters);
                }
                else
                {
                    BoatBookingStr ItemMasters1 = new BoatBookingStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
                }

            }
            catch (Exception ex)
            {
                BoatBookingStr ConfRes = new BoatBookingStr
                {
                    Response = Convert.ToString(ex),
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
        [Route("ViewBookingSummaryOtherRevenue")]
        public IHttpActionResult ViewBookingSummaryOtherRevenue([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                if (PinDet.QueryType != null)
                {
                    SqlCommand cmd = new SqlCommand("ViewBookingSummaryOtherRevenue", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", PinDet.UserId.ToString());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(PinDet.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(PinDet.ToDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@CorpId", PinDet.CorpId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    return Ok(ds);
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
                return Ok(ex.ToString());
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("BoatUtilization")]
        public IHttpActionResult BoatUtilization([FromBody] BoatBooking BU)
        {
            try
            {
                if (BU.QueryType != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ViewBoatUtilization", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", BU.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", BU.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", BU.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", BU.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", BU.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(BU.Fromdate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(BU.Todate.Trim(), objEnglishDate));

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    return Ok(ds);
                }
                else
                {
                    BoatBookingStr InsCE = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RptReprint")]
        public IHttpActionResult RptReprint([FromBody] BoatingReport Boating)
        {
            try
            {
                string squery = string.Empty;

                if (Boating.ServiceName == "All")
                {
                    squery = "SELECT CONVERT(VARCHAR, CreatedDate, 6) AS 'Date', RIGHT(CONVERT(VARCHAR, CreatedDate, 0),8) AS 'Time', UserId, "
                        + " UserName, ServiceType, BookingId, Reason, dbo.GetBoatTripDetails('RePrintCount', '', BoatHouseId, BookingId, ServiceType, GETDATE()) AS 'Count' "
                        + " FROM ReprintReason WHERE CAST(CreatedDate AS DATE) BETWEEN "
                        + " @FromDate AND @ToDate "
                        + "AND BoatHouseId =@BoatHouseId  ORDER BY CreatedDate";
                }
                else
                {
                    squery = "SELECT CONVERT(VARCHAR, CreatedDate, 6) AS 'Date', RIGHT(CONVERT(VARCHAR, CreatedDate, 0),8) AS 'Time', UserId, "
                        + " UserName, ServiceType, BookingId, Reason, dbo.GetBoatTripDetails('RePrintCount', '', BoatHouseId, BookingId, ServiceType, GETDATE()) AS 'Count' "
                        + " FROM ReprintReason WHERE CAST(CreatedDate AS DATE) BETWEEN "
                        + " @FromDate AND @ToDate "
                        + "AND ServiceType =@ServiceName AND BoatHouseId =@BoatHouseId ORDER BY CreatedDate";
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@ServiceName", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@ServiceName"].Value = Boating.ServiceName.Trim();
                cmd.Parameters["@BoatHouseId"].Value = Boating.BoatHouseId.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(Boating.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(Boating.ToDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("BarChartsReport/Weekwise")]
        public IHttpActionResult GetBarcode([FromBody] BarCodes svalue)
        {
            try
            {
                List<BarCodes> li = new List<BarCodes>();
                SqlCommand cmd = new SqlCommand(" DECLARE @TODate DATE "
                                                 + " DECLARE @To DATE "
                                                 + " DECLARE @Day int "
                                                 + " DECLARE @COUNT int "
                                                 + " SET @COUNT = '10' "
                                                 + " SET @TODate = cast(getdate() as date) "
                                                 + " SET @Day = DATEPART(WEEKDAY, @TODate)  "
                                                 + " DECLARE @TddODate DATE "
                                                 + " ; with CTE_Months as "
                                                 + " ( "
                                                 + " SELECT DATEADD(wk, 0, DATEADD(DAY, @Day - DATEPART(WEEKDAY, @TODate), DATEDIFF(dd, 0, @TODate))) as Date "
                                                 + "  union all "
                                                 + " select DATEADD(wk, -1, Date) "
                                                 + " from CTE_Months "
                                                 + " where date > cast(DATEADD(wk, -@COUNT, @TODate) as date) "
                                                 + " ) "
                                                 + " SELECT TOP 1  cast(Date as date) as Date FROM CTE_Months ORDER BY cast(Date as date) ASC ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {

                    BarCodes Value = new BarCodes();
                    Value.Date = dt.Rows[0]["Date"].ToString();

                    string Query2 = string.Empty;
                    List<BarCodes> li1 = new List<BarCodes>();
                    Query2 = " SELECT wDays,COUNT FROM( "
                              + " SELECT 'Sunday' AS wDays "
                              + " UNION ALL SELECT  'Monday' UNION ALL SELECT  'Tuesday' "
                              + " UNION ALL SELECT  'Wednesday' UNION ALL SELECT  'Thursday'  UNION ALL SELECT  'Friday' "
                              + " UNION ALL SELECT  'Saturday') AS A "
                              + " Left Join ( "
                              + " select  FORMAT(BookingDate, 'dddd') as days, COUNT(FORMAT(BookingDate, 'dddd')) as 'COUNT' "
                              + " from BookingHdr "
                              + " Where CAST(BookingDate as DATE) BETWEEN  cast(@Date as date) and CAST(Getdate() as DATE) "
                              + " AND BoatHouseId = @BoatHouseId   AND  BookingMedia IN ('PW', 'PA', 'PI')"
                              + " GROUP BY FORMAT(BookingDate, 'dddd')) AS B On A.wDays = B.days ";
                    SqlCommand cmd1 = new SqlCommand(Query2, con);
                    cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd1.Parameters.Add(new SqlParameter("@Date", System.Data.SqlDbType.Date));
                    cmd1.Parameters["@BoatHouseId"].Value = svalue.BoatHouseId;
                    cmd1.Parameters["@Date"].Value = Value.Date;
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    con.Close();
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            BarCodes Value1 = new BarCodes();

                            if (svalue.BarCodeselected == "3")
                            {
                                Value1.MONTH_NAME = dt1.Rows[i]["wDays"].ToString();
                                Value1.TotalCount = dt1.Rows[i]["Count"].ToString();
                                if (Value1.TotalCount == "")
                                {
                                    Value1.TotalCount = "0";
                                }
                            }

                            li.Add(Value1);
                        }


                    }
                    BarChartsList AppList = new BarChartsList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(AppList);
                }

                else
                {
                    BarCodesRes AppRes = new BarCodesRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(AppRes);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("BarChartReports")]
        public IHttpActionResult BarChartReports([FromBody] BarCodes svalue)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (svalue.BarCodeselected == "0")
                {
                    sQuery = "";
                }
                else if (svalue.BarCodeselected == "1")
                {

                    sQuery = ";with   CTE_Months as "
                         + " (SELECT 0 AS Hour "
                         + "  UNION ALL SELECT  1 UNION ALL SELECT  2 UNION ALL SELECT  3 "
                         + " UNION ALL SELECT  4 UNION ALL SELECT  5 UNION ALL SELECT  6 "
                         + "  UNION ALL SELECT  7 UNION ALL SELECT  8 UNION ALL SELECT  9 "
                         + "  UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL SELECT 12 "
                         + "  UNION ALL SELECT 13 UNION ALL SELECT 14 UNION ALL SELECT 15 "
                         + "   UNION ALL SELECT 16 UNION ALL SELECT 17 UNION ALL SELECT 18 "
                         + "  UNION ALL SELECT 19 UNION ALL SELECT 20 UNION ALL SELECT 21 "
                         + "  UNION ALL SELECT 22 UNION ALL SELECT 23  UNION ALL SELECT 24) "
                         + "  select a.Hour , CAST(B.BookingDate AS DATE) Date , Count(CAST(B.BookingDate AS DATE))  TripCount from ( "
                         + "  select * from CTE_Months ) as A "
                         + "  left Join(SELECT DISTINCT(DATEPART(hour, BookingDate)) as 'hour', BookingDate FROM BookingHdr "
                         + "  Where CAST(BookingDate as date) = cast(getdate() as date)  AND BoatHouseId = @BoatHouseId AND BookingMedia IN('PW', 'PA', 'PI') "
                         + " ) AS B ON A.Hour  = B.hour "
                         + "  group by a.Hour, CAST(B.BookingDate AS DATE) ORDER BY Hour asc ";


                }
                else if (svalue.BarCodeselected == "2")
                {
                    sQuery = " DECLARE @TODate DATE "
                            + " DECLARE @To DATE "
                            + " DECLARE @Day int "
                            + " DECLARE @COUNT INT "
                            + " set @COUNT = '10' "
                            + " SET @TODate = cast(getdate() as date) "
                            + " SET @Day = DATEPART(WEEKDAY, @TODate) "
                            + " DECLARE @TddODate DATE "
                            + "; with CTE_Months as "
                            + " ( "
                            + " SELECT DATEADD(wk, 0, DATEADD(DAY, @Day - DATEPART(WEEKDAY, @TODate), DATEDIFF(dd, 0, @TODate))) as Date "
                            + " union all "
                            + " select DATEADD(wk, -1, Date) "
                            + " from CTE_Months "
                            + " where date > cast(DATEADD(wk, -@COUNT, @TODate) as date) "
                            + " ) "
                            + " SELECT convert(varchar, (A.Date), 103) as Date, FORMAT(A.Date, 'dddd') as 'DAY', "
                            + " count(CAST(B.BookingDate as date)) as 'COUNT' FROM "
                            + " CTE_Months  AS A Left Join BookingHdr AS B On A.Date = CAST(B.BookingDate AS Date) "
                            + "  and B.BookingMedia IN ('PW', 'PA', 'PI') AND B.BoatHouseId = @BoatHouseId "
                            + " group by CAST(B.BookingDate as date), A.Date Order by A.Date asc ";

                }

                else if (svalue.BarCodeselected == "4")
                {
                    sQuery = " DECLARE @Year  BIGINT "
                            + " declare @Count int "
                            + " set @Count = '10' "
                            + " SET @Year = @Count * 12 "
                            + " ; with CTE_Months as "
                            + " (select cast(DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) as date) as MonthYear "
                            + " union all  select DATEADD(MONTH, -1, MonthYear) from CTE_Months "
                            + " where MonthYear > cast(DATEADD(MONTH, -@Year, GETDATE()) as date) "
                            + " ), "
                            + " CTE_Count as "
                            + " ( "
                            + " select COUNT(BookingDate) AS TotalCount, "
                            + " cast(DATEADD(MONTH, DATEDIFF(MONTH, 0, BookingDate), 0) as date) as MonthYear "
                            + " from BookingHdr "
                            + " where "
                            + " BookingDate >= cast(DATEADD(MONTH, -@Year, GETDATE()) as date) "
                            + " and BookingMedia IN ('PW', 'PA', 'PI') AND BoatHouseId = @BoatHouseId "
                            + " group by "
                            + " cast(DATEADD(MONTH, DATEDIFF(MONTH, 0, BookingDate), 0) as date) "
                            + " ) "

                            + " SELECT RMonth, SUM(TotalCount)AS 'Count', OMonth FROM "
                            + " ( "
                            + " select  DATENAME(month, MonthYear) as 'RMonth', SUM(TotalCount) AS 'TotalCount', Month(MonthYear) AS 'OMonth' "
                            + " from CTE_Count GROUP BY  DATENAME(month, MonthYear), Month(MonthYear) "
                            + " union "
                            + " select  DATENAME(month, MonthYear) as 'RMonth', 0, Month(MonthYear) AS 'OMonth' "
                            + " from CTE_Months as m "
                            + " where not exists(select 1 from CTE_Count as c where c.MonthYear = m.MonthYear) "
                            + " GROUP BY  DATENAME(month, MonthYear), Month(MonthYear) "
                            + " ) AS A GROUP BY RMonth, OMonth "
                            + " ORDER BY OMonth "
                            + " option (maxrecursion 365) ";
                }

                List<BarCodes> li = new List<BarCodes>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = svalue.BoatHouseId;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BarCodes Value = new BarCodes();

                        if (svalue.BarCodeselected == "0")
                        {

                        }

                        else if (svalue.BarCodeselected == "1")
                        {
                            Value.Hour = dt.Rows[i]["Hour"].ToString();
                            Value.TotalCount = dt.Rows[i]["TripCount"].ToString();
                        }
                        else if (svalue.BarCodeselected == "2")
                        {
                            Value.Day = dt.Rows[i]["Date"].ToString();
                            Value.TotalCount = dt.Rows[i]["Count"].ToString();
                            if (Value.TotalCount == "")
                            {
                                Value.TotalCount = "0";
                            }
                        }

                        else if (svalue.BarCodeselected == "4")
                        {
                            Value.Year = dt.Rows[i]["RMonth"].ToString();
                            Value.TotalCount = dt.Rows[i]["Count"].ToString();


                        }


                        li.Add(Value);
                    }

                    BarChartsList ConfList = new BarChartsList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    BarCodesRes ConfRes = new BarCodesRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified Date : 16-09-2021
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ReceiptBalanceDetails")]
        public IHttpActionResult ReceiptBalanceDetails([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount, A.BillAmount, "
                        + " A.BalanceAmount, A.RStatus, A.RePaymentType, B.CustomerMobile FROM ReceiptBalanceDetails AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId "
                        + " WHERE CAST(A.Bookingdate AS DATE) = @BookingDate "
                        + " AND CAST(B.Bookingdate AS DATE) = @BookingDate "
                        + " AND A.BoatHouseId = @BoatHouseId  AND B.BoatHouseId = @BoatHouseId "
                        + " AND B.Status IN('B', 'R') AND A.Rstatus = @RStatus  Group By A.BookingId,A.BookingDate,A.CollectedAmount, A.BillAmount,  A.BalanceAmount, A.RStatus,A.RePaymentType,B.CustomerMobile ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@RStatus", System.Data.SqlDbType.VarChar));
                    cmd.Parameters["@BookingDate"].Value = DateTime.Parse(tripBook.BookingDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@RStatus"].Value = tripBook.RStatus.Trim();
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
                            row.CollectedAmount = dt.Rows[i]["CollectedAmount"].ToString();
                            row.BillAmount = dt.Rows[i]["BillAmount"].ToString();
                            row.BalanceAmount = dt.Rows[i]["BalanceAmount"].ToString();
                            row.RStatus = dt.Rows[i]["RStatus"].ToString();
                            row.RePaymentType = dt.Rows[i]["RePaymentType"].ToString();
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
        /// Modified By : Imran
        /// Modified Date : 16-09-2021
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ReceiptBalanceDetailsSettled")]
        public IHttpActionResult ReceiptBalanceDetailsSettled([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount, A.BillAmount, "
                           + " A.BalanceAmount, A.RStatus, A.RePaymentType, C.ConfigName, D.EmpFirstName + ' ' + D.EmpLastName as 'RefunderName' "
                           + " FROM ReceiptBalanceDetails AS A INNER JOIN ConfigurationMaster AS C ON C.ConfigID=A.RePaymentType AND C.TypeID='20' "
                           + " AND C.ActiveStatus='A' LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS D ON A.BoatHouseId = D.BranchId AND A.RefundBy = D.UserId "
                           + " INNER JOIN BookingHdr AS E ON A.BookingId = E.BookingId AND A.BoatHouseId = E.BoatHouseId "
                           + " WHERE CAST(A.Bookingdate AS DATE) =@BookingDate "
                           + " AND CAST(E.Bookingdate AS DATE) =@BookingDate "
                           + " AND E.BoatHouseId =@BoatHouseId AND  A.BoatHouseId =@BoatHouseId "
                           + " AND A.Rstatus =@RStatus AND  E.Status IN ('B', 'R') Group By A.BookingId,A.BookingDate,A.CollectedAmount, "
                          + "  A.BillAmount,  A.BalanceAmount,A.RStatus,A.RePaymentType,C.ConfigName,D.EmpFirstName,D.EmpLastName";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@RStatus", System.Data.SqlDbType.VarChar));
                    cmd.Parameters["@BookingDate"].Value = DateTime.Parse(tripBook.BookingDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    cmd.Parameters["@RStatus"].Value = tripBook.RStatus.Trim();
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
                            row.CollectedAmount = dt.Rows[i]["CollectedAmount"].ToString();
                            row.BillAmount = dt.Rows[i]["BillAmount"].ToString();
                            row.BalanceAmount = dt.Rows[i]["BalanceAmount"].ToString();
                            row.RStatus = dt.Rows[i]["RStatus"].ToString();
                            row.RePaymentType = dt.Rows[i]["ConfigName"].ToString();
                            row.RefunderName = dt.Rows[i]["RefunderName"].ToString();
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

        [HttpPost]
        [AllowAnonymous]
        [Route("ReceiptBalanceDetails/Update")]
        public IHttpActionResult ReceiptBalanceDetails([FromBody] BoatTicketDtl Trip)
        {
            try
            {
                if (Trip.QueryType != "" && Trip.BookingId != "" && Trip.RePaymentType != ""
                    && Trip.BookingId != "" && Trip.CustomerMobile != ""
                    && Trip.BoatHouseId != "")
                {
                    string sReturn = string.Empty;

                    SqlCommand cmd = new SqlCommand("ReceiptBalanceDetailsPRO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Trip.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", Trip.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@RePaymentType", Trip.RePaymentType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@CustomerMobileNo", Trip.CustomerMobile.Trim());
                    cmd.Parameters.AddWithValue("@BookingMedia", Trip.BookingMedia.Trim());
                    cmd.Parameters.AddWithValue("@RefundedBy", Trip.CreatedBy.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Trip.BoatHouseName.ToString());

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
                        BoatTicketDtlStr ConMstr = new BoatTicketDtlStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        BoatTicketDtlStr ConMstr = new BoatTicketDtlStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    BoatTicketDtlStr Vehicle = new BoatTicketDtlStr
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
        /// Modified by : subalakshmin
        /// Modified Date : 2022-05-24
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ReceiptBalanceDetailsRefundReport")]
        public IHttpActionResult ReceiptBalanceDetailsReport([FromBody] TripSheet tripBook)
        {
            try
            {
                string sQuery = string.Empty;
                string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
                if (DateTime.Parse(tripBook.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                    && DateTime.Parse(tripBook.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    sQuery = " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount,A.BillAmount, A.BalanceAmount, "
                        + " A.RStatus, A.RePaymentType FROM ReceiptBalanceDetails AS A  INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId "
                        + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate AND "
                        + " @ToDate "
                        + " AND CAST(B.Bookingdate AS DATE) BETWEEN @FromDate "
                        + " AND  @ToDate "
                        + " AND A.BoatHouseId =@BoatHouseId  AND B.BoatHouseId =@BoatHouseId"
                        + " AND B.Status IN ('B', 'R', 'P') AND A.Rstatus = @RStatus ORDER BY A.Bookingdate";
                }
                else if (DateTime.Parse(tripBook.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                  && DateTime.Parse(tripBook.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {


                    sQuery = " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount,A.BillAmount, A.BalanceAmount, "
                        + " A.RStatus, A.RePaymentType FROM ReceiptBalanceDetails AS A  INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId "
                        + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate AND "
                        + " @ToDate "
                        + " AND CAST(B.Bookingdate AS DATE) BETWEEN @FromDate "
                        + " AND  @ToDate "
                        + " AND A.BoatHouseId =@BoatHouseId  AND B.BoatHouseId =@BoatHouseId"
                        + " AND B.Status IN ('B', 'R', 'P') AND A.Rstatus = @RStatus ORDER BY A.Bookingdate";
                }
                else
                {
                    sQuery = " SELECT * FROM (SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount,A.BillAmount, A.BalanceAmount, "
                      + " A.RStatus, A.RePaymentType FROM ReceiptBalanceDetails AS A  INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId "
                      + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate AND "
                      + " @ToDate "
                      + " AND CAST(B.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND A.BoatHouseId =@BoatHouseId  AND B.BoatHouseId =@BoatHouseId"
                      + " AND B.Status IN ('B', 'R', 'P') AND A.Rstatus = @RStatus"
                      + " UNION ALL"
                      + " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount,A.BillAmount, A.BalanceAmount, "
                      + " A.RStatus, A.RePaymentType FROM ReceiptBalanceDetails AS A  INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId "
                      + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate AND "
                      + " @ToDate "
                      + " AND CAST(B.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND A.BoatHouseId =@BoatHouseId  AND B.BoatHouseId =@BoatHouseId"
                      + " AND B.Status IN ('B', 'R', 'P') AND A.Rstatus = @RStatus) AS A ORDER BY A.Bookingdate";
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@RStatus", System.Data.SqlDbType.VarChar));
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(tripBook.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(tripBook.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                cmd.Parameters["@RStatus"].Value = tripBook.RStatus.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Modified by : subalakshmin
        /// Modified Date : 2022-05-24
        /// </summary>
        /// <param name="tripBook"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ReceiptBalanceDetailsSettledReport")]
        public IHttpActionResult ReceiptBalanceDetailsSettledReport([FromBody] TripSheet tripBook)
        {
            try
            {
                string sQuery = string.Empty;
                string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
                if (DateTime.Parse(tripBook.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                    && DateTime.Parse(tripBook.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    sQuery = " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount, A.BillAmount, "
                      + " A.BalanceAmount, A.RStatus, A.RePaymentType, C.ConfigName, D.EmpFirstName + ' ' + D.EmpLastName as 'RefunderName' "
                      + " FROM ReceiptBalanceDetails AS A INNER JOIN ConfigurationMaster AS C ON C.ConfigID=A.RePaymentType AND C.TypeID='20' "
                      + " AND C.ActiveStatus='A' LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS D ON A.BoatHouseId = D.BranchId AND A.RefundBy = D.UserId "
                      + " INNER JOIN BookingHdr AS E ON A.BookingId = E.BookingId AND A.BoatHouseId = E.BoatHouseId "
                      + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND CAST(E.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND E.BoatHouseId =@BoatHouseId AND  A.BoatHouseId =@BoatHouseId "
                      + " AND A.Rstatus =@RStatus AND  E.Status IN ('B', 'R', 'P') ORDER BY A.Bookingdate";
                }
                else if (DateTime.Parse(tripBook.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                   && DateTime.Parse(tripBook.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    sQuery = " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount, A.BillAmount, "
                      + " A.BalanceAmount, A.RStatus, A.RePaymentType, C.ConfigName, D.EmpFirstName + ' ' + D.EmpLastName as 'RefunderName' "
                      + " FROM ReceiptBalanceDetails AS A INNER JOIN ConfigurationMaster AS C ON C.ConfigID=A.RePaymentType AND C.TypeID='20' "
                      + " AND C.ActiveStatus='A' LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS D ON A.BoatHouseId = D.BranchId AND A.RefundBy = D.UserId "
                      + " INNER JOIN BookingHdrHistory AS E ON A.BookingId = E.BookingId AND A.BoatHouseId = E.BoatHouseId "
                      + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND CAST(E.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND E.BoatHouseId =@BoatHouseId AND  A.BoatHouseId =@BoatHouseId "
                      + " AND A.Rstatus =@RStatus AND  E.Status IN ('B', 'R', 'P') ORDER BY A.Bookingdate";
                }
                else
                {
                    sQuery = " SELECT * FROM (SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount, A.BillAmount, "
                      + " A.BalanceAmount, A.RStatus, A.RePaymentType, C.ConfigName, D.EmpFirstName + ' ' + D.EmpLastName as 'RefunderName' "
                      + " FROM ReceiptBalanceDetails AS A INNER JOIN ConfigurationMaster AS C ON C.ConfigID=A.RePaymentType AND C.TypeID='20' "
                      + " AND C.ActiveStatus='A' LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS D ON A.BoatHouseId = D.BranchId AND A.RefundBy = D.UserId "
                      + " INNER JOIN BookingHdr AS E ON A.BookingId = E.BookingId AND A.BoatHouseId = E.BoatHouseId "
                      + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND CAST(E.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND E.BoatHouseId =@BoatHouseId AND  A.BoatHouseId =@BoatHouseId "
                      + " AND A.Rstatus =@RStatus AND  E.Status IN ('B', 'R', 'P')"
                      + " UNION ALL"
                      + " SELECT A.BookingId, CONVERT(VARCHAR, A.BookingDate, 103) AS 'BookingDate', A.CollectedAmount, A.BillAmount, "
                      + " A.BalanceAmount, A.RStatus, A.RePaymentType, C.ConfigName, D.EmpFirstName + ' ' + D.EmpLastName as 'RefunderName' "
                      + " FROM ReceiptBalanceDetails AS A INNER JOIN ConfigurationMaster AS C ON C.ConfigID=A.RePaymentType AND C.TypeID='20' "
                      + " AND C.ActiveStatus='A' LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS D ON A.BoatHouseId = D.BranchId AND A.RefundBy = D.UserId "
                      + " INNER JOIN BookingHdrHistory AS E ON A.BookingId = E.BookingId AND A.BoatHouseId = E.BoatHouseId "
                      + " WHERE CAST(A.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND CAST(E.Bookingdate AS DATE) BETWEEN @FromDate "
                      + " AND  @ToDate "
                      + " AND E.BoatHouseId =@BoatHouseId AND  A.BoatHouseId =@BoatHouseId "
                      + " AND A.Rstatus =@RStatus AND  E.Status IN ('B', 'R', 'P')) AS A ORDER BY A.Bookingdate";
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@RStatus", System.Data.SqlDbType.VarChar));
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(tripBook.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(tripBook.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                cmd.Parameters["@RStatus"].Value = tripBook.RStatus.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("ServiceWiseDeletePrintHistory")]
        public IHttpActionResult ServiceWiseDeletePrintHistory([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ServiceWiseDeletePrintHistory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceId", PinDet.Input2.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(PinDet.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@UniqueId", PinDet.Input1.ToString());
                    cmd.Parameters.AddWithValue("@UserId", PinDet.UserId.ToString());

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
                        CommonAPIMethodres ConMstr = new CommonAPIMethodres
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        CommonAPIMethodres ConMstr = new CommonAPIMethodres
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
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
        [Route("GenerateBoardingPass")]
        public IHttpActionResult GenerateBoardingPass([FromBody] TripSheet tripBook)
        {
            try
            {
                if (tripBook.BoatHouseId != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();

                    string sQuery = string.Empty;

                    sQuery = " SELECT A.BookingId, A.BDate AS 'BookingDate', A.BookingPin, A.BoatTypeId, A.BoatSeaterId, A.ExpectedBoatId, "
                            + " A.ExpectedBoatNum, A.ExpectedTime,B.BoatType,C.SeaterType FROM BookingDtl AS A "
                            + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                            + " INNER JOIN BoatSeat AS C ON A.BoatHouseId = c.BoatHouseId AND A.BoatSeaterId = C.BoatSeaterId "
                            + " WHERE ExpectedTime IS NULL AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId "
                            + " AND C.BoatHouseId = @BoatHouseId AND CAST(BDate AS DATE) = @BookingDate "
                            + " AND B.ActiveStatus = 'A' AND C.ActiveStatus = 'A'  "
                            + " ORDER BY BookingSerial DESC";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BookingDate"].Value = DateTime.Parse(tripBook.BookingDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = tripBook.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet row = new TripSheet();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            row.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            row.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            row.ExpectedBoatId = dt.Rows[i]["ExpectedBoatId"].ToString();
                            row.ExpectedBoatNum = dt.Rows[i]["ExpectedBoatNum"].ToString();
                            row.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            row.BoatType = dt.Rows[i]["BoatType"].ToString();
                            row.SeaterType = dt.Rows[i]["SeaterType"].ToString();
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

        // Scan User Access Rights
        [HttpPost]
        [AllowAnonymous]
        [Route("ScanUserAccessRights/getusername")]
        public IHttpActionResult ScanUser([FromBody] Rower ScanUserRights)
        {
            try
            {
                if (ScanUserRights.BoatHouseId != "")
                {
                    List<Rower> li = new List<Rower>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT UserId, UserName FROM UserAccessRights WHERE BBMTripSheet = 'Y' AND BoatHouseId = @BoatHouseId "
                        + " AND UserRole = '3' AND UserId IN (SELECT UserId FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster WHERE BranchId = @BoatHouseId "
                        + " AND ActiveStatus = 'A' AND RoleId = '3')", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = ScanUserRights.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Rower row = new Rower();
                            row.RowerId = dt.Rows[i]["UserId"].ToString();
                            row.RowerName = dt.Rows[i]["UserName"].ToString();

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

        [HttpPost]
        [AllowAnonymous]
        [Route("ScanUserRights")]
        public IHttpActionResult InsScanAccessRights([FromBody] BH_RowerBoatAssign Scanrights)
        {
            try
            {
                if (Scanrights.QueryType != "" && Scanrights.RowerId != "" && Scanrights.BoatTypeId != ""
                    && Scanrights.SeaterId != "" && Scanrights.SeaterName != "" && Scanrights.BoatHouseId != "" && Scanrights.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ScanUserAccessRightsDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Scanrights.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", Scanrights.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", Scanrights.UserId.ToString());
                    cmd.Parameters.AddWithValue("@UserName", Scanrights.UserName.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", Scanrights.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatType", Scanrights.BoatType.ToString());
                    cmd.Parameters.AddWithValue("@SeaterId", Scanrights.SeaterId.ToString());
                    cmd.Parameters.AddWithValue("@SeaterName", Scanrights.SeaterName.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Scanrights.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Scanrights.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", Scanrights.CreatedBy.Trim());

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

        // Android Device Allocation
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlDeviceNo")]
        public IHttpActionResult ddlDeviceNo()
        {
            try
            {

                List<DeviceInformation> li = new List<DeviceInformation>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DeviceInformation WHERE ActiveStatus = 'A' AND DeviceNo NOT IN (SELECT DeviceNo FROM DeviceAllocation WHERE ActiveStatus = 'A');", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DeviceInformation ShowBoathouseMstr = new DeviceInformation();
                        ShowBoathouseMstr.DeviceNo = dt.Rows[i]["DeviceNo"].ToString();

                        li.Add(ShowBoathouseMstr);
                    }

                    DeviceInformationList ConfList = new DeviceInformationList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    DeviceInformationRes ConfRes = new DeviceInformationRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                DeviceInformationRes ConfRes = new DeviceInformationRes
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
        [Route("DeviceNo/ddlUserName")]
        public IHttpActionResult DeviceNoddlUserName([FromBody] DeviceInformation DeviceNo)
        {
            try
            {
                string sCondtion = string.Empty;
                List<DeviceInformation> li = new List<DeviceInformation>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserId,EmpFirstName+' '+EmpLastName AS 'UserName' FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster WHERE ActiveStatus='A' AND BranchId=@BoatHouseId AND RoleId <> '2' ", con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = DeviceNo.BoatHouseId.Trim();
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
            catch (Exception ex)
            {
                DeviceInformationRes ConfRes = new DeviceInformationRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("DeviceAllocationINS")]
        public IHttpActionResult MstrConfigurationMaster([FromBody] DeviceInformation Device)
        {
            try
            {
                if (Device.QueryType != "" && Device.DeviceNo != "" && Device.BoatHouseId != ""
                    && Device.BoatHouseName != "" && Device.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("Device_Allocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Device.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", Device.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@DeviceNo", Device.DeviceNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Device.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Device.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", Device.CreatedBy.Trim());

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
                        DeviceInformationRes ConMstr = new DeviceInformationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        DeviceInformationRes ConMstr = new DeviceInformationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    DeviceInformationRes Vehicle = new DeviceInformationRes
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
        [Route("DeviceLogin")]
        public IHttpActionResult GetUserPwsDetails([FromBody] DeviceLogin Login)
        {
            try
            {
                string sQuery = string.Empty;

                sQuery = " SELECT B.UserId, B.UserName, B.EmpFirstName + ' ' + B.EmpLastName AS 'EmployeeName',  B.BranchId, B.BranchName, C.ConfigName, A.DeviceNo, "
                    + " D.BBMBooking AS 'BoatBooking',D.BBMOtherService AS 'OSBooking',D.BBMAdditionalService AS 'AdditionalTicketBooking',"
                    + " D.BRestaurant AS 'RestaurantBooking',D.BBMKioskBooking AS 'KioskBooking',"
                    + " D.BTMTripSheetSettle AS 'DepositRefund', D.BBMTripSheet AS 'TripSheet',D.EmployeeId, D.BoatBookingWithOtherservice, D.OfflineRights "
                    + " FROM UserBasedDeviceAllocationDetail AS A"
                    + " LEFT JOIN"
                    + " ("
                    + " SELECT UserId,BoatHouseId,BBMBooking,BBMOtherService,BBMAdditionalService,BRestaurant,BBMKioskBooking,BTMTripSheetSettle,BBMTripSheet,"
                    + "  EmpId AS 'EmployeeId',BBMBookingOthers AS 'BoatBookingWithOtherservice',OfflineRights FROM UserAccessRights"
                    + " WHERE UserId IN (SELECT UserId FROM UserBasedDeviceAllocationDetail WHERE DeviceNo=@DeviceNo AND ActiveStatus ='A')"
                    + " ) "
                    + " AS D ON A.UserId = D.UserId AND A.BoatHouseId = D.BoatHouseId"
                    + " INNER JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.EmpMaster AS B"
                    + " ON A.UserId = B.UserId AND A.BoatHouseId = B.BranchId "
                    + " INNER JOIN"
                    + " ConfigurationMaster AS C ON B.RoleId = C.ConfigID"
                    + " WHERE A.ActiveStatus = 'A' AND B.ActiveStatus = 'A' AND C.TypeID = '21' AND "
                    + " A.DeviceNo = @DeviceNo";


                List<DeviceLogin> li = new List<DeviceLogin>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@DeviceNo", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@DeviceNo"].Value = Login.DeviceNo.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DeviceLogin UserD = new DeviceLogin();

                        UserD.UserId = dt.Rows[i]["UserId"].ToString();
                        UserD.UserName = dt.Rows[i]["UserName"].ToString();
                        UserD.BoatHouseId = dt.Rows[i]["BranchId"].ToString();
                        UserD.BoatHouseName = dt.Rows[i]["BranchName"].ToString();
                        UserD.DeviceNo = dt.Rows[i]["DeviceNo"].ToString();
                        UserD.Role = dt.Rows[i]["ConfigName"].ToString();
                        UserD.BoatBooking = dt.Rows[i]["BoatBooking"].ToString();
                        UserD.OSBooking = dt.Rows[i]["OSBooking"].ToString();
                        UserD.AdditionalTicketBooking = dt.Rows[i]["AdditionalTicketBooking"].ToString();
                        UserD.RestaurantBooking = dt.Rows[i]["RestaurantBooking"].ToString();
                        UserD.KioskBooking = dt.Rows[i]["KioskBooking"].ToString();
                        UserD.DepositRefund = dt.Rows[i]["DepositRefund"].ToString();
                        UserD.TripSheet = dt.Rows[i]["TripSheet"].ToString();

                        UserD.EmployeeId = dt.Rows[i]["EmployeeId"].ToString();
                        UserD.EmployeeName = dt.Rows[i]["EmployeeName"].ToString();
                        UserD.BoatBookingWithOtherservice = dt.Rows[i]["BoatBookingWithOtherservice"].ToString();
                        UserD.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

                        li.Add(UserD);
                    }

                    DeviceLoginList UserList = new DeviceLoginList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(UserList);
                }

                else
                {
                    DeviceLoginRes UserRes = new DeviceLoginRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(UserRes);
                }
            }
            catch (Exception ex)
            {
                DeviceLoginRes UserRes = new DeviceLoginRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(UserRes);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetDeviceNo")]
        public IHttpActionResult DeviceNoUSer([FromBody] DeviceInformation DeviceNumberUser)
        {
            string sQuery = string.Empty;
            try
            {
                if (DeviceNumberUser.BoatHouseId != "")
                {
                    List<DeviceInformation> li = new List<DeviceInformation>();
                    con.Open();
                    sQuery = "  SELECT UniqueId, DeviceNo FROM DeviceAllocation "
                                      + " WHERE DeviceNo NOT IN(SELECT DeviceNo FROM UserBasedDeviceAllocationDetail "
                                      + " WHERE BoatHouseId =@BoatHouseId AND ActiveStatus = 'A') "
                                      + " AND ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId";
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = DeviceNumberUser.BoatHouseId.Trim();
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
                    DeviceInformationRes Vehicle = new DeviceInformationRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                DeviceInformationRes ConfRes = new DeviceInformationRes
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
        [Route("UBDeviceAllocation/Insert")]
        public IHttpActionResult InsUserBasedAllocation([FromBody] DeviceInformation DeviceAllocation)
        {
            try
            {
                if (DeviceAllocation.QueryType != "" && DeviceAllocation.DeviceNo != "" && DeviceAllocation.UserId != "")

                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("UserBasedAllocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", DeviceAllocation.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", DeviceAllocation.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", DeviceAllocation.UserId.ToString());
                    cmd.Parameters.AddWithValue("@UserName", DeviceAllocation.UserName.ToString());
                    cmd.Parameters.AddWithValue("@DeviceNo", DeviceAllocation.DeviceNo.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", DeviceAllocation.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", DeviceAllocation.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", DeviceAllocation.CreatedBy.Trim());

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
                        DeviceInformationRes InsCE = new DeviceInformationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        DeviceInformationRes InsCE = new DeviceInformationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }

                else
                {
                    DeviceInformationRes InsCE = new DeviceInformationRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                DeviceInformationRes ConfRes = new DeviceInformationRes
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
        [Route("DeviceInformation/Insert")]
        public IHttpActionResult InsDeviceInformation([FromBody] DeviceInformation DeviceNum)
        {
            try
            {
                if (DeviceNum.QueryType != "" && DeviceNum.DeviceNo != "")

                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("DeviceInformationDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", DeviceNum.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", DeviceNum.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@DeviceNo", DeviceNum.DeviceNo.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", DeviceNum.CreatedBy.Trim());

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
                        DeviceInformationRes InsCE = new DeviceInformationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        DeviceInformationRes InsCE = new DeviceInformationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    DeviceInformationRes InsCE = new DeviceInformationRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InsCE);
                }
            }
            catch (Exception ex)
            {
                DeviceInformationRes ConfRes = new DeviceInformationRes
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

        [HttpGet]
        [AllowAnonymous]
        [Route("ddlDeviceNoAll")]
        public IHttpActionResult ddlDeviceNoAll()
        {
            try
            {

                List<DeviceInformation> li = new List<DeviceInformation>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DeviceInformation WHERE ActiveStatus = 'A' ;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DeviceInformation ShowBoathouseMstr = new DeviceInformation();
                        ShowBoathouseMstr.DeviceNo = dt.Rows[i]["DeviceNo"].ToString();

                        li.Add(ShowBoathouseMstr);
                    }

                    DeviceInformationList ConfList = new DeviceInformationList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    DeviceInformationRes ConfRes = new DeviceInformationRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                DeviceInformationRes ConfRes = new DeviceInformationRes
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
        [Route("GetDeviceNoAll")]
        public IHttpActionResult DeviceNoUSerAll([FromBody] DeviceInformation DeviceNumberUser)
        {
            string sQuery = string.Empty;
            try
            {
                if (DeviceNumberUser.BoatHouseId != "")
                {
                    List<DeviceInformation> li = new List<DeviceInformation>();
                    con.Open();
                    sQuery = "  SELECT UniqueId, DeviceNo FROM DeviceAllocation "
                                      + " WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId";
                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = DeviceNumberUser.BoatHouseId.Trim();
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
                    DeviceInformationRes Vehicle = new DeviceInformationRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                DeviceInformationRes ConfRes = new DeviceInformationRes
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
        [Route("RowerMonthSettledGrid")]
        public IHttpActionResult RowerMonthSettledGrid([FromBody] SettlementAmount boatType)
        {
            try
            {
                if (boatType.FromDate != "" && boatType.ToDate != "" && boatType.BoatHouseId != "")
                {
                    List<SettlementAmount> li = new List<SettlementAmount>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT A.RowerId, B.RowerName, SUM(A.SettlementAmount) AS 'SettlementAmount' "
                                   + " FROM RowerSettlement AS A "
                                   + " INNER JOIN RowerMaster AS B ON A.RowerId = B.RowerId AND A.BoatHouseId = B.BoatHouseId "
                                   + " WHERE A.BoatHouseId = @BoatHouseId AND "
                                   + " CAST(A.SettlementDate AS DATE) BETWEEN @FromDate AND @ToDate "
                                   + " GROUP BY A.RowerId, B.RowerName "
                                   + " ORDER BY RowerId", con);

                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(boatType.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(boatType.ToDate.Trim(), objEnglishDate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SettlementAmount amt = new SettlementAmount();
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
        /// Newly added by Imran on 2022-05-11 for Discount
        /// </summary>
        /// <param name="Discount"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptDiscountDetails")]
        public IHttpActionResult DiscountDetails([FromBody] BoatingReport Discount)
        {
            try
            {
                string squery = string.Empty;

                string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                if (DateTime.Parse(Discount.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                    && DateTime.Parse(Discount.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = " SELECT BDL.BookingId, CONVERT(NVARCHAR, BHD.BookingDate, 103) AS 'BookingDate', "
                       + " SUM(BDL.BFDInitNetAmount) AS 'BillAmount', SUM(BDL.InitOfferAmount) AS 'OfferAmount', "
                       + " SUM(BDL.BFDInitNetAmount) - SUM(BDL.InitOfferAmount) AS 'NetAmount' FROM BookingDtl AS BDL "
                       + " INNER JOIN BookingHdr AS BHD ON BDL.BookingId = BHD.BookingId AND BDL.BoatHouseId = BHD.BoatHouseId "
                       + " WHERE InitOfferAmount > 0 AND CAST(BHD.BookingDate AS date) BETWEEN "
                       + " @FromDate AND "
                       + " @ToDate AND "
                       + " BDL.BoatHouseId = @BoatHouseId AND BHD.BoatHouseId = @BoatHouseId "
                       + " GROUP BY BDL.BookingId, BHD.BookingDate ORDER BY BHD.BookingDate ASC ";
                }
                else if (DateTime.Parse(Discount.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                    && DateTime.Parse(Discount.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = " SELECT BDL.BookingId, CONVERT(NVARCHAR, BHD.BookingDate, 103) AS 'BookingDate', "
                       + " SUM(BDL.BFDInitNetAmount) AS 'BillAmount', SUM(BDL.InitOfferAmount) AS 'OfferAmount', "
                       + " SUM(BDL.BFDInitNetAmount) - SUM(BDL.InitOfferAmount) AS 'NetAmount' FROM BookingDtlHistory AS BDL "
                       + " INNER JOIN BookingHdrHistory AS BHD ON BDL.BookingId = BHD.BookingId AND BDL.BoatHouseId = BHD.BoatHouseId "
                       + " WHERE InitOfferAmount > 0 AND CAST(BHD.BookingDate AS date) BETWEEN "
                       + " @FromDate AND "
                       + " @ToDate AND "
                       + " BDL.BoatHouseId = @BoatHouseId AND BHD.BoatHouseId = @BoatHouseId "
                       + " GROUP BY BDL.BookingId, BHD.BookingDate ORDER BY BHD.BookingDate ASC ";
                }
                else
                {
                    squery = " SELECT * FROM (SELECT BDL.BookingId, CONVERT(NVARCHAR, BHD.BookingDate, 103) AS 'BookingDate', "
                      + " SUM(BDL.BFDInitNetAmount) AS 'BillAmount', SUM(BDL.InitOfferAmount) AS 'OfferAmount', "
                      + " SUM(BDL.BFDInitNetAmount) - SUM(BDL.InitOfferAmount) AS 'NetAmount' FROM BookingDtl AS BDL "
                      + " INNER JOIN BookingHdr AS BHD ON BDL.BookingId = BHD.BookingId AND BDL.BoatHouseId = BHD.BoatHouseId "
                      + " WHERE InitOfferAmount > 0 AND CAST(BHD.BookingDate AS date) BETWEEN "
                      + " @FromDate AND "
                      + " @ToDate AND "
                      + " BDL.BoatHouseId = @BoatHouseId AND BHD.BoatHouseId = @BoatHouseId "
                      + " GROUP BY BDL.BookingId, BHD.BookingDate"
                      + " UNION ALL"
                      + " SELECT BDL.BookingId, CONVERT(NVARCHAR, BHD.BookingDate, 103) AS 'BookingDate', "
                      + " SUM(BDL.BFDInitNetAmount) AS 'BillAmount', SUM(BDL.InitOfferAmount) AS 'OfferAmount', "
                      + " SUM(BDL.BFDInitNetAmount) - SUM(BDL.InitOfferAmount) AS 'NetAmount' FROM BookingDtlHistory AS BDL "
                      + " INNER JOIN BookingHdrHistory AS BHD ON BDL.BookingId = BHD.BookingId AND BDL.BoatHouseId = BHD.BoatHouseId "
                      + " WHERE InitOfferAmount > 0 AND CAST(BHD.BookingDate AS date) BETWEEN "
                      + " @FromDate AND "
                      + " @ToDate AND "
                      + " BDL.BoatHouseId = @BoatHouseId AND BHD.BoatHouseId = @BoatHouseId "
                      + " GROUP BY BDL.BookingId, BHD.BookingDate) AS A  "
                      + " ORDER BY CONVERT(DATE,A.BookingDate,103) ASC ";
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = Discount.BoatHouseId.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(Discount.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(Discount.ToDate.Trim(), objEnglishDate);
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
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("AbstractTripSheetSummary")]
        public IHttpActionResult AbstractTripSheetReport([FromBody] AbstractTripSheetSummaryReport Abstract)
        {
            try
            {
                if (Abstract.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("AbstractTripSheetSummary", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Abstract.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Abstract.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Abstract.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Abstract.ToDate.Trim(), objEnglishDate));

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

        [HttpPost]
        [AllowAnonymous]
        [Route("PrintingRights")]
        public IHttpActionResult PrintingRights([FromBody] PrintingRights InsrPrtRgts)
        {
            try
            {
                if (InsrPrtRgts.QueryType != "" && InsrPrtRgts.UniqueId != "" && InsrPrtRgts.BoatHouseId != ""
                    && InsrPrtRgts.BoatHouseName != "" && InsrPrtRgts.OtherService != "" && InsrPrtRgts.Restaurant != ""
                    && InsrPrtRgts.AdditionalTicket != "" && InsrPrtRgts.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrPrintingAccessRights", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsrPrtRgts.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsrPrtRgts.UniqueId.ToString());

                    cmd.Parameters.AddWithValue("@BoatHouseId", InsrPrtRgts.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsrPrtRgts.BoatHouseName.ToString());

                    cmd.Parameters.AddWithValue("@OtherService", InsrPrtRgts.OtherService.ToString());
                    cmd.Parameters.AddWithValue("@Restaurant", InsrPrtRgts.Restaurant.Trim());
                    cmd.Parameters.AddWithValue("@AdditionalTicket", InsrPrtRgts.AdditionalTicket.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsrPrtRgts.CreatedBy.Trim());

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
                        EmployeeMasterRes EmMstr = new EmployeeMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(EmMstr);
                    }
                    else
                    {
                        EmployeeMasterRes EmMstr = new EmployeeMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(EmMstr);
                    }
                }
                else
                {
                    EmployeeMasterRes EmMstr = new EmployeeMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
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

        // Update User Rights for Restaurant, Bar, Hotel, Tour
        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateRights")]
        public IHttpActionResult UpdateRights([FromBody] BoatBooking UpRights)
        {
            try
            {
                if (UpRights.UserId != "" && UpRights.BranchId != "" && UpRights.ModuleType != "")
                {
                    string sReturn = string.Empty;
                    string sQuery = string.Empty;
                    if (UpRights.ModuleType == "Hotel")
                    {
                        sQuery = " UPDATE "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.ModuleAccessRights SET MHotel = 'Y' "
                               + " WHERE BranchId = @BranchId AND "
                               + " UserId = @UserId ";
                    }
                    else if (UpRights.ModuleType == "Tour")
                    {
                        sQuery = " UPDATE "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.ModuleAccessRights SET MTour = 'Y' "
                               + " WHERE BranchId = @BranchId AND "
                               + " UserId = @UserId";
                    }
                    else if (UpRights.ModuleType == "Restaurant")
                    {
                        sQuery = " UPDATE "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.ModuleAccessRights SET MRestaurant = 'Y' "
                               + " WHERE BranchId = @BranchId AND "
                               + " UserId = @UserId ";
                    }
                    else if (UpRights.ModuleType == "Bar")
                    {
                        sQuery = " UPDATE "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.ModuleAccessRights SET MBar = 'Y' "
                               + " WHERE BranchId = @BranchId AND "
                               + " UserId =@UserId";
                    }
                    else
                    {
                        BoatBookingStr InAppCar = new BoatBookingStr
                        {
                            Response = "Pass Valid Type",
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BranchId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BranchId"].Value = UpRights.BranchId.Trim();
                    cmd.Parameters["@UserId"].Value = UpRights.UserId.Trim();
                    con.Open();
                    int sResult = cmd.ExecuteNonQuery();
                    con.Close();

                    if (sResult > 0)
                    {
                        BoatBookingStr InAppCar = new BoatBookingStr
                        {
                            Response = "Updated Sucessfully",

                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        BoatBookingStr InAppCar = new BoatBookingStr
                        {
                            Response = "Error In Updating",
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    BoatBookingStr InAppCar = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(InAppCar);
                }
            }
            catch (Exception ex)
            {
                BoatBookingStr ConfRes = new BoatBookingStr
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
        /// Created Date : 28-07-2021
        /// Hosted By : Vediyappan.
        /// Hosted Date : 03-08-2021
        /// Modified by : Imran 
        /// Modified Date 19-08-2021
        /// </summary>
        /// <param name="Slot"></param>
        /// <returns></returns>         
        [HttpPost]
        [AllowAnonymous]
        [Route("BlockBoatBookingTime")]
        public async Task<IHttpActionResult> BlockBoatBookingTime([FromBody] BookingSlotSummaryDetails Slot)
        {
            var i = 0;
            if (Slot.SlotIds == null)
                return NotFound();

            var result = new List<string>();
            foreach (var id in Slot.SlotIds)
            {
                result.Add(await BlockBoatBookTime(id, Slot.BookingDate, Slot.BoatTypeIds[i], Slot.BoatSeaterIds[i], Slot.TripCountIds[i],
                    Slot.BoatHouseId, Slot.BookingType[i], Slot.BookingUser, Slot.BookingBlockId[i]));
                i++;
            }
            BookingSlotSummaryDetailsResNew BookingSlot = new BookingSlotSummaryDetailsResNew
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }


        /// <summary>
        /// Created By : Vediyappan
        /// Created Date : 27-07-2021
        /// Hosted By : Vediyappan.
        /// Hosted Date : 03-08-2021
        /// </summary>
        /// <param name="Stid"></param>
        /// <param name="BookingDate"></param>
        /// <param name="BoatTypeId"></param>
        /// <param name="BoatSeaterId"></param>
        /// <param name="TripCount"></param>
        /// <param name="BoatHouseId"></param>
        /// <param name="BookingType"></param>
        /// <param name="BookingUser"></param>
        /// <param name="BookingBlockId"></param>
        /// <returns></returns>         
        public async Task<dynamic> BlockBoatBookTime(int Stid, string BookingDate, int BoatTypeId, int BoatSeaterId,
           int TripCount, string BoatHouseId, string BookingType, string BookingUser, int BookingBlockId)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BookingSlotSummary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@SlotId", Stid.ToString());
                cmd.Parameters.AddWithValue("@BoatTypeId", BoatTypeId.ToString());
                cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSeaterId.ToString());
                cmd.Parameters.AddWithValue("@TripCount", TripCount.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BookingType", BookingType.ToString());
                cmd.Parameters.AddWithValue("@BookingUser", BookingUser.ToString());
                cmd.Parameters.AddWithValue("@BlockId", BookingBlockId.ToString());

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
        /// Created By : Vediyappan
        /// Created Date : 27-07-2021
        /// Hosted By : Vediyappan.
        /// Hosted Date : 03-08-2021
        /// </summary>
        /// <param name="Slot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BlockBoatBookingTimeSlotDept")]
        public async Task<IHttpActionResult> BlockBoatBookingTimeSlot([FromBody] BookingSlotSummaryDetails Slot)
        {
            try
            {
                if (Slot.BookingDate != null && Slot.SlotId != null && Slot.BoatTypeId != null && Slot.BoatSeaterId != null
                    && Slot.TripCount != null && Slot.BoatHouseId != null && Slot.BookingType != null && Slot.BookingUser != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("BookingSlotSummary", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@BookingDate", Slot.BookingDate.ToString());
                    cmd.Parameters.AddWithValue("@SlotId", Slot.SlotId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", Slot.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", Slot.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@TripCount", Slot.TripCount.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Slot.BoatCount.ToString());
                    cmd.Parameters.AddWithValue("@BookingType", Slot.BookingType.ToString());
                    cmd.Parameters.AddWithValue("@BookingUser", Slot.BookingUser.ToString());

                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con.Close();

                    if (sResult[0].Trim() == "Success")
                    {
                        BookingSlotSummaryDetailsRes BookingSlot = new BookingSlotSummaryDetailsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(BookingSlot);
                    }
                    else
                    {
                        BookingSlotSummaryDetailsRes BookingSlot = new BookingSlotSummaryDetailsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(BookingSlot);
                    }
                }
                else
                {
                    BookingSlotSummaryDetailsRes BookingSlot = new BookingSlotSummaryDetailsRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(BookingSlot);
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        /// Created Date 04-08-2021
        /// </summary>
        /// <param name="DelTmpSlot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateTmpBookedSlotDept")]
        public IHttpActionResult UpdateTmpBookedSlot([FromBody] BoatSlotMaster UpdTmpSlot)
        {
            try
            {
                if (UpdTmpSlot.BoatHouseId != "" && UpdTmpSlot.CheckInDate != "" && UpdTmpSlot.BoatTypeId != "" && UpdTmpSlot.BoatSeaterId != ""
                    && UpdTmpSlot.SlotType != "" && UpdTmpSlot.SlotIdold != "" && UpdTmpSlot.SlotId != "" && UpdTmpSlot.UserId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("UpdateTemporarySlotDept", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@BoatHouseId", UpdTmpSlot.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@CheckInDate", DateTime.Parse(UpdTmpSlot.CheckInDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatTypeId", UpdTmpSlot.BoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", UpdTmpSlot.BoatSeaterId.Trim());
                    cmd.Parameters.AddWithValue("@BookingType", UpdTmpSlot.SlotType.Trim());
                    cmd.Parameters.AddWithValue("@SlotIdold", UpdTmpSlot.SlotIdold.Trim());
                    cmd.Parameters.AddWithValue("@SlotId", UpdTmpSlot.SlotId.Trim());
                    cmd.Parameters.AddWithValue("@BlockId", UpdTmpSlot.BlockId.Trim());
                    cmd.Parameters.AddWithValue("@UserId", UpdTmpSlot.UserId.Trim());

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
                        BoatSlotMasterRes SlotMstr = new BoatSlotMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(SlotMstr);
                    }
                    else
                    {
                        BoatSlotMasterRes SlotMstr = new BoatSlotMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(SlotMstr);
                    }
                }
                else
                {
                    BoatSlotMasterRes SlotMstr = new BoatSlotMasterRes
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
        /// /Created By : Abhinaya
        /// Created Date : 04-08-2021
        /// </summary>
        /// <param name="BoatSlot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ChangeAvailbleBoatSlotTime")]
        public IHttpActionResult ChangeAvailbleBoatSlotTime([FromBody] BoatSlotMaster BoatSlot)
        {
            try
            {
                if (BoatSlot.BoatHouseId != "")
                {
                    List<BoatSlotMaster> li = new List<BoatSlotMaster>();
                    SqlCommand cmd = new SqlCommand("ChangeBoatBookingSlotDept", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@BoatHouseId", BoatSlot.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@CheckInDate", DateTime.Parse(BoatSlot.CheckInDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatTypeId", BoatSlot.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSlot.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@SlotType", BoatSlot.SlotType.ToString());
                    cmd.Parameters.AddWithValue("@UserId", BoatSlot.UserId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatSlotMaster BoatSlotMaster = new BoatSlotMaster();

                            BoatSlotMaster.SlotId = dt.Rows[i]["SlotId"].ToString();
                            BoatSlotMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatSlotMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatSlotMaster.SlotStartTime = dt.Rows[i]["SlotStartTime"].ToString();
                            BoatSlotMaster.SlotEndTime = dt.Rows[i]["SlotEndTime"].ToString();

                            BoatSlotMaster.SlotDuration = dt.Rows[i]["SlotDuration"].ToString();
                            BoatSlotMaster.TotalTripCount = dt.Rows[i]["TotalTripCount"].ToString();
                            BoatSlotMaster.AvailableBoatCount = dt.Rows[i]["AvailableBoatCount"].ToString();
                            BoatSlotMaster.AvailableSeatCount = dt.Rows[i]["AvailableSeatCount"].ToString();

                            BoatSlotMaster.SlotTime = dt.Rows[i]["SlotTime"].ToString();

                            li.Add(BoatSlotMaster);
                        }

                        BoatSlotMasterList BoatRate = new BoatSlotMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        BoatSlotMasterString BoatRate = new BoatSlotMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    BoatSlotMasterString Vehicle = new BoatSlotMasterString
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
        /// Created By: Jaya Suriya A
        /// Date :18-Aug-2021
        /// This Method Is Used To Get TripFeedBack Comments and userName
        /// </summary>
        /// <param name="tripFeedbackInput">This Parameter Is Used To Get Details From TripFeedBackComments Class</param>
        /// <returns></returns>        
        [HttpPost]
        [AllowAnonymous]
        [Route("GetTripFeedBackComments")]
        public IHttpActionResult GetTripFeedBack([FromBody] TripFeedBackComments tripFeedbackInput)
        {
            try
            {
                if (string.IsNullOrEmpty(tripFeedbackInput.BoatHouseId) ||
                string.IsNullOrEmpty(tripFeedbackInput.SeaterTypeId) ||
                string.IsNullOrEmpty(tripFeedbackInput.BoatTypeId))
                {
                    TripFeedBackCommentsResponse TripResponse = new TripFeedBackCommentsResponse()
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(TripResponse);
                }
                List<TripFeedBackComments> list = new List<TripFeedBackComments>();
                DataTable dt = new DataTable();

                SqlCommand cmd = new SqlCommand("sp_GetTripFeedBack", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@BoatHouseId", SqlDbType.Int).Value = tripFeedbackInput.BoatHouseId;
                cmd.Parameters.AddWithValue("@BoatTypeId", SqlDbType.Int).Value = tripFeedbackInput.BoatTypeId;
                cmd.Parameters.AddWithValue("@SeaterTypeId", SqlDbType.Int).Value = tripFeedbackInput.SeaterTypeId;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dtr in dt.Rows)
                    {
                        TripFeedBackComments tripFeedBackOutPut = new TripFeedBackComments();

                        tripFeedBackOutPut.BoatHouseId = dtr["BoatHouseId"].ToString().Trim();
                        tripFeedBackOutPut.BoatTypeId = dtr["BoatTypeId"].ToString().Trim();
                        tripFeedBackOutPut.SeaterTypeId = dtr["SeaterTypeId"].ToString().Trim();
                        tripFeedBackOutPut.AverageRating = dtr["AverageRating"].ToString().Trim();
                        tripFeedBackOutPut.Ratings = dtr["Ratings"].ToString().Trim();
                        tripFeedBackOutPut.Comments = dtr["Comments"].ToString().Trim();
                        tripFeedBackOutPut.FirstName = dtr["FirstName"].ToString().Trim();
                        list.Add(tripFeedBackOutPut);
                    }
                    TripFeedBackCommentsList TripList = new TripFeedBackCommentsList
                    {
                        Response = list,
                        StatusCode = 1
                    };
                    return Ok(list);
                }
                else
                {
                    TripFeedBackCommentsResponse TripResponse = new TripFeedBackCommentsResponse
                    {
                        Response = "No Records Found !!!.",
                        StatusCode = 0
                    };
                    return Ok(TripResponse);
                }
            }
            catch (Exception ex)
            {
                TripFeedBackCommentsResponse TripResponse = new TripFeedBackCommentsResponse
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(TripResponse);
            }
        }

        ///-- 2021-08-20
        ///********************************************//
        /// <summary>
        /// Online Boting
        /// Modified By : Jaya Suriya 
        /// Modified Date: 25-10-2021, Added OthersRescheduleDate Parameter
        /// </summary>
        /// <param name="BB"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PublicOnlineBoatBookingBfrTran")]
        public IHttpActionResult OnlineBoatBookingBfrTran(OnlineBoatBooking BB)
        {

            try
            {
                if (BB.BookingDate != "" && BB.BookingPin != "" && BB.UserId != "" && BB.MobileNo != "" && BB.PaymentMode != ""
                    && BB.Amount != "" && BB.BookingType != "" && BB.BoatHouseId != "" && BB.BookingMedia != "" && BB.ModuleType != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("PublicSaveOnlineBookingBeforeTransaction", con_Common);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    //Boat Booking
                    cmd.Parameters.AddWithValue("@QueryType", "Insert");
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BB.BookingDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BookingPin", BB.BookingPin.ToString());

                    cmd.Parameters.AddWithValue("@UserId", BB.UserId.ToString());
                    cmd.Parameters.AddWithValue("@MobileNo", BB.MobileNo.ToString());
                    cmd.Parameters.AddWithValue("@EmailId", BB.EmailId.ToString());
                    cmd.Parameters.AddWithValue("@PaymentMode", BB.PaymentMode.ToString());
                    cmd.Parameters.AddWithValue("@Amount", BB.Amount.ToString());

                    cmd.Parameters.AddWithValue("@BookingType", BB.BookingType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", BB.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", BB.BoatHouseName.ToString());

                    cmd.Parameters.AddWithValue("@PremiumStatus", BB.PremiumStatus.ToString());
                    cmd.Parameters.AddWithValue("@BoatPremiumStatus", String.Join(",", BB.BoatPremiumStatus.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@NoOfPass", BB.NoOfPass.ToString());
                    cmd.Parameters.AddWithValue("@NoOfChild", BB.NoOfChild.ToString());
                    cmd.Parameters.AddWithValue("@NoOfInfant", BB.NoOfInfant.ToString());

                    cmd.Parameters.AddWithValue("@BoatTypeId", String.Join(",", BB.BoatTypeId.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@BoatSeaterId", String.Join(",", BB.BoatSeaterId.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@BookingDuration", String.Join(",", BB.BookingDuration.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@InitBoatCharge", String.Join(",", BB.InitBoatCharge.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@InitRowerCharge", String.Join(",", BB.InitRowerCharge.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@BoatDeposit", String.Join(",", BB.BoatDeposit.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@InitOfferAmount", String.Join(",", BB.InitOfferAmount.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@InitNetAmount", String.Join(",", BB.InitNetAmount.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@CGSTTaxAmount", String.Join(",", BB.CGSTTaxAmount.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@SGSTTaxAmount", String.Join(",", BB.SGSTTaxAmount.Select(p => p.ToString()).ToArray()));

                    // Other Service Booking
                    cmd.Parameters.AddWithValue("@OthServiceStatus", String.Join(",", BB.OthServiceStatus.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@OthServiceId", String.Join(",", BB.OthServiceId.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@OthChargePerItem", String.Join(",", BB.OthChargePerItem.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@OthNoOfItems", String.Join(",", BB.OthNoOfItems.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@OthNetAmount", String.Join(",", BB.OthNetAmount.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@CGSTOthTaxDetails", String.Join(",", BB.CGSTOthTaxAmount.Select(p => p.ToString()).ToArray()));
                    cmd.Parameters.AddWithValue("@SGSTOthTaxAmount", String.Join(",", BB.SGSTOthTaxAmount.Select(p => p.ToString()).ToArray()));

                    if (BB.BookingType.ToString().Trim().ToUpper() == "R")
                    {
                        cmd.Parameters.AddWithValue("@BRSBookingId", BB.BRSBookingId.ToString());
                        cmd.Parameters.AddWithValue("@BRSNewDate", BB.BRSNewDate.ToString());
                        cmd.Parameters.AddWithValue("@BRSCharge", BB.BRSCharge.ToString());
                        cmd.Parameters.AddWithValue("@BRSCGST", BB.BRSCGST.ToString());
                        cmd.Parameters.AddWithValue("@BRSSGST", BB.BRSSGST.ToString());
                        cmd.Parameters.AddWithValue("@BRSRuleId", BB.BRSRuleId.ToString());
                        //Newly Added
                        cmd.Parameters.AddWithValue("@BRSBookingPIN", BB.BRSBookingPIN.ToString());//Newly Added for Public Rescheduling.
                        cmd.Parameters.AddWithValue("@BRSChargeType", BB.BRSChargeType.ToString());
                        if (BB.BRSOthersRescheduleDate.ToString().Trim() == "")
                        {
                            cmd.Parameters.AddWithValue("@OthersRescheduleDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@OthersRescheduleDate", DateTime.Parse(BB.BRSOthersRescheduleDate.ToString().Trim(), objEnglishDate));
                        }
                    }
                    if (BB.BookingType.ToString().Trim().ToUpper() == "R")
                    {
                        cmd.Parameters.AddWithValue("@BookingTimeSlotId", BB.BRSTimeSlot.ToString());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BookingTimeSlotId", String.Join(",", BB.BookingTimeSlotId.Select(p => p.ToString()).ToArray()));
                    }
                    cmd.Parameters.AddWithValue("@BookingMedia", BB.BookingMedia.ToUpper());

                    cmd.Parameters.AddWithValue("@BFDInitBoatCharge", BB.BFDInitBoatCharge);
                    cmd.Parameters.AddWithValue("@BFDInitNetAmount", BB.BFDInitNetAmount);
                    cmd.Parameters.AddWithValue("@BFDGST", BB.BFDGST);
                    cmd.Parameters.AddWithValue("@EntryType", BB.EntryType.ToUpper());
                    cmd.Parameters.AddWithValue("@ModuleType", BB.ModuleType.Trim());

                    // cmd.Parameters.AddWithValue("@BookingTimeSlotId", String.Join(",", BB.BookingTimeSlotId.Select(p => p.ToString()).ToArray()));
                    //New Field
                    cmd.Parameters.AddWithValue("@BookingBlockId", String.Join(",", BB.BookingBlockId.Select(p => p.ToString()).ToArray()));

                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con_Common.Open();

                    cmd.ExecuteNonQuery();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con_Common.Close();

                    if (sResult[0].Trim() == "Success")
                    {
                        OnlineBoatBookingStr TxMstr = new OnlineBoatBookingStr
                        {
                            Response = sResult[0].Trim() + '~' + sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(TxMstr);
                    }
                    else
                    {
                        OnlineBoatBookingStr TxMstr = new OnlineBoatBookingStr
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(TxMstr);
                    }
                }
                else
                {
                    BoatBookingStr TxMstr = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(TxMstr);
                }
            }
            catch (Exception ex)
            {
                BoatBookingStr TxMstr = new BoatBookingStr
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(TxMstr);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("PublicOnlineBoatBookingAftrTran")]
        public async Task<IHttpActionResult> OnlineBoatBookingAftrTran([FromBody] OnlineBoatBooking BB)
        {
            try
            {
                if (BB.TransactionNo != "" && BB.UserId != "" && BB.MobileNo != "" && BB.BankReferenceNo != ""
                    && BB.BookingType != "" && BB.BoatHouseId != "" && BB.BookingMedia != "" && BB.ModuleType != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("SaveOnlineBookingAfterTransaction", con_Common);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    //Boat Booking
                    cmd.Parameters.AddWithValue("@QueryType", "After");
                    cmd.Parameters.AddWithValue("@TransactionNo", BB.TransactionNo.ToString());
                    cmd.Parameters.AddWithValue("@UserId", BB.UserId.ToString());
                    cmd.Parameters.AddWithValue("@MobileNo", BB.MobileNo.ToString());
                    cmd.Parameters.AddWithValue("@EmailId", BB.EmailId.ToString());

                    cmd.Parameters.AddWithValue("@BankReferenceNo", BB.BankReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@BookingType", BB.BookingType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", BB.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", "");
                    cmd.Parameters.AddWithValue("@BookingMedia", BB.BookingMedia.ToString());

                    cmd.Parameters.AddWithValue("@OrderStatus", BB.OrderStatus.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", 0);
                    cmd.Parameters.AddWithValue("@BookingDate", "1900-01-01");
                    cmd.Parameters.AddWithValue("@ModuleType", BB.ModuleType.Trim());
                    cmd.Parameters.AddWithValue("@Amount", 0);

                    cmd.Parameters.AddWithValue("@OrderId", BB.OrderId.ToString());
                    cmd.Parameters.AddWithValue("@TrackingId", BB.TrackingId.ToString());
                    //Newly Added by Imran For Public Booking On 2021-10-25
                    cmd.Parameters.AddWithValue("@BankName", BB.BankName.ToString());

                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con_Common.Open();

                    cmd.ExecuteNonQuery();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con_Common.Close();

                    if (sResult[0].Trim() == "Success")
                    {
                        string FinalResponse = string.Empty;
                        int FinalStatusCode = 1;

                        if (BB.OrderStatus.ToUpper().Trim() == "SUCCESS" || BB.OrderStatus.Trim() == "Success" || BB.OrderStatus.Trim() == "success")
                        {
                            FinalResponse = await GetBoatBookingTransactionDetails(FinalStatusCode, BB.TransactionNo.Trim(), BB.UserId.Trim(), BB.MobileNo.Trim(),
                               BB.EmailId.Trim(), BB.BookingType.Trim(), BB.BookingMedia.Trim(), BB.BoatHouseId.ToString().Trim(), BB.ModuleType.Trim());
                        }
                        else
                        {
                            FinalResponse = BB.OrderStatus.Trim();
                            FinalStatusCode = 0;
                        }

                        OnlineBoatBookingStr TxMstr = new OnlineBoatBookingStr
                        {
                            Response = FinalResponse.Trim(),
                            StatusCode = FinalStatusCode
                        };
                        return Ok(TxMstr);
                    }
                    else
                    {
                        OnlineBoatBookingStr TxMstr = new OnlineBoatBookingStr
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(TxMstr);
                    }
                }
                else
                {
                    BoatBookingStr TxMstr = new BoatBookingStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(TxMstr);
                }
            }
            catch (Exception ex)
            {
                BoatBookingStr TxMstr = new BoatBookingStr
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(TxMstr);
            }
        }

        /// <summary>
        /// This Method is used for multiple booking.
        /// Created By : Imran
        /// Created Date : 19-08-2021
        /// Modified By : Jaya Suriya 
        /// Modified Date :25-10-2021, Added RescheduleDescription And RescheduleDescription in select Query
        /// </summary>
        /// <param name="FinalStatus"></param>
        /// <param name="TransactionNo"></param>
        /// <param name="UserId"></param>
        /// <param name="MobileNo"></param>
        /// <param name="EmailId"></param>
        /// <param name="BookingType"></param>
        /// <param name="BookingMedia"></param>
        /// <param name="BoatHouseId"></param>
        /// <param name="ModuleType"></param>
        /// <returns></returns>
        public async Task<string> GetBoatBookingTransactionDetails(int FinalStatus, string TransactionNo, string UserId, string MobileNo, string EmailId,
                   string BookingType, string BookingMedia, string BoatHouseId, string ModuleType)
        {
            string sReturn = string.Empty;
            //var result = new List<string>();
            try
            {
                string sQuery = string.Empty;

                //sQuery = "SELECT TransactionNo, CONVERT(VARCHAR, BookingDate, 103) AS 'BookingDate', BookingPin, UserId, MobileNo, EmailId, PaymentMode, Amount, "
                //    + " BoatHouseId, BoatHouseName, BookingMedia, BookingType, PremiumStatus,BoatPremiumStatus, NoOfPass, NoOfChild, NoOfInfant, BoatTypeId, "
                //    + " BoatSeaterId, BookingDuration, InitBoatCharge, InitRowerCharge, BoatDeposit, InitOfferAmount, InitNetAmount,CGSTTaxAmount,SGSTTaxAmount,"
                //    + " OthServiceStatus, OthServiceId, OthChargePerItem, OthNoOfItems, OthNetAmount, "
                //    + " CGSTOthTaxAmount,SGSTOthTaxAmount, BRSBookingId, BRSNewDate, BRSCharge, BRSCGST, BRSSGST, BRSRuleId, BRSBookingPin,"
                //    + " BFDInitBoatCharge, BFDInitNetAmount, BFDGST, ModuleType, BookingTimeSlotId, BookingBlockId "
                //    + " FROM OnlineBookingBeforeTransaction WHERE TransactionNo = '" + TransactionNo.Trim() + "'"
                //    + " AND UserId = '" + UserId.Trim() + "' AND  MobileNo = '" + MobileNo.Trim() + "' "
                //    + " AND BoatHouseId = '" + BoatHouseId.Trim() + "' AND BookingType = '" + BookingType.Trim() + "'"
                //    + " AND BookingMedia = '" + BookingMedia.Trim() + "' AND ModuleType = '" + ModuleType.Trim() + "'";

                sQuery = "SELECT TransactionNo, CONVERT(VARCHAR, BookingDate, 103) AS 'BookingDate', BookingPin, UserId, MobileNo, EmailId, PaymentMode, Amount, "
                               + " BoatHouseId, BoatHouseName, BookingMedia, BookingType, PremiumStatus,BoatPremiumStatus, NoOfPass, NoOfChild, NoOfInfant, BoatTypeId, "
                               + " BoatSeaterId, BookingDuration, InitBoatCharge, InitRowerCharge, BoatDeposit, InitOfferAmount, InitNetAmount,CGSTTaxAmount,SGSTTaxAmount,"
                               + " OthServiceStatus, OthServiceId, OthChargePerItem, OthNoOfItems, OthNetAmount, "
                               + " CGSTOthTaxAmount,SGSTOthTaxAmount, BRSBookingId, BRSNewDate, BRSCharge, BRSCGST, BRSSGST, BRSRuleId,BRSBookingPin,BRSChargeType, "
                               + " BFDInitBoatCharge, BFDInitNetAmount, BFDGST, ModuleType, BookingTimeSlotId, BookingBlockId ,"
                               + " ISNULL(CAST(OthersRescheduledDate AS VARCHAR),'-') AS 'OthersRescheduledDate' , "
                               + " ISNULL(RescheduleDescription, '-') AS 'RescheduleDescription'  "
                               + " FROM OnlineBookingBeforeTransaction WHERE TransactionNo = @TransactionNo"
                               + " AND UserId = @UserId AND  MobileNo = @MobileNo "
                               + " AND BoatHouseId = @BoatHouseId AND BookingType = @BookingType"
                               + " AND BookingMedia = @BookingMedia AND ModuleType =@ModuleType";

                SqlCommand cmd = new SqlCommand(sQuery, con_Common);
                cmd.Parameters.Add(new SqlParameter("@TransactionNo", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@MobileNo", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ModuleType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingMedia", System.Data.SqlDbType.Int));
                cmd.Parameters["@TransactionNo"].Value = TransactionNo.Trim();
                cmd.Parameters["@UserId"].Value = UserId.Trim();
                cmd.Parameters["@MobileNo"].Value = MobileNo.Trim();
                cmd.Parameters["@BoatHouseId"].Value = BoatHouseId.Trim();
                cmd.Parameters["@BookingType"].Value = BookingType.Trim();
                cmd.Parameters["@BookingMedia"].Value = BookingMedia.Trim();
                cmd.Parameters["@ModuleType"].Value = ModuleType.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["BookingType"].ToString().Trim() == "B" || dt.Rows[0]["BookingType"].ToString().Trim() == "b")
                    {
                        FinalStatus = 1;

                        string CurrentTime = DateTime.Now.ToString("HH:mm:ss");
                        string BookingDate = dt.Rows[0]["BookingDate"].ToString().Trim() + ' ' + CurrentTime;
                        //string[] PremiumStatus = dt.Rows[0]["PremiumStatus"].ToString().Split(',');
                        string[] BoatPremiumStatus = dt.Rows[0]["BoatPremiumStatus"].ToString().Split(',');
                        string[] BoatTypeId = dt.Rows[0]["BoatTypeId"].ToString().Split(',');
                        string[] BoatSeaterId = dt.Rows[0]["BoatSeaterId"].ToString().Split(',');
                        string[] BookingDuration = dt.Rows[0]["BookingDuration"].ToString().Split(',');
                        string[] InitBoatCharge = dt.Rows[0]["InitBoatCharge"].ToString().Split(',');
                        string[] InitRowerCharge = dt.Rows[0]["InitRowerCharge"].ToString().Split(',');
                        string[] BoatDeposit = dt.Rows[0]["BoatDeposit"].ToString().Split(',');
                        string[] InitOfferAmount = dt.Rows[0]["InitOfferAmount"].ToString().Split(',');
                        string[] InitNetAmount = dt.Rows[0]["InitNetAmount"].ToString().Split(',');
                        string[] CGSTTaxAmount = dt.Rows[0]["CGSTTaxAmount"].ToString().Split(',');
                        string[] SGSTTaxAmount = dt.Rows[0]["SGSTTaxAmount"].ToString().Split(',');
                        string[] OthServiceStatus = dt.Rows[0]["OthServiceStatus"].ToString().Split(',');
                        string[] OthServiceId = dt.Rows[0]["OthServiceId"].ToString().Split(',');
                        string[] OthChargePerItem = dt.Rows[0]["OthChargePerItem"].ToString().Split(',');
                        string[] OthNoOfItems = dt.Rows[0]["OthNoOfItems"].ToString().Split(',');
                        string[] OthNetAmount = dt.Rows[0]["OthNetAmount"].ToString().Split(',');
                        string[] CGSTOthTaxAmount = dt.Rows[0]["CGSTOthTaxAmount"].ToString().Split(',');
                        string[] SGSTOthTaxAmount = dt.Rows[0]["SGSTOthTaxAmount"].ToString().Split(',');
                        string[] BookingTimeSlotId = dt.Rows[0]["BookingTimeSlotId"].ToString().Split(',');
                        int[] BookingBlockId = Array.ConvertAll<string, int>(dt.Rows[0]["BookingBlockId"].ToString().TrimEnd(',').Split(','), Int32.Parse);
                        // int[] BookingBlockId = Array.ConvertAll<string, int>(dt.Rows[0]["BookingBlockId"].ToString().Split(','), Int32.Parse);


                        foreach (DataRow row in dt.Rows)
                        {
                            PublicOnlineBooking onlinebb = new PublicOnlineBooking()
                            {

                                TransactionNo = dt.Rows[0]["TransactionNo"].ToString().Trim(),
                                BookingDate = BookingDate,
                                BookingPin = dt.Rows[0]["BookingPin"].ToString().Trim(),
                                UserId = dt.Rows[0]["UserId"].ToString().Trim(),
                                MobileNo = dt.Rows[0]["MobileNo"].ToString().Trim(),
                                EmailId = dt.Rows[0]["EmailId"].ToString().Trim(),
                                PaymentMode = dt.Rows[0]["PaymentMode"].ToString().Trim(),
                                Amount = dt.Rows[0]["Amount"].ToString().Trim(),
                                BoatHouseId = dt.Rows[0]["BoatHouseId"].ToString().Trim(),
                                BoatHouseName = dt.Rows[0]["BoatHouseName"].ToString().Trim(),
                                BookingMedia = dt.Rows[0]["BookingMedia"].ToString().Trim(),
                                BookingType = dt.Rows[0]["BookingType"].ToString().Trim(),
                                PremiumStatus = dt.Rows[0]["PremiumStatus"].ToString().Trim(),
                                BoatPremiumStatus = BoatPremiumStatus,
                                NoOfPass = dt.Rows[0]["NoOfPass"].ToString().Trim(),
                                NoOfChild = dt.Rows[0]["NoOfChild"].ToString().Trim(),
                                NoOfInfant = dt.Rows[0]["NoOfInfant"].ToString().Trim(),
                                BoatTypeId = BoatTypeId,
                                BoatSeaterId = BoatSeaterId,
                                BookingDuration = BookingDuration,
                                InitBoatCharge = InitBoatCharge,
                                InitRowerCharge = InitRowerCharge,
                                BoatDeposit = BoatDeposit,
                                InitOfferAmount = InitOfferAmount,
                                InitNetAmount = InitNetAmount,
                                CGSTTaxAmount = CGSTTaxAmount,
                                SGSTTaxAmount = SGSTTaxAmount,
                                OthServiceStatus = OthServiceStatus,
                                OthServiceId = OthServiceId,
                                OthChargePerItem = OthChargePerItem,
                                OthNoOfItems = OthNoOfItems,
                                OthNetAmount = OthNetAmount,
                                CGSTOthTaxAmount = CGSTOthTaxAmount,
                                SGSTOthTaxAmount = SGSTOthTaxAmount,
                                BRSBookingId = dt.Rows[0]["BRSBookingId"].ToString().Trim(),

                                BRSNewDate = dt.Rows[0]["BRSNewDate"].ToString().Trim(),
                                BRSCharge = dt.Rows[0]["BRSCharge"].ToString().Trim(),
                                BRSCGST = dt.Rows[0]["BRSCGST"].ToString().Trim(),
                                BRSSGST = dt.Rows[0]["BRSSGST"].ToString().Trim(),
                                BRSRuleId = dt.Rows[0]["BRSRuleId"].ToString().Trim(),
                                BFDInitBoatCharge = dt.Rows[0]["BFDInitBoatCharge"].ToString().Trim(),

                                BFDInitNetAmount = dt.Rows[0]["BFDInitNetAmount"].ToString().Trim(),
                                BFDGST = dt.Rows[0]["BFDGST"].ToString().Trim(),
                                ModuleType = dt.Rows[0]["ModuleType"].ToString().Trim(),
                                BookingTimeSlotId = BookingTimeSlotId,
                                BookingBlockId = BookingBlockId,
                                CustomerGSTNo = ""


                            };
                            var result = await OnlineBoatBooking(onlinebb);

                            sReturn = result.Response[0].ToString();
                            //var result = task.GetAwaiter().GetResult();
                        }
                    }

                    if (dt.Rows[0]["BookingType"].ToString().Trim() == "I" || dt.Rows[0]["BookingType"].ToString().Trim() == "i")
                    {
                        FinalStatus = 1;

                        string CurrentTime = DateTime.Now.ToString("HH:mm:ss");
                        string BookingDate = dt.Rows[0]["BookingDate"].ToString().Trim() + ' ' + CurrentTime;

                        sReturn = SaveOnlineOtherServiceBooking(dt.Rows[0]["TransactionNo"].ToString().Trim(), dt.Rows[0]["Amount"].ToString().Trim(),
                            BookingDate, dt.Rows[0]["UserId"].ToString().Trim(),
                            dt.Rows[0]["MobileNo"].ToString().Trim(), EmailId.Trim(),
                            dt.Rows[0]["BoatHouseId"].ToString().Trim(), dt.Rows[0]["BoatHouseName"].ToString().Trim(),
                            dt.Rows[0]["BookingMedia"].ToString().Trim(), dt.Rows[0]["OthServiceStatus"].ToString().Trim(),
                            dt.Rows[0]["OthServiceId"].ToString().Trim(), dt.Rows[0]["OthChargePerItem"].ToString().Trim(),
                            dt.Rows[0]["OthNoOfItems"].ToString().Trim(), dt.Rows[0]["OthNetAmount"].ToString().Trim(),
                            dt.Rows[0]["CGSTOthTaxAmount"].ToString().Trim(), dt.Rows[0]["SGSTOthTaxAmount"].ToString().Trim(), dt.Rows[0]["ModuleType"].ToString().Trim());
                    }

                    if (dt.Rows[0]["BookingType"].ToString().Trim() == "R" || dt.Rows[0]["BookingType"].ToString().Trim() == "r")
                    {
                        FinalStatus = 1;

                        sReturn = SaveOnlineReSheduleBooking(dt.Rows[0]["TransactionNo"].ToString().Trim(),
                        dt.Rows[0]["Amount"].ToString().Trim(), dt.Rows[0]["UserId"].ToString().Trim(),
                        dt.Rows[0]["MobileNo"].ToString().Trim(), EmailId.Trim(),
                        dt.Rows[0]["BoatHouseId"].ToString().Trim(), dt.Rows[0]["BoatHouseName"].ToString().Trim(),
                        dt.Rows[0]["BookingMedia"].ToString().Trim(), dt.Rows[0]["BRSBookingId"].ToString().Trim(),
                        dt.Rows[0]["BRSNewDate"].ToString().Trim(), dt.Rows[0]["BRSCharge"].ToString().Trim(),
                        dt.Rows[0]["BRSCGST"].ToString().Trim(), dt.Rows[0]["BRSSGST"].ToString().Trim(),
                        dt.Rows[0]["BRSRuleId"].ToString().Trim(), dt.Rows[0]["ModuleType"].ToString().Trim(),
                        dt.Rows[0]["BRSBookingPin"].ToString().Trim(), dt.Rows[0]["BookingTimeSlotId"].ToString().Trim(),
                        dt.Rows[0]["BRSChargeType"].ToString().Trim(), dt.Rows[0]["RescheduleDescription"].ToString().Trim(),
                        dt.Rows[0]["OthersRescheduledDate"].ToString().Trim());

                    }
                }
                else
                {
                    FinalStatus = 0;
                }
            }
            catch (Exception ex)
            {
                FinalStatus = 0;
            }

            return sReturn;
        }

        public async Task<BoatBookingResponse> OnlineBoatBooking(PublicOnlineBooking BoatTest)
        {
            string sReturn = string.Empty;
            var i = 0;
            if (BoatTest.BookingBlockId == null)
                return new BoatBookingResponse();
            string[] test;
            test = BoatTest.BookingTimeSlotId.Where(x => x != "0").ToArray();
            string BoatCount = Convert.ToString(test.Count());
            var result = new List<string>();
            foreach (var id in BoatTest.BookingBlockId)


            {
                int Loop = i + 1;
                var result1 = await PublicOnlineBoatBooking(id, BoatTest.TransactionNo, BoatTest.BookingDate, BoatTest.BookingPin, BoatTest.UserId,
                    BoatTest.MobileNo, BoatTest.EmailId, BoatTest.Amount, BoatTest.BoatHouseId, BoatTest.BoatHouseName,
                    BoatTest.BookingMedia, BoatTest.BookingType, BoatTest.PremiumStatus, BoatTest.NoOfPass, BoatTest.NoOfChild, BoatTest.NoOfInfant,
                    BoatTest.BoatTypeId[i], BoatTest.BoatSeaterId[i], BoatTest.BookingDuration[i], BoatTest.InitBoatCharge[i],
                    BoatTest.InitRowerCharge[i], BoatTest.BoatDeposit[i], BoatTest.InitOfferAmount[i], BoatTest.InitNetAmount[i],
                    BoatTest.CGSTTaxAmount[i], BoatTest.SGSTTaxAmount[i], BoatTest.OthServiceStatus[i], BoatTest.OthServiceId[i], BoatTest.OthChargePerItem[i], BoatTest.OthNoOfItems[i],
                    BoatTest.OthNetAmount[i], BoatTest.CGSTOthTaxAmount[i], BoatTest.SGSTOthTaxAmount[i],
                    BoatTest.BFDInitBoatCharge, BoatTest.BFDInitNetAmount, BoatTest.BFDGST, BoatTest.ModuleType, BoatTest.CustomerGSTNo, BoatTest.BookingTimeSlotId[i],
                    Loop, BoatTest.BoatPremiumStatus[i], BoatCount);
                result.Add(result1);
                i++;
            }
            //string Return = [0].ToString().Trim();
            string[] sResult = result[0].Split('~');
            BoatBookingResponse BookingSlot = new BoatBookingResponse
            {
                Response = result,
                StatusCode = 1
            };
            //if (BookingSlot.Response[0].Equals("Success "))
            if (sResult[0].ToString().Trim() == "Success")
            {

                SaveTransactionHistroryDetails(BoatTest.TransactionNo, sResult[2].ToString().Trim(), BoatTest.BookingDate, BoatTest.Amount, BoatTest.BookingType,
                      BoatTest.UserId, BoatTest.MobileNo, BoatTest.EmailId, BoatTest.BoatHouseId, BoatTest.BoatHouseName, BoatTest.BookingMedia,
                      BoatTest.ModuleType);
            }
            //return BookingSlot.Response[0].ToString().Trim();
            return BookingSlot;
        }

        public async Task<dynamic> PublicOnlineBoatBooking(int BookingBlockId, string TransactionNo, string BookingDate, string BookingPin, string UserId,
       string MobileNo, string EmailId, string Amount, string BoatHouseId, string BoatHouseName, string BookingMedia, string BookingType, string PremiumStatus,
       string NoOfPass, string NoOfChild, string NoOfInfant, string BoatTypeId, string BoatSeaterId,
       string BookingDuration, string InitBoatCharge, string InitRowerCharge, string BoatDeposit, string InitOfferAmount, string InitNetAmount,
       string CGSTTaxAmount, string SGSTTaxAmount, string OthServiceStatus, string OthServiceId, string OthChargePerItem, string OthNoOfItems,
       string OthNetAmount, string CGSTOthTaxAmount, string SGSTOthTaxAmount,
       string BFDInitBoatCharge, string BFDInitNetAmount, string BFDGST, string ModuleType,
       string CustomerGSTNo, string BookingTimeSlotId, int Loop, string BoatPremiumStatus, string BoatCount)

        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BoatBookingMultiple", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "Insert");
                cmd.Parameters.AddWithValue("@BookingId", "0");
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.ToString(), objEnglishDate));
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.ToString());

                cmd.Parameters.AddWithValue("@BookingPin", BookingPin.ToString());
                cmd.Parameters.AddWithValue("@CustomerId", UserId.ToString());
                cmd.Parameters.AddWithValue("@CustomerMobileNo", MobileNo.ToString());
                cmd.Parameters.AddWithValue("@CustomerName", "OnlinePayment");
                cmd.Parameters.AddWithValue("@CustomerAddress", "OnlinePayment");

                if (CustomerGSTNo == "")
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString());
                }

                cmd.Parameters.AddWithValue("@PremiumStatus", PremiumStatus.ToString());
                cmd.Parameters.AddWithValue("@BoatPremiumStatus", BoatPremiumStatus.ToString());
                cmd.Parameters.AddWithValue("@NoOfPass", NoOfPass.ToString());
                cmd.Parameters.AddWithValue("@NoOfChild", NoOfChild.ToString());
                cmd.Parameters.AddWithValue("@NoOfInfant", NoOfInfant.ToString());
                cmd.Parameters.AddWithValue("@OfferId", "0");

                cmd.Parameters.AddWithValue("@InitBillAmount", Amount.ToString());
                cmd.Parameters.AddWithValue("@PaymentType", "3");
                cmd.Parameters.AddWithValue("@ActualBillAmount", "0");
                cmd.Parameters.AddWithValue("@Status", "B");
                cmd.Parameters.AddWithValue("@BoatTypeId", BoatTypeId.ToString());

                cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSeaterId.ToString());
                cmd.Parameters.AddWithValue("@BookingDuration", BookingDuration.ToString());
                cmd.Parameters.AddWithValue("@InitBoatCharge", InitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@InitRowerCharge", InitRowerCharge.ToString());
                cmd.Parameters.AddWithValue("@BoatDeposit", BoatDeposit.ToString());

                cmd.Parameters.AddWithValue("@CGSTTaxAmount", CGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTTaxAmount", SGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@InitOfferAmount", InitOfferAmount.ToString());
                cmd.Parameters.AddWithValue("@InitNetAmount", InitNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", UserId.ToString());

                // Other Service Booking
                cmd.Parameters.AddWithValue("@OthServiceStatus", OthServiceStatus.ToString());
                cmd.Parameters.AddWithValue("@OthServiceId", OthServiceId.ToString());
                cmd.Parameters.AddWithValue("@OthChargePerItem", OthChargePerItem.ToString());
                cmd.Parameters.AddWithValue("@OthNoOfItems", OthNoOfItems.ToString());

                cmd.Parameters.AddWithValue("@CGSTOthTaxAmount", CGSTOthTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTOthTaxAmount", SGSTOthTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@OthNetAmount", OthNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BFDInitBoatCharge", BFDInitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@BFDInitNetAmount", BFDInitNetAmount.ToString());
                cmd.Parameters.AddWithValue("@BFDGST", BFDGST.ToString());


                cmd.Parameters.AddWithValue("@CollectedAmount", "0");
                cmd.Parameters.AddWithValue("@BalanceAmount", "0");

                cmd.Parameters.AddWithValue("@BookingTimeSlotId", BookingTimeSlotId.ToString());
                cmd.Parameters.AddWithValue("@BookingBlockId", BookingBlockId.ToString());


                cmd.Parameters.AddWithValue("@BoatCount", BoatCount.ToString());
                cmd.Parameters.AddWithValue("@LoopCount", Loop.ToString());

                SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                RuturnValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(RuturnValue);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                string[] sResult = sReturn.Split('~');
                con.Close();
                //return sResult[0];
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
        /// Created By : Abhinaya
        /// Created Date : 17-08-2021 
        /// </summary>
        /// <param name="GetBlock"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetBlockIdToUpdateDept")]
        public IHttpActionResult GetBlockIdToUpdateDept([FromBody] BoatSlotMaster GetBlock)
        {
            try
            {
                if (GetBlock.BoatHouseId != "" && GetBlock.CheckInDate != "" && GetBlock.BoatTypeId != "" && GetBlock.BoatSeaterId != ""
                    && GetBlock.SlotType != "" && GetBlock.SlotIdold != "" && GetBlock.UserId != "")
                {
                    List<BoatSlotMaster> li = new List<BoatSlotMaster>();
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("GetBlockIdToUpdateDept", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@BoatHouseId", GetBlock.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@CheckInDate", DateTime.Parse(GetBlock.CheckInDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatTypeId", GetBlock.BoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", GetBlock.BoatSeaterId.Trim());
                    cmd.Parameters.AddWithValue("@BookingType", GetBlock.SlotType.Trim());
                    cmd.Parameters.AddWithValue("@SlotIdold", GetBlock.SlotIdold.Trim());
                    cmd.Parameters.AddWithValue("@BlockId", GetBlock.BlockId.Trim());
                    cmd.Parameters.AddWithValue("@UserId", GetBlock.UserId.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatSlotMaster BoatSlotMaster = new BoatSlotMaster();

                            BoatSlotMaster.BlockId = dt.Rows[i]["BSId"].ToString();
                            BoatSlotMaster.SlotId = dt.Rows[i]["SlotId"].ToString();
                            BoatSlotMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatSlotMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatSlotMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            BoatSlotMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            BoatSlotMaster.TotalTripCount = dt.Rows[i]["TripCount"].ToString();
                            BoatSlotMaster.SlotStartTime = dt.Rows[i]["SlotDescription"].ToString();

                            li.Add(BoatSlotMaster);
                        }

                        BoatSlotMasterList BoatRate = new BoatSlotMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        BoatSlotMasterString BoatRate = new BoatSlotMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    BoatSlotMasterString Vehicle = new BoatSlotMasterString
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
        /// Created By : Abhinaya
        /// Created Date : 17-08-2021
        /// </summary>
        /// <param name="UpdTmpSlot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateTmpBookedSlotDeptNew")]
        public async Task<IHttpActionResult> UpdateTmpBookedSlotNew([FromBody] BookingSlotSummaryDetails UpdTmpSlot)
        {
            var i = 0;
            if (UpdTmpSlot.BookingBlockId == null)
                return NotFound();

            var result = new List<string>();
            foreach (var id in UpdTmpSlot.BookingBlockId)
            {
                result.Add(await UpdateNew(id, UpdTmpSlot.SlotIds[i], UpdTmpSlot.BoatHouseId, UpdTmpSlot.BookingUser));
                i++;
            }
            BookingSlotSummaryDetailsResNew BookingSlot = new BookingSlotSummaryDetailsResNew
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }


        /// <summary>
        /// Created By : Abhinaya
        /// Created Date : 17-08-2021
        /// </summary>
        /// <param name="Stid"></param>
        /// <param name="BookingBlockId"></param>
        /// <param name="BoatHouseId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<dynamic> UpdateNew(int Stid, int SlotIds, string BoatHouseId, string BookingUser)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("UpdateTemporarySlotDept", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@SlotId", SlotIds.ToString());
                cmd.Parameters.AddWithValue("@BlockId", Stid.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@UserId", BookingUser.ToString());

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
        /// Created By : Abhinaya.k
        /// Created Date : 19-08-2021
        /// </summary>
        /// <param name="BoatTest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatBookingDept")]
        public async Task<IHttpActionResult> BoatBookingDept([FromBody] BoatBookingNew BoatTest)
        {
            var i = 0;
            if (BoatTest.Countslotids == null)
                return NotFound();
            string[] test;
            test = BoatTest.BoatSeaterId.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string BoatCount = Convert.ToString(test.Count());
            var result = new List<string>();
            foreach (var id in BoatTest.Countslotids)
            {

                result.Add(await BoatBookingDepartment(id, BoatTest.QueryType, BoatTest.BookingDate, BoatTest.BookingId, BoatTest.BoatHouseId, BoatTest.BoatHouseName,
                    BoatTest.BookingPin, BoatTest.CustomerId, BoatTest.CustomerMobileNo, BoatTest.CustomerName, BoatTest.CustomerAddress, BoatTest.CustomerGSTNo,
                    BoatTest.PremiumStatus, BoatTest.NoOfPass, BoatTest.NoOfChild, BoatTest.NoOfInfant, BoatTest.OfferId, BoatTest.InitBillAmount, BoatTest.PaymentType,
                    BoatTest.ActualBillAmount, BoatTest.Status, BoatTest.BoatTypeId[i], BoatTest.BoatSeaterId[i], BoatTest.BookingDuration[i], BoatTest.InitBoatCharge[i],
                    BoatTest.InitRowerCharge[i], BoatTest.BoatDeposit[i], BoatTest.InitOfferAmount[i], BoatTest.InitNetAmount[i], BoatTest.BookingMedia,
                    BoatTest.CreatedBy[i], BoatTest.OthServiceStatus[i], BoatTest.OthServiceId[i], BoatTest.OthChargePerItem[i], BoatTest.OthNoOfItems[i],
                    BoatTest.OthNetAmount[i], BoatTest.BFDInitBoatCharge, BoatTest.BFDInitNetAmount, BoatTest.BFDGST, BoatTest.CollectedAmount, BoatTest.BalanceAmount,
                    BoatTest.BookingBlockId[i], BoatTest.CGSTTaxAmount[i], BoatTest.SGSTTaxAmount[i], BoatTest.CGSTOthTaxAmount[i], BoatTest.SGSTOthTaxAmount[i],
                    BoatTest.BookingTimeSlotId[i], BoatTest.BoatPremiumStatus[i], BoatCount));
                i++;
            }
            BoatBookingResponse BookingSlot = new BoatBookingResponse
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }

        public async Task<dynamic> BoatBookingDepartment(int Countslotids, string QueryType, string BookingDate, string BookingId, int BoatHouseId, string BoatHouseName, string BookingPin,
          int CustomerId, string CustomerMobileNo, string CustomerName, string CustomerAddress, string CustomerGSTNo, string PremiumStatus, int NoOfPass, int NoOfChild,
          int NoOfInfant, int OfferId, decimal InitBillAmount, int PaymentType, decimal ActualBillAmount, string Status, string BoatTypeId, string BoatSeaterId,
          string BookingDuration, string InitBoatCharge, string InitRowerCharge, string BoatDeposit, string InitOfferAmount, string InitNetAmount,
          string BookingMedia, string CreatedBy, string OthServiceStatus, string OthServiceId, string OthChargePerItem, string OthNoOfItems,
           string OthNetAmount, string BFDInitBoatCharge, string BFDInitNetAmount, string BFDGST, decimal CollectedAmount, decimal BalanceAmount, string BookingBlockId,
           string CGSTTaxAmount, string SGSTTaxAmount, string CGSTOthTaxAmount, string SGSTOthTaxAmount, string BookingTimeSlotId, string BoatPremiumStatus, string BoatCount)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BoatBookingMultiple", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", QueryType.ToString());
                cmd.Parameters.AddWithValue("@BookingId", BookingId.ToString());
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.ToString(), objEnglishDate));
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.ToString());

                cmd.Parameters.AddWithValue("@BookingPin", BookingPin.ToString());
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId.ToString());
                cmd.Parameters.AddWithValue("@CustomerMobileNo", CustomerMobileNo.ToString());
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName.ToString());
                cmd.Parameters.AddWithValue("@CustomerAddress", CustomerAddress.ToString());

                if (CustomerGSTNo == "")
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString());
                }

                cmd.Parameters.AddWithValue("@PremiumStatus", PremiumStatus.ToString());

                cmd.Parameters.AddWithValue("@NoOfPass", NoOfPass.ToString());
                cmd.Parameters.AddWithValue("@NoOfChild", NoOfChild.ToString());
                cmd.Parameters.AddWithValue("@NoOfInfant", NoOfInfant.ToString());
                cmd.Parameters.AddWithValue("@OfferId", OfferId.ToString());

                cmd.Parameters.AddWithValue("@InitBillAmount", InitBillAmount.ToString());
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType.ToString());
                cmd.Parameters.AddWithValue("@ActualBillAmount", ActualBillAmount.ToString());
                cmd.Parameters.AddWithValue("@Status", Status.ToString());
                cmd.Parameters.AddWithValue("@BoatTypeId", BoatTypeId.ToString());

                cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSeaterId.ToString());
                cmd.Parameters.AddWithValue("@BookingDuration", BookingDuration.ToString());
                cmd.Parameters.AddWithValue("@InitBoatCharge", InitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@InitRowerCharge", InitRowerCharge.ToString());
                cmd.Parameters.AddWithValue("@BoatDeposit", BoatDeposit.ToString());

                // cmd.Parameters.AddWithValue("@TaxDetails", TaxDetails.ToString());
                cmd.Parameters.AddWithValue("@InitOfferAmount", InitOfferAmount.ToString());
                cmd.Parameters.AddWithValue("@InitNetAmount", InitNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy.ToString());

                // Other Service Booking
                cmd.Parameters.AddWithValue("@OthServiceStatus", OthServiceStatus.ToString());
                cmd.Parameters.AddWithValue("@OthServiceId", OthServiceId.ToString());
                cmd.Parameters.AddWithValue("@OthChargePerItem", OthChargePerItem.ToString());
                cmd.Parameters.AddWithValue("@OthNoOfItems", OthNoOfItems.ToString());

                //  cmd.Parameters.AddWithValue("@OthTaxDetails", OthTaxDetails.ToString());
                cmd.Parameters.AddWithValue("@OthNetAmount", OthNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BFDInitBoatCharge", BFDInitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@BFDInitNetAmount", BFDInitNetAmount.ToString());
                cmd.Parameters.AddWithValue("@BFDGST", BFDGST.ToString());

                if (CollectedAmount.ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@CollectedAmount", CollectedAmount.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CollectedAmount", CollectedAmount.ToString());
                }

                if (BalanceAmount.ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@BalanceAmount", BalanceAmount.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BalanceAmount", BalanceAmount.ToString());
                }

                cmd.Parameters.AddWithValue("@BookingTimeSlotId", BookingTimeSlotId.ToString());
                cmd.Parameters.AddWithValue("@BookingBlockId", BookingBlockId.ToString());

                //cmd.Parameters.AddWithValue("@TaxDetails", TaxDetails.ToString());
                //cmd.Parameters.AddWithValue("@TaxAmount", TaxAmount.ToString());
                //cmd.Parameters.AddWithValue("@OthTaxDetails", OthTaxDetails.ToString());
                //cmd.Parameters.AddWithValue("@OthTaxAmount", OthTaxAmount.ToString());

                cmd.Parameters.AddWithValue("@CGSTTaxAmount", CGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTTaxAmount", SGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@CGSTOthTaxAmount", CGSTOthTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTOthTaxAmount", SGSTOthTaxAmount.ToString());

                cmd.Parameters.AddWithValue("@LoopCount", Countslotids.ToString());
                cmd.Parameters.AddWithValue("@BoatPremiumStatus", BoatPremiumStatus.ToString());
                cmd.Parameters.AddWithValue("@BoatCount", BoatCount.ToString());


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

            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }


        /// <summary>
        /// Created By Vediyappan
        /// Created Date : 27-08-2021
        /// </summary>
        /// <param name="BTD"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetBoatAllTariffDetail")]
        public IHttpActionResult GetBoatAllTariffDetail([FromBody] BoatTariffDetail BTD)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (BTD.BoatHouseId != "")
                {
                    sQuery = "SELECT BRM.BoatTypeId,BT.BoatType, BRM.BoatSeaterId, BS.SeaterType, BRM.BoatHouseId, BRM.BoatHouseName, BRM.BoatMinTotAmt AS 'WDBoatMinTotAmt',"
                          + " BRM.RowerMinCharge AS 'WDRowerMinCharge', BRM.BoatMinCharge AS 'WDBoatMinCharge', BRM.BoatMinTaxAmt AS 'WDBoatMinTaxAmt',"
                          + " BRM.WEBoatMinTotAmt, BRM.WERowerMinCharge, BRM.WEBoatMinCharge, BRM.WEBoatMinTaxAmt, BRM.PerHeadApplicable, BRM.IWDBoatMinTotAmt,"
                          + " BRM.IWDRowerMinCharge, BRM.IWDBoatMinCharge, BRM.IWDBoatMinTaxAmt, BRM.IWEBoatMinTotAmt, BRM.IWERowerMinCharge, BRM.IWEBoatMinCharge,"
                          + " BRM.IWEBoatMinTaxAmt, BRM.BoatPremTotAmt AS 'ECBoatPremTotAmt', BRM.BoatPremMinCharge AS 'ECBoatPremMinCharge', "
                          + " BRM.RowerPremMinCharge AS 'ECRowerPremMinCharge', BRM.BoatPremTaxAmt AS 'ECBoatPremTaxAmt', BRM.DepositType, BRM.Deposit "
                          + " FROM BoatRateMaster AS BRM"
                          + " LEFT JOIN BoatTypes AS BT ON BRM.BoatTypeId = BT.BoatTypeId AND BRM.BoatHouseId = BT.BoatHouseId"
                          + " LEFT JOIN BoatSeat AS BS ON BRM.BoatSeaterId = BS.BoatSeaterId AND BRM.BoatHouseId = BS.BoatHouseId"
                          + " WHERE BRM.BoatHouseId = @BoatHouseId AND BRM.ActiveStatus = 'A'";

                    List<BoatTariffDetail> li = new List<BoatTariffDetail>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BTD.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatTariffDetail BT = new BoatTariffDetail();

                            BT.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BT.BoatType = dt.Rows[i]["BoatType"].ToString();
                            BT.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BT.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            BT.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            BT.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();

                            BT.WDBoatMinTotAmt = dt.Rows[i]["WDBoatMinTotAmt"].ToString();
                            BT.WDRowerMinCharge = dt.Rows[i]["WDRowerMinCharge"].ToString();
                            BT.WDBoatMinCharge = dt.Rows[i]["WDBoatMinCharge"].ToString();
                            BT.WDBoatMinTaxAmt = dt.Rows[i]["WDBoatMinTaxAmt"].ToString();

                            BT.WEBoatMinTotAmt = dt.Rows[i]["WEBoatMinTotAmt"].ToString();
                            BT.WERowerMinCharge = dt.Rows[i]["WERowerMinCharge"].ToString();
                            BT.WEBoatMinCharge = dt.Rows[i]["WEBoatMinCharge"].ToString();
                            BT.WEBoatMinTaxAmt = dt.Rows[i]["WEBoatMinTaxAmt"].ToString();

                            BT.PerHeadApplicable = dt.Rows[i]["PerHeadApplicable"].ToString();

                            BT.IWDBoatMinTotAmt = dt.Rows[i]["IWDBoatMinTotAmt"].ToString();
                            BT.IWDRowerMinCharge = dt.Rows[i]["IWDRowerMinCharge"].ToString();
                            BT.IWDBoatMinCharge = dt.Rows[i]["IWDBoatMinCharge"].ToString();
                            BT.IWDBoatMinTaxAmt = dt.Rows[i]["IWDBoatMinTaxAmt"].ToString();

                            BT.IWEBoatMinTotAmt = dt.Rows[i]["IWEBoatMinTotAmt"].ToString();
                            BT.IWERowerMinCharge = dt.Rows[i]["IWERowerMinCharge"].ToString();
                            BT.IWEBoatMinCharge = dt.Rows[i]["IWEBoatMinCharge"].ToString();
                            BT.IWEBoatMinTaxAmt = dt.Rows[i]["IWEBoatMinTaxAmt"].ToString();

                            BT.ECBoatPremTotAmt = dt.Rows[i]["ECBoatPremTotAmt"].ToString();
                            BT.ECBoatPremMinCharge = dt.Rows[i]["ECBoatPremMinCharge"].ToString();
                            BT.ECRowerPremMinCharge = dt.Rows[i]["ECRowerPremMinCharge"].ToString();
                            BT.ECBoatPremTaxAmt = dt.Rows[i]["ECBoatPremTaxAmt"].ToString();

                            BT.DepositType = dt.Rows[i]["DepositType"].ToString();
                            BT.Deposit = dt.Rows[i]["Deposit"].ToString();

                            li.Add(BT);

                        }

                        BoatTariffDetailList ConfList = new BoatTariffDetailList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BoatTariffDetailResponse ConfRes = new BoatTariffDetailResponse
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatTariffDetailResponse ConfRes = new BoatTariffDetailResponse
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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
        /// Created By Vediyappan
        /// Created Date : 27-08-2021
        /// </summary>
        /// <param name="BHWS"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetBoatHouseWeekDayEndDetail")]
        public IHttpActionResult GetBoatHouseWeekDayEndDetail([FromBody] WeekDayClass BHWS)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (BHWS.BoatHouseId != "")
                {
                    sQuery = " SELECT BoatHouseId, WeekDays, WeekDayDesc, HolidayDate, HolidayDesc FROM BHWeekDaysMaster"
                           + " WHERE BoatHouseId = @BoatHouseId ORDER BY UniqueId ASC";

                    List<WeekDayClass> li = new List<WeekDayClass>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHWS.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            WeekDayClass BWDS = new WeekDayClass();

                            BWDS.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            BWDS.WeekDays = dt.Rows[i]["WeekDays"].ToString();
                            BWDS.WeekDayDesc = dt.Rows[i]["WeekDayDesc"].ToString();
                            BWDS.HolidayDate = dt.Rows[i]["HolidayDate"].ToString();
                            BWDS.HolidayDesc = dt.Rows[i]["HolidayDesc"].ToString();

                            li.Add(BWDS);
                        }

                        WeekDayClasslList ConfList = new WeekDayClasslList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        WeekDayClassResponse ConfRes = new WeekDayClassResponse
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatTariffDetailResponse ConfRes = new BoatTariffDetailResponse
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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
        /// Created By : Abhinaya
        /// Created Date: 08-09-2021
        /// </summary>
        /// <param name="BoatTest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatBookingBulk")]
        public async Task<IHttpActionResult> BoatBookingBulk([FromBody] BoatBookingNew BoatTest)
        {
            var i = 0;
            if (BoatTest.Countslotids == null)
                return NotFound();
            string[] test;
            test = BoatTest.BoatSeaterId.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string BoatCount = Convert.ToString(test.Count());
            var result = new List<string>();
            foreach (var id in BoatTest.Countslotids)
            {

                result.Add(await BoatBookingDepartmentBulk(id, BoatTest.QueryType, BoatTest.BookingDate, BoatTest.BookingId, BoatTest.BoatHouseId, BoatTest.BoatHouseName,
                    BoatTest.BookingPin, BoatTest.CustomerId, BoatTest.CustomerMobileNo, BoatTest.CustomerName, BoatTest.CustomerAddress, BoatTest.CustomerGSTNo,
                    BoatTest.PremiumStatus, BoatTest.NoOfPass, BoatTest.NoOfChild, BoatTest.NoOfInfant, BoatTest.OfferId, BoatTest.InitBillAmount, BoatTest.PaymentType,
                    BoatTest.ActualBillAmount, BoatTest.Status, BoatTest.BoatTypeId[i], BoatTest.BoatSeaterId[i], BoatTest.BookingDuration[i], BoatTest.InitBoatCharge[i],
                    BoatTest.InitRowerCharge[i], BoatTest.BoatDeposit[i], BoatTest.InitOfferAmount[i], BoatTest.InitNetAmount[i], BoatTest.BookingMedia,
                    BoatTest.CreatedBy[i], BoatTest.OthServiceStatus[i], BoatTest.OthServiceId[i], BoatTest.OthChargePerItem[i], BoatTest.OthNoOfItems[i],
                    BoatTest.OthNetAmount[i], BoatTest.BFDInitBoatCharges[i], BoatTest.BFDInitNetAmounts[i], BoatTest.BFDGSTs[i], BoatTest.CollectedAmount, BoatTest.BalanceAmount,
                    BoatTest.BookingBlockId[i], BoatTest.CGSTTaxAmount[i], BoatTest.SGSTTaxAmount[i], BoatTest.CGSTOthTaxAmount[i], BoatTest.SGSTOthTaxAmount[i],
                    BoatTest.BookingTimeSlotId[i], BoatTest.BoatPremiumStatus[i], BoatCount));
                i++;
            }
            BoatBookingResponse BookingSlot = new BoatBookingResponse
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }

        public async Task<dynamic> BoatBookingDepartmentBulk(int Countslotids, string QueryType, string BookingDate, string BookingId, int BoatHouseId, string BoatHouseName, string BookingPin,
          int CustomerId, string CustomerMobileNo, string CustomerName, string CustomerAddress, string CustomerGSTNo, string PremiumStatus, int NoOfPass, int NoOfChild,
          int NoOfInfant, int OfferId, decimal InitBillAmount, int PaymentType, decimal ActualBillAmount, string Status, string BoatTypeId, string BoatSeaterId,
          string BookingDuration, string InitBoatCharge, string InitRowerCharge, string BoatDeposit, string InitOfferAmount, string InitNetAmount,
          string BookingMedia, string CreatedBy, string OthServiceStatus, string OthServiceId, string OthChargePerItem, string OthNoOfItems,
           string OthNetAmount, string BFDInitBoatCharge, string BFDInitNetAmount, string BFDGST, decimal CollectedAmount, decimal BalanceAmount, string BookingBlockId,
           string CGSTTaxAmount, string SGSTTaxAmount, string CGSTOthTaxAmount, string SGSTOthTaxAmount, string BookingTimeSlotId, string BoatPremiumStatus, string BoatCount)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("BoatBookingMultiple", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", QueryType.ToString());
                cmd.Parameters.AddWithValue("@BookingId", BookingId.ToString());
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.ToString(), objEnglishDate));
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.ToString());

                cmd.Parameters.AddWithValue("@BookingPin", BookingPin.ToString());
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId.ToString());
                cmd.Parameters.AddWithValue("@CustomerMobileNo", CustomerMobileNo.ToString());
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName.ToString());
                cmd.Parameters.AddWithValue("@CustomerAddress", CustomerAddress.ToString());

                if (CustomerGSTNo == "")
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString());
                }

                cmd.Parameters.AddWithValue("@PremiumStatus", PremiumStatus.ToString());

                cmd.Parameters.AddWithValue("@NoOfPass", NoOfPass.ToString());
                cmd.Parameters.AddWithValue("@NoOfChild", NoOfChild.ToString());
                cmd.Parameters.AddWithValue("@NoOfInfant", NoOfInfant.ToString());
                cmd.Parameters.AddWithValue("@OfferId", OfferId.ToString());

                cmd.Parameters.AddWithValue("@InitBillAmount", InitBillAmount.ToString());
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType.ToString());
                cmd.Parameters.AddWithValue("@ActualBillAmount", ActualBillAmount.ToString());
                cmd.Parameters.AddWithValue("@Status", Status.ToString());
                cmd.Parameters.AddWithValue("@BoatTypeId", BoatTypeId.ToString());

                cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSeaterId.ToString());
                cmd.Parameters.AddWithValue("@BookingDuration", BookingDuration.ToString());
                cmd.Parameters.AddWithValue("@InitBoatCharge", InitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@InitRowerCharge", InitRowerCharge.ToString());
                cmd.Parameters.AddWithValue("@BoatDeposit", BoatDeposit.ToString());

                // cmd.Parameters.AddWithValue("@TaxDetails", TaxDetails.ToString());
                cmd.Parameters.AddWithValue("@InitOfferAmount", InitOfferAmount.ToString());
                cmd.Parameters.AddWithValue("@InitNetAmount", InitNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy.ToString());

                // Other Service Booking
                cmd.Parameters.AddWithValue("@OthServiceStatus", OthServiceStatus.ToString());
                cmd.Parameters.AddWithValue("@OthServiceId", OthServiceId.ToString());
                cmd.Parameters.AddWithValue("@OthChargePerItem", OthChargePerItem.ToString());
                cmd.Parameters.AddWithValue("@OthNoOfItems", OthNoOfItems.ToString());

                //  cmd.Parameters.AddWithValue("@OthTaxDetails", OthTaxDetails.ToString());
                cmd.Parameters.AddWithValue("@OthNetAmount", OthNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BFDInitBoatCharge", BFDInitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@BFDInitNetAmount", BFDInitNetAmount.ToString());
                cmd.Parameters.AddWithValue("@BFDGST", BFDGST.ToString());

                if (CollectedAmount.ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@CollectedAmount", CollectedAmount.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CollectedAmount", CollectedAmount.ToString());
                }

                if (BalanceAmount.ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@BalanceAmount", BalanceAmount.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BalanceAmount", BalanceAmount.ToString());
                }

                cmd.Parameters.AddWithValue("@BookingTimeSlotId", BookingTimeSlotId.ToString());
                cmd.Parameters.AddWithValue("@BookingBlockId", BookingBlockId.ToString());

                //cmd.Parameters.AddWithValue("@TaxDetails", TaxDetails.ToString());
                //cmd.Parameters.AddWithValue("@TaxAmount", TaxAmount.ToString());
                //cmd.Parameters.AddWithValue("@OthTaxDetails", OthTaxDetails.ToString());
                //cmd.Parameters.AddWithValue("@OthTaxAmount", OthTaxAmount.ToString());

                cmd.Parameters.AddWithValue("@CGSTTaxAmount", CGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTTaxAmount", SGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@CGSTOthTaxAmount", CGSTOthTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTOthTaxAmount", SGSTOthTaxAmount.ToString());

                cmd.Parameters.AddWithValue("@LoopCount", Countslotids.ToString());
                cmd.Parameters.AddWithValue("@BoatPremiumStatus", BoatPremiumStatus.ToString());
                cmd.Parameters.AddWithValue("@BoatCount", BoatCount.ToString());


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
        /// Created By : Imran
        /// Created Date : 17-09-2021
        /// </summary>
        /// <param name="BoatTest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("OfflineBoatBookingService")]
        public async Task<IHttpActionResult> OfflineBoatBookingService([FromBody] BoatBookingNew BoatTest)
        {
            var i = 0;
            if (BoatTest.Countslotids == null)
                return NotFound();
            string[] test;
            test = BoatTest.BoatSeaterId.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string BoatCount = Convert.ToString(test.Count());
            var result = new List<string>();
            foreach (var id in BoatTest.Countslotids)
            {

                result.Add(await OfflineBoatBookingDepartment(id, BoatTest.QueryType, BoatTest.BookingDate, BoatTest.BookingId, BoatTest.BoatHouseId, BoatTest.BoatHouseName,
                    BoatTest.BookingPin, BoatTest.CustomerId, BoatTest.CustomerMobileNo, BoatTest.CustomerName, BoatTest.CustomerAddress, BoatTest.CustomerGSTNo,
                    BoatTest.PremiumStatus, BoatTest.NoOfPass, BoatTest.NoOfChild, BoatTest.NoOfInfant, BoatTest.OfferId, BoatTest.InitBillAmount, BoatTest.PaymentType,
                    BoatTest.ActualBillAmount, BoatTest.Status, BoatTest.BoatTypeId[i], BoatTest.BoatSeaterId[i], BoatTest.BookingDuration[i], BoatTest.InitBoatCharge[i],
                    BoatTest.InitRowerCharge[i], BoatTest.BoatDeposit[i], BoatTest.InitOfferAmount[i], BoatTest.InitNetAmount[i], BoatTest.BookingMedia,
                    BoatTest.CreatedBy[i], BoatTest.OthServiceStatus[i], BoatTest.OthServiceId[i], BoatTest.OthChargePerItem[i], BoatTest.OthNoOfItems[i],
                    BoatTest.OthNetAmount[i], BoatTest.BFDInitBoatCharge, BoatTest.BFDInitNetAmount, BoatTest.BFDGST, BoatTest.CollectedAmount, BoatTest.BalanceAmount,
                    BoatTest.BookingBlockId[i], BoatTest.CGSTTaxAmount[i], BoatTest.SGSTTaxAmount[i], BoatTest.CGSTOthTaxAmount[i], BoatTest.SGSTOthTaxAmount[i],
                    BoatTest.BookingTimeSlotId[i], BoatTest.BoatPremiumStatus[i], BoatCount));
                i++;
            }
            BoatBookingResponse BookingSlot = new BoatBookingResponse
            {
                Response = result,
                StatusCode = 1
            };
            return Ok(BookingSlot);
        }

        public async Task<dynamic> OfflineBoatBookingDepartment(int Countslotids, string QueryType, string BookingDate, string BookingId, int BoatHouseId, string BoatHouseName, string BookingPin,
          int CustomerId, string CustomerMobileNo, string CustomerName, string CustomerAddress, string CustomerGSTNo, string PremiumStatus, int NoOfPass, int NoOfChild,
          int NoOfInfant, int OfferId, decimal InitBillAmount, int PaymentType, decimal ActualBillAmount, string Status, string BoatTypeId, string BoatSeaterId,
          string BookingDuration, string InitBoatCharge, string InitRowerCharge, string BoatDeposit, string InitOfferAmount, string InitNetAmount,
          string BookingMedia, string CreatedBy, string OthServiceStatus, string OthServiceId, string OthChargePerItem, string OthNoOfItems,
           string OthNetAmount, string BFDInitBoatCharge, string BFDInitNetAmount, string BFDGST, decimal CollectedAmount, decimal BalanceAmount, string BookingBlockId,
           string CGSTTaxAmount, string SGSTTaxAmount, string CGSTOthTaxAmount, string SGSTOthTaxAmount, string BookingTimeSlotId, string BoatPremiumStatus, string BoatCount)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("OfflineBoatBooking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", QueryType.ToString());
                cmd.Parameters.AddWithValue("@BookingId", BookingId.ToString());
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BookingDate.ToString(), objEnglishDate));
                cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", BoatHouseName.ToString());

                cmd.Parameters.AddWithValue("@BookingPin", BookingPin.ToString());
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId.ToString());
                cmd.Parameters.AddWithValue("@CustomerMobileNo", CustomerMobileNo.ToString());
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName.ToString());
                cmd.Parameters.AddWithValue("@CustomerAddress", CustomerAddress.ToString());

                if (CustomerGSTNo == "")
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerGSTNo", CustomerGSTNo.ToString());
                }

                cmd.Parameters.AddWithValue("@PremiumStatus", PremiumStatus.ToString());

                cmd.Parameters.AddWithValue("@NoOfPass", NoOfPass.ToString());
                cmd.Parameters.AddWithValue("@NoOfChild", NoOfChild.ToString());
                cmd.Parameters.AddWithValue("@NoOfInfant", NoOfInfant.ToString());
                cmd.Parameters.AddWithValue("@OfferId", OfferId.ToString());

                cmd.Parameters.AddWithValue("@InitBillAmount", InitBillAmount.ToString());
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType.ToString());
                cmd.Parameters.AddWithValue("@ActualBillAmount", ActualBillAmount.ToString());
                cmd.Parameters.AddWithValue("@Status", Status.ToString());
                cmd.Parameters.AddWithValue("@BoatTypeId", BoatTypeId.ToString());

                cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSeaterId.ToString());
                cmd.Parameters.AddWithValue("@BookingDuration", BookingDuration.ToString());
                cmd.Parameters.AddWithValue("@InitBoatCharge", InitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@InitRowerCharge", InitRowerCharge.ToString());
                cmd.Parameters.AddWithValue("@BoatDeposit", BoatDeposit.ToString());

                // cmd.Parameters.AddWithValue("@TaxDetails", TaxDetails.ToString());
                cmd.Parameters.AddWithValue("@InitOfferAmount", InitOfferAmount.ToString());
                cmd.Parameters.AddWithValue("@InitNetAmount", InitNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BookingMedia", BookingMedia.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy.ToString());

                // Other Service Booking
                cmd.Parameters.AddWithValue("@OthServiceStatus", OthServiceStatus.ToString());
                cmd.Parameters.AddWithValue("@OthServiceId", OthServiceId.ToString());
                cmd.Parameters.AddWithValue("@OthChargePerItem", OthChargePerItem.ToString());
                cmd.Parameters.AddWithValue("@OthNoOfItems", OthNoOfItems.ToString());

                //  cmd.Parameters.AddWithValue("@OthTaxDetails", OthTaxDetails.ToString());
                cmd.Parameters.AddWithValue("@OthNetAmount", OthNetAmount.ToString());

                cmd.Parameters.AddWithValue("@BFDInitBoatCharge", BFDInitBoatCharge.ToString());
                cmd.Parameters.AddWithValue("@BFDInitNetAmount", BFDInitNetAmount.ToString());
                cmd.Parameters.AddWithValue("@BFDGST", BFDGST.ToString());

                if (CollectedAmount.ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@CollectedAmount", CollectedAmount.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CollectedAmount", CollectedAmount.ToString());
                }

                if (BalanceAmount.ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@BalanceAmount", BalanceAmount.ToString()).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BalanceAmount", BalanceAmount.ToString());
                }

                cmd.Parameters.AddWithValue("@BookingTimeSlotId", BookingTimeSlotId.ToString());
                cmd.Parameters.AddWithValue("@BookingBlockId", BookingBlockId.ToString());

                //cmd.Parameters.AddWithValue("@TaxDetails", TaxDetails.ToString());
                //cmd.Parameters.AddWithValue("@TaxAmount", TaxAmount.ToString());
                //cmd.Parameters.AddWithValue("@OthTaxDetails", OthTaxDetails.ToString());
                //cmd.Parameters.AddWithValue("@OthTaxAmount", OthTaxAmount.ToString());

                cmd.Parameters.AddWithValue("@CGSTTaxAmount", CGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTTaxAmount", SGSTTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@CGSTOthTaxAmount", CGSTOthTaxAmount.ToString());
                cmd.Parameters.AddWithValue("@SGSTOthTaxAmount", SGSTOthTaxAmount.ToString());

                cmd.Parameters.AddWithValue("@LoopCount", Countslotids.ToString());
                cmd.Parameters.AddWithValue("@BoatPremiumStatus", BoatPremiumStatus.ToString());
                cmd.Parameters.AddWithValue("@BoatCount", BoatCount.ToString());


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
        [Route("BoatTypeListForBoatBooking")]
        public IHttpActionResult BoatTypeListForBoatBooking([FromBody] NewBoatBooked btBook)
        {
            try
            {
                if (btBook.BoatHouseId != "")
                {
                    List<NewBoatBooked> li = new List<NewBoatBooked>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "BoatTypeListForBoatBooking");
                    cmd.Parameters.AddWithValue("@BoatHouseId", btBook.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(btBook.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(btBook.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@UserId", btBook.UserId);
                    cmd.Parameters.AddWithValue("@ServiceType", btBook.UserRole);
                    cmd.Parameters.AddWithValue("@PremiumStatus", btBook.PremiumStatus);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            NewBoatBooked bt = new NewBoatBooked();
                            bt.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            bt.BoatType = dt.Rows[i]["BoatType"].ToString();

                            li.Add(bt);
                        }

                        NewBoatBookedList btl = new NewBoatBookedList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(btl);
                    }

                    else
                    {
                        NewBoatBookedString Bts = new NewBoatBookedString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(Bts);
                    }
                }
                else
                {

                    NewBoatBookedString Vehicle = new NewBoatBookedString
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
        [Route("ScanRescheduling")]
        public IHttpActionResult ScanRescheduling([FromBody] TripSheet btBook)
        {
            try
            {
                if (btBook.BoatHouseId != "")
                {
                    List<TripSheet> li = new List<TripSheet>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "ScanRescheduling");
                    cmd.Parameters.AddWithValue("@BoatHouseId", btBook.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BookingId", btBook.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@Input1", btBook.BookingPin);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet bt = new TripSheet();
                            bt.BookingDate = dt.Rows[i]["BookingNewDate"].ToString();
                            bt.RStatus = dt.Rows[i]["Status"].ToString();
                            li.Add(bt);
                        }

                        TripSheetList btl = new TripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(btl);
                    }

                    else
                    {
                        TripSheetRes Bts = new TripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(Bts);
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
        /// Created By : Abhi
        /// Created Date : 23-09-2021
        /// </summary>
        /// <param name="PinDet"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatListHideShow")]
        public IHttpActionResult CommonReports([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                if (PinDet.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("BoatListHideShow", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", PinDet.UserId.ToString());

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
        /// Created By: Imran
        /// Created Date: 30-09-2021
        /// </summary>
        /// <param name="SB"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("SuccessOnlineBoatBookingAftrTran")]
        public async Task<IHttpActionResult> SuccessOnlineBoatBookingAftrTran([FromBody] OnlineBoatBooking SB)
        {
            string FinalResponse = string.Empty;
            int FinalStatusCode = 1;

            if (SB.OrderStatus.ToUpper().Trim() == "SUCCESS" || SB.OrderStatus.Trim() == "Success" || SB.OrderStatus.Trim() == "success")
            {
                FinalResponse = await GetBoatBookingTransactionDetails(FinalStatusCode, SB.TransactionNo.Trim(), SB.UserId.Trim(), SB.MobileNo.Trim(),
                SB.EmailId.Trim(), SB.BookingType.Trim(), SB.BookingMedia.Trim(), SB.BoatHouseId.ToString().Trim(), SB.ModuleType.Trim());
            }
            string[] sFinalResponse = FinalResponse.Split('~');
            string RB = sFinalResponse[0].ToString().Trim();
            if (RB != "Success")
            {
                FinalResponse = "Failure.";
                FinalStatusCode = 0;
            }
            OnlineBoatBookingStr TxMstr = new OnlineBoatBookingStr
            {
                Response = FinalResponse.Trim(),
                StatusCode = FinalStatusCode
            };
            return Ok(TxMstr);
        }

        //Newly Added
        //CreatedBy   : Vinitha.M
        //CreatedDate : 08-Nov-2021
        //Modified Date : 09-Nov-2021
        //Change Trip Sheet List Based on Date (Only for S-Admin)
        [HttpPost]
        [AllowAnonymous]
        [Route("ChangeBoatBookingSadmin")]
        public IHttpActionResult ChangeBoatBookingSadmin([FromBody] BookingBoardingPass Passes)
        {
            try
            {
                if (Passes.BoatHouseId != "" && Passes.BookingDate != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    string sQuery = "SELECT A.BookingId, A.BoatReferenceNo,A.BookingPin,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType,"
                    + " A.ActualBoatId, A.ActualBoatNum, E.RowerId,  E.RowerName, "
                    + " F.BoatMinTime + F.BoatGraceTime AS 'BoatDuration', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDuration', "
                    + " CONVERT(NVARCHAR(50), D.BookingDate, 103) AS 'BookingDate', "
                    + " CONVERT(VARCHAR, A.TripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripStartTime, 100),7) as TripStartTime, "
                    + " CONVERT(VARCHAR, A.TripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripEndTime, 100),7) as TripEndTime, "
                    + " RIGHT(CONVERT(VARCHAR, ExpectedTime, 100), 7) AS 'ExpectedTime', "
                    + " CASE when D.PremiumStatus = 'P' then 'Permium' else 'Normal' end as PremiumStatus, A.BoatDeposit, A.DepRefundAmount "
                    + " FROM BookingDtl AS A"
                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS E on A.RowerId = E.RowerId and A.BoatHouseId = E.BoatHouseId "
                    + " INNER JOIN BoatRateMaster AS F ON A.BoatHouseId = F.BoatHouseId AND A.BoatTypeId = F.BoatTypeId "
                    + " AND A.BoatSeaterId = F.BoatSeaterId AND F.ActiveStatus='A' "
                    + " WHERE A.BoatHouseId = @BoatHouseId"
                    + " AND CAST(D.BookingDate AS DATE) = @BookingDate  AND A.Status IN('B','R') AND D.Status IN ('B','R','P') "
                    + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL  and A.PremiumStatus not in ('I')";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                    cmd.Parameters["@BoatHouseId"].Value = Passes.BoatHouseId.Trim();
                    cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Passes.BookingDate.Trim(), objEnglishDate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BookingBoardingPass Boardingpass = new BookingBoardingPass();
                            Boardingpass.BookingId = dt.Rows[i]["BookingId"].ToString();
                            Boardingpass.BoatReferenceNum = dt.Rows[i]["BoatReferenceNo"].ToString();
                            Boardingpass.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            Boardingpass.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            Boardingpass.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            Boardingpass.BoatType = dt.Rows[i]["BoatType"].ToString();
                            Boardingpass.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            Boardingpass.SeatType = dt.Rows[i]["SeaterType"].ToString();
                            Boardingpass.ActualBoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            Boardingpass.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            Boardingpass.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            Boardingpass.Status = dt.Rows[i]["PremiumStatus"].ToString();
                            Boardingpass.RowerId = dt.Rows[i]["RowerId"].ToString();
                            Boardingpass.RowerName = dt.Rows[i]["RowerName"].ToString();
                            Boardingpass.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            Boardingpass.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            Boardingpass.BoatDuration = dt.Rows[i]["BoatDuration"].ToString();
                            Boardingpass.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            Boardingpass.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            Boardingpass.OldDepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                            li.Add(Boardingpass);
                        }

                        BookingBoardingPassList AvailList = new BookingBoardingPassList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(AvailList);
                    }

                    else
                    {
                        BookingBoardingPassRes availRes = new BookingBoardingPassRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(availRes);
                    }
                }
                else
                {
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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


        //*************************Suba*************************************
        //Newly Added
        //CreatedBy   : Imran
        //CreatedDate : 15-Nov-2021        
        //Service Wise Report Abstract Report

        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractServiceWiseCollection_Test")]
        public IHttpActionResult BoatwiseTripAbs_test([FromBody] BoatingReport Boating)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                string conditions1 = " WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                if (Boating.BoatTypeId != "0")
                {
                    conditions += " AND B.BoatTypeId = @BoatTypeId ";
                    conditions1 += " AND B.BoatTypeId = @BoatTypeId ";
                }
                if (Boating.PaymentType != "0")
                {
                    conditions += " AND C.PaymentType = @PaymentType ";
                    conditions1 += " AND C.PaymentType = @PaymentType ";
                }
                if (Boating.CreatedBy != "0")
                {
                    conditions += "AND C.CreatedBy =@CreatedBy ";
                    conditions1 += "AND C.CreatedBy =@CreatedBy ";
                }

                squery = "DECLARE @TAX DECIMAL(18, 2) "
                   + " SET @TAX = (SELECT TOP 1 TaxPercentage AS 'TAX' FROM TaxMaster WHERE ServiceName = '1' AND BoatHouseId = @BoatHouseId ) "
                   + " SELECT BookingDate, SUM(TotalCount)AS 'Count', Account AS 'Particulars', SUM(Amount)AS 'Amount', "
                   + " SUM(CGST) AS 'CGST', "
                   + " SUM(SGST) AS 'SGST', "
                   + " SUM(TotalAmount)AS 'TotalAmount', A.BoatType, A.orderid, A.BoatHouseId,A.BoatTypeId "
                   + " FROM "
                   + " (SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate', "
                   + " COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                   + " ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                   + " ((ISNULL(SUM(B.CGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'CGST', "
                   + " ((ISNULL(SUM(B.SGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'SGST', "
                   + " ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) + "
                   + " ((ISNULL(SUM(B.CGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) + "
                   + " ((ISNULL(SUM(B.SGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) + "
                   + " ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) + "
                   + " ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'TotalAmount', "
                   + " BT.BoatType, '0' as orderid, B.BoatHouseId,B.BoatTypeId "
                   + " FROM BookingDtl AS B "
                   + " INNER JOIN BookingHdr AS C ON B.BookingId = C.BookingId "
                   + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                   + " INNER JOIN BoatTypes AS BT ON B.BoatTypeId = BT.BoatTypeId AND B.BoatHouseId = BT.BoatHouseId "
                   + " " + conditions + " "
                   + " GROUP BY B.BookingId, CAST(C.BookingDate AS DATE),BT.BoatType,B.BoatHouseId,B.BoatTypeId) AS A "
                   + " GROUP BY BookingDate, Account, A.BoatType, A.orderid, A.BoatHouseId,A.BoatTypeId "
                   + " UNION ALL "
                   + " SELECT BookingDate, 0 AS 'Count', Account, SUM(Amount), SUM(CGST), SUM(SGST), SUM(TotalAmount), "
                   + " B.BoatType, B.orderid, B.BoatHouseId,B.BoatTypeId "
                   + " FROM "
                   + " ( "
                   + " SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate', "
                   + " COUNT(B.BookingId) AS 'TotalCount', 'Rower Charge' AS 'Account', "
                   + " ISNULL(CAST(SUM(B.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                   + " ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'CGST', "
                   + " ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'SGST', "
                   + " ISNULL(CAST(SUM(B.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'TotalAmount', "
                   + " BT.BoatType, '1' as orderid, B.BoatHouseId,B.BoatTypeId"
                   + " FROM BookingDtl AS B "
                   + " INNER JOIN BookingHdr AS C ON B.BookingId = C.BookingId "
                   + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                   + " INNER JOIN BoatTypes AS BT ON B.BoatTypeId = BT.BoatTypeId AND B.BoatHouseId = BT.BoatHouseId"
                   + " " + conditions1 + " "
                   + " GROUP BY CAST(C.BookingDate AS DATE),BT.BoatType,B.BoatHouseId,B.BoatTypeId "
                   + " ) "
                   + " AS B  "
                   + " GROUP BY BookingDate, Account, B.BoatType, B.orderid, B.BoatHouseId,B.BoatTypeId order by BoatType ASC, orderid ASC ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = Boating.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = Boating.BoatTypeId;
                cmd.Parameters["@PaymentType"].Value = Boating.PaymentType;
                cmd.Parameters["@CreatedBy"].Value = Boating.CreatedBy;
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Boating.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("RptAbstractServiceWiseCollectionMain_Test")]
        public IHttpActionResult BoatwiseTripAbsMain_test([FromBody] BoatingReport Boating)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate"
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                string conditions1 = " WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                if (Boating.BoatTypeId != "0")
                {
                    conditions += " AND B.BoatTypeId = @BoatTypeId ";
                    conditions1 += " AND B.BoatTypeId = @BoatTypeId ";
                }
                if (Boating.PaymentType != "0")
                {
                    conditions += " AND C.PaymentType = @PaymentType ";
                    conditions1 += " AND C.PaymentType = @PaymentType ";
                }
                if (Boating.CreatedBy != "0")
                {
                    conditions += "AND C.CreatedBy =@CreatedBy ";
                    conditions1 += "AND C.CreatedBy =@CreatedBy ";
                }

                squery = "DECLARE @TAX DECIMAL(18, 2) "
                         + "SET @TAX = (SELECT TOP 1 TaxPercentage AS 'TAX' FROM TaxMaster WHERE ServiceName = '1' AND BoatHouseId = @BoatHouseId )"
                         + " SELECT BookingDate, A.BoatType, A.BoatHouseId,A.BoatTypeId"
                         + " FROM "
                         + " (SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate',"
                         + " COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account',"
                         + " ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount',"
                         + " ((ISNULL(SUM(B.CGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'CGST',"
                         + " ((ISNULL(SUM(B.SGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'SGST',"
                         + " ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) + "
                         + " ((ISNULL(SUM(B.CGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) + "
                         + " ((ISNULL(SUM(B.SGSTTaxAmount), 0))) - ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) +"
                         + " ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) +"
                         + " ROUND((SUM(B.InitRowerCharge) * @TAX) / 100, 2) AS 'TotalAmount',"
                         + " BT.BoatType, B.BoatHouseId,B.BoatTypeId"
                         + " FROM BookingDtl AS B "
                         + " INNER JOIN BookingHdr AS C ON B.BookingId = C.BookingId "
                         + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                         + " INNER JOIN BoatTypes AS BT ON B.BoatTypeId = BT.BoatTypeId AND B.BoatHouseId = BT.BoatHouseId "
                         + " " + conditions + " "
                         + " GROUP BY B.BookingId, CAST(C.BookingDate AS DATE), BT.BoatType, B.BoatHouseId,B.BoatTypeId) AS A "
                         + " GROUP BY BookingDate, Account, A.BoatType, A.BoatHouseId ,A.BoatTypeId"
                         + "  order by BoatType ASC ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = Boating.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = Boating.BoatTypeId;
                cmd.Parameters["@PaymentType"].Value = Boating.PaymentType;
                cmd.Parameters["@CreatedBy"].Value = Boating.CreatedBy;
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Boating.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
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
        [Route("RptBoatingServiceWiseDetailed")]
        public IHttpActionResult RptBoatingServiceWiseDetailedNew([FromBody] BoatingReport Boating)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string sBookingDtl = string.Empty;
                string sBookingHdr = string.Empty;

                if (DateTime.Parse(Boating.FromDate.Trim(), objEnglishDate) == DateTime.Parse(Boating.ToDate.Trim(), objEnglishDate))
                {
                    if (CheckDate(Boating.FromDate.Trim()))
                    {
                        conditions = "WHERE  CAST(C.BookingDate AS DATE) "
                                   + " BETWEEN @FromDate "
                                   + " AND @ToDate "
                                   + " AND B.BoatHouseId = @BoatHouseId "
                                   + " AND C.BoatHouseId = @BoatHouseId "
                                   + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                        sBookingDtl = "BookingDtl";
                        sBookingHdr = "BookingHdr";
                    }
                    else
                    {
                        //string sFromDate = DateTime.Parse(Boating.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                        //string sToDate = DateTime.Parse(Boating.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                        conditions = " WHERE  C.BookingDate  BETWEEN @sFromDate AND @sToDate "
                                   + " AND B.BDate  BETWEEN @sFromDate AND @sToDate"
                                   + " AND B.BoatHouseId = @BoatHouseId "
                                   + " AND C.BoatHouseId = @BoatHouseId "
                                   + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                        sBookingDtl = "BookingDtlHistory";
                        sBookingHdr = "BookingHdrHistory";
                    }

                    if (Boating.BoatTypeId != "0")
                    {
                        conditions += " AND B.BoatTypeId = @BoatTypeId";
                    }
                    if (Boating.PaymentType != "0")
                    {
                        conditions += " AND C.PaymentType =@PaymentType";
                    }
                    if (Boating.CreatedBy != "0")
                    {
                        conditions += " AND C.CreatedBy = @CreatedBy";
                    }
                    squery = "  DECLARE @CgstTax DECIMAL(18, 2);"
                           + "  DECLARE @SgstTax DECIMAL(18, 2);"
                           + "  SET @CgstTax = (SELECT  TaxPercentage FROM TaxMaster WHERE TaxDescription = 'CGST' AND ServiceName = 1 AND ActiveStatus='A' AND BoatHouseId = @BoatHouseId ) "
                           + "  SET @SgstTax = (SELECT TaxPercentage FROM TaxMaster WHERE TaxDescription = 'SGST' AND ServiceName = 1 AND ActiveStatus='A' AND BoatHouseId = @BoatHouseId ) "
                           + " SELECT UniqueId, CreatedBy, BookingId AS 'BookingId', BookingDate, SUM(TotalCount) AS 'Count', Account AS 'Particulars', "
                           + " 2 AS 'Ordervalue', SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                           + " SUM(TotalAmount) AS 'TotalAmount' "
                           + " FROM "
                           + " ( "
                           + " SELECT C.UniqueId AS 'UniqueId', B.BookingId AS 'BookingId', CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100),7) AS 'BookingDate', COUNT(B.BookingId) AS 'TotalCount', "
                           + " 'Boat Revenue' AS 'Account', "
                           + " ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18,2)), 0) AS 'Amount', "

                           + " SUM(B.CGSTTaxAmount)  AS 'CGST', "
                           + " SUM(B.SGSTTaxAmount)  AS 'SGST', "
                           + " SUM(B.InitBoatCharge + B.CGSTTaxAmount + B.SGSTTaxAmount)  AS 'TotalAmount', "

                           + " C.Createdby AS 'CreatedBy' FROM "
                           + " " + sBookingDtl + " AS B "
                           + " INNER JOIN  " + sBookingHdr + " AS C ON B.BookingId = C.BookingId AND B.BookingId = C.BookingId "
                           + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                           + " " + conditions + " "
                           + " GROUP BY B.BookingId, BookingDate, C.UniqueId, C.Createdby "
                           + " ) "
                           + " AS A "
                           + " GROUP BY BookingId, BookingDate, Account, CreatedBy, UniqueId "
                           + " ORDER BY CreatedBy, UniqueId, BookingDate ASC ";
                }
                else
                {
                    if (CheckDate(Boating.FromDate.Trim()) && CheckDate(Boating.ToDate.Trim()))
                    {
                        conditions = "WHERE  CAST(C.BookingDate AS DATE) "
                                   + " BETWEEN @FromDate AND @ToDate "
                                   + " AND B.BoatHouseId =@BoatHouseId "
                                   + " AND C.BoatHouseId = @BoatHouseId "
                                   + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                        if (Boating.BoatTypeId != "0")
                        {
                            conditions += " AND B.BoatTypeId = @BoatTypeId ";
                        }
                        if (Boating.PaymentType != "0")
                        {
                            conditions += " AND C.PaymentType = @PaymentType ";
                        }
                        if (Boating.CreatedBy != "0")
                        {
                            conditions += " AND C.CreatedBy = @CreatedBy ";
                        }

                        squery = "  DECLARE @CgstTax DECIMAL(18, 2); "
                               + "  DECLARE @SgstTax DECIMAL(18, 2); "
                               + "  SET @CgstTax = (SELECT  TaxPercentage FROM TaxMaster WHERE TaxDescription='CGST' AND ServiceName=1 AND ActiveStatus='A' AND BoatHouseId = @BoatHouseId ) "
                               + "  SET @SgstTax = (SELECT TaxPercentage FROM TaxMaster WHERE TaxDescription='SGST' AND ServiceName=1 AND ActiveStatus='A' AND BoatHouseId = @BoatHouseId ) "
                               + "  (SELECT UniqueId, CreatedBy, BookingId AS 'BookingId', BookingDate, SUM(TotalCount) AS 'Count', "
                               + "  Account AS 'Particulars', SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                               + "  SUM(TotalAmount) AS 'TotalAmount', Ordervalue FROM "
                               + "  (SELECT C.UniqueId AS 'UniqueId', "
                               + "  B.BookingId AS 'BookingId', CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) as 'BookingDate', "
                               + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                               + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                               + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                               + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0) + ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', "
                               + "  C.Createdby AS 'CreatedBy', 0 AS 'Ordervalue'  FROM  BookingDtl AS B  INNER JOIN  BookingHdr AS C ON B.BookingId = C.BookingId "
                               + "  AND B.BookingId = C.BookingId  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                               + " " + conditions + " "
                               + "  GROUP BY  BookingDate, B.BookingId, "
                               + "  C.UniqueId, C.Createdby)  AS A "
                               + "  GROUP BY BookingDate,Ordervalue, Account,BookingId,   CreatedBy, UniqueId ) "
                               + "  Union All "
                               + "  SELECT  '' AS 'UniqueId', '' AS 'CreatedBy', '' AS 'BookingId', "
                               + "  A.BookingDate, SUM(TotalCount) AS 'Count',  'Total' AS 'Particulars', "
                               + "  SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                               + "  SUM(TotalAmount) AS 'TotalAmount',Ordervalue FROM "
                               + "  (SELECT  CONVERT(VARCHAR(50), C.BookingDate, 103) + ' ' + '13:00PM' as 'BookingDate', "
                               + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                               + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                               + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                               + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitBoatCharge) * (@SgstTax)/ 100), 0)  + ISNULL((SUM(B.InitRowerCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', 1 AS 'Ordervalue' "
                               + "  FROM  BookingDtl AS B INNER JOIN  BookingHdr AS C ON B.BookingId = C.BookingId  AND B.BookingId = C.BookingId "
                               + "  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                               + " " + conditions + " "
                               + "  GROUP BY  BookingDate )  AS A "
                               + "  GROUP BY  BookingDate,Ordervalue "
                               + "  ORDER BY BookingDate, Ordervalue ";
                    }
                    else if (!CheckDate(Boating.FromDate.Trim()) && !CheckDate(Boating.ToDate.Trim()))
                    {
                        string sFromDate = DateTime.Parse(Boating.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                        string sToDate = DateTime.Parse(Boating.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                        conditions = " WHERE  C.BookingDate  BETWEEN @sFromDate AND  @sToDate  "
                                   + " AND B.BDate  BETWEEN  @sFromDate  AND  @sToDate "
                                   + " AND B.BoatHouseId = @BoatHouseId  AND C.BoatHouseId =@BoatHouseId "
                                   + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                        if (Boating.BoatTypeId != "0")
                        {
                            conditions += " AND B.BoatTypeId = @BoatTypeId ";
                        }
                        if (Boating.PaymentType != "0")
                        {
                            conditions += " AND C.PaymentType =@PaymentType ";
                        }
                        if (Boating.CreatedBy != "0")
                        {
                            conditions += " AND C.CreatedBy = @CreatedBy  ";
                        }

                        squery = "  DECLARE @CgstTax DECIMAL(18, 2); "
                                + "  DECLARE @SgstTax DECIMAL(18, 2); "
                                + "  SET @CgstTax = (SELECT  TaxPercentage FROM TaxMaster WHERE TaxDescription='CGST' AND ServiceName=1 AND ActiveStatus='A' AND BoatHouseId = @BoatHouseId ) "
                                + "  SET @SgstTax = (SELECT TaxPercentage FROM TaxMaster WHERE TaxDescription='SGST' AND ServiceName=1 AND ActiveStatus='A' AND BoatHouseId = @BoatHouseId ) "
                                + "  (SELECT UniqueId, CreatedBy, BookingId AS 'BookingId', BookingDate, SUM(TotalCount) AS 'Count', "
                                + "  Account AS 'Particulars', SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                                + "  SUM(TotalAmount) AS 'TotalAmount', Ordervalue FROM "
                                + "  ( "
                                + "  SELECT C.UniqueId AS 'UniqueId', "
                                + "  B.BookingId AS 'BookingId', CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) as 'BookingDate', "
                                + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                                + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                                + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                                + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                                + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                                + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                                + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                                + "  ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0) + ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0)  + "
                                + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', "
                                + "  C.Createdby AS 'CreatedBy', 0 AS 'Ordervalue'  FROM  BookingDtlHistory AS B  INNER JOIN  BookingHdrHistory AS C ON B.BookingId = C.BookingId "
                                + "  AND B.BookingId = C.BookingId  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                                + " " + conditions + " "
                                + "  GROUP BY  BookingDate, B.BookingId,  C.UniqueId, C.Createdby "
                                + " )  AS A "
                                + "  GROUP BY BookingDate,Ordervalue, Account,BookingId,   CreatedBy, UniqueId ) "
                                + "  Union All "
                                + "  SELECT  '' AS 'UniqueId', '' AS 'CreatedBy', '' AS 'BookingId', "
                                + "  A.BookingDate, SUM(TotalCount) AS 'Count',  'Total' AS 'Particulars', "
                                + "  SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                                + "  SUM(TotalAmount) AS 'TotalAmount',Ordervalue FROM "
                                + "  (SELECT  CONVERT(VARCHAR(50), C.BookingDate, 103) + ' ' + '13:00PM' as 'BookingDate', "
                                + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                                + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                                + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                                + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                                + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                                + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                                + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                                + "  ISNULL((SUM(B.InitBoatCharge) * (@SgstTax)/ 100), 0)  + ISNULL((SUM(B.InitRowerCharge) * (@CgstTax)/ 100), 0)  + "
                                + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', 1 AS 'Ordervalue' "
                                + "  FROM  BookingDtlHistory AS B INNER JOIN  BookingHdrHistory AS C ON B.BookingId = C.BookingId  AND B.BookingId = C.BookingId "
                                + "  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                                + " " + conditions + " "
                                + "  GROUP BY  BookingDate )  AS A "
                                + "  GROUP BY  BookingDate,Ordervalue "
                                + "  ORDER BY BookingDate, Ordervalue ";
                    }
                    else
                    {
                        string sConditionHistory = string.Empty;
                        // string sFromDate = DateTime.Parse(Boating.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                        // string sToDate = DateTime.Parse(Boating.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                        conditions = "WHERE  CAST(C.BookingDate AS DATE) "
                                   + " BETWEEN @FromDate "
                                   + " AND @ToDate "
                                   + " AND B.BoatHouseId = @BoatHouseId "
                                   + " AND C.BoatHouseId = @BoatHouseId "
                                   + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";


                        sConditionHistory = " WHERE  C.BookingDate  BETWEEN @sFromDate AND @sToDate "
                                   + " AND B.BDate  BETWEEN @sFromDate AND @sToDate"
                                   + " AND B.BoatHouseId = @BoatHouseId "
                                   + " AND C.BoatHouseId = @BoatHouseId"
                                   + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                        if (Boating.BoatTypeId != "0")
                        {
                            conditions += " AND B.BoatTypeId = @BoatTypeId";
                            sConditionHistory += " AND B.BoatTypeId = @BoatTypeId";
                        }
                        if (Boating.PaymentType != "0")
                        {
                            conditions += " AND C.PaymentType = @PaymentType";
                            sConditionHistory += " AND B.BoatTypeId = @BoatTypeId";
                        }
                        if (Boating.CreatedBy != "0")
                        {
                            conditions += " AND C.CreatedBy = @CreatedBy";
                            sConditionHistory += " AND B.BoatTypeId = @BoatTypeId";
                        }

                        squery = "  DECLARE @CgstTax DECIMAL(18, 2); "
                               + "  DECLARE @SgstTax DECIMAL(18, 2); "
                               + "  SET @CgstTax = (SELECT  TaxPercentage FROM TaxMaster WHERE TaxDescription='CGST' AND ServiceName=1 AND ActiveStatus='A' AND BoatHouseId = @BoatHouseId ) "
                               + "  SET @SgstTax = (SELECT TaxPercentage FROM TaxMaster WHERE TaxDescription='SGST' AND ServiceName=1 AND ActiveStatus='A' AND  BoatHouseId = @BoatHouseId ) "
                               + "  (SELECT UniqueId, CreatedBy, BookingId AS 'BookingId', BookingDate, SUM(TotalCount) AS 'Count', "
                               + "  Account AS 'Particulars', SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                               + "  SUM(TotalAmount) AS 'TotalAmount', Ordervalue FROM "
                               + "  ( "
                               + "  SELECT C.UniqueId AS 'UniqueId', "
                               + "  B.BookingId AS 'BookingId', CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) as 'BookingDate', "
                               + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                               + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                               + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                               + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0) + ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', "
                               + "  C.Createdby AS 'CreatedBy', 0 AS 'Ordervalue'  FROM  BookingDtl AS B  INNER JOIN  BookingHdr AS C ON B.BookingId = C.BookingId "
                               + "  AND B.BookingId = C.BookingId  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                               + " " + conditions + " "
                               + "  GROUP BY  BookingDate, B.BookingId,  C.UniqueId, C.Createdby "
                               + "  UNION ALL "
                               + "  SELECT C.UniqueId AS 'UniqueId', "
                               + "  B.BookingId AS 'BookingId', CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) as 'BookingDate', "
                               + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                               + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                               + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                               + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0) + ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', "
                               + "  C.Createdby AS 'CreatedBy', 0 AS 'Ordervalue'  FROM  BookingDtlHistory AS B  INNER JOIN  BookingHdrHistory AS C ON B.BookingId = C.BookingId "
                               + "  AND B.BookingId = C.BookingId  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                               + " " + sConditionHistory + " "
                               + "  GROUP BY  BookingDate, B.BookingId,  C.UniqueId, C.Createdby "
                               + " )  AS A "
                               + "  GROUP BY BookingDate,Ordervalue, Account,BookingId,   CreatedBy, UniqueId ) "
                               + "  Union All "
                               + "  SELECT  '' AS 'UniqueId', '' AS 'CreatedBy', '' AS 'BookingId', "
                               + "  A.BookingDate, SUM(TotalCount) AS 'Count',  'Total' AS 'Particulars', "
                               + "  SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                               + "  SUM(TotalAmount) AS 'TotalAmount',Ordervalue FROM "
                               + "  ( "
                               + "  SELECT  CONVERT(VARCHAR(50), C.BookingDate, 103) + ' ' + '13:00PM' as 'BookingDate', "
                               + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                               + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                               + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                               + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitBoatCharge) * (@SgstTax)/ 100), 0)  + ISNULL((SUM(B.InitRowerCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', 1 AS 'Ordervalue' "
                               + "  FROM  BookingDtl AS B INNER JOIN  BookingHdr AS C ON B.BookingId = C.BookingId  AND B.BookingId = C.BookingId "
                               + "  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                               + " " + conditions + " "
                               + "  GROUP BY  BookingDate "
                               + "  UNION ALL"
                               + "  SELECT  CONVERT(VARCHAR(50), C.BookingDate, 103) + ' ' + '13:00PM' as 'BookingDate', "
                               + "  COUNT(B.BookingId) AS 'TotalCount', 'Boat Revenue' AS 'Account', "
                               + "  ISNULL(CAST(SUM(B.InitBoatCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + "  SUM(ISNULL(B.CGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/ 100) AS DECIMAL(18, 2)), 0)  + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@CgstTax)/100) AS DECIMAL(18, 2)), 0)  AS 'CGST', "
                               + "  SUM(ISNULL(B.SGSTTaxAmount,0)) - ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 + "
                               + "  ISNULL(CAST((SUM(B.InitRowerCharge) * (@SgstTax)) AS DECIMAL(18, 2)), 0) / 100 AS 'SGST', "
                               + "  ROUND(ISNULL(ISNULL(SUM(B.InitBoatCharge), 0) + ISNULL((SUM(B.InitBoatCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitBoatCharge) * (@SgstTax)/ 100), 0)  + ISNULL((SUM(B.InitRowerCharge) * (@CgstTax)/ 100), 0)  + "
                               + "  ISNULL((SUM(B.InitRowerCharge) * (@SgstTax)/ 100), 0) , 0), 0)  AS 'TotalAmount', 1 AS 'Ordervalue' "
                               + "  FROM  BookingDtlHistory AS B INNER JOIN  BookingHdrHistory AS C ON B.BookingId = C.BookingId  AND B.BookingId = C.BookingId "
                               + "  INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                               + " " + sConditionHistory + " "
                               + "  GROUP BY  BookingDate "
                               + ")  AS A "
                               + "  GROUP BY  BookingDate,Ordervalue "
                               + "  ORDER BY BookingDate, Ordervalue ";
                    }
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@sFromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = Boating.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = Boating.BoatTypeId;
                cmd.Parameters["@PaymentType"].Value = Boating.PaymentType;
                cmd.Parameters["@CreatedBy"].Value = Boating.CreatedBy;
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(Boating.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(Boating.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@sFromDate"].Value = DateTime.Parse(Boating.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                cmd.Parameters["@sToDate"].Value = DateTime.Parse(Boating.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();
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
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        public Boolean CheckDate(string FromDate)
        {
            DateTime CheckFromDate = DateTime.Parse(FromDate.ToString().Trim(), objEnglishDate);
            DateTime CheckNowDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"), objEnglishDate);
            int iDays = 7;
            if ((CheckNowDate - CheckFromDate).Days < iDays)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("RptServiceWiseCollection")]
        public IHttpActionResult BoatwiseTrip([FromBody] BoatingReport Boating)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";

                string conditions1 = " WHERE CAST(C.BookingDate AS DATE) "
                       + " BETWEEN @BookingDate "
                       + " AND @BookingDate "
                       + " AND B.BoatHouseId = @BoatHouseId "
                       + " AND C.BoatHouseId = @BoatHouseId "
                       + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' ";
                //SILLU 27 OCT 2021
                string conditions2 = " WHERE CAST(C.BookingDate AS DATE) "
                      + " BETWEEN @BookingDate "
                      + " AND @BookingDate "
                      + " AND B.BoatHouseId = @BoatHouseId "
                      + " AND C.BoatHouseId = @BoatHouseId "
                      + " AND C.Status IN ('B', 'R', 'P') AND B.Status IN ('B', 'R') AND D.TypeID = '20' AND B.BoatDeposit != 0 ";
                //SILLU 05 NOV 2021
                string conditions3 = " WHERE CAST(PaidDate AS DATE) "
                      + " BETWEEN @BookingDate "
                      + " AND @BookingDate "
                      + " AND BoatHouseId = @BoatHouseId "
                      + " AND PaymentStatus = 'P' AND PaidBy IS NOT NULL AND PaidAmount IS NOT NULL ";
                //SILLU 08 NOV 2021
                string conditions4 = " WHERE CAST(D.BookingDate AS Date) "
                     + " BETWEEN @BookingDate"
                     + " AND @BookingDate "
                     + " AND A.BoatHouseId =@BoatHouseId "
                     + " AND A.Status IN('B', 'R') AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL "
                     + " AND DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) > (A.BookingDuration + BRM.BoatGraceTime) "
                     + " AND A.BoatDeposit = '0' AND (A.ActualNetAmount - A.InitNetAmount) > 0 ";

                if (Boating.BoatTypeId != "0")
                {
                    conditions += " AND B.BoatTypeId = @BoatTypeId";
                    conditions1 += " AND B.BoatTypeId = @BoatTypeId";
                    conditions2 += " AND B.BoatTypeId = @BoatTypeId";
                    conditions3 += " AND BoatTypeId = @BoatTypeId";
                    conditions4 += " AND A.BoatTypeId = @BoatTypeId";
                }
                if (Boating.PaymentType != "0")
                {
                    conditions += " AND C.PaymentType = @PaymentType";
                    conditions1 += " AND C.PaymentType = @PaymentType";
                    conditions2 += " AND C.PaymentType = @PaymentType";
                    conditions4 += " AND D.PaymentType = @PaymentType";
                }
                if (Boating.CreatedBy != "0")
                {
                    conditions += " AND C.CreatedBy = @CreatedBy";
                    conditions1 += " AND C.CreatedBy = @CreatedBy";
                    conditions2 += " AND C.CreatedBy = @CreatedBy";
                    conditions3 += " AND RequestedBy = @CreatedBy";    //SILLU 05 NOV 2021
                    conditions4 += " AND A.SDUserBy = @CreatedBy";     //SILLU 08 NOV 2021
                }

                squery = " DECLARE @TAX DECIMAL(18, 2)"
                          + " SET @TAX = (SELECT TOP 1 TaxPercentage AS 'TAX' FROM TaxMaster WHERE ServiceName = '1' AND BoatHouseId = @BoatHouseId )"
                          + " SELECT BookingDate, SUM(TotalCount) AS 'Count', Account AS 'Particulars', SUM(Amount)AS 'Amount', SUM(CGST) AS 'CGST', SUM(SGST) AS 'SGST', "
                          + " SUM(TotalAmount) AS 'TotalAmount' "
                          + " FROM"
                          + " ("
                          + " SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate', COUNT(B.BookingId) AS 'TotalCount',"
                          + " 'Boat Revenue' AS 'Account',"
                          + " ISNULL(SUM(B.InitBoatCharge), 0) AS 'Amount',"

                           + " SUM(B.CGSTTaxAmount)  AS 'CGST', "
                           + " SUM(B.SGSTTaxAmount)  AS 'SGST', "
                           + " SUM(B.InitBoatCharge + B.CGSTTaxAmount + B.SGSTTaxAmount)  AS 'TotalAmount' "

                         + " FROM  BookingDtl AS B WITH (NOLOCK)"
                         + " INNER JOIN  BookingHdr AS C ON  B.BookingId = C.BookingId "
                         + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                         + " " + conditions + " "
                         + " GROUP BY B.BookingId, CAST(C.BookingDate AS DATE) "
                         + " ) "
                         + " AS A "
                         + " GROUP BY BookingDate, Account "
                         + " UNION ALL "

                         + " SELECT BookingDate, 0 AS 'Count', Account, SUM(Amount), 0 AS 'CGST', 0 AS 'SGST', SUM(TotalAmount) "

                         + " FROM "
                         + " ( "
                         + " SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate', COUNT(B.BookingId) AS 'TotalCount', "
                         + " 'Rower Charge' AS 'Account', "
                         + " ISNULL(CAST(SUM(B.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'Amount', "

                         + " ISNULL(CAST(SUM(B.InitRowerCharge) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "

                         + " FROM BookingDtl AS B WITH (NOLOCK) "
                        + " INNER JOIN  BookingHdr AS C ON  B.BookingId = C.BookingId "
                        + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                        + " " + conditions1 + " "
                        + " GROUP BY CAST(C.BookingDate AS DATE) "
                        + " ) "
                        + " AS B "
                        + " GROUP BY BookingDate, Account "
                        + " UNION ALL "
                        //////Sillu 27 oct 2021
                        + " SELECT BookingDate, SUM(TotalCount) AS 'Count', Account, SUM(Amount), 0 AS 'CGST', 0 AS 'SGST', SUM(TotalAmount) "
                         + " FROM "
                         + " ( "
                         + " SELECT CONVERT(NVARCHAR(20), CAST(C.BookingDate AS DATE), 103) AS 'BookingDate', COUNT(B.BookingId) AS 'TotalCount', "
                         + " 'Deposit Collected' AS 'Account', "
                         + " ISNULL(CAST(SUM(B.BoatDeposit) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                         + " ISNULL(CAST(SUM(B.BoatDeposit) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                         + " FROM BookingDtl AS B WITH (NOLOCK) "
                        + " INNER JOIN  BookingHdr AS C ON  B.BookingId = C.BookingId "
                        + " INNER JOIN ConfigurationMaster AS D ON C.PaymentType = D.ConfigID "
                        + " " + conditions2 + " "
                        + " GROUP BY CAST(C.BookingDate AS DATE) "
                        + " ) "
                        + " AS C "
                        + " GROUP BY BookingDate, Account ";
                //SILLU 08 NOV 2021


                if (Boating.PaymentType == "0" || Boating.PaymentType == "1")
                {
                    //SILLU 05 NOV 2021
                    squery +=
                         " UNION ALL "
                        + " SELECT BookingDate, SUM(TotalCount) AS 'Count', Account, SUM(Amount) AS 'Amount', 0 AS 'CGST', 0 AS 'SGST', SUM(Amount) AS 'TotalAmount' "
                        + " FROM "
                        + " ( "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BDate AS DATE), 103) AS 'BookingDate', COUNT(D.UniqueId) AS 'TotalCount','Extn Collected' AS 'Account', "
                        + " ISNULL(SUM(A.ActualNetAmount - A.InitNetAmount), 0) AS 'Amount' "
                        + " FROM BookingDtl AS A WITH(NOLOCK) "
                        + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                        + " INNER JOIN BoatRateMaster AS BRM ON A.BoatSeaterId = BRM.BoatSeaterId  AND A.BoatTypeId = BRM.BoatTypeId AND A.BoatHouseId = BRM.BoatHouseId "
                        + " " + conditions4 + " "
                        + " GROUP BY CAST(A.BDate AS DATE), ISNULL(A.ActualNetAmount - A.InitNetAmount, 0) "
                        + " ) "
                        + " AS E "
                        + " GROUP BY BookingDate, Account "
                        + " UNION ALL "
                        + " SELECT BookingDate, SUM(TotalCount) AS 'Count', Account, SUM(Amount), 0 AS 'CGST', 0 AS 'SGST', SUM(Amount) AS 'TotalAmount' "
                        + " FROM "
                        + " ( "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(PaidDate AS DATE), 103) AS 'BookingDate', COUNT(UniqueId) AS 'TotalCount', 'Refund Counter' AS 'Account',"
                        + " ISNULL(CAST(SUM(PaidAmount) AS DECIMAL(18, 2)), 0) AS 'Amount'  FROM BH_RefundCounter WITH (NOLOCK)"
                        + " " + conditions3 + " "
                        + " GROUP BY UniqueId, CAST(PaidDate AS DATE) "
                        + " ) "
                        + " AS D"
                        + " GROUP BY BookingDate, Account ";
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = Boating.BoatHouseId;
                cmd.Parameters["@BoatTypeId"].Value = Boating.BoatTypeId;
                cmd.Parameters["@PaymentType"].Value = Boating.PaymentType;
                cmd.Parameters["@CreatedBy"].Value = Boating.CreatedBy;
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Boating.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return Ok(ds);
                }
                else
                {
                    return Ok("No Records Found.");
                }
            }
            catch (Exception ex)
            {
                ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }

        }
        //************************Sillu*******************************


        //*****************************Trip Sheet Related Changes By Imran*****************************//

        //Trip Sheet Web Trip End

        /// <summary>
        /// Modified By : Imran.
        /// Modified Date : 30-03-2021
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("DisplayMaximumCount")]
        public IHttpActionResult AppVersion([FromBody] DisplayMaxCount MaxCount)
        {
            try
            {
                if (MaxCount.BoatHouseId != "" && MaxCount.MaxCount != "" && MaxCount.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrDisplayMaxCount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", MaxCount.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@MaxCount", MaxCount.MaxCount.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", MaxCount.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", MaxCount.CreatedBy.Trim());

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
                        DisplayMaxCountRes EmMstr = new DisplayMaxCountRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(EmMstr);
                    }
                    else
                    {
                        DisplayMaxCountRes EmMstr = new DisplayMaxCountRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(EmMstr);
                    }
                }
                else
                {
                    DisplayMaxCountRes EmMstr = new DisplayMaxCountRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
                }
            }
            catch (Exception ex)
            {
                DisplayMaxCountRes ConfRes = new DisplayMaxCountRes
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
        [Route("GetDisplayMaxCount")]
        public IHttpActionResult getCustomerName([FromBody] DisplayMaxCount Trip)
        {
            try
            {
                if (Convert.ToString(Trip.BoatHouseId) != null)
                {
                    List<DisplayMaxCount> li = new List<DisplayMaxCount>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select MC.Maxcount,BHM.BoatHouseName,MC.BoatHouseId from MaxiumCount as MC INNER JOIN BHMaster AS BHM ON MC.BoatHouseId = BHM.BoatHouseId", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DisplayMaxCount ShowBoatid = new DisplayMaxCount();
                            ShowBoatid.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            ShowBoatid.MaxCount = dt.Rows[i]["Maxcount"].ToString();
                            ShowBoatid.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();

                            li.Add(ShowBoatid);
                        }

                        DisplayMaxCountList ConfList = new DisplayMaxCountList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        DisplayMaxCountRes ConfRes = new DisplayMaxCountRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    DisplayMaxCountRes Vehicle = new DisplayMaxCountRes
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
                    Response = Convert.ToString(ex),
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
        /// Createdd By : Imran
        /// CReated Date : 2022-04-20
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("LoginLog")]
        public IHttpActionResult InsertLoginLog([FromBody] LoginLog Login)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("LoginHistory", con_Common);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", Login.QueryType.ToString());
                cmd.Parameters.AddWithValue("@UserName", Login.UserName.ToString());
                cmd.Parameters.AddWithValue("@SystemIP", Login.SystemIP.ToString());
                cmd.Parameters.AddWithValue("@SessionId", Login.SessionId.Trim());
                cmd.Parameters.AddWithValue("@PublicIP", Login.PublicIP.ToString());
                cmd.Parameters.AddWithValue("@Browser", Login.Browser.ToString());
                cmd.Parameters.AddWithValue("@BVersion", Login.BVersion.ToString());
                cmd.Parameters.AddWithValue("@Log", Login.Log.Trim());
                cmd.Parameters.AddWithValue("@UserId", Login.UserId.Trim());

                SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                RuturnValue.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(RuturnValue);
                con_Common.Open();
                cmd.ExecuteNonQuery();
                sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                string[] sResult = sReturn.Split('~');
                con_Common.Close();

                if (sResult[0].Trim() == "Success")
                {
                    LoginLogRes EmMstr = new LoginLogRes
                    {
                        Response = sResult[1].Trim(),
                        StatusCode = 1
                    };
                    return Ok(EmMstr);
                }
                else
                {
                    LoginLogRes EmMstr = new LoginLogRes
                    {
                        Response = sResult[1].Trim(),
                        StatusCode = 0
                    };

                    return Ok(EmMstr);

                }

            }
            catch (Exception ex)
            {
                LoginLogRes ConfRes = new LoginLogRes
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
        /// Createdd By : Imran
        /// CReated Date : 2022-04-20
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetSessionId")]
        public IHttpActionResult SessionId([FromBody] LoginLog Login)
        {
            try
            {
                if (Login.UserId != "" && Login.SessionId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("GetSessionId", con_Common);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@SessionId", Login.SessionId.Trim());
                    cmd.Parameters.AddWithValue("@UserId", Login.UserId.Trim());

                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con_Common.Open();
                    cmd.ExecuteNonQuery();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con_Common.Close();

                    if (sResult[0].Trim() == "Success")
                    {
                        LoginLogRes EmMstr = new LoginLogRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(EmMstr);
                    }
                    else
                    {
                        LoginLogRes EmMstr = new LoginLogRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(EmMstr);
                    }
                }
                else
                {
                    LoginLogRes EmMstr = new LoginLogRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
                }
            }
            catch (Exception ex)
            {
                LoginLogRes ConfRes = new LoginLogRes
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
        [Route("ViewLogDetailsNew")]
        public IHttpActionResult ViewLogDetailsNew([FromBody] BookingBoardingPass Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    string sQuery = " SELECT A.BookingId, A.BookingPin, A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BoatReferenceNo, "
                    + " A.NewBoatId, A.NewBoatNum, ISNULL(RIGHT(CONVERT(VARCHAR, A.NewExpectedTime, 100), 7), '00:00') AS 'NewExpectedTime', "
                    + " CONVERT(NVARCHAR(50), E.BookingDate, 103) AS 'BookingDate', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.OldTripStartTime, A.OldTripEndTime) AS NVARCHAR), 0) AS 'OldTravelDuration', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.NewTripStartTime, A.NewTripEndTime) AS NVARCHAR), 0) AS 'NewTravelDuration', "
                    + " A.NewRowerId, D.RowerName, CONVERT(VARCHAR, A.NewTripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.NewTripStartTime, 100), 7) AS 'NewTripStartTime', "
                    + " CONVERT(VARCHAR, A.NewTripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.NewTripEndTime, 100), 7) AS 'NewTripEndTime',  "
                    + " A.BoatHouseId, A.BoatHouseName FROM BoardingPassLog AS A "
                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS D on A.NewRowerId = D.RowerId and A.BoatHouseId = D.BoatHouseId "
                    + " INNER JOIN BookingHdr AS E ON A.BookingId = E.BookingId AND A.BoatHouseId = E.BoatHouseId "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(E.BookingDate AS DATE) BETWEEN "
                    + " @FromDate AND @ToDate "
                    + " ORDER BY A.CreatedDate DESC";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.Date));
                    cmd.Parameters["@BoatHouseId"].Value = Pass.BoatHouseId;
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Pass.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Pass.ToDate.Trim(), objEnglishDate);
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
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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
        [Route("ChangeBoatBookingSadminNew")]
        public IHttpActionResult ChangeBoatBookingSadminNew([FromBody] BookingBoardingPass Passes)
        {
            try
            {
                if (Passes.BoatHouseId != "" && Passes.BookingDate != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    string sQuery = "SELECT A.BookingId, A.BoatReferenceNo,A.BookingPin,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType,"
                    + " A.ActualBoatId, A.ActualBoatNum, E.RowerId,  E.RowerName, "
                    + " F.BoatMinTime + F.BoatGraceTime AS 'BoatDuration', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDuration', "
                    + " CONVERT(NVARCHAR(50), D.BookingDate, 103) AS 'BookingDate', "
                    + " CONVERT(VARCHAR, A.TripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripStartTime, 100),7) as TripStartTime, "
                    + " CONVERT(VARCHAR, A.TripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripEndTime, 100),7) as TripEndTime, "
                    + " RIGHT(CONVERT(VARCHAR, ExpectedTime, 100), 7) AS 'ExpectedTime', "
                    + " CASE when D.PremiumStatus = 'P' then 'Permium' else 'Normal' end as PremiumStatus, A.BoatDeposit, A.DepRefundAmount "
                    + " FROM BookingDtl AS A"
                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS E on A.RowerId = E.RowerId and A.BoatHouseId = E.BoatHouseId "
                    + " INNER JOIN BoatRateMaster AS F ON A.BoatHouseId = F.BoatHouseId AND A.BoatTypeId = F.BoatTypeId "
                    + " AND A.BoatSeaterId = F.BoatSeaterId AND F.ActiveStatus='A' "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND A.BookingPin=@BookingPin"
                    + " AND CAST(D.BookingDate AS DATE) = @BookingDate  AND A.Status IN('B','R') AND D.Status IN ('B','R','P') "
                    + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL  and A.PremiumStatus not in ('I')";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar));
                    cmd.Parameters["@BoatHouseId"].Value = Passes.BoatHouseId;
                    cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Passes.BookingDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BookingPin"].Value = Passes.BookingPin.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BookingBoardingPass Boardingpass = new BookingBoardingPass();
                            Boardingpass.BookingId = dt.Rows[i]["BookingId"].ToString();
                            Boardingpass.BoatReferenceNum = dt.Rows[i]["BoatReferenceNo"].ToString();
                            Boardingpass.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            Boardingpass.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            Boardingpass.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            Boardingpass.BoatType = dt.Rows[i]["BoatType"].ToString();
                            Boardingpass.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            Boardingpass.SeatType = dt.Rows[i]["SeaterType"].ToString();
                            Boardingpass.ActualBoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            Boardingpass.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            Boardingpass.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            Boardingpass.Status = dt.Rows[i]["PremiumStatus"].ToString();
                            Boardingpass.RowerId = dt.Rows[i]["RowerId"].ToString();
                            Boardingpass.RowerName = dt.Rows[i]["RowerName"].ToString();
                            Boardingpass.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            Boardingpass.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            Boardingpass.BoatDuration = dt.Rows[i]["BoatDuration"].ToString();
                            Boardingpass.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            Boardingpass.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            Boardingpass.OldDepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
                            li.Add(Boardingpass);
                        }

                        BookingBoardingPassList AvailList = new BookingBoardingPassList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(AvailList);
                    }

                    else
                    {
                        BookingBoardingPassRes availRes = new BookingBoardingPassRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(availRes);
                    }
                }
                else
                {
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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
        [Route("ChangeBoatBookingNew")]
        public IHttpActionResult ChangeBoatBookingNew([FromBody] BookingBoardingPass Pass)
        {
            try
            {
                if (Pass.BoatHouseId != "")
                {
                    List<BookingBoardingPass> li = new List<BookingBoardingPass>();
                    con.Open();
                    //Change by Imran on 2021-11-08 
                    string sQuery = "SELECT A.BookingId, A.BoatReferenceNo,A.BookingPin,A.BoatTypeId,B.BoatType,A.BoatSeaterId,C.SeaterType,"
                    + " A.ActualBoatId, A.ActualBoatNum, E.RowerId,  E.RowerName, "
                    + " F.BoatMinTime + F.BoatGraceTime AS 'BoatDuration', "
                    + " ISNULL(CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) AS NVARCHAR), 0) AS 'TravelDuration', "
                    + " CONVERT(NVARCHAR(50), D.BookingDate, 103) AS 'BookingDate', "
                    + " CONVERT(VARCHAR, A.TripStartTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripStartTime, 100),7) as TripStartTime, "
                    + " CONVERT(VARCHAR, A.TripEndTime, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.TripEndTime, 100),7) as TripEndTime, "
                    + " RIGHT(CONVERT(VARCHAR, ExpectedTime, 100), 7) AS 'ExpectedTime', "
                    + " CASE when D.PremiumStatus = 'P' then 'Permium' else 'Normal' end as PremiumStatus, A.BoatDeposit, A.DepRefundAmount "
                    + " FROM BookingDtl AS A"
                    + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                    + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                    + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                    + " LEFT JOIN RowerMaster AS E on A.RowerId = E.RowerId and A.BoatHouseId = E.BoatHouseId "
                    + " INNER JOIN BoatRateMaster AS F ON A.BoatHouseId = F.BoatHouseId AND A.BoatTypeId = F.BoatTypeId "
                    + " AND A.BoatSeaterId = F.BoatSeaterId AND F.ActiveStatus='A' "
                    + " WHERE A.BoatHouseId = @BoatHouseId AND A.BookingPin=@BookingPin"
                    + " AND CAST(D.BookingDate AS DATE) = CAST(GETDATE() AS DATE) AND A.Status IN('B','R') AND D.Status IN ('B','R','P') "
                    + " AND A.TripStartTime IS NOT NULL AND A.TripEndTime IS NOT NULL and A.PremiumStatus not in ('I') ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingPin", System.Data.SqlDbType.VarChar));
                    cmd.Parameters["@BoatHouseId"].Value = Pass.BoatHouseId;
                    cmd.Parameters["@BookingPin"].Value = Pass.BookingPin;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BookingBoardingPass Boardingpass = new BookingBoardingPass();
                            Boardingpass.BookingId = dt.Rows[i]["BookingId"].ToString();
                            Boardingpass.BoatReferenceNum = dt.Rows[i]["BoatReferenceNo"].ToString();
                            Boardingpass.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            Boardingpass.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            Boardingpass.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            Boardingpass.BoatType = dt.Rows[i]["BoatType"].ToString();
                            Boardingpass.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            Boardingpass.SeatType = dt.Rows[i]["SeaterType"].ToString();
                            Boardingpass.ActualBoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            Boardingpass.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            Boardingpass.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            Boardingpass.Status = dt.Rows[i]["PremiumStatus"].ToString();
                            Boardingpass.RowerId = dt.Rows[i]["RowerId"].ToString();
                            Boardingpass.RowerName = dt.Rows[i]["RowerName"].ToString();
                            Boardingpass.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            Boardingpass.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            Boardingpass.BoatDuration = dt.Rows[i]["BoatDuration"].ToString();
                            Boardingpass.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                            Boardingpass.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            Boardingpass.OldDepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();

                            li.Add(Boardingpass);
                        }

                        BookingBoardingPassList AvailList = new BookingBoardingPassList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(AvailList);
                    }

                    else
                    {
                        BookingBoardingPassRes availRes = new BookingBoardingPassRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(availRes);
                    }
                }
                else
                {
                    BookingBoardingPassRes availRes1 = new BookingBoardingPassRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
                }
            }
            catch (Exception ex)
            {
                BookingBoardingPassRes ConfRes = new BookingBoardingPassRes
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
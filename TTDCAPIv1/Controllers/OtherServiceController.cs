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
    public class OtherServiceController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_BoatHouse"].ConnectionString);
        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

        //Other Service Category Detail
        [HttpPost]
        [AllowAnonymous]
        [Route("OtherSvcCatDet")]
        public IHttpActionResult OtherSvcCatDet([FromBody] OtherServicesDetails InsItemMaster)
        {
            try
            {
                string sQuery = string.Empty;

                List<OtherServicesDetails> li = new List<OtherServicesDetails>();

                sQuery = " SELECT A.Category, B.ConfigName AS 'CategoryName', A.ServiceId, A.ServiceName, "
                    + " A.ShortName, A.BoatHouseId, A.BoatHouseName, "
                    + " A.ServiceTotalAmount, A.ChargePerItem, A.ChargePerItemTax, "
                    + " A.TaxID, "
                    + " dbo.GetTaxIdDetails('Other', '2', '', A.TaxID,@BoatHouseId) AS 'TaxName', A.ActiveStatus, A.Createdby FROM OtherServices AS A "
                    + " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND B.TypeID = '27' AND A.OtherServiceType = 'OS'"
                    + " WHERE A.ActiveStatus = 'A' AND A.BoatHouseId = @BoatHouseId"
                    + " AND A.Category IN (SELECT TempValue FROM dbo.CSVToTable(@Category) WHERE TempValue <> '')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = InsItemMaster.BoatHouseId.Trim();
                cmd.Parameters["@Category"].Value = InsItemMaster.Category.Trim();

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherServicesDetails OtherServiceMstr = new OtherServicesDetails();

                        OtherServiceMstr.Category = dt.Rows[i]["Category"].ToString();
                        OtherServiceMstr.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                        OtherServiceMstr.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                        OtherServiceMstr.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        OtherServiceMstr.ShortName = dt.Rows[i]["ShortName"].ToString();

                        OtherServiceMstr.ServiceTotalAmount = dt.Rows[i]["ServiceTotalAmount"].ToString();
                        OtherServiceMstr.AdultCharge = dt.Rows[i]["ChargePerItem"].ToString();
                        OtherServiceMstr.ChargePerItemTax = dt.Rows[i]["ChargePerItemTax"].ToString();


                        OtherServiceMstr.TaxID = dt.Rows[i]["TaxID"].ToString();
                        OtherServiceMstr.TaxName = dt.Rows[i]["TaxName"].ToString();
                        OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        OtherServiceMstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();

                        li.Add(OtherServiceMstr);
                    }

                    OtherServicesDetailslist OtherService = new OtherServicesDetailslist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(OtherService);
                }

                else
                {
                    OtherServicesString OtherService = new OtherServicesString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(OtherService);
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
        [Route("OtherBoatSvcCatDet")]
        public IHttpActionResult OtherBoatSvcCatDet([FromBody] OtherServicesDetails InsItemMaster)
        {
            try
            {
                string sQuery = string.Empty;

                List<OtherServicesDetails> li = new List<OtherServicesDetails>();

                sQuery = " SELECT A.Category, B.ConfigName AS 'CategoryName', A.ServiceId, A.ServiceName, "
                    + " A.ShortName, A.BoatHouseId, A.BoatHouseName, "
                    + " A.ServiceTotalAmount, A.ChargePerItem, A.ChargePerItemTax, "
                    + " A.TaxID, "
                    + " dbo.GetTaxIdDetails('Other', '2', '', A.TaxID,@BoatHouseId) AS 'TaxName', A.ActiveStatus, A.Createdby FROM OtherServices AS A "
                    + " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND B.TypeID = '34' AND A.OtherServiceType = 'AB'"
                    + " WHERE A.ActiveStatus = 'A' AND A.BoatHouseId = @BoatHouseId"
                    + " AND A.Category IN (SELECT TempValue FROM dbo.CSVToTable(@Category) WHERE TempValue <> '')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = InsItemMaster.BoatHouseId.Trim();
                cmd.Parameters["@Category"].Value = InsItemMaster.Category.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherServicesDetails OtherServiceMstr = new OtherServicesDetails();

                        OtherServiceMstr.Category = dt.Rows[i]["Category"].ToString();
                        OtherServiceMstr.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                        OtherServiceMstr.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                        OtherServiceMstr.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        OtherServiceMstr.ShortName = dt.Rows[i]["ShortName"].ToString();

                        OtherServiceMstr.ServiceTotalAmount = dt.Rows[i]["ServiceTotalAmount"].ToString();
                        OtherServiceMstr.AdultCharge = dt.Rows[i]["ChargePerItem"].ToString();
                        OtherServiceMstr.ChargePerItemTax = dt.Rows[i]["ChargePerItemTax"].ToString();


                        OtherServiceMstr.TaxID = dt.Rows[i]["TaxID"].ToString();
                        OtherServiceMstr.TaxName = dt.Rows[i]["TaxName"].ToString();
                        OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        OtherServiceMstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();

                        li.Add(OtherServiceMstr);
                    }

                    OtherServicesDetailslist OtherService = new OtherServicesDetailslist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(OtherService);
                }

                else
                {
                    OtherServicesString OtherService = new OtherServicesString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(OtherService);
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

        //Other Service Booking
        [HttpPost]
        [AllowAnonymous]
        [Route("BookingOtherServices")]
        public IHttpActionResult BookingOtherServices([FromBody] BookingOtherServices InsMatPur)
        {
            try
            {
                if (InsMatPur.QueryType != "" && InsMatPur.BookingId != "" && InsMatPur.ServiceId != ""
                    && InsMatPur.BookingType != "" && InsMatPur.BoatHouseId != "" && InsMatPur.BoatHouseName != ""
                    && InsMatPur.ChargePerItem != "" && InsMatPur.NoOfItems != "" && InsMatPur.CGSTTaxAmount != "" && InsMatPur.SGSTTaxAmount != ""
                    && InsMatPur.NetAmount != "" && InsMatPur.CreatedBy != "" && InsMatPur.BookingMedia != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("BookingOtherServices", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsMatPur.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", InsMatPur.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@ServiceId", InsMatPur.ServiceId.ToString());
                    cmd.Parameters.AddWithValue("@BookingType", InsMatPur.BookingType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsMatPur.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsMatPur.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(InsMatPur.BookingDate.ToString(), objEnglishDate));

                    cmd.Parameters.AddWithValue("@ChargePerItem", InsMatPur.ChargePerItem.ToString());
                    cmd.Parameters.AddWithValue("@NoOfItems", InsMatPur.NoOfItems.ToString());
                    // cmd.Parameters.AddWithValue("@TaxDetails", InsMatPur.TaxDetails.Trim());
                    cmd.Parameters.AddWithValue("@CGSTTaxAmount", InsMatPur.CGSTTaxAmount.Trim());
                    cmd.Parameters.AddWithValue("@SGSTTaxAmount", InsMatPur.SGSTTaxAmount.Trim());
                    cmd.Parameters.AddWithValue("@NetAmount", InsMatPur.NetAmount.Trim());
                    cmd.Parameters.AddWithValue("@BookingMedia", InsMatPur.BookingMedia.Trim());
                    cmd.Parameters.AddWithValue("@CustomerMobileNo", InsMatPur.CustomerMobileNo.Trim());
                    cmd.Parameters.AddWithValue("@PaymentType", InsMatPur.PaymentType.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsMatPur.CreatedBy.Trim());

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
                            Response = sResult[1].Trim() + '~' + sResult[2].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        ConfigurationMasterRes ConMstr = new ConfigurationMasterRes
                        {
                            Response = sResult[0].Trim(),
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

        //Additional Ticket Service Booking
        [HttpPost]
        [AllowAnonymous]
        [Route("BookingAdditionalTicket")]
        public IHttpActionResult BookingAdditionalTicket([FromBody] BookingOtherServices InsMatPur)
        {
            try
            {
                if (InsMatPur.QueryType != "" && InsMatPur.BookingId != "" && InsMatPur.ServiceId != ""
                    && InsMatPur.BookingType != "" && InsMatPur.BoatHouseId != "" && InsMatPur.BoatHouseName != ""
                    && InsMatPur.ChargePerItem != "" && InsMatPur.NoOfItems != "" && InsMatPur.CGSTTaxAmount != "" && InsMatPur.SGSTTaxAmount != ""
                    && InsMatPur.NetAmount != "" && InsMatPur.CreatedBy != "" && InsMatPur.BookingMedia != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("BookingAdditionalTicket", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsMatPur.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", InsMatPur.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@ServiceId", InsMatPur.ServiceId.ToString());
                    cmd.Parameters.AddWithValue("@BookingType", InsMatPur.BookingType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsMatPur.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsMatPur.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(InsMatPur.BookingDate.ToString(), objEnglishDate));

                    cmd.Parameters.AddWithValue("@ChargePerItem", InsMatPur.ChargePerItem.ToString());
                    cmd.Parameters.AddWithValue("@NoOfItems", InsMatPur.NoOfItems.ToString());
                    // cmd.Parameters.AddWithValue("@TaxDetails", InsMatPur.TaxDetails.Trim());
                    cmd.Parameters.AddWithValue("@CGSTTaxAmount", InsMatPur.CGSTTaxAmount.Trim());
                    cmd.Parameters.AddWithValue("@SGSTTaxAmount", InsMatPur.SGSTTaxAmount.Trim());
                    cmd.Parameters.AddWithValue("@NetAmount", InsMatPur.NetAmount.Trim());
                    cmd.Parameters.AddWithValue("@BookingMedia", InsMatPur.BookingMedia.Trim());
                    cmd.Parameters.AddWithValue("@CustomerMobileNo", InsMatPur.CustomerMobileNo.Trim());
                    cmd.Parameters.AddWithValue("@PaymentType", InsMatPur.PaymentType.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsMatPur.CreatedBy.Trim());

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
                            Response = sResult[1].Trim() + '~' + sResult[2].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        ConfigurationMasterRes ConMstr = new ConfigurationMasterRes
                        {
                            Response = sResult[0].Trim(),
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

        //Other Booked List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("OtherBookedList")]
        public IHttpActionResult GetOtherBookedList([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "OtherBookedDetails");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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
        /// Create By : Abhinaya
        /// Created Date : 2022-04-18
        /// Version: V2
        /// </summary>
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("OtherBookedListV2")]
        public IHttpActionResult GetOtherBookedListV2([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();
                int endcount = Int32.Parse(Otl.CountStart.Trim()) + 9;
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "OtherBookedDetailsV2");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());
                cmd.Parameters.AddWithValue("@Input1", Otl.CountStart.Trim());
                cmd.Parameters.AddWithValue("@Input2", endcount.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();
                        otl.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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

        ///// <summary>
        ///// Create By : Abhinaya
        ///// Created Date : 2022-04-18
        ///// Version: V2
        ///// </summary>
        ///// <param name="Otl"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("OtherBookedListPinV2")]
        //public IHttpActionResult GetOtherBookedListPinV2([FromBody] OtherTicketBookedDtl Otl)
        //{
        //    try
        //    {
        //        List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();
        //        SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Clear();
        //        cmd.CommandTimeout = 10000000;
        //        cmd.Parameters.AddWithValue("@QueryType", "OtherBookedDetailsPinV2");
        //        cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
        //        cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
        //        cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
        //        cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());
        //        cmd.Parameters.AddWithValue("@Input1", Otl.CountStart.Trim());

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
        //                otl.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                otl.BookingType = dt.Rows[i]["BookingType"].ToString();
        //                otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
        //                otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
        //                otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
        //                otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
        //                otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
        //                otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
        //                otl.Status = dt.Rows[i]["Status"].ToString();
        //                otl.RowNumber = dt.Rows[i]["RowNumber"].ToString();

        //                li.Add(otl);
        //            }

        //            OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(odl);
        //        }

        //        else
        //        {
        //            OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(ods);
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
        /// Create By : Abhinaya
        /// Created Date : 2022-04-18
        /// Version: V2
        /// Modified By : Abhinaya
        /// Modified Date : 2022-04-25
        /// Version: V2
        /// </summary>
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("OtherBookedListPinV2")]
        public IHttpActionResult GetOtherBookedListPinV2([FromBody] OtherTicketBookedDtl Otl)
        {
            try

            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();
                int endcount = Int32.Parse(Otl.CountStart.Trim()) + 9;
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "OtherBookedDetailsPinV2");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());
                cmd.Parameters.AddWithValue("@Input1", Otl.CountStart.Trim());
                cmd.Parameters.AddWithValue("@Input2", endcount);
                cmd.Parameters.AddWithValue("@Input3", Otl.Search.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();
                        otl.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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
        [Route("AdditionalTicketBookedDetails")]
        public IHttpActionResult GetAdditionalTicketBookedDetails([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "AdditionalTicketBookedDetails");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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
        /// Created By Abhinaya K
        /// Created Date : 2022-04-19
        /// Version : V2
        /// </summary>
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("AdditionalTicketBookedDetailsV2")]
        public IHttpActionResult GetAdditionalTicketBookedDetailsV2([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();
                int endcount = Int32.Parse(Otl.CountStart.Trim()) + 9;
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "AdditionalTicketBookedDetailsV2");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());
                cmd.Parameters.AddWithValue("@Input1", Otl.CountStart.Trim());
                cmd.Parameters.AddWithValue("@Input2", endcount.ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();
                        otl.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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
        /// Created By : Abhinaya K
        /// Created Date : 2022-04-19
        /// Version : V2
        /// </summary>
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("AdditionalTicketBookedDetailsPinV2")]
        public IHttpActionResult GetAdditionalTicketBookedDetailsPinV2([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "AdditionalTicketBookedDetailsPinV2");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());
                cmd.Parameters.AddWithValue("@Input1", Otl.CountStart.Trim());


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();
                        otl.RowNumber = dt.Rows[i]["RowNumber"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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
        /// Modified Date : 01-10-2021
        /// Modified By : Imran
        /// Modified Date : 13-10-2021
        /// </summary>
        /// <param name="Ot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("OtherTicket")]
        public IHttpActionResult GetOtherTicket([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "OtherPrintTicket");
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
                        ot.CustomerMobile = dt.Rows[i]["CustomerMobileNo"].ToString();
                        ot.CheckDate = dt.Rows[i]["CheckDate"].ToString();
                        ot.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                        ot.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();
                        ot.OrderId = dt.Rows[i]["OrderId"].ToString();

                        //Added By Imran 13-10-2021
                        ot.TrackingId = dt.Rows[i]["TrackingId"].ToString();
                        ot.BankReferenceNo = dt.Rows[i]["BankReferenceNo"].ToString();
                        ot.BankName = dt.Rows[i]["BankName"].ToString();

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

                    return GetOtherTicketOld(Ot);
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

        public IHttpActionResult GetOtherTicketOld([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "OtherPrintTicketOld");
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
                        ot.CustomerMobile = dt.Rows[i]["CustomerMobileNo"].ToString();
                        ot.CheckDate = dt.Rows[i]["CheckDate"].ToString();
                        ot.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                        ot.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();
                        ot.OrderId = dt.Rows[i]["OrderId"].ToString();

                        //Added By Imran 13-10-2021
                        ot.TrackingId = dt.Rows[i]["TrackingId"].ToString();
                        ot.BankReferenceNo = dt.Rows[i]["BankReferenceNo"].ToString();
                        ot.BankName = dt.Rows[i]["BankName"].ToString();

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
        /// Newly added by Brijin and Imran on 09-05-2022
        /// </summary>
        /// <param name="Ot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PrintAdditionalTicket")]
        public IHttpActionResult GetPrintAdditionalTicket([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "PrintAdditionalTicket");
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
                        ot.CustomerMobile = dt.Rows[i]["CustomerMobileNo"].ToString();
                        ot.CheckDate = dt.Rows[i]["CheckDate"].ToString();
                        ot.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                        ot.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();
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

                    return GetPrintAdditionalTicketOld(Ot);
                };

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
        /// Newly added by Brijin and Imran on 09-05-2022
        /// </summary>
        /// <param name="Ot"></param>
        /// <returns></returns>
        public IHttpActionResult GetPrintAdditionalTicketOld([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "PrintAdditionalTicketOld");
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
                        ot.CustomerMobile = dt.Rows[i]["CustomerMobileNo"].ToString();
                        ot.CheckDate = dt.Rows[i]["CheckDate"].ToString();
                        ot.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                        ot.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();
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
        [Route("RptOtherServiceWiseCollection")]
        public IHttpActionResult OtherService([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                                + "@BookingDate"
                                + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                + " AND A.BoatHouseId = @BoatHouseId AND "
                                + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                                   + " @BookingDate"
                                   + " AND CAST(D.BookingDate AS DATE) BETWEEN @BookingDate AND "
                                   + " @BookingDate "
                                   + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }


                squery = "  SELECT BookingDate, ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',  "
                        + "ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                    + "ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST',  "
                    + "ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                    + "  FROM "
                     + "    ( "
                    + "     SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                     + "    (ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) AS 'TaxAmount', A.NetAmount "
                     + "    FROM BookingOthers AS A "
                     + "    INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                     + "    INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                     + "   INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                       + " " + conditions + " "
                     + "   GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                     + "    UNION ALL "
                      + "   SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, "
                     + "    (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                     + "    (ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) AS 'TaxAmount', A.NetAmount "
                     + "    FROM BookingOthers AS A "
                     + "    INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                     + "   INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                     + "    INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                     + "    INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                      + " " + conditionType + " "
                       + "  GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                      + "   ) AS A "
                      + "   GROUP BY BookingDate,ServiceName ";




                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();

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
        [Route("RptAdditionalTicketCollection")]
        public IHttpActionResult AdditionalTicket([FromBody] OthServiceCat Oth)
        {
            try
            {
                string squery = string.Empty;

                string condition = string.Empty;

                condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '34' AND A.BookingType IN ('A','B') AND "
                          + " A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND B.OtherServiceType = 'AB' AND A.BStatus = 'B' ";

                if (Oth.BoatTypeId != "0")
                {
                    condition += " AND B.Category = @BoatTypeId";
                }
                if (Oth.Category != "0")
                {
                    condition += " AND A.ServiceId = @Category";
                }
                if (Oth.PaymentType != "0")
                {
                    condition += " AND A.PaymentType = PaymentType";
                }
                if (Oth.CreatedBy != "0")
                {
                    condition += " AND A.CreatedBy = @CreatedBy";
                }

                condition += " AND CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND @BookingDate";


                squery = "SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
                + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthers AS A"
                + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                + " GROUP BY C.ConfigID, C.ConfigName";

                SqlCommand cmd = new SqlCommand(squery, con);
                List<OthServiceCat> li = new List<OthServiceCat>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Oth.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = Oth.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = Oth.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = Oth.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = Oth.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Oth.CreatedBy.Trim();

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OthServiceCat OthDtl = new OthServiceCat();

                        OthDtl.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                        OthDtl.TotalCount = dt.Rows[i]["TicketCount"].ToString();
                        OthDtl.Amount = dt.Rows[i]["Amount"].ToString();
                        li.Add(OthDtl);
                    }

                    OthServiceCatList BoatRate = new OthServiceCatList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }
                else
                {
                    OthServiceCatString BoatRate = new OthServiceCatString
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
        [Route("RptOtherServiceDetailed")]
        public IHttpActionResult OtherServiceDtl([FromBody] BoatingReport OtherServiceDtl)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string conditionss = string.Empty;
                string conditiontype = string.Empty;
                string sBookingHdr = string.Empty;
                string sBookingOthers = string.Empty;

                if (DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) == DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate))
                {
                    conditions = string.Empty;
                    conditionss = string.Empty;
                    conditiontype = string.Empty;

                    if (CheckDate(OtherServiceDtl.FromDate.Trim()))
                    {
                        conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN @FromDate AND @ToDate "
                                   + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditionss = "WHERE CAST(BookingDate AS DATE) BETWEEN @FromDate AND @ToDate "
                                    + " AND C.OtherServiceType = 'OS' AND A.BookingType <> 'A' AND"
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN @FromDate AND @ToDate "
                                      + " AND CAST(D.BookingDate AS DATE) BETWEEN @FromDate AND @ToDate "
                                      + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                        sBookingHdr = "BookingHdr";
                        sBookingOthers = "BookingOthers";
                    }
                    else
                    {
                        conditions = " WHERE BookingDate BETWEEN @sFromDate AND @sToDate "
                                   + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditionss = "WHERE BookingDate BETWEEN @sFromDate AND @sToDate "
                                    + " AND C.OtherServiceType = 'OS' AND A.BookingType <> 'A' AND"
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE A.BookingDate BETWEEN @sFromDate AND @sToDate "
                                      + " AND D.BookingDate BETWEEN @sFromDate AND @sToDate "
                                      + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                        sBookingHdr = "BookingHdrHistory";
                        sBookingOthers = "BookingOthersHistory";
                    }

                    if (OtherServiceDtl.BoatTypeId != "0")
                    {
                        conditions += " AND C.Category = @BoatTypeId";
                        conditionss += " AND C.Category = @BoatTypeId";
                        conditiontype += " AND C.Category = @BoatTypeId";
                    }
                    if (OtherServiceDtl.Category != "0")
                    {
                        conditions += " AND C.ServiceId = @Category";
                        conditionss += " AND C.ServiceId = @Category";
                        conditiontype += " AND C.ServiceId = @Category";
                    }
                    if (OtherServiceDtl.PaymentType != "0")
                    {
                        conditions += " AND A.PaymentType = @PaymentType";
                        conditionss += " AND A.PaymentType = @PaymentType";
                        conditiontype += " AND A.PaymentType = @PaymentType";
                    }
                    if (OtherServiceDtl.CreatedBy != "0")
                    {
                        conditions += " AND A.CreatedBy = @CreatedBy";
                        conditionss += " AND A.CreatedBy = @CreatedBy";
                        conditiontype += " AND A.CreatedBy = @CreatedBy";
                    }

                    squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', 2 AS 'Ordervalue', "
                            + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                            + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                            + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                            + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                            + " FROM "
                            + " ( "
                            + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', "
                            + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                            + " ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', A.NetAmount, A.Createdby "
                            + " FROM " + sBookingOthers + " AS A "
                            + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                            + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                            + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                            + " " + conditions + " "
                            + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                            + " UNION ALL "
                            + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', A.BookingId, C.ServiceName, "
                            + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,  ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', A.NetAmount, A.Createdby "
                            + " FROM " + sBookingOthers + " AS A "
                            + " INNER JOIN " + sBookingHdr + " AS D ON D.BookingId = A.BookingId "
                            + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                            + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                            + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                            + " " + conditiontype + " "
                            + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                            + " ) AS A "
                            + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "
                            + " ORDER BY Createdby, UniqueId, ServiceName, BookingDate ASC ";
                }
                else
                {
                    if (CheckDate(OtherServiceDtl.FromDate.Trim()) && CheckDate(OtherServiceDtl.ToDate.Trim()))
                    {
                        conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                                   + " @FromDate"
                                   + " @ToDate"
                                   + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditionss = "WHERE CAST(BookingDate AS DATE) BETWEEN  "
                              + "@FromDate"
                              + "@ToDate"
                              + " AND C.OtherServiceType = 'OS' AND A.BookingType <> 'A' AND"
                              + " C.BoatHouseId = @BoatHouseId AND "
                              + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN "
                              + "@FromDate "
                              + "@ToDate"
                              + " AND CAST(D.BookingDate AS DATE) BETWEEN "
                              + "@FromDate"
                              + "@ToDate"
                              + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                              + " AND A.BoatHouseId = @BoatHouseId AND"
                              + " C.BoatHouseId = @BoatHouseId AND "
                              + " D.BoatHouseId = @BoatHouseId AND "
                              + " E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                        if (OtherServiceDtl.BoatTypeId != "0")
                        {
                            conditions += " AND C.Category = @BoatTypeId";
                            conditionss += " AND C.Category = @BoatTypeId";
                            conditiontype += " AND C.Category = @BoatTypeId";
                        }
                        if (OtherServiceDtl.Category != "0")
                        {
                            conditions += " AND C.ServiceId = @Category";
                            conditionss += " AND C.ServiceId = @Category";
                            conditiontype += " AND C.ServiceId = @Category";
                        }
                        if (OtherServiceDtl.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType";
                            conditionss += " AND A.PaymentType = @PaymentType";
                            conditiontype += " AND A.PaymentType = @PaymentType";
                        }
                        if (OtherServiceDtl.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy";
                            conditionss += " AND A.CreatedBy = @CreatedBy";
                            conditiontype += " AND A.CreatedBy =@CreatedBy";
                        }

                        squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', "
                                + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                                + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                                + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                                + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                                + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
                                + " FROM(SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) "
                                + " AS 'BookingDate', A.BookingId, C.ServiceName, "
                                + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                                + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue' FROM BookingOthers AS A  INNER JOIN OtherServices AS C "
                                + " ON A.ServiceId = C.ServiceId  INNER JOIN ConfigurationMaster AS D "
                                + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                                + " " + conditions + " "
                                + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                                + " UNION ALL "
                                + " SELECT A.UniqueId AS 'UniqueId', "
                                + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                                + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                                + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue'  FROM BookingOthers AS A  INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                                + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId"
                                + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                                + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                                + " " + conditiontype + " "
                                + " GROUP BY A.BookingDate, C.ServiceName, A.BookingId, A.NoOfItems, "
                                + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                                + " ) AS A "
                                + " GROUP BY  BookingDate, ServiceName,Ordervalue,BookingId, UniqueId, Createdby "
                                + " UNION ALL "
                                + " SELECT 0 AS 'UniqueId', 0 AS 'Createdby', '' AS 'BookingId' , BookingDate, 'Total' AS 'Particulars',   "
                                + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                                + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',   "
                                + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                                + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                                + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,1 AS 'Ordervalue' "
                                + " FROM(SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                                + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                                + " A.NetAmount, 0 AS 'Ordervalue' FROM BookingOthers AS A INNER JOIN OtherServices AS C "
                                + " ON A.ServiceId = C.ServiceId INNER JOIN ConfigurationMaster AS D "
                                + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                                + " " + conditionss + " "
                                + " GROUP BY A.BookingDate, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                                + "  ) AS A "
                                + " GROUP BY  BookingDate, Ordervalue "
                                + " ORDER BY BookingDate, ServiceName, Ordervalue ASC ";
                    }
                    else if (!CheckDate(OtherServiceDtl.FromDate.Trim()) && !CheckDate(OtherServiceDtl.ToDate.Trim()))
                    {

                        conditions = " WHERE BookingDate BETWEEN "
                                   + "@sFromDate"
                                   + " AND @sToDate"
                                   + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditionss = "WHERE BookingDate BETWEEN  "
                                    + "@sFromDate"
                                    + " AND @sToDate"
                                    + " AND C.OtherServiceType = 'OS' AND A.BookingType <> 'A' AND"
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE A.BookingDate BETWEEN "
                                      + " @sFromDate"
                                      + " AND @sToDate"
                                      + " AND D.BookingDate BETWEEN "
                                      + " @sFromDate"
                                      + " AND @sToDate"
                                      + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                        if (OtherServiceDtl.BoatTypeId != "0")
                        {
                            conditions += " AND C.Category = @BoatTypeId";
                            conditionss += " AND C.Category = @BoatTypeId";
                            conditiontype += " AND C.Category = @BoatTypeId";
                        }
                        if (OtherServiceDtl.Category != "0")
                        {
                            conditions += " AND C.ServiceId = @Category";
                            conditionss += " AND C.ServiceId = @Category";
                            conditiontype += " AND C.ServiceId = @Category";
                        }
                        if (OtherServiceDtl.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType";
                            conditionss += " AND A.PaymentType = @PaymentType";
                            conditiontype += " AND A.PaymentType = @PaymentType";
                        }
                        if (OtherServiceDtl.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy";
                            conditionss += " AND A.CreatedBy = @CreatedBy";
                            conditiontype += " AND A.CreatedBy = @CreatedBy";
                        }

                        squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
                               + " FROM(SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) "
                               + " AS 'BookingDate', A.BookingId, C.ServiceName, "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue' FROM BookingOthersHistory AS A  INNER JOIN OtherServices AS C "
                               + " ON A.ServiceId = C.ServiceId  INNER JOIN ConfigurationMaster AS D "
                               + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + " UNION ALL "
                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue'  FROM BookingOthersHistory AS A  INNER JOIN BookingHdrHistory AS D ON D.BookingId = A.BookingId "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId"
                               + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                               + " " + conditiontype + " "
                               + " GROUP BY A.BookingDate, C.ServiceName, A.BookingId, A.NoOfItems, "
                               + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + " ) AS A "
                               + " GROUP BY  BookingDate, ServiceName,Ordervalue,BookingId, UniqueId, Createdby "
                               + " UNION ALL "
                               + " SELECT 0 AS 'UniqueId', 0 AS 'Createdby', '' AS 'BookingId' , BookingDate, 'Total' AS 'Particulars',   "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,1 AS 'Ordervalue' "
                               + " FROM(SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, 0 AS 'Ordervalue' FROM BookingOthersHistory AS A INNER JOIN OtherServices AS C "
                               + " ON A.ServiceId = C.ServiceId INNER JOIN ConfigurationMaster AS D "
                               + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionss + " "
                               + " GROUP BY A.BookingDate, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                               + "  ) AS A "
                               + " GROUP BY  BookingDate, Ordervalue "
                               + " ORDER BY BookingDate, ServiceName, Ordervalue ASC ";
                    }
                    else
                    {

                        string conditionsHistroy = string.Empty;
                        string conditionssHistroy = string.Empty;
                        string conditiontypeHistroy = string.Empty;

                        conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                                   + "@FromDate"
                                   + " AND @ToDate"
                                   + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditionss = "WHERE CAST(BookingDate AS DATE) BETWEEN  "
                              + " @FromDate "
                              + " AND @ToDate"
                              + " AND C.OtherServiceType = 'OS' AND A.BookingType <> 'A' AND"
                              + " C.BoatHouseId = @BoatHouseId AND "
                              + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN "
                              + "@FromDate"
                              + "AND @ToDate"
                              + "AND CAST(D.BookingDate AS DATE) BETWEEN "
                              + "@FromDate"
                              + " AND @ToDate"
                              + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS'"
                              + " AND A.BoatHouseId = @BoatHouseId AND"
                              + " C.BoatHouseId = @BoatHouseId AND "
                              + " D.BoatHouseId = @BoatHouseId AND "
                              + " E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";


                        conditionsHistroy = " WHERE BookingDate BETWEEN "
                                   + " @sFromDate"
                                   + " AND @sToDate"
                                   + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditionssHistroy = "WHERE BookingDate BETWEEN  "
                                    + " @sFromDate"
                                    + " AND @sToDate"
                                    + " AND C.OtherServiceType = 'OS' AND A.BookingType <> 'A' AND"
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                        conditiontypeHistroy = " WHERE A.BookingDate BETWEEN "
                                      + " @sFromDate"
                                      + " AND @sToDate"
                                      + " AND D.BookingDate BETWEEN "
                                      + " @sFromDate"
                                      + " AND @sToDate"
                                      + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                        if (OtherServiceDtl.BoatTypeId != "0")
                        {
                            conditions += " AND C.Category = @BoatTypeId";
                            conditionss += " AND C.Category = @BoatTypeId";
                            conditiontype += " AND C.Category = @BoatTypeId ";

                            conditionsHistroy += " AND C.Category = @BoatTypeId";
                            conditionssHistroy += " AND C.Category = @BoatTypeId";
                            conditiontypeHistroy += " AND C.Category = @BoatTypeId";
                        }
                        if (OtherServiceDtl.Category != "0")
                        {
                            conditions += " AND C.ServiceId = @Category";
                            conditionss += " AND C.ServiceId =  @Category";
                            conditiontype += " AND C.ServiceId = @Category";

                            conditionsHistroy += " AND C.ServiceId = @Category";
                            conditionssHistroy += " AND C.ServiceId = @Category";
                            conditiontypeHistroy += " AND C.ServiceId = @Category";
                        }
                        if (OtherServiceDtl.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType";
                            conditionss += " AND A.PaymentType = @PaymentType";
                            conditiontype += " AND A.PaymentType = @PaymentType";

                            conditionsHistroy += " AND A.PaymentType = @PaymentType";
                            conditionssHistroy += " AND A.PaymentType = @PaymentType";
                            conditiontypeHistroy += " AND A.PaymentType = @PaymentType";
                        }
                        if (OtherServiceDtl.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy";
                            conditionss += " AND A.CreatedBy = @CreatedBy";
                            conditiontype += " AND A.CreatedBy = @CreatedBy";

                            conditionsHistroy += " AND A.CreatedBy = @CreatedBy";
                            conditionssHistroy += " AND A.CreatedBy = @CreatedBy";
                            conditiontypeHistroy += " AND A.CreatedBy = @CreatedBy";
                        }

                        squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
                               + " FROM( "
                               + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) "
                               + " AS 'BookingDate', A.BookingId, C.ServiceName, "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue' FROM BookingOthers AS A  INNER JOIN OtherServices AS C "
                               + " ON A.ServiceId = C.ServiceId  INNER JOIN ConfigurationMaster AS D "
                               + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, "
                               + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + " UNION ALL "
                               + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) "
                               + " AS 'BookingDate', A.BookingId, C.ServiceName, "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue' FROM BookingOthersHistory AS A  INNER JOIN OtherServices AS C "
                               + " ON A.ServiceId = C.ServiceId  INNER JOIN ConfigurationMaster AS D "
                               + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionsHistroy + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, "
                               + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + " UNION ALL "
                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue'  FROM BookingOthers AS A  INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId"
                               + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                               + " " + conditiontype + " "
                               + " GROUP BY A.BookingDate, C.ServiceName, A.BookingId, A.NoOfItems, "
                               + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + " UNION ALL"
                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue'  FROM BookingOthersHistory AS A  INNER JOIN BookingHdrHistory AS D ON D.BookingId = A.BookingId "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId"
                               + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                               + " " + conditiontypeHistroy + " "
                               + " GROUP BY A.BookingDate, C.ServiceName, A.BookingId, A.NoOfItems, "
                               + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + " ) AS A "
                               + " GROUP BY  BookingDate, ServiceName,Ordervalue,BookingId, UniqueId, Createdby "
                               + " UNION ALL "
                               + " SELECT 0 AS 'UniqueId', 0 AS 'Createdby', '' AS 'BookingId' , BookingDate, 'Total' AS 'Particulars',   "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,1 AS 'Ordervalue' "
                               + " FROM( "
                               + " SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, 0 AS 'Ordervalue' FROM BookingOthers AS A INNER JOIN OtherServices AS C "
                               + " ON A.ServiceId = C.ServiceId INNER JOIN ConfigurationMaster AS D "
                               + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionss + " "
                               + " GROUP BY A.BookingDate, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                               + " UNION ALL"
                               + " SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
                               + " A.NetAmount, 0 AS 'Ordervalue' FROM BookingOthersHistory AS A INNER JOIN OtherServices AS C "
                               + " ON A.ServiceId = C.ServiceId INNER JOIN ConfigurationMaster AS D "
                               + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionssHistroy + " "
                               + " GROUP BY A.BookingDate, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                               + "  ) AS A "
                               + " GROUP BY  BookingDate, Ordervalue "
                               + " ORDER BY BookingDate, ServiceName, Ordervalue ASC ";
                    }
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sFromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@FromDate"].Value = DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@sFromDate"].Value = DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                cmd.Parameters["@sToDate"].Value = DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();
                
                cmd.Parameters["@BoatHouseId"].Value = OtherServiceDtl.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherServiceDtl.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherServiceDtl.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherServiceDtl.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherServiceDtl.CreatedBy.Trim();

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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptOtherServiceDetailed")]
        //public IHttpActionResult OtherServiceDtl([FromBody] BoatingReport OtherServiceDtl)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        string conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
        //               + " AND A.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " C.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";

        //        string conditionss = "WHERE CAST(BookingDate AS DATE) BETWEEN  "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND C.OtherServiceType = 'OS' AND A.BookingType <> 'A' AND"
        //               + " C.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus = 'B' ";
        //        string conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND CAST(D.BookingDate AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
        //               + " AND A.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " C.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " D.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus = 'B' ";

        //        if (OtherServiceDtl.BoatTypeId != "0")
        //        {
        //            conditions += " AND C.Category = '" + OtherServiceDtl.BoatTypeId + "' ";
        //            conditionss += " AND C.Category = '" + OtherServiceDtl.BoatTypeId + "' ";
        //            conditiontype += " AND C.Category = '" + OtherServiceDtl.BoatTypeId + "' ";
        //        }
        //        if (OtherServiceDtl.Category != "0")
        //        {
        //            conditions += " AND C.ServiceId = '" + OtherServiceDtl.Category + "' ";
        //            conditionss += " AND C.ServiceId = '" + OtherServiceDtl.Category + "' ";
        //            conditiontype += " AND C.ServiceId = '" + OtherServiceDtl.Category + "' ";
        //        }
        //        if (OtherServiceDtl.PaymentType != "0")
        //        {
        //            conditions += " AND A.PaymentType = '" + OtherServiceDtl.PaymentType + "'";
        //            conditionss += " AND A.PaymentType = '" + OtherServiceDtl.PaymentType + "'";
        //            conditiontype += " AND A.PaymentType = '" + OtherServiceDtl.PaymentType + "'";
        //        }
        //        if (OtherServiceDtl.CreatedBy != "0")
        //        {
        //            conditions += " AND A.CreatedBy = '" + OtherServiceDtl.CreatedBy + "'";
        //            conditionss += " AND A.CreatedBy = '" + OtherServiceDtl.CreatedBy + "'";
        //            conditiontype += " AND A.CreatedBy = '" + OtherServiceDtl.CreatedBy + "'";
        //        }
        //        if (DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) == DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate))
        //        {
        //            squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', 2 AS 'Ordervalue', "
        //                        + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
        //                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
        //                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
        //                        + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
        //                        + " FROM "
        //                        + " ( "
        //                        + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
        //                        + " ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', A.NetAmount, A.Createdby "
        //                        + " FROM BookingOthers AS A "
        //                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
        //                        + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
        //                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
        //                        + " " + conditions + " "
        //                        + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
        //                        + " UNION ALL "
        //                        + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', A.BookingId, C.ServiceName, "
        //                        + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,  ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', A.NetAmount, A.Createdby "
        //                        + " FROM BookingOthers AS A "
        //                        + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
        //                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
        //                        + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
        //                        + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
        //                        + " " + conditiontype + " "
        //                        + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
        //                        + " ) AS A "
        //                        + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "
        //                        + " ORDER BY Createdby, UniqueId, ServiceName, BookingDate ASC ";
        //        }
        //        else
        //        {
        //            squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', "
        //                     + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
        //                     + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
        //                     + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
        //                     + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
        //                     + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
        //                     + " FROM(SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) "
        //                     + " AS 'BookingDate', A.BookingId, C.ServiceName, "
        //                     + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
        //                     + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue' FROM BookingOthers AS A  INNER JOIN OtherServices AS C "
        //                     + " ON A.ServiceId = C.ServiceId  INNER JOIN ConfigurationMaster AS D "
        //                     + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
        //                     + " " + conditions + " "
        //                     + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
        //                     + " UNION ALL "
        //                     + " SELECT A.UniqueId AS 'UniqueId', "
        //                     + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
        //                     + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
        //                     + " A.NetAmount, A.Createdby, 0 AS 'Ordervalue'  FROM BookingOthers AS A  INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
        //                     + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId"
        //                     + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
        //                     + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
        //                     + " " + conditiontype + " "
        //                     + " GROUP BY A.BookingDate, C.ServiceName, A.BookingId, A.NoOfItems, "
        //                     + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
        //                     + " ) AS A "
        //                     + " GROUP BY  BookingDate, ServiceName,Ordervalue,BookingId, UniqueId, Createdby "
        //                     + " UNION ALL "
        //                     + " SELECT 0 AS 'UniqueId', 0 AS 'Createdby', '' AS 'BookingId' , BookingDate, 'Total' AS 'Particulars',   "
        //                     + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
        //                     + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',   "
        //                     + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
        //                     + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
        //                     + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,1 AS 'Ordervalue' "
        //                     + " FROM(SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
        //                     + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, ISNULL(SUM(A.CGSTTaxAmount) +  SUM(A.SGSTTaxAmount),0) AS 'TaxAmount', "
        //                     + " A.NetAmount, 0 AS 'Ordervalue' FROM BookingOthers AS A INNER JOIN OtherServices AS C "
        //                     + " ON A.ServiceId = C.ServiceId INNER JOIN ConfigurationMaster AS D "
        //                     + " ON C.Category = D.ConfigID  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
        //                     + " " + conditionss + " "
        //                     + " GROUP BY A.BookingDate, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
        //                     + "  ) AS A "
        //                     + " GROUP BY  BookingDate, Ordervalue "
        //                     + " ORDER BY BookingDate, ServiceName, Ordervalue ASC ";
        //        }

        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(squery, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        con.Close();

        //        if (ds != null)
        //        {
        //            return Ok(ds);
        //        }
        //        else
        //        {
        //            return Ok("No Records Found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return Ok(ConfRes);
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("OtherServiceDetailsCount")]
        public IHttpActionResult OtherServiceDetailsCount([FromBody] BookingOtherServices dtl)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string sBookingMedia = string.Empty;

                if (dtl.CreatedBy != "0")
                {
                    conditions = " AND BO.Createdby=@CreatedBy";
                    sBookingMedia = " AND BO.BookingMedia NOT IN ('PW', 'PA', 'PI')";
                }

                squery = " SELECT SUM(ISNULL(BO.NoOfItems,0)) AS 'BookingCount', ISNULL(SUM(ISNULL(BO.NetAmount, 0)), 0) AS 'NetAmount' "
                + " FROM BookingOthers AS BO "
                + " INNER JOIN OtherServices AS OS ON OS.ServiceId = BO.ServiceId AND OS.OtherServiceType = 'OS' "
                + " WHERE CAST(BookingDate AS DATE) = @BookingDate AND BO.BoatHouseId = @BoatHouseId"
                + " AND BStatus = 'B' " + conditions + sBookingMedia;


                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(dtl.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = dtl.BoatHouseId.Trim();
                cmd.Parameters["@CreatedBy"].Value = dtl.CreatedBy.Trim();

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
        [Route("OtherBookedDetailsPopup")]
        public IHttpActionResult OtherBookedDetailsPopup([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "OtherBookedDetailsPopup");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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
        [Route("AddTicketDetailsCount")]
        public IHttpActionResult AddTicketDetailsCount([FromBody] BookingOtherServices dtl)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string sBookingMedia = string.Empty;

                if (dtl.CreatedBy != "0")
                {
                    conditions = " AND BO.Createdby= @CreatedBy";
                    sBookingMedia = " AND BO.BookingMedia NOT IN ('PW', 'PA', 'PI')";
                }

                squery = " SELECT SUM(ISNULL(Bo.NoOfItems,0)) AS 'BookingCount', ISNULL(SUM(ISNULL(BO.NetAmount, 0)), 0) AS 'NetAmount' "
                + " FROM BookingOthers AS BO "
                + " INNER JOIN OtherServices AS OS ON OS.ServiceId = BO.ServiceId AND OS.OtherServiceType = 'AB' "
                + " WHERE CAST(BookingDate AS DATE) = @BookingDate AND BO.BoatHouseId =@BoatHouseId"
                + " AND BStatus = 'B' " + conditions + sBookingMedia;


                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(dtl.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = dtl.BoatHouseId.Trim();
                cmd.Parameters["@CreatedBy"].Value = dtl.CreatedBy.Trim();
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
        [Route("AdditionalTicketBookedDetailsPopup")]
        public IHttpActionResult AdditionalTicketBookedDetailsPopup([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "AdditionalTicketBookedDetailsPopup");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherTicketBookedDtl otl = new OtherTicketBookedDtl();
                        otl.BookingId = dt.Rows[i]["BookingId"].ToString();
                        otl.BookingType = dt.Rows[i]["BookingType"].ToString();
                        otl.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        otl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        otl.ItemCharge = dt.Rows[i]["ItemCharge"].ToString();
                        otl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                        otl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                        otl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                        otl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                        otl.CustomerMobileNo = dt.Rows[i]["CustomerMobileNo"].ToString();
                        otl.Status = dt.Rows[i]["Status"].ToString();

                        li.Add(otl);
                    }

                    OtherTicketBookedDtlLst odl = new OtherTicketBookedDtlLst
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(odl);
                }

                else
                {
                    OtherTicketBookedDtlStr ods = new OtherTicketBookedDtlStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ods);
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
        ///  Imran 02-sep-2021
        ///  Modified By :  Abhi
        ///  Modified Date : 03-Sep-2021
        /// </summary>
        /// <param name="OtherService"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptOtherServiceWiseAdditionalCollection")]
        public IHttpActionResult OtherServiceAdditional([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                    + " @BookingDate"
                    + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                    + " AND A.BoatHouseId = @BoatHouseId AND "
                    + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '34' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                    + " @BookingDate "
                    + " AND CAST(D.BookingDate AS DATE) BETWEEN @BookingDate AND "
                    + " @BookingDate "
                    + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                    + " AND A.BoatHouseId = @BoatHouseId AND "
                    + " C.BoatHouseId = @BoatHouseId AND "
                    + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '34' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }

                squery = " SELECT BookingDate,ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                        + " ISNULL(CAST((SUM(TaxAmount)/2) AS DECIMAL(18,2)),0) AS 'CGST', "
                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                        + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                        + " FROM "
                        + " ( "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                        + " (SUM(A.CGSTTaxAmount)+SUM(A.SGSTTaxAmount)) AS 'TaxAmount', A.NetAmount "
                        + " FROM BookingOthers AS A "
                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                        + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                        + " UNION ALL "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, "
                        + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, (SUM(A.CGSTTaxAmount)+SUM(A.SGSTTaxAmount)) AS 'TaxAmount' , A.NetAmount "
                        + " FROM BookingOthers AS A "
                        + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                        + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                        + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                        + " " + conditionType + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                        + " ) AS A "
                        + " GROUP BY BookingDate,ServiceName ";


                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();

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
        [Route("RptOtherServiceAdditionalDtl")]
        public IHttpActionResult OtherServiceAdditionalDtl([FromBody] BoatingReport OtherServiceDtl)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string conditionss = string.Empty;
                string conditiontype = string.Empty;

                string conditionsHistory = string.Empty;
                string conditionssHistory = string.Empty;
                string conditiontypeHistory = string.Empty;

                string sBookingHdr = string.Empty;
                string sBookingOthers = string.Empty;


                if (DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) == DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate))
                {
                    conditions = string.Empty;
                    conditionss = string.Empty;
                    conditiontype = string.Empty;

                    if (CheckDate(OtherServiceDtl.FromDate.Trim()))
                    {
                        conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                                   + " @FromDate"
                                   + " AND @ToDate"
                                   + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditionss = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                                    + " @FromDate"
                                    + " AND @ToDate"
                                    + " AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN "
                                      + " @FromDate"
                                      + " AND @ToDate"
                                      + " AND CAST(D.BookingDate AS DATE) BETWEEN "
                                      + " @FromDate"
                                      + " AND @ToDate"
                                      + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '34' AND A.BStatus = 'B' ";

                        sBookingHdr = "BookingHdr";
                        sBookingOthers = "BookingOthers";
                    }
                    else
                    {

                        conditions = " WHERE BookingDate BETWEEN "
                                    + " @sFromDate "
                                    + " AND @sToDate"
                                    + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditionss = " WHERE BookingDate BETWEEN "
                                    + " @sFromDate "
                                    + " AND @sToDate"
                                    + " AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE A.BookingDate BETWEEN "
                                      + " @sFromDate"
                                      + " AND @sToDate"
                                      + " AND D.BookingDate BETWEEN "
                                      + " @sFromDate"
                                      + " AND @sToDate"
                                      + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '34' AND A.BStatus = 'B' ";

                        sBookingHdr = "BookingHdrHistory";
                        sBookingOthers = "BookingOthersHistory";
                    }

                    if (OtherServiceDtl.BoatTypeId != "0")
                    {
                        conditions += " AND C.Category = @BoatTypeId";
                        conditionss += " AND C.Category = @BoatTypeId";
                        conditiontype += " AND C.Category = @BoatTypeId";
                    }
                    if (OtherServiceDtl.Category != "0")
                    {
                        conditions += " AND C.ServiceId = @Category";
                        conditionss += " AND C.ServiceId = @Category";
                        conditiontype += " AND C.ServiceId = @Category";
                    }
                    if (OtherServiceDtl.PaymentType != "0")
                    {
                        conditions += " AND A.PaymentType = @PaymentType";
                        conditionss += " AND A.PaymentType = @PaymentType";
                        conditiontype += " AND A.PaymentType = @PaymentType";
                    }
                    if (OtherServiceDtl.CreatedBy != "0")
                    {
                        conditions += " AND A.CreatedBy = @CreatedBy";
                        conditionss += " AND A.CreatedBy = @CreatedBy";
                        conditiontype += " AND A.CreatedBy = @CreatedBy";
                    }

                    squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', 2 AS 'Ordervalue', "
                       + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                       + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                       + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                       + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                       + " FROM "
                       + " ( "
                       + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                       + " (SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                       + " FROM " + sBookingOthers + " AS A "
                       + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                       + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                       + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                       + " " + conditions + " "
                       + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                       + " UNION ALL "
                       + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', A.BookingId, C.ServiceName, "
                       + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                       + " FROM " + sBookingOthers + " AS A "
                       + " INNER JOIN " + sBookingHdr + " AS D ON D.BookingId = A.BookingId "
                       + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                       + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                       + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                       + " " + conditiontype + " "
                       + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                       + " ) AS A "
                       + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "
                       + " ORDER BY Createdby, UniqueId, ServiceName, BookingDate ASC ";
                }
                else
                {
                    if (CheckDate(OtherServiceDtl.FromDate.Trim()) && CheckDate(OtherServiceDtl.ToDate.Trim()))
                    {
                        conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                                   + " @FromDate "
                                   + " AND @ToDate"
                                   + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditionss = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                                    + " @FromDate"
                                    + " AND @ToDate"
                                    + " AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN "
                                      + " @FromDate"
                                      + " AND @ToDate"
                                      + " AND CAST(D.BookingDate AS DATE) BETWEEN "
                                      + " @FromDate"
                                      + " AND @ToDate"
                                      + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '34' AND A.BStatus = 'B' ";

                        if (OtherServiceDtl.BoatTypeId != "0")
                        {
                            conditions += " AND C.Category = @BoatTypeId";
                            conditionss += " AND C.Category = @BoatTypeId";
                            conditiontype += " AND C.Category = @BoatTypeId";
                        }
                        if (OtherServiceDtl.Category != "0")
                        {
                            conditions += " AND C.ServiceId = @Category";
                            conditionss += " AND C.ServiceId = @Category";
                            conditiontype += " AND C.ServiceId = @Category";
                        }
                        if (OtherServiceDtl.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType";
                            conditionss += " AND A.PaymentType = @PaymentType";
                            conditiontype += " AND A.PaymentType = @PaymentType";
                        }
                        if (OtherServiceDtl.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy";
                            conditionss += " AND A.CreatedBy = @CreatedBy";
                            conditiontype += " AND A.CreatedBy = @CreatedBy";
                        }

                        squery = " SELECT UniqueId, Createdby, BookingId, BookingDate, ServiceName AS 'Particulars', "
                            + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                            + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                            + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                            + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                            + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                            + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
                            + " FROM(SELECT A.UniqueId AS 'UniqueId', "
                            + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                            + "  A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                            + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                            + " FROM BookingOthers AS A "
                            + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                            + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                            + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                            + " " + conditions + " "
                            + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                            + " UNION ALL "
                            + " SELECT A.UniqueId AS 'UniqueId', "
                            + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                            + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                            + " A.NoOfItems, (SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                            + " FROM BookingOthers AS A  INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                            + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                            + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                            + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                            + " " + conditiontype + " "
                            + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby) AS A "
                            + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "
                            + " UNION ALL "
                            + " SELECT '' as UniqueId, '' as Createdby,'' as BookingId,BookingDate, 'Total' AS 'Particulars', "
                            + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                            + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                            + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                            + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                            + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                            + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount', 1 AS 'Ordervalue' "
                            + " FROM(SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM'  AS 'BookingDate', "
                            + "  (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                            + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                            + " FROM BookingOthers AS A "
                            + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                            + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                            + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                            + " " + conditionss + " "
                            + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby) AS A "
                            + " GROUP BY  BookingDate "
                            + " ORDER BY BookingDate, Ordervalue ASC ";
                    }
                    else if (!CheckDate(OtherServiceDtl.FromDate.Trim()) && !CheckDate(OtherServiceDtl.ToDate.Trim()))
                    {

                        conditions = " WHERE BookingDate BETWEEN "
                                    + " @sFromDate"
                                    + " AND @sToDate"
                                    + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditionss = " WHERE BookingDate BETWEEN "
                                    + " @sFromDate "
                                    + " AND @sToDate"
                                    + " AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE A.BookingDate BETWEEN "
                                      + " @sFromDate "
                                      + " AND @sToDate "
                                      + " AND D.BookingDate BETWEEN "
                                      + " @sFromDate "
                                      + " AND @sToDate"
                                      + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '34' AND A.BStatus = 'B' ";

                        squery = " SELECT UniqueId, Createdby, BookingId, BookingDate, ServiceName AS 'Particulars', "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
                               + " FROM(SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + "  A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthersHistory AS A "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                               + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + " UNION ALL "
                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems, (SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthersHistory AS A  INNER JOIN BookingHdrHistory AS D ON D.BookingId = A.BookingId "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                               + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                               + " " + conditiontype + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby) AS A "
                               + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "
                               + " UNION ALL "
                               + " SELECT '' as UniqueId, '' as Createdby,'' as BookingId,BookingDate, 'Total' AS 'Particulars', "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount', 1 AS 'Ordervalue' "
                               + " FROM(SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM'  AS 'BookingDate', "
                               + "  (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthersHistory AS A "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                               + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionss + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby) AS A "
                               + " GROUP BY  BookingDate "
                               + " ORDER BY BookingDate, Ordervalue ASC ";

                    }
                    else
                    {
                        conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                                    + " @FromDate"
                                    + " AND @ToDate"
                                    + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditionss = " WHERE CAST(BookingDate AS DATE) BETWEEN "
                              + " @FromDate"
                              + " AND @ToDate"
                              + " AND C.OtherServiceType = 'AB'"
                              + " AND A.BoatHouseId = @BoatHouseId AND "
                              + " C.BoatHouseId = @BoatHouseId AND "
                              + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN "
                              + " @FromDate"
                              + " AND @ToDate"
                              + " AND CAST(D.BookingDate AS DATE) BETWEEN "
                              + " @FromDate"
                              + " AND @ToDate"
                              + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                              + " AND A.BoatHouseId = @BoatHouseId AND "
                              + " C.BoatHouseId = @BoatHouseId AND "
                              + " D.BoatHouseId = @BoatHouseId AND "
                              + " E.TypeID = '27' AND F.TypeID = '34' AND A.BStatus = 'B' ";


                        conditionsHistory = " WHERE BookingDate BETWEEN "
                                    + " @sFromDate"
                                    + " AND @sToDate"
                                    + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditionssHistory = " WHERE BookingDate BETWEEN "
                                    + " @sFromDate "
                                    + " AND @sToDate"
                                    + " AND C.OtherServiceType = 'AB' "
                                    + " AND A.BoatHouseId = @BoatHouseId AND "
                                    + " C.BoatHouseId = @BoatHouseId AND "
                                    + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

                        conditiontypeHistory = " WHERE A.BookingDate BETWEEN "
                                      + " @sFromDate "
                                      + " AND @sToDate "
                                      + " AND D.BookingDate BETWEEN "
                                      + " '@sFromDate"
                                      + " AND @sToDate "
                                      + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                                      + " AND A.BoatHouseId = @BoatHouseId AND "
                                      + " C.BoatHouseId = @BoatHouseId AND "
                                      + " D.BoatHouseId = @BoatHouseId AND "
                                      + " E.TypeID = '27' AND F.TypeID = '34' AND A.BStatus = 'B' ";

                        if (OtherServiceDtl.BoatTypeId != "0")
                        {
                            conditions += " AND C.Category = @BoatTypeId ";
                            conditionss += " AND C.Category = @BoatTypeId ";
                            conditiontype += " AND C.Category = @BoatTypeId ";

                            conditionsHistory += " AND C.Category = @BoatTypeId ";
                            conditionssHistory += " AND C.Category = @BoatTypeId ";
                            conditiontypeHistory += " AND C.Category = @BoatTypeId ";
                        }
                        if (OtherServiceDtl.Category != "0")
                        {
                            conditions += " AND C.ServiceId = @Category ";
                            conditionss += " AND C.ServiceId = @Category ";
                            conditiontype += " AND C.ServiceId = @Category ";

                            conditionsHistory += " AND C.ServiceId = @Category ";
                            conditionssHistory += " AND C.ServiceId = @Category ";
                            conditiontypeHistory += " AND C.ServiceId = @Category ";
                        }
                        if (OtherServiceDtl.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType";
                            conditionss += " AND A.PaymentType = @PaymentType";
                            conditiontype += " AND A.PaymentType = @PaymentType";

                            conditionsHistory += " AND A.PaymentType = @PaymentType";
                            conditionssHistory += " AND A.PaymentType = @PaymentType";
                            conditiontypeHistory += " AND A.PaymentType = @PaymentType";
                        }
                        if (OtherServiceDtl.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy";
                            conditionss += " AND A.CreatedBy = @CreatedBy";
                            conditiontype += " AND A.CreatedBy = @CreatedBy";

                            conditionsHistory += " AND A.CreatedBy = @CreatedBy";
                            conditionssHistory += " AND A.CreatedBy = @CreatedBy";
                            conditiontypeHistory += " AND A.CreatedBy = @CreatedBy";
                        }
                        squery = " SELECT UniqueId, Createdby, BookingId, BookingDate, ServiceName AS 'Particulars', "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
                               + " FROM( "

                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + "  A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthers AS A "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                               + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, "
                               + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "

                               + " UNION ALL "

                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + "  A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthersHistory AS A "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                               + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionsHistory + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, "
                               + " A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "

                               + " UNION ALL "

                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems, (SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthers AS A  INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                               + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                               + " " + conditiontype + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "

                               + " UNION ALL "

                               + " SELECT A.UniqueId AS 'UniqueId', "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems, (SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthersHistory AS A  INNER JOIN BookingHdrHistory AS D ON D.BookingId = A.BookingId "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                               + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                               + " " + conditiontypeHistory + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + ") AS A "
                               + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "

                               + " UNION ALL "

                               + " SELECT '' as UniqueId, '' as Createdby,'' as BookingId,BookingDate, 'Total' AS 'Particulars', "
                               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
                               + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount', 1 AS 'Ordervalue' "
                               + " FROM "
                               + " ( "
                               + " SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM'  AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthers AS A "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                               + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionss + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, "
                               + " A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "

                                + " UNION ALL "

                               + " SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM'  AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
                               + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
                               + " FROM BookingOthersHistory AS A "
                               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                               + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                               + " " + conditionssHistory + " "
                               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, "
                               + " A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
                               + ") AS A "
                               + " GROUP BY  BookingDate "
                               + " ORDER BY BookingDate, Ordervalue ASC ";
                    }
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);


                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sFromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@sFromDate"].Value = DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                cmd.Parameters["@sToDate"].Value = DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherServiceDtl.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherServiceDtl.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherServiceDtl.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherServiceDtl.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherServiceDtl.CreatedBy.Trim();

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

        ///// <summary>
        ///// Imran 02-sep-2021
        ///// ///  Modified By :  Abhi
        /////  Modified Date : 03-Sep-2021
        ///// </summary>
        ///// <param name="OtherServiceDtl"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptOtherServiceAdditionalDtl")]
        //public IHttpActionResult OtherServiceAdditionalDtl([FromBody] BoatingReport OtherServiceDtl)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        string conditions = " WHERE CAST(BookingDate AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
        //               + " AND A.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " C.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";
        //        string conditionss = " WHERE CAST(BookingDate AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND C.OtherServiceType = 'AB' "
        //               + " AND A.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " C.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " D.TypeID = '27' AND E.TypeID = '34' AND A.BStatus = 'B' ";

        //        string conditiontype = " WHERE CAST(A.BookingDate AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND CAST(D.BookingDate AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) + "' "
        //               + " AND '" + DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate) + "' "
        //               + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
        //               + " AND A.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " C.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " D.BoatHouseId = '" + OtherServiceDtl.BoatHouseId.Trim() + "' AND "
        //               + " E.TypeID = '27' AND F.TypeID = '34' AND A.BStatus = 'B' ";

        //        if (OtherServiceDtl.BoatTypeId != "0")
        //        {
        //            conditions += " AND C.Category = '" + OtherServiceDtl.BoatTypeId + "' ";
        //            conditionss += " AND C.Category = '" + OtherServiceDtl.BoatTypeId + "' ";
        //            conditiontype += " AND C.Category = '" + OtherServiceDtl.BoatTypeId + "' ";
        //        }
        //        if (OtherServiceDtl.Category != "0")
        //        {
        //            conditions += " AND C.ServiceId = '" + OtherServiceDtl.Category + "' ";
        //            conditionss += " AND C.ServiceId = '" + OtherServiceDtl.Category + "' ";
        //            conditiontype += " AND C.ServiceId = '" + OtherServiceDtl.Category + "' ";
        //        }
        //        if (OtherServiceDtl.PaymentType != "0")
        //        {
        //            conditions += " AND A.PaymentType = '" + OtherServiceDtl.PaymentType + "'";
        //            conditionss += " AND A.PaymentType = '" + OtherServiceDtl.PaymentType + "'";
        //            conditiontype += " AND A.PaymentType = '" + OtherServiceDtl.PaymentType + "'";
        //        }
        //        if (OtherServiceDtl.CreatedBy != "0")
        //        {
        //            conditions += " AND A.CreatedBy = '" + OtherServiceDtl.CreatedBy + "'";
        //            conditionss += " AND A.CreatedBy = '" + OtherServiceDtl.CreatedBy + "'";
        //            conditiontype += " AND A.CreatedBy = '" + OtherServiceDtl.CreatedBy + "'";
        //        }
        //        if (DateTime.Parse(OtherServiceDtl.FromDate.Trim(), objEnglishDate) == DateTime.Parse(OtherServiceDtl.ToDate.Trim(), objEnglishDate))
        //        {
        //            squery = " SELECT UniqueId, Createdby, BookingId,BookingDate,ServiceName AS 'Particulars', 2 AS 'Ordervalue', "
        //               + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
        //               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
        //               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
        //               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
        //               + " FROM "
        //               + " ( "
        //               + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
        //               + " (SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
        //               + " FROM BookingOthers AS A "
        //               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
        //               + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
        //               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
        //               + " " + conditions + " "
        //               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
        //               + " UNION ALL "
        //               + " SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', A.BookingId, C.ServiceName, "
        //               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
        //               + " FROM BookingOthers AS A "
        //               + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
        //               + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
        //               + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
        //               + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
        //               + " " + conditiontype + " "
        //               + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
        //               + " ) AS A "
        //               + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "
        //               + " ORDER BY Createdby, UniqueId, ServiceName, BookingDate ASC ";
        //        }
        //        else
        //        {
        //            squery = " SELECT UniqueId, Createdby, BookingId, BookingDate, ServiceName AS 'Particulars', "
        //                      + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
        //                      + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
        //                      + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
        //                      + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
        //                      + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
        //                      + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,0 AS 'Ordervalue' "
        //                      + " FROM(SELECT A.UniqueId AS 'UniqueId', "
        //                      + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
        //                      + "  A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
        //                      + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
        //                      + " FROM BookingOthers AS A "
        //                      + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
        //                      + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
        //                      + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
        //                      + " " + conditions + " "
        //                      + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby "
        //                      + " UNION ALL "
        //                      + " SELECT A.UniqueId AS 'UniqueId', "
        //                      + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate', "
        //                      + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
        //                      + " A.NoOfItems, (SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
        //                      + " FROM BookingOthers AS A  INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
        //                      + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
        //                      + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
        //                      + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
        //                      + " " + conditiontype + " "
        //                      + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby) AS A "
        //                      + " GROUP BY BookingId, BookingDate, ServiceName, UniqueId, Createdby "
        //                      + " UNION ALL "
        //                      + " SELECT '' as UniqueId, '' as Createdby,'' as BookingId,BookingDate, 'Total' AS 'Particulars', "
        //                      + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
        //                      + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
        //                      + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
        //                      + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
        //                      + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) "
        //                      + " + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount', 1 AS 'Ordervalue' "
        //                      + " FROM(SELECT CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM'  AS 'BookingDate', "
        //                      + "  (A.ChargePerItem * A.NoOfItems) AS 'Charge', "
        //                      + " A.NoOfItems,(SUM(A.CGSTTaxAmount) + SUM(A.SGSTTaxAmount))  AS 'TaxAmount', A.NetAmount, A.Createdby "
        //                      + " FROM BookingOthers AS A "
        //                      + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
        //                      + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
        //                      + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
        //                      + " " + conditionss + " "
        //                      + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.Createdby) AS A "
        //                      + " GROUP BY  BookingDate "
        //                      + " ORDER BY BookingDate, Ordervalue ASC ";
        //        }

        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(squery, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        con.Close();

        //        if (ds != null)
        //        {
        //            return Ok(ds);
        //        }
        //        else
        //        {
        //            return Ok("No Records Found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
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
        /// Modified By : Subalakshmi
        /// Modified Date : 23-09-2021
        /// Modified By Abhinaya K
        /// Modified Date 09-05-2022
        /// </summary>
        /// <param name="UserBased"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptUserBasedServiceList")]
        public IHttpActionResult UserBasedService([FromBody] BoatingReport UserBased)
        {
            try
            {
                string squery = string.Empty;

                if (UserBased.BoatHouseId != "" && UserBased.FromDate != "" && UserBased.ToDate != ""
                    && UserBased.CreatedBy != "")
                {
                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");


                    if (DateTime.Parse(UserBased.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(UserBased.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate',  C.BoatType AS 'BoatType', "
                               + " D.SeaterType AS 'BoatSeater',  COUNT(A.BookingId) AS 'Count', ISNULL(SUM(A.InitBoatCharge), 0) + "
                               + " ISNULL(SUM(A.InitRowerCharge), 0) + ISNULL(SUM(A.CGSTTaxAmount), 0) + "
                               + " ISNULL(SUM(A.CGSTTaxAmount), 0) as 'Amount', ISNULL(SUM(A.BoatDeposit), 0) AS 'BoatDeposit' , "
                               + " ISNULL(SUM(A.InitNetAmount), 0) AS 'NetAmount' "
                               + " FROM BookingDtl AS A INNER JOIN  BookingHdr AS B ON A.BookingId = B.BookingId "
                               + " AND A.BoatHouseId = B.BoatHouseId "
                               + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                               + " AND B.BoatHouseId = C.BoatHouseId  INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId "
                               + " AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
                               + " WHERE CAST(B.BookingDate AS DATE) BETWEEN  @FromDate "
                               + " AND @ToDate "
                               + " AND B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoatHouseId = @BoatHouseId"
                               + " AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId"
                               + "  AND D.BoatHouseId = @BoatHouseId"
                               + " AND B.Createdby =  @CreatedBy "
                               + " GROUP BY CAST(B.BookingDate AS DATE), C.BoatType, D.SeaterType ";

                    }

                    else if (DateTime.Parse(UserBased.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(UserBased.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate',  C.BoatType AS 'BoatType', "
                               + " D.SeaterType AS 'BoatSeater',  COUNT(A.BookingId) AS 'Count', ISNULL(SUM(A.InitBoatCharge), 0) + "
                               + " ISNULL(SUM(A.InitRowerCharge), 0) + ISNULL(SUM(A.CGSTTaxAmount), 0) + "
                               + " ISNULL(SUM(A.CGSTTaxAmount), 0) as 'Amount', ISNULL(SUM(A.BoatDeposit), 0) AS 'BoatDeposit' , "
                               + " ISNULL(SUM(A.InitNetAmount), 0) AS 'NetAmount' "
                               + " FROM BookingDtlHistory AS A INNER JOIN  BookingHdrHistory AS B ON A.BookingId = B.BookingId "
                               + " AND A.BoatHouseId = B.BoatHouseId "
                               + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                               + " AND B.BoatHouseId = C.BoatHouseId  INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId "
                               + " AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
                               + " WHERE CAST(B.BookingDate AS DATE) BETWEEN  @FromDate "
                               + " AND @ToDate "
                               + " AND B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoatHouseId = @BoatHouseId "
                               + " AND B.BoatHouseId = @BoatHouseId  AND C.BoatHouseId = @BoatHouseId "
                               + "  AND D.BoatHouseId = @BoatHouseId "
                               + " AND B.Createdby =  @CreatedBy "
                               + " GROUP BY CAST(B.BookingDate AS DATE), C.BoatType, D.SeaterType ";

                    }

                    else
                    {
                        squery = "SELECT * FROM ("
                            + " SELECT CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate',  C.BoatType AS 'BoatType', "
                            + " D.SeaterType AS 'BoatSeater',  COUNT(A.BookingId) AS 'Count', ISNULL(SUM(A.InitBoatCharge), 0) + "
                            + " ISNULL(SUM(A.InitRowerCharge), 0) + ISNULL(SUM(A.CGSTTaxAmount), 0) + "
                            + " ISNULL(SUM(A.CGSTTaxAmount), 0) as 'Amount', ISNULL(SUM(A.BoatDeposit), 0) AS 'BoatDeposit' , "
                            + " ISNULL(SUM(A.InitNetAmount), 0) AS 'NetAmount' "
                            + " FROM BookingDtl AS A INNER JOIN  BookingHdr AS B ON A.BookingId = B.BookingId "
                            + " AND A.BoatHouseId = B.BoatHouseId "
                            + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                            + " AND B.BoatHouseId = C.BoatHouseId  INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId "
                            + " AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
                            + " WHERE CAST(B.BookingDate AS DATE) BETWEEN  @FromDate "
                            + " AND @ToDate "
                            + " AND B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoatHouseId = @BoatHouseId "
                            + " AND B.BoatHouseId = @BoatHouseId  AND C.BoatHouseId = @BoatHouseId "
                            + "  AND D.BoatHouseId = @BoatHouseId "
                            + " AND B.Createdby =  @CreatedBy "
                            + " GROUP BY CAST(B.BookingDate AS DATE), C.BoatType, D.SeaterType "
                            + " UNION ALL"
                            + " SELECT CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate',  C.BoatType AS 'BoatType', "
                            + " D.SeaterType AS 'BoatSeater',  COUNT(A.BookingId) AS 'Count', ISNULL(SUM(A.InitBoatCharge), 0) + "
                            + " ISNULL(SUM(A.InitRowerCharge), 0) + ISNULL(SUM(A.CGSTTaxAmount), 0) + "
                            + " ISNULL(SUM(A.CGSTTaxAmount), 0) as 'Amount', ISNULL(SUM(A.BoatDeposit), 0) AS 'BoatDeposit' , "
                            + " ISNULL(SUM(A.InitNetAmount), 0) AS 'NetAmount' "
                            + " FROM BookingDtlHistory AS A INNER JOIN  BookingHdrHistory AS B ON A.BookingId = B.BookingId "
                            + " AND A.BoatHouseId = B.BoatHouseId "
                            + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                            + " AND B.BoatHouseId = C.BoatHouseId  INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId "
                            + " AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
                            + " WHERE CAST(B.BookingDate AS DATE) BETWEEN  @FromDate"
                            + " AND @ToDate "
                            + " AND B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoatHouseId = @BoatHouseId"
                            + " AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId"
                            + "  AND D.BoatHouseId = @BoatHouseId "
                            + " AND B.Createdby = @CreatedBy "
                            + " GROUP BY CAST(B.BookingDate AS DATE), C.BoatType, D.SeaterType) AS A ORDER BY BookingDate DESC "; ;

                    }

                    SqlCommand cmd = new SqlCommand(squery, con);
                    List<BoatingReport> li = new List<BoatingReport>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(UserBased.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(UserBased.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = UserBased.BoatHouseId.Trim();
                    cmd.Parameters["@CreatedBy"].Value = UserBased.CreatedBy.Trim();

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

                            UserBasedList.BoatCharge = dt.Rows[i]["Amount"].ToString();
                            UserBasedList.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            UserBasedList.Amount = dt.Rows[i]["NetAmount"].ToString();
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

        ///// <summary>
        ///// Modified By : Subalakshmi
        ///// Modified Date : 23-09-2021
        ///// </summary>
        ///// <param name="UserBased"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptUserBasedServiceList")]
        //public IHttpActionResult UserBasedService([FromBody] BoatingReport UserBased)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (UserBased.BoatHouseId != "" && UserBased.FromDate != "" && UserBased.ToDate != ""
        //            && UserBased.CreatedBy != "")
        //        {
        //            squery = " SELECT CONVERT(NVARCHAR(20), CAST(B.BookingDate AS DATE), 103) AS 'BookingDate',  C.BoatType AS 'BoatType', "
        //                        + " D.SeaterType AS 'BoatSeater',  COUNT(A.BookingId) AS 'Count', ISNULL(SUM(A.InitBoatCharge), 0) + "
        //                        + " ISNULL(SUM(A.InitRowerCharge), 0) + ISNULL(SUM(A.CGSTTaxAmount), 0) + "
        //                        + " ISNULL(SUM(A.CGSTTaxAmount), 0) as 'Amount', ISNULL(SUM(A.BoatDeposit), 0) AS 'BoatDeposit' , "
        //                        + " ISNULL(SUM(A.InitNetAmount), 0) AS 'NetAmount' "
        //                        + " FROM BookingDtl AS A INNER JOIN  BookingHdr AS B ON A.BookingId = B.BookingId "
        //                        + " AND A.BoatHouseId = B.BoatHouseId "
        //                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
        //                        + " AND B.BoatHouseId = C.BoatHouseId  INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId "
        //                        + " AND A.BoatHouseId = D.BoatHouseId  AND B.BoatHouseId = D.BoatHouseId "
        //                        + " WHERE CAST(B.BookingDate AS DATE) BETWEEN  '" + DateTime.Parse(UserBased.FromDate.Trim(), objEnglishDate) + "' "
        //                        + " AND '" + DateTime.Parse(UserBased.ToDate.Trim(), objEnglishDate) + "'  "
        //                        + " AND B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoatHouseId = '" + UserBased.BoatHouseId.Trim() + "' "
        //                        + " AND B.BoatHouseId = '" + UserBased.BoatHouseId.Trim() + "'  AND C.BoatHouseId = '" + UserBased.BoatHouseId.Trim() + "' "
        //                        + "  AND D.BoatHouseId = '" + UserBased.BoatHouseId.Trim() + "' "
        //                        + " AND B.Createdby =  '" + UserBased.CreatedBy.Trim() + "' "
        //                        + " GROUP BY CAST(B.BookingDate AS DATE), C.BoatType, D.SeaterType ";


        //            SqlCommand cmd = new SqlCommand(squery, con);
        //            List<BoatingReport> li = new List<BoatingReport>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);

        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    BoatingReport UserBasedList = new BoatingReport();

        //                    UserBasedList.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    UserBasedList.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                    UserBasedList.BoatSeater = dt.Rows[i]["BoatSeater"].ToString();
        //                    UserBasedList.Count = dt.Rows[i]["Count"].ToString();

        //                    UserBasedList.BoatCharge = dt.Rows[i]["Amount"].ToString();
        //                    UserBasedList.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
        //                    UserBasedList.Amount = dt.Rows[i]["NetAmount"].ToString();
        //                    li.Add(UserBasedList);
        //                }

        //                BoatingReportList BoatRate = new BoatingReportList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }
        //            else
        //            {
        //                BoatingReportres BoatRate = new BoatingReportres
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            BoatingReportres Vehicle = new BoatingReportres
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
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return Ok(ConfRes);
        //    }

        //}


        // Newly added by Brijin and Imran on 09-05-2022
        /// <summary>
        /// /Modified By Brijin On 19-05-2022
        /// </summary>
        /// <param name="Oth"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAdditionalTktSummary")]
        public IHttpActionResult RptAdditionalTktSummary([FromBody] OthServiceCat Oth)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
                    && Oth.FromDate != "" && Oth.ToDate != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '34' AND A.BoatHouseId = @BoatHouseId "
                        + " AND B.BoatHouseId = @BoatHouseId AND B.OtherServiceType = 'AB' AND A.BStatus = 'B' "
                        + " AND CAST(A.BookingDate AS DATE) BETWEEN @FromDate AND @ToDate";

                    if (Oth.Category != "0")
                    {
                        condition += " AND C.ConfigID= @Category";
                    }
                    if (Oth.ServiceId != "0")
                    {
                        condition += " AND A.ServiceId = @ServiceId";
                    }

                    if (Oth.BookingType != "0")
                    {
                        condition += " AND A.BookingType =@BookingType";
                    }
                    else
                    {
                        condition += " AND A.BookingType IN ('A','B') ";
                    }

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT C.ConfigName AS 'CategoryName', B.ServiceName, CASE WHEN A.BookingType ='A' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
                           + " ISNULL(SUM(A.NoOfItems),2) AS 'NoOfItems', ISNULL(B.ServiceTotalAmount, 0) AS 'ItemAmount',"
                          + " ISNULL(SUM(A.NetAmount),0) AS 'NetAmount' FROM BookingOthers AS A"
                          + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                          + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                          + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType Order by C.ConfigName";
                    }

                    else if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT C.ConfigName AS 'CategoryName', B.ServiceName, CASE WHEN A.BookingType ='A' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
                           + " ISNULL(SUM(A.NoOfItems),2) AS 'NoOfItems', ISNULL(B.ServiceTotalAmount, 0) AS 'ItemAmount',"
                          + " ISNULL(SUM(A.NetAmount),0) AS 'NetAmount' FROM BookingOthersHistory AS A"
                          + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                          + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                          + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType Order by C.ConfigName";
                    }
                    else
                    {
                        squery = " SELECT  A.CategoryName, A.ServiceName, A.BookingType, SUM(A.NoOfItems) AS 'NoOfItems', SUM(A.ItemAmount) AS 'ItemAmount', SUM(A.NetAmount) AS 'NetAmount' "
                        + " from (SELECT C.ConfigName AS 'CategoryName', B.ServiceName, CASE WHEN A.BookingType = 'A' THEN 'Independent' ELSE 'Along With Boating' "
                        + "  END AS 'BookingType', ISNULL(SUM(A.NoOfItems), 2) AS 'NoOfItems', ISNULL(B.ServiceTotalAmount, 0) AS 'ItemAmount', ISNULL(SUM(A.NetAmount), 0) AS 'NetAmount' FROM BookingOthers AS A INNER JOIN OtherServices AS "
                        + "  B ON A.ServiceId = B.ServiceId INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                        + "  GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType "
                        + "  UNION ALL  "
                        + "  SELECT C.ConfigName AS 'CategoryName', B.ServiceName, CASE WHEN A.BookingType = 'A' THEN 'Independent' ELSE 'Along With Boating' "
                        + "  END AS 'BookingType', ISNULL(SUM(A.NoOfItems), 2) AS 'NoOfItems',ISNULL(B.ServiceTotalAmount, 0) AS 'ItemAmount', ISNULL(SUM(A.NetAmount), 0) AS 'NetAmount' FROM BookingOthersHistory AS A INNER JOIN OtherServices AS "
                        + "  B ON A.ServiceId = B.ServiceId INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                        + "  GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType) as A  GROUP BY A.CategoryName, A.ServiceName, A.ItemAmount, A.BookingType  Order by A.CategoryName";
                    }

                    SqlCommand cmd = new SqlCommand(squery, con);
                    List<OthServiceCat> li = new List<OthServiceCat>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.NVarChar,2));

                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = Oth.BoatHouseId.Trim();
                    cmd.Parameters["@ServiceId"].Value = Oth.ServiceId.Trim();
                    cmd.Parameters["@Category"].Value = Oth.Category.Trim();
                    cmd.Parameters["@BookingType"].Value = Oth.BookingType.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OthServiceCat OthDtl = new OthServiceCat();

                            OthDtl.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OthDtl.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            OthDtl.BookingType = dt.Rows[i]["BookingType"].ToString();
                            OthDtl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                            OthDtl.ItemAmount = dt.Rows[i]["ItemAmount"].ToString();
                            OthDtl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                            li.Add(OthDtl);
                        }

                        OthServiceCatList BoatRate = new OthServiceCatList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        OthServiceCatString BoatRate = new OthServiceCatString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    OthServiceCatString Vehicle = new OthServiceCatString
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

        // Newly added by Brijin and Imran on 09-05-2022
        /// <summary>
        /// /Modified By Brijin On 19-05-2022
        /// </summary>
        /// <param name="Oth"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractAdditionalTkt")]
        public IHttpActionResult RptAbstractAdditionalTkt([FromBody] OthServiceCat Oth)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
                    && Oth.FromDate != "" && Oth.ToDate != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '34' AND A.BoatHouseId = @BoatHouseId " +
                        "AND B.BoatHouseId = @BoatHouseId AND B.OtherServiceType = 'AB' AND A.BStatus = 'B' ";

                    if (Oth.Category != "0")
                    {
                        condition += " AND C.ConfigID= @Category";
                    }
                    if (Oth.ServiceId != "0")
                    {
                        condition += " AND A.ServiceId = @ServiceId";
                    }

                    if (Oth.BookingType != "0")
                    {
                        condition += " AND A.BookingType = @BookingType";
                    }
                    else
                    {
                        condition += " AND A.BookingType IN ('A','B') ";
                    }

                    condition += " AND CAST(A.BookingDate AS DATE) BETWEEN @FromDate AND @ToDate";

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = "SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
                    + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthers AS A"
                    + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                    + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                    + " GROUP BY C.ConfigID, C.ConfigName";
                    }
                    if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = "SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
                    + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthersHistory AS A"
                    + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                    + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                    + " GROUP BY C.ConfigID, C.ConfigName";
                    }
                    else
                    {
                        squery = "SELECT A.CategoryId, A.CategoryName, SUM(A.TicketCount) AS 'TicketCount', SUM(A.Amount) AS 'Amount' FROM "
                            + " ( SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', SUM(A.NoOfItems) AS 'TicketCount', SUM(A.NetAmount) AS 'Amount' "
                            + " FROM BookingOthers AS A "
                            + "  INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId "
                            + "  INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                            + "  GROUP BY C.ConfigID, C.ConfigName "
                            + "  UNION ALL "
                            + "  SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', SUM(A.NoOfItems) AS 'TicketCount', SUM(A.NetAmount) AS 'Amount' " 
                            + "  FROM BookingOthersHistory AS A "
                            + "  INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId "
                            + "  INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                            + "  GROUP BY C.ConfigID, C.ConfigName) AS A GROUP BY A.CategoryId, A.CategoryName";
                    }
                    SqlCommand cmd = new SqlCommand(squery, con);
                    List<OthServiceCat> li = new List<OthServiceCat>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = Oth.BoatHouseId.Trim();
                    cmd.Parameters["@ServiceId"].Value = Oth.ServiceId.Trim();
                    cmd.Parameters["@Category"].Value = Oth.Category.Trim();
                    cmd.Parameters["@BookingType"].Value = Oth.BookingType.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OthServiceCat OthDtl = new OthServiceCat();

                            OthDtl.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OthDtl.TicketCount = dt.Rows[i]["TicketCount"].ToString();
                            OthDtl.Amount = dt.Rows[i]["Amount"].ToString();
                            li.Add(OthDtl);
                        }

                        OthServiceCatList BoatRate = new OthServiceCatList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        OthServiceCatString BoatRate = new OthServiceCatString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    OthServiceCatString Vehicle = new OthServiceCatString
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

        // Newly added by Brijin and Imran on 09-05-2022
        /// <summary>
        /// /Modified By Brijin On 19-05-2022
        /// </summary>
        /// <param name="Oth"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAdditionalTkt")]
        public IHttpActionResult RptAdditionalTkt([FromBody] OthServiceCat Oth)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
                    && Oth.FromDate != "" && Oth.ToDate != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '34' AND A.BoatHouseId = @BoatHouseId"
                        + " AND B.BoatHouseId = @BoatHouseId AND B.OtherServiceType = 'AB' "
                        + " AND A.BStatus = 'B' AND CAST(A.BookingDate AS DATE) BETWEEN @FromDate "
                        + " AND @ToDate";

                    if (Oth.Category != "0")
                    {
                        condition += " AND C.ConfigID= @Category";
                    }
                    if (Oth.ServiceId != "0")
                    {
                        condition += " AND A.ServiceId = @ServiceId";
                    }

                    if (Oth.BookingType != "0")
                    {
                        condition += " AND A.BookingType =@BookingType";
                    }
                    else
                    {
                        condition += " AND A.BookingType IN ('A','B') ";
                    }

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = "SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                        + "case when A.BookingType ='A' then 'Independent' else 'Along With Boating' end as BookingType,"
                        + "A.ChargePerItem,A.NoOfItems,ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                        + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                        + " A.NetAmount FROM BookingOthers AS A"
                        + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                        + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID" + condition + ""
                        + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                        + " A.NoOfItems Order by C.ConfigID";
                    }
                    else if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                       && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = "SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                        + "case when A.BookingType ='A' then 'Independent' else 'Along With Boating' end as BookingType,"
                        + "A.ChargePerItem,A.NoOfItems,ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                        + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                        + " A.NetAmount FROM BookingOthersHistory AS A"
                        + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                        + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID" + condition + ""
                        + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                        + " A.NoOfItems Order by C.ConfigID";
                    }
                    else
                    {
                        squery = "SELECT * FROM (SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                       + "case when A.BookingType ='A' then 'Independent' else 'Along With Boating' end as BookingType,"
                       + "A.ChargePerItem,A.NoOfItems,ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                       + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                       + " A.NetAmount FROM BookingOthers AS A"
                       + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                       + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID" + condition + ""
                       + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                       + " A.NoOfItems"
                       + "  UNION ALL "
                       + "  SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                       + "case when A.BookingType ='A' then 'Independent' else 'Along With Boating' end as BookingType,"
                       + "A.ChargePerItem,A.NoOfItems,ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                       + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                       + " A.NetAmount FROM BookingOthersHistory AS A"
                       + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                       + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID" + condition + ""
                       + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                       + " A.NoOfItems) AS A Order by A.CategoryId,A.BookingDate";
                    }

                    SqlCommand cmd = new SqlCommand(squery, con);
                    List<OthServiceCat> li = new List<OthServiceCat>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.NVarChar));

                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = Oth.BoatHouseId.Trim();
                    cmd.Parameters["@ServiceId"].Value = Oth.ServiceId.Trim();
                    cmd.Parameters["@Category"].Value = Oth.Category.Trim();
                    cmd.Parameters["@BookingType"].Value = Oth.BookingType.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OthServiceCat OthDtl = new OthServiceCat();

                            OthDtl.BookingId = dt.Rows[i]["BookingId"].ToString();
                            OthDtl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            OthDtl.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            OthDtl.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OthDtl.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            OthDtl.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            OthDtl.BookingType = dt.Rows[i]["BookingType"].ToString();
                            OthDtl.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                            OthDtl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                            OthDtl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                            OthDtl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                            OthDtl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                            li.Add(OthDtl);
                        }

                        OthServiceCatList BoatRate = new OthServiceCatList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        OthServiceCatString BoatRate = new OthServiceCatString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    OthServiceCatString Vehicle = new OthServiceCatString
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
        /// Modified By Jaya Suriya A, Added A.CreatedBy
        /// Modified Date:15-11-2021
        /// </summary>
        /// <param name="OtherService"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractOtherServiceWiseCollection")]
        public IHttpActionResult OtherServiceAbs([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                                + " @BookingDate "
                                + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                                + " AND A.BoatHouseId = @BoatHouseId AND "
                                + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus ='B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                                   + " @BookingDate "
                                   + " AND CAST(D.BookingDate AS DATE) BETWEEN @BookingDate AND "
                                   + " @BookingDate "
                                   + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus ='B' ";

                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }
                squery = " SELECT BookingDate, ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',  "
                       + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                       + "ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST',  "
                       + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                       + " FROM "
                       + " ( "
                       + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                       + " (ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) AS 'TaxAmount', A.NetAmount "
                       + "  FROM BookingOthers AS A "
                       + "  INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                       + "  INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                       + "  INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                       + " " + conditions + " "
                       + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                       + " UNION ALL "
                       + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, "
                       + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                       + "  (ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) AS 'TaxAmount', A.NetAmount "
                       + "  FROM BookingOthers AS A "
                       + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                       + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                       + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                       + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                       + " " + conditionType + " "
                       + "  GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                       + "  ) AS A "
                       + "  GROUP BY BookingDate,ServiceName ";



                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();
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
        /// Modified By Jaya Suriya A, Added A.CreatedBy
        /// Modified Date:15-11-2021 
        /// </summary>
        /// <param name="OtherService"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractAdditionalOtherServiceWiseCollection")]
        public IHttpActionResult OtherServiceAbsAdditional([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                                + " @BookingDate "
                                + " AND A.BookingType = 'A'  AND C.OtherServiceType = 'AB' "
                                + " AND A.BoatHouseId = @BoatHouseId AND "

                                + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '34' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                                   + " @BookingDate"
                                   + " AND CAST(D.BookingDate AS DATE) BETWEEN @BookingDate AND "
                                   + " @BookingDate"
                                   + " AND A.BookingType = 'B'  AND C.OtherServiceType = 'AB' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "

                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '34' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }

                squery = " SELECT BookingDate,ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                        + " ISNULL(CAST((SUM(TaxAmount)/2) AS DECIMAL(18,2)),0) AS 'CGST', "
                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                        + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                        + " FROM "
                        + " ( "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                        + " (SUM(A.CGSTTaxAmount)+SUM(A.SGSTTaxAmount)) AS 'TaxAmount', A.NetAmount "
                        + " FROM BookingOthers AS A "
                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                        + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                        + " UNION ALL "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, "
                        + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, (SUM(A.CGSTTaxAmount)+SUM(A.SGSTTaxAmount)) AS 'TaxAmount', A.NetAmount "
                        + " FROM BookingOthers AS A "
                        + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId  "
                        + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                        + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                        + " " + conditionType + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount "
                        + " ) AS A "
                        + " GROUP BY BookingDate,ServiceName ";


                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();
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


        //*************************Suba*************************************
        //*************************Pretheka*************************************

        /// <summary>
        /// Modified By Imran, Added A.CreatedBy
        /// Modified Date:16-11-2021
        /// </summary>
        /// <param name="OtherService"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractAdditionalOtherServiceWiseCollection_Test")]
        public IHttpActionResult OtherServiceAbsAdditional_Test([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '34' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND CAST(D.BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND A.BookingType = 'B' AND C.OtherServiceType = 'AB' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND "
                + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '34' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }

                squery = " SELECT BookingDate,ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                        + " ISNULL(CAST((SUM(TaxAmount)/2) AS DECIMAL(18,2)),0) AS 'CGST', "
                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                        + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',A.ConfigName,A.BoatHouseId,A.ConfigID "
                        + " FROM "
                        + " ( "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                        + " (SUM(A.CGSTTaxAmount)+SUM(A.SGSTTaxAmount)) AS 'TaxAmount', A.NetAmount,D.ConfigName,A.BoatHouseId,D.ConfigID "
                        + " FROM BookingOthers AS A "
                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                        + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount,D.ConfigName,A.BoatHouseId ,D.ConfigID "
                        + " UNION ALL "
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, "
                        + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, (SUM(A.CGSTTaxAmount)+SUM(A.SGSTTaxAmount)) AS 'TaxAmount', A.NetAmount,E.ConfigName,A.BoatHouseId,E.ConfigID "
                        + " FROM BookingOthers AS A "
                        + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                        + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                        + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                        + " " + conditionType + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount,E.ConfigName,A.BoatHouseId,E.ConfigID "
                        + " ) AS A "
                        + " GROUP BY BookingDate,ServiceName,ConfigName,BoatHouseId,ConfigID ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();
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
        /// Modified By Imran, Added A.CreatedBy
        /// Modified Date:16-11-2021
        /// </summary>
        /// <param name="OtherService"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractAdditionalOtherServiceWiseCollectionMain_Test")]
        public IHttpActionResult OtherServiceAbsAdditionalMain_Test([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND A.BookingType = 'A' AND C.OtherServiceType = 'AB' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '34' AND E.TypeID = '20' AND A.BStatus = 'B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND CAST(D.BookingDate AS DATE) BETWEEN BookingDate AND "
                + " BookingDate "
                + " AND A.BookingType = 'B' AND C.OtherServiceType = 'AB' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND "
                + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '34' AND F.TypeID = '20' AND A.BStatus = 'B' ";

                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }

                squery = "SELECT  CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate',D.ConfigName,A.BoatHouseId,D.ConfigID "
                         + " FROM BookingOthers AS A  INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                         + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID"
                         + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                         + " " + conditions + " "
                         + " GROUP BY CAST(A.BookingDate AS DATE), "
                         + " D.ConfigName,A.BoatHouseId ,D.ConfigID ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();
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
        /// Modified By Imran, Added A.CreatedBy
        /// Modified Date:16-11-2021
        /// </summary>
        /// <param name="OtherService"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractOtherServiceWiseCollectionMain_Test")]
        public IHttpActionResult OtherServiceAbs_Test([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus ='B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND CAST(D.BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND "
                + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus ='B' ";


                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }
                squery = " SELECT BookingDate, A.ConfigName ,A.BoatHouseId,A.ConfigID FROM "
                        + " ("
                        + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', D.ConfigName, A.BoatHouseId, D.ConfigID"
                        + " FROM BookingOthers AS A"
                        + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId"
                        + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID"
                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), D.ConfigName, A.BoatHouseId, D.ConfigID "
                        + " UNION ALL"
                        + " SELECT CONVERT(NVARCHAR(20),"
                        + " CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', E.ConfigName, A.BoatHouseId, E.ConfigID "
                        + " FROM BookingOthers AS A"
                        + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                        + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                        + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                        + " " + conditionType + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), E.ConfigName, "
                        + " A.BoatHouseId, E.ConfigID "
                        + " ) AS A GROUP BY BookingDate,ConfigName,BoatHouseId,ConfigID ORDER BY ConfigName ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();
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
        /// Modified By Imran, Added A.CreatedBy
        /// Modified Date:16-11-2021
        /// </summary>
        /// <param name="OtherService"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractOtherServiceWiseCollection_Test")]
        public IHttpActionResult OtherServiceAbs_Test1([FromBody] BoatingReport OtherService)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND A.BookingType = 'I' AND C.OtherServiceType = 'OS' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND D.TypeID = '27' AND E.TypeID = '20' AND A.BStatus ='B' ";

                string conditionType = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND CAST(D.BookingDate AS DATE) BETWEEN @BookingDate AND "
                + " @BookingDate "
                + " AND A.BookingType = 'B' AND C.OtherServiceType = 'OS' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND "
                + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '27' AND F.TypeID = '20' AND A.BStatus ='B' ";

                if (OtherService.BoatTypeId != "0")
                {
                    conditions += " AND C.Category = @BoatTypeId";
                    conditionType += " AND C.Category = @BoatTypeId";
                }
                if (OtherService.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category";
                    conditionType += " AND A.ServiceId = @Category";
                }
                if (OtherService.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                    conditionType += " AND A.PaymentType = @PaymentType";
                }
                if (OtherService.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                    conditionType += " AND A.CreatedBy = @CreatedBy";
                }
                squery = " SELECT BookingDate, ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount', "
                + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', "
                + "ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',A.ConfigName,A.BoatHouseId,A.ConfigID "
                + " FROM "
                + " ( "
                + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                + " (ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) AS 'TaxAmount', A.NetAmount,D.ConfigName,A.BoatHouseId,D.ConfigID "
                + " FROM BookingOthers AS A "
                + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                + " INNER JOIN ConfigurationMaster AS D ON C.Category = D.ConfigID "
                + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                + " " + conditions + " "
                + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount,D.ConfigName,A.BoatHouseId,D.ConfigID "
                + " UNION ALL "
                + " SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS 'BookingDate', C.ServiceName, "
                + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                + " (ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) AS 'TaxAmount', A.NetAmount ,E.ConfigName,A.BoatHouseId,E.ConfigID "
                + " FROM BookingOthers AS A "
                + " INNER JOIN BookingHdr AS D ON D.BookingId = A.BookingId "
                + " INNER JOIN OtherServices AS C ON A.ServiceId = C.ServiceId "
                + " INNER JOIN ConfigurationMaster AS E ON C.Category = E.ConfigID "
                + " INNER JOIN ConfigurationMaster AS F ON A.PaymentType = F.ConfigID "
                + " " + conditionType + " "
                + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, A.ServiceId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount,E.ConfigName,A.BoatHouseId,E.ConfigID "
                + " ) AS A "
                + " GROUP BY BookingDate,ServiceName,ConfigName,BoatHouseId,ConfigID ORDER BY ConfigName ";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(OtherService.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = OtherService.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = OtherService.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = OtherService.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = OtherService.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = OtherService.CreatedBy.Trim();
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptAdditionalTktSummary")]
        //public IHttpActionResult RptAdditionalTktSummary([FromBody] OthServiceCat Oth)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
        //            && Oth.FromDate != "" && Oth.ToDate != "")
        //        {

        //            string condition = string.Empty;

        //            condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '34' AND A.BoatHouseId =" + Oth.BoatHouseId.Trim() + ""
        //                + " AND B.BoatHouseId =" + Oth.BoatHouseId.Trim() + " AND B.OtherServiceType = 'AB' AND A.BStatus = 'B' "
        //                + " AND CAST(A.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) + "') "
        //                + " AND ('" + DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) + "')";

        //            if (Oth.Category != "0")
        //            {
        //                condition += " AND C.ConfigID= '" + Oth.Category.Trim() + "'";
        //            }
        //            if (Oth.ServiceId != "0")
        //            {
        //                condition += " AND A.ServiceId = '" + Oth.ServiceId.Trim() + "'";
        //            }

        //            if (Oth.BookingType != "0")
        //            {
        //                condition += " AND A.BookingType ='" + Oth.BookingType.Trim() + "'";
        //            }
        //            else
        //            {
        //                condition += " AND A.BookingType IN ('A','B') ";
        //            }

        //            squery = " SELECT C.ConfigName AS 'CategoryName', B.ServiceName, CASE WHEN A.BookingType ='A' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
        //                    + " ISNULL(SUM(A.NoOfItems),2) AS 'NoOfItems', ISNULL(B.ServiceTotalAmount, 0) AS 'ItemAmount',"
        //                   + " ISNULL(SUM(A.NetAmount),0) AS 'NetAmount' FROM BookingOthers AS A"
        //                   + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
        //                   + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
        //                   + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType Order by C.ConfigName";

        //            SqlCommand cmd = new SqlCommand(squery, con);
        //            List<OthServiceCat> li = new List<OthServiceCat>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);

        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    OthServiceCat OthDtl = new OthServiceCat();

        //                    OthDtl.CategoryName = dt.Rows[i]["CategoryName"].ToString();
        //                    OthDtl.ServiceName = dt.Rows[i]["ServiceName"].ToString();
        //                    OthDtl.BookingType = dt.Rows[i]["BookingType"].ToString();
        //                    OthDtl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
        //                    OthDtl.ItemAmount = dt.Rows[i]["ItemAmount"].ToString();
        //                    OthDtl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
        //                    li.Add(OthDtl);
        //                }

        //                OthServiceCatList BoatRate = new OthServiceCatList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }
        //            else
        //            {
        //                OthServiceCatString BoatRate = new OthServiceCatString
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            OthServiceCatString Vehicle = new OthServiceCatString
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
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return Ok(ConfRes);
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptAbstractAdditionalTkt")]
        //public IHttpActionResult RptAbstractAdditionalTkt([FromBody] OthServiceCat Oth)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
        //            && Oth.FromDate != "" && Oth.ToDate != "")
        //        {

        //            string condition = string.Empty;

        //            condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '34' AND A.BoatHouseId ="
        //                + Oth.BoatHouseId.Trim() + " AND B.BoatHouseId =" + Oth.BoatHouseId.Trim() + " AND B.OtherServiceType = 'AB' AND A.BStatus = 'B' ";

        //            if (Oth.Category != "0")
        //            {
        //                condition += " AND C.ConfigID= '" + Oth.Category.Trim() + "'";
        //            }
        //            if (Oth.ServiceId != "0")
        //            {
        //                condition += " AND A.ServiceId = '" + Oth.ServiceId.Trim() + "'";
        //            }

        //            if (Oth.BookingType != "0")
        //            {
        //                condition += " AND A.BookingType ='" + Oth.BookingType.Trim() + "'";
        //            }
        //            else
        //            {
        //                condition += " AND A.BookingType IN ('A','B') ";
        //            }

        //            condition += " AND CAST(A.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) + "')";


        //            squery = "SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
        //            + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthers AS A"
        //            + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
        //            + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
        //            + " GROUP BY C.ConfigID, C.ConfigName";

        //            SqlCommand cmd = new SqlCommand(squery, con);
        //            List<OthServiceCat> li = new List<OthServiceCat>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    OthServiceCat OthDtl = new OthServiceCat();

        //                    OthDtl.CategoryName = dt.Rows[i]["CategoryName"].ToString();
        //                    OthDtl.TicketCount = dt.Rows[i]["TicketCount"].ToString();
        //                    OthDtl.Amount = dt.Rows[i]["Amount"].ToString();
        //                    li.Add(OthDtl);
        //                }

        //                OthServiceCatList BoatRate = new OthServiceCatList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }
        //            else
        //            {
        //                OthServiceCatString BoatRate = new OthServiceCatString
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            OthServiceCatString Vehicle = new OthServiceCatString
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
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return Ok(ConfRes);
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptAdditionalTkt")]
        //public IHttpActionResult RptAdditionalTkt([FromBody] OthServiceCat Oth)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
        //            && Oth.FromDate != "" && Oth.ToDate != "")
        //        {

        //            string condition = string.Empty;

        //            condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '34' AND A.BoatHouseId =" + Oth.BoatHouseId.Trim() + ""
        //                + " AND B.BoatHouseId =" + Oth.BoatHouseId.Trim() + "  AND B.OtherServiceType = 'AB' "
        //                + "  AND A.BStatus = 'B' AND CAST(A.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) + "') "
        //                + " AND ('" + DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) + "')";

        //            if (Oth.Category != "0")
        //            {
        //                condition += " AND C.ConfigID= '" + Oth.Category.Trim() + "'";
        //            }
        //            if (Oth.ServiceId != "0")
        //            {
        //                condition += " AND A.ServiceId = '" + Oth.ServiceId.Trim() + "'";
        //            }

        //            if (Oth.BookingType != "0")
        //            {
        //                condition += " AND A.BookingType ='" + Oth.BookingType.Trim() + "'";
        //            }
        //            else
        //            {
        //                condition += " AND A.BookingType IN ('A','B') ";
        //            }

        //            squery = "SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
        //                + "case when A.BookingType ='A' then 'Independent' else 'Along With Boating' end as BookingType,"
        //                + "A.ChargePerItem,A.NoOfItems,ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
        //                + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) as 'TaxAmount',"
        //                + " A.NetAmount FROM BookingOthers AS A"
        //                + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
        //                + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID" + condition + ""
        //                + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
        //                + " A.NoOfItems Order by C.ConfigID";


        //            SqlCommand cmd = new SqlCommand(squery, con);
        //            List<OthServiceCat> li = new List<OthServiceCat>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);

        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    OthServiceCat OthDtl = new OthServiceCat();

        //                    OthDtl.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    OthDtl.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    OthDtl.CategoryId = dt.Rows[i]["CategoryId"].ToString();
        //                    OthDtl.CategoryName = dt.Rows[i]["CategoryName"].ToString();
        //                    OthDtl.ServiceId = dt.Rows[i]["ServiceId"].ToString();
        //                    OthDtl.ServiceName = dt.Rows[i]["ServiceName"].ToString();
        //                    OthDtl.BookingType = dt.Rows[i]["BookingType"].ToString();
        //                    OthDtl.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
        //                    OthDtl.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
        //                    OthDtl.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
        //                    OthDtl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
        //                    OthDtl.NetAmount = dt.Rows[i]["NetAmount"].ToString();
        //                    li.Add(OthDtl);
        //                }

        //                OthServiceCatList BoatRate = new OthServiceCatList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }
        //            else
        //            {
        //                OthServiceCatString BoatRate = new OthServiceCatString
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            OthServiceCatString Vehicle = new OthServiceCatString
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
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //        return Ok(ConfRes);
        //    }
        //}
    }
}

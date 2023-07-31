using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using TTDCAPIv1.Models;
using TTDCAPIv1.ReportModel;

namespace TTDCAPIv1.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]

    public class RestaurantController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_BoatHouse"].ConnectionString);
        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

        /***********************************Boat Type Master**************************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FoodCategoryMstr/BHId")]
        public IHttpActionResult getBTMstrId([FromBody] FoodCategory bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<FoodCategory> li = new List<FoodCategory>();

                    string sQuery = string.Empty;

                    sQuery = " SELECT CategoryId, CategoryName, ActiveStatus FROM FoodCategory "
                           + " WHERE ActiveStatus IN ('A','D') AND BoatHouseId = @BoatHouseId ";

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
                            FoodCategory BoatTypeMaster = new FoodCategory();

                            BoatTypeMaster.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            BoatTypeMaster.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            BoatTypeMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                            li.Add(BoatTypeMaster);
                        }

                        FoodCategoryList BoatType = new FoodCategoryList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatType);
                    }

                    else
                    {
                        FoodCategoryString BoatType = new FoodCategoryString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatType);
                    }
                }
                else
                {
                    FoodCategoryString Vehicle = new FoodCategoryString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FoodCategoryString ConfRes = new FoodCategoryString
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
        [Route("FoodCategoryMstr")]
        public IHttpActionResult mstrBoatTypeMaster([FromBody] FoodCategory InsBoatTypeMaster)
        {
            try
            {
                if (InsBoatTypeMaster.QueryType != ""
                    && InsBoatTypeMaster.CategoryName != "" && InsBoatTypeMaster.BoatHouseId != ""
                    && InsBoatTypeMaster.BoatHouseName != "" && InsBoatTypeMaster.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrFoodCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBoatTypeMaster.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CategoryId", InsBoatTypeMaster.CategoryId.ToString());
                    cmd.Parameters.AddWithValue("@CategoryName", InsBoatTypeMaster.CategoryName.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBoatTypeMaster.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBoatTypeMaster.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsBoatTypeMaster.CreatedBy.Trim());
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
                        FoodCategoryString Boattype = new FoodCategoryString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Boattype);
                    }
                    else
                    {
                        FoodCategoryString Boattype = new FoodCategoryString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Boattype);
                    }
                }
                else
                {
                    FoodCategoryString Boattype = new FoodCategoryString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Boattype);
                }
            }
            catch (Exception ex)
            {
                FoodCategoryString ConfRes = new FoodCategoryString
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

        /***********************************Food Item Master**************************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FoodItemMstr/BHId")]
        public IHttpActionResult FoodItemMstrBHId([FromBody] FoodItemMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    string sQuery = string.Empty;

                    sQuery = " SELECT A.StockEntryMaintenance, A.CategoryId, B.CategoryName, A.ServiceId, A.ServiceName, "
                        + " A.ShortName, A.ServiceTotalAmount, A.BoatHouseId, A.BoatHouseName, A.ChargePerItem, "
                        + " A.ChargePerItemTax, A.TaxId, A.ActiveStatus, "
                        + " CASE WHEN A.TaxID > 0 THEN dbo.GetTaxIdDetails('Restaurants', '5', '', A.TaxID,@BoatHouseId) ELSE 'Nil Tax' END AS 'TaxName' "
                        + " FROM FoodItemMaster AS A "
                        + " INNER JOIN FoodCategory AS B ON A.CategoryId = B.CategoryId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.ActiveStatus IN ('A','D')  AND A.BoatHouseId = @BoatHouseId ";

                    List<FoodItemMaster> li = new List<FoodItemMaster>();
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
                            FoodItemMaster OtherServiceMstr = new FoodItemMaster();

                            OtherServiceMstr.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            OtherServiceMstr.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OtherServiceMstr.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            OtherServiceMstr.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            OtherServiceMstr.ShortName = dt.Rows[i]["ShortName"].ToString();
                            OtherServiceMstr.ServiceTotalAmount = dt.Rows[i]["ServiceTotalAmount"].ToString();
                            OtherServiceMstr.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                            OtherServiceMstr.ChargePerItemTax = dt.Rows[i]["ChargePerItemTax"].ToString();
                            OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            OtherServiceMstr.TaxID = dt.Rows[i]["TaxId"].ToString();
                            OtherServiceMstr.TaxName = dt.Rows[i]["TaxName"].ToString();
                            OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OtherServiceMstr.StockEntryMaintenance = dt.Rows[i]["StockEntryMaintenance"].ToString();

                            li.Add(OtherServiceMstr);

                        }

                        FoodItemMasterList OtherService = new FoodItemMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(OtherService);
                    }

                    else
                    {
                        FoodItemMasterString OtherService = new FoodItemMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(OtherService);
                    }
                }
                else
                {
                    FoodItemMasterString Vehicle = new FoodItemMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FoodItemMasterString ConfRes = new FoodItemMasterString
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
        /// Create By : Pretheka
        /// Created Date : 2022-04-20
        /// Version : V2
        /// 
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.     
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FoodItemMstr/BHIdV2")]
        public IHttpActionResult FoodItemMstrBHIdV2([FromBody] FoodItemMasterV2 bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId.Trim() != null && bHMstr.BoatHouseId != "" && bHMstr.CountStart.Trim() != null
                    && bHMstr.CountStart.Trim() != "" && bHMstr.CategoryId.Trim() != null && bHMstr.CategoryId != ""
                    && bHMstr.ServiceId.Trim() != null && bHMstr.ServiceId.Trim() != "")
                {
                    string sQuery = string.Empty;
                    string sCondition = string.Empty;

                    if (bHMstr.CategoryId != "0")
                    {
                        sCondition += " AND A.CategoryId = @CategoryId ";
                    }

                    if (bHMstr.ServiceId != "0")
                    {
                        sCondition += " AND A.ServiceId= @ServiceId ";
                    }

                    sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY C.CategoryId) 'RowNumber', * FROM ( "
                        + " SELECT A.StockEntryMaintenance, A.CategoryId, B.CategoryName, A.ServiceId, A.ServiceName, "
                        + " A.ShortName, A.ServiceTotalAmount, A.BoatHouseId, A.BoatHouseName, A.ChargePerItem, "
                        + " A.ChargePerItemTax, A.ActiveStatus, A.TaxId, "
                        + " CASE WHEN A.TaxID > 0 THEN dbo.GetTaxIdDetails('Restaurants', '5', '', A.TaxID,@BoatHouseId) ELSE 'Nil Tax' END AS 'TaxName' "
                        + " FROM FoodItemMaster AS A "
                        + " INNER JOIN FoodCategory AS B ON A.CategoryId = B.CategoryId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.ActiveStatus IN ('A','D')  AND A.BoatHouseId = @BoatHouseId " + sCondition.ToString().Trim() + ""
                        + " ) AS C) AS D where D.RowNumber BETWEEN @CountStart AND @CountEnd ORDER BY D.RowNumber ASC";

                    List<FoodItemMasterV2> li = new List<FoodItemMasterV2>();
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@CategoryId"].Value = bHMstr.CategoryId.Trim();
                    cmd.Parameters["@ServiceId"].Value = bHMstr.ServiceId.Trim();
                    cmd.Parameters["@CountStart"].Value = bHMstr.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(bHMstr.CountStart.Trim()) + 9;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FoodItemMasterV2 OtherServiceMstr = new FoodItemMasterV2();

                            OtherServiceMstr.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            OtherServiceMstr.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OtherServiceMstr.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            OtherServiceMstr.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            OtherServiceMstr.ShortName = dt.Rows[i]["ShortName"].ToString();
                            OtherServiceMstr.ServiceTotalAmount = dt.Rows[i]["ServiceTotalAmount"].ToString();
                            OtherServiceMstr.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                            OtherServiceMstr.ChargePerItemTax = dt.Rows[i]["ChargePerItemTax"].ToString();
                            OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            OtherServiceMstr.TaxID = dt.Rows[i]["TaxId"].ToString();
                            OtherServiceMstr.TaxName = dt.Rows[i]["TaxName"].ToString();
                            OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OtherServiceMstr.StockEntryMaintenance = dt.Rows[i]["StockEntryMaintenance"].ToString();
                            OtherServiceMstr.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(OtherServiceMstr);

                        }

                        FoodItemMasterListV2 OtherService = new FoodItemMasterListV2
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(OtherService);
                    }

                    else
                    {
                        FoodItemMasterStringV2 OtherService = new FoodItemMasterStringV2
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(OtherService);
                    }
                }
                else
                {
                    FoodItemMasterStringV2 Vehicle = new FoodItemMasterStringV2
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }

            }
            catch (Exception ex)
            {
                FoodItemMasterStringV2 ConfRes = new FoodItemMasterStringV2
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
        [Route("FoodItemMaster")]
        public IHttpActionResult MstrFoodItemMaster([FromBody] FoodItemMaster InsMstrFdItem)
        {
            try
            {
                if (InsMstrFdItem.QueryType != "" && InsMstrFdItem.CategoryId != ""
                    && InsMstrFdItem.ServiceName != "" && InsMstrFdItem.BoatHouseId != ""
                    && InsMstrFdItem.BoatHouseName != "" && InsMstrFdItem.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrFoodItemMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsMstrFdItem.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ServiceId", InsMstrFdItem.ServiceId.ToString());
                    cmd.Parameters.AddWithValue("@CategoryId", InsMstrFdItem.CategoryId.ToString());
                    cmd.Parameters.AddWithValue("@ServiceName", InsMstrFdItem.ServiceName.ToString());
                    cmd.Parameters.AddWithValue("@ShortName", InsMstrFdItem.ShortName.ToString());
                    cmd.Parameters.AddWithValue("@ServiceTotalAmount", InsMstrFdItem.ServiceTotalAmount.ToString());
                    cmd.Parameters.AddWithValue("@ChargePerItem", InsMstrFdItem.ChargePerItem.ToString());
                    cmd.Parameters.AddWithValue("@ChargePerItemTax", InsMstrFdItem.ChargePerItemTax.ToString());
                    cmd.Parameters.AddWithValue("@TaxId", InsMstrFdItem.TaxID.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsMstrFdItem.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsMstrFdItem.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@StockEntryMaintenance", InsMstrFdItem.StockEntryMaintenance.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsMstrFdItem.CreatedBy.Trim());

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

        /***********************************Booking Restaurant Services**************************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FoodCategory/BHId")]
        public IHttpActionResult getFoodCatId([FromBody] FoodCategory bHMstr)
        {
            try
            {
                string sQuery = string.Empty;

                if (bHMstr.BoatHouseId != "")
                {
                    List<FoodCategory> li = new List<FoodCategory>();

                    sQuery = " SELECT CategoryId, CategoryName, ActiveStatus FROM FoodCategory "
                           + " WHERE ActiveStatus IN ('A') AND BoatHouseId = @BoatHouseId "
                           + " AND CategoryId IN (SELECT CategoryId FROM FoodItemMaster) ";

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
                            FoodCategory BoatTypeMaster = new FoodCategory();

                            BoatTypeMaster.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            BoatTypeMaster.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            BoatTypeMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                            li.Add(BoatTypeMaster);
                        }

                        FoodCategoryList BoatType = new FoodCategoryList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatType);
                    }

                    else
                    {
                        FoodCategoryString BoatType = new FoodCategoryString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatType);
                    }
                }
                else
                {
                    FoodCategoryString Vehicle = new FoodCategoryString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FoodCategoryString ConfRes = new FoodCategoryString
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
        [Route("BookingRestaurantServices")]
        public IHttpActionResult BookingRestaurantServices([FromBody] BookingOtherServices InsMatPur)
        {
            try
            {
                if (InsMatPur.QueryType != "" && InsMatPur.BookingId != "" && InsMatPur.ServiceId != ""
                    && InsMatPur.BookingType != "" && InsMatPur.BoatHouseId != "" && InsMatPur.BoatHouseName != ""
                    && InsMatPur.ChargePerItem != "" && InsMatPur.NoOfItems != "" && InsMatPur.TaxDetails != ""
                    && InsMatPur.NetAmount != "" && InsMatPur.CreatedBy != "" && InsMatPur.BookingMedia != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("BookingRestaurantServices", con);
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
                    cmd.Parameters.AddWithValue("@TaxDetails", InsMatPur.TaxDetails.Trim());
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

        [HttpPost]
        [AllowAnonymous]
        [Route("OfflineBookingRestaurantServices")]
        public IHttpActionResult OfflineBookingRestaurantServices([FromBody] BookingOtherServices InsMatPur)
        {
            try
            {
                if (InsMatPur.QueryType != "" && InsMatPur.BookingId != "" && InsMatPur.ServiceId != ""
                    && InsMatPur.BookingType != "" && InsMatPur.BoatHouseId != "" && InsMatPur.BoatHouseName != ""
                    && InsMatPur.ChargePerItem != "" && InsMatPur.NoOfItems != ""
                    && InsMatPur.TaxDetails != "" && InsMatPur.NetAmount != ""
                    && InsMatPur.CreatedBy != "" && InsMatPur.BookingMedia != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("OfflineBookingRestaurantServices", con);
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
                    cmd.Parameters.AddWithValue("@TaxDetails", InsMatPur.TaxDetails.Trim());
                    cmd.Parameters.AddWithValue("@NetAmount", InsMatPur.NetAmount.Trim());
                    cmd.Parameters.AddWithValue("@BookingMedia", InsMatPur.BookingMedia.Trim());
                    cmd.Parameters.AddWithValue("@CustomerMobileNo", "");
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


        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        ///  Modified By : Silambarasu
        /// Modified Date : 27-April-2023
        /// Description : Parameter not working so reverted
        /// </summary>
        /// <param name="InsItemMaster"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantSvcCatDet")]
        public IHttpActionResult RestaurantSvcCatDet([FromBody] OtherServicesDetails InsItemMaster)
        {
            try
            {
                string sQuery = string.Empty;

                List<OtherServicesDetails> li = new List<OtherServicesDetails>();
                if (InsItemMaster.Category != null && InsItemMaster.BoatHouseId != null)
                {
                    if (InsItemMaster.Category == "0" || InsItemMaster.Category == "")
                    {
                        sQuery = " SELECT A.CategoryId, B.CategoryName, A.ServiceId, A.ServiceName, A.ShortName, A.BoatHouseId, A.BoatHouseName, "
                        + " A.ServiceTotalAmount,Cast(A.AvailableQty AS Int) as 'AvailableQty',A.StockEntryMaintenance, A.ChargePerItem, A.ChargePerItemTax, "
                        + " A.TaxId, "
                        + " dbo.GetTaxIdDetails('Restaurants', '5', '', A.TaxId,@BoatHouseId) AS 'TaxName', A.ActiveStatus, A.Createdby FROM FoodItemMaster AS A "
                        + " INNER JOIN FoodCategory AS B ON A.CategoryId = B.CategoryId AND A.BoatHouseId = B.BoatHouseId  "
                        + " WHERE A.ActiveStatus = 'A' AND A.BoatHouseId = @BoatHouseId ";

                    }
                    else
                    {
                        sQuery = " SELECT A.CategoryId, B.CategoryName, A.ServiceId, A.ServiceName, A.ShortName, A.BoatHouseId, A.BoatHouseName, "
                        + " A.ServiceTotalAmount,Cast(A.AvailableQty AS Int) as 'AvailableQty',A.StockEntryMaintenance, A.ChargePerItem, A.ChargePerItemTax, "
                        + " A.TaxId, "
                        + " dbo.GetTaxIdDetails('Restaurants', '5', '', A.TaxId,@BoatHouseId) AS 'TaxName', A.ActiveStatus, A.Createdby FROM FoodItemMaster AS A "
                        + " INNER JOIN FoodCategory AS B ON A.CategoryId = B.CategoryId AND A.BoatHouseId = B.BoatHouseId  "
                        + " WHERE A.ActiveStatus = 'A' AND A.BoatHouseId = @BoatHouseId "
                        + " AND A.CategoryId IN ( @Category )";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BoatHouseId"].Value = InsItemMaster.BoatHouseId.Trim();
                    cmd.Parameters["@Category"].Value = InsItemMaster.Category.Trim();


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OtherServicesDetails OtherServiceMstr = new OtherServicesDetails();

                            OtherServiceMstr.Category = dt.Rows[i]["CategoryId"].ToString();
                            OtherServiceMstr.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OtherServiceMstr.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            OtherServiceMstr.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            OtherServiceMstr.ShortName = dt.Rows[i]["ShortName"].ToString();

                            OtherServiceMstr.ServiceTotalAmount = dt.Rows[i]["ServiceTotalAmount"].ToString();
                            OtherServiceMstr.AvailableQty = dt.Rows[i]["AvailableQty"].ToString();
                            OtherServiceMstr.StockEntryMaintenance = dt.Rows[i]["StockEntryMaintenance"].ToString();
                            OtherServiceMstr.AdultCharge = dt.Rows[i]["ChargePerItem"].ToString();
                            OtherServiceMstr.ChargePerItemTax = dt.Rows[i]["ChargePerItemTax"].ToString();


                            OtherServiceMstr.TaxID = dt.Rows[i]["TaxId"].ToString();
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
                else
                {
                    OtherServicesString OtherService = new OtherServicesString
                    {
                        Response = "Must Pass All Parameters.",
                        StatusCode = 0
                    };
                    return Ok(OtherService);
                }
            }
            catch (Exception ex)
            {
                OtherServicesString ConfRes = new OtherServicesString
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
        /// 
        /// </summary>
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantBookedList")]
        public IHttpActionResult GetOtherBookedList([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "RestaurantBookedDetails");
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
        /// Created By : Subalakshmi
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified By : Subalakshmi
        /// Modified Date : 2022-04-25
        /// Version : V2
        /// </summary>
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantBookedListV2")]
        public IHttpActionResult GetOtherBookedListV2([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                int endcount = Int32.Parse(Otl.CountStart.Trim()) + 9;

                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "RestaurantBookedDetailsV2");
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
        /// Created By : Subalakshmi
        /// Created Date : 2022-04-18
        /// Version : V2
        /// Modified By : Subalakshmi
        /// Modified Date : 2022-04-25
        /// Version : V2
        /// </summary>      
        /// <param name="Otl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantBookedListPinV2")]
        public IHttpActionResult GetOtherBookedListV2Search([FromBody] OtherTicketBookedDtl Otl)
        {
            try
            {
                int endcount = Int32.Parse(Otl.CountStart.Trim()) + 9;
                List<OtherTicketBookedDtl> li = new List<OtherTicketBookedDtl>();

                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "RestaurantBookedDetailsPinV2");
                cmd.Parameters.AddWithValue("@BoatHouseId", Otl.BoatHouseId.Trim());
                cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Otl.FromDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Otl.ToDate.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@UserId", Otl.UserId.Trim());
                cmd.Parameters.AddWithValue("@Input1", Otl.Input1.Trim());
                cmd.Parameters.AddWithValue("@Input2", Otl.CountStart.ToString());
                cmd.Parameters.AddWithValue("@Input3", endcount.ToString());

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
        /// Modified by Subalakshmi,Imran on 2022-05-20
        /// </summary>
        /// <param name="Ot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantTicket")]
        public IHttpActionResult GetOtherTicket([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "RestaurantPrintTicket");
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
                    return GetOtherTicketHistory(Ot);


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
        public IHttpActionResult GetOtherTicketHistory([FromBody] OtherTicket Ot)
        {
            try
            {
                List<OtherTicket> li = new List<OtherTicket>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "RestaurantPrintTicketHistory");
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


        //Add new End points on 2021-04-29
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantStockEntry")]
        public IHttpActionResult mstrRestaurantStockEntry([FromBody] RestStockEntry ResStockEntry)
        {
            try
            {
                if (ResStockEntry.QueryType != "" && ResStockEntry.ItemCategoryId != ""
                    && ResStockEntry.ItemCategory != "" && ResStockEntry.ItemNameId != ""
                    && ResStockEntry.ItemName != "" && ResStockEntry.BoatHouseId != ""
                    && ResStockEntry.BoatHouseName != "" && ResStockEntry.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrRestaurantStockEntry", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", ResStockEntry.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@StockId", ResStockEntry.StockId.ToString());
                    cmd.Parameters.AddWithValue("@ItemCategoryId", ResStockEntry.ItemCategoryId.ToString());
                    cmd.Parameters.AddWithValue("@ItemCategory", ResStockEntry.ItemCategory.Trim());
                    cmd.Parameters.AddWithValue("@ItemNameId", ResStockEntry.ItemNameId.Trim());
                    cmd.Parameters.AddWithValue("@ItemName", ResStockEntry.ItemName.ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Parse(ResStockEntry.Date.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@UOM", ResStockEntry.UOM.ToString());
                    cmd.Parameters.AddWithValue("@Quantity", ResStockEntry.Quantity.Trim());
                    cmd.Parameters.AddWithValue("@Reference", ResStockEntry.Reference.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", ResStockEntry.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", ResStockEntry.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", ResStockEntry.CreatedBy.Trim());
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
                        RestStockEntryRes stock = new RestStockEntryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(stock);
                    }
                    else
                    {
                        RestStockEntryRes stock = new RestStockEntryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(stock);
                    }
                }
                else
                {
                    RestStockEntryRes stock = new RestStockEntryRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(stock);
                }
            }
            catch (Exception ex)
            {
                RestStockEntryRes stockEntry = new RestStockEntryRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(stockEntry);
            }
        }

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <param name="BHFoodItem"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FoodItemName/BHId")]
        public IHttpActionResult GETFoodItemName([FromBody] FoodCategory BHFoodItem)
        {
            try
            {
                string sQuery = string.Empty;

                if (BHFoodItem.CategoryId != null && BHFoodItem.BoatHouseId != null && BHFoodItem.CategoryId != "" 
                    && BHFoodItem.BoatHouseId != "")
                {
                    
                    List<FoodCategory> li = new List<FoodCategory>();

                    sQuery = " SELECT A.ServiceId, A.ServiceName, B.CategoryId, B.CategoryName, A.BoatHouseId, " 
                        + " A.StockEntryMaintenance FROM FoodItemMaster AS A "
                        + " INNER JOIN FoodCategory AS B ON A.BoatHouseId = B.BoatHouseId AND A.CategoryId = B.CategoryId "
                        + " WHERE B.CategoryId = @CategoryId AND StockEntryMaintenance = 'Y' "
                        + " AND B.ActiveStatus = 'A' AND A.ActiveStatus = 'A' AND B.BoatHouseId = @BoatHouseId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHFoodItem.BoatHouseId.Trim();
                    cmd.Parameters["@CategoryId"].Value = BHFoodItem.CategoryId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FoodCategory BoatTypeMaster = new FoodCategory();

                            BoatTypeMaster.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            BoatTypeMaster.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            BoatTypeMaster.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            BoatTypeMaster.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            // BoatTypeMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                            li.Add(BoatTypeMaster);
                        }

                        FoodCategoryList BoatType = new FoodCategoryList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatType);
                    }

                    else
                    {
                        FoodCategoryString BoatType = new FoodCategoryString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatType);
                    }
                }
                else
                {
                    FoodCategoryString Vehicle = new FoodCategoryString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FoodCategoryString ConfRes = new FoodCategoryString
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
        /// Create By : Pretheka
        /// Created Date : 2022-04-20
        /// Version : V2 
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="BHFoodItem"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FoodItemName/BHIdV2")]
        public IHttpActionResult GETFoodItemNameV2([FromBody] FoodCategory BHFoodItem)
        {
            try
            {
                string sQuery = string.Empty;

                if (BHFoodItem.BoatHouseId != "")
                {
                    List<FoodCategory> li = new List<FoodCategory>();

                    sQuery = " SELECT A.ServiceId, A.ServiceName, B.CategoryId, B.CategoryName, A.BoatHouseId, A.StockEntryMaintenance FROM FoodItemMaster AS A "
                            + " INNER JOIN FoodCategory AS B ON A.BoatHouseId = B.BoatHouseId AND A.CategoryId = B.CategoryId "
                            + " WHERE  B.CategoryId = @CategoryId AND B.BoatHouseId = @BoatHouseId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHFoodItem.BoatHouseId.Trim();
                    cmd.Parameters["@CategoryId"].Value = BHFoodItem.CategoryId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FoodCategory BoatTypeMaster = new FoodCategory();

                            BoatTypeMaster.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            BoatTypeMaster.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            BoatTypeMaster.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            BoatTypeMaster.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            // BoatTypeMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                            li.Add(BoatTypeMaster);
                        }

                        FoodCategoryList BoatType = new FoodCategoryList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatType);
                    }

                    else
                    {
                        FoodCategoryString BoatType = new FoodCategoryString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatType);
                    }
                }
                else
                {
                    FoodCategoryString Vehicle = new FoodCategoryString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FoodCategoryString ConfRes = new FoodCategoryString
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="bHMstrStock"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("StockEntryDetails/BHId")]
        public IHttpActionResult StockEntryDetails([FromBody] RestStockEntry bHMstrStock)
        {
            try
            {
                if (bHMstrStock.BoatHouseId != "")
                {
                    string sQuery = string.Empty;

                    sQuery = " SELECT StockId,ItemCategoryId,ItemCategory,ItemNameId,ItemName, "
                           + " CONVERT(NVARCHAR,Date,103) AS 'Date',UOM,Quantity,Reference, "
                           + " BoatHouseId,BoatHouseName,ActiveStatus FROM RestaurantStockEntry "
                           + " WHERE BoatHouseId = @BoatHouseId ";

                    List<RestStockEntry> li = new List<RestStockEntry>();

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstrStock.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RestStockEntry OtherServiceMstr = new RestStockEntry();

                            OtherServiceMstr.ItemCategoryId = dt.Rows[i]["ItemCategoryId"].ToString();
                            OtherServiceMstr.ItemCategory = dt.Rows[i]["ItemCategory"].ToString();
                            OtherServiceMstr.ItemNameId = dt.Rows[i]["ItemNameId"].ToString();
                            OtherServiceMstr.ItemName = dt.Rows[i]["ItemName"].ToString();
                            OtherServiceMstr.Date = dt.Rows[i]["Date"].ToString();
                            OtherServiceMstr.UOM = dt.Rows[i]["UOM"].ToString();
                            OtherServiceMstr.Quantity = dt.Rows[i]["Quantity"].ToString();
                            OtherServiceMstr.Reference = dt.Rows[i]["Reference"].ToString();
                            OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            OtherServiceMstr.StockId = dt.Rows[i]["StockId"].ToString();

                            li.Add(OtherServiceMstr);
                        }

                        RestStockEntryList OtherService = new RestStockEntryList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(OtherService);
                    }
                    else
                    {
                        RestStockEntryRes OtherService = new RestStockEntryRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(OtherService);
                    }
                }
                else
                {
                    RestStockEntryRes Vehicle = new RestStockEntryRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                RestStockEntryRes ConfRes = new RestStockEntryRes
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
        /// MODIFIED BY : VINITHA M
        /// MODIFIED DATE : 2022-04-20
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="bHMstrStock"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("StockEntryDetails/BHIdV2")]
        public IHttpActionResult fromtoDate([FromBody] RestStockEntry bHMstrStock)
        {
            try
            {
                if (bHMstrStock.BoatHouseId != "" && bHMstrStock.CountStart != "" && bHMstrStock.ItemCategoryId != "" &&
                    bHMstrStock.ItemNameId != "" && bHMstrStock.FromDate != "" && bHMstrStock.ToDate != "")
                {
                    string sQuery = string.Empty;
                    string sCondition = string.Empty;

                    if (bHMstrStock.ItemCategoryId != "0")
                    {
                        sCondition += " AND ItemCategoryId = @ItemCategoryId ";
                    }

                    if (bHMstrStock.ItemNameId != "0")
                    {
                        sCondition += " AND ItemNameId = @ItemNameId";
                    }

                    sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.Date) 'RowNumber', * FROM "
                           + " ( SELECT StockId,ItemCategoryId,ItemCategory,ItemNameId,ItemName, CONVERT(NVARCHAR,Date,103) AS 'Date',UOM,Quantity,"
                           + " Reference, BoatHouseId,BoatHouseName,ActiveStatus FROM RestaurantStockEntry WHERE BoatHouseId = @BoatHouseId "
                           + " AND Date Between @FromDate and @ToDate " + sCondition.ToString().Trim() + " )AS A) AS B "
                           + " where B.RowNumber BETWEEN @CountStart AND @CountEnd ORDER BY B.RowNumber ASC ";

                    List<RestStockEntry> li = new List<RestStockEntry>();

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CountEnd", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ItemCategoryId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ItemNameId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = bHMstrStock.BoatHouseId.Trim();
                    cmd.Parameters["@CountStart"].Value = bHMstrStock.CountStart.Trim();
                    cmd.Parameters["@CountEnd"].Value = Int32.Parse(bHMstrStock.CountStart.Trim()) + 9;
                    cmd.Parameters["@ItemCategoryId"].Value = bHMstrStock.ItemCategoryId.Trim();
                    cmd.Parameters["@ItemNameId"].Value = bHMstrStock.ItemNameId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(bHMstrStock.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(bHMstrStock.ToDate.Trim(), objEnglishDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RestStockEntry OtherServiceMstr = new RestStockEntry();

                            OtherServiceMstr.ItemCategoryId = dt.Rows[i]["ItemCategoryId"].ToString();
                            OtherServiceMstr.ItemCategory = dt.Rows[i]["ItemCategory"].ToString();
                            OtherServiceMstr.ItemNameId = dt.Rows[i]["ItemNameId"].ToString();
                            OtherServiceMstr.ItemName = dt.Rows[i]["ItemName"].ToString();
                            OtherServiceMstr.Date = dt.Rows[i]["Date"].ToString();
                            OtherServiceMstr.UOM = dt.Rows[i]["UOM"].ToString();
                            OtherServiceMstr.Quantity = dt.Rows[i]["Quantity"].ToString();
                            OtherServiceMstr.Reference = dt.Rows[i]["Reference"].ToString();
                            OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            OtherServiceMstr.StockId = dt.Rows[i]["StockId"].ToString();
                            OtherServiceMstr.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(OtherServiceMstr);

                        }

                        RestStockEntryList OtherService = new RestStockEntryList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(OtherService);
                    }
                    else
                    {
                        RestStockEntryRes OtherService = new RestStockEntryRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(OtherService);
                    }
                }
                else
                {
                    RestStockEntryRes Vehicle = new RestStockEntryRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                RestStockEntryRes ConfRes = new RestStockEntryRes
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 25-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="Restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptRestaurantServiceWiseCollection")]
        public IHttpActionResult Restaurant([FromBody] BoatingReport Restaurant)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate AND @BookingDate "
                        + " AND A.BookingType = 'R' AND B.BookingType = 'R' AND A.BStatus='B'"
                        + " AND A.BoatHouseId = @BoatHouseId AND "
                        + " B.BoatHouseId = @BoatHouseId AND "
                        + " C.BoatHouseId = @BoatHouseId AND "
                        + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' ";

                if (Restaurant.BoatTypeId != "0")
                {
                    conditions += " AND D.CategoryId = @BoatTypeId ";
                }
                if (Restaurant.Category != "0")
                {
                    conditions += " AND A.ServiceId = @Category ";
                }
                if (Restaurant.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType ";
                }
                if (Restaurant.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy ";
                }

                squery = "SELECT BookingDate,ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                        + " ISNULL(CAST((SUM(TaxAmount)/2) AS DECIMAL(18,2)),0) AS 'CGST', "
                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                        + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                        + " FROM "
                        + " (SELECT CONVERT(NVARCHAR(20),CAST(A.BookingDate AS DATE),103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                        + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount "
                        + " FROM BookingRestaurant AS A "
                        + " INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount) "
                        + " AS A "
                        + " GROUP BY BookingDate,ServiceName";


                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = Restaurant.BoatHouseId.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Restaurant.BookingDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatTypeId"].Value = Restaurant.BoatTypeId.Trim();
                cmd.Parameters["@Category"].Value = Restaurant.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = Restaurant.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Restaurant.CreatedBy.Trim();
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
        /// Modified Date 25-Apr-2023 
        /// Modified For Adding Parameterised Query
        /// </summary>
        /// <param name="Restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptRestaurantServiceDetailed")]
        public IHttpActionResult RptRestaurantServiceDtl([FromBody] BoatingReport Restaurant)
        {
            try
            {
                string squery = string.Empty;
                string sBookingRestaurant = string.Empty;
                string sBookingTaxDtl = string.Empty;
                string conditions = string.Empty;
                string conditionsHistory = string.Empty;
                string sFromDate = string.Empty;
                string sToDate = string.Empty;

                if (DateTime.Parse(Restaurant.FromDate.Trim(), objEnglishDate) == DateTime.Parse(Restaurant.ToDate.Trim(), objEnglishDate))
                {
                    conditions = string.Empty;

                    if (CheckDate(Restaurant.FromDate.Trim()))
                    {
                        conditions = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @FromDate "
                           + " AND @ToDate "
                           + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                           + " AND A.BoatHouseId = @BoatHouseId AND "
                           + " B.BoatHouseId = @BoatHouseId  AND "
                           + " C.BoatHouseId = @BoatHouseId  AND "
                           + " D.BoatHouseId = @BoatHouseId  AND E.TypeID = '20' AND A.Bstatus ='B' ";

                        sBookingRestaurant = "BookingRestaurant";
                        sBookingTaxDtl = "BookingTaxDtl";
                    }
                    else
                    {
                        sFromDate = DateTime.Parse(Restaurant.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                        sToDate = DateTime.Parse(Restaurant.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                        conditions = "WHERE A.BookingDate BETWEEN @sFromDate "
                           + " AND @sToDate AND B.Bdate BETWEEN @sFromDate AND  @sToDate "
                           + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                           + " AND A.BoatHouseId = @BoatHouseId AND "
                           + " B.BoatHouseId = @BoatHouseId AND "
                           + " C.BoatHouseId = @BoatHouseId AND "
                           + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.Bstatus ='B' ";

                        sBookingRestaurant = "BookingRestaurantHistory";
                        sBookingTaxDtl = "BookingTaxDtlHistory";
                    }

                    if (Restaurant.BoatTypeId != "0")
                    {
                        conditions += " AND D.CategoryId = @CategoryId ";
                    }
                    if (Restaurant.Category != "0")
                    {
                        conditions += " AND A.ServiceId = @ServiceId ";
                    }
                    if (Restaurant.PaymentType != "0")
                    {
                        conditions += " AND A.PaymentType = @PaymentType ";
                    }
                    if (Restaurant.CreatedBy != "0")
                    {
                        conditions += " AND A.CreatedBy = @CreatedBy ";
                    }

                    squery = " SELECT UniqueId, CreatedBy, BookingDate, BookingId, ServiceName AS 'Particulars', 2 AS 'Ordervalue', "
                        + " SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                        + " ISNULL(CAST((SUM(TaxAmount)/2) AS DECIMAL(18,2)),0) AS 'CGST', "
                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                        + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                        + " FROM "
                        + " (SELECT A.UniqueId AS 'UniqueId', CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100),7) AS 'BookingDate', "
                        + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                        + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, A.CreatedBy AS 'CreatedBy' "
                        + " FROM " + sBookingRestaurant + " AS A "
                        + " INNER JOIN " + sBookingTaxDtl + " AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                        + " " + conditions + " "
                        + " GROUP BY A.BookingDate, A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.CreatedBy) "
                        + " AS A "
                        + " GROUP BY BookingDate,ServiceName, BookingId, CreatedBy, UniqueId "
                        + " ORDER BY CreatedBy, UniqueId, ServiceName, BookingDate ASC ";
                }
                else
                {
                    if (CheckDate(Restaurant.FromDate.Trim()) && CheckDate(Restaurant.ToDate.Trim()))
                    {
                        conditions = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @FromDate "
                                   + " AND @ToDate "
                                   + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " B.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.Bstatus ='B' ";

                        if (Restaurant.BoatTypeId != "0")
                        {
                            conditions += " AND D.CategoryId = @CategoryId";
                        }
                        if (Restaurant.Category != "0")
                        {
                            conditions += " AND A.ServiceId = @ServiceId ";
                        }
                        if (Restaurant.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType ";
                        }
                        if (Restaurant.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy ";
                        }

                        squery = " SELECT UniqueId, CreatedBy, BookingDate, BookingId, "
                               + " ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count',   "
                               + " ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST',  "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) +  "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) +  "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',Ordervalue  "
                               + " FROM(SELECT A.UniqueId AS 'UniqueId',  "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate',  "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,  "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, A.CreatedBy AS 'CreatedBy', 0 AS 'Ordervalue'  "
                               + " FROM BookingRestaurant AS A  "
                               + " INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId  "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId  "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId  "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, A.BookingId,  "
                               + " C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.CreatedBy)  AS A  "
                               + " GROUP BY BookingDate,Ordervalue,ServiceName, BookingId, CreatedBy, UniqueId  "
                               + " UNION ALL  "
                               + " SELECT  '' AS 'UniqueId', '' AS 'CreatedBy',BookingDate, '' AS 'BookingId',  "
                               + " 'Total' AS 'Particulars', SUM(A.NoOfItems) AS 'Count',  "
                               + " ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',Ordervalue "
                               + " FROM(SELECT "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, 1 AS 'Ordervalue' "
                               + " FROM BookingRestaurant AS A "
                               + " INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, "
                               + " A.NoOfItems, A.ChargePerItem, A.NetAmount)  AS A "
                               + " GROUP BY BookingDate, Ordervalue "
                               + " ORDER BY BookingDate, Ordervalue  ASC ";
                    }
                    else if (!CheckDate(Restaurant.FromDate.Trim()) && !CheckDate(Restaurant.ToDate.Trim()))
                    {
                        sFromDate = DateTime.Parse(Restaurant.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                        sToDate = DateTime.Parse(Restaurant.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                        conditions = "WHERE A.BookingDate BETWEEN @sFromDate "
                           + " AND @sToDate AND B.Bdate BETWEEN @sFromDate AND  @sToDate  "
                           + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                           + " AND A.BoatHouseId = @BoatHouseId AND "
                           + " B.BoatHouseId = @BoatHouseId AND "
                           + " C.BoatHouseId = @BoatHouseId AND "
                           + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.Bstatus ='B' ";

                        if (Restaurant.BoatTypeId != "0")
                        {
                            conditions += " AND D.CategoryId = @CategoryId";
                        }
                        if (Restaurant.Category != "0")
                        {
                            conditions += " AND A.ServiceId = @ServiceId";
                        }
                        if (Restaurant.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType";
                        }
                        if (Restaurant.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy";
                        }

                        squery = " SELECT UniqueId, CreatedBy, BookingDate, BookingId, "
                               + " ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count',   "
                               + " ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST',  "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) +  "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) +  "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',Ordervalue  "
                               + " FROM(SELECT A.UniqueId AS 'UniqueId',  "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate',  "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,  "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, A.CreatedBy AS 'CreatedBy', 0 AS 'Ordervalue'  "
                               + " FROM BookingRestaurantHistory AS A  "
                               + " INNER JOIN BookingTaxDtlHistory AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId  "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId  "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId  "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, A.BookingId,  "
                               + " C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount, A.UniqueId, A.CreatedBy)  AS A  "
                               + " GROUP BY BookingDate,Ordervalue,ServiceName, BookingId, CreatedBy, UniqueId  "
                               + " UNION ALL  "
                               + " SELECT  '' AS 'UniqueId', '' AS 'CreatedBy',BookingDate, '' AS 'BookingId',  "
                               + " 'Total' AS 'Particulars', SUM(A.NoOfItems) AS 'Count',  "
                               + " ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',Ordervalue "
                               + " FROM(SELECT "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, 1 AS 'Ordervalue' "
                               + " FROM BookingRestaurantHistory AS A "
                               + " INNER JOIN BookingTaxDtlHistory AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, "
                               + " A.NoOfItems, A.ChargePerItem, A.NetAmount)  AS A "
                               + " GROUP BY BookingDate, Ordervalue "
                               + " ORDER BY BookingDate, Ordervalue  ASC ";
                    }
                    else
                    {
                        sFromDate = DateTime.Parse(Restaurant.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                        sToDate = DateTime.Parse(Restaurant.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                        conditions = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @FromDate "
                                   + " AND @ToDate "
                                   + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                                   + " AND A.BoatHouseId = @BoatHouseId AND "
                                   + " B.BoatHouseId = @BoatHouseId AND "
                                   + " C.BoatHouseId = @BoatHouseId AND "
                                   + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.Bstatus ='B' ";

                        conditionsHistory = " WHERE A.BookingDate BETWEEN '" + sFromDate + "' "
                                        + " AND '" + sToDate + "' AND B.Bdate BETWEEN '" + sFromDate + "' AND  '" + sToDate + "' "
                                        + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                                        + " AND A.BoatHouseId = @BoatHouseId AND "
                                        + " B.BoatHouseId = @BoatHouseId AND "
                                        + " C.BoatHouseId = @BoatHouseId AND "
                                        + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.Bstatus ='B' ";

                        if (Restaurant.BoatTypeId != "0")
                        {
                            conditions += " AND D.CategoryId = @CategoryId ";
                            conditionsHistory += " AND D.CategoryId = @CategoryId ";
                        }
                        if (Restaurant.Category != "0")
                        {
                            conditions += " AND A.ServiceId = @ServiceId ";
                            conditionsHistory += " AND A.ServiceId = @ServiceId ";
                        }
                        if (Restaurant.PaymentType != "0")
                        {
                            conditions += " AND A.PaymentType = @PaymentType ";
                            conditionsHistory += " AND A.PaymentType = @PaymentType ";
                        }
                        if (Restaurant.CreatedBy != "0")
                        {
                            conditions += " AND A.CreatedBy = @CreatedBy ";
                            conditionsHistory += " AND A.CreatedBy = @CreatedBy ";
                        }

                        squery = " SELECT UniqueId, CreatedBy, BookingDate, BookingId, "
                               + " ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count',   "
                               + " ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',   "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST',  "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) +  "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) +  "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',Ordervalue  "
                               + " FROM( "
                               + " SELECT A.UniqueId AS 'UniqueId',  "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate',  "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,  "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, A.CreatedBy AS 'CreatedBy', 0 AS 'Ordervalue'  "
                               + " FROM BookingRestaurant AS A  "
                               + " INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId  "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId  "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId  "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, A.BookingId,  "
                               + " C.ServiceName, A.NoOfItems, A.ChargePerItem, "
                               + " A.NetAmount, A.UniqueId, A.CreatedBy "
                               + " UNION ALL"
                               + " SELECT A.UniqueId AS 'UniqueId',  "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate',  "
                               + " A.BookingId, C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems,  "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, A.CreatedBy AS 'CreatedBy', 0 AS 'Ordervalue'  "
                               + " FROM BookingRestaurantHistory AS A  "
                               + " INNER JOIN BookingTaxDtlHistory AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId  "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId  "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId  "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditionsHistory + " "
                               + " GROUP BY A.BookingDate, A.BookingId,  "
                               + " C.ServiceName, A.NoOfItems, A.ChargePerItem, "
                               + " A.NetAmount, A.UniqueId, A.CreatedBy "
                               + ")  AS A  "
                               + " GROUP BY BookingDate,Ordervalue,ServiceName, BookingId, CreatedBy, UniqueId  "
                               + " UNION ALL  "
                               + " SELECT  '' AS 'UniqueId', '' AS 'CreatedBy',BookingDate, '' AS 'BookingId',  "
                               + " 'Total' AS 'Particulars', SUM(A.NoOfItems) AS 'Count',  "
                               + " ISNULL(CAST(SUM(Charge) AS DECIMAL(18, 2)), 0) AS 'Amount',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST',  "
                               + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                               + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + "
                               + " CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount',Ordervalue "
                               + " FROM( "
                               + " SELECT "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, 1 AS 'Ordervalue' "
                               + " FROM BookingRestaurant AS A "
                               + " INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditions + " "
                               + " GROUP BY A.BookingDate, "
                               + " A.NoOfItems, A.ChargePerItem, A.NetAmount "
                               + " UNION ALL"
                               + " SELECT "
                               + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + '13:00PM' AS 'BookingDate', "
                               + " (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                               + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, 1 AS 'Ordervalue' "
                               + " FROM BookingRestaurantHistory AS A "
                               + " INNER JOIN BookingTaxDtlHistory AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                               + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId "
                               + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId "
                               + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID  "
                               + " " + conditionsHistory + " "
                               + " GROUP BY A.BookingDate, "
                               + " A.NoOfItems, A.ChargePerItem, A.NetAmount "
                               + ")  AS A "
                               + " GROUP BY BookingDate, Ordervalue "
                               + " ORDER BY BookingDate, Ordervalue  ASC ";
                    }
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sFromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sToDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = Restaurant.BoatHouseId.Trim();
                cmd.Parameters["@CategoryId"].Value = Restaurant.BoatTypeId.Trim();
                cmd.Parameters["@ServiceId"].Value = Restaurant.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = Restaurant.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Restaurant.CreatedBy.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(Restaurant.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(Restaurant.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@sFromDate"].Value = sFromDate;
                cmd.Parameters["@sToDate"].Value = sToDate;
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


   

        /// <summary>
        /// Modified By Abhinaya K
        /// Modified Date 25-Apr-2023 
        /// Modified For Adding Parameterised Query
        /// </summary>
        /// <param name="RestaurantDetailed"></param>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        [Route("RptRestaurantDetailedServiceWiseCollection")]
        public IHttpActionResult AllRestaurantDetailed([FromBody] BoatingReport RestaurantDetailed)
        {
            try
            {
                string squery = string.Empty;

                string sUserCondition = string.Empty;
                string conditions = string.Empty;
                string conditionsHistory = string.Empty;
                string sFromDate = string.Empty;
                string sToDate = string.Empty;
                if (RestaurantDetailed.CreatedBy != "0")
                {
                    sUserCondition = " AND A.CreatedBy = @CreatedBy";
                }
                if (CheckDate(RestaurantDetailed.FromDate.Trim()) && CheckDate(RestaurantDetailed.ToDate.Trim()))
                {
                    conditions = " WHERE CAST(A.BookingDate  AS DATE) BETWEEN @FromDate "
                               + " AND @ToDate AND "
                               + " A.BoatHouseId = @BoatHouseId AND "
                               + " B.BoatHouseId = @BoatHouseId AND "
                               + " C.BoatHouseId = @BoatHouseId AND "
                               + " A.BookingType = 'R' AND C.BookingType = 'R' AND A.BStatus='B'";

                    squery = " SELECT A.UniqueId, A.BookingId,CONVERT(NVARCHAR(20),CAST(A.BookingDate AS DATE),103) AS 'BookingDate',B.ServiceName AS 'Particulars', "
                           + " A.NoOfItems AS 'QTY',B.ServiceTotalAmount AS 'CostPerItem',ISNULL(CAST(A.ChargePerItem * A.NoOfItems AS DECIMAL(18,2)),0) AS 'Amount', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'CGST', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'SGST', "
                           + " ISNULL(CAST(A.ChargePerItem * A.NoOfItems + SUM(C.TaxAmount) AS DECIMAL(18, 2)),0) AS 'TotalAmount', "
                           + " A.CreatedBy "
                           + " FROM BookingRestaurant AS A "
                           + " INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                           + " INNER JOIN BookingTaxDtl AS C ON A.BookingId = C.BookingId AND A.ServiceId = C.ServiceId AND "
                           + " B.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId AND B.BoatHouseId = C.BoatHouseId "
                           + " " + conditions + " "
                           + " " + sUserCondition + " "
                           + " GROUP BY A.BookingId,CAST(A.BookingDate AS DATE),B.ServiceName,A.ChargePerItem,A.NoOfItems, A.UniqueId, A.CreatedBy, B.ServiceTotalAmount "
                           + " ORDER BY A.CreatedBy, A.UniqueId ASC ";
                }
                else if (!CheckDate(RestaurantDetailed.FromDate.Trim()) && !CheckDate(RestaurantDetailed.ToDate.Trim()))
                {
                    sFromDate = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                    sToDate = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                    conditions = " WHERE A.BookingDate BETWEEN @sFromDate AND @sToDate AND "
                               + " C.BDate BETWEEN @sFromDate AND @sToDate AND "
                               + " A.BoatHouseId = @BoatHouseId AND "
                               + " B.BoatHouseId = @BoatHouseId AND "
                               + " C.BoatHouseId = @BoatHouseId AND "
                               + " A.BookingType = 'R' AND C.BookingType = 'R' AND A.BStatus='B'";

                    squery = " SELECT A.UniqueId, A.BookingId,CONVERT(NVARCHAR(20),CAST(A.BookingDate AS DATE),103) AS 'BookingDate',B.ServiceName AS 'Particulars', "
                           + " A.NoOfItems AS 'QTY',B.ServiceTotalAmount AS 'CostPerItem',ISNULL(CAST(A.ChargePerItem * A.NoOfItems AS DECIMAL(18,2)),0) AS 'Amount', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'CGST', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'SGST', "
                           + " ISNULL(CAST(A.ChargePerItem * A.NoOfItems + SUM(C.TaxAmount) AS DECIMAL(18, 2)),0) AS 'TotalAmount', "
                           + " A.CreatedBy "
                           + " FROM BookingRestaurantHistory AS A "
                           + " INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                           + " INNER JOIN BookingTaxDtlHistory AS C ON A.BookingId = C.BookingId AND A.ServiceId = C.ServiceId AND "
                           + " B.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId AND B.BoatHouseId = C.BoatHouseId "
                           + " " + conditions + " "
                            + " " + sUserCondition + " "
                           + " GROUP BY A.BookingId,CAST(A.BookingDate AS DATE),B.ServiceName,A.ChargePerItem,A.NoOfItems, A.UniqueId, A.CreatedBy, B.ServiceTotalAmount "
                           + " ORDER BY A.CreatedBy, A.UniqueId ASC ";
                }
                else
                {
                    conditions = " WHERE CAST(A.BookingDate  AS DATE) BETWEEN @FromDate "
                                + " AND @ToDate AND "
                                + " A.BoatHouseId = @BoatHouseId AND "
                                + " B.BoatHouseId = @BoatHouseId AND "
                                + " C.BoatHouseId = @BoatHouseId AND "
                                + " A.BookingType = 'R' AND C.BookingType = 'R' AND A.BStatus='B'";

                    sFromDate = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                    sToDate = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                    conditionsHistory = " WHERE A.BookingDate BETWEEN @sFromDate AND @sToDate AND "
                               + " C.BDate BETWEEN @sFromDate AND @sToDate AND "
                               + " A.BoatHouseId = @BoatHouseId AND "
                               + " B.BoatHouseId = @BoatHouseId AND "
                               + " C.BoatHouseId = @BoatHouseId AND "
                               + " A.BookingType = 'R' AND C.BookingType = 'R' AND A.BStatus='B'";

                    squery = " SELECT A.UniqueId, A.BookingId,CONVERT(NVARCHAR(20),CAST(A.BookingDate AS DATE),103) AS 'BookingDate',B.ServiceName AS 'Particulars', "
                           + " A.NoOfItems AS 'QTY',B.ServiceTotalAmount AS 'CostPerItem',ISNULL(CAST(A.ChargePerItem * A.NoOfItems AS DECIMAL(18,2)),0) AS 'Amount', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'CGST', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'SGST', "
                           + " ISNULL(CAST(A.ChargePerItem * A.NoOfItems + SUM(C.TaxAmount) AS DECIMAL(18, 2)),0) AS 'TotalAmount', "
                           + " A.CreatedBy "
                           + " FROM BookingRestaurant AS A "
                           + " INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                           + " INNER JOIN BookingTaxDtl AS C ON A.BookingId = C.BookingId AND A.ServiceId = C.ServiceId AND "
                           + " B.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId AND B.BoatHouseId = C.BoatHouseId "
                           + " " + conditions + " "
                           + " " + sUserCondition + " "
                           + " GROUP BY A.BookingId,CAST(A.BookingDate AS DATE),B.ServiceName,A.ChargePerItem,A.NoOfItems, A.UniqueId, A.CreatedBy, B.ServiceTotalAmount "
                           + " UNION ALL"
                           + " SELECT A.UniqueId, A.BookingId,CONVERT(NVARCHAR(20),CAST(A.BookingDate AS DATE),103) AS 'BookingDate',B.ServiceName AS 'Particulars', "
                           + " A.NoOfItems AS 'QTY',B.ServiceTotalAmount AS 'CostPerItem',ISNULL(CAST(A.ChargePerItem * A.NoOfItems AS DECIMAL(18,2)),0) AS 'Amount', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'CGST', "
                           + " ISNULL(CAST((SUM(C.TaxAmount) / 2) AS DECIMAL(18, 2)),0) AS 'SGST', "
                           + " ISNULL(CAST(A.ChargePerItem * A.NoOfItems + SUM(C.TaxAmount) AS DECIMAL(18, 2)),0) AS 'TotalAmount', "
                           + " A.CreatedBy "
                           + " FROM BookingRestaurantHistory AS A "
                           + " INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                           + " INNER JOIN BookingTaxDtlHistory AS C ON A.BookingId = C.BookingId AND A.ServiceId = C.ServiceId AND "
                           + " B.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId AND B.BoatHouseId = C.BoatHouseId "
                           + " " + conditionsHistory + " "
                            + " " + sUserCondition + " "
                           + " GROUP BY A.BookingId,CAST(A.BookingDate AS DATE),B.ServiceName,A.ChargePerItem,A.NoOfItems, A.UniqueId, A.CreatedBy, B.ServiceTotalAmount "
                           + " ORDER BY A.CreatedBy, A.UniqueId ASC ";
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sFromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@sToDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BoatHouseId"].Value = RestaurantDetailed.BoatHouseId.Trim();
                cmd.Parameters["@CreatedBy"].Value = RestaurantDetailed.CreatedBy.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate);
                //cmd.Parameters["@sFromDate"].Value = sFromDate;
                //cmd.Parameters["@sToDate"].Value = sToDate;

                cmd.Parameters["@sFromDate"].Value = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                cmd.Parameters["@sToDate"].Value = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

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
        /// Modified Date 25-Apr-2023 
        /// Modified For Adding Parameterised Query 
        /// </summary>
        /// <param name="dtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantDetailsCount")]
        public IHttpActionResult RestaurantDetailsCount([FromBody] BookingOtherServices dtl)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string sBookingMedia = string.Empty;

                if (dtl.CreatedBy != "0")
                {
                    conditions = " AND Createdby=@CreatedBy ";
                    sBookingMedia = " AND BookingMedia NOT IN ('PW', 'PA', 'PI')";
                }

                squery = " SELECT ISNULL(SUM(ISNULL(NoOfItems,0)),0) AS 'BookingCount', ISNULL(SUM(ISNULL(NetAmount, 0)), 0) AS 'NetAmount' "
                + " FROM BookingRestaurant "
                + " WHERE CAST(BookingDate AS DATE) = @BookingDate AND BoatHouseId = @BoatHouseId "
                + " AND BookingType = 'R' AND BStatus = 'B' " + conditions + sBookingMedia;
                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = dtl.BoatHouseId.Trim();
                cmd.Parameters["@CreatedBy"].Value = dtl.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(dtl.BookingDate.Trim(), objEnglishDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();
                return Ok(ds);
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
        /// Modified By Jaya Suriya A, Added A.CreatedBy
        /// Modified Date:15-11-2021
        /// Modified By Abhinaya K
        /// Modified Date 25-Apr-2023
        ///  Modified For Adding Parameterised Query 
        /// </summary>
        /// <param name="Restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractRestaurantServiceWiseCollection")]
        public IHttpActionResult RestaurantAbs([FromBody] BoatingReport Restaurant)
        {
            try
            {
                string squery = string.Empty;

                string conditions = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate "
                        + " AND @BookingDate "
                        + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                        + " AND A.BoatHouseId = @BoatHouseId AND "
                        + " B.BoatHouseId = @BoatHouseId AND "
                        + " C.BoatHouseId = @BoatHouseId AND "
                        + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.BStatus = 'B' ";

                if (Restaurant.BoatTypeId != "0")
                {
                    conditions += " AND D.CategoryId = @CategoryId";
                }
                if (Restaurant.Category != "0")
                {
                    conditions += " AND A.ServiceId = @ServiceId";
                }
                if (Restaurant.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType";
                }
                if (Restaurant.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                }

                squery = "SELECT BookingDate,ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                        + " ISNULL(CAST((SUM(TaxAmount)/2) AS DECIMAL(18,2)),0) AS 'CGST', "
                        + " ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST', "
                        + " ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) AS DECIMAL(18, 2)), 0) AS 'TotalAmount' "
                        + " FROM "
                        + " (SELECT CONVERT(NVARCHAR(20),CAST(A.BookingDate AS DATE),103) AS 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, "
                        + " SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount "
                        + " FROM BookingRestaurant AS A "
                        + " INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN ConfigurationMaster AS E ON A.PaymentType = E.ConfigID "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, C.ServiceName, A.NoOfItems, A.ChargePerItem, A.NetAmount) "
                        + " AS A "
                        + " GROUP BY BookingDate,ServiceName";


                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = Restaurant.BoatHouseId.Trim();
                cmd.Parameters["@CategoryId"].Value = Restaurant.BoatTypeId.Trim();
                cmd.Parameters["@ServiceId"].Value = Restaurant.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = Restaurant.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Restaurant.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Restaurant.BookingDate.Trim(), objEnglishDate);

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
        /// Modified By Imran, Added A.CreatedBy
        /// Modified Date:16-11-2021
        /// Modified By Abhinaya K
        /// Modified Date 25-Apr-2023
        ///  Modified For Adding Parameterised Query 
        /// </summary>
        /// <param name="Restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractRestaurantServiceWiseCollection_Test")]
        public IHttpActionResult RestaurantAbs_Test([FromBody] BoatingReport Restaurant)
        {
            try
            {
                string squery = string.Empty;
                string conditions = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate "
                + " AND @BookingDate "
                + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                + " AND A.BoatHouseId = @BoatHouseId' AND "
                + " B.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND "
                + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.BStatus = 'B' ";

                if (Restaurant.BoatTypeId != "0")
                {
                    conditions += " AND D.CategoryId = @CategoryId ";
                }
                if (Restaurant.Category != "0")
                {
                    conditions += " AND A.ServiceId = @ServiceId ";
                }
                if (Restaurant.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType ";
                }
                if (Restaurant.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy ";
                }

                squery = "SELECT BookingDate,ServiceName AS 'Particulars', SUM(A.NoOfItems) AS 'Count', ISNULL(CAST(SUM(Charge) AS DECIMAL(18,2)),0) AS 'Amount', "
                        + "ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'CGST', ISNULL(CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)), 0) AS 'SGST',  "
                        + "ISNULL(CAST(CAST(SUM(Charge) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2)) + CAST((SUM(TaxAmount) / 2) AS DECIMAL(18, 2))  "
                        + " AS DECIMAL(18, 2)), 0) AS 'TotalAmount' ,A.CategoryName AS 'ConfigName',A.BoatHouseId,A.CategoryId AS 'ConfigID'  FROM (SELECT CONVERT(NVARCHAR(20), CAST(A.BookingDate AS DATE), 103) AS  "
                        + " 'BookingDate', C.ServiceName, (A.ChargePerItem * A.NoOfItems) AS 'Charge', A.NoOfItems, SUM(B.TaxAmount) AS 'TaxAmount', A.NetAmount, D.CategoryName,  "
                        + " A.BoatHouseId, D.CategoryId FROM BookingRestaurant AS A INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId  "
                        + " AND A.BoatHouseId = B.BoatHouseId INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId INNER JOIN  "
                        + " FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId INNER JOIN ConfigurationMaster AS E ON  "
                        + " A.PaymentType = E.ConfigID  "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE), A.BookingId, C.ServiceName,  "
                        + " A.NoOfItems, A.ChargePerItem, A.NetAmount, D.CategoryName, A.BoatHouseId, D.CategoryId)  "
                        + " AS A GROUP BY BookingDate,ServiceName,A.CategoryName,BoatHouseId,CategoryId order by CategoryName asc";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = Restaurant.BoatHouseId.Trim();
                cmd.Parameters["@CategoryId"].Value = Restaurant.BoatTypeId.Trim();
                cmd.Parameters["@ServiceId"].Value = Restaurant.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = Restaurant.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Restaurant.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Restaurant.BookingDate.Trim(), objEnglishDate);
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
        /// Modified By Imran, Added A.CreatedBy
        /// Modified Date:16-11-2021
        /// Modified By Abhinaya K
        /// Modified Date 25-Apr-2023
        ///  Modified For Adding Parameterised Query 
        /// </summary>
        /// <param name="Restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractRestaurantServiceWiseCollectionMain_Test")]
        public IHttpActionResult RestaurantAbsMain_Test([FromBody] BoatingReport Restaurant)
        {
            try
            {
                string squery = string.Empty;
                string conditions = "WHERE CAST(A.BookingDate AS DATE) BETWEEN @BookingDate "
                + " AND @BookingDate "
                + " AND A.BookingType = 'R' AND B.BookingType = 'R' "
                + " AND A.BoatHouseId = @BoatHouseId AND "
                + " B.BoatHouseId = @BoatHouseId AND "
                + " C.BoatHouseId = @BoatHouseId AND "
                + " D.BoatHouseId = @BoatHouseId AND E.TypeID = '20' AND A.BStatus = 'B' ";

                if (Restaurant.BoatTypeId != "0")
                {
                    conditions += " AND D.CategoryId = @CategoryId";
                }
                if (Restaurant.Category != "0")
                {
                    conditions += " AND A.ServiceId = @ServiceId";
                }
                if (Restaurant.PaymentType != "0")
                {
                    conditions += " AND A.PaymentType = @PaymentType ";
                }
                if (Restaurant.CreatedBy != "0")
                {
                    conditions += " AND A.CreatedBy = @CreatedBy";
                }

                squery = "SELECT BookingDate,A.CategoryName AS 'ConfigName',A.BoatHouseId,A.CategoryId AS 'ConfigID' FROM (SELECT CONVERT(NVARCHAR(20),CAST(A.BookingDate AS DATE),103) AS "
                        + " 'BookingDate', D.CategoryName, "
                        + " A.BoatHouseId,D.CategoryId FROM BookingRestaurant AS A INNER JOIN BookingTaxDtl AS B ON A.BookingId = B.BookingId AND A.ServiceId = B.ServiceId "
                        + " AND A.BoatHouseId = B.BoatHouseId INNER JOIN FoodItemMaster AS C ON A.ServiceId = C.ServiceId AND A.BoatHouseId = C.BoatHouseId INNER JOIN "
                        + " FoodCategory AS D ON C.CategoryId = D.CategoryId AND C.BoatHouseId = D.BoatHouseId INNER JOIN ConfigurationMaster AS E ON "
                        + " A.PaymentType = E.ConfigID  "
                        + " " + conditions + " "
                        + " GROUP BY CAST(A.BookingDate AS DATE),D.CategoryName,A.BoatHouseId,D.CategoryId) "
                        + " AS A GROUP BY BookingDate,A.CategoryName,BoatHouseId,CategoryId order by CategoryName asc";

                con.Open();
                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BookingDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));

                cmd.Parameters["@BoatHouseId"].Value = Restaurant.BoatHouseId.Trim();
                cmd.Parameters["@CategoryId"].Value = Restaurant.BoatTypeId.Trim();
                cmd.Parameters["@ServiceId"].Value = Restaurant.Category.Trim();
                cmd.Parameters["@PaymentType"].Value = Restaurant.PaymentType.Trim();
                cmd.Parameters["@CreatedBy"].Value = Restaurant.CreatedBy.Trim();
                cmd.Parameters["@BookingDate"].Value = DateTime.Parse(Restaurant.BookingDate.Trim(), objEnglishDate);


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
        /// Created By Abhinaya
        /// Created Date 30-dec-2021
        ///  Modified For Adding Parameterised Query 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var props = typeof(TSource).GetProperties();

            var dt = new DataTable();
            dt.Columns.AddRange(
              props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );

            source.ToList().ForEach(
              i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );

            return dt;
        }


        /// <summary>
        /// Created By abhinaya
        /// Created Date 30-dec-2021
        /// Modified By : Jaya Suriya
        /// Modified Date 2022-05-26
        /// </summary>
        /// <param name="RestaurantDetailed"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantCatWiseReport")]
        public IHttpActionResult RestaurantCatWiseReport([FromBody] BoatingReport RestaurantDetailed)
        {
            try
            {
                string sQuery = string.Empty;
                var fromDate = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate);
                var toDate = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate);
                var bhId = RestaurantDetailed.BoatHouseId;

                sQuery = string.Format(@"SELECT ServiceId,CategoryId, ServiceName, ServiceTotalAmount, BoatHouseId
                            FROm FoodItemMaster AS B 
                           WHERE B.BoatHouseId = '{0}'", bhId);

                DataTable dtFoodItemMaster = new DataTable();
                DataTable dtBookingRestaurant = new DataTable();
                DataTable dtFoodCategory = new DataTable();

                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtFoodItemMaster);
                IList<clFooditemMaster> clfooditems = dtFoodItemMaster.AsEnumerable().
                    Select(r => new clFooditemMaster
                    {
                        BoatHouseId = r.Field<int>("BoatHouseId"),
                        CategoryId = r.Field<string>("CategoryId"),
                        ServiceName = r.Field<string>("ServiceName"),
                        ServiceId = r.Field<int>("ServiceId"),
                        ServiceTotalAmount = r.Field<decimal>("ServiceTotalAmount"),

                    }).ToList();
                if (CheckDate(RestaurantDetailed.FromDate.Trim()) && CheckDate(RestaurantDetailed.ToDate.Trim()))
                {
                    sQuery = string.Format(@"SELECT NoOfItems, ChargePerItem, BookingId,
                                             BoatHouseId,ServiceId 
                                             FROm BookingRestaurant AS A  
                                             WHERE A.BStatus = 'B' AND A.BoatHouseId = '{0}'
                                             and CAST(A.BookingDate AS DATE)
                                             BETWEEN '{1}' AND '{2}'", bhId, fromDate, toDate);
                }
                else if (!CheckDate(RestaurantDetailed.FromDate.Trim()) && !CheckDate(RestaurantDetailed.ToDate.Trim()))
                {
                    string sFromDate = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                    string sToDate = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                    sQuery = string.Format(
                                            @"SELECT NoOfItems, ChargePerItem, BookingId, 
                                              BoatHouseId,ServiceId 
                                              FROm BookingRestaurantHistory AS A  
                                              WHERE A.BStatus = 'B' AND A.BoatHouseId = '{0}' and 
                                              A.BookingDate
                                              BETWEEN '{1}' AND '{2}'", bhId, sFromDate, sToDate
                                           );
                }
                else
                {
                    string sFromDate = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                    string sToDate = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                    sQuery = string.Format(
                                            @"SELECT NoOfItems, ChargePerItem, BookingId, 
                                              BoatHouseId,ServiceId 
                                              FROm BookingRestaurant AS A  
                                              WHERE A.BStatus = 'B' AND A.BoatHouseId = '{0}' and 
                                              CAST(A.BookingDate AS DATE)
                                              BETWEEN '{1}' AND '{2}'
                                              UNION ALL
                                              SELECT NoOfItems, ChargePerItem, BookingId, 
                                              BoatHouseId,ServiceId 
                                              FROm BookingRestaurantHistory AS A  
                                              WHERE A.BStatus = 'B' AND A.BoatHouseId = '{0}' and 
                                              CAST(A.BookingDate AS DATE)
                                              BETWEEN '{3}' AND '{4}'
                                              ", bhId, fromDate, toDate, sFromDate, sToDate
                                           );
                }

                cmd = new SqlCommand(sQuery, con);
                da = new SqlDataAdapter(cmd);
                da.Fill(dtBookingRestaurant);
                IList<clBookingRestaurant> clbookingres = dtBookingRestaurant.AsEnumerable()
                        .Select(r => new clBookingRestaurant
                        {
                            BoatHouseId = r.Field<int>("BoatHouseId"),
                            BookingId = r.Field<string>("BookingId"),
                            ChargePerItem = r.Field<decimal>("ChargePerItem"),
                            NoOfItems = r.Field<int>("NoOfItems"),
                            ServiceId = r.Field<int>("ServiceId")
                        }).ToList();

                sQuery = string.Format(@"SELECT CAST(CategoryId AS VARCHAR) AS 'CategoryId', CategoryName, BoatHouseId FROm FoodCategory AS C 
                          WHERE C.BoatHouseId = '{0}' ", bhId);
                cmd = new SqlCommand(sQuery, con);
                da = new SqlDataAdapter(cmd);
                da.Fill(dtFoodCategory);
                IList<clFoodCategory> clfoodcategories = dtFoodCategory.AsEnumerable().
                    Select(r =>
                    new clFoodCategory
                    {
                        BoatHouseId = r.Field<int>("BoatHouseId"),
                        CategoryId = r.Field<string>("CategoryId"),
                        CategoryName = r.Field<string>("CategoryName"),

                    }).ToList();


                var taxcal = (from cbl in clbookingres
                              join fooditem in clfooditems
                              on new { cbl.BoatHouseId, cbl.ServiceId } equals new { fooditem.BoatHouseId, fooditem.ServiceId }
                              select new TaxCal
                              {
                                  BoatHouseId = fooditem.BoatHouseId,
                                  BookingId = cbl.BookingId,
                                  ServiceId = fooditem.ServiceId
                              }).ToList();
                if (CheckDate(RestaurantDetailed.FromDate.Trim()) && CheckDate(RestaurantDetailed.ToDate.Trim()))
                {
                    foreach (var item in taxcal)
                    {
                        string taxquery = string.Format(@"select dbo.GetTaxAmountDetails
                        ('AbsRestaurant', '{0}', '{1}', '{2}')", item.BoatHouseId, item.BookingId, item.ServiceId);

                        cmd = new SqlCommand(taxquery, con);
                        var res = cmd.ExecuteScalar();
                        item.TaxAmt = (decimal)res;
                    }

                }
                else if (!CheckDate(RestaurantDetailed.FromDate.Trim()) && !CheckDate(RestaurantDetailed.ToDate.Trim()))
                {
                    foreach (var item in taxcal)
                    {
                        string taxquery = string.Format(@"select dbo.GetTaxAmountDetails
                        ('AbsRestaurantHistory', '{0}', '{1}', '{2}')", item.BoatHouseId, item.BookingId, item.ServiceId);

                        cmd = new SqlCommand(taxquery, con);
                        var res = cmd.ExecuteScalar();
                        item.TaxAmt = (decimal)res;
                    }
                }
                else
                {
                    foreach (var item in taxcal)
                    {
                        string taxquery = string.Format(@"select dbo.GetTaxAmountDetails
                        ('AbsRestaurantAndHistory', '{0}', '{1}', '{2}')", item.BoatHouseId, item.BookingId, item.ServiceId);

                        cmd = new SqlCommand(taxquery, con);
                        var res = cmd.ExecuteScalar();
                        item.TaxAmt = (decimal)res;
                    }
                }
                var dtTaxcal = ConvertToDataTable(taxcal);

                int count = 1;
                var reportstru = (from cbl in clbookingres
                                  join fooditmmstr in clfooditems
                                    on new { cbl.ServiceId, cbl.BoatHouseId } equals new { fooditmmstr.ServiceId, fooditmmstr.BoatHouseId }
                                  join foodcat in clfoodcategories
                                  on new { cbl.BoatHouseId, fooditmmstr.CategoryId } equals new { foodcat.BoatHouseId, foodcat.CategoryId }
                                  let txamt = taxcal.Where(b => b.BoatHouseId == cbl.BoatHouseId
                                          && b.BookingId == cbl.BookingId && b.ServiceId == cbl.ServiceId).Select(t => t.TaxAmt).FirstOrDefault()
                                  select new CategoryReport
                                  {
                                      Sno = count++,
                                      CategoryId = foodcat.CategoryId,
                                      CategoryName = foodcat.CategoryName,
                                      ServiceId = fooditmmstr.ServiceId,
                                      ServiceName = fooditmmstr.ServiceName,
                                      ItemRate = fooditmmstr.ServiceTotalAmount,
                                      Quantity = cbl.NoOfItems,
                                      Charge = cbl.ChargePerItem * cbl.NoOfItems,
                                      TaxAmount = txamt,
                                      Total = txamt + (cbl.ChargePerItem * cbl.NoOfItems),
                                      OrderValue = 1
                                  }
                            );
                var rep = reportstru.OrderBy(o => o.CategoryName).
                    GroupBy(p => new { p.CategoryId, p.CategoryName, p.ServiceId, p.ServiceName, p.ItemRate }).
                    Select(rp => new CategoryReport
                    {
                        CategoryId = rp.Key.CategoryId,
                        CategoryName = rp.Key.CategoryName,
                        ServiceId = rp.Key.ServiceId,
                        ServiceName = rp.Key.ServiceName,
                        ItemRate = rp.Key.ItemRate,
                        Quantity = rp.Sum(a => a.Quantity),
                        Charge = rp.Sum(a => a.Charge),
                        TaxAmount = rp.Sum(a => a.TaxAmount),
                        Total = rp.Sum(a => a.Total),

                    });
                var headers = reportstru.GroupBy(pg => new { pg.CategoryId, pg.CategoryName })
                    .OrderBy(o => o.Key.CategoryId).Select(st => new CategoryReport
                    {
                        CategoryId = st.Key.CategoryId,
                        ServiceName = st.Key.CategoryName,

                    });

                var Total = reportstru.GroupBy(pg => new { pg.CategoryId, pg.CategoryName })
                   .OrderBy(o => o.Key.CategoryId).Select(st => new CategoryReport
                   {
                       CategoryId = st.Key.CategoryId,
                       ServiceName = "Total",
                       Quantity = st.Sum(a => a.Quantity),
                       Charge = st.Sum(a => a.Charge),
                       TaxAmount = st.Sum(a => a.TaxAmount),
                       Total = st.Sum(a => a.Total),

                   });
                var dtTotal = ConvertToDataTable(Total);
                var dtRe = ConvertToDataTable(rep);
                var dtHeaders = ConvertToDataTable(headers);


                int counts = 1;
                var infoQuery = (from Header in headers
                                 select new CategoryReport
                                 {
                                     Sno = 0,
                                     CategoryId = Header.CategoryId,
                                     CategoryName = Header.CategoryName,
                                     ServiceId = Header.ServiceId,
                                     ServiceName = Header.ServiceName,
                                     ItemRate = Header.ItemRate,
                                     Quantity = Header.Quantity,
                                     Charge = Header.Charge,
                                     TaxAmount = Header.TaxAmount,
                                     Total = Header.Total,
                                     OrderValue = 0,
                                 })
                                .Union
                                    (from owed in rep
                                     select new CategoryReport
                                     {
                                         Sno = counts++,
                                         CategoryId = owed.CategoryId,
                                         CategoryName = owed.CategoryName,
                                         ServiceId = owed.ServiceId,
                                         ServiceName = owed.ServiceName,
                                         ItemRate = owed.ItemRate,
                                         Quantity = owed.Quantity,
                                         Charge = owed.Charge,
                                         TaxAmount = owed.TaxAmount,
                                         Total = owed.Total,
                                         OrderValue = 1,
                                     })
                                      .Union(from Totals in Total
                                             select new CategoryReport
                                             {
                                                 Sno = 0,
                                                 CategoryId = Totals.CategoryId,
                                                 CategoryName = Totals.CategoryName,
                                                 ServiceId = Totals.ServiceId,
                                                 ServiceName = Totals.ServiceName,
                                                 ItemRate = Totals.ItemRate,
                                                 Quantity = Totals.Quantity,
                                                 Charge = Totals.Charge,
                                                 TaxAmount = Totals.TaxAmount,
                                                 Total = Totals.Total,
                                                 OrderValue = 2,
                                             });
                var FinalQuerys = infoQuery.OrderBy(o => o.OrderValue).OrderBy(o => o.CategoryId).
        Select(st => new CategoryReport
        {
            Sno = st.Sno,
            CategoryId = st.CategoryId,
            CategoryName = st.CategoryName,
            ServiceId = st.ServiceId,
            ServiceName = st.ServiceName,
            ItemRate = st.ItemRate,
            Quantity = st.Quantity,
            Charge = st.Charge,
            TaxAmount = st.TaxAmount,
            Total = st.Total,
            OrderValue = st.OrderValue,

        });

                var infoQuerys = ConvertToDataTable(FinalQuerys);
                con.Close();

                DataSet ds = new DataSet();

                ds.Tables.Add(infoQuerys);

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

        ///// <summary>
        ///// Created By abhinaya
        ///// Created Date 30-dec-2021
        ///// </summary>
        ///// <param name="RestaurantDetailed"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RestaurantCatWiseReport")]
        //public IHttpActionResult RestaurantCatWiseReport([FromBody] BoatingReport RestaurantDetailed)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        var fromDate = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate);
        //        var toDate = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate);
        //        var bhId = RestaurantDetailed.BoatHouseId;

        //        sQuery = string.Format(@"SELECT ServiceId,CategoryId, ServiceName, ServiceTotalAmount, BoatHouseId
        //                    FROm FoodItemMaster AS B 
        //                   WHERE B.BoatHouseId = '{0}'", bhId);

        //        DataTable dtFoodItemMaster = new DataTable();
        //        DataTable dtBookingRestaurant = new DataTable();
        //        DataTable dtFoodCategory = new DataTable();

        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(sQuery, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dtFoodItemMaster);
        //        IList<clFooditemMaster> clfooditems = dtFoodItemMaster.AsEnumerable().
        //            Select(r => new clFooditemMaster
        //            {
        //                BoatHouseId = r.Field<int>("BoatHouseId"),
        //                CategoryId = r.Field<string>("CategoryId"),
        //                ServiceName = r.Field<string>("ServiceName"),
        //                ServiceId = r.Field<int>("ServiceId"),
        //                ServiceTotalAmount = r.Field<decimal>("ServiceTotalAmount"),

        //            }).ToList();

        //        sQuery = string.Format(@"SELECT NoOfItems, ChargePerItem, BookingId, BoatHouseId,ServiceId 
        //                FROm BookingRestaurant AS A  
        //                   WHERE A.BStatus = 'B' AND A.BoatHouseId = '{0}' and CAST(A.BookingDate AS DATE)
        //                BETWEEN '{1}' AND '{2}'", bhId, fromDate, toDate);
        //        cmd = new SqlCommand(sQuery, con);
        //        da = new SqlDataAdapter(cmd);
        //        da.Fill(dtBookingRestaurant);
        //        IList<clBookingRestaurant> clbookingres = dtBookingRestaurant.AsEnumerable()
        //                .Select(r => new clBookingRestaurant
        //                {
        //                    BoatHouseId = r.Field<int>("BoatHouseId"),
        //                    BookingId = r.Field<string>("BookingId"),
        //                    ChargePerItem = r.Field<decimal>("ChargePerItem"),
        //                    NoOfItems = r.Field<int>("NoOfItems"),
        //                    ServiceId = r.Field<int>("ServiceId")
        //                }).ToList();

        //        sQuery = string.Format(@"SELECT CAST(CategoryId AS VARCHAR) AS 'CategoryId', CategoryName, BoatHouseId FROm FoodCategory AS C 
        //                  WHERE C.BoatHouseId = '{0}' ", bhId);
        //        cmd = new SqlCommand(sQuery, con);
        //        da = new SqlDataAdapter(cmd);
        //        da.Fill(dtFoodCategory);
        //        IList<clFoodCategory> clfoodcategories = dtFoodCategory.AsEnumerable().
        //            Select(r =>
        //            new clFoodCategory
        //            {
        //                BoatHouseId = r.Field<int>("BoatHouseId"),
        //                CategoryId = r.Field<string>("CategoryId"),
        //                CategoryName = r.Field<string>("CategoryName"),

        //            }).ToList();


        //        var taxcal = (from cbl in clbookingres
        //                      join fooditem in clfooditems
        //                      on new { cbl.BoatHouseId, cbl.ServiceId } equals new { fooditem.BoatHouseId, fooditem.ServiceId }
        //                      select new TaxCal
        //                      {
        //                          BoatHouseId = fooditem.BoatHouseId,
        //                          BookingId = cbl.BookingId,
        //                          ServiceId = fooditem.ServiceId
        //                      }).ToList();

        //        foreach (var item in taxcal)
        //        {
        //            string taxquery = string.Format(@"select dbo.GetTaxAmountDetails
        //            ('AbsRestaurant', '{0}', '{1}', '{2}')", item.BoatHouseId, item.BookingId, item.ServiceId);

        //            cmd = new SqlCommand(taxquery, con);
        //            var res = cmd.ExecuteScalar();
        //            item.TaxAmt = (decimal)res;
        //        }

        //        var dtTaxcal = ConvertToDataTable(taxcal);

        //        int count = 1;
        //        var reportstru = (from cbl in clbookingres
        //                          join fooditmmstr in clfooditems
        //                            on new { cbl.ServiceId, cbl.BoatHouseId } equals new { fooditmmstr.ServiceId, fooditmmstr.BoatHouseId }
        //                          join foodcat in clfoodcategories
        //                          on new { cbl.BoatHouseId, fooditmmstr.CategoryId } equals new { foodcat.BoatHouseId, foodcat.CategoryId }
        //                          let txamt = taxcal.Where(b => b.BoatHouseId == cbl.BoatHouseId
        //                                  && b.BookingId == cbl.BookingId && b.ServiceId == cbl.ServiceId).Select(t => t.TaxAmt).FirstOrDefault()
        //                          select new CategoryReport
        //                          {
        //                              Sno = count++,
        //                              CategoryId = foodcat.CategoryId,
        //                              CategoryName = foodcat.CategoryName,
        //                              ServiceId = fooditmmstr.ServiceId,
        //                              ServiceName = fooditmmstr.ServiceName,
        //                              ItemRate = fooditmmstr.ServiceTotalAmount,
        //                              Quantity = cbl.NoOfItems,
        //                              Charge = cbl.ChargePerItem * cbl.NoOfItems,
        //                              TaxAmount = txamt,
        //                              Total = txamt + (cbl.ChargePerItem * cbl.NoOfItems),
        //                              OrderValue = 1
        //                          }
        //                    );
        //        var rep = reportstru.OrderBy(o => o.CategoryName).
        //            GroupBy(p => new { p.CategoryId, p.CategoryName, p.ServiceId, p.ServiceName, p.ItemRate }).
        //            Select(rp => new CategoryReport
        //            {
        //                CategoryId = rp.Key.CategoryId,
        //                CategoryName = rp.Key.CategoryName,
        //                ServiceId = rp.Key.ServiceId,
        //                ServiceName = rp.Key.ServiceName,
        //                ItemRate = rp.Key.ItemRate,
        //                Quantity = rp.Sum(a => a.Quantity),
        //                Charge = rp.Sum(a => a.Charge),
        //                TaxAmount = rp.Sum(a => a.TaxAmount),
        //                Total = rp.Sum(a => a.Total),

        //            });
        //        var headers = reportstru.GroupBy(pg => new { pg.CategoryId, pg.CategoryName })
        //            .OrderBy(o => o.Key.CategoryId).Select(st => new CategoryReport
        //            {
        //                CategoryId = st.Key.CategoryId,
        //                ServiceName = st.Key.CategoryName,

        //            });

        //        var Total = reportstru.GroupBy(pg => new { pg.CategoryId, pg.CategoryName })
        //           .OrderBy(o => o.Key.CategoryId).Select(st => new CategoryReport
        //           {
        //               CategoryId = st.Key.CategoryId,
        //               ServiceName = "Total",
        //               Quantity = st.Sum(a => a.Quantity),
        //               Charge = st.Sum(a => a.Charge),
        //               TaxAmount = st.Sum(a => a.TaxAmount),
        //               Total = st.Sum(a => a.Total),

        //           });
        //        var dtTotal = ConvertToDataTable(Total);
        //        var dtRe = ConvertToDataTable(rep);
        //        var dtHeaders = ConvertToDataTable(headers);


        //        int counts = 1;
        //        var infoQuery = (from Header in headers
        //                         select new CategoryReport
        //                         {
        //                             Sno = 0,
        //                             CategoryId = Header.CategoryId,
        //                             CategoryName = Header.CategoryName,
        //                             ServiceId = Header.ServiceId,
        //                             ServiceName = Header.ServiceName,
        //                             ItemRate = Header.ItemRate,
        //                             Quantity = Header.Quantity,
        //                             Charge = Header.Charge,
        //                             TaxAmount = Header.TaxAmount,
        //                             Total = Header.Total,
        //                             OrderValue = 0,
        //                         })
        //                        .Union
        //                            (from owed in rep
        //                             select new CategoryReport
        //                             {
        //                                 Sno = counts++,
        //                                 CategoryId = owed.CategoryId,
        //                                 CategoryName = owed.CategoryName,
        //                                 ServiceId = owed.ServiceId,
        //                                 ServiceName = owed.ServiceName,
        //                                 ItemRate = owed.ItemRate,
        //                                 Quantity = owed.Quantity,
        //                                 Charge = owed.Charge,
        //                                 TaxAmount = owed.TaxAmount,
        //                                 Total = owed.Total,
        //                                 OrderValue = 1,
        //                             })
        //                              .Union(from Totals in Total
        //                                     select new CategoryReport
        //                                     {
        //                                         Sno = 0,
        //                                         CategoryId = Totals.CategoryId,
        //                                         CategoryName = Totals.CategoryName,
        //                                         ServiceId = Totals.ServiceId,
        //                                         ServiceName = Totals.ServiceName,
        //                                         ItemRate = Totals.ItemRate,
        //                                         Quantity = Totals.Quantity,
        //                                         Charge = Totals.Charge,
        //                                         TaxAmount = Totals.TaxAmount,
        //                                         Total = Totals.Total,
        //                                         OrderValue = 2,
        //                                     });
        //        var FinalQuerys = infoQuery.OrderBy(o => o.OrderValue).OrderBy(o => o.CategoryId).
        //Select(st => new CategoryReport
        //{
        //    Sno = st.Sno,
        //    CategoryId = st.CategoryId,
        //    CategoryName = st.CategoryName,
        //    ServiceId = st.ServiceId,
        //    ServiceName = st.ServiceName,
        //    ItemRate = st.ItemRate,
        //    Quantity = st.Quantity,
        //    Charge = st.Charge,
        //    TaxAmount = st.TaxAmount,
        //    Total = st.Total,
        //    OrderValue = st.OrderValue,

        //});

        //        var infoQuerys = ConvertToDataTable(FinalQuerys);
        //        con.Close();

        //        DataSet ds = new DataSet();

        //        ds.Tables.Add(infoQuerys);

        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptResDetailedCatWiseCollection")]
        //public IHttpActionResult RptResDetailedCatWiseCollection([FromBody] BoatingReport RestaurantDetailed)
        //{
        //    try
        //    {
        //        string squery = string.Empty;
        //        string conditions = string.Empty;

        //        if (RestaurantDetailed.CreatedBy != "0")
        //        {
        //            conditions = " A.CreatedBy = '" + RestaurantDetailed.CreatedBy + "' AND ";
        //        }
        //        if (RestaurantDetailed.BoatHouseId != "" && RestaurantDetailed.FromDate != "")
        //        {
        //            squery = "SELECT * FROM "
        //        + " ( "
        //          + " SELECT  '0' as 'SNo',C.CategoryId, '' as CategoryName, '0' AS ServiceId, C.CategoryName AS ServiceName, '0' AS 'ItemRate', "
        //          + " '0' AS 'Quantity', '0' AS 'Charge', "
        //          + "  '0' AS 'TaxAmount', "
        //          + "  '0' AS 'Total', '0' AS 'Ordervalue' "
        //          + "  FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
        //          + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
        //           + " WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
        //           + " AND A.BoatHouseId = '" + RestaurantDetailed.BoatHouseId.Trim() + "'  and CAST(A.BookingDate AS DATE)  BETWEEN ('"
        //           + DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate) + "') "
        //           + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
        //           + "  ) "
        //           + " AS A GROUP BY A.CategoryId,A.Ordervalue,A.CategoryName,A.ServiceId,A.ServiceName,A.ItemRate,A.Quantity,A.TaxAmount,A.Charge,A.Total,A.SNo "
        //          + " UNION ALL "
        //          + " SELECT* FROM "
        //          + " ( "
        //          + " SELECT row_number() over(partition by C.CategoryId order by C.CategoryId) as 'SNo', C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount, 0) AS 'ItemRate', "
        //          + " SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity', (ISNULL(A.ChargePerItem, 0) * SUM(ISNULL(A.NoOfItems, 0))) AS 'Charge', "
        //          + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
        //          + " SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
        //          + "  AS 'Total', 1 AS 'Ordervalue' "
        //          + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
        //          + "  INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
        //          + "  WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
        //          + "  AND A.BoatHouseId = '" + RestaurantDetailed.BoatHouseId.Trim() + "'  and CAST(A.BookingDate AS DATE)  BETWEEN ('"
        //          + DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate) + "') "
        //          + "  GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
        //          + "  )  "
        //          + " AS A "
        //          + " GROUP BY A.CategoryId,A.Ordervalue,A.CategoryName,A.ServiceId,A.ServiceName,A.ItemRate,A.Quantity,A.TaxAmount,A.Charge,A.Total,A.SNo "
        //          + "  UNION ALL "
        //          + "  SELECT* FROM "
        //          + "  ( "
        //          + "  SELECT '0' AS 'SNo',CategoryId, '' AS CategoryName, '' AS ServiceId, 'Total' AS ServiceName, 0 AS 'ItemRate', "
        //          + "  SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity', ISNULL(SUM(ISNULL((A.ChargePerItem), 0) * ISNULL(A.NoOfItems, 0)), 0) AS 'Charge', "
        //          + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
        //          + " SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
        //          + "  AS 'Total', 2 AS 'Ordervalue' "
        //          + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
        //          + "  WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId "
        //          + " AND A.BoatHouseId = '" + RestaurantDetailed.BoatHouseId.Trim() + "'  and CAST(A.BookingDate AS DATE)  BETWEEN ('"
        //          + DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate) + "') "
        //          + "  GROUP BY CategoryId "
        //          + "  )  "
        //          + "  AS B "
        //          + " GROUP BY B.CategoryId,B.Ordervalue,B.CategoryName, Ordervalue,B.ServiceId,B.ServiceName,B.ItemRate,B.Quantity,B.TaxAmount,B.Charge,B.Total,B.SNo "
        //          + "  ORDER BY CategoryId, Ordervalue";

        //            con.Open();
        //            SqlCommand cmd = new SqlCommand(squery, con);
        //            cmd.CommandTimeout = 900000;
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);
        //            con.Close();
        //            return Ok(ds);
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
        //        BoatingReportres ConfRes = new BoatingReportres
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
        /// Modified By Abhinaya K
        /// Modified Date 25-Apr-2023
        ///  Modified For Adding Parameterised Query 
        /// </summary>
        /// <param name="RestaurantDetailed"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptResDetailedCatWiseCollection")]
        public IHttpActionResult RptResDetailedCatWiseCollection([FromBody] BoatingReport RestaurantDetailed)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                string conditions_2 = string.Empty;
                string sFromDate = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "00:00:00".Trim();
                string sToDate = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate).ToString("yyyy-MM-dd") + " " + "23:59:59".Trim();

                if (RestaurantDetailed.CreatedBy != "0")
                {
                    conditions = " A.CreatedBy = @CreatedBy AND ";
                    conditions_2 = " A.CreatedBy = @CreatedBy AND ";
                }
                if (RestaurantDetailed.BoatHouseId != "" && RestaurantDetailed.FromDate != "")
                {
                    if (CheckDate(RestaurantDetailed.FromDate.Trim()) && CheckDate(RestaurantDetailed.ToDate.Trim()))
                    {
                        squery = "SELECT * FROM "
                                + " ( "
                                  + " SELECT  '0' as 'SNo',C.CategoryId, '' as CategoryName, '0' AS ServiceId, C.CategoryName AS ServiceName, '0' AS 'ItemRate', "
                                  + " '0' AS 'Quantity', '0' AS 'Charge', "
                                  + "  '0' AS 'TaxAmount', "
                                  + "  '0' AS 'Total', '0' AS 'Ordervalue' "
                                  + "  FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                  + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                   + " WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                   + " AND A.BoatHouseId = @BoatHouseId  and CAST(A.BookingDate AS DATE)  BETWEEN (@FromDate) AND (@ToDate) "
                                   + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
                                   + "  ) "
                                   + " AS A GROUP BY A.CategoryId,A.Ordervalue,A.CategoryName,A.ServiceId,A.ServiceName,A.ItemRate,A.Quantity,A.TaxAmount,A.Charge,A.Total,A.SNo "
                                  + " UNION ALL "
                                  + " SELECT* FROM "
                                  + " ( "
                                  + " SELECT row_number() over(partition by C.CategoryId order by C.CategoryId) as 'SNo', C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount, 0) AS 'ItemRate', "
                                  + " SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity', (ISNULL(A.ChargePerItem, 0) * SUM(ISNULL(A.NoOfItems, 0))) AS 'Charge', "
                                  + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                  + " SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
                                  + "  AS 'Total', 1 AS 'Ordervalue' "
                                  + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                  + "  INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                  + "  WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                  + "  AND A.BoatHouseId = @BoatHouseId  and CAST(A.BookingDate AS DATE)  BETWEEN (@FromDate) AND (@ToDate) "
                                  + "  GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
                                  + "  )  "
                                  + " AS A "
                                  + " GROUP BY A.CategoryId,A.Ordervalue,A.CategoryName,A.ServiceId,A.ServiceName,A.ItemRate,A.Quantity,A.TaxAmount,A.Charge,A.Total,A.SNo "
                                  + "  UNION ALL "
                                  + "  SELECT* FROM "
                                  + "  ( "
                                  + "  SELECT '0' AS 'SNo',CategoryId, '' AS CategoryName, '' AS ServiceId, 'Total' AS ServiceName, 0 AS 'ItemRate', "
                                  + "  SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity', ISNULL(SUM(ISNULL((A.ChargePerItem), 0) * ISNULL(A.NoOfItems, 0)), 0) AS 'Charge', "
                                  + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                  + " SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
                                  + "  AS 'Total', 2 AS 'Ordervalue' "
                                  + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                  + "  WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId "
                                  + " AND A.BoatHouseId = @BoatHouseId  and CAST(A.BookingDate AS DATE)  BETWEEN (@FromDate) AND (@ToDate) "
                                  + "  GROUP BY CategoryId "
                                  + "  )  "
                                  + "  AS B "
                                  + " GROUP BY B.CategoryId,B.Ordervalue,B.CategoryName, Ordervalue,B.ServiceId,B.ServiceName,B.ItemRate,B.Quantity,B.TaxAmount,B.Charge,B.Total,B.SNo "
                                  + "  ORDER BY CategoryId, Ordervalue";

                    }
                    else if (!CheckDate(RestaurantDetailed.FromDate.Trim()) && !CheckDate(RestaurantDetailed.ToDate.Trim()))
                    {
                        squery = "SELECT * FROM "
                                + " ( "
                                  + " SELECT  '0' as 'SNo',C.CategoryId, '' as CategoryName, '0' AS ServiceId, C.CategoryName AS ServiceName, '0' AS 'ItemRate', "
                                  + " '0' AS 'Quantity', '0' AS 'Charge', "
                                  + "  '0' AS 'TaxAmount', "
                                  + "  '0' AS 'Total', '0' AS 'Ordervalue' "
                                  + "  FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                  + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                   + " WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                   + " AND A.BoatHouseId = @BoatHouseId  and A.BookingDate  BETWEEN (@sFromDate) AND (@sToDate) "
                                   + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
                                   + "  ) "
                                   + " AS A GROUP BY A.CategoryId,A.Ordervalue,A.CategoryName,A.ServiceId,A.ServiceName,A.ItemRate,A.Quantity,A.TaxAmount,A.Charge,A.Total,A.SNo "
                                  + " UNION ALL "
                                  + " SELECT* FROM "
                                  + " ( "
                                  + " SELECT row_number() over(partition by C.CategoryId order by C.CategoryId) as 'SNo', C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount, 0) AS 'ItemRate', "
                                  + " SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity', (ISNULL(A.ChargePerItem, 0) * SUM(ISNULL(A.NoOfItems, 0))) AS 'Charge', "
                                  + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                  + " SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
                                  + "  AS 'Total', 1 AS 'Ordervalue' "
                                  + " FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                  + "  INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                  + "  WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                  + "  AND A.BoatHouseId = @BoatHouseId  and A.BookingDate  BETWEEN (@sFromDate) AND (@sToDate) "
                                  + "  GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
                                  + "  )  "
                                  + " AS A "
                                  + " GROUP BY A.CategoryId,A.Ordervalue,A.CategoryName,A.ServiceId,A.ServiceName,A.ItemRate,A.Quantity,A.TaxAmount,A.Charge,A.Total,A.SNo "
                                  + "  UNION ALL "
                                  + "  SELECT* FROM "
                                  + "  ( "
                                  + "  SELECT '0' AS 'SNo',CategoryId, '' AS CategoryName, '' AS ServiceId, 'Total' AS ServiceName, 0 AS 'ItemRate', "
                                  + "  SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity', ISNULL(SUM(ISNULL((A.ChargePerItem), 0) * ISNULL(A.NoOfItems, 0)), 0) AS 'Charge', "
                                  + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                  + " SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
                                  + "  AS 'Total', 2 AS 'Ordervalue' "
                                  + " FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                  + "  WHERE " + conditions + "  A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId "
                                  + " AND A.BoatHouseId = @BoatHouseId  and A.BookingDate  BETWEEN (@sFromDate) AND (@sToDate) "
                                  + "  GROUP BY CategoryId "
                                  + "  )  "
                                  + "  AS B "
                                  + " GROUP BY B.CategoryId,B.Ordervalue,B.CategoryName, Ordervalue,B.ServiceId,B.ServiceName,B.ItemRate,B.Quantity,B.TaxAmount,B.Charge,B.Total,B.SNo "
                                  + "  ORDER BY CategoryId, Ordervalue";
                    }
                    else
                    {
                        squery = "SELECT "
                                + "SNo,CategoryId,CategoryName,ServiceId,ServiceName, "
                                + "SUM(ItemRate) AS 'ItemRate',SUM(Quantity) AS 'Quantity', "
                                + "SUM(Charge) AS 'Charge',SUM(TaxAmount) AS 'TaxAmount', "
                                + "SUM(Total) AS 'Total',Ordervalue "
                                + "FROM "
                                + "( "
                                + "SELECT "
                                + "'0' as 'SNo', "
                                + "C.CategoryId, '' as 'CategoryName',  "
                                + "'0' AS ServiceId,  "
                                + "C.CategoryName AS 'ServiceName', "
                                + "0 AS 'ItemRate', "
                                + "0 AS 'Quantity',  "
                                + "0 AS 'Charge', "
                                + "0 AS 'TaxAmount', "
                                + "0 AS 'Total', "
                                + "0 AS 'Ordervalue' "
                                + "FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                + "WHERE  "
                                + conditions + ""
                                + "A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                + "AND A.BoatHouseId = @BoatHouseId "
                                + "and CAST(A.BookingDate AS DATE)  BETWEEN (@FromDate) "
                                + "AND (@ToDate) "
                                + "GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "

                                + "UNION ALL  "

                                + "SELECT   "
                                + "'0' as 'SNo', "
                                + "C.CategoryId, "
                                + "'' as 'CategoryName', "
                                + "'0' AS 'ServiceId', "
                                + "C.CategoryName AS 'ServiceName', "
                                + "0 AS 'ItemRate', "
                                + "0 AS 'Quantity',  "
                                + "0 AS 'Charge', "
                                + "0 AS 'TaxAmount', "
                                + "0 AS 'Total', "
                                + "0 AS 'Ordervalue' "
                                + "FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                + "WHERE "
                                + conditions_2 + ""
                                + "A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                + "AND A.BoatHouseId = @BoatHouseId  "
                                + "and A.BookingDate   BETWEEN (@sFromDate) AND (@sToDate) "
                                + "GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
                                + ") AS A "
                                + "GROUP BY SNo,CategoryId,CategoryName,ServiceId,ServiceName,Ordervalue "

                                + "UNION ALL "

                                + "SELECT  "
                                + "ROW_NUMBER() over(partition by CategoryId order by CategoryId) as 'SNo', "
                                + "CategoryId, CategoryName, ServiceId, ServiceName, "
                                + "SUM(ItemRate) AS 'ItemRate',SUM(Quantity) AS 'Quantity',SUM(Charge) AS 'Charge', "
                                + "SUM(TaxAmount) AS 'TaxAmount',SUM(Total) AS 'Total',Ordervalue "
                                + "FROM  "
                                + "( "
                                + "SELECT "
                                + "C.CategoryId, "
                                + "C.CategoryName, "
                                + "B.ServiceId, "
                                + "B.ServiceName,  "
                                + "ISNULL(B.ServiceTotalAmount, 0) AS 'ItemRate', "
                                + "SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity',  "
                                + "(ISNULL(A.ChargePerItem, 0) * SUM(ISNULL(A.NoOfItems, 0))) AS 'Charge', "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                + "SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) +  "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'Total', "
                                + "1 AS 'Ordervalue' "
                                + "FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                + "WHERE  "
                                + conditions + ""
                                + "A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                + "AND A.BoatHouseId = @BoatHouseId  "
                                + "and CAST(A.BookingDate AS DATE)  BETWEEN (@FromDate)  "
                                + "AND (@ToDate) "
                                + "GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "

                                + "UNION ALL "

                                + "SELECT "
                                + "C.CategoryId, "
                                + "C.CategoryName,  "
                                + "B.ServiceId, "
                                + "B.ServiceName,  "
                                + "ISNULL(B.ServiceTotalAmount, 0) AS 'ItemRate', "
                                + "SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity',  "
                                + "(ISNULL(A.ChargePerItem, 0) * SUM(ISNULL(A.NoOfItems, 0))) AS 'Charge', "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                + "SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) +  "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
                                + "AS 'Total', 1 AS 'Ordervalue' "
                                + "FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId "
                                + "WHERE  "
                                + conditions_2 + ""
                                + "A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                + "AND A.BoatHouseId = @BoatHouseId  "
                                + "and A.BookingDate  BETWEEN (@sFromDate) AND (@sToDate) "
                                + "GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
                                + ") AS A "
                                + "GROUP BY A.CategoryId,A.Ordervalue,A.CategoryName,A.ServiceId,A.ServiceName "

                                + "UNION ALL "

                                + "SELECT  "
                                + "SNo, "
                                + "CategoryId, "
                                + "CategoryName, "
                                + "ServiceId, "
                                + "ServiceName, "
                                + "SUM(ItemRate) AS 'ItemRate', "
                                + "SUM(Quantity) AS 'Quantity', "
                                + "SUM(Charge) AS 'Charge', "
                                + "SUM(TaxAmount) AS 'TaxAmount', "
                                + "SUM(Total) AS 'Total', "
                                + "Ordervalue "
                                + "FROM "
                                + "( "
                                + "SELECT "
                                + "'0' AS 'SNo', "
                                + "CategoryId,  "
                                + "'' AS 'CategoryName', "
                                + "'' AS 'ServiceId', "
                                + "'Total' AS 'ServiceName', "
                                + "0 AS 'ItemRate', "
                                + "SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity', "
                                + "ISNULL(SUM(ISNULL((A.ChargePerItem), 0) * ISNULL(A.NoOfItems, 0)), 0) AS 'Charge', "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                + "SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'Total',  "
                                + "2 AS 'Ordervalue' "
                                + "FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                + "WHERE  "
                                + conditions + ""
                                + "A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId "
                                + "AND A.BoatHouseId = @BoatHouseId "
                                + "and CAST(A.BookingDate AS DATE)  BETWEEN (@FromDate)"
                                + "AND (@ToDate) "
                                + "GROUP BY CategoryId "

                                + "UNION ALL "

                                + "SELECT  "
                                + "'0' AS 'SNo', "
                                + "CategoryId, '' AS 'CategoryName', "
                                + "'' AS ServiceId,  "
                                + "'Total' AS 'ServiceName', "
                                + "0 AS 'ItemRate', "
                                + "SUM(ISNULL(A.NoOfItems, 0)) AS 'Quantity',  "
                                + "ISNULL(SUM(ISNULL((A.ChargePerItem), 0) * ISNULL(A.NoOfItems, 0)), 0) AS 'Charge', "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) AS 'TaxAmount', "
                                + "SUM(ISNULL(A.NoOfItems, 0) * ISNULL(A.ChargePerItem, 0)) + "
                                + "SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId), 0)) "
                                + "AS 'Total', 2 AS 'Ordervalue' "
                                + "FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                                + "WHERE "
                                + conditions_2 + ""
                                + "A.BStatus = 'B'  AND A.BoatHouseId = B.BoatHouseId "
                                + "AND A.BoatHouseId = @BoatHouseId  "
                                + "and A.BookingDate BETWEEN (@sFromDate) AND (@sToDate) "
                                + "GROUP BY CategoryId "
                                + ")  "
                                + "AS B "
                                + "GROUP BY "
                                + "SNo,CategoryId,CategoryName,ServiceId,ServiceName,Ordervalue "
                                + "ORDER BY CategoryId, Ordervalue ";
                    }

                    con.Open();
                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@sFromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@sToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = RestaurantDetailed.BoatHouseId.Trim();
                    cmd.Parameters["@CreatedBy"].Value = RestaurantDetailed.CreatedBy.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(RestaurantDetailed.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(RestaurantDetailed.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@sFromDate"].Value = sFromDate;
                    cmd.Parameters["@sToDate"].Value = sToDate;

                    cmd.CommandTimeout = 900000;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                    return Ok(ds);
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

    }
}
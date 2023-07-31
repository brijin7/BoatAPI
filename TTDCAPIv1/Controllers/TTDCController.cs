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

    public class TTDCController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_BoatHouse"].ConnectionString);
        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

        /***********************************Dropdown**************************************/



        //Dropdown Role


        //Dropdown Employee Name
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlEmpName")]
        public IHttpActionResult ddlEmpName()
        {
            try
            {
                List<EmployeeMaster> li = new List<EmployeeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmpID, CONCAT(EmpFirstName, ' ', EmpLastName) AS Name  FROM EmpMaster where ActiveStatus='A';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EmployeeMaster ShowEmpMstr = new EmployeeMaster();
                        ShowEmpMstr.EmpId = dt.Rows[i]["EmpId"].ToString();
                        ShowEmpMstr.EmpName = dt.Rows[i]["Name"].ToString();
                        li.Add(ShowEmpMstr);
                    }

                    EmployeeMasterList ConfList = new EmployeeMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    EmployeeMasterRes ConfRes = new EmployeeMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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

        //Dropdown Employee Name
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlEmpBoatHouse")]
        public IHttpActionResult ddlEmpBoatHouse()
        {
            try
            {
                List<EmployeeMaster> li = new List<EmployeeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmpID, CONCAT(EmpFirstName, ' ', EmpLastName) AS Name  FROM EmpMaster where ActiveStatus='A' AND RoleId IN (1);", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EmployeeMaster ShowEmpMstr = new EmployeeMaster();
                        ShowEmpMstr.EmpId = dt.Rows[i]["EmpId"].ToString();
                        ShowEmpMstr.EmpName = dt.Rows[i]["Name"].ToString();
                        li.Add(ShowEmpMstr);
                    }

                    EmployeeMasterList ConfList = new EmployeeMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    EmployeeMasterRes ConfRes = new EmployeeMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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


        //Dropdown Boat Type
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlBoatType")]
        public IHttpActionResult ddlBoatType()
        {
            try
            {
                List<BoatTypeMaster> li = new List<BoatTypeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select BoatTypeId , BoatType from BoatTypes where ActiveStatus='A';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTypeMaster ShowBoatTypes = new BoatTypeMaster();
                        ShowBoatTypes.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        ShowBoatTypes.BoatType = dt.Rows[i]["BoatType"].ToString();

                        li.Add(ShowBoatTypes);
                    }

                    BoatTypeMasterList ConfList = new BoatTypeMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    BoatTypeMasterString ConfRes = new BoatTypeMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                BoatTypeMasterString ConfRes = new BoatTypeMasterString
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




        //Dropdown Boat Seater
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlBoatSeater")]
        public IHttpActionResult ddlBoatSeater()
        {
            try
            {
                List<BoatSeatMaster> li = new List<BoatSeatMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select BoatSeaterId , SeaterType from BoatSeat where ActiveStatus='A';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatSeatMaster ShowBoatSeater = new BoatSeatMaster();
                        ShowBoatSeater.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        ShowBoatSeater.SeaterType = dt.Rows[i]["SeaterType"].ToString();

                        li.Add(ShowBoatSeater);
                    }

                    BoatSeatMasterList ConfList = new BoatSeatMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    BoatSeatMasterString ConfRes = new BoatSeatMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                BoatSeatMasterString ConfRes = new BoatSeatMasterString
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



        //Dropdown Boat Type
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlBoatMaster")]
        public IHttpActionResult ddlBoatMaster()
        {
            try
            {
                List<BoatMstr> li = new List<BoatMstr>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT BoatId, BoatName, BoatNum FROM BoatMaster WHERE BoatStatus = 1;", con);
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
            catch (Exception ex)
            {
                BoatMsrString ConfRes = new BoatMsrString
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




        //Tax Id Based on Boat House Id

        [HttpPost]
        [AllowAnonymous]
        [Route("BoatHouseTaxId/BHID")]
        public IHttpActionResult BoatHouseTaxId([FromBody] BoatSeatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null)
                {
                    List<BoatHouseMaster> li = new List<BoatHouseMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TaxId FROM BHMaster WHERE BoatHouseId= @BoatHouseId ;", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatHouseMaster ShowBoathouseMstr = new BoatHouseMaster();
                            ShowBoathouseMstr.TaxId = Convert.ToInt32(dt.Rows[i]["TaxId"].ToString());
                            li.Add(ShowBoathouseMstr);
                        }

                        BoatHouseMasterList ConfList = new BoatHouseMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BoatHouseMasterString ConfRes = new BoatHouseMasterString
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
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }

        //Dropdown Tax master

        [HttpPost]
        [AllowAnonymous]
        [Route("ddlTaxMaster")]
        public IHttpActionResult ddlTaxMaster([FromBody] TaxMaster Ins)
        {
            try
            {
                List<TaxMaster> li = new List<TaxMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select TaxID, TaxName from TaxMaster where ActiveStatus='A';", con);
                cmd.Parameters.AddWithValue("@BoatHouseId", Ins.BoatHouseId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TaxMaster ShowTaxMaster = new TaxMaster();
                        ShowTaxMaster.TaxId = dt.Rows[i]["TaxID"].ToString();
                        ShowTaxMaster.TaxName = dt.Rows[i]["TaxName"].ToString();

                        li.Add(ShowTaxMaster);
                    }

                    TaxMasterList ConfList = new TaxMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    TaxMasterRes ConfRes = new TaxMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                TaxMasterRes ConfRes = new TaxMasterRes
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

        //Dropdown Offer

        [HttpGet]
        [AllowAnonymous]
        [Route("ddlOffer")]
        public IHttpActionResult ddlOffer()
        {
            try
            {
                List<OfferMaster> li = new List<OfferMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT OfferId, OfferName FROM OfferMaster WHERE ActiveStatus = 'A';", con);
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
            catch (Exception ex)
            {
                OfferMasterString ConfRes = new OfferMasterString
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


        //Dropdown Offer based on BHId




        [HttpGet]
        [AllowAnonymous]
        [Route("ddlItemMstr")]
        public IHttpActionResult ddlItemMstr()
        {
            try
            {
                List<ItemMaster> li = new List<ItemMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ItemId,ItemDescription from ItemMaster Where ActiveStatus='A'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ItemMaster ShowItemType = new ItemMaster();
                        ShowItemType.ItemId = dt.Rows[i]["ItemId"].ToString();
                        ShowItemType.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();
                        li.Add(ShowItemType);
                    }

                    ItemMasterList ConfList = new ItemMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ItemMasterString ConfRes = new ItemMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                ItemMasterString ConfRes = new ItemMasterString
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


        //Dropdown Reason for Resched based on BHId

        [HttpPost]
        [AllowAnonymous]
        [Route("ddlReasonResched/BHId")]
        public IHttpActionResult ddlReasonReschedBhId([FromBody] ReasonMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null)
                {
                    List<ReasonMaster> li = new List<ReasonMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ActivityId AS 'ReasonId', Description AS 'ReasonName' FROM CancelReschedMstr "
                                    + " WHERE ActivityType = 'R' AND BoatHouseId = @BoatHouseId;", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ReasonMaster ShowTaxMaster = new ReasonMaster();
                            ShowTaxMaster.ReasonId = Convert.ToInt32(dt.Rows[i]["ReasonId"].ToString());
                            ShowTaxMaster.ReasonName = dt.Rows[i]["ReasonName"].ToString();

                            li.Add(ShowTaxMaster);
                        }

                        ReasonMasterList ConfList = new ReasonMasterList
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
                    OfferMasterString Vehicle = new OfferMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                OfferMasterString Vehicle = new OfferMasterString
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }


        //Dropdown Reason for Resched based on BHId

        [HttpPost]
        [AllowAnonymous]
        [Route("ddlReasonCancel/BHId")]
        public IHttpActionResult ddlReasonCancelBhId([FromBody] ReasonMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null)
                {
                    List<ReasonMaster> li = new List<ReasonMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ActivityId AS 'ReasonId', Description AS 'ReasonName' FROM CancelReschedMstr "
                                    + " WHERE ActivityType = 'C' AND BoatHouseId = @BoatHouseId;", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ReasonMaster ShowTaxMaster = new ReasonMaster();
                            ShowTaxMaster.ReasonId = Convert.ToInt32(dt.Rows[i]["ReasonId"].ToString());
                            ShowTaxMaster.ReasonName = dt.Rows[i]["ReasonName"].ToString();

                            li.Add(ShowTaxMaster);
                        }

                        ReasonMasterList ConfList = new ReasonMasterList
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
                    OfferMasterString Vehicle = new OfferMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                OfferMasterString Vehicle = new OfferMasterString
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }


        // Get State
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLState")]
        public IHttpActionResult GetState()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                SqlCommand cmd = new SqlCommand("SELECT ConfigId, ConfigName FROM ConfigurationMaster WHERE ActiveStatus = 'A' AND TypeId = '1' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster getConfigType = new ConfigurationMaster();
                        getConfigType.ConfigId = dt.Rows[i]["ConfigId"].ToString();
                        getConfigType.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        li.Add(getConfigType);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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

        // Get Zone
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLZone")]
        public IHttpActionResult GetZone()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                SqlCommand cmd = new SqlCommand("SELECT ConfigId, ConfigName FROM ConfigurationMaster WHERE ActiveStatus = 'A' AND TypeId = '2' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster getConfigType = new ConfigurationMaster();
                        getConfigType.ConfigId = dt.Rows[i]["ConfigId"].ToString();
                        getConfigType.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        li.Add(getConfigType);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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

        // Get District
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLDistrict")]
        public IHttpActionResult GetDistrict()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                SqlCommand cmd = new SqlCommand("SELECT ConfigId, ConfigName FROM ConfigurationMaster WHERE ActiveStatus = 'A' AND TypeId = '3' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster getConfigType = new ConfigurationMaster();
                        getConfigType.ConfigId = dt.Rows[i]["ConfigId"].ToString();
                        getConfigType.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        li.Add(getConfigType);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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

        // Get City
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLCity")]
        public IHttpActionResult GetCity()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                SqlCommand cmd = new SqlCommand("SELECT ConfigId, ConfigName FROM ConfigurationMaster WHERE ActiveStatus = 'A' AND TypeId = '4' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster getConfigType = new ConfigurationMaster();
                        getConfigType.ConfigId = dt.Rows[i]["ConfigId"].ToString();
                        getConfigType.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        li.Add(getConfigType);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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

        // Get Attraction
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLAttraction")]
        public IHttpActionResult GetAttraction()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                SqlCommand cmd = new SqlCommand("SELECT ConfigId, ConfigName FROM ConfigurationMaster WHERE ActiveStatus = 'A' AND TypeId = '5' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster getConfigType = new ConfigurationMaster();
                        getConfigType.ConfigId = dt.Rows[i]["ConfigId"].ToString();
                        getConfigType.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        li.Add(getConfigType);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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


        [HttpGet]
        [AllowAnonymous]
        [Route("ddlGetLocation")]
        public IHttpActionResult GetLocation()
        {
            try
            {
                List<LocationMaster> li = new List<LocationMaster>();
                SqlCommand cmd = new SqlCommand("SELECT LocationId, LocationName FROM LocationMaster WHERE ActiveStatus = 'A'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LocationMaster getlocation = new LocationMaster();
                        getlocation.LocationId = Convert.ToInt32(dt.Rows[i]["LocationId"].ToString());
                        getlocation.LocationName = dt.Rows[i]["LocationName"].ToString();
                        li.Add(getlocation);
                    }

                    LocationMasterList ConfList = new LocationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    LocationMasterRes ConfRes = new LocationMasterRes
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

        [HttpGet]
        [AllowAnonymous]
        [Route("ddlContactType")]
        public IHttpActionResult ddlContactType()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ConfigID,ConfigName from ConfigurationMaster where TypeID='6' AND ActiveStatus='A';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowContactType = new ConfigurationMaster();
                        ShowContactType.ConfigId = dt.Rows[i]["ConfigID"].ToString();
                        ShowContactType.ConfigName = dt.Rows[i]["ConfigName"].ToString();

                        li.Add(ShowContactType);
                    }

                    ConfigurationMasterList ContactType = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ContactType);
                }

                else
                {
                    ConfigurationMasterRes ContactType = new ConfigurationMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ContactType);
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


        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/ddlEventType")]
        public IHttpActionResult ddlEventType()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ConfigID,ConfigName from ConfigurationMaster where TypeID='7' AND ActiveStatus='A';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowEventType = new ConfigurationMaster();
                        ShowEventType.ConfigId = dt.Rows[i]["ConfigID"].ToString();
                        ShowEventType.ConfigName = dt.Rows[i]["ConfigName"].ToString();

                        li.Add(ShowEventType);
                    }

                    ConfigurationMasterList EventType = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(EventType);
                }

                else
                {
                    ConfigurationMasterRes EventType = new ConfigurationMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(EventType);
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

        [HttpGet]
        [AllowAnonymous]
        [Route("ddlCulturalEvents")]
        public IHttpActionResult ddlCulturalEvents()
        {
            try
            {
                List<CulturalEvents> li = new List<CulturalEvents>();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select EventId,EventName from CulturalEvents Where ActiveStatus='A';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CulturalEvents ShowEventType = new CulturalEvents();
                        ShowEventType.EventId = dt.Rows[i]["EventId"].ToString();
                        ShowEventType.EventName = dt.Rows[i]["EventName"].ToString();

                        li.Add(ShowEventType);
                    }

                    CulturalEventsList EventType = new CulturalEventsList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(EventType);
                }

                else
                {
                    CulturalEventsRes EventType = new CulturalEventsRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(EventType);
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

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/ImpLinks")]
        public IHttpActionResult getImpLinks()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId = 12", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowConfMstr = new ConfigurationMaster();
                        ShowConfMstr.ConfigId = dt.Rows[i]["ConfigID"].ToString();
                        ShowConfMstr.ConfigName = dt.Rows[i]["ConfigName"].ToString();

                        li.Add(ShowConfMstr);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLEnquiry")]
        public IHttpActionResult getEnquiry()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId=25", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowConfMstr = new ConfigurationMaster();
                        ShowConfMstr.ConfigId = dt.Rows[i]["ConfigID"].ToString();
                        ShowConfMstr.ConfigName = dt.Rows[i]["ConfigName"].ToString();

                        li.Add(ShowConfMstr);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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

        [HttpGet]
        [AllowAnonymous]
        [Route("ddlCompanyMaster")]
        public IHttpActionResult ddlCompanyMaster()
        {
            try
            {
                List<CompanyMaster> li = new List<CompanyMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select CorpID,CorpName from CompanyMaster", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CompanyMaster ShowCompanyMaster = new CompanyMaster();
                        ShowCompanyMaster.CorpID = dt.Rows[i]["CorpID"].ToString();
                        ShowCompanyMaster.CorpName = dt.Rows[i]["CorpName"].ToString();

                        li.Add(ShowCompanyMaster);
                    }

                    CompanyMasterList ContactType = new CompanyMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ContactType);
                }

                else
                {
                    CompanyMasterRes ContactType = new CompanyMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ContactType);
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


        //Dropdown Department based on Modules

        [HttpGet]
        [AllowAnonymous]
        [Route("ddlDeptAllModules")]
        public IHttpActionResult ddlDeptAllModules()
        {
            try
            {
                List<DepartmentModules> li = new List<DepartmentModules>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" SELECT DISTINCT (A.Department), B.ConfigName AS 'DeptName' FROM DeptDesgMap AS A "
                    + " INNER JOIN ConfigurationMaster AS B ON A.Department = B.ConfigID AND A.ActiveStatus = B.ActiveStatus AND B.TypeID = '19' "
                    + " WHERE A.ActiveStatus = 'A' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DepartmentModules ShowTaxMaster = new DepartmentModules();
                        ShowTaxMaster.Id = dt.Rows[i]["Department"].ToString();
                        ShowTaxMaster.Name = dt.Rows[i]["DeptName"].ToString();

                        li.Add(ShowTaxMaster);
                    }

                    DepartmentModulesList ConfList = new DepartmentModulesList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    DepartmentModulesRes ConfRes = new DepartmentModulesRes
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

        [HttpGet]
        [AllowAnonymous]
        [Route("ddlTaxMasterListAll")]
        public IHttpActionResult ddlTaxMasterListAll([FromBody] TaxMaster Ins)
        {
            try
            {
                List<TaxMaster> li = new List<TaxMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" SELECT TaxId, STUFF((SELECT ', ' + CAST(TaxDescription AS VARCHAR(20)) + '-' + CAST(TaxPercentage AS VARCHAR(20)) [text()] "
                            + " FROM TaxMaster WHERE TaxId = t.TaxId FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ' ') TaxDescription "
                            + " FROM TaxMaster t WHERE t.ActiveStatus = 'A' AND BoatHouseId=@BoatHouseId GROUP BY TaxId", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@BoatHouseId", Ins.BoatHouseId);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TaxMaster Tax1 = new TaxMaster();
                        Tax1.TaxId = dt.Rows[i]["TaxId"].ToString();
                        Tax1.TaxName = dt.Rows[i]["TaxDescription"].ToString();
                        li.Add(Tax1);
                    }

                    TaxMasterList Tax = new TaxMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Tax);
                }

                else
                {
                    TaxMasterRes Tax = new TaxMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(Tax);
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
        //[Route("MongoImageAPI")]
        //public IHttpActionResult MongoImageAPI()
        //{
        //    var QueryType = HttpContext.Current.Request.Params["QueryType"];
        //    var BoatHouseId = HttpContext.Current.Request.Params["BoatHouseId"];
        //    var ImageLink = HttpContext.Current.Request.Files["ImageLink"];
        //    var FormName = HttpContext.Current.Request.Params["FormName"];
        //    var PrevImageLink = HttpContext.Current.Request.Params["PrevImageLink"];

        //    if (ImageLink != null && ImageLink.ContentLength > 0)
        //    {
        //        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        //        var ext = ImageLink.FileName.Substring(ImageLink.FileName.LastIndexOf('.'));
        //        var extension = ext.ToLower();
        //        if (!AllowedFileExtensions.Contains(extension))
        //        {
        //            ImageUploadRes ImgData = new ImageUploadRes
        //            {
        //                Response = "Please Upload image of type .jpg, .png, .jpeg",
        //                StatusCode = 0
        //            };
        //            return Ok(ImgData);
        //        }
        //        else
        //        {
        //            int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

        //            if (ImageLink.ContentLength > MaxContentLength)
        //            {
        //                ImageUploadRes ImgData = new ImageUploadRes
        //                {
        //                    Response = "Please Upload a file upto 1 mb.",
        //                    StatusCode = 0
        //                };
        //                return Ok(ImgData);
        //            }
        //            else
        //            {
        //var server = MongoClient.Get("mongodb://localhost:27020");
        //var database = server.GetDatabase("tesdb");

        //var fileName = "D:\\Untitled.png";
        //var newFileName = "D:\\new_Untitled.png";
        //using (var fs = new FileStream(fileName, FileMode.Open))
        //{
        //    var gridFsInfo = database.GridFS.Upload(fs, fileName);
        //    var fileId = gridFsInfo.Id;

        //    ObjectId oid = new ObjectId(fileId);
        //    var file = database.GridFS.FindOne(Query.EQ("_id", oid));

        //    using (var stream = file.OpenRead())
        //    {
        //        var bytes = new byte[stream.Length];
        //        stream.Read(bytes, 0, (int)stream.Length);
        //        using (var newFs = new FileStream(newFileName, FileMode.Create))
        //        {
        //            newFs.Write(bytes, 0, bytes.Length);
        //        }
        //    }
        //}

        //string tString = System.DateTime.Now.ToString("yyyyMMddHHmmssss");
        //Random generator = new Random();
        //String rString = generator.Next(0, 999999).ToString("D6");
        //string NewFileName = tString.Trim() + "" + rString.Trim() + extension;

        //var filePath = HttpContext.Current.Server.MapPath("~/Document/" + BoatHouseId + "_" + FormName + "_" + NewFileName);
        //var StorePath = ConfigurationManager.AppSettings["ImageUrl"] + BoatHouseId + "_" + FormName + "_" + NewFileName;


        //if (QueryType == "Insert")
        //{
        //    ImageLink.SaveAs(filePath);
        //}
        //else
        //{
        //    string filename = string.Empty;
        //    filename = Path.GetFileName(PrevImageLink);

        //    if (File.Exists(HttpContext.Current.Server.MapPath("~/Document/" + filename)))
        //    {
        //        File.Delete(HttpContext.Current.Server.MapPath("~/Document/" + filename));
        //        ImageLink.SaveAs(filePath);
        //    }
        //    else
        //    {
        //        ImageUploadRes ImgData = new ImageUploadRes
        //        {
        //            Response = "Please pass Previous Image Link.",
        //            StatusCode = 0
        //        };
        //        return Ok(ImgData);

        //    }

        //}
        //ImageUploadRes ConMstr = new ImageUploadRes
        //{
        //    Response = StorePath,
        //    StatusCode = 1
        //};
        //return Ok(ConMstr);
        //            }
        //        }

        //    }

        //    else
        //    {
        //        ImageUploadRes ImgData = new ImageUploadRes
        //        {
        //            Response = "Please Upload Image file.",
        //            StatusCode = 0
        //        };
        //        return Ok(ImgData);
        //    }
        //}


        /**********************************User Profile***********************************/




        [HttpPost]
        [AllowAnonymous]
        [Route("publicUserByMobNo")]
        public IHttpActionResult GetUserDataByMobileNo([FromBody] UserLogin Login)
        {
            try
            {
                List<UserLogin> li = new List<UserLogin>();
                con.Open();
                SqlCommand cmd = new SqlCommand("  SELECT UserId, UserName, FirstName, LastName, Address1, Address2, Zipcode, City, District, State, "
                    + "  MobileNo, MailId, RewardUtilized, PromoNotification, ActiveStatus FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.PublicProfile WHERE MobileNo = @MobileNo ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@MobileNo", System.Data.SqlDbType.NVarChar, 10));
                cmd.Parameters["@MobileNo"].Value = Login.MobileNo.Trim();

                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UserLogin UserD = new UserLogin();

                        UserD.UserId = dt.Rows[i]["UserId"].ToString();
                        UserD.UserName = dt.Rows[i]["UserName"].ToString();
                        UserD.FirstName = dt.Rows[i]["FirstName"].ToString();
                        UserD.LastName = dt.Rows[i]["LastName"].ToString();
                        UserD.Address1 = dt.Rows[i]["Address1"].ToString();
                        UserD.Address2 = dt.Rows[i]["Address2"].ToString();
                        UserD.Zipcode = dt.Rows[i]["Zipcode"].ToString();
                        UserD.City = dt.Rows[i]["City"].ToString();
                        UserD.District = dt.Rows[i]["District"].ToString();
                        UserD.State = dt.Rows[i]["State"].ToString();
                        UserD.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        UserD.EmailId = dt.Rows[i]["MailId"].ToString();
                        UserD.RewardUtilized = dt.Rows[i]["RewardUtilized"].ToString();
                        UserD.PromoNotification = dt.Rows[i]["PromoNotification"].ToString();
                        UserD.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(UserD);
                    }

                    UserLoginList UserList = new UserLoginList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(UserList);
                }

                else
                {
                    UserLoginRes UserRes = new UserLoginRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(UserRes);
                }
            }
            catch (Exception ex)
            {
                UserLoginRes UserRes = new UserLoginRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(UserRes);
            }
        }


        /**************************Module Access Rights*****************************************/


        //List Admin Access for Employee
        [HttpPost]
        [AllowAnonymous]
        [Route("AdminAccess/ListAll/Emp")]
        public IHttpActionResult AdminAccessEmpList([FromBody] AdminAccess UserAcc)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(UserAcc.UserId) != "")
                {
                    sCondition = " AND A.UserId = @UserId";
                }
                else if (Convert.ToString(UserAcc.UserRole) != "")
                {
                    sCondition = " AND A.UserRole = @UserRole";
                }
                else if (Convert.ToString(UserAcc.UserId) != "" && Convert.ToString(UserAcc.UserRole) != "")
                {
                    sCondition = " AND A.UserRole = @UserRole AND A.UserId = @UserId ";
                }

                sQuery = " SELECT A.UniqueId, A.UserId, CONCAT(B.EmpFirstName, ' ', B.EmpLastName) AS 'Name', B.UserName, A.UserRole, C.ConfigName 'RoleName', "
                    + " A.MMaster, A.MBS, A.MTMS, A.MHMS, A.MAccounts, A.MComMaster, A.MBhMaster, A.MHotelMaster, A.MTourMaster, A.MAccessRights, A.MOtherMaster, "
                    + " A.BMaster, A.BTransaction, A.BBooking, A.BReports, A.BRestaurant, A.BoatHouseId, A.BoatHouseName "
                    + " FROM UserAccessRights AS A "
                    + " INNER JOIN EmpMaster AS B ON A.UserId = B.EmpID AND B.ActiveStatus = 'A' "
                    + " INNER JOIN ConfigurationMaster AS C ON A.UserRole = C.ConfigID AND C.ActiveStatus = 'A' AND C.TypeID = 21 "
                    + " WHERE B.RoleId IN (1,2) ";

                sQuery = sQuery + sCondition;

                List<AdminAccess> li = new List<AdminAccess>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserRole", System.Data.SqlDbType.Int));
                cmd.Parameters["@UserId"].Value = UserAcc.UserId.Trim();
                cmd.Parameters["@UserRole"].Value = UserAcc.UserRole.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AdminAccess ShowConfMstr = new AdminAccess();
                        ShowConfMstr.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
                        ShowConfMstr.Name = dt.Rows[i]["Name"].ToString();
                        ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
                        ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
                        ShowConfMstr.RoleName = dt.Rows[i]["RoleName"].ToString();
                        ShowConfMstr.MMaster = dt.Rows[i]["MMaster"].ToString();
                        ShowConfMstr.MBS = dt.Rows[i]["MBS"].ToString();
                        ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
                        ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
                        ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
                        ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
                        ShowConfMstr.MAccounts = dt.Rows[i]["MAccounts"].ToString();

                        ShowConfMstr.MComMaster = dt.Rows[i]["MComMaster"].ToString();
                        ShowConfMstr.MBhMaster = dt.Rows[i]["MBhMaster"].ToString();
                        ShowConfMstr.MHotelMaster = dt.Rows[i]["MHotelMaster"].ToString();
                        ShowConfMstr.MTourMaster = dt.Rows[i]["MTourMaster"].ToString();
                        ShowConfMstr.MAccessRights = dt.Rows[i]["MAccessRights"].ToString();
                        ShowConfMstr.MOtherMaster = dt.Rows[i]["MOtherMaster"].ToString();

                        ShowConfMstr.BMaster = dt.Rows[i]["BMaster"].ToString();
                        ShowConfMstr.BTransaction = dt.Rows[i]["BTransaction"].ToString();
                        ShowConfMstr.BBooking = dt.Rows[i]["BBooking"].ToString();
                        ShowConfMstr.BReports = dt.Rows[i]["BReports"].ToString();
                        ShowConfMstr.BRestaurant = dt.Rows[i]["BRestaurant"].ToString();

                        ShowConfMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowConfMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        li.Add(ShowConfMstr);
                    }

                    AdminAccessList ConfList = new AdminAccessList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    AdminAccessRes ConfRes = new AdminAccessRes
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




        //Get Details based on Employee Id
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlEmpUser/Emp")]
        public IHttpActionResult ConfigMstrDetonType([FromBody] EmployeeMaster EmpMstr)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(EmpMstr.EmpId) != "")
                {
                    sCondition = " AND EmpID = @EmpId";
                }

                sQuery = " SELECT RoleId, BoatHouseId, BoatHouseName FROM EmpMaster WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId";

                sQuery = sQuery + sCondition;

                List<EmployeeMaster> li = new List<EmployeeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);


                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@EmpId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = EmpMstr.BoatHouseId.Trim();
                cmd.Parameters["@EmpId"].Value = EmpMstr.EmpId.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EmployeeMaster ShowConfMstr = new EmployeeMaster();
                        ShowConfMstr.RoleId = dt.Rows[i]["RoleId"].ToString();
                        ShowConfMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowConfMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        li.Add(ShowConfMstr);
                    }

                    EmployeeMasterList ConfList = new EmployeeMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    EmployeeMasterRes ConfRes = new EmployeeMasterRes
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

        //List Admin Access for Dept User

        /// <summary>
        /// Modified By : M Vinitha
        /// Modified Date : 22-09-2021
        /// Modified By : Brijin A
        /// Modified Date : 26-04-2023
        /// </summary>
        /// <param name="UserAcc"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("AdminAccess/ListAll/User")]
        public IHttpActionResult AdminAccessUserList([FromBody] AdminAccess UserAcc)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(UserAcc.UserId) != "")
                {
                    sCondition = " AND A.UserId = @UserId ";
                }
                else if (Convert.ToString(UserAcc.UserRole) != "")
                {
                    sCondition = " AND A.UserRole = @UserRole ";
                }
                else if (Convert.ToString(UserAcc.UserId) != "" && Convert.ToString(UserAcc.UserRole) != "")
                {
                    sCondition = " AND A.UserRole = @UserRole AND A.UserId = @UserId ";
                }

                sQuery = " SELECT A.UniqueId, A.UserId, CONCAT(B.EmpFirstName, ' ', B.EmpLastName) AS 'Name', B.UserName, A.UserRole, C.ConfigName 'RoleName', "
                    + " A.MMaster, A.MBS, A.MTMS, A.MHMS, A.MAccounts, A.BMaster, A.BTransaction, A.BBooking, A.BReports, A.BRestaurant, A.BoatHouseId, A.BoatHouseName, "
                    + " A.BBMBooking, A.BBMBookingOthers, A.BBMBulkBooking, A.BBMOtherService, A.BBMBulkOtherService, A.BBMCancellation, A.BBMReScheduling, A.BTMMaterialPurchase, "
                    + " A.BTMMaterialIssue, A.BTMTripSheetSettle, A.BTMRowerSettle, A.BTMFoodStockEntryMaintance, A.BRMBooking, A.BRMOtherService, A.BRMTripSheet, A.BRMRowerSettle, A.BRMAbstractBoatBook , "
                    + " A.OfflineRights "
                    + " FROM UserAccessRights AS A "
                    + " INNER JOIN EmpMaster AS B ON A.UserId = B.EmpID AND B.ActiveStatus = 'A' "
                    + " INNER JOIN ConfigurationMaster AS C ON A.UserRole = C.ConfigID AND C.ActiveStatus = 'A' AND C.TypeID = 21 "
                    + " WHERE B.RoleId IN (3) AND B.BoatHouseId = @BoatHouseId AND A.BoatHouseId = @BoatHouseId";

                sQuery = sQuery + sCondition;

                List<AdminAccess> li = new List<AdminAccess>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserRole", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = UserAcc.BoatHouseId.Trim();
                cmd.Parameters["@UserRole"].Value = UserAcc.UserRole.Trim();
                cmd.Parameters["@UserId"].Value = UserAcc.UserId.Trim();

                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AdminAccess ShowConfMstr = new AdminAccess();
                        ShowConfMstr.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
                        ShowConfMstr.Name = dt.Rows[i]["Name"].ToString();
                        ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
                        ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
                        ShowConfMstr.RoleName = dt.Rows[i]["RoleName"].ToString();
                        ShowConfMstr.MMaster = dt.Rows[i]["MMaster"].ToString();
                        ShowConfMstr.MBS = dt.Rows[i]["MBS"].ToString();
                        ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
                        ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
                        ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
                        ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
                        ShowConfMstr.MAccounts = dt.Rows[i]["MAccounts"].ToString();
                        ShowConfMstr.BMaster = dt.Rows[i]["BMaster"].ToString();
                        ShowConfMstr.BTransaction = dt.Rows[i]["BTransaction"].ToString();
                        ShowConfMstr.BBooking = dt.Rows[i]["BBooking"].ToString();
                        ShowConfMstr.BReports = dt.Rows[i]["BReports"].ToString();
                        ShowConfMstr.BRestaurant = dt.Rows[i]["BRestaurant"].ToString();

                        ShowConfMstr.BMBooking = dt.Rows[i]["BBMBooking"].ToString();
                        ShowConfMstr.BMBookingOthers = dt.Rows[i]["BBMBookingOthers"].ToString();
                        ShowConfMstr.BMBulkBooking = dt.Rows[i]["BBMBulkBooking"].ToString();
                        ShowConfMstr.BMOtherService = dt.Rows[i]["BBMOtherService"].ToString();
                        ShowConfMstr.BMBulkOtherService = dt.Rows[i]["BBMBulkOtherService"].ToString();
                        ShowConfMstr.BMCancellation = dt.Rows[i]["BBMCancellation"].ToString();
                        ShowConfMstr.BMReSchedule = dt.Rows[i]["BBMReScheduling"].ToString();

                        ShowConfMstr.TMMaterialPur = dt.Rows[i]["BTMMaterialPurchase"].ToString();
                        ShowConfMstr.TMMaterialIss = dt.Rows[i]["BTMMaterialIssue"].ToString();
                        ShowConfMstr.TMTripSheetSettle = dt.Rows[i]["BTMTripSheetSettle"].ToString();
                        ShowConfMstr.TMRowerSettle = dt.Rows[i]["BTMRowerSettle"].ToString();
                        ShowConfMstr.TMStockEntryMaintance = dt.Rows[i]["BTMFoodStockEntryMaintance"].ToString();


                        ShowConfMstr.RMBooking = dt.Rows[i]["BRMBooking"].ToString();
                        ShowConfMstr.RMOtherSvc = dt.Rows[i]["BRMOtherService"].ToString();
                        ShowConfMstr.RMTripSheetSettle = dt.Rows[i]["BRMTripSheet"].ToString();
                        ShowConfMstr.RMRowerSettle = dt.Rows[i]["BRMRowerSettle"].ToString();
                        ShowConfMstr.RMAbstractBoatBook = dt.Rows[i]["BRMAbstractBoatBook"].ToString();

                        ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

                        ShowConfMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowConfMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        li.Add(ShowConfMstr);
                    }

                    AdminAccessList ConfList = new AdminAccessList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    AdminAccessRes ConfRes = new AdminAccessRes
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
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("AdminAccess/ListAll/User")]
        //public IHttpActionResult AdminAccessUserList([FromBody] AdminAccess UserAcc)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        string sCondition = string.Empty;

        //        if (Convert.ToString(UserAcc.UserId) != "")
        //        {
        //            sCondition = " AND A.UserId = '" + UserAcc.UserId.ToString() + "' ";
        //        }
        //        else if (Convert.ToString(UserAcc.UserRole) != "")
        //        {
        //            sCondition = " AND A.UserRole = '" + UserAcc.UserRole.ToString() + "' ";
        //        }
        //        else if (Convert.ToString(UserAcc.UserId) != "" && Convert.ToString(UserAcc.UserRole) != "")
        //        {
        //            sCondition = " AND A.UserRole = '" + UserAcc.UserRole.ToString() + "' AND A.UserId = '" + UserAcc.UserId.ToString() + "' ";
        //        }

        //        sQuery = " SELECT A.UniqueId, A.UserId, CONCAT(B.EmpFirstName, ' ', B.EmpLastName) AS 'Name', B.UserName, A.UserRole, C.ConfigName 'RoleName', "
        //            + " A.MMaster, A.MBS, A.MTMS, A.MHMS, A.MAccounts, A.BMaster, A.BTransaction, A.BBooking, A.BReports, A.BRestaurant, A.BoatHouseId, A.BoatHouseName, "
        //            + " A.BBMBooking, A.BBMBookingOthers, A.BBMBulkBooking, A.BBMOtherService, A.BBMBulkOtherService, A.BBMCancellation, A.BBMReScheduling, A.BTMMaterialPurchase, "
        //            + " A.BTMMaterialIssue, A.BTMTripSheetSettle, A.BTMRowerSettle, A.BRMBooking, A.BRMOtherService, A.BRMTripSheet, A.BRMRowerSettle, A.BRMAbstractBoatBook , "
        //            + " A.OfflineRights "
        //            + " FROM UserAccessRights AS A "
        //            + " INNER JOIN EmpMaster AS B ON A.UserId = B.EmpID AND B.ActiveStatus = 'A' "
        //            + " INNER JOIN ConfigurationMaster AS C ON A.UserRole = C.ConfigID AND C.ActiveStatus = 'A' AND C.TypeID = 21 "
        //            + " WHERE B.RoleId IN (3) AND B.BoatHouseId = '" + UserAcc.BoatHouseId + "' AND A.BoatHouseId = '" + UserAcc.BoatHouseId + "'";

        //        sQuery = sQuery + sCondition;

        //        List<AdminAccess> li = new List<AdminAccess>();
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(sQuery, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                AdminAccess ShowConfMstr = new AdminAccess();
        //                ShowConfMstr.UniqueId = dt.Rows[i]["UniqueId"].ToString();
        //                ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
        //                ShowConfMstr.Name = dt.Rows[i]["Name"].ToString();
        //                ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
        //                ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
        //                ShowConfMstr.RoleName = dt.Rows[i]["RoleName"].ToString();
        //                ShowConfMstr.MMaster = dt.Rows[i]["MMaster"].ToString();
        //                ShowConfMstr.MBS = dt.Rows[i]["MBS"].ToString();
        //                ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
        //                ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
        //                ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
        //                ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
        //                ShowConfMstr.MAccounts = dt.Rows[i]["MAccounts"].ToString();
        //                ShowConfMstr.BMaster = dt.Rows[i]["BMaster"].ToString();
        //                ShowConfMstr.BTransaction = dt.Rows[i]["BTransaction"].ToString();
        //                ShowConfMstr.BBooking = dt.Rows[i]["BBooking"].ToString();
        //                ShowConfMstr.BReports = dt.Rows[i]["BReports"].ToString();
        //                ShowConfMstr.BRestaurant = dt.Rows[i]["BRestaurant"].ToString();

        //                ShowConfMstr.BMBooking = dt.Rows[i]["BBMBooking"].ToString();
        //                ShowConfMstr.BMBookingOthers = dt.Rows[i]["BBMBookingOthers"].ToString();
        //                ShowConfMstr.BMBulkBooking = dt.Rows[i]["BBMBulkBooking"].ToString();
        //                ShowConfMstr.BMOtherService = dt.Rows[i]["BBMOtherService"].ToString();
        //                ShowConfMstr.BMBulkOtherService = dt.Rows[i]["BBMBulkOtherService"].ToString();
        //                ShowConfMstr.BMCancellation = dt.Rows[i]["BBMCancellation"].ToString();
        //                ShowConfMstr.BMReSchedule = dt.Rows[i]["BBMReScheduling"].ToString();

        //                ShowConfMstr.TMMaterialPur = dt.Rows[i]["BTMMaterialPurchase"].ToString();
        //                ShowConfMstr.TMMaterialIss = dt.Rows[i]["BTMMaterialIssue"].ToString();
        //                ShowConfMstr.TMTripSheetSettle = dt.Rows[i]["BTMTripSheetSettle"].ToString();
        //                ShowConfMstr.TMRowerSettle = dt.Rows[i]["BTMRowerSettle"].ToString();

        //                ShowConfMstr.RMBooking = dt.Rows[i]["BRMBooking"].ToString();
        //                ShowConfMstr.RMOtherSvc = dt.Rows[i]["BRMOtherService"].ToString();
        //                ShowConfMstr.RMTripSheetSettle = dt.Rows[i]["BRMTripSheet"].ToString();
        //                ShowConfMstr.RMRowerSettle = dt.Rows[i]["BRMRowerSettle"].ToString();
        //                ShowConfMstr.RMAbstractBoatBook = dt.Rows[i]["BRMAbstractBoatBook"].ToString();

        //                ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

        //                ShowConfMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
        //                ShowConfMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                li.Add(ShowConfMstr);
        //            }

        //            AdminAccessList ConfList = new AdminAccessList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(ConfList);
        //        }

        //        else
        //        {
        //            AdminAccessRes ConfRes = new AdminAccessRes
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(ConfRes);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(ConfRes);
        //    }
        //}


        //List Admin Access for All

        /// <summary>
        /// Modified By : M Vinitha
        /// Modified Date : 22-09-2021
        /// </summary>
        /// <param name="UserAcc"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("AdminUserAccess/ListAll/User")]
        public IHttpActionResult AdminAccessList([FromBody] AdminAccess UserAcc)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(UserAcc.UserId) != "")
                {
                    sCondition = " WHERE A.UserId = @UserId";
                }
                else if (Convert.ToString(UserAcc.UserRole) != "")
                {
                    sCondition = " WHERE A.UserRole = @UserRole ";
                }
                else if (Convert.ToString(UserAcc.UserId) != "" && Convert.ToString(UserAcc.UserRole) != "")
                {
                    sCondition = " WHERE A.UserRole = @UserRole AND A.UserId = @UserId ";
                }

                sQuery = " SELECT A.UniqueId, A.UserId, CONCAT(B.EmpFirstName, ' ', B.EmpLastName) AS 'Name', B.UserName, A.UserRole, C.ConfigName 'RoleName', "
                    + " A.MMaster, A.MBS, A.MTMS, A.MHMS, A.MAccounts, A.MComMaster, A.MBhMaster, A.MHotelMaster, A.MTourMaster, A.MAccessRights, A.MOtherMaster, "
                    + " A.BMaster, A.BTransaction, A.BBooking, A.BReports, A.BRestaurant, A.BoatHouseId, A.BoatHouseName, "
                    + " A.BBMBooking, A.BBMBookingOthers, A.BBMOtherService, A.BBMCancellation, A.BBMReScheduling, A.BTMMaterialPurchase, A.BTMMaterialIssue,"
                    + " A.BTMTripSheetSettle, A.BTMRowerSettle,A.BTMFoodStockEntryMaintance,A.BRMBooking, A.BRMOtherService, A.BRMTripSheet, A.BRMRowerSettle, A.BRMAbstractBoatBook, "
                    + " A.OfflineRights "
                    + " FROM UserAccessRights AS A "
                    + " INNER JOIN EmpMaster AS B ON A.UserId = B.EmpID AND B.ActiveStatus = 'A' "
                    + " INNER JOIN ConfigurationMaster AS C ON A.UserRole = C.ConfigID AND C.ActiveStatus = 'A' AND C.TypeID = 21 ";

                sQuery = sQuery + sCondition;

                List<AdminAccess> li = new List<AdminAccess>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserRole", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = UserAcc.BoatHouseId.Trim();
                cmd.Parameters["@UserRole"].Value = UserAcc.UserRole.Trim();
                cmd.Parameters["@UserId"].Value = UserAcc.UserId.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AdminAccess ShowConfMstr = new AdminAccess();
                        ShowConfMstr.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
                        ShowConfMstr.Name = dt.Rows[i]["Name"].ToString();
                        ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
                        ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
                        ShowConfMstr.RoleName = dt.Rows[i]["RoleName"].ToString();
                        ShowConfMstr.MMaster = dt.Rows[i]["MMaster"].ToString();
                        ShowConfMstr.MBS = dt.Rows[i]["MBS"].ToString();
                        ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
                        ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
                        ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
                        ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
                        ShowConfMstr.MAccounts = dt.Rows[i]["MAccounts"].ToString();

                        ShowConfMstr.MComMaster = dt.Rows[i]["MComMaster"].ToString();
                        ShowConfMstr.MBhMaster = dt.Rows[i]["MBhMaster"].ToString();
                        ShowConfMstr.MHotelMaster = dt.Rows[i]["MHotelMaster"].ToString();
                        ShowConfMstr.MTourMaster = dt.Rows[i]["MTourMaster"].ToString();
                        ShowConfMstr.MAccessRights = dt.Rows[i]["MAccessRights"].ToString();
                        ShowConfMstr.MOtherMaster = dt.Rows[i]["MOtherMaster"].ToString();

                        ShowConfMstr.BMaster = dt.Rows[i]["BMaster"].ToString();
                        ShowConfMstr.BTransaction = dt.Rows[i]["BTransaction"].ToString();
                        ShowConfMstr.BBooking = dt.Rows[i]["BBooking"].ToString();
                        ShowConfMstr.BReports = dt.Rows[i]["BReports"].ToString();
                        ShowConfMstr.BRestaurant = dt.Rows[i]["BRestaurant"].ToString();

                        ShowConfMstr.BMBooking = dt.Rows[i]["BBMBooking"].ToString();
                        ShowConfMstr.BMBookingOthers = dt.Rows[i]["BBMBookingOthers"].ToString();
                        ShowConfMstr.BMOtherService = dt.Rows[i]["BBMOtherService"].ToString();
                        ShowConfMstr.BMCancellation = dt.Rows[i]["BBMCancellation"].ToString();
                        ShowConfMstr.BMReSchedule = dt.Rows[i]["BBMReScheduling"].ToString();

                        ShowConfMstr.TMMaterialPur = dt.Rows[i]["BTMMaterialPurchase"].ToString();
                        ShowConfMstr.TMMaterialIss = dt.Rows[i]["BTMMaterialIssue"].ToString();
                        ShowConfMstr.TMTripSheetSettle = dt.Rows[i]["BTMTripSheetSettle"].ToString();
                        ShowConfMstr.TMRowerSettle = dt.Rows[i]["BTMRowerSettle"].ToString();
                        ShowConfMstr.TMStockEntryMaintance = dt.Rows[i]["BTMFoodStockEntryMaintance"].ToString();

                        ShowConfMstr.RMBooking = dt.Rows[i]["BRMBooking"].ToString();
                        ShowConfMstr.RMOtherSvc = dt.Rows[i]["BRMOtherService"].ToString();
                        ShowConfMstr.RMTripSheetSettle = dt.Rows[i]["BRMTripSheet"].ToString();
                        ShowConfMstr.RMRowerSettle = dt.Rows[i]["BRMRowerSettle"].ToString();
                        ShowConfMstr.RMAbstractBoatBook = dt.Rows[i]["BRMAbstractBoatBook"].ToString();

                        ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

                        ShowConfMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowConfMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        li.Add(ShowConfMstr);
                    }

                    AdminAccessList ConfList = new AdminAccessList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    AdminAccessRes ConfRes = new AdminAccessRes
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("AdminUserAccess/ListAll/User")]
        //public IHttpActionResult AdminAccessList([FromBody] AdminAccess UserAcc)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        string sCondition = string.Empty;

        //        if (Convert.ToString(UserAcc.UserId) != "")
        //        {
        //            sCondition = " WHERE A.UserId = '" + UserAcc.UserId.ToString() + "' ";
        //        }
        //        else if (Convert.ToString(UserAcc.UserRole) != "")
        //        {
        //            sCondition = " WHERE A.UserRole = '" + UserAcc.UserRole.ToString() + "' ";
        //        }
        //        else if (Convert.ToString(UserAcc.UserId) != "" && Convert.ToString(UserAcc.UserRole) != "")
        //        {
        //            sCondition = " WHERE A.UserRole = '" + UserAcc.UserRole.ToString() + "' AND A.UserId = '" + UserAcc.UserId.ToString() + "' ";
        //        }

        //        sQuery = " SELECT A.UniqueId, A.UserId, CONCAT(B.EmpFirstName, ' ', B.EmpLastName) AS 'Name', B.UserName, A.UserRole, C.ConfigName 'RoleName', "
        //            + " A.MMaster, A.MBS, A.MTMS, A.MHMS, A.MAccounts, A.MComMaster, A.MBhMaster, A.MHotelMaster, A.MTourMaster, A.MAccessRights, A.MOtherMaster, "
        //            + " A.BMaster, A.BTransaction, A.BBooking, A.BReports, A.BRestaurant, A.BoatHouseId, A.BoatHouseName, "
        //            + " A.BBMBooking, A.BBMBookingOthers, A.BBMOtherService, A.BBMCancellation, A.BBMReScheduling, A.BTMMaterialPurchase, A.BTMMaterialIssue,"
        //            + " A.BTMTripSheetSettle, A.BTMRowerSettle, A.BRMBooking, A.BRMOtherService, A.BRMTripSheet, A.BRMRowerSettle, A.BRMAbstractBoatBook, "
        //            + " A.OfflineRights "
        //            + " FROM UserAccessRights AS A "
        //            + " INNER JOIN EmpMaster AS B ON A.UserId = B.EmpID AND B.ActiveStatus = 'A' "
        //            + " INNER JOIN ConfigurationMaster AS C ON A.UserRole = C.ConfigID AND C.ActiveStatus = 'A' AND C.TypeID = 21 ";

        //        sQuery = sQuery + sCondition;

        //        List<AdminAccess> li = new List<AdminAccess>();
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(sQuery, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                AdminAccess ShowConfMstr = new AdminAccess();
        //                ShowConfMstr.UniqueId = dt.Rows[i]["UniqueId"].ToString();
        //                ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
        //                ShowConfMstr.Name = dt.Rows[i]["Name"].ToString();
        //                ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
        //                ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
        //                ShowConfMstr.RoleName = dt.Rows[i]["RoleName"].ToString();
        //                ShowConfMstr.MMaster = dt.Rows[i]["MMaster"].ToString();
        //                ShowConfMstr.MBS = dt.Rows[i]["MBS"].ToString();
        //                ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
        //                ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
        //                ShowConfMstr.MTMS = dt.Rows[i]["MTMS"].ToString();
        //                ShowConfMstr.MHMS = dt.Rows[i]["MHMS"].ToString();
        //                ShowConfMstr.MAccounts = dt.Rows[i]["MAccounts"].ToString();

        //                ShowConfMstr.MComMaster = dt.Rows[i]["MComMaster"].ToString();
        //                ShowConfMstr.MBhMaster = dt.Rows[i]["MBhMaster"].ToString();
        //                ShowConfMstr.MHotelMaster = dt.Rows[i]["MHotelMaster"].ToString();
        //                ShowConfMstr.MTourMaster = dt.Rows[i]["MTourMaster"].ToString();
        //                ShowConfMstr.MAccessRights = dt.Rows[i]["MAccessRights"].ToString();
        //                ShowConfMstr.MOtherMaster = dt.Rows[i]["MOtherMaster"].ToString();

        //                ShowConfMstr.BMaster = dt.Rows[i]["BMaster"].ToString();
        //                ShowConfMstr.BTransaction = dt.Rows[i]["BTransaction"].ToString();
        //                ShowConfMstr.BBooking = dt.Rows[i]["BBooking"].ToString();
        //                ShowConfMstr.BReports = dt.Rows[i]["BReports"].ToString();
        //                ShowConfMstr.BRestaurant = dt.Rows[i]["BRestaurant"].ToString();

        //                ShowConfMstr.BMBooking = dt.Rows[i]["BBMBooking"].ToString();
        //                ShowConfMstr.BMBookingOthers = dt.Rows[i]["BBMBookingOthers"].ToString();
        //                ShowConfMstr.BMOtherService = dt.Rows[i]["BBMOtherService"].ToString();
        //                ShowConfMstr.BMCancellation = dt.Rows[i]["BBMCancellation"].ToString();
        //                ShowConfMstr.BMReSchedule = dt.Rows[i]["BBMReScheduling"].ToString();

        //                ShowConfMstr.TMMaterialPur = dt.Rows[i]["BTMMaterialPurchase"].ToString();
        //                ShowConfMstr.TMMaterialIss = dt.Rows[i]["BTMMaterialIssue"].ToString();
        //                ShowConfMstr.TMTripSheetSettle = dt.Rows[i]["BTMTripSheetSettle"].ToString();
        //                ShowConfMstr.TMRowerSettle = dt.Rows[i]["BTMRowerSettle"].ToString();

        //                ShowConfMstr.RMBooking = dt.Rows[i]["BRMBooking"].ToString();
        //                ShowConfMstr.RMOtherSvc = dt.Rows[i]["BRMOtherService"].ToString();
        //                ShowConfMstr.RMTripSheetSettle = dt.Rows[i]["BRMTripSheet"].ToString();
        //                ShowConfMstr.RMRowerSettle = dt.Rows[i]["BRMRowerSettle"].ToString();
        //                ShowConfMstr.RMAbstractBoatBook = dt.Rows[i]["BRMAbstractBoatBook"].ToString();

        //                ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

        //                ShowConfMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
        //                ShowConfMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                li.Add(ShowConfMstr);
        //            }

        //            AdminAccessList ConfList = new AdminAccessList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(ConfList);
        //        }

        //        else
        //        {
        //            AdminAccessRes ConfRes = new AdminAccessRes
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(ConfRes);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(ConfRes);
        //    }
        //}

        /**********************************User Registration***********************************/

        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("UserReg")]
        public IHttpActionResult UserRegistration([FromBody] UserRegistration InsUserReg)
        {
            try
            {
                if (InsUserReg.QueryType != null
                    && InsUserReg.MobileNo != null && InsUserReg.Password != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("UserRegistration", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsUserReg.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@MobileNo", InsUserReg.MobileNo.ToString());
                    cmd.Parameters.AddWithValue("@Password", InsUserReg.Password.ToString());

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
                        UserRegistrationRes ConMstr = new UserRegistrationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        UserRegistrationRes ConMstr = new UserRegistrationRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    UserRegistrationRes Vehicle = new UserRegistrationRes
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


        /***********************************Configuration Master**************************************/

        //Display Configuration List

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/ListAll")]
        public IHttpActionResult ShowConfigMstrDet()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT A.TypeId, B.TypeName, A.ConfigId, A.ConfigName, A.ActiveStatus, A.CreatedBy "
                        + " FROM ConfigurationMaster AS A "
                        + " INNER JOIN ConfigurationType AS B ON A.TypeId = B.TypeId AND B.ActiveStatus = 'A' "
                        + " WHERE A.ActiveStatus IN ('A','D') ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowConfMstr = new ConfigurationMaster();
                        ShowConfMstr.TypeId = dt.Rows[i]["TypeId"].ToString();
                        ShowConfMstr.TypeName = dt.Rows[i]["TypeName"].ToString();
                        ShowConfMstr.ConfigId = dt.Rows[i]["ConfigId"].ToString();
                        ShowConfMstr.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        li.Add(ShowConfMstr);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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


        /***********************************Boat Type Master**************************************/

        [HttpGet]
        [AllowAnonymous]
        [Route("BoatTypeMstr/ListAll")]
        public IHttpActionResult GetBoatTypeMaster()
        {
            try
            {
                List<BoatTypeMaster> li = new List<BoatTypeMaster>();

                SqlCommand cmd = new SqlCommand("SELECT A.BoatTypeId,A.BoatType,A.BoatHouseId,B.BoatHouseName, A.ActiveStatus FROM BoatTypes AS A "
                    + " INNER JOIN BHMaster AS B ON A.BoatHouseId=B.BoatHouseId AND A.ActiveStatus=B.ActiveStatus WHERE A.ActiveStatus='A' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTypeMaster BoatTypeMaster = new BoatTypeMaster();

                        BoatTypeMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        BoatTypeMaster.BoatType = dt.Rows[i]["BoatType"].ToString();
                        BoatTypeMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        BoatTypeMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        BoatTypeMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                        li.Add(BoatTypeMaster);
                    }

                    BoatTypeMasterList BoatType = new BoatTypeMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatType);
                }

                else
                {
                    BoatTypeMasterString BoatType = new BoatTypeMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatType);
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

        /***********************************Boat Seater Master**************************************/

        [HttpGet]
        [AllowAnonymous]
        [Route("BoatSeaterMstr/ListAll")]
        public IHttpActionResult GetBoatSeaterMaster()
        {
            try
            {
                List<BoatSeatMaster> li = new List<BoatSeatMaster>();

                SqlCommand cmd = new SqlCommand("SELECT A.BoatSeaterId,A.SeaterType,A.BoatHouseId,B.BoatHouseName,A.NoOfSeats, A.ActiveStatus FROM BoatSeat AS A "
                    + " INNER JOIN BHMaster AS B ON A.BoatHouseId=B.BoatHouseId AND A.ActiveStatus=B.ActiveStatus WHERE A.ActiveStatus='A' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatSeatMaster BoatSeatMaster = new BoatSeatMaster();

                        BoatSeatMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        BoatSeatMaster.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                        BoatSeatMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        BoatSeatMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        BoatSeatMaster.NoOfSeats = dt.Rows[i]["NoOfSeats"].ToString();
                        BoatSeatMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(BoatSeatMaster);
                    }

                    BoatSeatMasterList BoatSeat = new BoatSeatMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatSeat);
                }

                else
                {
                    BoatSeatMasterString BoatSeat = new BoatSeatMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatSeat);
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


        /***********************************Boat Rate Master**************************************/

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("BoatRateMstr/ListAll")]
        //public IHttpActionResult GetBoatRateMaster()
        //{
        //    try
        //    {
        //        List<BoatRateMaster> li = new List<BoatRateMaster>();

        //        SqlCommand cmd = new SqlCommand("SELECT A.BoatTypeId,C.BoatType,A.BoatHouseId,B.BoatHouseName,A.BoatSeaterId,D.SeaterType,A.BoatImageLink,"
        //            + " CASE When  A.SelfDrive='A' Then 'Allowed' else  'Not Allowed' End As SelfDrive,A.Deposit,CASE When  A.TimeExtension='A' Then 'Allowed' else  'Not Allowed' End As TimeExtension, "
        //            + " A.BoatMinTime,A.BoatExtnTime,A.BoatGraceTime,A.BoatMinCharge,A.BoatExtnCharge,A.RowerMinCharge,A.RowerExtnCharge,A.BoatPremMinCharge, "
        //            + " CASE When  A.DepositType='P' Then 'Percentage' else  'Fixed Amount' End As DepositType,A.BoatPremExtnCharge,A.RowerPremMinCharge, "
        //            + " A.RowerPremExtnCharge,A.MaxTripsPerDay, A.ActiveStatus FROM BoatRateMaster AS A "
        //            + " INNER JOIN BHMaster AS B ON A.BoatHouseId=B.BoatHouseId AND A.ActiveStatus=B.ActiveStatus "
        //            + " INNER JOIN BoatTypes AS C ON A.BoatTypeId=C.BoatTypeId AND A.ActiveStatus=C.ActiveStatus "
        //            + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId=D.BoatSeaterId AND A.ActiveStatus=D.ActiveStatus WHERE A.ActiveStatus='A' ", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                BoatRateMaster BoatRateMaster = new BoatRateMaster();

        //                BoatRateMaster.BoatTypeId = Convert.ToInt32(dt.Rows[i]["BoatTypeId"].ToString());
        //                BoatRateMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
        //                BoatRateMaster.BoatSeaterId = Convert.ToInt32(dt.Rows[i]["BoatSeaterId"].ToString());
        //                BoatRateMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
        //                BoatRateMaster.BoatHouseId = Convert.ToInt32(dt.Rows[i]["BoatHouseId"].ToString());
        //                BoatRateMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                BoatRateMaster.BoatImageLink = dt.Rows[i]["BoatImageLink"].ToString();
        //                BoatRateMaster.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
        //                BoatRateMaster.DepositType = dt.Rows[i]["DepositType"].ToString();
        //                BoatRateMaster.Deposit = Convert.ToDecimal(dt.Rows[i]["Deposit"].ToString());
        //                BoatRateMaster.TimeExtension = dt.Rows[i]["TimeExtension"].ToString();
        //                BoatRateMaster.BoatMinTime = Convert.ToInt32(dt.Rows[i]["BoatMinTime"].ToString());
        //                BoatRateMaster.BoatExtnTime = Convert.ToInt32(dt.Rows[i]["BoatExtnTime"].ToString());
        //                BoatRateMaster.BoatGraceTime = Convert.ToInt32(dt.Rows[i]["BoatGraceTime"].ToString());
        //                BoatRateMaster.BoatMinCharge = Convert.ToDecimal(dt.Rows[i]["BoatMinCharge"].ToString());
        //                BoatRateMaster.BoatExtnCharge = Convert.ToDecimal(dt.Rows[i]["BoatExtnCharge"].ToString());
        //                BoatRateMaster.RowerMinCharge = Convert.ToDecimal(dt.Rows[i]["RowerMinCharge"].ToString());
        //                BoatRateMaster.RowerExtnCharge = Convert.ToDecimal(dt.Rows[i]["RowerExtnCharge"].ToString());
        //                BoatRateMaster.BoatPremMinCharge = Convert.ToDecimal(dt.Rows[i]["BoatPremMinCharge"].ToString());
        //                BoatRateMaster.BoatPremExtnCharge = Convert.ToDecimal(dt.Rows[i]["BoatPremExtnCharge"].ToString());
        //                BoatRateMaster.RowerPremMinCharge = Convert.ToDecimal(dt.Rows[i]["RowerPremMinCharge"].ToString());
        //                BoatRateMaster.RowerPremExtnCharge = Convert.ToDecimal(dt.Rows[i]["RowerPremExtnCharge"].ToString());
        //                BoatRateMaster.MaxTripsPerDay = Convert.ToInt32(dt.Rows[i]["MaxTripsPerDay"].ToString());
        //                BoatRateMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
        //                li.Add(BoatRateMaster);
        //            }

        //            BoatRateMasterList BoatRate = new BoatRateMasterList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(BoatRate);
        //        }

        //        else
        //        {
        //            BoatRateMasterString BoatRate = new BoatRateMasterString
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(BoatRate);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(ConfRes);
        //    }
        //}


        /***********************************Boat Master**************************************/

        [HttpGet]
        [AllowAnonymous]
        [Route("BoatMstr/ListAll")]
        public IHttpActionResult GetBoatMaster()
        {
            try
            {
                List<BoatMaster> li = new List<BoatMaster>();

                SqlCommand cmd = new SqlCommand(" SELECT A.BoatId,A.BoatName, A.BoatNum, A.BoatTypeId,C.BoatType, A.BoatHouseId,B.BoatHouseName, "
                        + " A.BoatSeaterId, D.SeaterType, A.BoatStatus, E.ConfigName AS 'BoatStatName', A.PaymentModel, "
                        + " F.ConfigName AS 'PayType', A.PaymentPercent, A.PaymentAmount, "
                        + " CASE When  A.BoatOwner = 'T' Then 'Own' else  'Private' End As BoatOwner FROM BoatMaster AS A "
                        + " INNER JOIN BHMaster AS B ON A.BoatHouseId = B.BoatHouseId AND B.ActiveStatus = 'A' "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND C.ActiveStatus = 'A' "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND D.ActiveStatus = 'A' "
                        + " INNER JOIN ConfigurationMaster AS E ON A.BoatStatus = E.ConfigID AND E.TypeID = '16' AND E.ActiveStatus = 'A' "
                        + " INNER JOIN ConfigurationMaster AS F ON A.PaymentModel = F.ConfigID AND F.TypeID = '20' AND F.ActiveStatus = 'A' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatMaster BoatMaster = new BoatMaster();

                        BoatMaster.BoatId = dt.Rows[i]["BoatId"].ToString();
                        BoatMaster.BoatName = dt.Rows[i]["BoatName"].ToString();
                        BoatMaster.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                        BoatMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        BoatMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                        BoatMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        BoatMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                        BoatMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        BoatMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        BoatMaster.BoatStatus = dt.Rows[i]["BoatStatus"].ToString();
                        BoatMaster.BoatStatusName = dt.Rows[i]["BoatStatName"].ToString();
                        BoatMaster.BoatOwner = dt.Rows[i]["BoatOwner"].ToString();
                        BoatMaster.PaymentModel = dt.Rows[i]["PaymentModel"].ToString();
                        BoatMaster.PayTypeName = dt.Rows[i]["PayType"].ToString();
                        BoatMaster.PaymentAmount = dt.Rows[i]["PaymentAmount"].ToString();
                        BoatMaster.PaymentPercent = dt.Rows[i]["PaymentPercent"].ToString();

                        li.Add(BoatMaster);
                    }

                    BoatMasterList BoatRate = new BoatMasterList
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
        [Route("OfferMstr/BHId")]
        public IHttpActionResult OfferMasterId([FromBody] OfferMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null)
                {
                    List<OfferMaster> li = new List<OfferMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT OfferId, OfferType, OfferName, AmountType, Offer, MinBillAmount, "
                        + " MinNoOfTickets,Convert(Nvarchar(20),EffectiveFrom,105) 'EffectiveFrom', "
                        + " Convert(Nvarchar(20), EffectiveTill, 105) 'EffectiveTill', ActiveStatus, Createdby, Convert(Nvarchar(20), CreatedDate, 105) 'CreatedDate', "
                        + " Updatedby, Convert(Nvarchar(20), UpdatedDate, 105) 'UpdatedDate' from OfferMaster Where ActiveStatus IN ('A','D') "
                        + " AND BoatHouseId = @BoatHouseId ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
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
        [Route("OfferMstr/BHId/OffId")]
        public IHttpActionResult OffMstrId([FromBody] OfferMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && Convert.ToString(bHMstr.OfferId) != null)
                {
                    List<OfferMaster> li = new List<OfferMaster>();

                    SqlCommand cmd = new SqlCommand(" SELECT OfferId, OfferName, BoatHouseId, BoatHouseName, OfferType, AmountType, Offer, MinBIllAmount, MinNoOfTickets, "
                        + " CONVERT(NVARCHAR(50), EffectiveFrom, 105) AS 'EffectiveFrom', CONVERT(NVARCHAR(50), EffectiveTill, 105) AS 'EffectiveTill', "
                        + " ActiveStatus, Createdby FROM OfferMaster WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId "
                        + " AND OfferId = @OfferId ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@OfferId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@OfferId"].Value = bHMstr.OfferId.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OfferMaster OfferMasters = new OfferMaster();

                            OfferMasters.OfferId = dt.Rows[i]["OfferId"].ToString();
                            OfferMasters.OfferName = dt.Rows[i]["OfferName"].ToString();
                            OfferMasters.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            OfferMasters.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            OfferMasters.OfferType = dt.Rows[i]["OfferType"].ToString();
                            OfferMasters.AmountType = dt.Rows[i]["AmountType"].ToString();
                            OfferMasters.Offer = dt.Rows[i]["Offer"].ToString();
                            OfferMasters.MinBillAmount = dt.Rows[i]["MinBillAmount"].ToString();
                            OfferMasters.MinNoOfTickets = dt.Rows[i]["MinNoOfTickets"].ToString();
                            OfferMasters.EffectiveFrom = dt.Rows[i]["EffectiveFrom"].ToString();
                            OfferMasters.EffectiveTill = dt.Rows[i]["EffectiveTill"].ToString();
                            OfferMasters.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OfferMasters.CreatedBy = dt.Rows[i]["Createdby"].ToString();

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


        [HttpGet]
        [AllowAnonymous]
        [Route("ItemMaster/ListAll")]
        public IHttpActionResult getItemMaster()
        {
            try
            {
                List<ItemMaster> li = new List<ItemMaster>();

                SqlCommand cmd = new SqlCommand("select A.ItemId , A.ItemDescription , A.ItemType , C.ConfigName As ItemName ,A.UOM , B.ConfigName as UOMName , "
                                                + " A.ItemRate, A.OpeningQty, A.ActiveStatus, A.CreatedBy  From ItemMaster AS A INNER JOIN ConfigurationMaster AS B "
                                                + " ON A.ItemType = B.ConfigID AND B.ActiveStatus = 'A' AND B.TypeID = 22 INNER JOIN ConfigurationMaster "
                                                 + "AS C ON A.ItemType = C.ConfigID AND C.ActiveStatus = 'A' AND C.TypeID = 23 where A.ActiveStatus IN ('A','D') ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ItemMaster ItemMaster = new ItemMaster();

                        ItemMaster.ItemId = dt.Rows[i]["ItemId"].ToString();
                        ItemMaster.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();
                        ItemMaster.ItemType = dt.Rows[i]["ItemType"].ToString();
                        ItemMaster.ItemName = dt.Rows[i]["ItemName"].ToString();
                        ItemMaster.UOM = dt.Rows[i]["UOM"].ToString();
                        ItemMaster.UOMName = dt.Rows[i]["UOMName"].ToString();
                        ItemMaster.ItemRate = dt.Rows[i]["ItemRate"].ToString();
                        ItemMaster.OpeningQty = dt.Rows[i]["OpeningQty"].ToString();
                        ItemMaster.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        ItemMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();



                        li.Add(ItemMaster);
                    }
                    ItemMasterList ItemMasters = new ItemMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ItemMasters);
                }
                else
                {
                    ItemMasterString ItemMasters1 = new ItemMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
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


        /***********************************Boat Consumption Master**************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("BHRate/ddlBoatType")]
        public IHttpActionResult BoatType([FromBody] BoatTypeMaster boatType)
        {
            try
            {
                if (boatType.BoatHouseId != null)
                {
                    List<BoatTypeMaster> li = new List<BoatTypeMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select Distinct( A.BoatTypeId),C.BoatType from BoatRateMaster AS A  "
                         + " Inner Join BoatTypes AS C On A.BoatTypeId = C.BoatTypeId ANd C.ActiveStatus = 'A' "
                          + "where A.ActiveStatus = 'A' And A.BoatHouseId=@BoatHouseId", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatTypeMaster ShowBoatTypes = new BoatTypeMaster();
                            ShowBoatTypes.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowBoatTypes.BoatType = dt.Rows[i]["BoatType"].ToString();

                            li.Add(ShowBoatTypes);
                        }

                        BoatTypeMasterList ConfList = new BoatTypeMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BoatTypeMasterString ConfRes = new BoatTypeMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatTypeMasterString Vehicle = new BoatTypeMasterString
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

        [HttpPost]
        [AllowAnonymous]
        [Route("BHRate/ddlBoatSeater")]
        public IHttpActionResult BHMstrBoatSeater([FromBody] BoatSeatMaster boatSeat)
        {
            try
            {
                if (boatSeat.BoatHouseId != null)
                {
                    List<BoatSeatMaster> li = new List<BoatSeatMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select Distinct( A.BoatSeaterId),B.SeaterType from BoatRateMaster AS A "
                                       + "Inner Join BoatSeat AS B On A.BoatSeaterId = B.BoatSeaterId ANd B.ActiveStatus = 'A' "
                                        + "where A.ActiveStatus = 'A' ANd A.BoatHouseId= @BoatHouseId ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatSeat.BoatHouseId.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatSeatMaster ShowBoatSeater = new BoatSeatMaster();
                            ShowBoatSeater.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowBoatSeater.SeaterType = dt.Rows[i]["SeaterType"].ToString();

                            li.Add(ShowBoatSeater);
                        }

                        BoatSeatMasterList ConfList = new BoatSeatMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BoatSeatMasterString ConfRes = new BoatSeatMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatSeatMasterString Vehicle = new BoatSeatMasterString
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


        /***********************************Boat Booking**************************************/


        [HttpPost]
        [AllowAnonymous]
        [Route("Boating/BookedList")]
        public IHttpActionResult BoatBookedList([FromBody] BoatTicketDtl BoatAvList)
        {
            try
            {
                if (BoatAvList.BoatHouseId != null)
                {
                    List<BoatTicketDtl> li = new List<BoatTicketDtl>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "BoatBooked");
                    cmd.Parameters.AddWithValue("@BoatHouseId", BoatAvList.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(BoatAvList.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(BoatAvList.FromDate.Trim(), objEnglishDate));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatTicketDtl Botlst = new BoatTicketDtl();

                            Botlst.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            Botlst.BoatType = dt.Rows[i]["BoatType"].ToString();
                            Botlst.BookedTrips = dt.Rows[i]["BookedTrips"].ToString();


                            li.Add(Botlst);
                        }

                        BoatTicketDtllist Bolist = new BoatTicketDtllist
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
        //[Route("BoatAvailableList")]
        //public IHttpActionResult BoatAvailable([FromBody] BoatAvailable BoatAvList)
        //{
        //    try
        //    {
        //        if (BoatAvList.BoatHouseId != null)
        //        {
        //            List<BoatAvailable> li = new List<BoatAvailable>();
        //            SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Clear();
        //            cmd.CommandTimeout = 10000000;
        //            cmd.Parameters.AddWithValue("@QueryType", "BoatAvailable");
        //            cmd.Parameters.AddWithValue("@BoatHouseId", BoatAvList.BoatHouseId.Trim());
        //            cmd.Parameters.AddWithValue("@FromDate", SqlDbType.VarChar).Value = DateTime.Parse(BoatAvList.BookingDate.Trim(), objEnglishDate);
        //            cmd.Parameters.AddWithValue("@ToDate", SqlDbType.VarChar).Value = DateTime.Parse(BoatAvList.BookingDate.Trim(), objEnglishDate);
        //            cmd.Parameters.AddWithValue("@PremiumStatus", BoatAvList.PremiumStatus);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();

        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    BoatAvailable Botlst = new BoatAvailable();

        //                    Botlst.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();

        //                    Botlst.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                    Botlst.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                    Botlst.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                    Botlst.SeaterType = dt.Rows[i]["SeaterType"].ToString();
        //                    Botlst.NoOfSeats = dt.Rows[i]["NoOfSeats"].ToString();
        //                    Botlst.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
        //                    Botlst.DepositType = dt.Rows[i]["DepositType"].ToString();
        //                    Botlst.Deposit = dt.Rows[i]["Deposit"].ToString();
        //                    Botlst.TimeExtension = dt.Rows[i]["TimeExtension"].ToString();

        //                    Botlst.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();
        //                    Botlst.BoatExtnTime = dt.Rows[i]["BoatExtnTime"].ToString();
        //                    Botlst.BoatGraceTime = dt.Rows[i]["BoatGraceTime"].ToString();
        //                    Botlst.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();

        //                    Botlst.BoatTotalCharge = dt.Rows[i]["BoatTotalCharge"].ToString();
        //                    Botlst.BoatMinCharge = dt.Rows[i]["BoatMinCharge"].ToString();
        //                    Botlst.BoatExtnCharge = dt.Rows[i]["BoatExtnCharge"].ToString();
        //                    Botlst.RowerMinCharge = dt.Rows[i]["RowerMinCharge"].ToString();
        //                    Botlst.RowerExtnCharge = dt.Rows[i]["RowerExtnCharge"].ToString();
        //                    Botlst.BoatExtnTotalCharge = dt.Rows[i]["BoatExtnTotalCharge"].ToString();
        //                    Botlst.BoatTaxCharge = dt.Rows[i]["BoatTaxCharge"].ToString();
        //                    Botlst.BoatExtnTaxCharge = dt.Rows[i]["BoatExtnTaxCharge"].ToString();


        //                    Botlst.MaxTripsPerDay = dt.Rows[i]["MaxTripsPerDay"].ToString();
        //                    Botlst.BoatImageLink = dt.Rows[i]["BoatImageLink"].ToString();

        //                    Botlst.BookedTrips = dt.Rows[i]["BookedTrips"].ToString();
        //                    Botlst.RemainTrips = dt.Rows[i]["RemainTrips"].ToString();


        //                    li.Add(Botlst);
        //                }

        //                BoatAvailableList Bolist = new BoatAvailableList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(Bolist);

        //            }
        //            else
        //            {
        //                UserLoginRes ConfRes = new UserLoginRes
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(ConfRes);
        //            }
        //        }
        //        else
        //        {
        //            UserLoginRes Vehicle = new UserLoginRes
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
        //        return Ok(ConfRes);
        //    }
        //}

        /// <summary>
        /// Modified By : Imran
        /// Modified Date : 12-08-2021
        /// Add @BoatType Extra parameter
        /// Modified : JayaSuriya
        /// Modified date : 18-08-2021
        /// Add Two Extra Field Return. like Ratings,NoOfRatings
        /// </summary>
        /// <param name="BoatAvList"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatAvailableList")]
        public IHttpActionResult BoatAvailable([FromBody] BoatAvailable BoatAvList)
        {
            try
            {
                if (BoatAvList.BoatHouseId != null)
                {
                    List<BoatAvailable> li = new List<BoatAvailable>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "BoatAvailable");
                    cmd.Parameters.AddWithValue("@BoatHouseId", BoatAvList.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", SqlDbType.VarChar).Value = DateTime.Parse(BoatAvList.BookingDate.Trim(), objEnglishDate);
                    cmd.Parameters.AddWithValue("@ToDate", SqlDbType.VarChar).Value = DateTime.Parse(BoatAvList.BookingDate.Trim(), objEnglishDate);
                    cmd.Parameters.AddWithValue("@PremiumStatus", BoatAvList.PremiumStatus);
                    cmd.Parameters.AddWithValue("@BoatType", BoatAvList.BoatType);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatAvailable Botlst = new BoatAvailable();

                            Botlst.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();

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

                            Botlst.BoatTotalCharge = dt.Rows[i]["BoatTotalCharge"].ToString();
                            Botlst.BoatMinCharge = dt.Rows[i]["BoatMinCharge"].ToString();
                            Botlst.RowerMinCharge = dt.Rows[i]["RowerMinCharge"].ToString();
                            Botlst.BoatTaxCharge = dt.Rows[i]["BoatTaxCharge"].ToString();

                            Botlst.NTBoatTotalCharge = dt.Rows[i]["NTBoatTotalCharge"].ToString();
                            Botlst.NTBoatMinCharge = dt.Rows[i]["NTBoatMinCharge"].ToString();
                            Botlst.NTRowerMinCharge = dt.Rows[i]["NTRowerMinCharge"].ToString();
                            Botlst.NTBoatTaxCharge = dt.Rows[i]["NTBoatTaxCharge"].ToString();



                            Botlst.ECBoatTotalCharge = dt.Rows[i]["ECBoatTotalCharge"].ToString();
                            Botlst.ECBoatMinCharge = dt.Rows[i]["ECBoatMinCharge"].ToString();
                            Botlst.ECRowerMinCharge = dt.Rows[i]["ECRowerMinCharge"].ToString();
                            Botlst.ECBoatTaxCharge = dt.Rows[i]["ECBoatTaxCharge"].ToString();
                            Botlst.BHShortCode = dt.Rows[i]["BHShortCode"].ToString();

                            //Botlst.BoatExtnCharge = dt.Rows[i]["BoatExtnCharge"].ToString();
                            //Botlst.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            //Botlst.RowerExtnCharge = dt.Rows[i]["RowerExtnCharge"].ToString();
                            //Botlst.BoatExtnTotalCharge = dt.Rows[i]["BoatExtnTotalCharge"].ToString();
                            //Botlst.BoatExtnTaxCharge = dt.Rows[i]["BoatExtnTaxCharge"].ToString();
                            //Botlst.MaxTripsPerDay = dt.Rows[i]["MaxTripsPerDay"].ToString();
                            //Botlst.BookedTrips = dt.Rows[i]["BookedTrips"].ToString();
                            //Botlst.RemainTrips = dt.Rows[i]["RemainTrips"].ToString();

                            Botlst.BoatImageLink = dt.Rows[i]["BoatImageLink"].ToString();
                            Botlst.Individual = dt.Rows[i]["Individual"].ToString();
                            Botlst.Normal = dt.Rows[i]["Normal"].ToString();
                            Botlst.Express = dt.Rows[i]["Express"].ToString();


                            Botlst.AverageRating = dt.Rows[i]["AverageRating"].ToString();
                            Botlst.NoOfRatings = dt.Rows[i]["NoOfRatings"].ToString();


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
        [Route("Boating/RemainingList")]
        public IHttpActionResult BoatBookedRemainingList([FromBody] BoatTicketDtl BoatAvList)
        {
            try
            {
                if (BoatAvList.BoatHouseId != null)
                {
                    List<BoatTicketDtl> li = new List<BoatTicketDtl>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "BoatBookedRemain");
                    cmd.Parameters.AddWithValue("@BoatHouseId", BoatAvList.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(BoatAvList.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(BoatAvList.FromDate.Trim(), objEnglishDate));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatTicketDtl Botlst = new BoatTicketDtl();

                            Botlst.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            Botlst.BoatType = dt.Rows[i]["BoatType"].ToString();
                            Botlst.RemainTrips = dt.Rows[i]["RemainTrips"].ToString();


                            li.Add(Botlst);
                        }

                        BoatTicketDtllist Bolist = new BoatTicketDtllist
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
        [Route("GenOffPinTranDetails")]
        public IHttpActionResult GenerateOfflinePinTransactionDetails([FromBody] OfflinePin BtPin)
        {
            try
            {
                if (BtPin.BoatHouseId != null)
                {
                    List<OfflinePin> li = new List<OfflinePin>();
                    SqlCommand cmd = new SqlCommand("GenerateOfflinePinTransactionDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@UserId", BtPin.UserId.Trim());
                    cmd.Parameters.AddWithValue("@BookingMedia", BtPin.BookingMedia.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", BtPin.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", BtPin.BoatHouseId.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OfflinePin Botlst = new OfflinePin();

                            Botlst.SerialNo = dt.Rows[i]["Sno"].ToString();
                            Botlst.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            Botlst.BookingId = dt.Rows[i]["BookingId"].ToString();
                            Botlst.OthBookingId = dt.Rows[i]["OthBookingId"].ToString();
                            Botlst.RestBookingId = dt.Rows[i]["RestBookingId"].ToString();
                            li.Add(Botlst);
                        }

                        OfflinePinList Bolist = new OfflinePinList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(Bolist);

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
                else
                {
                    BookedPinStr Vehicle = new BookedPinStr
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BookedPinStr ConfRes = new BookedPinStr
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


        /***********************************Boat Booking Other Services**************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("CategoryList/BhId")]
        public IHttpActionResult CategoryBhid([FromBody] ConfigurationMaster OthSvc)
        {
            try
            {

                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" SELECT DISTINCT A.Category, B.ConfigName FROM OtherServices AS A "
                    + " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND TypeID = '27' "
                    + " WHERE A.ActiveStatus = 'A' AND A.OtherServiceType = 'OS' AND A.BoatHouseId = @BoatHouseId; ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = OthSvc.BoatHouseId.Trim();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowEventType = new ConfigurationMaster();
                        ShowEventType.ConfigId = dt.Rows[i]["Category"].ToString();
                        ShowEventType.ConfigName = dt.Rows[i]["ConfigName"].ToString();

                        li.Add(ShowEventType);
                    }

                    ConfigurationMasterList EventType = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(EventType);
                }

                else
                {
                    ConfigurationMasterRes EventType = new ConfigurationMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(EventType);
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
        [Route("AdditionalBoatOtherService")]
        public IHttpActionResult AdditionalBoatOtherService([FromBody] ConfigurationMaster OthSvc)
        {
            try
            {

                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" SELECT DISTINCT A.Category, B.ConfigName FROM OtherServices AS A "
                    + " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND TypeID = '34' "
                    + " WHERE A.ActiveStatus = 'A' AND A.OtherServiceType = 'AB' AND A.BoatHouseId = @BoatHouseId; ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = OthSvc.BoatHouseId.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowEventType = new ConfigurationMaster();
                        ShowEventType.ConfigId = dt.Rows[i]["Category"].ToString();
                        ShowEventType.ConfigName = dt.Rows[i]["ConfigName"].ToString();

                        li.Add(ShowEventType);
                    }

                    ConfigurationMasterList EventType = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(EventType);
                }

                else
                {
                    ConfigurationMasterRes EventType = new ConfigurationMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(EventType);
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

        //Insert


        [HttpPost]
        [AllowAnonymous]
        [Route("OfflineBookingOtherServices")]
        public IHttpActionResult OfflineBookingOtherServices([FromBody] BookingOtherServices InsMatPur)
        {
            try
            {
                if (InsMatPur.QueryType != null && Convert.ToString(InsMatPur.BookingId) != null && InsMatPur.ServiceId != null
                    && Convert.ToString(InsMatPur.BookingType) != null && Convert.ToString(InsMatPur.BoatHouseId) != null && InsMatPur.BoatHouseName != null
                    && Convert.ToString(InsMatPur.ChargePerItem) != null && Convert.ToString(InsMatPur.NoOfItems) != null
                    && Convert.ToString(InsMatPur.TaxDetails) != null
                    && Convert.ToString(InsMatPur.NetAmount) != null && InsMatPur.CreatedBy != null && InsMatPur.BookingMedia != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("OfflineBookingOtherServices", con);
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
        //[Route("BoatBookingServiceBulk")]
        //public IHttpActionResult BoatBookingServiceBulk([FromBody] BoatBooking BB)
        //{
        //    try
        //    {
        //        if (BB.QueryType != null && Convert.ToString(BB.BookingId) != null && Convert.ToString(BB.BookingDate) != null
        //        && Convert.ToString(BB.BoatHouseId) != null && BB.BoatHouseName != null && Convert.ToString(BB.CustomerId) != null
        //        && BB.CustomerMobileNo != null && BB.CustomerName != null && BB.CustomerAddress != null && BB.PremiumStatus != null
        //        && BB.NoOfPass != null && BB.NoOfChild != null && BB.NoOfInfant != null
        //        && Convert.ToString(BB.OfferId) != null && BB.InitBillAmount != null && BB.PaymentType != null && BB.ActualBillAmount != null
        //        && BB.Status != null && BB.BoatTypeId != null && BB.BoatSeaterId != null && BB.BookingDuration != null && BB.InitBoatCharge != null
        //        && BB.InitRowerCharge != null && BB.BoatDeposit != null && BB.TaxDetails != null && BB.InitOfferAmount != null
        //        && BB.InitNetAmount != null && BB.CreatedBy != null
        //        && BB.BFDInitBoatCharge != null && BB.BFDInitNetAmount != null && BB.BFDGST != null)
        //        {
        //            string sReturn = string.Empty;
        //            SqlCommand cmd = new SqlCommand("BoatBookingBulk", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Clear();
        //            cmd.CommandTimeout = 10000000;

        //            //Boat Booking
        //            cmd.Parameters.AddWithValue("@QueryType", BB.QueryType.ToString());
        //            cmd.Parameters.AddWithValue("@BookingId", BB.BookingId.ToString());
        //            cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BB.BookingDate.ToString(), objEnglishDate));
        //            cmd.Parameters.AddWithValue("@BoatHouseId", BB.BoatHouseId.ToString());
        //            cmd.Parameters.AddWithValue("@BoatHouseName", BB.BoatHouseName.ToString());

        //            cmd.Parameters.AddWithValue("@BookingPin", BB.BookingPin.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerId", BB.CustomerId.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerMobileNo", BB.CustomerMobileNo.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerName", BB.CustomerName.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerAddress", BB.CustomerAddress.ToString());

        //            cmd.Parameters.AddWithValue("@PremiumStatus", BB.PremiumStatus.ToString());
        //            cmd.Parameters.AddWithValue("@NoOfPass", BB.NoOfPass.ToString());
        //            cmd.Parameters.AddWithValue("@NoOfChild", BB.NoOfChild.ToString());
        //            cmd.Parameters.AddWithValue("@NoOfInfant", BB.NoOfInfant.ToString());
        //            cmd.Parameters.AddWithValue("@OfferId", BB.OfferId.ToString());

        //            cmd.Parameters.AddWithValue("@InitBillAmount", BB.InitBillAmount.ToString());
        //            cmd.Parameters.AddWithValue("@PaymentType", BB.PaymentType.ToString());
        //            cmd.Parameters.AddWithValue("@ActualBillAmount", BB.ActualBillAmount.ToString());
        //            cmd.Parameters.AddWithValue("@Status", BB.Status.ToString());
        //            cmd.Parameters.AddWithValue("@BoatTypeId", BB.BoatTypeId.ToString());

        //            cmd.Parameters.AddWithValue("@BoatSeaterId", BB.BoatSeaterId.ToString());
        //            cmd.Parameters.AddWithValue("@BookingDuration", BB.BookingDuration.ToString());
        //            cmd.Parameters.AddWithValue("@InitBoatCharge", BB.InitBoatCharge.ToString());
        //            cmd.Parameters.AddWithValue("@InitRowerCharge", BB.InitRowerCharge.ToString());
        //            cmd.Parameters.AddWithValue("@BoatDeposit", BB.BoatDeposit.ToString());

        //            cmd.Parameters.AddWithValue("@TaxDetails", BB.TaxDetails.ToString());
        //            cmd.Parameters.AddWithValue("@InitOfferAmount", BB.InitOfferAmount.ToString());
        //            cmd.Parameters.AddWithValue("@InitNetAmount", BB.InitNetAmount.ToString());
        //            cmd.Parameters.AddWithValue("@BookingMedia", BB.BookingMedia.ToString());
        //            cmd.Parameters.AddWithValue("@CreatedBy", BB.CreatedBy.ToString());

        //            // Other Service Booking
        //            cmd.Parameters.AddWithValue("@OthServiceStatus", BB.OthServiceStatus.ToString());
        //            cmd.Parameters.AddWithValue("@OthServiceId", BB.OthServiceId.ToString());
        //            cmd.Parameters.AddWithValue("@OthChargePerItem", BB.OthChargePerItem.ToString());
        //            cmd.Parameters.AddWithValue("@OthNoOfItems", BB.OthNoOfItems.ToString());

        //            cmd.Parameters.AddWithValue("@OthTaxDetails", BB.OthTaxDetails.ToString());
        //            cmd.Parameters.AddWithValue("@OthNetAmount", BB.OthNetAmount.ToString());
        //            cmd.Parameters.AddWithValue("@BFDInitBoatCharge", BB.BFDInitBoatCharge.ToString());
        //            cmd.Parameters.AddWithValue("@BFDInitNetAmount", BB.BFDInitNetAmount.ToString());
        //            cmd.Parameters.AddWithValue("@BFDGST", BB.BFDGST.ToString());

        //            SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
        //            RuturnValue.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(RuturnValue);
        //            con.Open();

        //            cmd.ExecuteNonQuery();
        //            sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
        //            string[] sResult = sReturn.Split('~');
        //            con.Close();

        //            if (sResult[0].Trim() == "Success")
        //            {
        //                BoatBookingStr TxMstr = new BoatBookingStr
        //                {
        //                    Response = sResult[1].Trim() + '~' + sResult[2].Trim(),
        //                    StatusCode = 1
        //                };
        //                return Ok(TxMstr);
        //            }
        //            else
        //            {
        //                BoatBookingStr TxMstr = new BoatBookingStr
        //                {
        //                    Response = sResult[1].Trim(),
        //                    StatusCode = 0
        //                };
        //                return Ok(TxMstr);
        //            }
        //        }
        //        else
        //        {
        //            BoatBookingStr TxMstr = new BoatBookingStr
        //            {
        //                Response = "Must Pass All Parameters",
        //                StatusCode = 0
        //            };
        //            return Ok(TxMstr);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BoatBookingStr TxMstr = new BoatBookingStr
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(TxMstr);
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("OfflineBoatBookingService")]
        //public IHttpActionResult OfflineBoatBookingService([FromBody] BoatBooking BB)
        //{
        //    try
        //    {
        //        if (BB.QueryType != null && Convert.ToString(BB.BookingId) != null && Convert.ToString(BB.BookingDate) != null
        //        && Convert.ToString(BB.BoatHouseId) != null && BB.BoatHouseName != null && Convert.ToString(BB.CustomerId) != null
        //        && BB.CustomerMobileNo != null && BB.CustomerName != null && BB.CustomerAddress != null && BB.PremiumStatus != null
        //        && BB.NoOfPass != null && BB.NoOfChild != null && BB.NoOfInfant != null
        //        && Convert.ToString(BB.OfferId) != null && BB.InitBillAmount != null && BB.PaymentType != null && BB.ActualBillAmount != null
        //        && BB.Status != null && BB.BoatTypeId != null && BB.BoatSeaterId != null && BB.BookingDuration != null && BB.InitBoatCharge != null
        //        && BB.InitRowerCharge != null && BB.BoatDeposit != null && BB.TaxDetails != null && BB.InitOfferAmount != null
        //        && BB.InitNetAmount != null && BB.CreatedBy != null)
        //        {
        //            string sReturn = string.Empty;
        //            SqlCommand cmd = new SqlCommand("OfflineBoatBooking", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Clear();
        //            cmd.CommandTimeout = 10000000;

        //            //Boat Booking
        //            cmd.Parameters.AddWithValue("@QueryType", BB.QueryType.ToString());
        //            cmd.Parameters.AddWithValue("@BookingId", BB.BookingId.ToString());
        //            cmd.Parameters.AddWithValue("@BookingDate", DateTime.Parse(BB.BookingDate.ToString(), objEnglishDate));
        //            cmd.Parameters.AddWithValue("@BoatHouseId", BB.BoatHouseId.ToString());
        //            cmd.Parameters.AddWithValue("@BoatHouseName", BB.BoatHouseName.ToString());

        //            cmd.Parameters.AddWithValue("@BookingPin", BB.BookingPin.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerId", BB.CustomerId.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerMobileNo", BB.CustomerMobileNo.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerName", BB.CustomerName.ToString());
        //            cmd.Parameters.AddWithValue("@CustomerAddress", BB.CustomerAddress.ToString());
        //            cmd.Parameters.AddWithValue("@PremiumStatus", BB.PremiumStatus.ToString());

        //            cmd.Parameters.AddWithValue("@NoOfPass", BB.NoOfPass.ToString());
        //            cmd.Parameters.AddWithValue("@NoOfChild", BB.NoOfChild.ToString());
        //            cmd.Parameters.AddWithValue("@NoOfInfant", BB.NoOfInfant.ToString());
        //            cmd.Parameters.AddWithValue("@OfferId", BB.OfferId.ToString());

        //            cmd.Parameters.AddWithValue("@InitBillAmount", BB.InitBillAmount.ToString());
        //            cmd.Parameters.AddWithValue("@PaymentType", BB.PaymentType.ToString());
        //            cmd.Parameters.AddWithValue("@ActualBillAmount", BB.ActualBillAmount.ToString());
        //            cmd.Parameters.AddWithValue("@Status", BB.Status.ToString());
        //            cmd.Parameters.AddWithValue("@BoatTypeId", BB.BoatTypeId.ToString());

        //            cmd.Parameters.AddWithValue("@BoatSeaterId", BB.BoatSeaterId.ToString());
        //            cmd.Parameters.AddWithValue("@BookingDuration", BB.BookingDuration.ToString());
        //            cmd.Parameters.AddWithValue("@InitBoatCharge", BB.InitBoatCharge.ToString());
        //            cmd.Parameters.AddWithValue("@InitRowerCharge", BB.InitRowerCharge.ToString());
        //            cmd.Parameters.AddWithValue("@BoatDeposit", BB.BoatDeposit.ToString());

        //            cmd.Parameters.AddWithValue("@TaxDetails", BB.TaxDetails.ToString());
        //            cmd.Parameters.AddWithValue("@InitOfferAmount", BB.InitOfferAmount.ToString());
        //            cmd.Parameters.AddWithValue("@InitNetAmount", BB.InitNetAmount.ToString());

        //            cmd.Parameters.AddWithValue("@BookingMedia", BB.BookingMedia.ToString());
        //            cmd.Parameters.AddWithValue("@CreatedBy", BB.CreatedBy.ToString());

        //            // Other Service Booking
        //            cmd.Parameters.AddWithValue("@OthServiceStatus", BB.OthServiceStatus.ToString());
        //            cmd.Parameters.AddWithValue("@OthServiceId", BB.OthServiceId.ToString());
        //            cmd.Parameters.AddWithValue("@OthChargePerItem", BB.OthChargePerItem.ToString());
        //            cmd.Parameters.AddWithValue("@OthNoOfItems", BB.OthNoOfItems.ToString());

        //            cmd.Parameters.AddWithValue("@OthTaxDetails", BB.OthTaxDetails.ToString());
        //            cmd.Parameters.AddWithValue("@OthNetAmount", BB.OthNetAmount.ToString());

        //            SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
        //            RuturnValue.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(RuturnValue);
        //            con.Open();

        //            cmd.ExecuteNonQuery();
        //            sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
        //            string[] sResult = sReturn.Split('~');
        //            con.Close();

        //            if (sResult[0].Trim() == "Success")
        //            {
        //                BoatBookingStr TxMstr = new BoatBookingStr
        //                {
        //                    Response = sResult[1].Trim() + '~' + sResult[2].Trim(),
        //                    StatusCode = 1
        //                };
        //                return Ok(TxMstr);
        //            }
        //            else
        //            {
        //                BoatBookingStr TxMstr = new BoatBookingStr
        //                {
        //                    Response = sResult[1].Trim(),
        //                    StatusCode = 0
        //                };
        //                return Ok(TxMstr);
        //            }
        //        }
        //        else
        //        {
        //            BoatBookingStr TxMstr = new BoatBookingStr
        //            {
        //                Response = "Must Pass All Parameters",
        //                StatusCode = 0
        //            };
        //            return Ok(TxMstr);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BoatBookingStr TxMstr = new BoatBookingStr
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(TxMstr);
        //    }
        //}


        //[HttpPost]
        //[AllowAnonymous]
        //[Route("Cancellation")]
        //public IHttpActionResult Cancellation([FromBody] CancelReschedMstr Cancel)
        //{
        //    try
        //    {
        //        if (Cancel.QueryType != null && Cancel.ActivityType != null && Cancel.BoatHouseId != null
        //            && Convert.ToString(Cancel.BookingId) != null)
        //        {
        //            List<CancelReschedMstr> li = new List<CancelReschedMstr>();
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("EXECUTE [dbo].[MstrCancelBooking]   '" + Cancel.QueryType.Trim() + "'," + Cancel.BookingId.ToString() + ", " + Cancel.BoatHouseId.Trim() + ", '" + Cancel.ActivityType.Trim() + "' ", con);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataSet dt = new DataSet();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Tables[0].Rows.Count > 0)
        //            {
        //                if (dt.Tables[0].Rows[0]["Status"].ToString().Trim() == "Success")
        //                {

        //                    CancelReschedMstr CancelBooking = new CancelReschedMstr();
        //                    CancelBooking.ActivityId = dt.Tables[0].Rows[0]["ActivityId"].ToString();
        //                    CancelBooking.Description = dt.Tables[0].Rows[0]["Description"].ToString();
        //                    CancelBooking.BoatHouseId = dt.Tables[0].Rows[0]["BoatHouseId"].ToString();
        //                    CancelBooking.BoatHouseName = dt.Tables[0].Rows[0]["BoatHouseName"].ToString();
        //                    CancelBooking.ActivityType = dt.Tables[0].Rows[0]["ActivityType"].ToString();
        //                    CancelBooking.ChargeType = dt.Tables[0].Rows[0]["ChargeType"].ToString();
        //                    CancelBooking.Charges = dt.Tables[0].Rows[0]["Charges"].ToString();
        //                    CancelBooking.EffectiveFrom = dt.Tables[0].Rows[0]["EffectiveFrom"].ToString();
        //                    CancelBooking.EffectiveTill = dt.Tables[0].Rows[0]["EffectiveTill"].ToString();
        //                    CancelBooking.ApplicableBefore = dt.Tables[0].Rows[0]["ApplicableBefore"].ToString();
        //                    CancelBooking.InitBillAmount = Convert.ToDecimal(dt.Tables[0].Rows[0]["InitBillAmount"].ToString());
        //                    CancelBooking.CustomerName = dt.Tables[0].Rows[0]["CustomerName"].ToString();
        //                    CancelBooking.MobileNo = dt.Tables[0].Rows[0]["CustomerMobile"].ToString();
        //                    CancelBooking.BookingDate = dt.Tables[0].Rows[0]["BookingDate"].ToString();
        //                    li.Add(CancelBooking);
        //                    CancelReschedMstrList ConfList = new CancelReschedMstrList
        //                    {
        //                        Response = li,
        //                        StatusCode = 1
        //                    };
        //                    return Ok(ConfList);
        //                }
        //                else
        //                {
        //                    CancelReschedMstrRes ConfRes1 = new CancelReschedMstrRes
        //                    {
        //                        Response = dt.Tables[0].Rows[0]["Status"].ToString().Trim(),
        //                        StatusCode = 1
        //                    };
        //                    return Ok(ConfRes1);
        //                }



        //            }
        //            else
        //            {
        //                CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
        //                {
        //                    Response = dt.Tables[1].Rows[0]["Status"].ToString().Trim(),
        //                    StatusCode = 0
        //                };
        //                return Ok(ConfRes);
        //            }
        //        }
        //        else
        //        {
        //            CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
        //            {
        //                Response = "Must Pass All Parameters",
        //                StatusCode = 0
        //            };
        //            return Ok(ConfRes);

        //        }

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(ConfRes);
        //    }
        //}


        /// <summary>
        /// Modified By : Imran
        /// Modified Date : 18-10-2021
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PublicBookingHistory")]
        public IHttpActionResult PublicBookingHistory([FromBody] BoatBooking Cancel)
        {
            try
            {
                if (Convert.ToString(Cancel.UserId) != null)
                {
                    List<BoatBooking> li = new List<BoatBooking>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT A.BookingId,A.BookingPin,B.PaymentType 'PaymentTypeId',A.BoatHouseId,A.BoatHouseName,B.CustomerName, B.CustomerMobile, "
                   + " convert(varchar(10), B.BookingDate, 101) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate', "
                   + " convert(varchar(10), A.CancelledDate, 101) + right(convert(varchar(32), A.CancelledDate, 100), 8) AS 'CancelledDate', "
                   + " C.ConfigName 'PaymentType', ISNULL(SUM(A.initNetAmount), 0) 'initNetAmount', ISNULL(SUM(A.BoatDeposit), 0) 'BoatDeposit', "
                   + " ISNULL(SUM(A.initBoatCharge), 0) 'initBoatCharge', ISNULL(SUM(A.InitRowerCharge), 0) 'InitRowerCharge', B.UserId, "
                   + " CASE WHEN B.PremiumStatus = 'P' THEN 'Premium' ELSE 'Normal' END AS PremiumStatus, "
                   + " BS.SeaterType, BT.BoatType,STUFF(RIGHT(' ' + CONVERT(VARCHAR(30),D.SlotStartTime,100),7),6,0,' ') AS 'SlotTime' "
                   + " FROM BookingDtl AS A "
                   + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                   + " INNER JOIN ConfigurationMaster AS C ON B.PaymentType = C.ConfigId AND A.BoatHouseId = B.BoatHouseId AND C.TypeID = 20 "
                   + " INNER JOIN BoatSeat AS Bs ON A.BoatSeaterId = Bs.BoatSeaterId AND A.BoatHouseId = Bs.BoatHouseId "
                   + " INNER JOIN BoatTypes AS Bt ON A.BoatTypeId = Bt.BoatTypeId AND A.BoatHouseId = Bt.BoatHouseId "
                   + " INNER JOIN BoatSlotMaster AS D ON A.BoatHouseId = D.BoatHouseId AND D.ActiveStatus='A' AND A.BoatTypeId=D.BoatTypeId AND A.BoatSeaterId = D.BoatSeaterId AND A.TimeSlotId = D.SlotId "
                   + " WHERE A.Status IN('C') AND B.UserId = @UserId "
                   + " GROUP BY A.BookingId, B.UserId, B.CustomerMobile, B.CustomerName, B.BookingDate, C.ConfigName, A.BoatHouseName, "
                   + " A.BoatHouseId, A.BookingPin, B.PaymentType, B.PremiumStatus, A.CancelledDate, BS.SeaterType, BT.BoatType,D.SlotStartTime ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@UserId"].Value = Cancel.UserId.Trim();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            BoatBooking CancelBooking = new BoatBooking();
                            CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
                            CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            CancelBooking.PaymentTypeId = dt.Rows[i]["PaymentTypeId"].ToString();
                            CancelBooking.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.CustomerMobileNo = dt.Rows[i]["CustomerMobile"].ToString();
                            CancelBooking.InitNetAmount = dt.Rows[i]["initNetAmount"].ToString();
                            CancelBooking.InitBoatCharge = dt.Rows[i]["initBoatCharge"].ToString();
                            CancelBooking.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            CancelBooking.UserId = dt.Rows[i]["UserId"].ToString();
                            CancelBooking.CancelledDate = dt.Rows[i]["CancelledDate"].ToString();
                            CancelBooking.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            CancelBooking.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            CancelBooking.TimeSlot = dt.Rows[i]["SlotTime"].ToString();

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
                            Response = "No Booking Details Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                    {
                        Response = "Must Pass User Id",
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("PublicBookingHistory")]
        //public IHttpActionResult PublicBookingHistory([FromBody] BoatBooking Cancel)
        //{
        //    try
        //    {
        //        if (Convert.ToString(Cancel.UserId) != null)
        //        {
        //            List<BoatBooking> li = new List<BoatBooking>();
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT A.BookingId,B.BookingPin,B.PaymentType 'PaymentTypeId',A.BoatHouseId,A.BoatHouseName,B.CustomerName, B.CustomerMobile, "
        //           + "  convert(varchar(10), B.BookingDate, 101) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate', "
        //           + "  convert(varchar(10), A.CancelledDate, 101) + right(convert(varchar(32), A.CancelledDate, 100), 8) AS 'CancelledDate', "
        //           + " C.ConfigName 'PaymentType', ISNULL(SUM(A.initNetAmount), 0) 'initNetAmount', ISNULL(SUM(A.BoatDeposit), 0) 'BoatDeposit', "
        //           + "  ISNULL(SUM(A.initBoatCharge), 0) 'initBoatCharge', ISNULL(SUM(A.InitRowerCharge), 0) 'InitRowerCharge', B.UserId, "
        //           + " CASE WHEN B.PremiumStatus='P' THEN 'Premium' ELSE 'Normal' END AS PremiumStatus  FROM BookingDtl AS A "
        //           + "  INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
        //           + " INNER JOIN ConfigurationMaster AS C ON B.PaymentType = C.ConfigId AND A.BoatHouseId = B.BoatHouseId  AND C.TypeID = 20 "
        //           + " WHERE  A.Status IN('C') AND  B.UserId ='" + Cancel.UserId.Trim() + "'  "
        //           + "  GROUP BY A.BookingId, B.UserId, B.CustomerMobile, B.CustomerName, B.BookingDate, C.ConfigName, A.BoatHouseName,A.BoatHouseId,B.BookingPin,B.PaymentType,B.PremiumStatus,A.CancelledDate ", con);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {

        //                    BoatBooking CancelBooking = new BoatBooking();
        //                    CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
        //                    CancelBooking.PaymentTypeId = dt.Rows[i]["PaymentTypeId"].ToString();
        //                    CancelBooking.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
        //                    CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                    CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
        //                    CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
        //                    CancelBooking.CustomerMobileNo = dt.Rows[i]["CustomerMobile"].ToString();
        //                    CancelBooking.InitNetAmount = dt.Rows[i]["initNetAmount"].ToString();
        //                    CancelBooking.InitBoatCharge = dt.Rows[i]["initBoatCharge"].ToString();
        //                    CancelBooking.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
        //                    CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
        //                    CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
        //                    CancelBooking.UserId = dt.Rows[i]["UserId"].ToString();
        //                    CancelBooking.CancelledDate = dt.Rows[i]["CancelledDate"].ToString();

        //                    li.Add(CancelBooking);
        //                }
        //                BoatBookingList ConfList = new BoatBookingList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(ConfList);
        //            }


        //            else
        //            {
        //                CancelReschedMstrRes ConfRes1 = new CancelReschedMstrRes
        //                {
        //                    Response = "No Booking Details Found",
        //                    StatusCode = 0
        //                };
        //                return Ok(ConfRes1);
        //            }
        //        }

        //        else
        //        {
        //            CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
        //            {
        //                Response = "Must Pass User Id",
        //                StatusCode = 0
        //            };
        //            return Ok(ConfRes);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(ConfRes);
        //    }

        //}


        /***********************************Boat Booking Trip Sheet**************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet")]
        public IHttpActionResult BookingTripSheet([FromBody] TripSheet Trip)
        {
            try
            {

                if (Trip.QueryType != null && Trip.BookingId != null && Convert.ToString(Trip.BoatReferenceNo) != null && Convert.ToString(Trip.BoatHouseId) != null
                    && Trip.BoatHouseName != null && Convert.ToString(Trip.BoatId) != null && Convert.ToString(Trip.BoatNum) != null
                    && Trip.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("TripSheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Trip.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", Trip.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatReferenceNo", Trip.BoatReferenceNo.ToString());
                    cmd.Parameters.AddWithValue("@RowerId", Trip.RowerId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", Trip.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@BoatId", Trip.BoatId.ToString());
                    cmd.Parameters.AddWithValue("@BoatNum", Trip.BoatNum.ToString());
                    if (Trip.QueryType == "DW")
                    {
                        cmd.Parameters.AddWithValue("@TripStartTime", DateTime.Parse(Trip.TripStartTime.ToString(), objEnglishDate));
                        cmd.Parameters.AddWithValue("@TripEndTime", DateTime.Parse(Trip.TripEndTime.ToString(), objEnglishDate));
                    }
                    cmd.Parameters.AddWithValue("@CreatedBy", Trip.CreatedBy.Trim());

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
        [Route("TripSheet/GetBoatId")]
        public IHttpActionResult getBoatIdBoatNum([FromBody] TripSheet Trip)
        {
            try
            {
                if (Convert.ToString(Trip.BoatHouseId) != null)
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT BoatId, BoatNum,BoatNum +'~'+ BoatName as 'BoatName' " +
                        "FROM BoatMaster WHERE BoatHouseId= @BoatHouseId ", con);
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
                            TripSheet ShowBoatid = new TripSheet();
                            ShowBoatid.BoatId = dt.Rows[i]["BoatId"].ToString();
                            ShowBoatid.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowBoatid.BoatName = dt.Rows[i]["BoatName"].ToString();

                            li.Add(ShowBoatid);
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
        [Route("TripSheet/ListAll")]
        public IHttpActionResult getTripSheet([FromBody] TripSheet TripSheet)
        {
            try
            {
                if (TripSheet.BillQRCode != null)
                {
                    List<GridTripSheet> li = new List<GridTripSheet>();

                    SqlCommand cmd = new SqlCommand("SELECT BookingId,A.BoatHouseName,InitBoatCharge AS 'BoatCharge',ISNULL(InitRowerCharge,0) AS 'InitRowerCharge',"
                        + " ISNULL(ActualRowerCharge,0) AS 'ActualRowerCharge',ActualBoatNum as 'BoatNumber',ActualBoatId as 'BoatId', "
                        + " B.BoatName,ISNULL(convert(varchar(10), TripStartTime, 103) + right(convert(varchar(32), TripStartTime, 100), 8), '-')  "
                        + " AS 'TripStartTime',ISNULL(convert(varchar(10), TripEndTime, 103) + right(convert(varchar(32), TripEndTime, 100), 8), '-') AS 'TripEndTime', A.BillQRCode "
                        + " FROM BookingDtl as A INNER JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId "
                        + " WHERE(TripStartTime IS NOT NULL OR TripStartTime != '') AND A.BillQRCode = @BillQRCode", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BillQRCode", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BillQRCode"].Value = TripSheet.BillQRCode.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            GridTripSheet ts = new GridTripSheet();
                            ts.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ts.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            ts.BoatCharge = dt.Rows[i]["BoatCharge"].ToString();
                            ts.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
                            ts.ActualRowerCharge = dt.Rows[i]["ActualRowerCharge"].ToString();
                            ts.BoatNumber = dt.Rows[i]["BoatNumber"].ToString();
                            ts.BoatId = dt.Rows[i]["BoatId"].ToString();
                            ts.BoatName = dt.Rows[i]["BoatName"].ToString();
                            ts.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            ts.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            ts.BillQRCode = dt.Rows[i]["BillQRCode"].ToString();
                            li.Add(ts);
                        }
                        GridTripSheetList ItemMasters = new GridTripSheetList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        GridTripSheetRes ItemMasters1 = new GridTripSheetRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }

                }
                else
                {

                    TripSheetRes ItemMasters1 = new TripSheetRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
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

        /********************************* Boat Booking & Booking Other Service Abstract **************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("AbstractBoatBooking")]
        public IHttpActionResult AbstractBoatBooking([FromBody] BookingHeaderAbs Trip)
        {
            try
            {

                if (Trip.FromDate != null && Trip.BoatHouseId != null)
                {
                    List<BookingHeaderAbs> li = new List<BookingHeaderAbs>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "AbstractBoatBooking");
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Trip.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Trip.ToDate.Trim(), objEnglishDate));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BookingHeaderAbs ShowBoatid = new BookingHeaderAbs();
                            ShowBoatid.UserId = dt.Rows[i]["UserId"].ToString();
                            ShowBoatid.UserName = dt.Rows[i]["UserName"].ToString();
                            ShowBoatid.NoCount = dt.Rows[i]["Count"].ToString();
                            ShowBoatid.BoatCharge = dt.Rows[i]["Charge"].ToString();
                            ShowBoatid.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            ShowBoatid.TotalAmount = dt.Rows[i]["NetAmount"].ToString();

                            li.Add(ShowBoatid);
                        }

                        BookingHeaderAbsList ConfList = new BookingHeaderAbsList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BookingHeaderAbsRes ConfRes = new BookingHeaderAbsRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BookingHeaderAbsRes Vehicle = new BookingHeaderAbsRes
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


        [HttpPost]
        [AllowAnonymous]
        [Route("UserBoatBookingDtl")]
        public IHttpActionResult UserBoatBookingDtl([FromBody] BookingHeaderAbs Trip)
        {
            try
            {

                if (Trip.FromDate != null && Trip.BoatHouseId != null && Trip.UserId != null)
                {
                    List<BookingHeaderAbs> li = new List<BookingHeaderAbs>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "UserBoatBookingDtl");
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Trip.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Trip.ToDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@UserId", Trip.UserId.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BookingHeaderAbs ShowBoatid = new BookingHeaderAbs();
                            ShowBoatid.BookingId = dt.Rows[i]["BookingId"].ToString();
                            ShowBoatid.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            ShowBoatid.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowBoatid.BoatType = dt.Rows[i]["BoatType"].ToString();
                            ShowBoatid.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowBoatid.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            ShowBoatid.NoCount = dt.Rows[i]["Count"].ToString();
                            ShowBoatid.BoatCharge = dt.Rows[i]["Charge"].ToString();
                            ShowBoatid.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            ShowBoatid.TotalAmount = dt.Rows[i]["NetAmount"].ToString();

                            li.Add(ShowBoatid);
                        }

                        BookingHeaderAbsList ConfList = new BookingHeaderAbsList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BookingHeaderAbsRes ConfRes = new BookingHeaderAbsRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BookingHeaderAbsRes Vehicle = new BookingHeaderAbsRes
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

        //Booking Other Services
        [HttpPost]
        [AllowAnonymous]
        [Route("AbstractOtherSvc")]
        public IHttpActionResult BookingOtherservices([FromBody] AbstractOtherService Trip)
        {
            try
            {
                if (Trip.FromDate != null && Trip.BoatHouseId != null)
                {
                    List<AbstractOtherService> li = new List<AbstractOtherService>();
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "AbstractOtherService");
                    cmd.Parameters.AddWithValue("@BoatHouseId", Trip.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(Trip.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(Trip.ToDate.Trim(), objEnglishDate));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AbstractOtherService ShowBoatid = new AbstractOtherService();
                            ShowBoatid.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            ShowBoatid.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            ShowBoatid.NoCount = dt.Rows[i]["Count"].ToString();
                            ShowBoatid.Amount = dt.Rows[i]["Amount"].ToString();
                            li.Add(ShowBoatid);
                        }

                        AbstractOtherServiceList ConfList = new AbstractOtherServiceList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        AbstractOtherServiceRes ConfRes = new AbstractOtherServiceRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    AbstractOtherServiceRes Vehicle = new AbstractOtherServiceRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                AbstractOtherServiceRes ConfRes = new AbstractOtherServiceRes
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

        /***********************************Online Boat Booking Before Transaction**************************************/

        /***********************************TripSheet Web******************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheetweb/ListAll")]
        public IHttpActionResult gettripsheet([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null)
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand(" SELECT D.BookingId, A.BoatHouseId,A.BoatHouseName,A.BoatReferenceNo, "
                        + " A.BoatTypeId, E.BoatType, A.BoatSeaterId, F.SeaterType,  "
                        + " B.BoatId, B.BoatNum + '~' + B.BoatName as BoatName, C.RowerId, "
                        + " C.RowerName, CONVERT(NVARCHAR(50), A.TripStartTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripStartTime, 22), 10, 11) AS TripStartTime, "
                        + " CONVERT(NVARCHAR(50), A.TripEndTime, 105) + ' ' + SUBSTRING(CONVERT(varchar(20), A.TripEndTime, 22), 10, 11) AS TripEndTime FROM BookingDtl as A "
                        + " INNER JOIN BookingHdr AS D On D.BookingId = A.BookingId AND D.BoatHouseId = A.BoatHouseId "
                        + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId "
                        + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId "
                        + " INNER JOIN BoatTypes AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatHouseId = E.BoatHouseId "
                        + " INNER JOIN BoatSeat AS F ON A.BoatSeaterId = F.BoatSeaterId AND A.BoatHouseId = F.BoatHouseId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND CAST(D.BookingDate As Date) = CAST(GETDATE() AS date) AND A.Status IN('B','R') ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();

                            tripsheets.BookingId = dt.Rows[i]["BookingId"].ToString();
                            tripsheets.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            tripsheets.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            tripsheets.BoatType = dt.Rows[i]["BoatType"].ToString();
                            tripsheets.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            tripsheets.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();

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
        [Route("TripSheetweb/ReferenceNum")]
        public IHttpActionResult getrefernum([FromBody] TripSheetWeb bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != null && bHMstr.BoatReferenceNo != null)
                {
                    List<TripSheetWeb> li = new List<TripSheetWeb>();

                    SqlCommand cmd = new SqlCommand("SELECT A.BoatReferenceNo , B.BoatId,B.BoatNum + '~'+B.BoatName as BoatName,C.RowerId, "
                        + " C.RowerName, A.TripStartTime , A.TripEndTime FROM BookingDtl as A "
                        + " LEFT JOIN BoatMaster AS B ON B.BoatId = A.ActualBoatId "
                        + " LEFT JOIN RowerMaster As C on A.RowerId = C.RowerId "
                        + " WHERE A.BoatHouseId = @BoatHouseId AND A.BoatReferenceNo = @BoatReferenceNo ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatReferenceNo", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BoatReferenceNo"].Value = bHMstr.BoatReferenceNo.Trim();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheetWeb tripsheets = new TripSheetWeb();


                            tripsheets.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            tripsheets.BoatId = dt.Rows[i]["BoatId"].ToString();

                            tripsheets.BoatName = dt.Rows[i]["BoatName"].ToString();
                            tripsheets.RowerId = dt.Rows[i]["RowerId"].ToString();
                            tripsheets.RowerName = dt.Rows[i]["RowerName"].ToString();
                            tripsheets.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            tripsheets.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();

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
        [Route("TripSheetWeb/GetBoatId")]
        public IHttpActionResult getBoatIdBoatType([FromBody] TripSheet Trip)
        {
            try
            {
                if (Convert.ToString(Trip.BoatHouseId) != null)
                {
                    List<TripSheet> li = new List<TripSheet>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT BoatId, BoatNum, BoatNum +'~'+ BoatName AS 'BoatName' FROM BoatMaster "
                        + " WHERE BoatHouseId = @BoatHouseId AND BoatTypeId = @BoatTypeId", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = Trip.BoatTypeId.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripSheet ShowBoatid = new TripSheet();
                            ShowBoatid.BoatId = dt.Rows[i]["BoatId"].ToString();
                            ShowBoatid.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowBoatid.BoatName = dt.Rows[i]["BoatName"].ToString();

                            li.Add(ShowBoatid);
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


        /************************************TRIPSHEET SETTELEMENT********************************/


        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/getCustomer")]
        public IHttpActionResult getCustomer([FromBody] TripCustomer customer)
        {
            try
            {
                if (customer.BoatHouseId != null && customer.BoatReferenceNo != null)
                {
                    List<TripCustomer> li = new List<TripCustomer>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(A.ActualBoatId, 0) AS 'BoatId', ISNULL(A.ActualBoatNum, 0) AS 'BoatNumber', ISNULL(A.RowerId, 0) AS 'RowerId', "
                    + " convert(varchar(10), A.TripStartTime, 101) + right(convert(varchar(32), A.TripStartTime, 100), 8) AS 'TripStartTime', "
                    + " convert(varchar(10), A.TripEndTime, 101) + right(convert(varchar(32), A.TripEndTime, 100), 8) AS 'TripEndTime', "
                    + " ISNULL(A.InitNetAmount, 0) AS 'InitNetAmount', ISNULL(A.BoatDeposit, 0) AS 'BoatDeposit', "
                    + " A.BookingId, B.CustomerName, B.CustomerMobile, B.CustomerAddress, B.InitBillAmount, B.ActualBillAmount, C.ConfigName AS 'PaymentType' FROM BookingDtl AS  A "
                    + " JOIN BookingHdr AS B ON A.BookingId = B.BookingId "
                    + " JOIN ConfigurationMaster AS C ON C.ConfigID = B.PaymentType AND C.TypeID = 20 "
                    + " WHERE A.BoatReferenceNo = @BoatReferenceNo AND A.BoatHouseId = @BoatHouseId", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatReferenceNo", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = customer.BoatHouseId.Trim();
                    cmd.Parameters["@BoatReferenceNo"].Value = customer.BoatReferenceNo.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TripCustomer row = new TripCustomer();
                            row.BoatId = dt.Rows[i]["BoatId"].ToString();
                            row.BoatNumber = dt.Rows[i]["BoatNumber"].ToString();
                            row.RowerId = dt.Rows[i]["RowerId"].ToString();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.BookingId = dt.Rows[i]["BookingId"].ToString();
                            row.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            row.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();

                            row.InitBillAmount = dt.Rows[i]["InitBillAmount"].ToString();
                            row.ActualBillAmount = dt.Rows[i]["ActualBillAmount"].ToString();
                            row.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            row.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            row.CustomerAddress = dt.Rows[i]["CustomerAddress"].ToString();

                            li.Add(row);
                        }

                        TripCustomerList ConfList = new TripCustomerList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        TripCustomerRes ConfRes = new TripCustomerRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    TripCustomerRes Vehicle = new TripCustomerRes
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


        [HttpPost]
        [AllowAnonymous]
        [Route("TripSheet/DetailsBasedOnTime")]
        public IHttpActionResult DetailsBasedOnTime([FromBody] BoatCharges enq)
        {

            if (enq.QueryType != null && enq.BoatReferenceNo != null && enq.BoatHouseId != null && enq.TripStartTime != "" && enq.TripEndTime != "")
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("ViewTripSheetSettelementBasedOnTime", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", enq.QueryType.ToString());
                cmd.Parameters.AddWithValue("@BoatReferenceNo", enq.BoatReferenceNo.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseId", enq.BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@TripStartTime", DateTime.Parse(enq.TripStartTime.Trim(), objEnglishDate));
                cmd.Parameters.AddWithValue("@TripEndTime", DateTime.Parse(enq.TripEndTime.Trim(), objEnglishDate));



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

                            li.Add(row);
                        }
                        else if (enq.QueryType == "GetRowerCharge")
                        {
                            BoatCharges row = new BoatCharges();
                            row.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
                            row.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
                            row.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();

                            row.RowerMinCharge = dt.Rows[i]["RowerCharge"].ToString();
                            row.RowerExtnCharge = dt.Rows[i]["RowerECharge"].ToString();


                            row.BoatExtnTime = dt.Rows[i]["BoatExtnTime"].ToString();
                            row.BoatGraceTime = dt.Rows[i]["BoatGraceTime"].ToString();
                            row.Totaltime = dt.Rows[i]["Totaltime"].ToString();

                            row.ActualNetAmt = dt.Rows[i]["ActualNetAmt"].ToString();

                            row.ExtensionChargePerMin = dt.Rows[i]["ExtensionChargePerMin"].ToString();

                            row.TimeDiff = dt.Rows[i]["TimeDiff"].ToString();
                            row.ExtraTime = dt.Rows[i]["ExtraTime"].ToString();
                            row.ExtraExtCharge = dt.Rows[i]["ExtraExtCharge"].ToString();
                            row.TotalNetAmount = dt.Rows[i]["TotalNetAmount"].ToString();

                            li.Add(row);
                        }
                        else if (enq.QueryType == "GetOfferAmount")
                        {
                            BoatCharges row = new BoatCharges();
                            row.OfferAmount = dt.Rows[i]["OfferAmount"].ToString();
                            li.Add(row);

                        }
                        else if (enq.QueryType == "GetNetAmount")
                        {
                            BoatCharges row = new BoatCharges();
                            row.ActualNetAmt = dt.Rows[i]["Total"].ToString();
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
                    if (enq.QueryType == "GetBoatCharge")
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

        [HttpPost]
        [AllowAnonymous]
        [Route("getCustomerName")]
        public IHttpActionResult getCustomerName([FromBody] TripSheet Trip)
        {
            try
            {
                if (Convert.ToString(Trip.BoatHouseId) != null)
                {
                    List<EmployeeMaster> li = new List<EmployeeMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT EmpId , EmpFirstName , EmpLastName  , UserName  FROM EmpMaster WHERE BoatHouseId= @BoatHouseId", con);
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
                            EmployeeMaster ShowBoatid = new EmployeeMaster();
                            ShowBoatid.EmpId = dt.Rows[i]["EmpId"].ToString();
                            ShowBoatid.EmpFirstName = dt.Rows[i]["EmpFirstName"].ToString();
                            ShowBoatid.EmpLastName = dt.Rows[i]["EmpLastName"].ToString();
                            ShowBoatid.UserName = dt.Rows[i]["UserName"].ToString();

                            li.Add(ShowBoatid);
                        }

                        EmployeeMasterList ConfList = new EmployeeMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        EmployeeMasterRes ConfRes = new EmployeeMasterRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    EmployeeMasterRes Vehicle = new EmployeeMasterRes
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


        [HttpPost]
        [AllowAnonymous]
        [Route("bookingAmount")]
        public IHttpActionResult bookingAmount([FromBody] BookingHeader Trip)
        {
            try
            {
                if (Convert.ToString(Trip.BoatHouseId) != null)
                {
                    List<BookingHeader> li = new List<BookingHeader>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT  B.UserName, sum(A.NoOfPass) AS Tickets , sum(C.InitNetAmount) AS TotalAmount  FROM " +
                                                    "BookingHdr AS A inner JOIN EmpMaster AS B ON A.CreatedBy = B.CreatedBy INNER JOIN BookingDtl " +
                                                    " AS C ON C.BookingId = A.BookingId  WHERE C.BoatHouseId = @BoatHouseId GROUP BY  B.UserName", con);
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
                            BookingHeader ShowBoatid = new BookingHeader();
                            ShowBoatid.UserName = dt.Rows[i]["UserName"].ToString();
                            ShowBoatid.NoOfPass = dt.Rows[i]["Tickets"].ToString();
                            ShowBoatid.InitNetAmount = dt.Rows[i]["TotalAmount"].ToString();
                            li.Add(ShowBoatid);
                        }

                        BookingHeaderList ConfList = new BookingHeaderList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BookingHeaderRes ConfRes = new BookingHeaderRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BookingHeaderRes Vehicle = new BookingHeaderRes
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

        // ****** Ticket Printing Instructions Details ******


        // ****** CCAvenue Payment Gateway Details ****** //

        [HttpPost]
        [AllowAnonymous]
        [Route("CCAvenueBankDet")]
        public IHttpActionResult CCAvenueBankDet([FromBody] Common Comm)
        {
            try
            {
                if (Comm.PaymentMode != null)
                {
                    List<Common> li = new List<Common>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT CardBankName, CAST(Sno AS VARCHAR) + '-' + CAST(DataAccept AS VARCHAR) AS 'DataAccept' "
                        + " FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.CCAvenueBankDetails WHERE PaymentMode= @PaymentMode", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@PaymentMode", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@PaymentMode"].Value = Comm.PaymentMode.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Common CancelBooking = new Common();

                            CancelBooking.CardBankName = dt.Rows[i]["CardBankName"].ToString();
                            CancelBooking.DataAccept = dt.Rows[i]["DataAccept"].ToString();

                            li.Add(CancelBooking);
                        }
                        CommonList ConfList = new CommonList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        CommonRes ConfRes1 = new CommonRes
                        {
                            Response = "No Records Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }

                else
                {
                    CommonRes ConfRes = new CommonRes
                    {
                        Response = "Must Pass Payment Mode",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);

                }

            }
            catch (Exception ex)
            {
                CommonRes ConfRes = new CommonRes
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


        //[HttpGet]
        //[AllowAnonymous]
        //[Route("CCAvenueBankRateDetails")]
        //public IHttpActionResult CCAvenueBankRateDetails()
        //{
        //    try
        //    {
        //        List<Common> li = new List<Common>();
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT MasterCreditcard, AmexCreditcard, DebitCardAmount, DebitCardBelow, "
        //            + " DebitCardAbove, HDFCNetBank, "
        //            + " OthersNetBank, CashCard, GST, BankCharges FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.CCAvenueRates ORDER BY CAST(Sno AS INT) DESC", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {

        //                Common CancelBooking = new Common();

        //                CancelBooking.MasterCreditcard = dt.Rows[i]["MasterCreditcard"].ToString();
        //                CancelBooking.AmexCreditcard = dt.Rows[i]["AmexCreditcard"].ToString();
        //                CancelBooking.DebitCardAmount = dt.Rows[i]["DebitCardAmount"].ToString();
        //                CancelBooking.DebitCardBelow = dt.Rows[i]["DebitCardBelow"].ToString();
        //                CancelBooking.DebitCardAbove = dt.Rows[i]["DebitCardAbove"].ToString();
        //                CancelBooking.HDFCNetBank = dt.Rows[i]["HDFCNetBank"].ToString();
        //                CancelBooking.OthersNetBank = dt.Rows[i]["OthersNetBank"].ToString();
        //                CancelBooking.CashCard = dt.Rows[i]["CashCard"].ToString();
        //                CancelBooking.GST = dt.Rows[i]["GST"].ToString();
        //                CancelBooking.BankCharges = dt.Rows[i]["BankCharges"].ToString();

        //                li.Add(CancelBooking);
        //            }
        //            CommonList ConfList = new CommonList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(ConfList);
        //        }


        //        else
        //        {
        //            CommonRes ConfRes1 = new CommonRes
        //            {
        //                Response = "No Records Found",
        //                StatusCode = 0
        //            };
        //            return Ok(ConfRes1);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        CommonRes ConfRes = new CommonRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(ConfRes);
        //    }

        //}


        // ****** SMS Refernce Log Details ****** //

        [HttpPost]
        [AllowAnonymous]
        [Route("SMSReferenceLog")]
        public IHttpActionResult getSMSReferenceLog([FromBody] SMSService btSMS)
        {
            try
            {
                if (btSMS.ReferenceNo != null)
                {

                    con.Open();

                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "SMSReference");
                    cmd.Parameters.AddWithValue("@ServiceType", btSMS.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@ReferenceNo", btSMS.ReferenceNo.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TableShow");
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

                //List<SMSService> li = new List<SMSService>();
                //con.Open();
                //SqlCommand cmd = new SqlCommand(" SELECT ServiceType, BookingId, BoatHouseId, BoatHouseName, MobileNo, MediaType "
                //    + " FROM SMSServiceLogDetails "
                //    + " WHERE ReferenceNo = '" + btSMS.ReferenceNo.Trim() + "' AND ServiceType = '" + btSMS.ServiceType.Trim() + "'", con);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //con.Close();
                //if (dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        SMSService Rower = new SMSService();
                //        Rower.ServiceType = dt.Rows[i]["ServiceType"].ToString();
                //        Rower.BookingId = dt.Rows[i]["BookingId"].ToString();
                //        Rower.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                //        Rower.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                //        Rower.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                //        Rower.MediaType = dt.Rows[i]["MediaType"].ToString();
                //        li.Add(Rower);
                //    }

                //    SMSServiceList ConfList = new SMSServiceList
                //    {
                //        Response = li,
                //        StatusCode = 1
                //    };
                //    return Ok(ConfList);
                //}

                //else
                //{
                //    SMSServiceRes ConfRes = new SMSServiceRes
                //    {
                //        Response = "No Records Found.",
                //        StatusCode = 0
                //    };
                //    return Ok(ConfRes);
                //}

            }
            catch (Exception ex)
            {
                SMSServiceRes Vehicle = new SMSServiceRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/ddlGetRowerName")]
        public IHttpActionResult GetRowerName()
        {
            try
            {
                List<RowerMaster> li = new List<RowerMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select RowerId, RowerName from RowerMaster  where ActiveStatus = 'A'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RowerMaster Rower = new RowerMaster();
                        Rower.RowerId = dt.Rows[i]["RowerId"].ToString();
                        Rower.RowerName = dt.Rows[i]["RowerName"].ToString();

                        li.Add(Rower);
                    }

                    RowerMasterList ConfList = new RowerMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    RowerMasterRes ConfRes = new RowerMasterRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
                }
            }
            catch (Exception ex)
            {
                RowerMasterRes ConfRes = new RowerMasterRes
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

        // ****** Rower Charge Details ****** //

        [HttpPost]
        [AllowAnonymous]
        [Route("RowerCharger/ListAll")]
        public IHttpActionResult RowerCharges([FromBody] RowerChargers RowCharges)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(RowCharges.BoatHouseId) != "" && RowCharges.BoatTypeId != "" && RowCharges.RowerId != ""
                    && RowCharges.FromDate != "" && RowCharges.ToDate != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = @BoatHouseId AND A.Status IN ('B', 'R') AND B.Status IN ('B', 'R', 'P') "
                              + " AND A.RowerId > 0 AND A.ActualRowerCharge > 0";

                    if (RowCharges.BoatTypeId != "0")
                    {
                        condition += " AND A.BoatTypeId = @BoatTypeId";
                    }

                    if (RowCharges.RowerId != "0")
                    {
                        condition += " AND A.RowerId= @RowerId";
                    }

                    //condition += " AND CAST(B.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(RowCharges.FromDate.Trim()) + "') AND ('" + DateTime.Parse(RowCharges.ToDate.Trim()) + "')";
                    condition += " AND CAST(B.BookingDate AS DATE) BETWEEN @FromDate AND @ToDate";

                    squery = "SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge FROM BookingDtl AS A"
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + "";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    List<RowerChargers> li = new List<RowerChargers>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = RowCharges.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = RowCharges.BoatTypeId.Trim();
                    cmd.Parameters["@RowerId"].Value = RowCharges.RowerId.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RowerChargers lstRowCharges = new RowerChargers();

                            lstRowCharges.BookingId = dt.Rows[i]["BookingId"].ToString();
                            lstRowCharges.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            lstRowCharges.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            lstRowCharges.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            lstRowCharges.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            lstRowCharges.BoatType = dt.Rows[i]["BoatType"].ToString();
                            lstRowCharges.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            lstRowCharges.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            lstRowCharges.RowerId = dt.Rows[i]["RowerId"].ToString();
                            lstRowCharges.RowerName = dt.Rows[i]["RowerName"].ToString();
                            lstRowCharges.ActualRowerCharge = dt.Rows[i]["ActualRowerCharge"].ToString();
                            li.Add(lstRowCharges);
                        }

                        RowerChargersList BoatRate = new RowerChargersList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        RowerChargersString BoatRate = new RowerChargersString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RowerChargersString Vehicle = new RowerChargersString
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
        /// NEWLY ADDED BY ABHINAYA K        
        /// ADDED DATE 20APR2022
        /// Newly Modified by Imran on 2022-05-24
        /// </summary>
        /// <param name="RowCharges"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerCharger/ListAllV2")]
        public IHttpActionResult RowerChargesV2([FromBody] RowerChargersV2 RowCharges)
        {
            try
            {
                string squery = string.Empty;
                int endcount = Int32.Parse(RowCharges.CountStart.Trim()) + 9;
                if (Convert.ToString(RowCharges.BoatHouseId) != "" && RowCharges.BoatTypeId != "" && RowCharges.RowerId != ""
                    && RowCharges.FromDate != "" && RowCharges.ToDate != "")
                {

                    string condition = string.Empty;
                    string condition1 = string.Empty;
                    condition = " WHERE A.BoatHouseId =@BoatHouseId AND A.Status IN ('B', 'R') AND B.Status IN ('B', 'R', 'P') "
                              + " AND A.RowerId > 0 AND A.ActualRowerCharge > 0";

                    if (RowCharges.BoatTypeId != "0")
                    {
                        condition += " AND A.BoatTypeId = @BoatTypeId";
                    }

                    if (RowCharges.RowerId != "0")
                    {
                        condition += " AND A.RowerId=@RowerId";
                    }

                    //condition += " AND CAST(B.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(RowCharges.FromDate.Trim()) + "') AND ('" + DateTime.Parse(RowCharges.ToDate.Trim()) + "')";
                    condition += " AND CAST(B.BookingDate AS DATE) BETWEEN @FromDate AND @ToDate ";
                    condition1 = ") AS A) AS B where B.RowNumber BETWEEN @CountStart AND '" + endcount + "' ORDER BY B.RowNumber ASC ";

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
                    if (DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.OrderDate ASC) 'RowNumber', * FROM ( "
                        + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtl AS A"
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + " " + condition1 + "";
                    }
                    else if (DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                       && DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.OrderDate ASC) 'RowNumber', * FROM ( "
                        + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtlHistory AS A"
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + " " + condition1 + "";
                    }
                    else
                    {
                        squery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.OrderDate ASC) 'RowNumber', * FROM ( "
                        + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtl AS A"
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + ""
                        + " UNION ALL"
                        + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtlHistory AS A"
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + " " + condition1 + "";
                    }
                    SqlCommand cmd = new SqlCommand(squery, con);
                    List<RowerChargersV2> li = new List<RowerChargersV2>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);


                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = RowCharges.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = RowCharges.BoatTypeId.Trim();
                    cmd.Parameters["@RowerId"].Value = RowCharges.RowerId.Trim();
                    cmd.Parameters["@CountStart"].Value = RowCharges.CountStart.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RowerChargersV2 lstRowCharges = new RowerChargersV2();

                            lstRowCharges.BookingId = dt.Rows[i]["BookingId"].ToString();
                            lstRowCharges.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            lstRowCharges.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            lstRowCharges.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            lstRowCharges.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            lstRowCharges.BoatType = dt.Rows[i]["BoatType"].ToString();
                            lstRowCharges.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            lstRowCharges.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            lstRowCharges.RowerId = dt.Rows[i]["RowerId"].ToString();
                            lstRowCharges.RowerName = dt.Rows[i]["RowerName"].ToString();
                            lstRowCharges.ActualRowerCharge = dt.Rows[i]["ActualRowerCharge"].ToString();
                            lstRowCharges.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(lstRowCharges);
                        }

                        RowerChargersV2List BoatRate = new RowerChargersV2List
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        RowerChargersV2String BoatRate = new RowerChargersV2String
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RowerChargersV2String Vehicle = new RowerChargersV2String
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
        ///// NEWLY ADDED BY ABHINAYA K
        ///// ADDED DATE 20APR2022
        ///// </summary>
        ///// <param name="RowCharges"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RowerCharger/ListAllPinV2")]
        //public IHttpActionResult RowerChargesPinV2([FromBody] RowerChargersV2 RowCharges)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (Convert.ToString(RowCharges.BoatHouseId) != "" && RowCharges.BoatTypeId != "" && RowCharges.RowerId != ""
        //            && RowCharges.FromDate != "" && RowCharges.ToDate != "")
        //        {

        //            string condition = string.Empty;

        //            condition = " WHERE A.BoatHouseId =" + RowCharges.BoatHouseId.Trim() + " AND A.Status IN ('B', 'R') AND B.Status IN ('B', 'R', 'P') "
        //                      + " AND A.RowerId > 0 AND A.ActualRowerCharge > 0";

        //            if (RowCharges.BoatTypeId != "0")
        //            {
        //                condition += " AND A.BoatTypeId = '" + RowCharges.BoatTypeId.Trim() + "'";
        //            }

        //            if (RowCharges.RowerId != "0")
        //            {
        //                condition += " AND A.RowerId='" + RowCharges.RowerId.Trim() + "'";
        //            }

        //            //condition += " AND CAST(B.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(RowCharges.FromDate.Trim()) + "') AND ('" + DateTime.Parse(RowCharges.ToDate.Trim()) + "')";
        //            condition += " AND CAST(B.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate) + "') ";
        //            condition += " ) AS A) AS B WHERE(CAST(B.BookingPin AS nvarchar) = '" + RowCharges.SearchBy.Trim() + "' OR B.BookingId = '" + RowCharges.SearchBy.Trim() + "')  "
        //                       + " ORDER BY B.RowNumber ASC ";

        //            squery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.OrderDate ASC) 'RowNumber', * FROM ( "
        //                + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
        //                + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtl AS A"
        //                + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
        //                + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
        //                + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
        //                + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + "";

        //            SqlCommand cmd = new SqlCommand(squery, con);
        //            List<RowerChargersV2> li = new List<RowerChargersV2>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    RowerChargersV2 lstRowCharges = new RowerChargersV2();

        //                    lstRowCharges.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    lstRowCharges.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
        //                    lstRowCharges.BookingPin = dt.Rows[i]["BookingPin"].ToString();
        //                    lstRowCharges.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    lstRowCharges.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                    lstRowCharges.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                    lstRowCharges.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                    lstRowCharges.SeaterType = dt.Rows[i]["SeaterType"].ToString();
        //                    lstRowCharges.RowerId = dt.Rows[i]["RowerId"].ToString();
        //                    lstRowCharges.RowerName = dt.Rows[i]["RowerName"].ToString();
        //                    lstRowCharges.ActualRowerCharge = dt.Rows[i]["ActualRowerCharge"].ToString();
        //                    lstRowCharges.RowNumber = dt.Rows[i]["RowNumber"].ToString();
        //                    li.Add(lstRowCharges);
        //                }

        //                RowerChargersV2List BoatRate = new RowerChargersV2List
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }
        //            else
        //            {
        //                RowerChargersV2String BoatRate = new RowerChargersV2String
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            RowerChargersV2String Vehicle = new RowerChargersV2String
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


        // ****** Report - Booking Other Services ****** //


        /// <summary>
        /// NEWLY ADDED BY ABHINAYA K
        /// ADDED DATE 20APR2022
        /// Modified by Brijin On 2022-05-24
        /// </summary>
        /// <param name="RowCharges"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RowerCharger/ListAllPinV2")]
        public IHttpActionResult RowerChargesPinV2([FromBody] RowerChargersV2 RowCharges)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(RowCharges.BoatHouseId) != "" && RowCharges.BoatTypeId != "" && RowCharges.RowerId != ""
                    && RowCharges.FromDate != "" && RowCharges.ToDate != "")
                {

                    string condition = string.Empty;
                    string condition1 = string.Empty;

                    condition = " WHERE A.BoatHouseId =@BoatHouseId AND A.Status IN ('B', 'R') AND B.Status IN ('B', 'R', 'P') "
                              + " AND A.RowerId > 0 AND A.ActualRowerCharge > 0";

                    if (RowCharges.BoatTypeId != "0")
                    {
                        condition += " AND A.BoatTypeId = @BoatTypeId";
                    }

                    if (RowCharges.RowerId != "0")
                    {
                        condition += " AND A.RowerId= @RowerId";
                    }

                    //condition += " AND CAST(B.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(RowCharges.FromDate.Trim()) + "') AND ('" + DateTime.Parse(RowCharges.ToDate.Trim()) + "')";
                    condition += " AND CAST(B.BookingDate AS DATE) BETWEEN @FromDate AND @ToDate ";
                    condition += " ) AS A) AS B WHERE(CAST(B.BookingPin AS nvarchar) = @SearchBy OR B.BookingId = @SearchBy)  "
                               + " ORDER BY B.RowNumber ASC ";
                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
                    if (DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.OrderDate ASC) 'RowNumber', * FROM ( "
                        + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtl AS A"
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + "";
                    }
                    else if (DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                     && DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        squery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.OrderDate ASC) 'RowNumber', * FROM ( "
                       + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                       + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtlHistory AS A"
                       + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                       + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                       + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                       + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + "";
                    }
                    else
                    {
                        squery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.OrderDate ASC) 'RowNumber', * FROM ( "
                        + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtl AS A"
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId "
                        + " UNION ALL"
                        + " SELECT B.BookingId, A.BookingPin, A.BoatReferenceNo, CONVERT(varchar,B.BookingDate,105)+'  '+CONVERT(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, A.BoatTypeId, C.BoatType, A.BoatSeaterId, D.SeaterType,"
                        + " A.RowerId,E.RowerName, A.ActualRowerCharge,B.BookingDate AS 'OrderDate' FROM BookingDtlHistory AS A"
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId"
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId"
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId"
                        + " INNER JOIN RowerMaster AS E ON A.RowerId = E.RowerId AND A.BoatHouseId = E.BoatHouseId " + condition + "";
                    }


                    SqlCommand cmd = new SqlCommand(squery, con);
                    List<RowerChargersV2> li = new List<RowerChargersV2>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@SearchBy", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(RowCharges.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(RowCharges.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = RowCharges.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = RowCharges.BoatTypeId.Trim();
                    cmd.Parameters["@RowerId"].Value = RowCharges.RowerId.Trim();
                    cmd.Parameters["@SearchBy"].Value = RowCharges.SearchBy.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RowerChargersV2 lstRowCharges = new RowerChargersV2();

                            lstRowCharges.BookingId = dt.Rows[i]["BookingId"].ToString();
                            lstRowCharges.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            lstRowCharges.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            lstRowCharges.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            lstRowCharges.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            lstRowCharges.BoatType = dt.Rows[i]["BoatType"].ToString();
                            lstRowCharges.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            lstRowCharges.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            lstRowCharges.RowerId = dt.Rows[i]["RowerId"].ToString();
                            lstRowCharges.RowerName = dt.Rows[i]["RowerName"].ToString();
                            lstRowCharges.ActualRowerCharge = dt.Rows[i]["ActualRowerCharge"].ToString();
                            lstRowCharges.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                            li.Add(lstRowCharges);
                        }

                        RowerChargersV2List BoatRate = new RowerChargersV2List
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        RowerChargersV2String BoatRate = new RowerChargersV2String
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RowerChargersV2String Vehicle = new RowerChargersV2String
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


        [HttpPost]
        [AllowAnonymous]
        [Route("getRptBookingOthers/ListAll")]
        public IHttpActionResult getRptBookingOthers([FromBody] RptBookingOthers others)
        {
            try
            {
                string squery = string.Empty;
                string conditions = string.Empty;
                if (Convert.ToString(others.BoatHouseId) != null)
                {
                    //string conditions = " WHERE A.BoathouseId='" + others.BoatHouseId + "' AND CAST( A.BookingDate AS DATE) BETWEEN CONVERT(date, '" + others.FromDate + "', 103) AND CONVERT(date, '" + others.ToDate + "', 103)";
                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.ServiceId != "0" && others.BookingType != "0")
                    {
                        conditions = " where A.BoatHouseId = @BoatHouseId  and A.BookingType = @BookingType and A.ServiceId = @ServiceId and "
                                  + "CAST(A.BookingDate AS DATE) BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103) ";
                    }

                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.ServiceId != "0" && others.ServiceId != "")
                    {
                        conditions = " where A.BoatHouseId = @BoatHouseId  and A.ServiceId = @ServiceId and  "
                                    + " CAST(A.BookingDate AS DATE) BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date,@ToDate, 103)";
                    }
                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.BookingType != "0")
                    {
                        conditions = "where A.BoatHouseId = @BoatHouseId  and A.BookingType = @BookingType and "
                                   + " CAST(A.BookingDate AS DATE) BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103)";
                    }
                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.ServiceId == "0" && others.BookingType == "0")
                    {
                        conditions = "where A.BoatHouseId = @BoatHouseId and "
                                   + " CAST(A.BookingDate AS DATE) BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103)";
                    }
                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null)
                    {
                        conditions = "where A.BoatHouseId = @BoatHouseId and "
                                    + " CAST(A.BookingDate AS DATE) BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date,@ToDate, 103)";
                    }

                    squery = " select A.BookingId, A.ServiceId , B.ServiceName , case when  A.BookingType ='B' then 'Along With Boating ' else 'Independent ' end as 'BookingType', "
                        + " CONVERT(VARCHAR, A.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, A.BookingDate, 100), 7) AS 'BookingDate' , A.NetAmount , A.BoatHouseId ,  "
                        + "SUM(ISNULL(A.ChargePerItem, 0) * ISNULL(A.NoOfItems, 0)) AS 'ServiceFare' , dbo.GetTaxAmountDetails('OtherBooking', @BoatHouseId, A.BookingId, '') AS 'TaxAmount' "
                        + "from BookingOthers as A inner Join OtherServices as b on A.ServiceId = B.ServiceId " + conditions + " "
                        + "group by  A.BookingId, A.ServiceId , B.ServiceName , A.BookingDate , A.NetAmount , A.BoatHouseId ,  A.BookingType";





                    SqlCommand cmd = new SqlCommand(squery, con);

                    List<RptBookingOthers> li = new List<RptBookingOthers>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.NVarChar, 1));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@FromDate"].Value = others.FromDate.Trim();
                    cmd.Parameters["@ToDate"].Value = others.ToDate.Trim();
                    cmd.Parameters["@BoatHouseId"].Value = others.BoatHouseId.Trim();
                    cmd.Parameters["@BookingType"].Value = others.BookingType.Trim();
                    cmd.Parameters["@ServiceId"].Value = others.ServiceId.Trim();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptBookingOthers lstOthers = new RptBookingOthers();

                            lstOthers.BookingId = dt.Rows[i]["BookingId"].ToString();
                            lstOthers.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            lstOthers.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            lstOthers.BookingType = dt.Rows[i]["BookingType"].ToString();
                            lstOthers.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            lstOthers.NetAmount = dt.Rows[i]["NetAmount"].ToString();
                            lstOthers.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            lstOthers.ServiceFare = dt.Rows[i]["ServiceFare"].ToString();
                            lstOthers.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();


                            li.Add(lstOthers);
                        }

                        RptBookingOthersList BoatRate = new RptBookingOthersList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        RowerChargersString BoatRate = new RowerChargersString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RowerChargersString Vehicle = new RowerChargersString
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

        [HttpPost]
        [AllowAnonymous]
        [Route("ddlServiceName")]
        public IHttpActionResult getServiceName([FromBody] RptBookingOthers Service)
        {
            try
            {
                if (Service.BoatHouseId != null)
                {
                    List<RptBookingOthers> li = new List<RptBookingOthers>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("	 select ServiceId , ServiceName from OtherServices where  ActiveStatus='A' and  BoatHouseId=@BoatHouseId", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = Service.BoatHouseId.Trim();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptBookingOthers Rower = new RptBookingOthers();
                            Rower.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            Rower.ServiceName = dt.Rows[i]["ServiceName"].ToString();

                            li.Add(Rower);
                        }

                        RptBookingOthersList ConfList = new RptBookingOthersList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        RowerChargersString ConfRes = new RowerChargersString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    RowerChargersString Vehicle = new RowerChargersString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BoatTypeMasterString Vehicle = new BoatTypeMasterString
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }

        // ****** Report - PaymentType ****** //

        [HttpPost]
        [AllowAnonymous]
        [Route("PaymentType/ListAll")]
        public IHttpActionResult RowerCharges([FromBody] PaymentType RowCharges)
        {
            try
            {
                string squery = string.Empty;

                string conditions = " WHERE A.BoathouseId=@BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date,@ToDate, 103)";

                if (RowCharges.BoatType != "0")
                {
                    conditions += " AND A.BoatTypeId= @BoatType";
                }

                if (RowCharges.PaymentTypeId != "0")
                {
                    conditions += " AND F.ConfigID= @PaymentTypeId";
                }

                if (RowCharges.BoatSeater != "0")
                {
                    conditions += " AND A.BoatSeaterId= @BoatSeater";
                }

                squery = "SELECT A.BookingId,A.BoatReferenceNo, convert(varchar,B.BookingDate,103)+'  '+convert(varchar(20),convert(time,B.BookingDate),100)  AS BookingDate, "
                         + " C.BoatType,D.SeaterType,A.InitBoatCharge,A.InitRowerCharge,A.BoatDeposit,SUM(E.TaxAmount) AS Tax FROM BookingDtl AS A "
                         + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId  and A.BoatHouseId = B.BoatHouseId "
                         + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId  and A.BoatHouseId = C.BoatHouseId "
                         + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId  and A.BoatHouseId = D.BoatHouseId "
                         + " INNER JOIN BookingTaxDtl AS E ON A.BoatReferenceNo = E.ServiceId  and A.BoatHouseId = E.BoatHouseId "
                         + " INNER JOIN ConfigurationMaster AS F ON F.TypeID = '20' AND B.PaymentType = F.ConfigID "
                         + " " + conditions + " GROUP BY A.BookingId,A.BoatReferenceNo,  B.BookingDate,C.BoatType,D.SeaterType, "
                         + " A.InitBoatCharge,A.InitRowerCharge,A.BoatDeposit  ";


                SqlCommand cmd = new SqlCommand(squery, con);
                List<PaymentType> li = new List<PaymentType>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSeater", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PaymentTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters["@FromDate"].Value = RowCharges.FromDate.Trim();
                cmd.Parameters["@ToDate"].Value = RowCharges.ToDate.Trim();
                cmd.Parameters["@BoatHouseId"].Value = RowCharges.BoatHouseId.Trim();
                cmd.Parameters["@BoatType"].Value = RowCharges.BoatType.Trim();
                cmd.Parameters["@BoatSeater"].Value = RowCharges.BoatSeater.Trim();
                cmd.Parameters["@PaymentTypeId"].Value = RowCharges.PaymentTypeId.Trim();

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PaymentType lstRowCharges = new PaymentType();

                        lstRowCharges.BookingId = dt.Rows[i]["BookingId"].ToString();
                        lstRowCharges.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                        lstRowCharges.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        lstRowCharges.BoatType = dt.Rows[i]["BoatType"].ToString();
                        lstRowCharges.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
                        lstRowCharges.BoatCharge = dt.Rows[i]["InitBoatCharge"].ToString();
                        lstRowCharges.RowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
                        lstRowCharges.Deposit = dt.Rows[i]["BoatDeposit"].ToString();
                        lstRowCharges.Tax = dt.Rows[i]["Tax"].ToString();
                        li.Add(lstRowCharges);
                    }

                    PaymentTypeList BoatRate = new PaymentTypeList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }

                else
                {
                    PaymentTyperes BoatRate = new PaymentTyperes
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

        // ****** Report - Boat Wise Trip ****** //


        /// <summary>
        /// Modified by -silu
        /// Modified date-2022-05-10
        /// </summary>
        /// <param name="BoatTrip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatwiseTrip/ListAll")]
        public IHttpActionResult BoatwiseTrip([FromBody] BoatwiseTrip BoatTrip)
        {
            try
            {
                string squery = string.Empty;


                string conditions = " WHERE B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId=@BoatHouseId AND CAST(B.BookingDate AS DATE) "
                    + " BETWEEN @FromDate AND @ToDate";

                if (BoatTrip.BoatType != "0")
                {
                    conditions += " AND A.BoatTypeId= @BoatType";
                }

                if (BoatTrip.BoatSelection != "0")
                {
                    conditions += " AND E.BoatNum= @BoatSelection";
                }

                if (BoatTrip.BoatSeater != "0")
                {
                    conditions += " AND A.BoatSeaterId= @BoatSeater";
                }

                if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
                {
                    conditions += "";
                }

                else
                {
                    conditions += " AND B.Createdby= @UserId";
                }

                string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = " SELECT C.BoatTypeId, C.BoatType, COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        //+ " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId";
                }

                else if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                 && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = " SELECT C.BoatTypeId, C.BoatType, COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtlHistory AS A "
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        //+ " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId";
                }
                else
                {
                    squery = " SELECT A.BoatTypeId,A.BoatType,SUM(A.Trips) AS 'Trips',SUM(A.Total) AS 'Total' FROM "
                        + " ( SELECT C.BoatTypeId, C.BoatType, COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId"
                        + " Union ALL"
                        + " SELECT C.BoatTypeId, C.BoatType, COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtlHistory AS A "
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId) AS A "
                        + " GROUP BY A.BoatType, A.BoatTypeId";
                }


                SqlCommand cmd = new SqlCommand(squery, con);
                List<BoatwiseTrip> li = new List<BoatwiseTrip>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSelection", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSeater", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.VarChar,10));
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = BoatTrip.BoatHouseId.Trim();
                cmd.Parameters["@BoatType"].Value = BoatTrip.BoatType.Trim();
                cmd.Parameters["@BoatSelection"].Value = BoatTrip.BoatSelection.Trim();
                cmd.Parameters["@BoatSeater"].Value = BoatTrip.BoatSeater.Trim();
                cmd.Parameters["@UserId"].Value = BoatTrip.UserId.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

                        lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                        lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
                        if (lstBoatTrip.Trips == "")
                        {
                            lstBoatTrip.Trips = "0";
                        }
                        lstBoatTrip.Amount = dt.Rows[i]["Total"].ToString();
                        if (lstBoatTrip.Amount == "")
                        {
                            lstBoatTrip.Amount = "0.00";
                        }

                        li.Add(lstBoatTrip);
                    }

                    BoatwiseTripList BoatRate = new BoatwiseTripList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }

                else
                {
                    BoatwiseTripres BoatRate = new BoatwiseTripres
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

        /// <summary>
        /// Modified by -silu
        /// Modified date-2022-05-10
        /// </summary>
        /// <param name="BoatTrip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatwiseTrip/Summary")]
        public IHttpActionResult BoatwiseSummary([FromBody] BoatwiseTrip BoatTrip)
        {
            try
            {
                string squery = string.Empty;

                string conditions = " WHERE B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId=@BoatHouseId AND CAST(B.BookingDate AS DATE) "
                     + " BETWEEN @FromDate AND @ToDate";

                if (BoatTrip.BoatType != "0")
                {
                    conditions += " AND A.BoatTypeId= @BoatType";
                }

                if (BoatTrip.BoatSelection != "0")
                {
                    conditions += " AND E.BoatNum= @BoatSelection";
                }

                if (BoatTrip.BoatSeater != "0")
                {
                    conditions += " AND A.BoatSeaterId= @BoatSeater";
                }

                if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
                {
                    conditions += " ";
                }
                else
                {
                    conditions += " AND B.Createdby= @UserId";
                }


                string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = "SELECT C.BoatTypeId, C.BoatType,D.BoatSeaterId,D.SeaterType,COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId, D.BoatSeaterId, D.SeaterType ";
                }
                else if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = "SELECT C.BoatTypeId, C.BoatType,D.BoatSeaterId,D.SeaterType,COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtlHistory AS A "
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId, D.BoatSeaterId, D.SeaterType ";
                }
                else
                {

                    squery = "SELECT A.BoatTypeId, A.BoatType,A.BoatSeaterId,A.SeaterType,SUM(A.Trips) AS Trips,  SUM(A.Total) AS Total FROM "
                        + " ( SELECT C.BoatTypeId, C.BoatType,D.BoatSeaterId,D.SeaterType,COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId, D.BoatSeaterId, D.SeaterType "
                        + " Union ALL"
                        + " SELECT C.BoatTypeId, C.BoatType,D.BoatSeaterId,D.SeaterType,COUNT(A.BookingId) AS Trips, "
                        + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtlHistory AS A "
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
                        + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId, D.BoatSeaterId, D.SeaterType) AS A "
                        + " GROUP BY A.BoatType, A.BoatTypeId, A.BoatSeaterId, A.SeaterType";
                }


                SqlCommand cmd = new SqlCommand(squery, con);
                List<BoatwiseTrip> li = new List<BoatwiseTrip>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSelection", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSeater", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.VarChar,10));
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = BoatTrip.BoatHouseId.Trim();
                cmd.Parameters["@BoatType"].Value = BoatTrip.BoatType.Trim();
                cmd.Parameters["@BoatSelection"].Value = BoatTrip.BoatSelection.Trim();
                cmd.Parameters["@BoatSeater"].Value = BoatTrip.BoatSeater.Trim();
                cmd.Parameters["@UserId"].Value = BoatTrip.UserId.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

                        lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                        lstBoatTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        lstBoatTrip.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
                        lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
                        if (lstBoatTrip.Trips == "")
                        {
                            lstBoatTrip.Trips = "0";
                        }
                        lstBoatTrip.Amount = dt.Rows[i]["Total"].ToString();
                        if (lstBoatTrip.Amount == "")
                        {
                            lstBoatTrip.Amount = "0.00";
                        }
                        li.Add(lstBoatTrip);
                    }

                    BoatwiseTripList BoatRate = new BoatwiseTripList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }

                else
                {
                    BoatwiseTripres BoatRate = new BoatwiseTripres
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

        /// <summary>
        /// Modified by -silu
        /// Modified date-2022-05-10
        /// </summary>
        /// <param name="BoatTrip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatwiseTripType/Detail")]
        public IHttpActionResult BoatwiseAllTripSummary([FromBody] BoatwiseTrip BoatTrip)
        {
            try
            {
                string squery = string.Empty;


                string conditions = " WHERE D.Status IN ('B','R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId=@BoatHouseId AND CAST(D.BookingDate AS DATE) BETWEEN "
                    + " @FromDate AND @ToDate  AND "
                    + " A.BoatTypeId= @BoatTypeId AND A.BoatSeaterId = @BoatSeaterId ";

                if (BoatTrip.BoatType != "0")
                {
                    conditions += " AND A.BoatTypeId= @BoatType";
                }

                if (BoatTrip.BoatSelection != "0")
                {
                    conditions += " AND E.BoatNum= @BoatSelection";
                }

                if (BoatTrip.BoatSeater != "0")
                {
                    conditions += " AND A.BoatSeaterId= @BoatSeater";
                }

                if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
                {
                    conditions += " ";
                }
                else
                {
                    conditions += " AND D.Createdby= @UserId";
                }

                string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                    && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
                        + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                        + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0)) - SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                        + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " "
                        + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                        + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";
                }

                else if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                     && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
                        + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                        + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0)) - SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                        + " FROM BookingDtlHistory AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BookingHdrHistory AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                        + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " "
                        + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                        + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";
                }
                else
                {
                    squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
                       + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                       + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0)) - SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                        + " FROM BookingDtl AS A "
                       + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                       + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                       + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                       + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                       + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " "
                       + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                       + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin "
                       + " Union All "
                       + " SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar, D.BookingDate, 103) + '  ' + convert(varchar(20), "
                       + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                       + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0)) - SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                       + " FROM BookingDtlHistory AS A "
                       + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                       + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                       + " INNER JOIN BookingHdrHistory AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                       + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                       + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " "
                       + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                       + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";
                }


                SqlCommand cmd = new SqlCommand(squery, con);
                List<BoatwiseTrip> li = new List<BoatwiseTrip>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSelection", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSeater", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.VarChar,10));
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate);
                cmd.Parameters["@BoatHouseId"].Value = BoatTrip.BoatHouseId.Trim();
                cmd.Parameters["@BoatType"].Value = BoatTrip.BoatType.Trim();
                cmd.Parameters["@BoatTypeId"].Value = BoatTrip.BoatTypeId.Trim();
                cmd.Parameters["@BoatSelection"].Value = BoatTrip.BoatSelection.Trim();
                cmd.Parameters["@BoatSeater"].Value = BoatTrip.BoatSeater.Trim();
                cmd.Parameters["@BoatSeaterId"].Value = BoatTrip.BoatSeaterId.Trim();
                cmd.Parameters["@UserId"].Value = BoatTrip.UserId.Trim();
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

                        lstBoatTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                        lstBoatTrip.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                        lstBoatTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                        lstBoatTrip.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                        lstBoatTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        lstBoatTrip.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
                        lstBoatTrip.BoatName = dt.Rows[i]["BoatName"].ToString();
                        lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
                        if (lstBoatTrip.Trips == "")
                        {
                            lstBoatTrip.Trips = "0";
                        }
                        lstBoatTrip.Amount = dt.Rows[i]["Amount"].ToString();
                        if (lstBoatTrip.Amount == "")
                        {
                            lstBoatTrip.Amount = "0.00";
                        }
                        li.Add(lstBoatTrip);
                    }

                    BoatwiseTripList BoatRate = new BoatwiseTripList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }

                else
                {
                    BoatwiseTripres BoatRate = new BoatwiseTripres
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

        /// <summary>
        /// Modified by -silu
        /// Modified date-2022-05-10
        /// </summary>
        /// <param name="BoatTrip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatwiseAllType/Detail")]
        public IHttpActionResult BoatwiseTripSummary([FromBody] BoatwiseTrip BoatTrip)
        {
            try
            {
                string squery = string.Empty;

                string conditions = " WHERE D.Status IN ('B','R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId=@BoatHouseId AND CAST(D.BookingDate AS DATE) BETWEEN "
                    + " CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103) AND "
                    + " A.BoatTypeId= @BoatTypeId  ";

                if (BoatTrip.BoatType != "0")
                {
                    conditions += " AND A.BoatTypeId= @BoatType";
                }

                if (BoatTrip.BoatSelection != "0")
                {
                    conditions += " AND E.BoatNum= @BoatSelection";
                }

                if (BoatTrip.BoatSeater != "0")
                {
                    conditions += " AND A.BoatSeaterId= @BoatSeater";
                }

                if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
                {
                    conditions += " ";
                }
                else
                {
                    conditions += " AND D.Createdby= @UserId";
                }

                string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                    && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
                        + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                        + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                        + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                        + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";
                }
                else if (DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                    && DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                {
                    squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
                        + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                        + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                        + " FROM BookingDtlHistory AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BookingHdrHistory AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                        + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                        + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";
                }
                else
                {
                    squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
                        + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                        + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                        + " FROM BookingDtl AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                        + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                        + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin "
                        + " Union All "
                        + " SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
                        + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
                        + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Amount "
                        + " FROM BookingDtlHistory AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
                        + " INNER JOIN BookingHdrHistory AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
                        + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
                        + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
                        + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";
                }

                SqlCommand cmd = new SqlCommand(squery, con);
                List<BoatwiseTrip> li = new List<BoatwiseTrip>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);


                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatType", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSelection", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatSeater", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters["@FromDate"].Value = BoatTrip.FromDate.Trim();
                cmd.Parameters["@ToDate"].Value = BoatTrip.ToDate.Trim();
                cmd.Parameters["@BoatHouseId"].Value = BoatTrip.BoatHouseId.Trim();
                cmd.Parameters["@BoatType"].Value = BoatTrip.BoatType.Trim();
                cmd.Parameters["@BoatTypeId"].Value = BoatTrip.BoatTypeId.Trim();
                cmd.Parameters["@BoatSelection"].Value = BoatTrip.BoatSelection.Trim();
                cmd.Parameters["@BoatSeater"].Value = BoatTrip.BoatSeater.Trim();
                cmd.Parameters["@UserId"].Value = BoatTrip.UserId.Trim();

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

                        lstBoatTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
                        lstBoatTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                        lstBoatTrip.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                        lstBoatTrip.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
                        lstBoatTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        lstBoatTrip.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
                        lstBoatTrip.BoatName = dt.Rows[i]["BoatName"].ToString();
                        lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
                        lstBoatTrip.Amount = dt.Rows[i]["Amount"].ToString();
                        if (lstBoatTrip.Amount == "")
                        {
                            lstBoatTrip.Amount = "0.00";
                        }
                        li.Add(lstBoatTrip);
                    }

                    BoatwiseTripList BoatRate = new BoatwiseTripList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }

                else
                {
                    BoatwiseTripres BoatRate = new BoatwiseTripres
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


        /// <summary>
        /// Modified By Suba On 19-05-2022
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <param name="others"></param>
        /// <returns></returns>        
        [HttpPost]
        [AllowAnonymous]
        [Route("RestaurantAbstract")]
        public IHttpActionResult Restaurant([FromBody] RptRestaurant others)
        {
            try
            {
                string sQuery = string.Empty;
                string conditions = string.Empty;

                if (Convert.ToString(others.BoatHouseId) != null)
                {
                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId == "0")
                    {
                        conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId AND A.BoatHouseId = @BoatHouseId and CAST(A.BookingDate AS DATE) "
                                    + "  BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103) and C.CategoryId = @CategoryId";
                    }
                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId == "0")
                    {
                        conditions = " WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId AND A.BoatHouseId = @BoatHouseId and CAST(A.BookingDate AS DATE) "
                                     + "BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103) ";

                    }
                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId != "0")
                    {
                        conditions = " WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId AND A.BoatHouseId = @BoatHouseId"
                                     + " and CAST(A.BookingDate AS DATE) BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103)  "
                                     + " and B.ServiceId = ServiceId and B.CategoryId = @CategoryId";

                    }

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");


                    if (DateTime.Parse(others.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(others.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT C.CategoryId, C.CategoryName, SUM(A.NoOfItems) AS 'Quantity', SUM(A.NetAmount) AS 'Total' "
                            + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                            + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId " + conditions + "  GROUP BY C.CategoryId, C.CategoryName";
                    }

                    else if (DateTime.Parse(others.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                      && DateTime.Parse(others.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT C.CategoryId, C.CategoryName, SUM(A.NoOfItems) AS 'Quantity', SUM(A.NetAmount) AS 'Total' "
                            + " FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                            + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId " + conditions + "  GROUP BY C.CategoryId, C.CategoryName";
                    }
                    else
                    {
                        sQuery = "SELECT A.CategoryId, A.CategoryName, SUM(A.Quantity) AS 'Quantity', SUM(A.Total) AS 'Total'  FROM "
                          + "( SELECT C.CategoryId, C.CategoryName, SUM(A.NoOfItems) AS 'Quantity', SUM(A.NetAmount) AS 'Total' "
                          + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                          + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId " + conditions + "  GROUP BY C.CategoryId, C.CategoryName"
                          + " UNION ALL "
                          + " SELECT C.CategoryId, C.CategoryName, SUM(A.NoOfItems) AS 'Quantity', SUM(A.NetAmount) AS 'Total' "
                          + " FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                          + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId " + conditions + "  GROUP BY C.CategoryId, C.CategoryName ) AS A GROUP BY A.CategoryId, A.CategoryName";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = others.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = others.FromDate.Trim();
                    cmd.Parameters["@ToDate"].Value = others.ToDate.Trim();
                    cmd.Parameters["@ServiceId"].Value = others.ServiceId.Trim();
                    cmd.Parameters["@CategoryId"].Value = others.CategoryId.Trim();

                    List<RptRestaurant> li = new List<RptRestaurant>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptRestaurant lstOthers = new RptRestaurant();
                            lstOthers.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            lstOthers.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            lstOthers.Quantity = dt.Rows[i]["Quantity"].ToString();
                            lstOthers.Total = dt.Rows[i]["Total"].ToString();


                            li.Add(lstOthers);
                        }

                        RptRestaurantList BoatRate = new RptRestaurantList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        RptRestaurantString BoatRate = new RptRestaurantString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RptRestaurantString Vehicle = new RptRestaurantString
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
        /// Modified By Suba On 19-05-2022
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <param name="others"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Restaurant/ListAll")]
        public IHttpActionResult Restaurantlistall([FromBody] RptRestaurant others)
        {
            try
            {
                string sQuery = string.Empty;
                string conditions = string.Empty;

                if (Convert.ToString(others.BoatHouseId) != null)
                {
                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId != "0")
                    {
                        conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
                                   + " AND A.BoatHouseId = @BoatHouseId AND C.CategoryId = @CategoryId  and B.ServiceId=@ServiceId and CAST(A.BookingDate AS DATE) "
                                   + " BETWEEN (@FromDate) AND (@ToDate) ";
                    }

                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId == "0")
                    {
                        conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId  AND A.BoatHouseId = @BoatHouseId AND C.CategoryId = @CategoryId  and CAST(A.BookingDate AS DATE) "
                                    + " BETWEEN (@FromDate) AND (@ToDate) ";
                    }
                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId != "0")
                    {
                        conditions = "WHERE A.BStatus='B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId  AND A.BoatHouseId = @BoatHouseId AND B.ServiceId = @ServiceId  and CAST(A.BookingDate AS DATE) "
                                    + " BETWEEN (@FromDate) AND (@ToDate) ";
                    }
                    else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId == "0")
                    {
                        conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId   "
                                    + " AND A.BoatHouseId = @BoatHouseId  and CAST(A.BookingDate AS DATE)  "
                                    + " BETWEEN (@FromDate) AND (@ToDate)  ";

                    }

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(others.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(others.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate', "
                           + " SUM(ISNULL(A.NoOfItems,0)) AS 'Quantity', (ISNULL(A.ChargePerItem,0) * SUM(ISNULL(A.NoOfItems,0))) AS 'Charge', "
                           + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'TaxAmount',   "
                           + " SUM(ISNULL(A.NoOfItems,0) * ISNULL(A.ChargePerItem,0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'Total' "
                           + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                           + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId  " + conditions + " "
                           + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem ";
                    }

                    else if (DateTime.Parse(others.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                       && DateTime.Parse(others.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate', "
                           + " SUM(ISNULL(A.NoOfItems,0)) AS 'Quantity', (ISNULL(A.ChargePerItem,0) * SUM(ISNULL(A.NoOfItems,0))) AS 'Charge', "
                           + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'TaxAmount',   "
                           + " SUM(ISNULL(A.NoOfItems,0) * ISNULL(A.ChargePerItem,0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'Total' "
                           + " FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                           + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId  " + conditions + " "
                           + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem ";
                    }

                    else
                    {
                        sQuery = "SELECT A.CategoryId, A.CategoryName, A.ServiceId, A.ServiceName, SUM(A.ItemRate) AS 'ItemRate',  SUM(A.Quantity) AS 'Quantity', "
                           + "SUM(A.Charge)  AS 'Charge',  SUM(A.TaxAmount) AS 'TaxAmount',SUM(A.Total) AS 'Total'   FROM "
                           + " ( SELECT C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate', "
                           + " SUM(ISNULL(A.NoOfItems,0)) AS 'Quantity', (ISNULL(A.ChargePerItem,0) * SUM(ISNULL(A.NoOfItems,0))) AS 'Charge', "
                           + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'TaxAmount',   "
                           + " SUM(ISNULL(A.NoOfItems,0) * ISNULL(A.ChargePerItem,0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'Total' "
                           + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                           + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId  " + conditions + " "
                           + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem "
                           + " UNION ALL"
                           + " SELECT C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate', "
                           + " SUM(ISNULL(A.NoOfItems,0)) AS 'Quantity', (ISNULL(A.ChargePerItem,0) * SUM(ISNULL(A.NoOfItems,0))) AS 'Charge', "
                           + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'TaxAmount',   "
                           + " SUM(ISNULL(A.NoOfItems,0) * ISNULL(A.ChargePerItem,0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurantHistory', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'Total' "
                           + " FROM BookingRestaurantHistory AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
                           + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId  " + conditions + " "
                           + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem )  AS A "
                           + " GROUP BY A.ServiceId, A.ServiceName, A.CategoryId, A.CategoryName ";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = others.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(others.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(others.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ServiceId"].Value = others.ServiceId.Trim();
                    cmd.Parameters["@CategoryId"].Value = others.CategoryId.Trim();

                    List<RptRestaurant> li = new List<RptRestaurant>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptRestaurant lstOthers = new RptRestaurant();
                            lstOthers.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            lstOthers.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            lstOthers.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            lstOthers.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            lstOthers.ItemRate = dt.Rows[i]["ItemRate"].ToString();
                            lstOthers.Quantity = dt.Rows[i]["Quantity"].ToString();
                            lstOthers.Charge = dt.Rows[i]["Charge"].ToString();
                            lstOthers.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                            if (lstOthers.TaxAmount == "")
                            {
                                lstOthers.TaxAmount = "0.00";
                            }
                            lstOthers.Total = dt.Rows[i]["Total"].ToString();
                            if (lstOthers.Total == "")
                            {
                                lstOthers.Total = "0.00";
                            }


                            li.Add(lstOthers);
                        }

                        RptRestaurantList BoatRate = new RptRestaurantList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        RptRestaurantString BoatRate = new RptRestaurantString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RptRestaurantString Vehicle = new RptRestaurantString
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
        ///  Modified By Suba On 19-05-2022
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.    
        /// </summary>
        /// <param name="others"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("listingRestaurant_Test")]
        public IHttpActionResult listingRestaurant_Test([FromBody] RptRestaurant others)
        {
            try
            {
                string sQuery = string.Empty;
                string conditions = string.Empty;

                if (Convert.ToString(others.BoatHouseId) != null)
                {
                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId != "0")
                    {

                        conditions = " WHERE C.BStatus='B' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId AND D.BoatHouseId = @BoatHouseId AND "
                                    + " C.ServiceId = @ServiceId and D.BookingType = 'R' and A.CategoryId = @CategoryId and CAST(C.BookingDate AS DATE)  "
                                    + " BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103)";
                    }

                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId == "0")
                    {
                        conditions = " WHERE  C.BStatus='B' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId AND D.BoatHouseId = @BoatHouseId AND "
                                     + " D.BookingType = 'R' and CAST(C.BookingDate AS DATE)  BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103) ";
                    }

                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId == "0")
                    {
                        conditions = " WHERE C.BStatus='B' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId AND D.BoatHouseId = @BoatHouseId AND "
                                     + " D.BookingType = 'R' and A.CategoryId = @CategoryId and CAST(C.BookingDate AS DATE)   BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103)";
                    }

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");


                    if (DateTime.Parse(others.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(others.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " select C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName, "
                          + " ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate',  (ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0)) AS 'ChargePerItem', ISNULL(C.NoOfItems,0) AS 'NoOfItems', "
                         + "  SUM(ISNULL(D.TaxAmount, 0)) AS 'TaxAmount', "
                         + " ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0) + SUM(ISNULL(D.TaxAmount, 0))  AS  'Total' ,  "
                         + " CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) AS 'BookingDate' "
                         + " from FoodCategory  as A inner Join FoodItemMaster as B on A.CategoryId = B.CategoryId inner Join BookingRestaurant as c "
                         + " on B.ServiceId = C.ServiceId inner Join BookingTaxDtl as D on C.ServiceId = D.ServiceId and C.BookingId = D.BookingId " + conditions + " "
                         + " GROUP BY C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName , C.ChargePerItem , C.NoOfItems , C.BookingDate, "
                         + " B.BoatHouseId, C.BookingId, B.ServiceTotalAmount ";
                    }

                    else if (DateTime.Parse(others.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                         && DateTime.Parse(others.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " select C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName, "
                          + " ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate',  (ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0)) AS 'ChargePerItem', ISNULL(C.NoOfItems,0) AS 'NoOfItems', "
                         + "  SUM(ISNULL(D.TaxAmount, 0)) AS 'TaxAmount', "
                         + " ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0) + SUM(ISNULL(D.TaxAmount, 0))  AS  'Total' ,  "
                         + " CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) AS 'BookingDate' "
                         + " from FoodCategory  as A inner Join FoodItemMaster as B on A.CategoryId = B.CategoryId inner Join BookingRestaurantHistory as c "
                         + " on B.ServiceId = C.ServiceId inner Join BookingTaxDtlHistory as D on C.ServiceId = D.ServiceId and C.BookingId = D.BookingId " + conditions + " "
                         + " GROUP BY C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName , C.ChargePerItem , C.NoOfItems , C.BookingDate, "
                         + " B.BoatHouseId, C.BookingId, B.ServiceTotalAmount ";
                    }
                    else
                    {
                        sQuery = " select C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName, "
                        + " ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate',  (ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0)) AS 'ChargePerItem', ISNULL(C.NoOfItems,0) AS 'NoOfItems', "
                        + "  SUM(ISNULL(D.TaxAmount, 0)) AS 'TaxAmount', "
                        + " ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0) + SUM(ISNULL(D.TaxAmount, 0))  AS  'Total' ,  "
                        + " CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) AS 'BookingDate' "
                        + " from FoodCategory  as A inner Join FoodItemMaster as B on A.CategoryId = B.CategoryId inner Join BookingRestaurant as c "
                        + " on B.ServiceId = C.ServiceId inner Join BookingTaxDtl as D on C.ServiceId = D.ServiceId and C.BookingId = D.BookingId " + conditions + " "
                        + " GROUP BY C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName , C.ChargePerItem , C.NoOfItems , C.BookingDate, "
                        + " B.BoatHouseId, C.BookingId, B.ServiceTotalAmount "
                        + " UNION ALL"
                        + " select C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName, "
                        + " ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate',  (ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0)) AS 'ChargePerItem', ISNULL(C.NoOfItems,0) AS 'NoOfItems', "
                        + "  SUM(ISNULL(D.TaxAmount, 0)) AS 'TaxAmount', "
                        + " ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0) + SUM(ISNULL(D.TaxAmount, 0))  AS  'Total' ,  "
                        + " CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) AS 'BookingDate' "
                        + " from FoodCategory  as A inner Join FoodItemMaster as B on A.CategoryId = B.CategoryId inner Join BookingRestaurantHistory as c "
                        + " on B.ServiceId = C.ServiceId inner Join BookingTaxDtlHistory as D on C.ServiceId = D.ServiceId and C.BookingId = D.BookingId " + conditions + " "
                        + " GROUP BY C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName , C.ChargePerItem , C.NoOfItems , C.BookingDate, "
                        + " B.BoatHouseId, C.BookingId, B.ServiceTotalAmount";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = others.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = others.FromDate.Trim();
                    cmd.Parameters["@ToDate"].Value = others.ToDate.Trim();
                    cmd.Parameters["@ServiceId"].Value = others.ServiceId.Trim();
                    cmd.Parameters["@CategoryId"].Value = others.CategoryId.Trim();

                    List<RptRestaurant> li = new List<RptRestaurant>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptRestaurant lstOthers = new RptRestaurant();
                            lstOthers.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            lstOthers.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            lstOthers.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            lstOthers.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            lstOthers.BookingId = dt.Rows[i]["BookingId"].ToString();
                            lstOthers.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                            lstOthers.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                            lstOthers.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            lstOthers.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                            lstOthers.Total = dt.Rows[i]["Total"].ToString();
                            lstOthers.ItemRate = dt.Rows[i]["ItemRate"].ToString();


                            li.Add(lstOthers);
                        }

                        RptRestaurantList BoatRate = new RptRestaurantList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        RptRestaurantString BoatRate = new RptRestaurantString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RptRestaurantString Vehicle = new RptRestaurantString
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


        //[HttpPost]
        //[AllowAnonymous]
        //[Route("BoatwiseTrip/ListAll")]
        //public IHttpActionResult BoatwiseTrip([FromBody] BoatwiseTrip BoatTrip)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        string conditions = " WHERE B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId='" + BoatTrip.BoatHouseId + "' AND CAST(B.BookingDate AS DATE) "
        //            + " BETWEEN ('" + DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) + "')";

        //        if (BoatTrip.BoatType != "0")
        //        {
        //            conditions += " AND A.BoatTypeId= '" + BoatTrip.BoatType + "'";
        //        }

        //        if (BoatTrip.BoatSelection != "0")
        //        {
        //            conditions += " AND E.BoatNum= '" + BoatTrip.BoatSelection + "'";
        //        }

        //        if (BoatTrip.BoatSeater != "0")
        //        {
        //            conditions += " AND A.BoatSeaterId= '" + BoatTrip.BoatSeater + "'";
        //        }

        //        if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
        //        {
        //            conditions += "";
        //        }

        //        else
        //        {
        //            conditions += " AND B.Createdby= '" + BoatTrip.UserId + "'";
        //        }


        //        squery = " SELECT C.BoatTypeId, C.BoatType, COUNT(A.BookingId) AS Trips,SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtl AS A "
        //                + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
        //                + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
        //                + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
        //                //+ " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId "
        //                + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId";


        //        SqlCommand cmd = new SqlCommand(squery, con);
        //        List<BoatwiseTrip> li = new List<BoatwiseTrip>();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

        //                lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
        //                if (lstBoatTrip.Trips == "")
        //                {
        //                    lstBoatTrip.Trips = "0";
        //                }
        //                lstBoatTrip.Amount = dt.Rows[i]["Total"].ToString();
        //                if (lstBoatTrip.Amount == "")
        //                {
        //                    lstBoatTrip.Amount = "0.00";
        //                }

        //                li.Add(lstBoatTrip);
        //            }

        //            BoatwiseTripList BoatRate = new BoatwiseTripList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(BoatRate);
        //        }

        //        else
        //        {
        //            BoatwiseTripres BoatRate = new BoatwiseTripres
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(BoatRate);
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
        //[Route("BoatwiseTrip/Summary")]
        //public IHttpActionResult BoatwiseSummary([FromBody] BoatwiseTrip BoatTrip)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        string conditions = " WHERE B.Status IN ('B', 'R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId='" + BoatTrip.BoatHouseId + "' AND CAST(B.BookingDate AS DATE) "
        //             + " BETWEEN ('" + DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) + "')";

        //        if (BoatTrip.BoatType != "0")
        //        {
        //            conditions += " AND A.BoatTypeId= '" + BoatTrip.BoatType + "'";
        //        }

        //        if (BoatTrip.BoatSelection != "0")
        //        {
        //            conditions += " AND E.BoatNum= '" + BoatTrip.BoatSelection + "'";
        //        }

        //        if (BoatTrip.BoatSeater != "0")
        //        {
        //            conditions += " AND A.BoatSeaterId= '" + BoatTrip.BoatSeater + "'";
        //        }

        //        if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
        //        {
        //            conditions += " ";
        //        }
        //        else
        //        {
        //            conditions += " AND B.Createdby= '" + BoatTrip.UserId + "'";
        //        }


        //        squery = "SELECT C.BoatTypeId, C.BoatType,D.BoatSeaterId,D.SeaterType,COUNT(A.BookingId) AS Trips, "
        //                + " SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Total FROM BookingDtl AS A "
        //                + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
        //                + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId "
        //                + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId "
        //                // + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId "
        //                + " " + conditions + " GROUP BY C.BoatType, C.BoatTypeId, D.BoatSeaterId, D.SeaterType ";


        //        SqlCommand cmd = new SqlCommand(squery, con);
        //        List<BoatwiseTrip> li = new List<BoatwiseTrip>();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

        //                lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                lstBoatTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                lstBoatTrip.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
        //                lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
        //                if (lstBoatTrip.Trips == "")
        //                {
        //                    lstBoatTrip.Trips = "0";
        //                }
        //                lstBoatTrip.Amount = dt.Rows[i]["Total"].ToString();
        //                if (lstBoatTrip.Amount == "")
        //                {
        //                    lstBoatTrip.Amount = "0.00";
        //                }
        //                li.Add(lstBoatTrip);
        //            }

        //            BoatwiseTripList BoatRate = new BoatwiseTripList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(BoatRate);
        //        }

        //        else
        //        {
        //            BoatwiseTripres BoatRate = new BoatwiseTripres
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(BoatRate);
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
        //[Route("BoatwiseTripType/Detail")]
        //public IHttpActionResult BoatwiseAllTripSummary([FromBody] BoatwiseTrip BoatTrip)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        string conditions = " WHERE D.Status IN ('B','R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId='" + BoatTrip.BoatHouseId + "' AND CAST(D.BookingDate AS DATE) BETWEEN "
        //            + " ('" + DateTime.Parse(BoatTrip.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(BoatTrip.ToDate.Trim(), objEnglishDate) + "')  AND "
        //            + " A.BoatTypeId= '" + BoatTrip.BoatTypeId + "' AND A.BoatSeaterId = '" + BoatTrip.BoatSeaterId + "' ";

        //        if (BoatTrip.BoatType != "0")
        //        {
        //            conditions += " AND A.BoatTypeId= '" + BoatTrip.BoatType + "'";
        //        }

        //        if (BoatTrip.BoatSelection != "0")
        //        {
        //            conditions += " AND E.BoatNum= '" + BoatTrip.BoatSelection + "'";
        //        }

        //        if (BoatTrip.BoatSeater != "0")
        //        {
        //            conditions += " AND A.BoatSeaterId= '" + BoatTrip.BoatSeater + "'";
        //        }

        //        if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
        //        {
        //            conditions += " ";
        //        }
        //        else
        //        {
        //            conditions += " AND D.Createdby= '" + BoatTrip.UserId + "'";
        //        }

        //        squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
        //                + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
        //                + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0)) - SUM(ISNULL(A.BoatDeposit,0)) AS Amount FROM BookingDtl AS A "
        //                + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
        //                + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
        //                + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
        //                + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
        //                + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
        //                + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";


        //        SqlCommand cmd = new SqlCommand(squery, con);
        //        List<BoatwiseTrip> li = new List<BoatwiseTrip>();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

        //                lstBoatTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                lstBoatTrip.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
        //                lstBoatTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
        //                lstBoatTrip.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                lstBoatTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                lstBoatTrip.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
        //                lstBoatTrip.BoatName = dt.Rows[i]["BoatName"].ToString();
        //                lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
        //                if (lstBoatTrip.Trips == "")
        //                {
        //                    lstBoatTrip.Trips = "0";
        //                }
        //                lstBoatTrip.Amount = dt.Rows[i]["Amount"].ToString();
        //                if (lstBoatTrip.Amount == "")
        //                {
        //                    lstBoatTrip.Amount = "0.00";
        //                }
        //                li.Add(lstBoatTrip);
        //            }

        //            BoatwiseTripList BoatRate = new BoatwiseTripList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(BoatRate);
        //        }

        //        else
        //        {
        //            BoatwiseTripres BoatRate = new BoatwiseTripres
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(BoatRate);
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
        //[Route("BoatwiseAllType/Detail")]
        //public IHttpActionResult BoatwiseTripSummary([FromBody] BoatwiseTrip BoatTrip)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        string conditions = " WHERE D.Status IN ('B','R', 'P') AND A.Status IN ('B', 'R') AND A.BoathouseId='" + BoatTrip.BoatHouseId + "' AND CAST(D.BookingDate AS DATE) BETWEEN "
        //            + " CONVERT(date, '" + BoatTrip.FromDate + "', 103) AND CONVERT(date, '" + BoatTrip.ToDate + "', 103) AND "
        //            + " A.BoatTypeId= '" + BoatTrip.BoatTypeId + "'  ";

        //        if (BoatTrip.BoatType != "0")
        //        {
        //            conditions += " AND A.BoatTypeId= '" + BoatTrip.BoatType + "'";
        //        }

        //        if (BoatTrip.BoatSelection != "0")
        //        {
        //            conditions += " AND E.BoatNum= '" + BoatTrip.BoatSelection + "'";
        //        }

        //        if (BoatTrip.BoatSeater != "0")
        //        {
        //            conditions += " AND A.BoatSeaterId= '" + BoatTrip.BoatSeater + "'";
        //        }

        //        if (BoatTrip.UserId == "Admin" || BoatTrip.UserId == "Sadmin")
        //        {
        //            conditions += " ";
        //        }
        //        else
        //        {
        //            conditions += " AND D.Createdby= '" + BoatTrip.UserId + "'";
        //        }


        //        squery = "SELECT A.BookingId, A.BookingPin,A.BoatReferenceNo,A.BoatTypeId,convert(varchar,D.BookingDate,103)+'  '+convert(varchar(20), "
        //                + " convert(time,D.BookingDate),100)  AS BookingDate,B.BoatType,A.BoatSeaterId,C.SeaterType,E.BoatName, "
        //                + " COUNT(A.BookingId) AS Trips, SUM(ISNULL(A.InitNetAmount,0))-SUM(ISNULL(A.BoatDeposit,0)) AS Amount FROM BookingDtl AS A "
        //                + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
        //                + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = C.BoatHouseId "
        //                + " INNER JOIN BookingHdr AS D ON A.BookingId = D.BookingId AND A.BoatHouseId = D.BoatHouseId "
        //                + " INNER JOIN BoatMaster AS E ON A.BoatTypeId = E.BoatTypeId AND A.BoatSeaterId = E.BoatSeaterId "
        //                + " AND A.BoatHouseId = E.BoatHouseId AND A.ActualBoatId = E.BoatId " + conditions + " GROUP BY A.BookingId,B.BoatType,A.BoatTypeId,A.BoatSeaterId, "
        //                + " C.SeaterType,D.BookingDate,E.BoatName,A.BoatReferenceNo, A.BookingPin ";


        //        SqlCommand cmd = new SqlCommand(squery, con);
        //        List<BoatwiseTrip> li = new List<BoatwiseTrip>();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                BoatwiseTrip lstBoatTrip = new BoatwiseTrip();

        //                lstBoatTrip.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                lstBoatTrip.BookingPin = dt.Rows[i]["BookingPin"].ToString();
        //                lstBoatTrip.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
        //                lstBoatTrip.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                lstBoatTrip.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                lstBoatTrip.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                lstBoatTrip.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                lstBoatTrip.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
        //                lstBoatTrip.BoatName = dt.Rows[i]["BoatName"].ToString();
        //                lstBoatTrip.Trips = dt.Rows[i]["Trips"].ToString();
        //                lstBoatTrip.Amount = dt.Rows[i]["Amount"].ToString();
        //                if (lstBoatTrip.Amount == "")
        //                {
        //                    lstBoatTrip.Amount = "0.00";
        //                }
        //                li.Add(lstBoatTrip);
        //            }

        //            BoatwiseTripList BoatRate = new BoatwiseTripList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(BoatRate);
        //        }

        //        else
        //        {
        //            BoatwiseTripres BoatRate = new BoatwiseTripres
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(BoatRate);
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




        // ****** Report - Restaurant ****** //

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RestaurantAbstract")]
        //public IHttpActionResult Restaurant([FromBody] RptRestaurant others)
        //{
        //    try
        //    {
        //        string squery = string.Empty;
        //        string conditions = string.Empty;
        //        if (Convert.ToString(others.BoatHouseId) != null)
        //        {
        //            if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId == "0")
        //            {
        //                conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId AND A.BoatHouseId = '" + others.BoatHouseId + "' and CAST(A.BookingDate AS DATE) "
        //                            + "  BETWEEN CONVERT(date, '" + others.FromDate + "', 103) AND CONVERT(date, '" + others.ToDate + "', 103) and C.CategoryId = '" + others.CategoryId + "'";
        //            }
        //            else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId == "0")
        //            {
        //                conditions = " WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId AND A.BoatHouseId = '" + others.BoatHouseId + "' and CAST(A.BookingDate AS DATE) "
        //                             + "BETWEEN CONVERT(date, '" + others.FromDate + "', 103) AND CONVERT(date, '" + others.ToDate + "', 103) ";

        //            }
        //            else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId != "0")
        //            {
        //                conditions = " WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId AND A.BoatHouseId = '" + others.BoatHouseId + "'"
        //                             + " and CAST(A.BookingDate AS DATE) BETWEEN CONVERT(date, '" + others.FromDate + "', 103) AND CONVERT(date, '" + others.ToDate + "', 103)  "
        //                             + " and B.ServiceId = '" + others.ServiceId + "' and B.CategoryId ='" + others.CategoryId + "'";

        //            }

        //            squery = "SELECT C.CategoryId, C.CategoryName, SUM(A.NoOfItems) AS 'Quantity', SUM(A.NetAmount) AS 'Total' "
        //                     + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
        //                     + "INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId " + conditions + "  GROUP BY C.CategoryId, C.CategoryName";
        //            SqlCommand cmd = new SqlCommand(squery, con);

        //            List<RptRestaurant> li = new List<RptRestaurant>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    RptRestaurant lstOthers = new RptRestaurant();
        //                    lstOthers.CategoryId = dt.Rows[i]["CategoryId"].ToString();
        //                    lstOthers.CategoryName = dt.Rows[i]["CategoryName"].ToString();
        //                    lstOthers.Quantity = dt.Rows[i]["Quantity"].ToString();
        //                    lstOthers.Total = dt.Rows[i]["Total"].ToString();


        //                    li.Add(lstOthers);
        //                }

        //                RptRestaurantList BoatRate = new RptRestaurantList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }

        //            else
        //            {
        //                RptRestaurantString BoatRate = new RptRestaurantString
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            RptRestaurantString Vehicle = new RptRestaurantString
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
        //[Route("Restaurant/ListAll")]
        //public IHttpActionResult Restaurantlistall([FromBody] RptRestaurant others)
        //{
        //    try
        //    {
        //        string squery = string.Empty;
        //        string conditions = string.Empty;

        //        if (Convert.ToString(others.BoatHouseId) != null)
        //        {
        //            if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId != "0")
        //            {
        //                conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId "
        //                           + " AND A.BoatHouseId = '" + others.BoatHouseId + "' AND C.CategoryId = '" + others.CategoryId + "'  and B.ServiceId='" + others.ServiceId + "' and CAST(A.BookingDate AS DATE) "
        //                           + " BETWEEN ('" + DateTime.Parse(others.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(others.ToDate.Trim(), objEnglishDate) + "') ";
        //            }

        //            else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId == "0")
        //            {
        //                conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId  AND A.BoatHouseId = '" + others.BoatHouseId + "' AND C.CategoryId = '" + others.CategoryId + "'  and CAST(A.BookingDate AS DATE) "
        //                            + " BETWEEN ('" + DateTime.Parse(others.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(others.ToDate.Trim(), objEnglishDate) + "') ";
        //            }
        //            else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId != "0")
        //            {
        //                conditions = "WHERE A.BStatus='B'  AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId  AND A.BoatHouseId = '" + others.BoatHouseId + "' AND B.ServiceId = '" + others.ServiceId + "'  and CAST(A.BookingDate AS DATE) "
        //                            + " BETWEEN ('" + DateTime.Parse(others.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(others.ToDate.Trim(), objEnglishDate) + "') ";
        //            }
        //            else if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId == "0")
        //            {
        //                conditions = "WHERE A.BStatus='B' AND A.BoatHouseId = B.BoatHouseId AND A.BoatHouseId = C.BoatHouseId   "
        //                            + " AND A.BoatHouseId = '" + others.BoatHouseId + "'  and CAST(A.BookingDate AS DATE)  "
        //                            + " BETWEEN ('" + DateTime.Parse(others.FromDate.Trim(), objEnglishDate) + "') AND ('" + DateTime.Parse(others.ToDate.Trim(), objEnglishDate) + "')  ";

        //            }

        //            squery = "SELECT C.CategoryId, C.CategoryName, B.ServiceId, B.ServiceName, ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate', "
        //                    + " SUM(ISNULL(A.NoOfItems,0)) AS 'Quantity', (ISNULL(A.ChargePerItem,0) * SUM(ISNULL(A.NoOfItems,0))) AS 'Charge', "
        //                    + " SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'TaxAmount',   "
        //                    + " SUM(ISNULL(A.NoOfItems,0) * ISNULL(A.ChargePerItem,0)) + SUM(ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, A.BookingId, B.ServiceId),0)) AS 'Total' "
        //                    + " FROM BookingRestaurant AS A INNER JOIN FoodItemMaster AS B ON A.ServiceId = B.ServiceId "
        //                    + " INNER JOIN FoodCategory AS C ON B.CategoryId = C.CategoryId  " + conditions + " "
        //                    + " GROUP BY B.ServiceId, B.ServiceName, C.CategoryId, C.CategoryName, B.ServiceTotalAmount, A.ChargePerItem ";
        //            SqlCommand cmd = new SqlCommand(squery, con);


        //            List<RptRestaurant> li = new List<RptRestaurant>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    RptRestaurant lstOthers = new RptRestaurant();
        //                    lstOthers.CategoryId = dt.Rows[i]["CategoryId"].ToString();
        //                    lstOthers.CategoryName = dt.Rows[i]["CategoryName"].ToString();
        //                    lstOthers.ServiceId = dt.Rows[i]["ServiceId"].ToString();
        //                    lstOthers.ServiceName = dt.Rows[i]["ServiceName"].ToString();
        //                    lstOthers.ItemRate = dt.Rows[i]["ItemRate"].ToString();
        //                    lstOthers.Quantity = dt.Rows[i]["Quantity"].ToString();
        //                    lstOthers.Charge = dt.Rows[i]["Charge"].ToString();
        //                    lstOthers.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
        //                    if (lstOthers.TaxAmount == "")
        //                    {
        //                        lstOthers.TaxAmount = "0.00";
        //                    }
        //                    lstOthers.Total = dt.Rows[i]["Total"].ToString();
        //                    if (lstOthers.Total == "")
        //                    {
        //                        lstOthers.Total = "0.00";
        //                    }


        //                    li.Add(lstOthers);
        //                }

        //                RptRestaurantList BoatRate = new RptRestaurantList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }

        //            else
        //            {
        //                RptRestaurantString BoatRate = new RptRestaurantString
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            RptRestaurantString Vehicle = new RptRestaurantString
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

        ////Change by imran om 2022-04-04
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("listingRestaurant_Test")]
        //public IHttpActionResult listingRestaurant_Test([FromBody] RptRestaurant others)
        //{
        //    try
        //    {
        //        string squery = string.Empty;
        //        string conditions = string.Empty;
        //        if (Convert.ToString(others.BoatHouseId) != null)
        //        {
        //            if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId != "0")
        //            {

        //                conditions = " WHERE C.BStatus='B' AND A.BoatHouseId = '" + others.BoatHouseId + "' AND B.BoatHouseId = '" + others.BoatHouseId + "' AND C.BoatHouseId = '" + others.BoatHouseId + "' AND "
        //                             + " D.BoatHouseId = '" + others.BoatHouseId + "'   AND "
        //                            + " C.ServiceId = '" + others.ServiceId + "' and D.BookingType = 'R' and A.CategoryId = '" + others.CategoryId + "' and CAST(C.BookingDate AS DATE)  "
        //                            + " BETWEEN CONVERT(date, '" + others.FromDate + "', 103) AND CONVERT(date, '" + others.ToDate + "', 103)";
        //            }
        //            if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId == "0")
        //            {
        //                conditions = " WHERE  C.BStatus='B' AND A.BoatHouseId = '" + others.BoatHouseId + "' AND B.BoatHouseId = '" + others.BoatHouseId + "' AND C.BoatHouseId = '" + others.BoatHouseId + "' AND "
        //                             + " D.BoatHouseId = '" + others.BoatHouseId + "'   AND "
        //                             + " D.BookingType = 'R' and CAST(C.BookingDate AS DATE)  BETWEEN CONVERT(date, '" + others.FromDate + "', 103) AND CONVERT(date, '" + others.ToDate + "', 103) ";
        //            }

        //            if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId == "0")
        //            {
        //                conditions = " WHERE C.BStatus='B' AND A.BoatHouseId = '" + others.BoatHouseId + "' AND B.BoatHouseId = '" + others.BoatHouseId + "' AND C.BoatHouseId = '" + others.BoatHouseId + "' AND "
        //                             + " D.BoatHouseId = '" + others.BoatHouseId + "'   AND "
        //                             + " D.BookingType = 'R' and A.CategoryId ='" + others.CategoryId + "' and CAST(C.BookingDate AS DATE)   BETWEEN CONVERT(date, '" + others.FromDate + "', 103) AND CONVERT(date, '" + others.ToDate + "', 103)";
        //            }

        //            squery = " select C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName, "
        //                    + " ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate',  (ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0)) AS 'ChargePerItem', ISNULL(C.NoOfItems,0) AS 'NoOfItems', "
        //                   + "  SUM(ISNULL(D.TaxAmount, 0)) AS 'TaxAmount', "
        //                   + " ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0) + SUM(ISNULL(D.TaxAmount, 0))  AS  'Total' ,  "
        //                   + " CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) AS 'BookingDate' "
        //                   + " from FoodCategory  as A inner Join FoodItemMaster as B on A.CategoryId = B.CategoryId inner Join BookingRestaurant as c "
        //                   + " on B.ServiceId = C.ServiceId inner Join BookingTaxDtl as D on C.ServiceId = D.ServiceId and C.BookingId = D.BookingId " + conditions + " "
        //                   + " GROUP BY C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName , C.ChargePerItem , C.NoOfItems , C.BookingDate, "
        //                   + " B.BoatHouseId, C.BookingId, B.ServiceTotalAmount ";
        //            SqlCommand cmd = new SqlCommand(squery, con);


        //            List<RptRestaurant> li = new List<RptRestaurant>();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    RptRestaurant lstOthers = new RptRestaurant();
        //                    lstOthers.CategoryId = dt.Rows[i]["CategoryId"].ToString();
        //                    lstOthers.CategoryName = dt.Rows[i]["CategoryName"].ToString();
        //                    lstOthers.ServiceId = dt.Rows[i]["ServiceId"].ToString();
        //                    lstOthers.ServiceName = dt.Rows[i]["ServiceName"].ToString();
        //                    lstOthers.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    lstOthers.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
        //                    lstOthers.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
        //                    lstOthers.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    lstOthers.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
        //                    lstOthers.Total = dt.Rows[i]["Total"].ToString();
        //                    lstOthers.ItemRate = dt.Rows[i]["ItemRate"].ToString();


        //                    li.Add(lstOthers);
        //                }

        //                RptRestaurantList BoatRate = new RptRestaurantList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatRate);
        //            }

        //            else
        //            {
        //                RptRestaurantString BoatRate = new RptRestaurantString
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BoatRate);
        //            }
        //        }
        //        else
        //        {
        //            RptRestaurantString Vehicle = new RptRestaurantString
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


        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="others"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("listingRestaurant")]
        public IHttpActionResult listingRestaurant([FromBody] RptRestaurant others)
        {
            try
            {
                string sQuery = string.Empty;
                string conditions = string.Empty;

                if (Convert.ToString(others.BoatHouseId) != null)
                {
                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId != "0")
                    {

                        conditions = " WHERE C.BStatus='B' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId AND D.BoatHouseId = @BoatHouseId AND "
                                    + " C.ServiceId = @ServiceId and D.BookingType = 'R' and A.CategoryId = @CategoryId and CAST(C.BookingDate AS DATE)  "
                                    + " BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103)";
                    }
                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId == "0" && others.ServiceId == "0")
                    {
                        conditions = " WHERE  C.BStatus='B' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId AND D.BoatHouseId = @BoatHouseId AND "
                                     + " D.BookingType = 'R' and CAST(C.BookingDate AS DATE)  BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103) ";
                    }

                    if (Convert.ToString(others.BoatHouseId) != null && others.FromDate != null && others.ToDate != null && others.CategoryId != "0" && others.ServiceId == "0")
                    {
                        conditions = " WHERE C.BStatus='B' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND C.BoatHouseId = @BoatHouseId AND D.BoatHouseId = @BoatHouseId AND "
                                     + " D.BookingType = 'R' and A.CategoryId = @CategoryId and CAST(C.BookingDate AS DATE)   BETWEEN CONVERT(date, @FromDate, 103) AND CONVERT(date, @ToDate, 103)";
                    }

                    sQuery = " select C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName, "
                            + " ISNULL(B.ServiceTotalAmount,0) AS 'ItemRate',  (ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0)) AS 'ChargePerItem', ISNULL(C.NoOfItems,0) AS 'NoOfItems', "
                           + " ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, C.BookingId, B.ServiceId),0) AS 'TaxAmount' , "
                           + " ISNULL(C.ChargePerItem,0) * ISNULL(C.NoOfItems,0) + ISNULL(dbo.GetTaxAmountDetails('AbsRestaurant', B.BoatHouseId, C.BookingId, B.ServiceId),0)  AS  'Total' ,  "
                           + " CONVERT(VARCHAR, C.BookingDate, 103) + ' ' + RIGHT(CONVERT(VARCHAR, C.BookingDate, 100), 7) AS 'BookingDate' "
                           + " from FoodCategory  as A inner Join FoodItemMaster as B on A.CategoryId = B.CategoryId inner Join BookingRestaurant as c "
                           + " on B.ServiceId = C.ServiceId inner Join BookingTaxDtl as D on C.ServiceId = D.ServiceId and C.BookingId = D.BookingId " + conditions + " "
                           + " GROUP BY C.BookingId, A.CategoryId , A.CategoryName , B.serviceId , B.ServiceName , C.ChargePerItem , C.NoOfItems , C.BookingDate, "
                           + " B.BoatHouseId, C.BookingId, B.ServiceTotalAmount ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = others.BoatHouseId.Trim();
                    cmd.Parameters["@FromDate"].Value = others.FromDate.Trim();
                    cmd.Parameters["@ToDate"].Value = others.ToDate.Trim();
                    cmd.Parameters["@ServiceId"].Value = others.ServiceId.Trim();
                    cmd.Parameters["@CategoryId"].Value = others.CategoryId.Trim();

                    List<RptRestaurant> li = new List<RptRestaurant>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptRestaurant lstOthers = new RptRestaurant();
                            lstOthers.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                            lstOthers.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            lstOthers.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            lstOthers.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            lstOthers.BookingId = dt.Rows[i]["BookingId"].ToString();
                            lstOthers.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                            lstOthers.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
                            lstOthers.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            lstOthers.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                            lstOthers.Total = dt.Rows[i]["Total"].ToString();
                            lstOthers.ItemRate = dt.Rows[i]["ItemRate"].ToString();


                            li.Add(lstOthers);
                        }

                        RptRestaurantList BoatRate = new RptRestaurantList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        RptRestaurantString BoatRate = new RptRestaurantString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    RptRestaurantString Vehicle = new RptRestaurantString
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
        /// Newly added by Brijin and Imran on 09-05-2022
        /// Modified by Brijin and Imran on 19-05-2022
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <param name="Oth"></param>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        [Route("RptOtherServiceSummary")]
        public IHttpActionResult RptOtherServiceSummary([FromBody] OthServiceCat Oth)
        {
            try
            {
                string sQuery = string.Empty;

                if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
                    && Oth.FromDate != "" && Oth.ToDate != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '27' AND A.BoatHouseId = @BoatHouseId "
                        + " AND B.BoatHouseId = @BoatHouseId AND B.OtherServiceType = 'OS' AND A.BStatus = 'B' "
                        + " AND CAST(A.BookingDate AS DATE) BETWEEN (@FromDate) AND (@ToDate)";

                    if (Oth.Category != "0")
                    {
                        condition += " AND C.ConfigID= @Category ";
                    }
                    if (Oth.ServiceId != "0")
                    {
                        condition += " AND A.ServiceId = @ServiceId";
                    }

                    if (Oth.BookingType != "0")
                    {
                        condition += " AND A.BookingType = @BookingType ";
                    }
                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");
                    if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " SELECT C.ConfigName AS 'CategoryName',  B.ServiceName, CASE WHEN A.BookingType ='I' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
                            + " ISNULL(B.ServiceTotalAmount,0) as 'ItemAmount', ISNULL(SUM(A.NoOfItems),0) as 'NoOfItems',"
                            + " ISNULL(B.ChargePerItem * SUM(A.NoOfItems),0) as 'ItemFare', ISNULL((B.ChargePerItemTax * SUM(A.NoOfItems)),0) AS 'TaxAmount', "
                            + " ISNULL(SUM(A.NetAmount),0) as 'NetAmount' FROM BookingOthers AS A"
                            + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                            + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                            + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType, B.ChargePerItem, B.ChargePerItemTax, C.ConfigID  ORDER BY C.ConfigID";
                    }
                    else if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " SELECT C.ConfigName AS 'CategoryName',  B.ServiceName, CASE WHEN A.BookingType ='I' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
                            + " ISNULL(B.ServiceTotalAmount,0) as 'ItemAmount', ISNULL(SUM(A.NoOfItems),0) as 'NoOfItems',"
                            + " ISNULL(B.ChargePerItem * SUM(A.NoOfItems),0) as 'ItemFare', ISNULL((B.ChargePerItemTax * SUM(A.NoOfItems)),0) AS 'TaxAmount', "
                            + " ISNULL(SUM(A.NetAmount),0) as 'NetAmount' FROM BookingOthersHistory AS A"
                            + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                            + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                            + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType, B.ChargePerItem, B.ChargePerItemTax, C.ConfigID  ORDER BY C.ConfigID";
                    }
                    else
                    {
                        sQuery = " SELECT  A.ConfigID,A.CategoryName,  A.ServiceName, A.BookingType,SUM(A.ItemAmount)  as 'ItemAmount', "
                           + " SUM(A.NoOfItems) as 'NoOfItems', SUM(A.ItemFare) as 'ItemFare', SUM(A.TaxAmount) as 'TaxAmount',  SUM(A.NetAmount) as 'NetAmount'"
                           + " FROM (SELECT  C.ConfigID,C.ConfigName AS 'CategoryName',  B.ServiceName, CASE WHEN A.BookingType ='I' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
                           + " ISNULL(B.ServiceTotalAmount,0) as 'ItemAmount', ISNULL(SUM(A.NoOfItems),0) as 'NoOfItems',"
                           + " ISNULL(B.ChargePerItem * SUM(A.NoOfItems),0) as 'ItemFare', ISNULL((B.ChargePerItemTax * SUM(A.NoOfItems)),0) AS 'TaxAmount', "
                           + " ISNULL(SUM(A.NetAmount),0) as 'NetAmount' FROM BookingOthers AS A"
                           + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                           + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                           + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType, B.ChargePerItem, B.ChargePerItemTax, C.ConfigID "
                           + " UNION ALL "
                           + " SELECT C.ConfigID,C.ConfigName AS 'CategoryName',  B.ServiceName, CASE WHEN A.BookingType ='I' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
                           + " ISNULL(B.ServiceTotalAmount,0) as 'ItemAmount', ISNULL(SUM(A.NoOfItems),0) as 'NoOfItems',"
                           + " ISNULL(B.ChargePerItem * SUM(A.NoOfItems),0) as 'ItemFare', ISNULL((B.ChargePerItemTax * SUM(A.NoOfItems)),0) AS 'TaxAmount', "
                           + " ISNULL(SUM(A.NetAmount),0) as 'NetAmount' FROM BookingOthersHistory AS A"
                           + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                           + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
                           + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType, B.ChargePerItem, B.ChargePerItemTax, C.ConfigID ) AS A  GROUP BY A.CategoryName, A.ServiceName, A.ItemAmount, A.ConfigID ,A.BookingType ORDER BY A.ConfigID";
                    }


                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = Oth.BoatHouseId.Trim();
                    cmd.Parameters["@Category"].Value = Oth.Category.Trim();
                    cmd.Parameters["@ServiceId"].Value = Oth.ServiceId.Trim();
                    cmd.Parameters["@BookingType"].Value = Oth.BookingType.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate);

                    List<OthServiceCat> li = new List<OthServiceCat>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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
                            OthDtl.ItemFare = dt.Rows[i]["ItemFare"].ToString();
                            OthDtl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
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
        /// Newly added by Brijin and Imran on 09-05-2022
        /// Modified by Brijin and Imran on 19-05-2022
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.   
        /// </summary>
        /// <param name="Oth"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptAbstractOtherService")]
        public IHttpActionResult RptAbstractOtherService([FromBody] OthServiceCat Oth)
        {
            try
            {
                string sQuery = string.Empty;

                if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
                    && Oth.FromDate != "" && Oth.ToDate != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '27' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId AND B.OtherServiceType = 'OS' AND A.BStatus = 'B' ";

                    if (Oth.Category != "0")
                    {
                        condition += " AND C.ConfigID = @Category";
                    }
                    if (Oth.ServiceId != "0")
                    {
                        condition += " AND A.ServiceId = @ServiceId";
                    }

                    if (Oth.BookingType != "0")
                    {
                        condition += " AND A.BookingType = @BookingType";
                    }

                    condition += " AND CAST(A.BookingDate AS DATE) BETWEEN ( @FromDate ) AND ( @ToDate )";

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
                    + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthers AS A"
                    + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                    + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                    + " GROUP BY C.ConfigID, C.ConfigName";
                    }
                    else if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
                    + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthersHistory AS A"
                    + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                    + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                    + " GROUP BY C.ConfigID, C.ConfigName";
                    }
                    else
                    {
                        sQuery = "SELECT A.CategoryId, A.CategoryName,  SUM(A.TicketCount) AS 'TicketCount', SUM(A.Amount) AS 'Amount' FROM "
                          + "( SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
                      + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthers AS A"
                      + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                      + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                      + " GROUP BY C.ConfigID, C.ConfigName"
                      + " UNION ALL"
                      + "  SELECT C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName',  SUM(A.NoOfItems) AS 'TicketCount',"
                      + " SUM(A.NetAmount) AS 'Amount' FROM BookingOthersHistory AS A"
                      + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                      + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID  " + condition + ""
                      + " GROUP BY C.ConfigID, C.ConfigName ) AS A GROUP BY A.CategoryId, A.CategoryName";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));


                    cmd.Parameters["@BoatHouseId"].Value = Oth.BoatHouseId.Trim();
                    cmd.Parameters["@Category"].Value = Oth.Category.Trim();
                    cmd.Parameters["@ServiceId"].Value = Oth.ServiceId.Trim();
                    cmd.Parameters["@BookingType"].Value = Oth.BookingType.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate);


                    List<OthServiceCat> li = new List<OthServiceCat>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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


        /// <summary>
        /// Newly added by Brijin and Imran on 09-05-2022
        /// Modified by Brijin and Imran on 19-05-2022
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.     
        /// </summary>
        /// <param name="Oth"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptOtherService")]
        public IHttpActionResult RptOtherService([FromBody] OthServiceCat Oth)
        {
            try
            {
                string sQuery = string.Empty;

                if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
                    && Oth.FromDate != "" && Oth.ToDate != "")
                {
                    string condition = string.Empty;

                    condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '27' AND A.BoatHouseId = @BoatHouseId"
                    + " AND B.BoatHouseId = @BoatHouseId  AND B.OtherServiceType = 'OS' "
                    + "  AND A.BStatus = 'B' AND CAST(A.BookingDate AS DATE) BETWEEN ( @FromDate ) AND ( @ToDate )";

                    if (Oth.Category != "0")
                    {
                        condition += " AND C.ConfigID = @Category";
                    }
                    if (Oth.ServiceId != "0")
                    {
                        condition += " AND A.ServiceId = @ServiceId";
                    }

                    if (Oth.BookingType != "0")
                    {
                        condition += " AND A.BookingType = @BookingType";
                    }

                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                        + "case when A.BookingType ='I' then 'Independent' else 'Along With Boating' end as BookingType, A.ChargePerItem,A.NoOfItems, "
                        + " ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                        + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                        + " A.NetAmount FROM BookingOthers AS A"
                        + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                        + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + " "
                        + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                        + " A.NoOfItems Order by C.ConfigID";
                    }
                    else if (DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = "SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                        + "case when A.BookingType ='I' then 'Independent' else 'Along With Boating' end as BookingType, A.ChargePerItem,A.NoOfItems, "
                        + " ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                        + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                        + " A.NetAmount FROM BookingOthersHistory AS A"
                        + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                        + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + " "
                        + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                        + " A.NoOfItems Order by C.ConfigID";
                    }
                    else
                    {
                        sQuery = "SELECT * FROM (SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                       + "case when A.BookingType ='I' then 'Independent' else 'Along With Boating' end as BookingType, A.ChargePerItem,A.NoOfItems, "
                       + " ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                       + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                       + " A.NetAmount FROM BookingOthers AS A"
                       + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                       + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + " "
                       + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                       + " A.NoOfItems"
                       + " UNION All "
                       + " SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
                       + "case when A.BookingType ='I' then 'Independent' else 'Along With Boating' end as BookingType, A.ChargePerItem,A.NoOfItems, "
                       + " ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
                       + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0))) as 'TaxAmount',"
                       + " A.NetAmount FROM BookingOthersHistory AS A"
                       + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
                       + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + " "
                       + " GROUP BY A.BookingId, A.BookingDate, C.ConfigID, C.ConfigName, A.ServiceId, B.ServiceName,A.BookingType, A.NetAmount, A.ChargePerItem,"
                       + " A.NoOfItems) AS A Order by A.CategoryId,A.BookingDate";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Category", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingType", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));


                    cmd.Parameters["@BoatHouseId"].Value = Oth.BoatHouseId.Trim();
                    cmd.Parameters["@Category"].Value = Oth.Category.Trim();
                    cmd.Parameters["@ServiceId"].Value = Oth.ServiceId.Trim();
                    cmd.Parameters["@BookingType"].Value = Oth.BookingType.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate);

                    List<OthServiceCat> li = new List<OthServiceCat>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptOtherServiceSummary")]
        //public IHttpActionResult RptOtherServiceSummary([FromBody] OthServiceCat Oth)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
        //            && Oth.FromDate != "" && Oth.ToDate != "")
        //        {

        //            string condition = string.Empty;

        //            condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '27' AND A.BoatHouseId =" + Oth.BoatHouseId.Trim() + ""
        //                + " AND B.BoatHouseId =" + Oth.BoatHouseId.Trim() + " AND B.OtherServiceType = 'OS' AND A.BStatus = 'B' "
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

        //            squery = " SELECT C.ConfigName AS 'CategoryName',  B.ServiceName, CASE WHEN A.BookingType ='I' THEN 'Independent' ELSE 'Along With Boating' END AS 'BookingType', "
        //                    + " ISNULL(B.ServiceTotalAmount,0) as 'ItemAmount', ISNULL(SUM(A.NoOfItems),0) as 'NoOfItems',"
        //                    + " ISNULL(B.ChargePerItem * SUM(A.NoOfItems),0) as 'ItemFare', ISNULL((B.ChargePerItemTax * SUM(A.NoOfItems)),0) AS 'TaxAmount', "
        //                    + " ISNULL(SUM(A.NetAmount),0) as 'NetAmount' FROM BookingOthers AS A"
        //                    + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
        //                    + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + ""
        //                    + " GROUP BY C.ConfigName, B.ServiceName, B.ServiceTotalAmount, A.BookingType, B.ChargePerItem, B.ChargePerItemTax, C.ConfigID  ORDER BY C.ConfigID";

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
        //                    OthDtl.ItemFare = dt.Rows[i]["ItemFare"].ToString();
        //                    OthDtl.TaxAmount = dt.Rows[i]["TaxAmount"].ToString();
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
        //[Route("RptAbstractOtherService")]
        //public IHttpActionResult RptAbstractOtherService([FromBody] OthServiceCat Oth)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
        //            && Oth.FromDate != "" && Oth.ToDate != "")
        //        {

        //            string condition = string.Empty;

        //            condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '27' AND A.BoatHouseId ="
        //                + Oth.BoatHouseId.Trim() + " AND B.BoatHouseId =" + Oth.BoatHouseId.Trim() + " AND B.OtherServiceType = 'OS' AND A.BStatus = 'B' ";

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

        //            condition += " AND CAST(A.BookingDate AS DATE) BETWEEN ('" + DateTime.Parse(Oth.FromDate.Trim(), objEnglishDate) + "') "
        //                + " AND ('" + DateTime.Parse(Oth.ToDate.Trim(), objEnglishDate) + "')";


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
        //[Route("RptOtherService")]
        //public IHttpActionResult RptOtherService([FromBody] OthServiceCat Oth)
        //{
        //    try
        //    {
        //        string squery = string.Empty;

        //        if (Convert.ToString(Oth.Category) != "" && Oth.ServiceId != "" && Oth.BookingType != ""
        //            && Oth.FromDate != "" && Oth.ToDate != "")
        //        {

        //            string condition = string.Empty;

        //            condition = " WHERE A.BoatHouseId = B.BoatHouseId AND C.TypeID = '27' AND A.BoatHouseId =" + Oth.BoatHouseId.Trim() + ""
        //                + " AND B.BoatHouseId =" + Oth.BoatHouseId.Trim() + "  AND B.OtherServiceType = 'OS' "
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

        //            squery = "SELECT A.BookingId, A.BookingDate, C.ConfigID AS 'CategoryId', C.ConfigName AS 'CategoryName', A.ServiceId, B.ServiceName,"
        //                + "case when A.BookingType ='I' then 'Independent' else 'Along With Boating' end as BookingType, A.ChargePerItem,A.NoOfItems, "
        //                + " ISNULL(A.ChargePerItem * A.NoOfItems, 0) as 'ServiceFare', "
        //                + " ((ISNULL(SUM(A.CGSTTaxAmount), 0) + ISNULL(SUM(A.SGSTTaxAmount), 0)) as 'TaxAmount',"
        //                + " A.NetAmount FROM BookingOthers AS A"
        //                + " INNER JOIN OtherServices AS B ON A.ServiceId = B.ServiceId"
        //                + " INNER JOIN ConfigurationMaster AS C ON B.Category = C.ConfigID " + condition + " "
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


        [HttpPost]
        [AllowAnonymous]
        [Route("GetDashBoardBookingDetails")]
        public IHttpActionResult GetDashBoardBookingDetails([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                if (PinDet.QueryType != null)
                {
                    SqlCommand cmd = new SqlCommand("GetDashBoardBookingDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", PinDet.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", PinDet.BoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", PinDet.BoatSeaterId.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(PinDet.FromDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(PinDet.ToDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatStatus", PinDet.BoatStatus.Trim());

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

        [HttpPost]
        [AllowAnonymous]
        [Route("GetDashboardBoatCount")]
        public IHttpActionResult GetDashboardBoatCount([FromBody] CommonAPIMethod Dashboard)
        {
            try
            {
                List<CancelReschedMstr> li = new List<CancelReschedMstr>();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                if (Convert.ToString(Dashboard.BoatHouseId) != null && Convert.ToString(Dashboard.BoatTypeId) != null
                    && Dashboard.BoatSeaterId != null && Dashboard.BoatStatusId != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("GetDashBoardBoatCount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "DashboardBoatCount");
                    cmd.Parameters.AddWithValue("@CorpId", Dashboard.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", Dashboard.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", Dashboard.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", Dashboard.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatStatusId", Dashboard.BoatStatusId.ToString());

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
                        da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "TableShow");
                        if (ds != null)
                        {

                            return Ok(ds);
                        }

                        else
                        {

                            return NotFound();
                        }
                    }

                    else
                    {
                        return NotFound();
                    }
                }
                return Ok();
            }

            catch (Exception ex)
            {
                CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
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
        /// Report on Available Boat Capacity
        /// Dropdown to get boattype based on boat Status
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.     
        /// </summary>
        /// <param name="boatType"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BStatus/ddlBoatType")]
        public IHttpActionResult BoatTypeStatus([FromBody] RptavailBoatCapacity boatType)
        {
            try
            {
                if (boatType.BoatHouseId != null && boatType.BoatStatus != null)
                {
                    string sQuery = string.Empty;
                    List<RptavailBoatCapacity> li = new List<RptavailBoatCapacity>();
                    con.Open();

                    sQuery = " SELECT Distinct(A.BoatTypeId),B.BoatType,A.BoatStatus FROM BoatMaster AS A INNER JOIN  BoatTypes AS B "
                           + " ON A.BoatTypeId = B.BoatTypeId AND B.ActiveStatus = 'A' WHERE A.BoatStatus = @BoatStatus AND A.BoatHouseId = @BoatHouseId";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatStatus", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
                    cmd.Parameters["@BoatStatus"].Value = boatType.BoatStatus.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptavailBoatCapacity ShowBoatTypes = new RptavailBoatCapacity();
                            ShowBoatTypes.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            ShowBoatTypes.BoatType = dt.Rows[i]["BoatType"].ToString();

                            li.Add(ShowBoatTypes);
                        }

                        RptavailBoatCapacityResList BoatTypeList = new RptavailBoatCapacityResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatTypeList);
                    }
                    else
                    {
                        RptavailBoatCapacityString BTRes = new RptavailBoatCapacityString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BTRes);
                    }
                }
                else
                {
                    RptavailBoatCapacityString typeRes = new RptavailBoatCapacityString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(typeRes);
                }
            }
            catch (Exception ex)
            {
                RptavailBoatCapacityString Vehicle = new RptavailBoatCapacityString
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }


        /// <summary>
        /// Dropdown Boatseat on BoatStatus
        /// Dropdown to get boattype based on boat Status
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.  
        /// </summary>
        /// <param name="boatSeat"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BStatus/ddlBoatSeat")]
        public IHttpActionResult SeatStatus([FromBody] RptavailBoatCapacity boatSeat)
        {
            try
            {
                if (boatSeat.BoatHouseId != null && boatSeat.BoatStatus != null && boatSeat.BoatTypeId != null)
                {
                    string sQuery = string.Empty;
                    List<RptavailBoatCapacity> li = new List<RptavailBoatCapacity>();
                    con.Open();

                    sQuery = " SELECT Distinct(A.BoatSeaterId),B.SeaterType FROM BoatMaster AS A INNER JOIN  BoatSeat AS B "
                           + " ON A.BoatSeaterId = B.BoatSeaterId AND B.ActiveStatus = 'A' WHERE A.BoatStatus = @BoatStatus "
                           + " AND A.BoatHouseId = @BoatHouseId AND A.BoatTypeId = @BoatTypeId";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatStatus", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = boatSeat.BoatHouseId.Trim();
                    cmd.Parameters["@BoatStatus"].Value = boatSeat.BoatStatus.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = boatSeat.BoatTypeId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptavailBoatCapacity ShowBoatTypes = new RptavailBoatCapacity();
                            ShowBoatTypes.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            ShowBoatTypes.SeaterType = dt.Rows[i]["SeaterType"].ToString();

                            li.Add(ShowBoatTypes);
                        }

                        RptavailBoatCapacityResList BoatTypeList = new RptavailBoatCapacityResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatTypeList);
                    }

                    else
                    {
                        RptavailBoatCapacityString BTRes = new RptavailBoatCapacityString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BTRes);
                    }
                }
                else
                {
                    RptavailBoatCapacityString typeRes = new RptavailBoatCapacityString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(typeRes);
                }
            }
            catch (Exception ex)
            {
                RptavailBoatCapacityString Vehicle = new RptavailBoatCapacityString
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }

        //Dropdown BoatName on BoatStatus
        //[HttpPost]//Dropdown to get boattype based on boat Status
        //[AllowAnonymous]
        //[Route("BStatus/ddlBoatName")]
        //public IHttpActionResult BoatNameStatus([FromBody] RptavailBoatCapacity boatSeat)
        //{
        //    try
        //    {
        //        if (boatSeat.BoatHouseId != null && boatSeat.BoatStatus != null && boatSeat.BoatTypeId != null && boatSeat.BoatSeaterId != null)
        //        {
        //            List<RptavailBoatCapacity> li = new List<RptavailBoatCapacity>();
        //            con.Open();

        //            SqlCommand cmd = new SqlCommand("SELECT Distinct(BoatNum),BoatName FROM BoatMaster  WHERE BoatStatus = '" + boatSeat.BoatStatus + "' AND BoatHouseId = '" + boatSeat.BoatHouseId + "' "
        //                + "AND BoatTypeId = '" + boatSeat.BoatTypeId + "' AND BoatSeaterId = '" + boatSeat.BoatSeaterId + "'", con);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    RptavailBoatCapacity ShowBoatTypes = new RptavailBoatCapacity();
        //                    ShowBoatTypes.BoatNum = dt.Rows[i]["BoatNum"].ToString();
        //                    ShowBoatTypes.BoatName = dt.Rows[i]["BoatName"].ToString();

        //                    li.Add(ShowBoatTypes);
        //                }

        //                RptavailBoatCapacityResList BoatTypeList = new RptavailBoatCapacityResList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(BoatTypeList);
        //            }

        //            else
        //            {
        //                RptavailBoatCapacityString BTRes = new RptavailBoatCapacityString
        //                {
        //                    Response = "No Records Found.",
        //                    StatusCode = 0
        //                };
        //                return Ok(BTRes);
        //            }
        //        }
        //        else
        //        {
        //            RptavailBoatCapacityString typeRes = new RptavailBoatCapacityString
        //            {
        //                Response = "Must Pass All Parameters",
        //                StatusCode = 0
        //            };
        //            return Ok(typeRes);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RptavailBoatCapacityString Vehicle = new RptavailBoatCapacityString
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(Vehicle);
        //    }
        //}

        /// <summary>
        /// Dropdown to get boattype based on boat Status
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.  
        /// </summary>
        /// <param name="boatSeat"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BStatus/ddlBoatName")]
        public IHttpActionResult BoatNameStatus([FromBody] RptavailBoatCapacity boatSeat)
        {
            try
            {
                if (boatSeat.BoatHouseId != null && boatSeat.BoatStatus != null && boatSeat.BoatTypeId != null && boatSeat.BoatSeaterId != null)
                {
                    string sQuery = string.Empty;
                    List<RptavailBoatCapacity> li = new List<RptavailBoatCapacity>();
                    con.Open();

                    sQuery = " SELECT Distinct(BoatNum), BoatName, BoatId FROM BoatMaster  WHERE BoatStatus = @BoatStatus AND BoatHouseId = @BoatHouseId "
                           + " AND BoatTypeId = @BoatTypeId AND BoatSeaterId = @BoatSeaterId";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatStatus", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = boatSeat.BoatHouseId.Trim();
                    cmd.Parameters["@BoatStatus"].Value = boatSeat.BoatStatus.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = boatSeat.BoatTypeId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = boatSeat.BoatSeaterId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptavailBoatCapacity ShowBoatTypes = new RptavailBoatCapacity();
                            ShowBoatTypes.BoatId = dt.Rows[i]["BoatId"].ToString();
                            ShowBoatTypes.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            ShowBoatTypes.BoatName = dt.Rows[i]["BoatName"].ToString();

                            li.Add(ShowBoatTypes);
                        }

                        RptavailBoatCapacityResList BoatTypeList = new RptavailBoatCapacityResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatTypeList);
                    }

                    else
                    {
                        RptavailBoatCapacityString BTRes = new RptavailBoatCapacityString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BTRes);
                    }
                }
                else
                {
                    RptavailBoatCapacityString typeRes = new RptavailBoatCapacityString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(typeRes);
                }
            }
            catch (Exception ex)
            {
                RptavailBoatCapacityString Vehicle = new RptavailBoatCapacityString
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(Vehicle);
            }
        }


        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.  
        /// </summary>
        /// <param name="RptavalAll"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BH/RptAvalBoatCapALL")]
        public IHttpActionResult RptAvalBoatCapALL([FromBody] RptavailBoatCapacity RptavalAll)
        {
            try
            {
                if (RptavalAll.BoatHouseId != null)
                {
                    string sQuery = string.Empty;
                    string Condition = string.Empty;

                    if (RptavalAll.BoatHouseId != null && RptavalAll.BoatStatus == "0" && RptavalAll.BoatNum == "0" && RptavalAll.BoatTypeId == "0" && RptavalAll.BoatSeaterId == "0")
                    {
                        Condition = " WHERE A.BoatHouseId = @BoatHouseId";
                    }

                    if (RptavalAll.BoatHouseId != null && RptavalAll.BoatStatus != "0" && RptavalAll.BoatNum == "0" && RptavalAll.BoatTypeId == "0" && RptavalAll.BoatSeaterId == "0")
                    {

                        Condition = " WHERE A.BoatHouseId = @BoatHouseId AND B.BoatStatus = @BoatStatus ";
                    }

                    if (RptavalAll.BoatHouseId != null && RptavalAll.BoatStatus != "0" && RptavalAll.BoatNum == "0" && RptavalAll.BoatTypeId != "0" && RptavalAll.BoatSeaterId == "0")
                    {
                        Condition = " WHERE A.BoatHouseId = @BoatHouseId AND B.BoatStatus = @BoatStatus AND A.BoatTypeId = @BoatTypeId ";
                    }

                    if (RptavalAll.BoatHouseId != null && RptavalAll.BoatStatus != "0" && RptavalAll.BoatNum == "0" && RptavalAll.BoatTypeId != "0" && RptavalAll.BoatSeaterId != "0")
                    {
                        Condition = " WHERE A.BoatHouseId = @BoatHouseId AND B.BoatStatus = @BoatStatus AND A.BoatTypeId = @BoatTypeId AND A.BoatSeaterId = @BoatSeaterId ";
                    }

                    if (RptavalAll.BoatHouseId != null && RptavalAll.BoatStatus != "0" && RptavalAll.BoatNum != "0" && RptavalAll.BoatTypeId != "0" && RptavalAll.BoatSeaterId != "0")
                    {
                        Condition = " WHERE A.BoatHouseId = @BoatHouseId AND B.BoatStatus = @BoatStatus AND A.BoatTypeId = @BoatTypeId AND A.BoatSeaterId = @BoatSeaterId AND B.BoatNum = @BoatNum ";
                    }

                    List<RptavailBoatCapacity> li = new List<RptavailBoatCapacity>();
                    con.Open();

                    sQuery = "select A.BoatTypeId,D.BoatType,A.BoatSeaterId,E.SeaterType,B.BoatNum,B.BoatName,B.BoatStatus,C.ConfigName as 'BoatStatusName',A.MaxTripsPerDay"
                                   + " from BoatRateMaster AS A Inner Join BoatMaster AS B ON A.BoatTypeId = B.BoatTypeId and A.BoatSeaterId = B.BoatSeaterId Inner Join  ConfigurationMaster AS C on "
                                   + " B.BoatStatus = C.ConfigID and C.ActiveStatus = 'A' and TypeId = 16 INNER JOIN BoatTypes AS D ON A.BoatTypeId = D.BoatTypeId AND D.ActiveStatus = 'A' "
                                   + " INNER JOIN BoatSeat AS E ON A.BoatSeaterId = E.BoatSeaterId AND E.ActiveStatus = 'A'  " + Condition + "";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatStatus", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatNum", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BoatHouseId"].Value = RptavalAll.BoatHouseId.Trim();
                    cmd.Parameters["@BoatStatus"].Value = RptavalAll.BoatStatus.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = RptavalAll.BoatTypeId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = RptavalAll.BoatSeaterId.Trim();
                    cmd.Parameters["@BoatNum"].Value = RptavalAll.BoatNum.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();


                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptavailBoatCapacity showAvalALl = new RptavailBoatCapacity();
                            showAvalALl.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            showAvalALl.BoatType = dt.Rows[i]["BoatType"].ToString();
                            showAvalALl.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            showAvalALl.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            showAvalALl.BoatNum = dt.Rows[i]["BoatNum"].ToString();
                            showAvalALl.BoatName = dt.Rows[i]["BoatName"].ToString();
                            showAvalALl.BoatStatus = dt.Rows[i]["BoatStatus"].ToString();
                            showAvalALl.BoatStatusName = dt.Rows[i]["BoatStatusName"].ToString();
                            showAvalALl.MaxTripsPerDay = dt.Rows[i]["MaxTripsPerDay"].ToString();
                            li.Add(showAvalALl);
                        }

                        RptavailBoatCapacityResList AvailList = new RptavailBoatCapacityResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(AvailList);
                    }

                    else
                    {
                        RptavailBoatCapacityString availRes = new RptavailBoatCapacityString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(availRes);
                    }
                }
                else
                {
                    RptavailBoatCapacityString availRes1 = new RptavailBoatCapacityString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
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
        /// Modified by :Brijin
        /// Modified Date : 2022-05-20
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.  
        /// </summary>
        /// <param name="RptavalSum"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BH/RptAvalBoatCapSummary")]
        public IHttpActionResult RptAvalBoatCapSum([FromBody] RptavailBoatCapacity RptavalSum)
        {
            try
            {
                string sQuery = string.Empty;
                string sQuery1 = string.Empty;

                if (RptavalSum.BoatHouseId != null && RptavalSum.TripStartTime != null)
                {
                    List<RptavailBoatCapacity> li = new List<RptavailBoatCapacity>();
                    con.Open();

                    sQuery1 = " SELECT BOOKINGID FROM BookingDtl AS A  WHERE A.BoathouseId = @BoatHouseId  AND CAST(A.TripStartTime AS DATE) BETWEEN CONVERT(date, @TripStartTime, 103) AND CONVERT(date, @TripStartTime, 103) ";

                    SqlCommand cmd1 = new SqlCommand(sQuery1, con);
                    cmd1.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd1.Parameters.Add(new SqlParameter("@TripStartTime", System.Data.SqlDbType.NVarChar));
                    cmd1.Parameters["@BoatHouseId"].Value = RptavalSum.BoatHouseId.Trim();
                    cmd1.Parameters["@TripStartTime"].Value = RptavalSum.TripStartTime.Trim();

                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    con.Close();

                    if (dt1.Rows.Count > 0)
                    {
                        sQuery = "SELECT * FROM( SELECT B.BoatTypeId,B.BoatType,SUM(A.MaxTripsPerDay) AS TripCapacity, "
                        + " SUM(A.BoatMinTotAmt * A.MaxTripsPerDay) AS TripTotalAmount FROM BoatRateMaster AS A"
                        + "  INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId WHERE A.BoathouseId = @BoatHouseId GROUP BY B.BoatType, B.BoatTypeId) AS A "
                        + " Inner JOIN(SELECT B.BoatTypeId,B.BoatType,COUNT(A.BoatTypeId) AS Rcount , Sum(InitNetAmount)AS RInitNetAmount"
                        + " FROM BookingDtl AS A INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.BoathouseId = @BoatHouseId AND A.Status IN ('B', 'R') AND CAST(A.TripStartTime AS DATE) BETWEEN CONVERT(date, @TripStartTime, 103) AND  CONVERT(date, @TripStartTime, 103) "
                        + " GROUP BY B.BoatTypeId, B.BoatType) AS B ON A.BoatTypeId = B.BoatTypeId";
                    }
                    else
                    {
                        sQuery = "SELECT * FROM( SELECT B.BoatTypeId,B.BoatType,SUM(A.MaxTripsPerDay) AS TripCapacity, "
                       + " SUM(A.BoatMinTotAmt * A.MaxTripsPerDay) AS TripTotalAmount FROM BoatRateMaster AS A"
                       + "  INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId WHERE A.BoathouseId = @BoatHouseId GROUP BY B.BoatType, B.BoatTypeId) AS A "
                       + " Inner JOIN(SELECT B.BoatTypeId,B.BoatType,COUNT(A.BoatTypeId) AS Rcount , Sum(InitNetAmount)AS RInitNetAmount"
                       + " FROM BookingDtlHistory AS A INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                       + " WHERE A.BoathouseId = @BoatHouseId AND A.Status IN ('B', 'R') AND CAST(A.TripStartTime AS DATE) BETWEEN CONVERT(date, @TripStartTime, 103) AND CONVERT(date, @TripStartTime, 103) "
                       + " GROUP BY B.BoatTypeId, B.BoatType) AS B ON A.BoatTypeId = B.BoatTypeId";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@TripStartTime", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BoatHouseId"].Value = RptavalSum.BoatHouseId.Trim();
                    cmd.Parameters["@TripStartTime"].Value = RptavalSum.TripStartTime.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RptavailBoatCapacity showAvalSUM = new RptavailBoatCapacity();
                            showAvalSUM.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            showAvalSUM.BoatType = dt.Rows[i]["BoatType"].ToString();
                            showAvalSUM.TripStartTime = dt.Rows[i]["TripCapacity"].ToString();
                            showAvalSUM.RevenueCount = dt.Rows[i]["Rcount"].ToString();
                            showAvalSUM.RevenueAmount = dt.Rows[i]["RInitNetAmount"].ToString();
                            showAvalSUM.TripTotalAmount = dt.Rows[i]["TripTotalAmount"].ToString();

                            li.Add(showAvalSUM);
                        }

                        RptavailBoatCapacityResList AvailList = new RptavailBoatCapacityResList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(AvailList);
                    }
                    else
                    {
                        RptavailBoatCapacityString availRes = new RptavailBoatCapacityString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(availRes);
                    }
                }
                else
                {
                    RptavailBoatCapacityString availRes1 = new RptavailBoatCapacityString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(availRes1);
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



        /***********************Report - TRIP SHEET SUMMARY***********************************/

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptTripSheetSummary")]
        //public IHttpActionResult RptTripSheetSummary([FromBody] RptTripSheetSummary TripSheet)
        //{
        //    if (Convert.ToString(TripSheet.BoatHouseId) != null && TripSheet.FromDate != null && TripSheet.ToDate != null && TripSheet.DepositType != null && TripSheet.BoatTypeId != null && TripSheet.BoatSeaterId != null && TripSheet.BoatId != null)
        //    {
        //        List<RptTripSheetSummary> li = new List<RptTripSheetSummary>();
        //        string deposittype = "";
        //        if (TripSheet.DepositType != "0")
        //        {
        //            deposittype = " AND A.DepRefundStatus='" + TripSheet.DepositType + "'";
        //        }

        //        string boattype = "";
        //        if (TripSheet.BoatTypeId != "0")
        //        {
        //            boattype = " AND A.BoatTypeId='" + TripSheet.BoatTypeId + "'";
        //        }

        //        string boatseater = "";
        //        if (TripSheet.BoatSeaterId != "0")
        //        {
        //            boatseater = " AND A.BoatSeaterId='" + TripSheet.BoatSeaterId + "'";
        //        }

        //        string boat = "";
        //        if (TripSheet.BoatId != "0")
        //        {
        //            boat = " AND A.ActualBoatId='" + TripSheet.BoatId + "'";
        //        }
        //        string sQuery = string.Empty;


        //        SqlCommand cmd = new SqlCommand("SELECT A.BookingId,A.BoatReferenceNo, A.BoatTypeId, C.BoatType AS 'BoatTypeName', A.BoatSeaterId, D.SeaterType as 'BoatSeaterName', "
        //                  + " A.BookingDuration, B.PaymentType, E.ConfigName as 'PaymentTypeName',ISNULL(A.InitNetAmount,0) AS 'InitNetAmount', B.CustomerName, B.CustomerMobile, "
        //                  + " A.BoatReferenceNo,ISNULL(convert(varchar(10), A.TripStartTime, 103) + right(convert(varchar(32), A.TripStartTime, 100), 8), '-') "
        //                  + " AS 'TripStartTime',ISNULL(convert(varchar(10), A.TripEndTime, 103) + right(convert(varchar(32), A.TripEndTime, 100), 8), '-') AS "
        //                  + " 'TripEndTime',CAST(DATEDIFF(HOUR, A.TripStartTime , A.TripEndTime) % 24  as nvarchar)  +':' +CAST(DATEDIFF(MINUTE, A.TripStartTime, A.TripEndTime) % 60 as nvarchar) + ':' +"
        //                  + " cast(DATEDIFF(SECOND, A.TripStartTime, A.TripEndTime) % 60 as nvarchar) as 'TravelDuration', "
        //                  + " ISNULL(A.DepRefundAmount,0) AS 'DepRefundAmount',CASE WHEN BoatDeposit='0.00' THEN 'Not Deposited'"
        //                  + " WHEN A.TripEndTime is NULL and A.DepRefundStatus = 'N'  then 'Trip Not Ended and Time Extended'" +
        //                  " WHEN A.DepRefundStatus = 'Y'  and A.BoatDeposit != A.DepRefundAmount THEN 'Pending' " +
        //                  " WHEN A.DepRefundStatus = 'N' THEN 'Not Refunded' else 'Refund 'END AS 'DepRefundStatus' " +
        //                  " FROM BookingDtl AS A JOIN BookingHdr AS B ON A.BookingId = B.BookingId "
        //                  + " JOIN BoatTypes AS C ON C.BoatTypeId = A.BoatTypeId AND C.BoatHouseId = A.BoatHouseId AND C.ActiveStatus = 'A' "
        //                  + " JOIN BoatSeat AS D ON D.BoatSeaterId = A.BoatSeaterId AND D.BoatHouseId = A.BoatHouseId AND D.ActiveStatus = 'A' "
        //                  + " JOIN ConfigurationMaster AS E ON E.ConfigID = B.PaymentType AND E.ActiveStatus = 'A' AND E.TypeID = 20 "
        //                  + " WHERE A.TripStartTime IS NOT NULL and  CAST(B.BookingDate AS DATE) BETWEEN '" + DateTime.Parse(TripSheet.FromDate, objEnglishDate) + "' AND '" + DateTime.Parse(TripSheet.ToDate, objEnglishDate) + "' "
        //                  + " AND A.BoatHouseId='" + TripSheet.BoatHouseId + "'" + deposittype + ' ' + boattype + ' ' + boatseater + ' ' + boat, con);


        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                RptTripSheetSummary ts = new RptTripSheetSummary();
        //                ts.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                ts.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
        //                ts.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                ts.BoatTypeName = dt.Rows[i]["BoatTypeName"].ToString();
        //                ts.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                ts.BoatSeaterName = dt.Rows[i]["BoatSeaterName"].ToString();
        //                ts.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
        //                ts.PaymentType = dt.Rows[i]["PaymentType"].ToString();
        //                ts.PaymentTypeName = dt.Rows[i]["PaymentTypeName"].ToString();
        //                ts.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
        //                ts.CustomerName = dt.Rows[i]["CustomerName"].ToString();
        //                ts.CustomerMobile = dt.Rows[i]["CustomerMobile"].ToString();
        //                ts.TripStartTime = dt.Rows[i]["TripStartTime"].ToString();
        //                ts.TripEndTime = dt.Rows[i]["TripEndTime"].ToString();
        //                ts.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
        //                ts.DepRefundAmount = dt.Rows[i]["DepRefundAmount"].ToString();
        //                ts.DepRefundStatus = dt.Rows[i]["DepRefundStatus"].ToString();
        //                li.Add(ts);
        //            }
        //            RptTripSheetSummaryList ItemMasters = new RptTripSheetSummaryList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(ItemMasters);
        //        }
        //        else
        //        {
        //            RptTripSheetSummaryRes ItemMasters1 = new RptTripSheetSummaryRes
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(ItemMasters1);
        //        }
        //    }
        //    else
        //    {

        //        RptTripSheetSummaryRes ItemMasters1 = new RptTripSheetSummaryRes
        //        {
        //            Response = "Must Pass All Parameters",
        //            StatusCode = 0
        //        };
        //        return Ok(ItemMasters1);
        //    }
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptTripSheetSummary/NoOfTrips")]
        //public IHttpActionResult RptTripSheetSummaryNoOfTrips([FromBody] RptTripSheetSummary TripSheet)
        //{
        //    if (Convert.ToString(TripSheet.BoatHouseId) != null && TripSheet.FromDate != null
        //        && TripSheet.ToDate != null && TripSheet.DepositType != null && TripSheet.BoatTypeId != null && TripSheet.BoatSeaterId != null)
        //    {
        //        List<RptTripSheetSummary> li = new List<RptTripSheetSummary>();
        //        string sQuery = "select BDL.BoatSeaterId,BDL.BoatTypeId,BDL.BoatHouseName,BDL.BoatHouseId, "
        //                    + " (select BoatType from BoatTypes BTY where BTY.BoatTypeId = BDL.BoatTypeId and BTY.BoatHouseId = BDL.BoatHouseId)  BoatType, "
        //                    + " (select SeaterType from BoatSeat BS  where BS.BoatSeaterId = BDL.BoatSeaterId and BS.BoatHouseId = BDL.BoatHouseId) BoatSeater,  "
        //                    + " CAST(count(BDL.BookingID) AS int) as NoOfTrips,  sum(ISNULL(InitNetAmount, 0)) as 'CollectionAmt',sum(ISNULL(BoatDeposit, 0)) as 'Deposit', "
        //                    + "ISNULL(sum(case DepRefundStatus when 'Y' then ISNULL(DepRefundAmount, 0) end), 0) 'ClaimedDeposit', "
        //                    + "ISNULL(sum(case DepRefundStatus when 'N' then ISNULL(DepRefundAmount, 0) end), 0) 'UnClaimedDeposit', "
        //                    + "sum(case when(ISNULL(BoatDeposit, 0) - ISNULL(DepRefundAmount, 0)) = 0 then 0 else (ISNULL(BoatDeposit, 0) - ISNULL(DepRefundAmount, 0)) end) 'ExtendedAmt', "
        //                    + "sum(ISNULL(ActualRowerCharge, 0)) as 'Roweramt',"
        //                    + "sum(case  when  SettlementID is null then 0 else ISNULL(ActualRowerCharge, 0) end) 'RowerSettlementAmt' "
        //                    + "from BookingDtl BDL inner join BookingHdr BHD on BDL.BookingId = BHD.BookingId and BDL.BoatHouseId = BHD.BoatHouseId "
        //                    + "where BDL.BoatHouseId = '" + TripSheet.BoatHouseId.Trim() + "'  AND BDL.TripStartTime IS NOT NULL "
        //                    + "and cast(BHD.BookingDate  AS DATE) >= '" + DateTime.Parse(TripSheet.FromDate, objEnglishDate) + "' and "
        //                    + "cast(BHD.BookingDate  AS DATE) <= '" + DateTime.Parse(TripSheet.ToDate, objEnglishDate) + "' "
        //                    + "group by BoatTypeId,BoatSeaterId,BDL.BoatHouseId , BDL.BoatSeaterId , BDL.BoatTypeId , BDL.BoatHouseName";
        //        SqlCommand cmd = new SqlCommand(sQuery, con);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                RptTripSheetSummary ts = new RptTripSheetSummary();

        //                ts.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
        //                ts.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                ts.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                ts.BoatSeaterName = dt.Rows[i]["BoatSeater"].ToString();
        //                ts.NoOfTrips = dt.Rows[i]["NoOfTrips"].ToString();
        //                ts.RefundAmt = dt.Rows[i]["ClaimedDeposit"].ToString();
        //                ts.CollectionAmt = dt.Rows[i]["CollectionAmt"].ToString();
        //                ts.UnClaimbedDeposit = dt.Rows[i]["UnClaimedDeposit"].ToString();
        //                ts.ExtendedCharge = dt.Rows[i]["ExtendedAmt"].ToString();
        //                ts.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                ts.RowerAmount = dt.Rows[i]["Roweramt"].ToString();
        //                ts.RowerSettlement = dt.Rows[i]["RowerSettlementAmt"].ToString();
        //                li.Add(ts);
        //            }
        //            RptTripSheetSummaryList ItemMasters = new RptTripSheetSummaryList
        //            {
        //                Response = li,
        //                StatusCode = 1
        //            };
        //            return Ok(ItemMasters);
        //        }
        //        else
        //        {
        //            RptTripSheetSummaryRes ItemMasters1 = new RptTripSheetSummaryRes
        //            {
        //                Response = "No Records Found.",
        //                StatusCode = 0
        //            };
        //            return Ok(ItemMasters1);
        //        }

        //    }
        //    else
        //    {

        //        RptTripSheetSummaryRes ItemMasters1 = new RptTripSheetSummaryRes
        //        {
        //            Response = "Must Pass All Parameters",
        //            StatusCode = 0
        //        };
        //        return Ok(ItemMasters1);
        //    }
        //}



        /***********************Report - Booking Cancellation ***********************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.   
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptBookingCancelDetails")]
        public IHttpActionResult RptBookingCancelDetails([FromBody] CancelReschedMstr Cancel)
        {
            try
            {
                if (Cancel.BoatHouseId != null)
                {
                    string sQuery = string.Empty;
                    string sCondition = string.Empty;

                    if (Cancel.BoatTypeId != "0")
                    {
                        sCondition += " AND A.BoatTypeId = @BoatTypeId ";
                    }

                    if (Cancel.BoatSeaterId != "0")
                    {
                        sCondition += " AND A.BoatSeaterId = @BoatSeaterId ";
                    }

                    if (Cancel.PaymentType != "0")
                    {
                        sCondition += " AND A.RePaymentType = @PaymentType";
                    }


                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();
                    con.Open();
                    sQuery = " SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                     + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtl AS A "
                     + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                     + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                     + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                     + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                     + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                     + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + " ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = Cancel.BoatTypeId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = Cancel.BoatSeaterId.Trim();
                    cmd.Parameters["@PaymentType"].Value = Cancel.PaymentType.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Cancel.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Cancel.ToDate.Trim(), objEnglishDate);

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
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.MobileNo = dt.Rows[i]["CustomerMobile"].ToString();
                            CancelBooking.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            CancelBooking.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            CancelBooking.BoatType = dt.Rows[i]["BoatType"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.CancelCharges = dt.Rows[i]["CancelCharges"].ToString();
                            CancelBooking.CancelRefund = dt.Rows[i]["CancelRefund"].ToString();

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
                        Response = "Must Pass Boat House Id ",
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
        /// Create By : Pretheka
        /// Created Date : 2022-04-19      
        /// Version : V2
        /// Newly added by Imran on 2022-05-24 Version 3.
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.  
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptBookingCancelDetailsV2")]
        public IHttpActionResult RptBookingCancelDetailsV2([FromBody] CancelReschedMstr Cancel)
        {
            try
            {
                if (Cancel.BoatHouseId != null)
                {
                    string sQuery = string.Empty;
                    string sCondition = string.Empty;

                    if (Cancel.BoatTypeId != "0")
                    {
                        sCondition += " AND A.BoatTypeId = @BoatTypeId ";
                    }

                    if (Cancel.BoatSeaterId != "0")
                    {
                        sCondition += " AND A.BoatSeaterId = @BoatSeaterId";
                    }

                    if (Cancel.PaymentType != "0")
                    {
                        sCondition += " AND A.RePaymentType = @PaymentType";
                    }


                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();
                    int endcount = Int32.Parse(Cancel.CountStart.Trim()) + 9;
                    con.Open();


                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(Cancel.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Cancel.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingDate) 'RowNumber', * FROM ("
                       + "  SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                       + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtl AS A "
                       + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                       + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                       + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                       + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ") AS A)"
                       + " AS B where B.RowNumber BETWEEN @CountStart AND @EndCount ORDER BY B.RowNumber ASC ";
                    }
                    else if (DateTime.Parse(Cancel.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Cancel.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingDate) 'RowNumber', * FROM ("
                       + "  SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                       + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtlHistory AS A "
                       + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                       + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                       + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                       + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ") AS A)"
                       + " AS B where B.RowNumber BETWEEN @CountStart AND @EndCount ORDER BY B.RowNumber ASC ";
                    }
                    else
                    {
                        sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingDate) 'RowNumber', * FROM ("
                       + "  SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                       + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtl AS A "
                       + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                       + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                       + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                       + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ""
                       + " UNION ALL"
                       + " SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                       + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtlHistory AS A "
                       + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                       + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                       + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                       + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                       + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ") AS A)"
                       + " AS B where B.RowNumber BETWEEN @CountStart AND @EndCount ORDER BY B.RowNumber ASC ";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@EndCount", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = Cancel.BoatTypeId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = Cancel.BoatSeaterId.Trim();
                    cmd.Parameters["@PaymentType"].Value = Cancel.PaymentType.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Cancel.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Cancel.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@CountStart"].Value = Cancel.CountStart.Trim();
                    cmd.Parameters["@EndCount"].Value = endcount;

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
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.MobileNo = dt.Rows[i]["CustomerMobile"].ToString();
                            CancelBooking.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            CancelBooking.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            CancelBooking.BoatType = dt.Rows[i]["BoatType"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.CancelCharges = dt.Rows[i]["CancelCharges"].ToString();
                            CancelBooking.CancelRefund = dt.Rows[i]["CancelRefund"].ToString();
                            CancelBooking.RowNumber = dt.Rows[i]["RowNumber"].ToString();

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
                        Response = "Must Pass Boat House Id ",
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
        /// NEWLY ADDED BY ABHINAYA K
        /// ADDED DATE 20APR2022
        /// Modified by Brijin On 2022-05-24
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="RowCharges"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("RptBookingCancelListPinV2")]
        public IHttpActionResult RptBookingCancelListPinV2([FromBody] CancelReschedMstr Cancel)
        {
            try
            {
                if (Cancel.BoatHouseId != null)
                {
                    string sQuery = string.Empty;
                    string sCondition = string.Empty;

                    if (Cancel.BoatTypeId != "0")
                    {
                        sCondition += " AND A.BoatTypeId = @BoatTypeId ";
                    }

                    if (Cancel.BoatSeaterId != "0")
                    {
                        sCondition += " AND A.BoatSeaterId = @BoatSeaterId ";
                    }

                    if (Cancel.PaymentType != "0")
                    {
                        sCondition += " AND A.RePaymentType = @PaymentType";
                    }


                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();

                    con.Open();
                    string Last7Days = DateTime.Today.AddDays(-7).ToString("dd-MM-yyyy");

                    if (DateTime.Parse(Cancel.FromDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                        && DateTime.Parse(Cancel.ToDate.Trim(), objEnglishDate) > DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingDate) 'RowNumber', * FROM ("
                        + " SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                        + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtl AS A "
                        + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                        + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                        + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                        + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ") AS A)"
                        + " AS B where B.BookingId = @CountStart  ORDER BY B.RowNumber ASC ";

                    }
                    else if (DateTime.Parse(Cancel.FromDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate)
                       && DateTime.Parse(Cancel.ToDate.Trim(), objEnglishDate) <= DateTime.Parse(Last7Days.Trim(), objEnglishDate))
                    {
                        sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingDate) 'RowNumber', * FROM ("
                        + " SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                        + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtlHistory AS A "
                        + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                        + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                        + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                        + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                        + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ") AS A)"
                        + " AS B where B.BookingId = @CountStart  ORDER BY B.RowNumber ASC ";

                    }
                    else
                    {
                        sQuery = " SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingDate) 'RowNumber', * FROM ("
                      + "  SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                      + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtl AS A "
                      + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                      + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                      + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                      + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                      + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                      + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ""
                      + " UNION ALL"
                      + " SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
                      + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtlHistory AS A "
                      + " INNER JOIN BookingHdrHistory AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                      + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
                      + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
                      + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
                      + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = @BoatHouseId AND CAST(B.BookingDate AS DATE) BETWEEN "
                      + " @FromDate AND @ToDate " + sCondition.ToString().Trim() + ") AS A)"
                      + " AS B where B.BookingId = @CountStart ORDER BY B.RowNumber ASC ";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PaymentType", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@CountStart", System.Data.SqlDbType.Int));

                    cmd.Parameters["@BoatHouseId"].Value = Cancel.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = Cancel.BoatTypeId.Trim();
                    cmd.Parameters["@BoatSeaterId"].Value = Cancel.BoatSeaterId.Trim();
                    cmd.Parameters["@PaymentType"].Value = Cancel.PaymentType.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Cancel.FromDate.Trim(), objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Cancel.ToDate.Trim(), objEnglishDate);
                    cmd.Parameters["@CountStart"].Value = Cancel.CountStart.Trim();

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
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.MobileNo = dt.Rows[i]["CustomerMobile"].ToString();
                            CancelBooking.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            CancelBooking.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            CancelBooking.BoatType = dt.Rows[i]["BoatType"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.CancelCharges = dt.Rows[i]["CancelCharges"].ToString();
                            CancelBooking.CancelRefund = dt.Rows[i]["CancelRefund"].ToString();
                            CancelBooking.RowNumber = dt.Rows[i]["RowNumber"].ToString();

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
                        Response = "Must Pass Boat House Id ",
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

        ///// <summary>
        ///// Create By : Pretheka
        ///// Created Date : 2022-04-19
        ///// Version: V2
        ///// </summary>
        ///// <param name="Cancel"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("RptBookingCancelListPinV2")]
        //public IHttpActionResult RptBookingCancelListPinV2([FromBody] CancelReschedMstr Cancel)
        //{
        //    try
        //    {
        //        if (Cancel.BoatHouseId != null)
        //        {
        //            string sCondition = string.Empty;

        //            if (Cancel.BoatTypeId != "0")
        //            {
        //                sCondition += " AND A.BoatTypeId= '" + Cancel.BoatTypeId.ToString().Trim() + "'";
        //            }


        //            if (Cancel.BoatSeaterId != "0")
        //            {
        //                sCondition += " AND A.BoatSeaterId= '" + Cancel.BoatSeaterId.ToString().Trim() + "'";
        //            }

        //            if (Cancel.PaymentType != "0")
        //            {
        //                sCondition += " AND A.RePaymentType= '" + Cancel.PaymentType.ToString().Trim() + "'";
        //            }


        //            List<CancelReschedMstr> li = new List<CancelReschedMstr>();

        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT * FROM  (SELECT  ROW_NUMBER() OVER(ORDER BY A.BookingDate) 'RowNumber', * FROM ("
        //            + " SELECT A.BookingId,A.BoatReferenceNo,Convert(Nvarchar(50),B.BookingDate,105) 'BookingDate',A.BoatTypeId,C.BoatType,A.BoatSeaterId,D.SeaterType, "
        //            + " B.CustomerName, B.CustomerMobile, A.BoatHouseName, A.CancelCharges, A.CancelRefund, E.ConfigName 'PaymentType' FROM BookingDtl AS A "
        //            + " INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
        //            + " INNER JOIN BoatTypes AS C ON A.BoatTypeId = C.BoatTypeId AND A.BoatHouseId = C.BoatHouseId AND C.ActiveStatus = 'A' "
        //            + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND A.BoatHouseId = D.BoatHouseId AND C.ActiveStatus = 'A' "
        //            + " INNER JOIN ConfigurationMaster AS E ON A.RePaymentType = E.ConfigId AND E.TypeID = 20 "
        //            + " WHERE B.Status IN ('P', 'C') AND A.Status = 'C' AND A.BoatHouseId = '" + Cancel.BoatHouseId.Trim() + "' AND CAST(B.BookingDate AS DATE) BETWEEN "
        //            + " '" + DateTime.Parse(Cancel.FromDate, objEnglishDate) + "' AND '" + DateTime.Parse(Cancel.ToDate, objEnglishDate) + "'" + sCondition.ToString().Trim() + ") AS A)"
        //            + " AS B where B.BookingId='" + Cancel.CountStart + "'  ORDER BY B.RowNumber ASC ", con);

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {

        //                    CancelReschedMstr CancelBooking = new CancelReschedMstr();
        //                    CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    CancelBooking.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
        //                    CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
        //                    CancelBooking.MobileNo = dt.Rows[i]["CustomerMobile"].ToString();
        //                    CancelBooking.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                    CancelBooking.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                    CancelBooking.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                    CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
        //                    CancelBooking.BoatSeater = dt.Rows[i]["SeaterType"].ToString();
        //                    CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                    CancelBooking.CancelCharges = dt.Rows[i]["CancelCharges"].ToString();
        //                    CancelBooking.CancelRefund = dt.Rows[i]["CancelRefund"].ToString();
        //                    CancelBooking.RowNumber = dt.Rows[i]["RowNumber"].ToString();

        //                    li.Add(CancelBooking);
        //                }
        //                CancelReschedMstrList ConfList = new CancelReschedMstrList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(ConfList);
        //            }


        //            else
        //            {
        //                CancelReschedMstrRes ConfRes1 = new CancelReschedMstrRes
        //                {
        //                    Response = "No Records Found",
        //                    StatusCode = 0
        //                };
        //                return Ok(ConfRes1);
        //            }
        //        }

        //        else
        //        {
        //            CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
        //            {
        //                Response = "Must Pass Boat House Id ",
        //                StatusCode = 0
        //            };
        //            return Ok(ConfRes);

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


        /***** Online Booked Histrory *****/

        /// <summary>
        /// Modified By : Imran
        /// Modified DAte : 2021-10-22
        /// Remove two fields.
        /// </summary>
        /// <param name="Btl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PublicBookedHistory")]
        public IHttpActionResult PublicBookedHistory([FromBody] PublicBookedHistory Btl)
        {
            try
            {

                List<PublicBookedHistory> li = new List<PublicBookedHistory>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "PublicBookedHistory");
                cmd.Parameters.AddWithValue("@UserId", Btl.UserId.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PublicBookedHistory bt = new PublicBookedHistory();

                        //bt.TransactionNo = dt.Rows[i]["TransactionNo"].ToString();
                        bt.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.Amount = dt.Rows[i]["Amount"].ToString();
                        //bt.BookingType = dt.Rows[i]["BookingType"].ToString();
                        bt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        bt.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        bt.BoardingPassStatus = dt.Rows[i]["BoardingPassStatus"].ToString();
                        bt.TripStartStatus = dt.Rows[i]["TripStartStatus"].ToString();
                        bt.BookingStatus = dt.Rows[i]["BookingStatus"].ToString();

                        li.Add(bt);
                    }

                    PublicBookedHistorylist Bdl = new PublicBookedHistorylist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Bdl);
                }

                else
                {
                    PublicBookedHistoryStr FBRes = new PublicBookedHistoryStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(FBRes);
                }
            }
            catch (Exception ex)
            {
                PublicBookedHistoryStr ConfRes = new PublicBookedHistoryStr
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
        /// Created By : Imran
        /// Crreated Date : 27-10-2021
        /// </summary>
        /// <param name="Btl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PublicOthersBookedHistory")]
        public IHttpActionResult PublicOthersBookedHistory([FromBody] PublicBookedHistory Btl)
        {
            try
            {

                List<PublicBookedHistory> li = new List<PublicBookedHistory>();
                SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", "PublicOthersBookedHistory");
                cmd.Parameters.AddWithValue("@UserId", Btl.UserId.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PublicBookedHistory bt = new PublicBookedHistory();


                        bt.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        bt.BookingId = dt.Rows[i]["BookingId"].ToString();
                        bt.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                        bt.Amount = dt.Rows[i]["Amount"].ToString();
                        bt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        bt.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        bt.BookingStatus = dt.Rows[i]["BookingStatus"].ToString();


                        li.Add(bt);
                    }

                    PublicBookedHistorylist Bdl = new PublicBookedHistorylist
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(Bdl);
                }

                else
                {
                    PublicBookedHistoryStr FBRes = new PublicBookedHistoryStr
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(FBRes);
                }
            }
            catch (Exception ex)
            {
                PublicBookedHistoryStr ConfRes = new PublicBookedHistoryStr
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

        /******************************** Public - Boat Booking Cancellation **********************************/

        /// <summary>
        /// Modified By : Imran
        /// Modified Date : 24-09-2021
        /// Modified By : Imran
        /// Modified Date : 13-10-2021
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BookingDetailsBasedOnUserId")]
        public IHttpActionResult BookingDetailsBasedOnUserId([FromBody] BoatBooking Cancel)
        {
            try
            {
                if (Convert.ToString(Cancel.UserId) != null)
                {
                    List<BoatBooking> li = new List<BoatBooking>();
                    string sQuery = string.Empty;

                    sQuery = " SELECT A.BookingId,B.PaymentType 'PaymentTypeId',A.BoatHouseId,A.BoatHouseName,B.CustomerName, "
                     + "   B.CustomerMobile, convert(varchar(10), B.BookingDate, 101) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate', "
                     + "   C.ConfigName 'PaymentType', B.UserId, "
                     + "   CASE WHEN B.PremiumStatus = 'P' THEN 'Premium' ELSE 'Normal' END AS PremiumStatus  FROM BookingDtl AS A "
                     + "   INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
                     + "   INNER JOIN ConfigurationMaster AS C ON B.PaymentType = C.ConfigId AND A.BoatHouseId = B.BoatHouseId  AND C.TypeID = 20 "
                     + "   WHERE  A.Status IN('B', 'R') AND  B.UserId = @UserId AND B.BookingMedia IN('PW', 'PA') AND A.TripStartTime IS NULL AND "
                     + "   CAST(B.BookingDate AS DATE) >= CAST(GETDATE() AS DATE) "
                     + "   GROUP BY A.BookingId, B.UserId, B.CustomerMobile, B.CustomerName, B.BookingDate, C.ConfigName, "
                     + "   A.BoatHouseName, A.BoatHouseId, B.BookingPin, B.PaymentType, B.PremiumStatus";

                    con.Open();
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@UserId"].Value = Cancel.UserId.Trim();
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
                            CancelBooking.PaymentTypeId = dt.Rows[i]["PaymentTypeId"].ToString();
                            CancelBooking.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.CustomerMobileNo = dt.Rows[i]["CustomerMobile"].ToString();
                            CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            //CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            CancelBooking.UserId = dt.Rows[i]["UserId"].ToString();

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
                            Response = "No Booking Details Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
                    }
                }
                else
                {
                    CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
                    {
                        Response = "Must Pass User Id",
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


        //[HttpPost]
        //[AllowAnonymous]
        //[Route("BookingDetailsBasedOnUserId")]
        //public IHttpActionResult BookingDetailsBasedOnUserId([FromBody] BoatBooking Cancel)
        //{
        //    try
        //    {
        //        if (Convert.ToString(Cancel.UserId) != null)
        //        {
        //            List<BoatBooking> li = new List<BoatBooking>();
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand("SELECT A.BookingId,B.BookingPin,B.PaymentType 'PaymentTypeId',A.BoatHouseId,A.BoatHouseName,B.CustomerName, B.CustomerMobile, "
        //           + "  convert(varchar(10), B.BookingDate, 101) + right(convert(varchar(32), B.BookingDate, 100), 8) AS 'BookingDate', "
        //           + " C.ConfigName 'PaymentType', ISNULL(SUM(A.initNetAmount), 0) 'initNetAmount', ISNULL(SUM(A.BoatDeposit), 0) 'BoatDeposit', "
        //           + "  ISNULL(SUM(A.initBoatCharge), 0) 'initBoatCharge', ISNULL(SUM(A.InitRowerCharge), 0) 'InitRowerCharge', B.UserId, "
        //           + " CASE WHEN B.PremiumStatus='P' THEN 'Premium' ELSE 'Normal' END AS PremiumStatus  FROM BookingDtl AS A "
        //           + "  INNER JOIN BookingHdr AS B ON A.BookingId = B.BookingId AND A.BoatHouseId = B.BoatHouseId "
        //           + " INNER JOIN ConfigurationMaster AS C ON B.PaymentType = C.ConfigId AND A.BoatHouseId = B.BoatHouseId  AND C.TypeID = 20 "
        //           + " WHERE  A.Status IN('B', 'R') AND  B.UserId ='" + Cancel.UserId.Trim() + "' AND B.BookingMedia IN('PW', 'PA') AND A.TripStartTime IS NULL AND CAST(B.BookingDate AS DATE) >= CAST(GETDATE() AS DATE)  "
        //           + "  GROUP BY A.BookingId, B.UserId, B.CustomerMobile, B.CustomerName, B.BookingDate, C.ConfigName, A.BoatHouseName,A.BoatHouseId,B.BookingPin,B.PaymentType,B.PremiumStatus ", con);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {

        //                    BoatBooking CancelBooking = new BoatBooking();
        //                    CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
        //                    CancelBooking.PaymentTypeId = dt.Rows[i]["PaymentTypeId"].ToString();
        //                    CancelBooking.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
        //                    CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                    CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
        //                    CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
        //                    CancelBooking.CustomerMobileNo = dt.Rows[i]["CustomerMobile"].ToString();
        //                    CancelBooking.InitNetAmount = dt.Rows[i]["initNetAmount"].ToString();
        //                    CancelBooking.InitBoatCharge = dt.Rows[i]["initBoatCharge"].ToString();
        //                    CancelBooking.InitRowerCharge = dt.Rows[i]["InitRowerCharge"].ToString();
        //                    CancelBooking.PaymentType = dt.Rows[i]["PaymentType"].ToString();
        //                    CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
        //                    CancelBooking.UserId = dt.Rows[i]["UserId"].ToString();

        //                    li.Add(CancelBooking);
        //                }
        //                BoatBookingList ConfList = new BoatBookingList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(ConfList);
        //            }


        //            else
        //            {
        //                CancelReschedMstrRes ConfRes1 = new CancelReschedMstrRes
        //                {
        //                    Response = "No Booking Details Found",
        //                    StatusCode = 0
        //                };
        //                return Ok(ConfRes1);
        //            }
        //        }

        //        else
        //        {
        //            CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
        //            {
        //                Response = "Must Pass User Id",
        //                StatusCode = 0
        //            };
        //            return Ok(ConfRes);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        //        {
        //            Response = Convert.ToString(ex),
        //            StatusCode = 0
        //        };
        //        return Ok(ConfRes);
        //    }

        //}

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/PublicDDLPayType")]
        public IHttpActionResult PublicDDLPayType()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                string sQuery = string.Empty;

                sQuery = "SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId=20 AND ConfigId !=1";

                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowConfMstr = new ConfigurationMaster();
                        ShowConfMstr.ConfigId = dt.Rows[i]["ConfigID"].ToString();
                        ShowConfMstr.ConfigName = dt.Rows[i]["ConfigName"].ToString();

                        li.Add(ShowConfMstr);
                    }

                    ConfigurationMasterList ConfList = new ConfigurationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
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

        /******************************** Public - Boat Booking ReSchedulling **********************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("GetOnlineBBDetails")]
        public IHttpActionResult GetOnlineBBDetails(OnlineBoatBooking bt)
        {
            try
            {
                if (bt.TransactionNo != null && bt.MobileNo != null)
                {
                    SqlCommand cmd = new SqlCommand("GetOnlineBoatBookingDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@TransactionNo", bt.TransactionNo.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", bt.MobileNo.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", bt.BoatHouseId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "TableShow");
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

        /******************************** Boarding Pass **********************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("Boardingpass/UserId")]
        public IHttpActionResult Boardingpass([FromBody] BoardingPass Board)
        {
            try
            {
                if (Board.UserId != null)
                {
                    List<BoardingPass> li = new List<BoardingPass>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "PBoardingPass");
                    cmd.Parameters.AddWithValue("@UserId", Board.UserId.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            BoardingPass CancelBooking = new BoardingPass();
                            CancelBooking.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
                            CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            li.Add(CancelBooking);
                        }
                        BoardingPassList ConfList = new BoardingPassList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        BoardingPassRes ConfRes1 = new BoardingPassRes
                        {
                            Response = "No Booking Details Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.    
        /// </summary>
        /// <param name="Trip"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoardPassNextBoat")]
        public IHttpActionResult BoardingpassId([FromBody] BoardingPass Trip)
        {
            try
            {
                if (Trip.BoatHouseId != null && Trip.BookingId != null)
                {
                    List<BoardingPass> li = new List<BoardingPass>();

                    string ExeQuery = "EXECUTE [dbo].[spGetNextBoatDetail] @BoatHouseId, @BookingId ";

                    SqlCommand cmdExe = new SqlCommand(ExeQuery, con);

                    cmdExe.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmdExe.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.NVarChar));
                    cmdExe.Parameters["@BoatHouseId"].Value = Trip.BoatHouseId.Trim();
                    cmdExe.Parameters["@BookingId"].Value = Trip.BookingId.Trim();

                    con.Open();
                    int i = cmdExe.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        BoardingPassRes ConfRes = new BoardingPassRes
                        {
                            Response = "Success",
                            StatusCode = 1
                        };
                        return Ok(ConfRes);
                    }
                    else
                    {
                        AbstractOtherServiceRes ConfRes = new AbstractOtherServiceRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    AbstractOtherServiceRes Vehicle = new AbstractOtherServiceRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                AbstractOtherServiceRes ConfRes = new AbstractOtherServiceRes
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
        /// Modified by : Imran
        /// Modified Date : 2021-10-06
        /// Modified by : Jaya Suriya
        /// Modified Date : 2021-11-13
        /// Added BST.ActiveStatus='A' For  BoatSlotMaster
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="Board"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Boardingpass/BookingId")]
        public IHttpActionResult BoardingpassBookingid([FromBody] BoardingPass Board)
        {
            try
            {
                if (Board.BoatHouseId != null && Board.BookingId != null)
                {
                    string sQuery = string.Empty;

                    sQuery = "SELECT * FROM "
                                + "  (SELECT A.ActualBoatId, A.ExpectedTime, A.BookingId, A.BookingSerial, B.UserId, convert(nvarchar, CAST(B.BookingDate as time), 100) as BoardingTime, "
                                + "  A.BoatHouseId, A.BoatHouseName, A.BoatTypeId, C.BoatType, A.BoatSeaterId, B.CustomerName, "
                                + "  A.BoatDeposit, isnull(A.ActualBoatNum, 0) as ActualBoatNum, D.SeaterType, A.BoatReferenceNo, "
                                + "  case when B.PremiumStatus = 'P' then 'Permium' else 'Normal' end as PremiumStatus,  "
                                + "  convert(varchar, B.BookingDate, 100) as BookingDate, A.BookingDuration, A.InitNetAmount,  A.BookingPin, "
                                + "  ISNULL((CONVERT(VARCHAR(5), BST.SlotStartTime, 108) + '-' + CONVERT(VARCHAR(5), BST.SlotEndTime, 108)), '-') "
                                + "  AS 'SlotTime'  FROM BookingDtl AS A  "
                                + "  INNER JOIN BookingHdr AS B ON A.BoatHouseId = B.BoatHouseId AND A.BookingId = B.BookingId  "
                                + "  inner Join BoatTypes as C on A.BoatTypeId = C.BoatTypeId  "
                                + "  inner Join BoatSeat as D on A.BoatSeaterId = D.BoatSeaterId and A.BoatHouseId = D.BoatHouseId  "
                                + "  INNER JOIN BoatSlotMaster AS BST ON A.TimeSlotId = BST.SlotId AND BST.ActiveStatus='A' "
                                + "  where A.BoatHouseId = @BoatHouseId and A.BookingId = '@BookingId ) AS AA  "
                                + "  LEFT JOIN  "
                                + " (SELECT OrderId, TransactionNo, TrackingId, BankReferenceNo, BoatHouseId  "
                                + "  from ["+ ConfigurationManager.AppSettings["CommonDB"] +"].[dbo].[OnlineBookingAfterTransaction] WITH (NOLOCK) WHERE "
                                + "  TransactionNo= (SELECT TOP 1 TransactionNo FROM["+ ConfigurationManager.AppSettings["CommonDB"] +"].[dbo].[OnlineBookingTransactionHistory] "
                                + "  WITH(NOLOCK) WHERE BoatHouseId = BoatHouseId AND BookingId = @BookingId  ORDER BY TransactionNo DESC) "
                                + "  ) AS BB ON AA.BoatHouseId = BB.BoatHouseId "
                                + "   order by BookingPin ASc";


                    List<BoardingPass> li = new List<BoardingPass>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BoatHouseId"].Value = Board.BoatHouseId.Trim();
                    cmd.Parameters["@BookingId"].Value = Board.BookingId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();


                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoardingPass CancelBooking = new BoardingPass();
                            CancelBooking.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
                            CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
                            CancelBooking.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            CancelBooking.BoatType = dt.Rows[i]["BoatType"].ToString();
                            CancelBooking.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            CancelBooking.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
                            CancelBooking.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
                            CancelBooking.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
                            CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
                            CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                            CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
                            CancelBooking.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
                            CancelBooking.UserId = dt.Rows[i]["UserId"].ToString();
                            CancelBooking.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
                            CancelBooking.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
                            CancelBooking.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
                            CancelBooking.ActualBoatId = dt.Rows[i]["ActualBoatId"].ToString();
                            CancelBooking.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
                            CancelBooking.OrderId = dt.Rows[i]["OrderId"].ToString();
                            CancelBooking.SlotTime = dt.Rows[i]["SlotTime"].ToString();
                            if (CancelBooking.ActualBoatNum == "")
                            {
                                CancelBooking.ActualBoatNum = "0";
                            }

                            li.Add(CancelBooking);
                        }
                        BoardingPassList ConfList = new BoardingPassList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }


                    else
                    {
                        BoardingPassRes ConfRes1 = new BoardingPassRes
                        {
                            Response = "No Booking Details Found",
                            StatusCode = 0
                        };
                        return Ok(ConfRes1);
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
        //[Route("Boardingpass/BookingId")]
        //public IHttpActionResult BoardingpassBookingid([FromBody] BoardingPass Board)
        //{
        //    try
        //    {
        //        if (Board.BoatHouseId != null && Board.BookingId != null)
        //        {
        //            string sQuery = string.Empty;

        //            //sQuery = "SELECT A.ActualBoatId,A.ExpectedTime, A.BookingId, A.BookingSerial, B.UserId, convert(nvarchar,CAST(B.BookingDate as time),100) as BoardingTime, "
        //            //    + " A.BoatHouseId, A.BoatHouseName, A.BookingId, A.BoatTypeId,C.BoatType, A.BoatSeaterId,B.CustomerName, "
        //            //    + " A.BoatDeposit, isnull(A.ActualBoatNum ,0) as ActualBoatNum, D.SeaterType, A.BoatReferenceNo,"
        //            //    + " case when B.PremiumStatus = 'P' then 'Permium' else 'Normal' end as PremiumStatus, "
        //            //    + " convert(varchar, B.BookingDate, 100) as BookingDate, A.BookingDuration, A.InitNetAmount,  A.BookingPin "
        //            //    + " FROM BookingDtl AS A  INNER JOIN BookingHdr AS B ON A.BoatHouseId = B.BoatHouseId "
        //            //    + " AND A.BookingId = B.BookingId  inner Join BoatTypes as C on A.BoatTypeId = C.BoatTypeId "
        //            //    + " inner Join BoatSeat as D on A.BoatSeaterId = D.BoatSeaterId and A.BoatHouseId = D.BoatHouseId "
        //            //    + " where A.BoatHouseId = '" + Board.BoatHouseId + "' and A.BookingId = '" + Board.BookingId + "' order by BookingPin ASc";

        //            sQuery = "SELECT * FROM "
        //                        + "  (SELECT A.ActualBoatId, A.ExpectedTime, A.BookingId, A.BookingSerial, B.UserId, convert(nvarchar, CAST(B.BookingDate as time), 100) as BoardingTime, "
        //                        + "  A.BoatHouseId, A.BoatHouseName, A.BoatTypeId, C.BoatType, A.BoatSeaterId, B.CustomerName, "
        //                        + "  A.BoatDeposit, isnull(A.ActualBoatNum, 0) as ActualBoatNum, D.SeaterType, A.BoatReferenceNo, "
        //                        + "  case when B.PremiumStatus = 'P' then 'Permium' else 'Normal' end as PremiumStatus,  "
        //                        + "  convert(varchar, B.BookingDate, 100) as BookingDate, A.BookingDuration, A.InitNetAmount,  A.BookingPin, "
        //                        + "  ISNULL((CONVERT(VARCHAR(5), BST.SlotStartTime, 108) + '-' + CONVERT(VARCHAR(5), BST.SlotEndTime, 108)), '-') "
        //                        + "  AS 'SlotTime'  FROM BookingDtl AS A  "
        //                        + "  INNER JOIN BookingHdr AS B ON A.BoatHouseId = B.BoatHouseId AND A.BookingId = B.BookingId  "
        //                        + "  inner Join BoatTypes as C on A.BoatTypeId = C.BoatTypeId  "
        //                        + "  inner Join BoatSeat as D on A.BoatSeaterId = D.BoatSeaterId and A.BoatHouseId = D.BoatHouseId  "
        //                        + "  INNER JOIN BoatSlotMaster AS BST ON A.TimeSlotId = BST.SlotId  "
        //                        + "  where A.BoatHouseId = '" + Board.BoatHouseId + "' and A.BookingId = '" + Board.BookingId + "' ) AS AA  "
        //                        + "  LEFT JOIN  "
        //                        + " (SELECT OrderId, TransactionNo, TrackingId, BankReferenceNo, BoatHouseId  "
        //                        + "  from ["+ ConfigurationManager.AppSettings["CommonDB"] +"].[dbo].[OnlineBookingAfterTransaction] WITH (NOLOCK) WHERE "
        //                        + "  TransactionNo= (SELECT TOP 1 TransactionNo FROM["+ ConfigurationManager.AppSettings["CommonDB"] +"].[dbo].[OnlineBookingTransactionHistory] "
        //                        + "  WITH(NOLOCK) WHERE BoatHouseId = BoatHouseId AND BookingId = '" + Board.BookingId + "'  ORDER BY TransactionNo DESC) "
        //                        + "  ) AS BB ON AA.BoatHouseId = BB.BoatHouseId "
        //                        + "   order by BookingPin ASc";


        //            List<BoardingPass> li = new List<BoardingPass>();
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand(sQuery, con);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    BoardingPass CancelBooking = new BoardingPass();
        //                    CancelBooking.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
        //                    CancelBooking.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        //                    CancelBooking.BookingId = dt.Rows[i]["BookingId"].ToString();
        //                    CancelBooking.PremiumStatus = dt.Rows[i]["PremiumStatus"].ToString();
        //                    CancelBooking.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        //                    CancelBooking.BoatType = dt.Rows[i]["BoatType"].ToString();
        //                    CancelBooking.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        //                    CancelBooking.SeaterType = dt.Rows[i]["SeaterType"].ToString();
        //                    CancelBooking.BookingDate = dt.Rows[i]["BookingDate"].ToString();
        //                    CancelBooking.BookingDuration = dt.Rows[i]["BookingDuration"].ToString();
        //                    CancelBooking.InitNetAmount = dt.Rows[i]["InitNetAmount"].ToString();
        //                    CancelBooking.BookingPin = dt.Rows[i]["BookingPin"].ToString();
        //                    CancelBooking.CustomerName = dt.Rows[i]["CustomerName"].ToString();
        //                    CancelBooking.BoatDeposit = dt.Rows[i]["BoatDeposit"].ToString();
        //                    CancelBooking.ActualBoatNum = dt.Rows[i]["ActualBoatNum"].ToString();
        //                    CancelBooking.UserId = dt.Rows[i]["UserId"].ToString();
        //                    CancelBooking.BoardingTime = dt.Rows[i]["BoardingTime"].ToString();
        //                    CancelBooking.BookingSerial = dt.Rows[i]["BookingSerial"].ToString();
        //                    CancelBooking.BoatReferenceNo = dt.Rows[i]["BoatReferenceNo"].ToString();
        //                    CancelBooking.ActualBoatId = dt.Rows[i]["ActualBoatId"].ToString();
        //                    CancelBooking.ExpectedTime = dt.Rows[i]["ExpectedTime"].ToString();
        //                    CancelBooking.OrderId = dt.Rows[i]["OrderId"].ToString();
        //                    CancelBooking.SlotTime = dt.Rows[i]["SlotTime"].ToString();
        //                    if (CancelBooking.ActualBoatNum == "")
        //                    {
        //                        CancelBooking.ActualBoatNum = "0";
        //                    }

        //                    li.Add(CancelBooking);
        //                }
        //                BoardingPassList ConfList = new BoardingPassList
        //                {
        //                    Response = li,
        //                    StatusCode = 1
        //                };
        //                return Ok(ConfList);
        //            }


        //            else
        //            {
        //                BoardingPassRes ConfRes1 = new BoardingPassRes
        //                {
        //                    Response = "No Booking Details Found",
        //                    StatusCode = 0
        //                };
        //                return Ok(ConfRes1);
        //            }

        //        }
        //        else
        //        {

        //            NewBoatBookedString Vehicle = new NewBoatBookedString
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
        //        return Ok(ConfRes);
        //    }

        //}


        /****************************Re Entry Trip*****************************************/

        /****************************Change Boat Details*****************************************/

        /***********************************Trip Feedback**************************************/

        //Insert

        [HttpPost]
        [AllowAnonymous]
        [Route("TripFeedBack")]
        public IHttpActionResult InsertTripFeedBack([FromBody] TripFeedback InsShiftMstr)
        {
            try
            {
                if (InsShiftMstr.QueryType != null && InsShiftMstr.BoatHouseId != null
                    && InsShiftMstr.BoatHouseName != null && InsShiftMstr.BoatType != null
                    && InsShiftMstr.Ratings != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("InsertTripFeedBack", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsShiftMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", InsShiftMstr.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatType", InsShiftMstr.BoatType.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsShiftMstr.BoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@SeaterId", InsShiftMstr.SeaterId.Trim());
                    cmd.Parameters.AddWithValue("@Ratings", InsShiftMstr.Ratings.Trim());
                    cmd.Parameters.AddWithValue("@Comments", InsShiftMstr.Comments.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsShiftMstr.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsShiftMstr.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", InsShiftMstr.MobileNo.Trim());

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
                        ShiftMasterRes ShMstr = new ShiftMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ShMstr);
                    }
                    else
                    {
                        ShiftMasterRes ShMstr = new ShiftMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ShMstr);
                    }
                }
                else
                {
                    ShiftMasterRes ShMstr = new ShiftMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ShMstr);
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


        /****************************PRICE COMPARISON*****************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("PriceComparison")]
        public IHttpActionResult PriceComparison([FromBody] PriceComparison price)
        {
            try
            {
                if (price.QueryType != null && price.Input1 != null && price.Input2 != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ViewPriceComparison", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", price.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@Input1", price.Input1.ToString());
                    cmd.Parameters.AddWithValue("@Input2", price.Input2.ToString());
                    cmd.Parameters.AddWithValue("@Input3", price.Input3.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", price.CorpId.ToString());

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
                return Ok(ex.ToString());
            }
        }




        /**********************************Logesh Dashboard*******************************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetBoatSeaterBasedBoatMaster")]
        public IHttpActionResult GetBoatSeaterBasedoBoatMaster([FromBody] BoatSeatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatTypeId != null)
                {
                    List<BoatSeatMaster> li = new List<BoatSeatMaster>();
                    string sQuery = string.Empty;

                    sQuery = " SELECT distinct A.SeaterType, A.BoatSeaterId from(  SELECT SeaterType, STUFF((SELECT ', ' +  "
                     + " CAST(BoatSeaterId AS VARCHAR(10))[text()] FROM BoatSeat  WHERE SeaterType = t.SeaterType AND ActiveStatus = 'A' "
                     + " AND t.ActiveStatus = 'A'  FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ' ') 'BoatSeaterId' "
                     + " FROM BoatSeat t WHERE ActiveStatus = 'A'  GROUP BY SeaterType, ActiveStatus ) AS A "
                     + " INNER JOIN BoatMaster AS B ON B.BoatTypeId IN(SELECT * FROM dbo.CSVToLIst( @BoatTypeId )) "
                     + " AND B.BoatSeaterId IN(SELECT * FROM dbo.CSVToLIst(A.BoatSeaterId)) ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatTypeId"].Value = bHMstr.BoatTypeId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatSeatMaster BoatSeatMaster = new BoatSeatMaster();

                            BoatSeatMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatSeatMaster.SeaterType = dt.Rows[i]["SeaterType"].ToString();

                            li.Add(BoatSeatMaster);
                        }

                        BoatSeatMasterList BoatSeat = new BoatSeatMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatSeat);
                    }

                    else
                    {
                        BoatSeatMasterString BoatSeat = new BoatSeatMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatSeat);
                    }
                }
                else
                {
                    BoatHouseMasterString Vehicle = new BoatHouseMasterString
                    {
                        Response = "Must Pass Boat type Id",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BoatSeatMasterString ConfRes = new BoatSeatMasterString
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetBoatTypeBasedBoatMaster")]
        public IHttpActionResult GetBoatTypeBasedBoatMaster()
        {
            try
            {
                List<BoatTypeMaster> li = new List<BoatTypeMaster>();
                string sQuery = string.Empty;

                sQuery = " SELECT distinct A.BoatType, A.BoatTypeId from( "
                        + " SELECT BoatType, STUFF((SELECT ', ' + CAST(BoatTypeId AS VARCHAR(10))[text()] FROM BoatTypes "
                        + " WHERE BoatType = t.BoatType AND ActiveStatus = 'A' AND t.ActiveStatus = 'A' "
                        + " FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ' ') 'BoatTypeId' "
                        + " FROM BoatTypes t WHERE ActiveStatus = 'A' "
                        + " GROUP BY BoatType, ActiveStatus ) AS A "
                        + " INNER JOIN BoatMaster AS B ON B.BoatTypeId IN(SELECT * FROM dbo.CSVToLIst(A.BoatTypeId)) ";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatTypeMaster BoatTypes = new BoatTypeMaster();

                        BoatTypes.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        BoatTypes.BoatType = dt.Rows[i]["BoatType"].ToString();

                        li.Add(BoatTypes);
                    }

                    BoatTypeMasterList BoatSeat = new BoatTypeMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatSeat);
                }

                else
                {
                    BoatTypeMasterString BoatSeat = new BoatTypeMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatSeat);
                }


            }
            catch (Exception ex)
            {
                BoatSeatMasterString ConfRes = new BoatSeatMasterString
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="boat"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetBoatTypeBasedBoatHouse")]
        public IHttpActionResult GetBoatTypeBasedBoatHouse([FromBody] BoatTypeMaster boat)
        {
            try
            {
                if (boat.BoatHouseId != null)
                {
                    List<BoatTypeMaster> li = new List<BoatTypeMaster>();
                    string sQuery = string.Empty;

                    sQuery = " SELECT distinct A.BoatType, A.BoatTypeId from( "
                        + " SELECT BoatType, STUFF((SELECT ', ' + CAST(BoatTypeId AS VARCHAR(10))[text()] FROM BoatTypes "
                        + " WHERE BoatType = t.BoatType AND ActiveStatus = 'A' AND t.ActiveStatus = 'A' AND BoatHouseId=t.BoatHouseId "
                        + " FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ' ') 'BoatTypeId' "
                        + " FROM BoatTypes t WHERE ActiveStatus = 'A' "
                        + " GROUP BY BoatType, ActiveStatus,BoatHouseId ) AS A "
                        + " INNER JOIN BoatMaster AS B ON A.BoatTypeId=B.BoatTypeId AND BoatHouseId=B.BoatHouseId AND B.BoatHouseId = @BoatHouseId ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boat.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatTypeMaster BoatTypes = new BoatTypeMaster();

                            BoatTypes.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatTypes.BoatType = dt.Rows[i]["BoatType"].ToString();

                            li.Add(BoatTypes);
                        }

                        BoatTypeMasterList BoatSeat = new BoatTypeMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatSeat);
                    }

                    else
                    {
                        BoatTypeMasterString BoatSeat = new BoatTypeMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatSeat);
                    }
                }
                else
                {
                    BoatTypeMasterString BoatSeat = new BoatTypeMasterString
                    {
                        Response = "Must Pass Boat House Id.",
                        StatusCode = 0
                    };
                    return Ok(BoatSeat);
                }


            }
            catch (Exception ex)
            {
                BoatSeatMasterString ConfRes = new BoatSeatMasterString
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetBoatHouseBasedBoatMaster")]
        public IHttpActionResult GetBoatHouseBasedBoatMaster()
        {
            try
            {
                List<BoatHouseMaster> li = new List<BoatHouseMaster>();
                string sQuery = string.Empty;

                sQuery = " SELECT Distinct A.BoatHouseId,B.BoatHouseName FROM BoatMaster AS A "
                       + " INNER JOIN BHMaster AS B ON A.BoatHouseId = B.BoatHouseId ";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatHouseMaster BoatHouse = new BoatHouseMaster();

                        BoatHouse.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        BoatHouse.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();

                        li.Add(BoatHouse);
                    }

                    BoatHouseMasterList BoatSeat = new BoatHouseMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatSeat);
                }

                else
                {
                    BoatHouseMasterString BoatSeat = new BoatHouseMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatSeat);
                }


            }
            catch (Exception ex)
            {
                BoatHouseMasterString ConfRes = new BoatHouseMasterString
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
        [Route("GetDashboardBoatCountDrillDown")]
        public IHttpActionResult GetDashboardBoatCountDrillDown([FromBody] BoatSeatMaster Dashboard)
        {
            try
            {
                List<CancelReschedMstr> li = new List<CancelReschedMstr>();
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                if (Convert.ToString(Dashboard.BoatHouseId) != null && Convert.ToString(Dashboard.BoatTypeId) != null
                    && Dashboard.BoatSeaterId != null && Dashboard.BoatStatusId != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("GetDashBoardBoatCount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "DrillDownDashboardBoatCount");
                    cmd.Parameters.AddWithValue("@BoatHouseId", Dashboard.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", Dashboard.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", Dashboard.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatStatusId", Dashboard.BoatStatusId.ToString());

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
                        da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "TableShow");
                        if (ds != null)
                        {

                            return Ok(ds);
                        }

                        else
                        {

                            return NotFound();
                        }
                    }

                    else
                    {
                        return NotFound();
                    }
                }
                return Ok();
            }

            catch (Exception ex)
            {
                CancelReschedMstrRes ConfRes = new CancelReschedMstrRes
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

        /***********************************Other Master API**************************************/

        /***********************************City Mapping**************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("CityMapping")]
        public IHttpActionResult CityMaster([FromBody] CityMapping InsCityMap)
        {
            try
            {
                if (InsCityMap.QueryType != null && Convert.ToString(InsCityMap.StateId) != null && Convert.ToString(InsCityMap.ZoneId) != null
                    && Convert.ToString(InsCityMap.DistrictId) != null && Convert.ToString(InsCityMap.CityId) != null && InsCityMap.CityDescription != null
                    && InsCityMap.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("CityMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsCityMap.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CityId", InsCityMap.CityId.ToString());
                    cmd.Parameters.AddWithValue("@DistrictId", InsCityMap.DistrictId.ToString());
                    cmd.Parameters.AddWithValue("@ZoneId", InsCityMap.ZoneId.ToString());
                    cmd.Parameters.AddWithValue("@StateId", InsCityMap.StateId.ToString());
                    cmd.Parameters.AddWithValue("@CityDescription", InsCityMap.CityDescription.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsCityMap.CreatedBy.Trim());

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


        //Display City Mapping List
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("CityMap/ListAll")]
        public IHttpActionResult ShowCityMapDet()
        {
            try
            {
                List<CityMapping> li = new List<CityMapping>();
                string sQuery = string.Empty;

                sQuery = " SELECT A.CityId, B.ConfigName AS 'CityName', A.DistrictId, C.ConfigName AS 'DistrictName', "
                        + " A.ZoneId, D.ConfigName AS 'ZoneName', A.StateId, E.ConfigName AS 'StateName', A.CityDescription, A.ActiveStatus, A.CreatedBy FROM CityMapping AS A "
                        + " INNER JOIN ConfigurationMaster AS B ON A.CityId = B.ConfigId AND  B.ActiveStatus='A' AND B.TypeId = '4' "
                        + " INNER JOIN ConfigurationMaster AS C ON A.DistrictId = C.ConfigId AND  C.ActiveStatus='A' AND C.TypeId = '3' "
                        + " INNER JOIN ConfigurationMaster AS D ON A.ZoneId = D.ConfigId AND  D.ActiveStatus='A' AND D.TypeId = '2' "
                        + " INNER JOIN ConfigurationMaster AS E ON A.StateId = E.ConfigId AND E.ActiveStatus='A' AND E.TypeId = '1' "
                        + " WHERE  A.ActiveStatus IN ('A','D') ";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CityMapping ShowConfMstr = new CityMapping();
                        ShowConfMstr.CityId = Convert.ToInt32(dt.Rows[i]["CityId"].ToString());
                        ShowConfMstr.DistrictId = Convert.ToInt32(dt.Rows[i]["DistrictId"].ToString());
                        ShowConfMstr.ZoneId = Convert.ToInt32(dt.Rows[i]["ZoneId"].ToString());
                        ShowConfMstr.StateId = Convert.ToInt32(dt.Rows[i]["StateId"].ToString());
                        ShowConfMstr.CityName = dt.Rows[i]["CityName"].ToString();
                        ShowConfMstr.DistrictName = dt.Rows[i]["DistrictName"].ToString();
                        ShowConfMstr.ZoneName = dt.Rows[i]["ZoneName"].ToString();
                        ShowConfMstr.StateName = dt.Rows[i]["StateName"].ToString();
                        ShowConfMstr.CityDescription = dt.Rows[i]["CityDescription"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();

                        li.Add(ShowConfMstr);
                    }

                    CityMappingList ConfList = new CityMappingList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    CityMappingRes ConfRes = new CityMappingRes
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


        /***********************************Location Master**************************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("LocationMaster/ListAll")]
        public IHttpActionResult showLocationMaster()
        {
            try
            {
                List<LocationMaster> li = new List<LocationMaster>();
                string sQuery = string.Empty;

                sQuery = " Select A.LocationId, A.LocationName, A.LocationDescription, A.LocationImageLink, A.CityId,"
                        + " B.ConfigName AS CityName, A.HomePageDisplay, A.CreatedBy,A.ActiveStatus  from LocationMaster AS A "
                        + " INNER JOIN  ConfigurationMaster AS B ON A.CityId = B.ConfigID AND  B.ActiveStatus ='A' AND B.TypeID='4' "
                        + " WHERE A.ActiveStatus IN ('A','D') ";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LocationMaster showlocnmstr = new LocationMaster();
                        showlocnmstr.LocationId = Convert.ToInt32(dt.Rows[i]["LocationId"].ToString());
                        showlocnmstr.CityId = Convert.ToInt32(dt.Rows[i]["CityId"].ToString());

                        showlocnmstr.LocationName = dt.Rows[i]["LocationName"].ToString();
                        showlocnmstr.LocationDescription = dt.Rows[i]["LocationDescription"].ToString();
                        showlocnmstr.LocationImageLink = dt.Rows[i]["LocationImageLink"].ToString();
                        showlocnmstr.CityName = dt.Rows[i]["CityName"].ToString();
                        showlocnmstr.HomePageDisplay = dt.Rows[i]["HomePageDisplay"].ToString();
                        showlocnmstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        showlocnmstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        li.Add(showlocnmstr);
                    }

                    LocationMasterList ConfList = new LocationMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    LocationMasterRes ConfRes = new LocationMasterRes
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



        [HttpPost]
        [AllowAnonymous]
        [Route("LocationMaster")]
        public IHttpActionResult MstrLocationMaster([FromBody] LocationMaster InsLocMstr)
        {
            try
            {
                if (InsLocMstr.QueryType != null && Convert.ToString(InsLocMstr.LocationId) != null && InsLocMstr.LocationName != null
                    && InsLocMstr.LocationDescription != null && Convert.ToString(InsLocMstr.CityId) != null && InsLocMstr.LocationImageLink != null
                    && InsLocMstr.HomePageDisplay != null && InsLocMstr.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrLocationMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsLocMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@LocationId", InsLocMstr.LocationId.ToString());
                    cmd.Parameters.AddWithValue("@LocationName", InsLocMstr.LocationName.ToString());
                    cmd.Parameters.AddWithValue("@CityId", InsLocMstr.CityId.ToString());
                    cmd.Parameters.AddWithValue("@LocationDescription", InsLocMstr.LocationDescription.ToString());
                    cmd.Parameters.AddWithValue("@LocationImageLink", InsLocMstr.LocationImageLink.ToString());
                    cmd.Parameters.AddWithValue("@HomePageDisplay", InsLocMstr.HomePageDisplay.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsLocMstr.CreatedBy.Trim());

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
                        LocationMasterRes ConMstr = new LocationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        LocationMasterRes ConMstr = new LocationMasterRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    LocationMasterRes Vehicle = new LocationMasterRes
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



        /**********************************Location Gallery***************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("LocatonGallery/ListAll")]
        public IHttpActionResult showlocatonGallery()
        {
            try
            {
                List<LocationGallery> li = new List<LocationGallery>();
                string sQuery = string.Empty;

                sQuery = " SELECT A.LocationId, B.LocationName, A.GalleryId, A.LocationImageLink AS 'LocationGalleryImage', "
                        + " A.ActiveStatus, A.CreatedBy FROM LocationGallery AS A "
                        + " INNER JOIN LocationMaster AS B ON A.LocationId = B.LocationId AND  B.ActiveStatus='A'  "
                        + " WHERE A.ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LocationGallery showlocnmstrgal = new LocationGallery();
                        showlocnmstrgal.LocationId = Convert.ToInt32(dt.Rows[i]["LocationId"].ToString());
                        showlocnmstrgal.GalleryId = Convert.ToInt32(dt.Rows[i]["GalleryId"].ToString());
                        showlocnmstrgal.LocationName = dt.Rows[i]["LocationName"].ToString();
                        showlocnmstrgal.LocationImageLink = dt.Rows[i]["LocationGalleryImage"].ToString();
                        showlocnmstrgal.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        showlocnmstrgal.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(showlocnmstrgal);
                    }

                    LocationGalleryList ConfList = new LocationGalleryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    LocationGalleryRes ConfRes = new LocationGalleryRes
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


        [HttpPost]
        [AllowAnonymous]
        [Route("LocationGallery")]
        public IHttpActionResult MstrLocationGallery([FromBody] LocationGallery InsLocGallery)
        {
            try
            {
                if (InsLocGallery.QueryType != null && Convert.ToString(InsLocGallery.LocationId) != null
                    && InsLocGallery.LocationImageLink != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrLocationGallery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsLocGallery.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@LocationId", Convert.ToInt32(InsLocGallery.LocationId.ToString()));
                    cmd.Parameters.AddWithValue("@GalleryId", Convert.ToInt32(InsLocGallery.GalleryId.ToString()));
                    cmd.Parameters.AddWithValue("@LocationImageLink", InsLocGallery.LocationImageLink.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsLocGallery.CreatedBy.Trim());

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
                        LocationGalleryRes ConMstr = new LocationGalleryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        LocationGalleryRes ConMstr = new LocationGalleryRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    LocationGalleryRes Vehicle = new LocationGalleryRes
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

        /**********************************Location Attraction Type Mapping***************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetLocAttrtypeMap/ListAll")]
        public IHttpActionResult GetLocAttrtypeMap()
        {
            try
            {
                List<LocationAttractionTypeMapping> li = new List<LocationAttractionTypeMapping>();
                string sQuery = string.Empty;

                sQuery = " SELECT A.LocationId, B.LocationName, A.AttractionId, C.ConfigName AS 'AttractionTypeName', A.ActiveStatus, A.CreatedBy FROM LocationAttTypeMap AS A "
                    + " INNER JOIN LocationMaster AS B ON A.LocationId = B.LocationId AND  B.ActiveStatus='A' "
                    + " INNER JOIN ConfigurationMaster AS C ON A.AttractionId = C.ConfigId AND  C.ActiveStatus='A' AND C.TypeId = '5' "
                    + " WHERE A.ActiveStatus IN ('A','D') ";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LocationAttractionTypeMapping ShowConfMstr = new LocationAttractionTypeMapping();
                        ShowConfMstr.LocationId = Convert.ToInt32(dt.Rows[i]["LocationId"].ToString());
                        ShowConfMstr.AttractionId = Convert.ToInt32(dt.Rows[i]["AttractionId"].ToString());
                        ShowConfMstr.LocationName = dt.Rows[i]["LocationName"].ToString();
                        ShowConfMstr.AttractionTypeName = dt.Rows[i]["AttractionTypeName"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        li.Add(ShowConfMstr);

                    }

                    LocationAttractionTypeMappingList ConfList = new LocationAttractionTypeMappingList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    LocationAttractionTypeMappingRes ConfRes = new LocationAttractionTypeMappingRes
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


        [HttpPost]
        [AllowAnonymous]
        [Route("LocationAttrTypeMap")]
        public IHttpActionResult MstrLocationAttTypeMap([FromBody] LocationAttractionTypeMapping locationAttr)
        {
            try
            {
                if (locationAttr.QueryType != null && Convert.ToString(locationAttr.LocationId) != null
                    && Convert.ToString(locationAttr.AttractionId) != null && locationAttr.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrLocationAttTypeMap", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", locationAttr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@LocationId", Convert.ToInt32(locationAttr.LocationId.ToString()));
                    cmd.Parameters.AddWithValue("@AttractionId", Convert.ToInt32(locationAttr.AttractionId.ToString()));
                    cmd.Parameters.AddWithValue("@CreatedBy", Convert.ToInt32(locationAttr.CreatedBy.ToString()));
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
                        LocationAttractionTypeMappingRes ConMstr = new LocationAttractionTypeMappingRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        LocationAttractionTypeMappingRes ConMstr = new LocationAttractionTypeMappingRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    LocationAttractionTypeMappingRes Vehicle = new LocationAttractionTypeMappingRes
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



        /**********************************Location City Mapping***************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ShowLocCityMapList/ListAll")]
        public IHttpActionResult ShowLocCityMapList()
        {
            try
            {
                List<LocationCityMapping> li = new List<LocationCityMapping>();
                string sQuery = string.Empty;

                sQuery = " SELECT A.LocationId, B.LocationName, A.CityId, C.ConfigName AS 'CityName', A.Distance, A.ActiveStatus, A.CreatedBy FROM LocationCityMap AS A "
                       + " INNER JOIN LocationMaster AS B ON A.LocationId = B.LocationId AND  B.ActiveStatus='A' "
                       + " INNER JOIN ConfigurationMaster AS C ON A.CityId = C.ConfigId AND  C.ActiveStatus='A' AND C.TypeId = '4' "
                       + " WHERE A.ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LocationCityMapping ShowConfMstr = new LocationCityMapping();
                        ShowConfMstr.LocationId = Convert.ToInt32(dt.Rows[i]["LocationId"].ToString());
                        ShowConfMstr.CityId = Convert.ToInt32(dt.Rows[i]["CityId"].ToString());
                        ShowConfMstr.LocationName = dt.Rows[i]["LocationName"].ToString();
                        ShowConfMstr.CityName = dt.Rows[i]["CityName"].ToString();
                        ShowConfMstr.Distance = Convert.ToInt32(dt.Rows[i]["Distance"].ToString());
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        li.Add(ShowConfMstr);

                    }

                    LocationCityMappingList ConfList = new LocationCityMappingList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    LocationCityMappingRes ConfRes = new LocationCityMappingRes
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



        [HttpPost]
        [AllowAnonymous]
        [Route("LocationCityMapping")]
        public IHttpActionResult MstrLocationCityMapping([FromBody] LocationCityMapping InsLocCityMap)
        {
            try
            {
                if (InsLocCityMap.QueryType != null && Convert.ToString(InsLocCityMap.LocationId) != null
                    && Convert.ToString(InsLocCityMap.CityId) != null && Convert.ToString(InsLocCityMap.Distance) != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrLocationCityMap", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsLocCityMap.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@LocationId", Convert.ToInt32(InsLocCityMap.LocationId.ToString()));
                    cmd.Parameters.AddWithValue("@CityId ", Convert.ToInt32(InsLocCityMap.CityId.ToString()));
                    cmd.Parameters.AddWithValue("@Distance ", InsLocCityMap.Distance.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsLocCityMap.CreatedBy.Trim());

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
                        LocationCityMappingRes LocCityMap3 = new LocationCityMappingRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(LocCityMap3);
                    }
                    else
                    {
                        LocationCityMappingRes LocCityMap2 = new LocationCityMappingRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(LocCityMap2);
                    }
                }
                else
                {
                    LocationCityMappingRes LocCityMap1 = new LocationCityMappingRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(LocCityMap1);
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



        /*************************************Important Contacts************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ImpContactList/ListAll")]
        public IHttpActionResult ShowImpContactList()
        {
            try
            {
                List<ImportantContact> li = new List<ImportantContact>();
                string sQuery = string.Empty;

                sQuery = " SELECT A.CityId, C.ConfigName AS 'CityName', A.ContactId, A.ContactTypeId, B.ConfigName AS 'ContactTypeName', "
                        + " A.Description, A.ContactInfo, A.ActiveStatus, A.ActiveStatus, A.CreatedBy FROM ImpContacts AS A "
                        + " INNER JOIN ConfigurationMaster AS B ON A.ContactTypeId = B.ConfigId AND  B.ActiveStatus = 'A' AND  B.TypeId = '6' "
                        + " INNER JOIN ConfigurationMaster AS C ON A.CityId = C.ConfigId AND  C.ActiveStatus ='A' AND  C.TypeId = '4' "
                        + " WHERE A.ActiveStatus IN ('A','D') ";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ImportantContact ShowConfMstr = new ImportantContact();
                        ShowConfMstr.ContactId = Convert.ToInt32(dt.Rows[i]["ContactId"].ToString());
                        ShowConfMstr.CityId = Convert.ToInt32(dt.Rows[i]["CityId"].ToString());
                        ShowConfMstr.ContactTypeId = Convert.ToInt32(dt.Rows[i]["ContactTypeId"].ToString());
                        ShowConfMstr.ContactTypeName = dt.Rows[i]["ContactTypeName"].ToString();
                        ShowConfMstr.CityName = dt.Rows[i]["CityName"].ToString();
                        ShowConfMstr.Description = dt.Rows[i]["Description"].ToString();
                        ShowConfMstr.ContactInfo = dt.Rows[i]["ContactInfo"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        li.Add(ShowConfMstr);
                    }

                    ImportantContactList ConfList = new ImportantContactList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    ImportantContactRes ConfRes = new ImportantContactRes
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



        [HttpPost]
        [AllowAnonymous]
        [Route("ImportantContact")]
        public IHttpActionResult MstrImportantContact([FromBody] ImportantContact InsImpContact)
        {
            try
            {
                if (InsImpContact.QueryType != null && Convert.ToString(InsImpContact.ContactTypeId) != null
                    && Convert.ToString(InsImpContact.CityId) != null && InsImpContact.Description != null
                    && InsImpContact.ContactInfo != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ImptContacts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsImpContact.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ContactId", Convert.ToInt32(InsImpContact.ContactId.ToString()));
                    cmd.Parameters.AddWithValue("@CityId", Convert.ToInt32(InsImpContact.CityId.ToString()));
                    cmd.Parameters.AddWithValue("@ContactTypeId", Convert.ToInt32(InsImpContact.ContactTypeId.ToString()));
                    cmd.Parameters.AddWithValue("@Description", InsImpContact.Description.ToString());
                    cmd.Parameters.AddWithValue("@ContactInfo", InsImpContact.ContactInfo.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsImpContact.CreatedBy.Trim());

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
                        ImportantContactRes ImpCont3 = new ImportantContactRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ImpCont3);
                    }
                    else
                    {
                        ImportantContactRes ImpCont2 = new ImportantContactRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ImpCont2);
                    }
                }
                else
                {
                    ImportantContactRes ImpCont1 = new ImportantContactRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ImpCont1);
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


        /*************************************TTDC Contacts************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("TTDCContacts/ListAll")]
        public IHttpActionResult ShowTTDCContacts()
        {
            try
            {
                List<TTDCContacts> li = new List<TTDCContacts>();
                string sQuery = string.Empty;

                sQuery = " Select A.ContactId,A.ContactTypeId,B.ConfigName as ContactTypeName,A.ContactName,A.ActiveStatus,A.Designation,A.ContactInfo , A.CreatedBy  from TTDCContact AS A "
                        + " Inner JOIN ConfigurationMaster AS B On A.ContactTypeId = B.ConfigId AND B.ActiveStatus ='A' AND  B.TypeId = '6' "
                        + " WHERE A.ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TTDCContacts ShowTTDCContacts = new TTDCContacts();
                        ShowTTDCContacts.ContactId = dt.Rows[i]["ContactId"].ToString();
                        ShowTTDCContacts.ContactTypeId = dt.Rows[i]["ContactTypeId"].ToString();
                        ShowTTDCContacts.ContactTypeName = dt.Rows[i]["ContactTypeName"].ToString();
                        ShowTTDCContacts.ContactName = dt.Rows[i]["ContactName"].ToString();
                        ShowTTDCContacts.Designation = dt.Rows[i]["Designation"].ToString();
                        ShowTTDCContacts.ContactInfo = dt.Rows[i]["ContactInfo"].ToString();
                        ShowTTDCContacts.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        ShowTTDCContacts.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowTTDCContacts);
                    }

                    TTDCContactsList ConfList = new TTDCContactsList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    TTDCContactsRes ConfRes = new TTDCContactsRes
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




        [HttpPost]
        [AllowAnonymous]
        [Route("TTDCContacts")]
        public IHttpActionResult MstrTTDCContacts([FromBody] TTDCContacts TTDC)
        {
            try
            {
                if (TTDC.QueryType != null && Convert.ToString(TTDC.ContactId) != null && TTDC.ContactName != null && TTDC.ContactInfo != null
                  && TTDC.Designation != null && Convert.ToString(TTDC.ContactTypeId) != null && TTDC.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrTTDCContacts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", TTDC.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ContactId", Convert.ToInt32(TTDC.ContactId.ToString()));
                    cmd.Parameters.AddWithValue("@ContactTypeId", Convert.ToInt32(TTDC.ContactTypeId.ToString()));
                    cmd.Parameters.AddWithValue("@ContactName", TTDC.ContactName.ToString());
                    cmd.Parameters.AddWithValue("@ContactInfo", TTDC.ContactInfo.ToString());
                    cmd.Parameters.AddWithValue("@Designation", TTDC.Designation.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", TTDC.CreatedBy.ToString());
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
                        TTDCContactsRes ConMstr = new TTDCContactsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        TTDCContactsRes ConMstr = new TTDCContactsRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    TTDCContactsRes Vehicle = new TTDCContactsRes
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




        /*************************************Food Master************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("FoodMaster/ListAll")]
        public IHttpActionResult showFoodmaster()
        {
            try
            {
                List<FoodMaster> li = new List<FoodMaster>();
                string sQuery = string.Empty;

                sQuery = "SELECT A.FoodId,A.FoodName,A.FoodDescription,A.FoodImageLink,"
                        + " Case When VegNonVeg = 'V' then 'Veg' else 'NonVeg' End As FoodType, "
                        + " A.FoodCity, B.ConfigName as CityName ,A.CreatedBy,A.ActiveStatus FROM FoodMaster AS A "
                        + "  Inner Join ConfigurationMaster As B ON A.FoodCity = B.ConfigID AND  TypeID = '4' AND B.ActiveStatus = 'A' "
                        + " where A.ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FoodMaster ShowFoodMaster = new FoodMaster();
                        ShowFoodMaster.FoodId = Convert.ToInt32(dt.Rows[i]["FoodId"].ToString());
                        ShowFoodMaster.FoodName = dt.Rows[i]["FoodName"].ToString();
                        ShowFoodMaster.FoodDescription = dt.Rows[i]["FoodDescription"].ToString();
                        ShowFoodMaster.FoodImageLink = dt.Rows[i]["FoodImageLink"].ToString();
                        ShowFoodMaster.FoodType = dt.Rows[i]["FoodType"].ToString();
                        ShowFoodMaster.FoodCity = dt.Rows[i]["FoodCity"].ToString();
                        ShowFoodMaster.CityName = dt.Rows[i]["CityName"].ToString();
                        ShowFoodMaster.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        ShowFoodMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowFoodMaster);
                    }

                    FoodMasterList ConfList = new FoodMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    FoodMasterString ConfRes = new FoodMasterString
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



        //CRUD
        [HttpPost]
        [AllowAnonymous]
        [Route("FoodMaster")]
        public IHttpActionResult FoodMstr([FromBody] FoodMaster Foodmstr)
        {
            try
            {
                if (Foodmstr.QueryType != null && Foodmstr.FoodName != null && Foodmstr.FoodDescription != null && Convert.ToString(Foodmstr.FoodId) != null &&
                    Foodmstr.FoodImageLink != null && Foodmstr.FoodCity != null && Foodmstr.CreatedBy != null && Foodmstr.VegNonVeg != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("mstrFoodMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Foodmstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@FoodId", Convert.ToInt32(Foodmstr.FoodId.ToString()));
                    cmd.Parameters.AddWithValue("@FoodName", Foodmstr.FoodName.ToString());
                    cmd.Parameters.AddWithValue("@FoodDescription", Foodmstr.FoodDescription.Trim());
                    cmd.Parameters.AddWithValue("@FoodImageLink", Foodmstr.FoodImageLink.Trim());
                    cmd.Parameters.AddWithValue("@FoodCity", Convert.ToInt32(Foodmstr.FoodCity.Trim()));
                    cmd.Parameters.AddWithValue("@VegNonVeg", Foodmstr.VegNonVeg.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", Foodmstr.CreatedBy.Trim());

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
                        FoodMasterString food = new FoodMasterString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(food);
                    }
                    else
                    {
                        FoodMasterString food = new FoodMasterString
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(food);
                    }
                }
                else
                {
                    FoodMasterString food = new FoodMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(food);
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


        /*************************************Cultural Events************************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("CulturalEvents/ListAll")]
        public IHttpActionResult CulturalEvents()
        {
            try
            {
                List<CulturalEvents> li = new List<CulturalEvents>();
                string sQuery = string.Empty;

                sQuery = " Select A.EventId,A.EventName,A.EventDescription,A.ActiveStatus,A.EventImageLink,A.EventType,C.ConfigName AS 'EventTypeName', "
                        + "  A.EventCity, B.ConfigName AS 'CityName' , A.CreatedBy from CulturalEvents AS A Inner Join ConfigurationMaster AS B ON A.EventCity = b.ConfigID "
                        + "  And  B.ActiveStatus='A' AND B.TypeID = '4'  Inner Join ConfigurationMaster AS C On A.EventType = C.ConfigID "
                        + "  And C.ActiveStatus='A' AND C.TypeID = '7' Where A.ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CulturalEvents ShowFoodMaster = new CulturalEvents();
                        ShowFoodMaster.EventId = dt.Rows[i]["EventId"].ToString();
                        ShowFoodMaster.EventName = dt.Rows[i]["EventName"].ToString();
                        ShowFoodMaster.EventDescription = dt.Rows[i]["EventDescription"].ToString();
                        ShowFoodMaster.EventImageLink = dt.Rows[i]["EventImageLink"].ToString();
                        ShowFoodMaster.EventType = dt.Rows[i]["EventType"].ToString();
                        ShowFoodMaster.EventTypeName = dt.Rows[i]["EventTypeName"].ToString();
                        ShowFoodMaster.EventCity = dt.Rows[i]["EventCity"].ToString();
                        ShowFoodMaster.CityName = dt.Rows[i]["CityName"].ToString();
                        ShowFoodMaster.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        ShowFoodMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                        li.Add(ShowFoodMaster);
                    }

                    CulturalEventsList ConfList = new CulturalEventsList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    CulturalEventsRes ConfRes = new CulturalEventsRes
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


        [HttpPost]
        [AllowAnonymous]
        [Route("CulturalEvents")]
        public IHttpActionResult PostCulturalEvents([FromBody] CulturalEvents InsCulture)
        {
            try
            {
                if (InsCulture.QueryType != null && Convert.ToString(InsCulture.EventId) != null && InsCulture.EventName != null
                    && InsCulture.EventDescription != null && InsCulture.EventImageLink != null &&
                    Convert.ToString(InsCulture.EventType) != null && Convert.ToString(InsCulture.EventCity) != null && InsCulture.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrCulturalEvents", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsCulture.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@EventId", Convert.ToInt32(InsCulture.EventId.ToString()));
                    cmd.Parameters.AddWithValue("@EventName", InsCulture.EventName.ToString());
                    cmd.Parameters.AddWithValue("@EventDescription", InsCulture.EventDescription.Trim());
                    cmd.Parameters.AddWithValue("@EventImageLink", InsCulture.EventImageLink.Trim());
                    cmd.Parameters.AddWithValue("@EventType", Convert.ToInt32(InsCulture.EventType.Trim()));
                    cmd.Parameters.AddWithValue("@EventCity", InsCulture.EventCity.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsCulture.CreatedBy.Trim());

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
                        CulturalEventsRes InsCE = new CulturalEventsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        CulturalEventsRes InsCE = new CulturalEventsRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    CulturalEventsRes InsCE = new CulturalEventsRes
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





        /*************************************Event Gallery************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetEventGallery/ListAll")]
        public IHttpActionResult GetEventGallery()
        {
            try
            {
                List<EventGallery> li = new List<EventGallery>();
                string sQuery = string.Empty;

                sQuery = " Select A.GalleryId,A.EventId,A.EventImageLink,B.EventName,A.ActiveStatus  from EventsGallery As A "
                        + " Inner Join CulturalEvents AS B on A.EventId = B.EventId And B.ActiveStatus='A'"
                        + " Where A.ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        EventGallery ShowEventMstr = new EventGallery();
                        ShowEventMstr.EventId = Convert.ToInt32(dt.Rows[i]["EventId"].ToString());
                        ShowEventMstr.GalleryId = Convert.ToInt32(dt.Rows[i]["GalleryId"].ToString());
                        ShowEventMstr.EventImageLink = dt.Rows[i]["EventImageLink"].ToString();
                        ShowEventMstr.EventName = dt.Rows[i]["EventName"].ToString();
                        ShowEventMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowEventMstr);

                    }

                    EventGalleryList ConfList = new EventGalleryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    EventGalleryRes ConfRes = new EventGalleryRes
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




        [HttpPost]
        [AllowAnonymous]
        [Route("EventGallery")]
        public IHttpActionResult EventGallery([FromBody] EventGallery EventsGal)
        {
            try
            {
                if (EventsGal.QueryType != null && Convert.ToString(EventsGal.EventId) != null && EventsGal.EventImageLink != null
                   && Convert.ToString(EventsGal.GalleryId) != null && EventsGal.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrEventGallery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", EventsGal.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@EventId", Convert.ToInt32(EventsGal.EventId.ToString()));
                    cmd.Parameters.AddWithValue("@EventImageLink", EventsGal.EventImageLink.ToString());
                    cmd.Parameters.AddWithValue("@GalleryId", EventsGal.GalleryId.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", EventsGal.CreatedBy.Trim());

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
                        CulturalEventsRes InsCE = new CulturalEventsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        CulturalEventsRes InsCE = new CulturalEventsRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    CulturalEventsRes InsCE = new CulturalEventsRes
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




        /*************************************Other Info************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetOtherInfo/ListAll")]
        public IHttpActionResult GetOtherInfo()
        {
            try
            {
                List<OtherInfo> li = new List<OtherInfo>();
                string sQuery = string.Empty;

                sQuery = "select InfoId,InfoName,InfoDescription,InfoImageLink,CreatedBy,ActiveStatus from OtherInfo where ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherInfo ShowOtherInfo = new OtherInfo();
                        ShowOtherInfo.InfoId = Convert.ToInt32(dt.Rows[i]["InfoId"].ToString());
                        ShowOtherInfo.InfoName = dt.Rows[i]["InfoName"].ToString();
                        ShowOtherInfo.InfoDescription = dt.Rows[i]["InfoDescription"].ToString();
                        ShowOtherInfo.InfoImageLink = dt.Rows[i]["InfoImageLink"].ToString();
                        ShowOtherInfo.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        ShowOtherInfo.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowOtherInfo);

                    }

                    OtherInfoList ConfList = new OtherInfoList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    OtherInfoRes ConfRes = new OtherInfoRes
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


        [HttpPost]
        [AllowAnonymous]
        [Route("OtherInfo")]
        public IHttpActionResult OtherInfo([FromBody] OtherInfo otherinfo)
        {
            try
            {
                if (otherinfo.QueryType != null && otherinfo.InfoName != null && otherinfo.InfoDescription != null && Convert.ToString(otherinfo.InfoId) != null &&
                    otherinfo.InfoImageLink != null && otherinfo.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrOtherInFO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", otherinfo.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@InfoId", Convert.ToInt32(otherinfo.InfoId.ToString()));
                    cmd.Parameters.AddWithValue("@InfoName", otherinfo.InfoName.ToString());
                    cmd.Parameters.AddWithValue("@InfoDescription", otherinfo.InfoDescription.Trim());
                    cmd.Parameters.AddWithValue("@InfoImageLink", otherinfo.InfoImageLink.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", otherinfo.CreatedBy.Trim());
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
                        OtherInfoRes Res = new OtherInfoRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Res);
                    }
                    else
                    {
                        OtherInfoRes Res = new OtherInfoRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Res);
                    }
                }
                else
                {
                    OtherInfoRes Res = new OtherInfoRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Res);
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



        /*************************************Other Gallery************************/


        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("OtherGallery/ListAll")]
        public IHttpActionResult GetOtherGallery()
        {
            try
            {
                List<OtherGallery> li = new List<OtherGallery>();

                string sQuery = string.Empty;

                sQuery = "Select GalleryId,Type,ImageVideolink,ActiveStatus from OtherGallery where ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OtherGallery ShowOtherGallery = new OtherGallery();
                        ShowOtherGallery.GalleryId = dt.Rows[i]["GalleryId"].ToString();
                        ShowOtherGallery.Type = dt.Rows[i]["Type"].ToString();
                        ShowOtherGallery.ImageVideoLink = dt.Rows[i]["ImageVideoLink"].ToString();
                        ShowOtherGallery.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowOtherGallery);

                    }

                    OtherGalleryList GalleryList = new OtherGalleryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(GalleryList);
                }

                else
                {
                    OtherGalleryrRes GalleryRes = new OtherGalleryrRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(GalleryRes);
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
        [Route("OtherGallery")]
        public IHttpActionResult OtherGallery([FromBody] OtherGallery InsOtherGallery)
        {
            try
            {
                if (InsOtherGallery.QueryType != null && Convert.ToString(InsOtherGallery.GalleryId) != null && InsOtherGallery.Type != null
                    && InsOtherGallery.ImageVideoLink != null && InsOtherGallery.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrOtherGallery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsOtherGallery.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@GalleryId", Convert.ToInt32(InsOtherGallery.GalleryId.ToString()));
                    cmd.Parameters.AddWithValue("@Type", InsOtherGallery.Type.ToString());
                    cmd.Parameters.AddWithValue("@ImageVideoLink", InsOtherGallery.ImageVideoLink.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsOtherGallery.CreatedBy.Trim());

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
                        OtherGalleryrRes InsCE = new OtherGalleryrRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        OtherGalleryrRes InsCE = new OtherGalleryrRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    OtherGalleryrRes InsCE = new OtherGalleryrRes
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

        /*************************************Important Links************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("Importantlinks/ListAll")]
        public IHttpActionResult GetImportantlinks()
        {
            try
            {
                List<ImportantLinks> li = new List<ImportantLinks>();
                string sQuery = string.Empty;

                sQuery = " Select A.LinkId, A.LinkType, A.LinkName,A.ActiveStatus, A.LinkURL, B.ConfigName AS LinkTypeName, A.LinkURL, A.CreatedBy from ImpLinks "
                        + " AS A Inner Join ConfigurationMaster AS B ON A.LinkType = B.ConfigID AND B.ActiveStatus = 'A' AND B.TypeID = '12' "
                        + " WHERE A.ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ImportantLinks ShowImportantlinks = new ImportantLinks();
                        ShowImportantlinks.LinkId = dt.Rows[i]["LinkId"].ToString();
                        ShowImportantlinks.LinkType = dt.Rows[i]["LinkType"].ToString();
                        ShowImportantlinks.LinkName = dt.Rows[i]["LinkName"].ToString();
                        ShowImportantlinks.LinkTypeName = dt.Rows[i]["LinkTypeName"].ToString();
                        ShowImportantlinks.LinkURL = dt.Rows[i]["LinkURL"].ToString();
                        ShowImportantlinks.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(ShowImportantlinks);

                    }

                    ImportantLinksList ImportLinkList = new ImportantLinksList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ImportLinkList);
                }

                else
                {
                    ImportantLinksRes ImportLinkRes = new ImportantLinksRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ImportLinkRes);
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
        [Route("ImportantLink")]
        public IHttpActionResult ImportantLinks([FromBody] ImportantLinks Links)
        {
            try
            {
                if (Links.QueryType != null && Links.LinkName != null && Links.LinkURL != null && Convert.ToString(Links.LinkId) != null
                    && Links.CreatedBy != null && Links.LinkType != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ImportantLinks", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Links.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@LinkId", Convert.ToInt32(Links.LinkId.ToString()));
                    cmd.Parameters.AddWithValue("@LinkName", Links.LinkName.ToString());
                    cmd.Parameters.AddWithValue("@LinkType", Convert.ToInt32(Links.LinkType.Trim()));
                    cmd.Parameters.AddWithValue("@LinkURL", Links.LinkURL.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", Links.CreatedBy.Trim());
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
                        ImportantLinksRes Res = new ImportantLinksRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Res);
                    }
                    else
                    {
                        ImportantLinksRes Res = new ImportantLinksRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Res);
                    }
                }
                else
                {
                    ImportantLinksRes Res = new ImportantLinksRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Res);
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


        /*************************************Enquiry************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("Enquiry/ListAll")]
        public IHttpActionResult GetEnquiry()
        {
            try
            {
                List<Enquiry> li = new List<Enquiry>();
                string sQuery = string.Empty;

                sQuery = " SELECT A.EnquiryId,A.EnquiryType,B.ConfigName As EnquiryTypeName,A.EnquiredBy,A.Address,A.MobileNo,A.MailId,A.QueryDetails, "
                       + " A.ResponseDetails , A.ResponseBy from Enquiry As A INNER JOIN ConfigurationMaster AS B ON A.EnquiryType = B.ConfigID AND B.TypeID = '25' AND B.ActiveStatus='A'  where ActiveStatus = 'A'";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Enquiry ShowEQ = new Enquiry();
                        ShowEQ.EnquiryId = dt.Rows[i]["EnquiryId"].ToString();
                        ShowEQ.EnquiryType = dt.Rows[i]["EnquiryType"].ToString();
                        ShowEQ.EnquiryTypeName = dt.Rows[i]["EnquiryTypeName"].ToString();
                        ShowEQ.EnquiredBy = dt.Rows[i]["EnquiredBy"].ToString();
                        ShowEQ.Address = dt.Rows[i]["Address"].ToString();
                        ShowEQ.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        ShowEQ.MailId = dt.Rows[i]["MailId"].ToString();
                        ShowEQ.QueryDetails = dt.Rows[i]["QueryDetails"].ToString();
                        ShowEQ.ResponseDetails = dt.Rows[i]["ResponseDetails"].ToString();
                        ShowEQ.ResponseBy = dt.Rows[i]["ResponseBy"].ToString();


                        li.Add(ShowEQ);

                    }

                    EnquiryList EnquiryList = new EnquiryList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(EnquiryList);
                }

                else
                {
                    EnquiryRes EnquiryRes = new EnquiryRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(EnquiryRes);
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
        [Route("Enquiry")]
        public IHttpActionResult Enquiry([FromBody] Enquiry enquiry)
        {
            try
            {
                if (enquiry.QueryType != null && enquiry.EnquiryType != null && enquiry.EnquiredBy != null
                    && Convert.ToString(enquiry.EnquiryId) != null && enquiry.Address != null && enquiry.MobileNo != null &&
                    enquiry.MailId != null && enquiry.QueryDetails != null && enquiry.ResponseDetails != null && enquiry.ResponseBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrEnquiry", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", enquiry.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@EnquiryId", Convert.ToInt32(enquiry.EnquiryId.ToString()));
                    cmd.Parameters.AddWithValue("@EnquiredBy", enquiry.EnquiredBy.ToString());
                    cmd.Parameters.AddWithValue("@EnquiryType", enquiry.EnquiryType.Trim());
                    cmd.Parameters.AddWithValue("@Address", enquiry.Address.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", enquiry.MobileNo.Trim());
                    cmd.Parameters.AddWithValue("@MailId", enquiry.MailId.Trim());
                    cmd.Parameters.AddWithValue("@QueryDetails", enquiry.QueryDetails.Trim());
                    cmd.Parameters.AddWithValue("@ResponseDetails", enquiry.ResponseDetails.Trim());
                    cmd.Parameters.AddWithValue("@ResponseBy", enquiry.ResponseBy.Trim());
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
                        EnquiryRes Res = new EnquiryRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Res);
                    }
                    else
                    {
                        EnquiryRes Res = new EnquiryRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Res);
                    }
                }
                else
                {
                    EnquiryRes Res = new EnquiryRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Res);
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


        /*************************************FeedBack************************/

        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("FeedBack/ListAll")]
        public IHttpActionResult GetFeedBack()
        {
            try
            {
                List<Feedback> li = new List<Feedback>();
                string sQuery = string.Empty;

                sQuery = " SELECT FeedbackId,GivenBy,Address,MobileNo,MailId,Feedback,HomePageDisplay,ActionDetails,"
                       + " Status, ActionBy, ActionDate from FeedBack";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Feedback ShowFeedback = new Feedback();
                        ShowFeedback.FeedbackId = dt.Rows[i]["FeedbackId"].ToString();
                        ShowFeedback.GivenBy = dt.Rows[i]["GivenBy"].ToString();
                        ShowFeedback.Address = dt.Rows[i]["Address"].ToString();
                        ShowFeedback.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        ShowFeedback.MailId = dt.Rows[i]["MailId"].ToString();
                        ShowFeedback.FeedbackDet = dt.Rows[i]["Feedback"].ToString();
                        ShowFeedback.HomePageDisplay = dt.Rows[i]["HomePageDisplay"].ToString();
                        ShowFeedback.ActionDetails = dt.Rows[i]["ActionDetails"].ToString();
                        ShowFeedback.Status = dt.Rows[i]["Status"].ToString();
                        ShowFeedback.ActionBy = dt.Rows[i]["ActionBy"].ToString();
                        ShowFeedback.ActionDate = dt.Rows[i]["ActionDate"].ToString();


                        li.Add(ShowFeedback);

                    }

                    FeedbackList FBList = new FeedbackList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(FBList);
                }

                else
                {
                    FeedbackRes FBRes = new FeedbackRes
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
        [Route("Feedbacks")]
        public IHttpActionResult InsFeedback([FromBody] Feedback feedback)
        {
            try
            {
                if (feedback.QueryType != null && Convert.ToString(feedback.FeedbackId) != null && feedback.GivenBy != null
                    && feedback.Address != null && feedback.MobileNo != null && feedback.MailId != null && feedback.FeedbackDet != null &&
                    feedback.HomePageDisplay != null && feedback.ActionDetails != null
                    && Convert.ToString(feedback.Status) != null && feedback.ActionBy != null && feedback.ActionDate != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrFeedBack", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", feedback.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@FeedbackId", Convert.ToInt32(feedback.FeedbackId.ToString()));
                    cmd.Parameters.AddWithValue("@GivenBy", feedback.GivenBy.ToString());
                    cmd.Parameters.AddWithValue("@Address", feedback.Address.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", feedback.MobileNo.Trim());
                    cmd.Parameters.AddWithValue("@MailId", feedback.MailId.Trim());
                    cmd.Parameters.AddWithValue("@Feedback", feedback.FeedbackDet.Trim());
                    cmd.Parameters.AddWithValue("@HomePageDisplay", feedback.HomePageDisplay.ToString());
                    cmd.Parameters.AddWithValue("@ActionDetails", feedback.ActionDetails.ToString());
                    cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(feedback.Status.Trim()));
                    cmd.Parameters.AddWithValue("@ActionBy", feedback.ActionBy.Trim());
                    cmd.Parameters.AddWithValue("@ActionDate", DateTime.Parse(feedback.ActionDate.Trim(), objEnglishDate));


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
                        FeedbackRes InsCE = new FeedbackRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        FeedbackRes InsCE = new FeedbackRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    FeedbackRes InsCE = new FeedbackRes
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


        /**************************************Scrolling Info**************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("ScrollingInfo")]
        public IHttpActionResult ScrollingInfo([FromBody] ScrollingInfo otherinfo)
        {
            try
            {
                if (otherinfo.QueryType != null && Convert.ToString(otherinfo.InfoId) != null && otherinfo.Information != null &&
                    otherinfo.InfoType != null && otherinfo.InfoLinkURL != null && otherinfo.CreatedBy != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrScrollingInfo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", otherinfo.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@InfoId", Convert.ToInt32(otherinfo.InfoId.ToString()));
                    cmd.Parameters.AddWithValue("@Information", otherinfo.Information.ToString());
                    cmd.Parameters.AddWithValue("@InfoLinkURL", otherinfo.InfoLinkURL.Trim());
                    cmd.Parameters.AddWithValue("@InfoType", otherinfo.InfoType.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", otherinfo.CreatedBy.Trim());
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
                        ScrollingInfoRes Res = new ScrollingInfoRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Res);
                    }
                    else
                    {
                        ScrollingInfoRes Res = new ScrollingInfoRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Res);
                    }
                }
                else
                {
                    OtherInfoRes Res = new OtherInfoRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Res);
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ScrollingInfo/ListAll")]
        public IHttpActionResult GetScrollingInfo()
        {
            try
            {
                List<ScrollingInfo> li = new List<ScrollingInfo>();
                string sQuery = string.Empty;

                sQuery = " Select InfoId, Information, InfoLinkURL, CASE WHEN InfoType = 'I' THEN 'Information' else 'Link' END AS InformationType, "
                       + " ActiveStatus, CreatedBy from Scrollinginfo WHERE ActiveStatus IN ('A','D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ScrollingInfo showscrollinginfo = new ScrollingInfo();
                        showscrollinginfo.InfoId = dt.Rows[i]["InfoId"].ToString();
                        showscrollinginfo.Information = dt.Rows[i]["Information"].ToString();
                        showscrollinginfo.InfoLinkURL = dt.Rows[i]["InfoLinkURL"].ToString();
                        showscrollinginfo.InformationType = dt.Rows[i]["InformationType"].ToString();
                        showscrollinginfo.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        showscrollinginfo.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                        li.Add(showscrollinginfo);

                    }

                    ScrollingInfoList FBList = new ScrollingInfoList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(FBList);
                }

                else
                {
                    ScrollingInfoRes FBRes = new ScrollingInfoRes
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


        /******************App Carousel*****************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("AppCarousel")]
        public IHttpActionResult InsAppCarousel([FromBody] AppCarousel InsCarousel)
        {
            try
            {
                if (InsCarousel.QueryType != null && Convert.ToString(InsCarousel.CarouselId) != null && Convert.ToString(InsCarousel.CorpID) != null
                    && InsCarousel.AppName != null && InsCarousel.Createdby != null && InsCarousel.Carousel != null)
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAppCarousel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsCarousel.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CarouselId", Convert.ToInt32(InsCarousel.CarouselId.ToString()));
                    cmd.Parameters.AddWithValue("@CorpID", Convert.ToInt32(InsCarousel.CorpID.ToString()));
                    cmd.Parameters.AddWithValue("@AppName", InsCarousel.AppName.Trim());
                    cmd.Parameters.AddWithValue("@Carousel", InsCarousel.Carousel.Trim());
                    cmd.Parameters.AddWithValue("@Createdby", InsCarousel.Createdby.Trim());


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
                        AppCarouselRes InAppCar = new AppCarouselRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        AppCarouselRes InAppCar = new AppCarouselRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    AppCarouselRes InAppCar = new AppCarouselRes
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
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("AppCarousel/ListAll")]
        public IHttpActionResult GetAppCarousel()
        {
            try
            {
                List<AppCarousel> li = new List<AppCarousel>();
                string sQuery = string.Empty;

                sQuery = " Select A.CorpID, B.CorpName, A.CarouselId, A.AppName, A.Carousel,A.ActiveStatus from AppCarousel AS A "
                       + " Inner Join CompanyMaster AS B On A.CorpID = B.CorpID Where A.ActiveStatus IN('A', 'D')";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AppCarousel ShowAppCarousel = new AppCarousel();
                        ShowAppCarousel.CarouselId = Convert.ToInt32(dt.Rows[i]["CarouselId"].ToString());
                        ShowAppCarousel.CorpID = Convert.ToInt32(dt.Rows[i]["CorpID"].ToString());
                        ShowAppCarousel.CorpName = dt.Rows[i]["CorpName"].ToString();
                        ShowAppCarousel.AppName = dt.Rows[i]["AppName"].ToString();
                        ShowAppCarousel.Carousel = dt.Rows[i]["Carousel"].ToString();
                        ShowAppCarousel.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                        li.Add(ShowAppCarousel);

                    }

                    AppCarouselList AppList = new AppCarouselList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(AppList);
                }

                else
                {
                    AppCarouselRes AppRes = new AppCarouselRes
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


        /// <summary>
        /// Developed By : Imran and Abhi
        /// Integrated By : Vediyappan.V
        /// Developed Date: 05-July-2021
        /// Modified By : Vediyappan.V
        /// Modified Date : 26-April-2023
        /// Description : Implement proper parameterized query mechanism in all user input fields.
        /// </summary>
        /// <param name="BRD"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRateDetails")]
        public IHttpActionResult BoatRateDetais([FromBody] BoatRateDetails BRD)
        {
            try
            {
                List<BoatRateDetails> li = new List<BoatRateDetails>();
                string sQuery = string.Empty;

                sQuery = " Select A.BoatHouseId, C.BoatType, C.BoatTypeId, D.SeaterType, D.BoatSeaterId, CASE WHEN A.SelfDrive = 'A' THEN 'Allowed' ELSE 'Not Allowed' End AS 'SelfDrive', "
                        + "  CASE WHEN A.TimeExtension = 'A' THEN 'Allowed' ELSE 'Not Allowed' End AS 'TimeExtension',A.BoatMinTime,A.BoatGraceTime,A.Deposit,A.BoatMinCharge, "
                        + "  A.RowerMinCharge,A.BoatMinTaxAmt,A.BoatMinTotAmt,A.BoatPremMinCharge,A.RowerPremMinCharge,A.BoatPremTaxAmt,A.BoatPremTotAmt, "
                        + "  CASE WHEN B.ExtensionType = 'N' THEN 'Normal' ELSE 'Premium' End AS 'ExtensionType',B.ExtFromTime,B.ExtToTime,CASE WHEN B.AmtType = 'P' THEN '%' "
                        + "  ELSE 'Fixed' End AS 'AmountType',B.Percentage,B.BoatExtnCharge,B.RowerExtnCharge,B.BoatExtnTaxAmt,B.BoatExtnTotAmt from BoatRateMaster as A "
                        + "  INNER JOIN BoatRateExtnCharge AS B ON A.BoatHouseId = B.BoatHouseId and A.BoatTypeId = B.BoatTypeId and A.BoatSeaterId = B.BoatSeaterId "
                        + "  INNER JOIN BoatTypes AS C ON A.BoatHouseId = C.BoatHouseId AND A.BoatTypeId = C.BoatTypeId "
                        + "  INNER JOIN BoatSeat AS D ON A.BoatHouseId = D.BoatHouseId AND A.BoatSeaterId = D.BoatSeaterId WHERE A.BoatHouseId = @BoatHouseId order by  BoatTypeId asc ,  BoatSeaterId  asc";


                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = BRD.BoatHouseId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatRateDetails ShowRateMaster = new BoatRateDetails();
                        ShowRateMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowRateMaster.BoatType = dt.Rows[i]["BoatType"].ToString();
                        ShowRateMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                        ShowRateMaster.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                        ShowRateMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                        ShowRateMaster.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                        ShowRateMaster.TimeExtension = dt.Rows[i]["TimeExtension"].ToString();
                        ShowRateMaster.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();
                        ShowRateMaster.BoatGraceTime = dt.Rows[i]["BoatGraceTime"].ToString();
                        ShowRateMaster.Deposit = dt.Rows[i]["Deposit"].ToString();
                        ShowRateMaster.BoatMinCharge = dt.Rows[i]["BoatMinCharge"].ToString();
                        ShowRateMaster.RowerMinCharge = dt.Rows[i]["RowerMinCharge"].ToString();
                        ShowRateMaster.BoatMinTaxAmt = dt.Rows[i]["BoatMinTaxAmt"].ToString();
                        ShowRateMaster.BoatMinTotAmt = dt.Rows[i]["BoatMinTotAmt"].ToString();
                        ShowRateMaster.BoatPremMinCharge = dt.Rows[i]["BoatPremMinCharge"].ToString();
                        ShowRateMaster.RowerPremMinCharge = dt.Rows[i]["RowerPremMinCharge"].ToString();
                        ShowRateMaster.BoatPremTaxAmt = dt.Rows[i]["BoatPremTaxAmt"].ToString();
                        ShowRateMaster.BoatPremTotAmt = dt.Rows[i]["BoatPremTotAmt"].ToString();
                        ShowRateMaster.ExtensionType = dt.Rows[i]["ExtensionType"].ToString();
                        ShowRateMaster.ExtFromTime = dt.Rows[i]["ExtFromTime"].ToString();
                        ShowRateMaster.ExtToTime = dt.Rows[i]["ExtToTime"].ToString();
                        ShowRateMaster.AmountType = dt.Rows[i]["AmountType"].ToString();
                        ShowRateMaster.Percentage = dt.Rows[i]["Percentage"].ToString();
                        ShowRateMaster.BoatExtnCharge = dt.Rows[i]["BoatExtnCharge"].ToString();
                        ShowRateMaster.RowerExtnCharge = dt.Rows[i]["RowerExtnCharge"].ToString();
                        ShowRateMaster.BoatExtnTaxAmt = dt.Rows[i]["BoatExtnTaxAmt"].ToString();
                        ShowRateMaster.BoatExtnTotAmt = dt.Rows[i]["BoatExtnTotAmt"].ToString();

                        li.Add(ShowRateMaster);
                    }

                    BoatRateDetailsList tripsheet = new BoatRateDetailsList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(tripsheet);
                }
                else
                {
                    BoatRateDetailsRes BoatHouse = new BoatRateDetailsRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatHouse);
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
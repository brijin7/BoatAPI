using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using TTDCAPIv1.Models;

namespace TTDCAPIv1.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class FixedAssetsController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_FA"].ConnectionString);
        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

        /***********************************Common Basic Details**************************************/

        [HttpPost]
        [AllowAnonymous]
        [Route("FA_CommonReport")]
        public IHttpActionResult CommonReport([FromBody] FA_CommonMethod PinDet)
        {
            try
            {
                if (PinDet.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("CommonBasicDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", PinDet.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@CorpId", PinDet.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", PinDet.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@Input1", PinDet.Input1.Trim());
                    cmd.Parameters.AddWithValue("@Input2", PinDet.Input2.Trim());
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

        /*Configuration Type Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsConfigType")]
        public IHttpActionResult AssertConfigType([FromBody] FA_ConfigurationType AssConfig)
        {
            try
            {
                if (
                    AssConfig.QueryType != "" && AssConfig.ConfigurationName != "" && AssConfig.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetConfigurationType", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", AssConfig.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@TypeId", AssConfig.TypeId.ToString());
                    cmd.Parameters.AddWithValue("@TypeName", AssConfig.ConfigurationName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", AssConfig.CreatedBy.ToString());

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
                        FA_ConfigurationTypeRes EmMstr = new FA_ConfigurationTypeRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(EmMstr);
                    }
                    else
                    {
                        FA_ConfigurationTypeRes EmMstr = new FA_ConfigurationTypeRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(EmMstr);
                    }
                }
                else
                {
                    FA_ConfigurationTypeRes EmMstr = new FA_ConfigurationTypeRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
                }
            }
            catch (Exception ex)
            {
                FA_ConfigurationTypeRes ConfRes = new FA_ConfigurationTypeRes
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

        /*Configuration Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsConfigMstr")]
        public IHttpActionResult MstrConfigurationMaster([FromBody] FA_ConfigurationMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.TypeId != "" && InsConfMstr.ConfigId != ""
                    && InsConfMstr.ConfigName != "" && InsConfMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrConfigurationMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@TypeId", InsConfMstr.TypeId.ToString());
                    cmd.Parameters.AddWithValue("@ConfigId", InsConfMstr.ConfigId.ToString());
                    cmd.Parameters.AddWithValue("@ConfigName", InsConfMstr.ConfigName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsConfMstr.CreatedBy.Trim());

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
                        FA_ConfigurationMasterRes ConMstr = new FA_ConfigurationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_ConfigurationMasterRes ConMstr = new FA_ConfigurationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_ConfigurationMasterRes Vehicle = new FA_ConfigurationMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_ConfigurationMasterRes ConfRes = new FA_ConfigurationMasterRes
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

        /*Asset Type Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsAssetType")]
        public IHttpActionResult AssetType([FromBody] FA_AssetTypeMaster Ass)
        {
            try
            {
                if (Ass.QueryType != "" && Ass.CorpId != "" && Ass.AssetType != "" && Ass.AssetTypeName != ""
                    && Ass.CodePrefix != "" && Ass.ActiveStatus != "" && Ass.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetType", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Ass.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", Ass.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@AssetType", Ass.AssetType.ToString());
                    cmd.Parameters.AddWithValue("@AssetTypeName", Ass.AssetTypeName.ToString());
                    cmd.Parameters.AddWithValue("@CodePrefix", Ass.CodePrefix.ToString());
                    cmd.Parameters.AddWithValue("@GroupCode", Ass.GroupCode.ToString());
                    cmd.Parameters.AddWithValue("@ActiveStatus", Ass.ActiveStatus.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", Ass.CreatedBy.ToString());

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
                        FA_AssetTypeMasterRes AssMstr = new FA_AssetTypeMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(AssMstr);
                    }
                    else
                    {
                        FA_AssetTypeMasterRes AssMstr = new FA_AssetTypeMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(AssMstr);
                    }
                }
                else
                {
                    FA_AssetTypeMasterRes AssMstr = new FA_AssetTypeMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(AssMstr);
                }
            }
            catch (Exception ex)
            {
                FA_AssetTypeMasterRes ConfRes = new FA_AssetTypeMasterRes
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

        /*Group Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsGroupMstr")]
        public IHttpActionResult MstrGroupMaster([FromBody] FA_GroupMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.CorpId != "" && InsConfMstr.GroupCategory != "" && InsConfMstr.GroupCode != ""
                    && InsConfMstr.GroupName != "" && InsConfMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrGroupMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsConfMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@GroupCategory", InsConfMstr.GroupCategory.ToString());
                    cmd.Parameters.AddWithValue("@GroupCode", InsConfMstr.GroupCode.ToString());
                    cmd.Parameters.AddWithValue("@GroupName", InsConfMstr.GroupName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsConfMstr.CreatedBy.Trim());

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
                        FA_GroupMasterRes ConMstr = new FA_GroupMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_GroupMasterRes ConMstr = new FA_GroupMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_GroupMasterRes Vehicle = new FA_GroupMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_GroupMasterRes ConfRes = new FA_GroupMasterRes
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


        /*Custodian Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsCustodianMstr")]
        public IHttpActionResult AssertConfigType([FromBody] FA_CustodianMaster AssCust)
        {
            try
            {
                if (
                    AssCust.QueryType != ""
                    && AssCust.CorpId != ""
                    && AssCust.CustodianId != ""
                    && AssCust.CustodianName != ""
                    && AssCust.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrCustodianMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", AssCust.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", AssCust.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@CustodianId", AssCust.CustodianId.ToString());
                    cmd.Parameters.AddWithValue("@CustodianName", AssCust.CustodianName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", AssCust.CreatedBy.ToString());

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
                        FA_CustodianMasterRes EmMstr = new FA_CustodianMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(EmMstr);
                    }
                    else
                    {
                        FA_CustodianMasterRes EmMstr = new FA_CustodianMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(EmMstr);
                    }
                }
                else
                {
                    FA_CustodianMasterRes EmMstr = new FA_CustodianMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
                }
            }
            catch (Exception ex)
            {
                FA_CustodianMasterRes ConfRes = new FA_CustodianMasterRes
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

        /*Depreciation Rate Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsDepRateMstr")]
        public IHttpActionResult MstrDepRateMaster([FromBody] FA_DepreciationRateMaster InsDepRateMstr)
        {
            try
            {
                if (InsDepRateMstr.QueryType != "" && InsDepRateMstr.GroupCode != "" && InsDepRateMstr.RateLifeFlag != "" && InsDepRateMstr.AnnualRate != ""
                    && InsDepRateMstr.LifeType != null && InsDepRateMstr.LifePeriod != ""
                    && InsDepRateMstr.CorpId != "" && InsDepRateMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrDepreciationRateMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsDepRateMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsDepRateMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@GroupCode", InsDepRateMstr.GroupCode.ToString());
                    cmd.Parameters.AddWithValue("@RateLifeFlag", InsDepRateMstr.RateLifeFlag.Trim());
                    cmd.Parameters.AddWithValue("@AnnualRate", InsDepRateMstr.AnnualRate.ToString());
                    cmd.Parameters.AddWithValue("@LifeType", InsDepRateMstr.LifeType.ToString());
                    cmd.Parameters.AddWithValue("@LifePeriod", InsDepRateMstr.LifePeriod.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsDepRateMstr.CreatedBy.Trim());

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
                        FA_DepreciationRateMasterRes ConMstr = new FA_DepreciationRateMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_DepreciationRateMasterRes ConMstr = new FA_DepreciationRateMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_DepreciationRateMasterRes Vehicle = new FA_DepreciationRateMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_DepreciationRateMasterRes ConfRes = new FA_DepreciationRateMasterRes
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

        /*Asset Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsAssetMstr")]
        public IHttpActionResult FA_MstrAssetMaster([FromBody] FA_AssetMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.CorpId != "" && InsConfMstr.BranchCode != ""
                    && InsConfMstr.AssetGroup != "" && InsConfMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsConfMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsConfMstr.AssetCode.Trim());
                    cmd.Parameters.AddWithValue("@AssetSl", InsConfMstr.AssetSl.Trim());
                    cmd.Parameters.AddWithValue("@AssetName", InsConfMstr.AssetName.Trim());
                    cmd.Parameters.AddWithValue("@AssetShName", InsConfMstr.AssetShName.Trim());
                    cmd.Parameters.AddWithValue("@AssetDesc", InsConfMstr.AssetDesc.Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", InsConfMstr.BranchCode.ToString());

                    cmd.Parameters.AddWithValue("@AssetTag", InsConfMstr.AssetTag.Trim());
                    cmd.Parameters.AddWithValue("@AssetType", InsConfMstr.AssetType.Trim());
                    cmd.Parameters.AddWithValue("@AssetGroup", InsConfMstr.AssetGroup.Trim());

                    cmd.Parameters.AddWithValue("@LocationId", InsConfMstr.LocationId.Trim());
                    cmd.Parameters.AddWithValue("@DeptId", InsConfMstr.DeptId.Trim());

                    if (InsConfMstr.VendorId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@VendorId", InsConfMstr.VendorId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@VendorId", System.DBNull.Value);
                    }

                    if (InsConfMstr.ManufId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@ManufId", InsConfMstr.ManufId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ManufId", System.DBNull.Value);
                    }

                    if (InsConfMstr.CustodianId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@CustodianId", InsConfMstr.CustodianId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CustodianId", System.DBNull.Value);
                    }


                    cmd.Parameters.AddWithValue("@InstallationDt", DateTime.Parse(InsConfMstr.InstallationDt.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@DepStartDt", DateTime.Parse(InsConfMstr.DepStartDt.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ProductSlNo", InsConfMstr.ProductSlNo.Trim());
                    cmd.Parameters.AddWithValue("@ProductQty", InsConfMstr.ProductQty.Trim());

                    cmd.Parameters.AddWithValue("@PurchaseRefNo", InsConfMstr.PurchaseRefNo.Trim());

                    if (InsConfMstr.PurchaseRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRefDt", DateTime.Parse(InsConfMstr.PurchaseRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRefDt", System.DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@InvoiceRefNo", InsConfMstr.InvoiceRefNo.Trim());

                    if (InsConfMstr.InvoiceRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceRefDt", DateTime.Parse(InsConfMstr.InvoiceRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceRefDt", System.DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@OtherRefNo", InsConfMstr.OtherRefNo.Trim());

                    if (InsConfMstr.OtherRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@OtherRefDt", DateTime.Parse(InsConfMstr.OtherRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OtherRefDt", System.DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@ProductCost", InsConfMstr.ProductCost.Trim());
                    cmd.Parameters.AddWithValue("@WarrantyRef", InsConfMstr.WarrantyRef.Trim());

                    if (InsConfMstr.WarrantyStart.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@WarrantyStart", DateTime.Parse(InsConfMstr.WarrantyStart.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WarrantyStart", System.DBNull.Value);
                    }
                    if (InsConfMstr.WarrantyEnd.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@WarrantyEnd", DateTime.Parse(InsConfMstr.WarrantyEnd.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WarrantyEnd", System.DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@ResidualPer", InsConfMstr.ResidualPer.Trim());
                    cmd.Parameters.AddWithValue("@ResidualValue", InsConfMstr.ResidualValue.Trim());

                    cmd.Parameters.AddWithValue("@OrginalQty", InsConfMstr.OrginalQty.Trim());
                    cmd.Parameters.AddWithValue("@OtherInfo", InsConfMstr.OtherInfo.Trim());
                    cmd.Parameters.AddWithValue("@WriteOffFlag", InsConfMstr.WriteOffFlag.Trim());
                    cmd.Parameters.AddWithValue("@NoDepFlag", InsConfMstr.NoDepFlag.Trim());
                    cmd.Parameters.AddWithValue("@QtySlNo", InsConfMstr.QtySlNo.Trim());
                    //cmd.Parameters.AddWithValue("@StockSI", InsConfMstr.StockSI.Trim());
                    //cmd.Parameters.AddWithValue("@StockName", InsConfMstr.StockName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsConfMstr.CreatedBy.Trim());

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
                        FA_LocationMasterRes ConMstr = new FA_LocationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_LocationMasterRes ConMstr = new FA_LocationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_LocationMasterRes Vehicle = new FA_LocationMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_LocationMasterRes ConfRes = new FA_LocationMasterRes
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

        /*Asset Disposal*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsDisposalHdr")]
        public IHttpActionResult InsDisposalHdr([FromBody] FA_DisposalDtl InsDisposalHdr)
        {
            try
            {
                if (InsDisposalHdr.QueryType != "" && InsDisposalHdr.CorpId != ""
                    && InsDisposalHdr.BranchCode != "" && InsDisposalHdr.DispDocNo != ""
                    && InsDisposalHdr.DispDocDt != "" && InsDisposalHdr.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrDisposaldtl", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsDisposalHdr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsDisposalHdr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsDisposalHdr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@DispTransNo", InsDisposalHdr.DispTransNo.ToString());
                    cmd.Parameters.AddWithValue("@TransSlNo", InsDisposalHdr.TransSlNo.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsDisposalHdr.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@DispDate", DateTime.Parse(InsDisposalHdr.DispDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@DispValue", InsDisposalHdr.DispValue.ToString());
                    cmd.Parameters.AddWithValue("@DispDocNo", InsDisposalHdr.DispDocNo.ToString());
                    cmd.Parameters.AddWithValue("@DispDocDt", DateTime.Parse(InsDisposalHdr.DispDocDt.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@CreatedBy", InsDisposalHdr.Createdby.ToString());

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
                        FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_DisposalDtlRes VendorRes = new FA_DisposalDtlRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(VendorRes);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsDiposalDtl")]
        public IHttpActionResult InsDiposalDtl([FromBody] FA_DisposalDtl InsDisposalHdr)
        {
            try
            {
                if (InsDisposalHdr.QueryType != "" && InsDisposalHdr.CorpId != "" && InsDisposalHdr.BranchCode != ""
                     && InsDisposalHdr.DispTransNo != "" && InsDisposalHdr.AssetCode != "" && InsDisposalHdr.DispDate != ""
                      && InsDisposalHdr.DispValue != "" && InsDisposalHdr.DispDocNo != "" && InsDisposalHdr.DispDocDt != ""
                      && InsDisposalHdr.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrDisposaldtl", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsDisposalHdr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsDisposalHdr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsDisposalHdr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@DispTransNo", InsDisposalHdr.DispTransNo.ToString());
                    cmd.Parameters.AddWithValue("@TransSlNo", InsDisposalHdr.TransSlNo.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsDisposalHdr.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@DispDate", DateTime.Parse(InsDisposalHdr.DispDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@DispValue", InsDisposalHdr.DispValue.ToString());
                    cmd.Parameters.AddWithValue("@DispDocNo", InsDisposalHdr.DispDocNo.ToString());
                    cmd.Parameters.AddWithValue("@DispDocDt", DateTime.Parse(InsDisposalHdr.DispDocDt.ToString(), objEnglishDate));

                    cmd.Parameters.AddWithValue("@CreatedBy", InsDisposalHdr.Createdby.ToString());

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
                        FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_DisposalDtlRes VendorRes = new FA_DisposalDtlRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(VendorRes);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsDiposalView")]
        public IHttpActionResult InsDiposalView([FromBody] FA_DisposalDtl InsDisposalHdr)
        {
            try
            {
                if (InsDisposalHdr.QueryType != "" && InsDisposalHdr.CorpId != "" && InsDisposalHdr.BranchCode != ""
                     && InsDisposalHdr.DispTransNo != "" && InsDisposalHdr.DispDate != ""
                      && InsDisposalHdr.DispValue != "" && InsDisposalHdr.DispDocDt != ""
                      && InsDisposalHdr.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrDisposaldtl", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsDisposalHdr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsDisposalHdr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsDisposalHdr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@DispTransNo", InsDisposalHdr.DispTransNo.ToString());
                    cmd.Parameters.AddWithValue("@TransSlNo", InsDisposalHdr.TransSlNo.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsDisposalHdr.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@DispDate", DateTime.Parse(InsDisposalHdr.DispDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@DispValue", InsDisposalHdr.DispValue.ToString());
                    cmd.Parameters.AddWithValue("@DispDocNo", InsDisposalHdr.DispDocNo.ToString());
                    cmd.Parameters.AddWithValue("@DispDocDt", DateTime.Parse(InsDisposalHdr.DispDocDt.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@CreatedBy", InsDisposalHdr.Createdby.ToString());

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
                        FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_DisposalDtlRes Invendor = new FA_DisposalDtlRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_DisposalDtlRes VendorRes = new FA_DisposalDtlRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(VendorRes);
            }
        }

        /*Asset Transfer*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsTransferHdr")]
        public IHttpActionResult InsTransferHdr([FromBody] FA_TransferDetails InsTransferHdr)
        {
            try
            {
                if (InsTransferHdr.QueryType != "" && InsTransferHdr.CorpId != "" && InsTransferHdr.BranchCode != "" && InsTransferHdr.TransfTransNo != ""
                    && InsTransferHdr.TransfDocNo != "" && InsTransferHdr.TransfDocDt != "" && InsTransferHdr.ActiveStatus != "" &&
                    InsTransferHdr.ApprovedBy != "" && InsTransferHdr.ApprovedDate != "" && InsTransferHdr.AssetCode != "" &&
                    InsTransferHdr.FromLocationId != "" && InsTransferHdr.FromDeptId != "" && InsTransferHdr.FromCustodianId != "" &&
                    InsTransferHdr.ToBranchCode != "" && InsTransferHdr.EffectiveDate != "" && InsTransferHdr.AckStatus != ""
                    && InsTransferHdr.AckBy != "" &&
                    InsTransferHdr.AckDate != "" && InsTransferHdr.AckRemarks != "" && InsTransferHdr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrTransferDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsTransferHdr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsTransferHdr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsTransferHdr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@TransfTransNo", InsTransferHdr.TransfTransNo.Trim());
                    cmd.Parameters.AddWithValue("@TransfDocNo", InsTransferHdr.TransfDocNo.ToString());
                    cmd.Parameters.AddWithValue("@TransfDocDt", DateTime.Parse(InsTransferHdr.TransfDocDt.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ActiveStatus", InsTransferHdr.ActiveStatus.Trim());
                    cmd.Parameters.AddWithValue("@ApprovedBy", InsTransferHdr.ApprovedBy.Trim());

                    cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Parse(InsTransferHdr.ApprovedDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@AssetCode", InsTransferHdr.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@FromLocationId", InsTransferHdr.FromLocationId.ToString());
                    cmd.Parameters.AddWithValue("@FromDeptId", InsTransferHdr.FromDeptId.Trim());
                    cmd.Parameters.AddWithValue("@FromCustodianId", InsTransferHdr.FromCustodianId.ToString());

                    cmd.Parameters.AddWithValue("@ToBranchCode", InsTransferHdr.ToBranchCode.ToString());
                    cmd.Parameters.AddWithValue("@ToLocationId", InsTransferHdr.ToLocationId.ToString());
                    cmd.Parameters.AddWithValue("@ToDeptId", InsTransferHdr.ToDeptId.ToString());
                    cmd.Parameters.AddWithValue("@ToCustodianId", InsTransferHdr.ToCustodianId.Trim());

                    cmd.Parameters.AddWithValue("@EffectiveDate", DateTime.Parse(InsTransferHdr.EffectiveDate.ToString(), objEnglishDate));

                    cmd.Parameters.AddWithValue("@AckStatus", InsTransferHdr.AckStatus.ToString());
                    cmd.Parameters.AddWithValue("@AckBy", InsTransferHdr.AckBy.ToString());
                    cmd.Parameters.AddWithValue("@AckDate", DateTime.Parse(InsTransferHdr.AckDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@AckRemarks", InsTransferHdr.AckRemarks.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsTransferHdr.CreatedBy.ToString());

                    cmd.Parameters.AddWithValue("@SlNo", InsTransferHdr.SlNo.ToString());
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
                        FA_TransferDetailsRes ConMstr = new FA_TransferDetailsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_TransferDetailsRes ConMstr = new FA_TransferDetailsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_TransferDetailsRes Vehicle = new FA_TransferDetailsRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_TransferDetailsRes ConfRes = new FA_TransferDetailsRes
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
        [Route("FA_InsTransferView")]
        public IHttpActionResult InsTransferView([FromBody] FA_TransferDetails InsTransferView)
        {
            try
            {
                if (InsTransferView.QueryType != "" && InsTransferView.CorpId != ""
                    && InsTransferView.BranchCode != "" && InsTransferView.AssetCode != ""
                     && InsTransferView.TransfTransNo != "" && InsTransferView.SlNo != ""
                     && InsTransferView.ToBranchCode != "" && InsTransferView.ToLocationId != ""
                     && InsTransferView.ToDeptId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrTransferDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsTransferView.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsTransferView.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsTransferView.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@TransfTransNo", InsTransferView.TransfTransNo.Trim());
                    cmd.Parameters.AddWithValue("@TransfDocNo", InsTransferView.TransfDocNo.ToString());
                    cmd.Parameters.AddWithValue("@TransfDocDt", DateTime.Parse(InsTransferView.TransfDocDt.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ActiveStatus", InsTransferView.ActiveStatus.Trim());
                    cmd.Parameters.AddWithValue("@ApprovedBy", InsTransferView.ApprovedBy.Trim());

                    cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Parse(InsTransferView.ApprovedDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@AssetCode", InsTransferView.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@FromLocationId", InsTransferView.FromLocationId.ToString());
                    cmd.Parameters.AddWithValue("@FromDeptId", InsTransferView.FromDeptId.Trim());
                    cmd.Parameters.AddWithValue("@FromCustodianId", InsTransferView.FromCustodianId.ToString());

                    cmd.Parameters.AddWithValue("@ToBranchCode", InsTransferView.ToBranchCode.ToString());
                    cmd.Parameters.AddWithValue("@ToLocationId", InsTransferView.ToLocationId.ToString());
                    cmd.Parameters.AddWithValue("@ToDeptId", InsTransferView.ToDeptId.ToString());
                    cmd.Parameters.AddWithValue("@ToCustodianId", InsTransferView.ToCustodianId.Trim());

                    cmd.Parameters.AddWithValue("@EffectiveDate", DateTime.Parse(InsTransferView.EffectiveDate.ToString(), objEnglishDate));

                    cmd.Parameters.AddWithValue("@AckStatus", InsTransferView.AckStatus.ToString());
                    cmd.Parameters.AddWithValue("@AckBy", InsTransferView.AckBy.ToString());
                    cmd.Parameters.AddWithValue("@AckDate", DateTime.Parse(InsTransferView.AckDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@AckRemarks", InsTransferView.AckRemarks.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsTransferView.CreatedBy.ToString());

                    cmd.Parameters.AddWithValue("@SlNo", InsTransferView.SlNo.ToString());

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
                        FA_TransferDetailsRes Invendor = new FA_TransferDetailsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_TransferDetailsRes Invendor = new FA_TransferDetailsRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_TransferDetailsRes Invendor = new FA_TransferDetailsRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_TransferDetailsRes VendorRes = new FA_TransferDetailsRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(VendorRes);
            }
        }

        /*Asset Scrap*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsScrapHdr")]
        public IHttpActionResult InsScrapHdr([FromBody] FA_ScrapDtl InsScrapHdr)
        {
            try
            {
                if (InsScrapHdr.QueryType != "" && InsScrapHdr.CorpId != ""
                    && InsScrapHdr.BranchCode != "" && InsScrapHdr.ScrapDocNo != ""
                    && InsScrapHdr.ScrapDocDt != "" && InsScrapHdr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetScrapHdr", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsScrapHdr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsScrapHdr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsScrapHdr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@ScrapTransNo", InsScrapHdr.ScrapTransNo.ToString());
                    cmd.Parameters.AddWithValue("@TransSlNo", InsScrapHdr.TransSlNo.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsScrapHdr.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDate", DateTime.Parse(InsScrapHdr.ScrapDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ScrapValue", InsScrapHdr.ScrapValue.ToString());
                    cmd.Parameters.AddWithValue("@Reason", InsScrapHdr.Reason.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDocNo", InsScrapHdr.ScrapDocNo.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDocDt", DateTime.Parse(InsScrapHdr.ScrapDocDt.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@CreatedBy", InsScrapHdr.CreatedBy.ToString());

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
                        FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_ScrapDtlRes VendorRes = new FA_ScrapDtlRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(VendorRes);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsScrapDtl")]
        public IHttpActionResult InsScrapDtl([FromBody] FA_ScrapDtl InsScrapHdr)
        {
            try
            {
                if (InsScrapHdr.QueryType != "" && InsScrapHdr.CorpId != "" && InsScrapHdr.BranchCode != ""
                     && InsScrapHdr.ScrapTransNo != "" && InsScrapHdr.AssetCode != "" && InsScrapHdr.ScrapDate != ""
                     && InsScrapHdr.ScrapDocNo != "" && InsScrapHdr.ScrapDocDt != ""
                      && InsScrapHdr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetScrapHdr", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsScrapHdr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsScrapHdr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsScrapHdr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@ScrapTransNo", InsScrapHdr.ScrapTransNo.ToString());
                    cmd.Parameters.AddWithValue("@TransSlNo", InsScrapHdr.TransSlNo.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsScrapHdr.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDate", DateTime.Parse(InsScrapHdr.ScrapDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ScrapValue", InsScrapHdr.ScrapValue.ToString());
                    cmd.Parameters.AddWithValue("@Reason", InsScrapHdr.Reason.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDocNo", InsScrapHdr.ScrapDocNo.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDocDt", DateTime.Parse(InsScrapHdr.ScrapDocDt.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@CreatedBy", InsScrapHdr.CreatedBy.ToString());

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
                        FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_ScrapDtlRes VendorRes = new FA_ScrapDtlRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(VendorRes);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsScrapView")]
        public IHttpActionResult InsScrapView([FromBody] FA_ScrapDtl InsScrapHdr)
        {
            try
            {
                if (InsScrapHdr.QueryType != "" && InsScrapHdr.CorpId != "" && InsScrapHdr.BranchCode != ""
                     && InsScrapHdr.ScrapTransNo != "" && InsScrapHdr.TransSlNo != "" && InsScrapHdr.ScrapDate != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetScrapHdr", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsScrapHdr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsScrapHdr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsScrapHdr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@ScrapTransNo", InsScrapHdr.ScrapTransNo.ToString());
                    cmd.Parameters.AddWithValue("@TransSlNo", InsScrapHdr.TransSlNo.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsScrapHdr.AssetCode.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDate", DateTime.Parse(InsScrapHdr.ScrapDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ScrapValue", InsScrapHdr.ScrapValue.ToString());
                    cmd.Parameters.AddWithValue("@Reason", InsScrapHdr.Reason.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDocNo", InsScrapHdr.ScrapDocNo.ToString());
                    cmd.Parameters.AddWithValue("@ScrapDocDt", DateTime.Parse(InsScrapHdr.ScrapDocDt.ToString(), objEnglishDate));

                    cmd.Parameters.AddWithValue("@CreatedBy", InsScrapHdr.CreatedBy.ToString());

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
                        FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_ScrapDtlRes Invendor = new FA_ScrapDtlRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_ScrapDtlRes VendorRes = new FA_ScrapDtlRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(VendorRes);
            }
        }

        /// <summary>
        /// Modified Abhinaya K
        /// Modified 2023-04-25  
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Removed 1=1 in where condition , seemed to be not used   
        /// </summary>
        /// <param name="assDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_GetAssetDetails")]
        public IHttpActionResult GetAssetDetails([FromBody] AssetDtl assDtl)
        {
            try
            {
                string squery = string.Empty;

                if (assDtl.AssetGroup != "" && assDtl.BranchCode != "" && assDtl.AssetType != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.ActiveStatus IN ('A', 'D') AND A.AssetStatus = 'L' AND CAST(A.InstallationDt AS DATE) BETWEEN @FromDate AND @ToDate ";

                    if (assDtl.AssetGroup != "All")
                    {
                        condition += " AND A.AssetGroup= @AssetGroup ";
                    }
                    if (assDtl.BranchCode != "Select Branch")
                    {
                        condition += " AND A.BranchCode = @BranchCode ";
                    }

                    if (assDtl.AssetType != "All")
                    {
                        condition += " AND A.AssetType = @AssetType ";
                    }

                    squery = "SELECT A.ApprovalStatus, A.UniqueId,A.CorpId,A.AssetCode,A.AssetSl,A.AssetName,A.AssetShName,A.AssetDesc,A.BranchCode, BR.BranchName,A.AssetTag,A.AssetType,"
                        + " AST.AssetTypeName,A.AssetGroup, GR.GroupName,A.LocationId,A.DeptId,A.VendorId, A.ManufId,A.CustodianId, "
                        + " CONVERT(NVARCHAR,A.InstallationDt,103) AS 'InstallationDt',CONVERT(NVARCHAR,A.DepStartDt,103) AS 'DepStartDt', "
                        + " A.ProductSlNo,A.ProductQty,A.PurchaseRefNo, A.PurchaseRefDt,A.InvoiceRefNo,CONVERT(NVARCHAR,A.InvoiceRefDt,103) AS 'InvoiceRefDt',A.OtherRefNo, A.OtherRefDt, A.ProductCost,"
                        + " A.WarrantyRef,A.WarrantyStart,A.WarrantyEnd,A.ResidualPer, A.ResidualValue,A.ScrapDocNo,A.ScrapDocDt,A.DispDocNo,A.DispDocDt,A.DispValue,"
                        + " A.OrginalQty,A.QtySlNo,A.OtherInfo,A.WriteOffFlag,A.NoDepFlag,A.AssetStatus,A.ActiveStatus,A.CreatedBy,A.CreatedDate,A.UpdatedBy,A.UpdatedDate, A.ApprovalStatus"
                        + " FROM AssetMaster AS A"
                        + " LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS BR ON A.BranchCode = BR.BranchCode AND A.CorpId = BR.CorpId"
                        + " LEFT JOIN GroupMaster AS GR ON A.AssetGroup = GR.GroupCode AND A.CorpId = GR.CorpId"
                        + " LEFT JOIN AssetType AS AST ON A.AssetType = AST.AssetType AND A.CorpId = AST.CorpId " + condition + " "
                        + " ORDER BY A.ApprovalStatus DESC ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                   
                    cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    
                    cmd.Parameters["@AssetGroup"].Value = assDtl.AssetGroup.Trim();
                    cmd.Parameters["@AssetType"].Value = assDtl.AssetType.Trim();
                    cmd.Parameters["@BranchCode"].Value = assDtl.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(assDtl.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(assDtl.ToDate, objEnglishDate);
                    List<AssetDtl> li = new List<AssetDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AssetDtl assDt = new AssetDtl();

                            assDt.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            assDt.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();

                            assDt.CorpId = dt.Rows[i]["CorpId"].ToString();
                            assDt.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            assDt.AssetSl = dt.Rows[i]["AssetSl"].ToString();
                            assDt.AssetName = dt.Rows[i]["AssetName"].ToString();
                            assDt.AssetShName = dt.Rows[i]["AssetShName"].ToString();

                            assDt.AssetDesc = dt.Rows[i]["AssetDesc"].ToString();
                            assDt.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            assDt.BranchName = dt.Rows[i]["BranchName"].ToString();
                            assDt.AssetTag = dt.Rows[i]["AssetTag"].ToString();
                            assDt.AssetType = dt.Rows[i]["AssetType"].ToString();
                            assDt.AssetTypeName = dt.Rows[i]["AssetTypeName"].ToString();
                            assDt.AssetGroup = dt.Rows[i]["AssetGroup"].ToString();
                            assDt.GroupName = dt.Rows[i]["GroupName"].ToString();

                            assDt.LocationId = dt.Rows[i]["LocationId"].ToString();
                            assDt.DeptId = dt.Rows[i]["DeptId"].ToString();
                            assDt.VendorId = dt.Rows[i]["VendorId"].ToString();
                            assDt.ManufId = dt.Rows[i]["ManufId"].ToString();
                            assDt.CustodianId = dt.Rows[i]["CustodianId"].ToString();

                            assDt.InstallationDt = dt.Rows[i]["InstallationDt"].ToString();
                            assDt.DepStartDt = dt.Rows[i]["DepStartDt"].ToString();
                            assDt.ProductSlNo = dt.Rows[i]["ProductSlNo"].ToString();
                            assDt.ProductQty = dt.Rows[i]["ProductQty"].ToString();
                            assDt.PurchaseRefNo = dt.Rows[i]["PurchaseRefNo"].ToString();

                            assDt.PurchaseRefDt = dt.Rows[i]["PurchaseRefDt"].ToString();
                            assDt.InvoiceRefNo = dt.Rows[i]["InvoiceRefNo"].ToString();
                            assDt.InvoiceRefDt = dt.Rows[i]["InvoiceRefDt"].ToString();
                            assDt.OtherRefNo = dt.Rows[i]["OtherRefNo"].ToString();
                            assDt.OtherRefDt = dt.Rows[i]["OtherRefDt"].ToString();

                            assDt.ProductCost = dt.Rows[i]["ProductCost"].ToString();
                            assDt.WarrantyRef = dt.Rows[i]["WarrantyRef"].ToString();
                            assDt.WarrantyStart = dt.Rows[i]["WarrantyStart"].ToString();
                            assDt.WarrantyEnd = dt.Rows[i]["WarrantyEnd"].ToString();
                            assDt.ResidualPer = dt.Rows[i]["ResidualPer"].ToString();

                            assDt.ResidualValue = dt.Rows[i]["ResidualValue"].ToString();
                            assDt.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            assDt.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            assDt.DispDocNo = dt.Rows[i]["DispDocNo"].ToString();
                            assDt.DispDocDt = dt.Rows[i]["DispDocDt"].ToString();

                            assDt.DispValue = dt.Rows[i]["DispValue"].ToString();
                            assDt.OrginalQty = dt.Rows[i]["OrginalQty"].ToString();
                            assDt.QtySlNo = dt.Rows[i]["QtySlNo"].ToString();
                            assDt.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
                            assDt.WriteOffFlag = dt.Rows[i]["WriteOffFlag"].ToString();

                            assDt.NoDepFlag = dt.Rows[i]["NoDepFlag"].ToString();
                            assDt.AssetStatus = dt.Rows[i]["AssetStatus"].ToString();
                            assDt.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            assDt.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                            assDt.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();

                            assDt.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                            assDt.UpdatedDate = dt.Rows[i]["UpdatedDate"].ToString();
                            assDt.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();

                            li.Add(assDt);
                        }

                        AssetDtlList BoatRate = new AssetDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        AssetDtlString BoatRate = new AssetDtlString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    AssetDtlString Vehicle = new AssetDtlString
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
        /// Modified Abhinaya K
        /// Modified 2023-04-25  
        /// </summary>
        /// <param name="InsScrapHdr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsScrapFilter")]
        public IHttpActionResult InsScrapFilter([FromBody] FA_ScrapDtl InsScrapHdr)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(InsScrapHdr.CorpId) != "" && InsScrapHdr.BranchCode != "" && InsScrapHdr.Input1 != ""
                    && InsScrapHdr.Input2 != "" && InsScrapHdr.Input3 != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.CorpId = @CorpId AND cast(A.ScrapDocDt as DATE) BETWEEN  @FromDate AND @ToDate ";

                    if (InsScrapHdr.BranchCode != "0")
                    {
                        condition += " AND A.BranchCode = @BranchCode ";
                    }
                    if (InsScrapHdr.Input1 != "0")
                    {
                        condition += " AND B.AssetGroup = @AssetGroup ";
                    }

                    if (InsScrapHdr.Input2 != "0")
                    {
                        condition += " AND B.AssetType= @AssetType ";
                    }


                    squery = "SELECT DISTINCT A.CorpId,A.UniqueId,A.ApprovalStatus,A.BranchCode,A.ScrapTransNo,A.ScrapDocNo,CONVERT(NVARCHAR,A.ScrapDocDt,105) AS 'ScrapDocDt',A.Reason, "
                         + " A.ActiveStatus FROM ScrapHdr AS A INNER JOIN AssetMaster AS B ON A.BranchCode = B.BranchCode "
                         + " INNER JOIN ScrapDtl AS C ON B.AssetCode = C.AssetCode AND A.ScrapTransNo = C.ScrapTransNo "
                         + " " + condition + " ORDER BY A.ScrapTransNo ASC,A.ApprovalStatus DESC ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@CorpId"].Value = InsScrapHdr.CorpId.Trim();
                    cmd.Parameters["@AssetGroup"].Value = InsScrapHdr.Input1.Trim();
                    cmd.Parameters["@AssetType"].Value = InsScrapHdr.Input2.Trim();
                    cmd.Parameters["@BranchCode"].Value = InsScrapHdr.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(InsScrapHdr.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(InsScrapHdr.ToDate, objEnglishDate);
                    List<FA_ScrapDtl> li = new List<FA_ScrapDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FA_ScrapDtl lstScrapFilter = new FA_ScrapDtl();

                            lstScrapFilter.CorpId = dt.Rows[i]["CorpId"].ToString();
                            lstScrapFilter.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            lstScrapFilter.ScrapTransNo = dt.Rows[i]["ScrapTransNo"].ToString();
                            lstScrapFilter.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            lstScrapFilter.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            lstScrapFilter.Reason = dt.Rows[i]["Reason"].ToString();
                            lstScrapFilter.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            lstScrapFilter.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            lstScrapFilter.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();
                            li.Add(lstScrapFilter);
                        }

                        FA_ScrapDtlList BoatRate = new FA_ScrapDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        FA_ScrapDtlRes BoatRate = new FA_ScrapDtlRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    FA_ScrapDtlRes Vehicle = new FA_ScrapDtlRes
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
        /// Modified Abhinaya K
        /// Modified 2023-04-25  
        /// </summary>
        /// <param name="DisposalFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("DisposalDetailFilter")]
        public IHttpActionResult DisposalDetailFilter([FromBody] FA_DisposalDtl DisposalFilter)
        {
            try
            {
                string squery = string.Empty;

                string conditions = " WHERE A.CorpId = @CorpId ";

                if (DisposalFilter.BranchCode != "All" && DisposalFilter.BranchCode != "0")
                {
                    conditions = " WHERE A.CorpId = @CorpId  "
                             + " AND A.BranchCode = @BranchCode  ";
                }

                if (DisposalFilter.BranchCode != "All" && DisposalFilter.AssetGroup != "All" && DisposalFilter.BranchCode != "0" && DisposalFilter.AssetGroup != "0")
                {
                    conditions = " WHERE A.CorpId = @CorpId'  AND CAST(A.CreatedDate AS date) = CAST(GetDate() As date) "
                        + " AND A.BranchCode = @BranchCode AND B.AssetGroup = @AssetGroup ";
                }

                if (DisposalFilter.AssetType != "All" && DisposalFilter.BranchCode != "All" && DisposalFilter.AssetGroup != "All" &&
                    DisposalFilter.AssetType != "0" && DisposalFilter.BranchCode != "0" && DisposalFilter.AssetGroup != "0")
                {
                    conditions = " WHERE A.CorpId = @CorpId  AND CAST(A.CreatedDate AS date) = CAST(GetDate() As date) "
                        + "AND A.BranchCode = @BranchCode AND B.AssetGroup = @AssetGroup "
                        + " AND  B.AssetType = @AssetType  ";

                }

                squery = " SELECT DISTINCT A.CorpId,A.UniqueId,A.ApprovalStatus,A.BranchCode,A.DispTransNo,A.DispDocNo,CONVERT(varchar(10), A.DispDocDt, 103) AS 'DispDocDt',A.ActiveStatus FROM DisposalHdr AS A "
                    + " INNER JOIN AssetMaster AS B ON A.BranchCode = B.BranchCode "
                    + " INNER JOIN DisposalDtl AS C ON B.AssetCode = C.AssetCode AND A.DispTransNo = C.DispTransNo "
                    + " " + conditions + " AND cast(A.DispDocDt as DATE) BETWEEN  @FromDate "
                    + "  AND @ToDate ORDER BY A.DispTransNo ASC,A.ApprovalStatus DESC ";


                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@CorpId"].Value = DisposalFilter.CorpId.Trim();
                cmd.Parameters["@AssetGroup"].Value = DisposalFilter.AssetGroup.Trim();
                cmd.Parameters["@AssetType"].Value = DisposalFilter.AssetType.Trim();
                cmd.Parameters["@BranchCode"].Value = DisposalFilter.BranchCode.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(DisposalFilter.FromDate, objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(DisposalFilter.ToDate, objEnglishDate);
                List<FA_DisposalDtl> li = new List<FA_DisposalDtl>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FA_DisposalDtl lstDisposalList = new FA_DisposalDtl();
                        lstDisposalList.CorpId = dt.Rows[i]["CorpId"].ToString();
                        lstDisposalList.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                        lstDisposalList.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        lstDisposalList.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();
                        lstDisposalList.DispTransNo = dt.Rows[i]["DispTransNo"].ToString();
                        lstDisposalList.DispDocNo = dt.Rows[i]["DispDocNo"].ToString();
                        lstDisposalList.DispDocDt = dt.Rows[i]["DispDocDt"].ToString();
                        lstDisposalList.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        li.Add(lstDisposalList);
                    }

                    FA_DisposalDtlList DispLi = new FA_DisposalDtlList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(DispLi);
                }

                else
                {
                    FA_DisposalDtlRes DisRes = new FA_DisposalDtlRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(DisRes);
                }
            }
            catch (Exception ex)
            {
                FA_DisposalDtlRes DisRes = new FA_DisposalDtlRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(DisRes);
            }

        }

        /***********FILTER TRANSFER DETAILS**************/

        /*  2021-06-11  The below API end point alter this API by Abhinaya, Ref karthikeyan sir email, changes in query.*/
        /// <summary>
        /// Modified By Abhinaya K 
        /// Modified Date 30-MAY-2022
        /// Modified Abhinaya K
        /// Modified 2023-04-25  
        /// </summary>
        /// <param name="TransferFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_TransferFilter")]
        public IHttpActionResult TransferFilter([FromBody] FA_CommonMethod TransferFilter)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(TransferFilter.BranchCode) != "")
                {
                    sCondition = " AND A.BranchCode = @BranchCode ";
                }
                if (Convert.ToString(TransferFilter.Input1) != "0")
                {
                    sCondition += " AND C.AssetType = @Input1 ";
                }
                if (Convert.ToString(TransferFilter.Input2) != "0")
                {
                    sCondition += " AND C.AssetGroup = @Input2 ";
                }
                sQuery = "SELECT DISTINCT A.CorpId,A.UniqueId,A.ApprovalStatus,A.BranchCode,A.TransfTransNo, "
                        + " A.TransfDocNo,CONVERT(varchar(10), A.TransfDocDt, 103) AS 'TransfDocDt', "
                        + " B.CorpId,B.AssetCode,B.ToBranchCode,B.ToLocationId,B.ToDeptId,B.ToCustodianId, CONVERT(varchar(10), B.EffectiveDate, 103) AS 'EffectiveDate' "
                        + " FROM TransferHdr AS A  INNER JOIN TransferDtl AS B ON B.BranchCode = A.BranchCode "
                        + " AND B.TransfTransNo = A.TransfTransNo   LEFT JOIN AssetMaster AS C "
                        + "  ON C.AssetCode = B.AssetCode AND C.BranchCode = B.ToBranchCode  "
                        + " WHERE A.CorpId = @CorpId AND CAST(A.TransfDocDt AS DATE) BETWEEN @Input3 AND @Input4 ";

                sQuery = sQuery + sCondition + " ORDER BY A.TransfTransNo ASC,A.ApprovalStatus DESC";

                List<FA_TransferDetails> li = new List<FA_TransferDetails>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Input2", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@Input1", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@Input3", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@Input4", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@CorpId"].Value = TransferFilter.CorpId.Trim();
                cmd.Parameters["@Input2"].Value = TransferFilter.Input2.Trim();
                cmd.Parameters["@Input1"].Value = TransferFilter.Input1.Trim();
                cmd.Parameters["@BranchCode"].Value = TransferFilter.BranchCode.Trim();
                cmd.Parameters["@Input3"].Value = DateTime.Parse(TransferFilter.Input3, objEnglishDate);
                cmd.Parameters["@Input4"].Value = DateTime.Parse(TransferFilter.Input4, objEnglishDate);
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
                FA_TransferDetailsRes ConfRes = new FA_TransferDetailsRes
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

        /***********FILTER GROUP WISE SUMMARY DEPRECIATION DETAILS**************/
        /// <summary>
        /// Modified Abhinaya K
        /// Modified 2023-04-25 
        /// </summary>
        /// <param name="DeprFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_DeprFilter_Old")]
        public IHttpActionResult DeprFilter_Old([FromBody] FA_CommonMethod DeprFilter)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(DeprFilter.BranchCode) != "0")
                {
                    sCondition = " AND A.BranchCode = @BranchCode ";
                }
                if (Convert.ToString(DeprFilter.Input1) != "0")
                {
                    sCondition += " AND B.BranchRegion = @BranchRegion ";
                }

                sQuery = " SELECT A.BranchCode,B.BranchName ,CAST(GroupCode AS NVARCHAR(MAX)) + ' - ' + CAST(GroupName AS NVARCHAR(MAX)) AS 'Group', "
                    + " SUM(OriginalCost) AS 'OriginalCost', SUM(GrossOpening) AS 'GrossOpening', "
                    + " SUM(GrossAddition) AS 'GrossAddition', SUM(GrossDeletion) AS 'GrossDeletion', "
                    + " SUM(GrossTotal) AS 'GrossTotal', SUM(DepOpening) AS 'DepOpening', SUM(DepCurrent) AS 'DepCurrent', SUM(DepTotal) AS 'DepTotal', "
                    + " SUM(AssetOpgValue) AS 'AssetOpgValue', SUM(AssetClgValue) AS 'AssetClgValue' "
                    + " FROM YearlyDepreciation AS A INNER JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS B ON B.BranchCode = A.BranchCode "
                    + " WHERE A.CorpId = @CorpId ";

                sQuery = sQuery + sCondition + " GROUP BY A.BranchCode,B.BranchName, GroupCode,GroupName";

                List<FA_TransferDetails> li = new List<FA_TransferDetails>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@BranchRegion", System.Data.SqlDbType.Int));

                cmd.Parameters["@CorpId"].Value = DeprFilter.CorpId.Trim();
                cmd.Parameters["@BranchCode"].Value = DeprFilter.BranchCode.Trim();
                cmd.Parameters["@BranchRegion"].Value = DeprFilter.Input1.ToString();
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
                FA_TransferDetailsRes ConfRes = new FA_TransferDetailsRes
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

        /***********FILTER GROUP WISE SUMMARY DEPRECIATION DETAILS**************/
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 2022-02-09
        /// Modified Abhinaya K
        /// Modified 2023-04-25 
        /// </summary>
        /// <param name="DeprAssetFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_DeprAssetFilter")]
        public IHttpActionResult DeprAssetFilter([FromBody] FA_CommonMethod DeprAssetFilter)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(DeprAssetFilter.BranchCode) != "0")
                {
                    sCondition = " AND A.BranchCode = @BranchCode ";
                }
                if (Convert.ToString(DeprAssetFilter.Input1) != "0")
                {
                    sCondition += " AND C.AssetGroup = @AssetGroup ";
                }
                if (Convert.ToString(DeprAssetFilter.Input2) != "0")
                {
                    sCondition += " AND A.DepPeriod = @DepPeriod ";
                }

                if (Convert.ToString(DeprAssetFilter.BranchCode) == "0")
                {
                    sCondition += " ORDER BY A.BranchCode ASC";
                }

                sQuery = "SELECT A.BranchCode,B.BranchName, A.AssetCode,A.AssetName, (CONVERT(NVARCHAR(MAX),A.InstallationDt,103) + ' - ' +CONVERT(NVARCHAR(MAX),A.DepStartDt,103) + ' / ' + DeptName) "
                + " AS 'InstallationDt',OriginalCost, GrossTransferIn, GrossTransferOut, GrossAddition, GrossDeletion, DisposalDate, GrossTotal, "
                + " CAST(LifePeriod AS nvarchar) + ' years' AS 'LifePeriod', DepDays, BalLife, DepOpening, DepTransferIn, DepTransferOut, "
                + " DepCurrent, DepTotal, AssetClgValue, AssetResValue, AssetOpgValue, ResSurpAmt "
                + " FROM YearlyDepreciation AS A INNER JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS B ON B.BranchCode = A.BranchCode "
                + " LEFT JOIN AssetMaster AS C ON C.AssetCode = A.AssetCode AND C.BranchCode = A.BranchCode"
                + " WHERE A.CorpId = @CorpId ";

                sQuery = sQuery + sCondition;


                List<FA_TransferDetails> li = new List<FA_TransferDetails>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@DepPeriod", System.Data.SqlDbType.VarChar,7));

                cmd.Parameters["@CorpId"].Value = DeprAssetFilter.CorpId.Trim();
                cmd.Parameters["@AssetGroup"].Value = DeprAssetFilter.Input1.ToString();
                cmd.Parameters["@BranchCode"].Value = DeprAssetFilter.BranchCode.Trim();
                cmd.Parameters["@DepPeriod"].Value = DeprAssetFilter.Input2.ToString();
              
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
                FA_TransferDetailsRes ConfRes = new FA_TransferDetailsRes
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
        /// Modified Abhinaya K
        /// Modified 2023-04-25  
        /// </summary>
        /// <param name="DeprFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_DeprFilter")]
        public IHttpActionResult DeprFilter([FromBody] FA_CommonMethod DeprFilter)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(DeprFilter.BranchCode) != "0")
                {
                    sCondition = " AND A.BranchCode = @BranchCode ";
                }

                if (Convert.ToString(DeprFilter.Input1) != "0")
                {
                    sCondition += " AND B.BranchRegion = @BranchRegion ";
                }

                sQuery = "SELECT A.BranchCode, B.BranchName, SUM(GrossOpening) AS 'GrossOpening', "
                    + " SUM(GrossAddition) + SUM(GrossTransferIn) AS 'GrossAddition', SUM(GrossDeletion) + SUM(GrossTransferOut) AS 'GrossDeletion', "
                    + " SUM(GrossTotal) AS 'GrossTotal', SUM(DepOpening) AS 'DepOpening', SUM(DepCurrent) + SUM(DepTransferIn) AS 'DepCurrent', "
                    + " SUM(DepDeletion) + SUM(DepTransferOut) AS 'DepAdjustment', SUM(DepTotal) AS 'DepTotal', "
                    + " SUM(AssetClgValue) AS 'AssetClgValue', SUM(AssetOpgValue) AS 'AssetOpgValue' "
                    + " FROM YearlyDepreciation AS A INNER JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS B ON B.BranchCode = A.BranchCode"
                    + " WHERE A.CorpId = @CorpId  ";

                sQuery = sQuery + sCondition + "  GROUP BY A.BranchCode, B.BranchName";

                List<FA_TransferDetails> li = new List<FA_TransferDetails>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@BranchRegion", System.Data.SqlDbType.Int));

                cmd.Parameters["@CorpId"].Value = DeprFilter.CorpId.Trim();
                cmd.Parameters["@BranchCode"].Value = DeprFilter.BranchCode.Trim();
                cmd.Parameters["@BranchRegion"].Value = DeprFilter.Input1.ToString();
               
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
                FA_TransferDetailsRes ConfRes = new FA_TransferDetailsRes
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
        [Route("FA_InsAssetMstrEdit")]
        public IHttpActionResult FA_MstrAssetMasterEdit([FromBody] FA_AssetMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.CorpId != "" && InsConfMstr.BranchCode != ""
                    && InsConfMstr.AssetGroup != "" && InsConfMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetMasterEdit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsConfMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", InsConfMstr.AssetCode.Trim());
                    cmd.Parameters.AddWithValue("@AssetSl", InsConfMstr.AssetSl.Trim());
                    cmd.Parameters.AddWithValue("@AssetName", InsConfMstr.AssetName.Trim());
                    cmd.Parameters.AddWithValue("@AssetShName", InsConfMstr.AssetShName.Trim());
                    cmd.Parameters.AddWithValue("@AssetDesc", InsConfMstr.AssetDesc.Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", InsConfMstr.BranchCode.ToString());

                    cmd.Parameters.AddWithValue("@AssetTag", InsConfMstr.AssetTag.Trim());
                    cmd.Parameters.AddWithValue("@AssetType", InsConfMstr.AssetType.Trim());
                    cmd.Parameters.AddWithValue("@AssetGroup", InsConfMstr.AssetGroup.Trim());

                    cmd.Parameters.AddWithValue("@LocationId", InsConfMstr.LocationId.Trim());
                    cmd.Parameters.AddWithValue("@DeptId", InsConfMstr.DeptId.Trim());

                    if (InsConfMstr.VendorId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@VendorId", InsConfMstr.VendorId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@VendorId", System.DBNull.Value);
                    }

                    if (InsConfMstr.ManufId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@ManufId", InsConfMstr.ManufId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ManufId", System.DBNull.Value);
                    }

                    if (InsConfMstr.CustodianId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@CustodianId", InsConfMstr.CustodianId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CustodianId", System.DBNull.Value);
                    }


                    cmd.Parameters.AddWithValue("@InstallationDt", DateTime.Parse(InsConfMstr.InstallationDt.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@DepStartDt", DateTime.Parse(InsConfMstr.DepStartDt.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ProductSlNo", InsConfMstr.ProductSlNo.Trim());
                    cmd.Parameters.AddWithValue("@ProductQty", InsConfMstr.ProductQty.Trim());

                    cmd.Parameters.AddWithValue("@PurchaseRefNo", InsConfMstr.PurchaseRefNo.Trim());

                    if (InsConfMstr.PurchaseRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRefDt", DateTime.Parse(InsConfMstr.PurchaseRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRefDt", System.DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@InvoiceRefNo", InsConfMstr.InvoiceRefNo.Trim());

                    if (InsConfMstr.InvoiceRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceRefDt", DateTime.Parse(InsConfMstr.InvoiceRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceRefDt", System.DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@OtherRefNo", InsConfMstr.OtherRefNo.Trim());

                    if (InsConfMstr.OtherRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@OtherRefDt", DateTime.Parse(InsConfMstr.OtherRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OtherRefDt", System.DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@ProductCost", InsConfMstr.ProductCost.Trim());
                    cmd.Parameters.AddWithValue("@WarrantyRef", InsConfMstr.WarrantyRef.Trim());

                    if (InsConfMstr.WarrantyStart.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@WarrantyStart", DateTime.Parse(InsConfMstr.WarrantyStart.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WarrantyStart", System.DBNull.Value);
                    }
                    if (InsConfMstr.WarrantyEnd.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@WarrantyEnd", DateTime.Parse(InsConfMstr.WarrantyEnd.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WarrantyEnd", System.DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@ResidualPer", InsConfMstr.ResidualPer.Trim());
                    cmd.Parameters.AddWithValue("@ResidualValue", InsConfMstr.ResidualValue.Trim());

                    cmd.Parameters.AddWithValue("@OrginalQty", InsConfMstr.OrginalQty.Trim());
                    cmd.Parameters.AddWithValue("@OtherInfo", InsConfMstr.OtherInfo.Trim());
                    cmd.Parameters.AddWithValue("@WriteOffFlag", InsConfMstr.WriteOffFlag.Trim());
                    cmd.Parameters.AddWithValue("@NoDepFlag", InsConfMstr.NoDepFlag.Trim());
                    cmd.Parameters.AddWithValue("@QtySlNo", InsConfMstr.QtySlNo.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsConfMstr.CreatedBy.Trim());

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
                        FA_LocationMasterRes ConMstr = new FA_LocationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_LocationMasterRes ConMstr = new FA_LocationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_LocationMasterRes Vehicle = new FA_LocationMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_LocationMasterRes ConfRes = new FA_LocationMasterRes
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

        /*Asset Opening Balance*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsAssetOB")]
        public IHttpActionResult FA_InsAssetOB([FromBody] FA_AssetOB Aob)
        {
            try
            {
                if (Aob.QueryType != "" && Aob.CorpId != "" && Aob.BranchCode != "" && Aob.AssetCode != ""
                    && Aob.FinYear != "" && Aob.GrossBlockOB != "" && Aob.DepBlockOB != "" && Aob.NetBlockOB != "" && Aob.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAssetOB", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@UniqueId", Aob.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@QueryType", Aob.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", Aob.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Aob.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@AssetCode", Aob.AssetCode.Trim());

                    cmd.Parameters.AddWithValue("@FinYear", Aob.FinYear.Trim());
                    cmd.Parameters.AddWithValue("@GrossBlockOB", Aob.GrossBlockOB.Trim());
                    cmd.Parameters.AddWithValue("@DepBlockOB", Aob.DepBlockOB.Trim());
                    cmd.Parameters.AddWithValue("@NetBlockOB", Aob.NetBlockOB.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", Aob.CreatedBy.Trim());

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
                        FA_AssetOBString ConMstr = new FA_AssetOBString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_AssetOBString ConMstr = new FA_AssetOBString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_AssetOBString Vehicle = new FA_AssetOBString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_AssetOBString ConfRes = new FA_AssetOBString
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

        /*FA_GetAssetOBDetails*/
        /// <summary>
        /// Modified Abhinaya K
        /// Modified 2023-04-25 
        /// </summary>
        /// <param name="FilterAssetOB"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsAssetOBFilter")]
        public IHttpActionResult InsAssetOBFilter([FromBody] FA_AssetOB FilterAssetOB)
        {
            try
            {
                string squery = string.Empty;

                //if (Convert.ToString(FilterAssetOB.CorpId) != "" && FilterAssetOB.BranchCode != "" && FilterAssetOB.AssetGroup != ""
                //    && FilterAssetOB.AssetType != "" && FilterAssetOB.FinYear != "")
                //{

                string condition = string.Empty;

                condition = " WHERE A.CorpId = @CorpId AND A.FinYear = @FinYear ";

                if (FilterAssetOB.BranchCode != "0")
                {
                    condition += " AND A.BranchCode = @BranchCode ";
                }
                if (FilterAssetOB.AssetGroup != "0")
                {
                    condition += " AND B.AssetGroup = @AssetGroup";
                }

                if (FilterAssetOB.AssetType != "0")
                {
                    condition += " AND B.AssetType=@AssetType";
                }

                squery = " SELECT A.UniqueId,A.FinYear,A.CorpId,A.BranchCode,A.AssetCode,B.AssetName,CAST(A.GrossBlockOB As Decimal(10,2)) as 'GrossBlockOB',CAST(A.DepBlockOB As Decimal(10,2)) as 'DepBlockOB', "
                + " CAST(A.NetBlockOB As Decimal(10, 2)) as 'NetBlockOB' from AssetOB AS A "
                + "  LEFT JOIN AssetMaster AS B ON A.BranchCode = B.BranchCode and A.AssetCode = B.AssetCode "
                + " " + condition + " ";



                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@FinYear", System.Data.SqlDbType.NVarChar,10));
 

                cmd.Parameters["@CorpId"].Value = FilterAssetOB.CorpId.Trim();
                cmd.Parameters["@AssetGroup"].Value = FilterAssetOB.AssetGroup.Trim();
                cmd.Parameters["@AssetType"].Value = FilterAssetOB.AssetType.Trim();
                cmd.Parameters["@BranchCode"].Value = FilterAssetOB.BranchCode.Trim();
                cmd.Parameters["@FinYear"].Value = FilterAssetOB.FinYear.Trim();
               
                List<FA_AssetOB> li = new List<FA_AssetOB>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FA_AssetOB lstAssetObFilter = new FA_AssetOB();

                        lstAssetObFilter.CorpId = dt.Rows[i]["CorpId"].ToString();
                        lstAssetObFilter.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                        lstAssetObFilter.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                        lstAssetObFilter.AssetName = dt.Rows[i]["AssetName"].ToString();
                        lstAssetObFilter.GrossBlockOB = dt.Rows[i]["GrossBlockOB"].ToString();
                        lstAssetObFilter.NetBlockOB = dt.Rows[i]["NetBlockOB"].ToString();
                        lstAssetObFilter.DepBlockOB = dt.Rows[i]["DepBlockOB"].ToString();
                        lstAssetObFilter.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        lstAssetObFilter.FinYear = dt.Rows[i]["FinYear"].ToString();

                        li.Add(lstAssetObFilter);
                    }

                    FA_AssetOBList BoatRate = new FA_AssetOBList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }
                else
                {
                    FA_AssetOBRes BoatRate = new FA_AssetOBRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatRate);
                }
                // }
                //else
                //{
                //    FA_AssetOBRes Vehicle = new FA_AssetOBRes
                //    {
                //        Response = "Must Pass All Parameters",
                //        StatusCode = 0
                //    };
                //    return Ok(Vehicle);
                //}
            }
            catch (Exception ex)
            {
                FA_AssetOBRes ConfRes = new FA_AssetOBRes
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

        //View Asset Depreciation
        /// <summary>
        /// Modified By : Vediyappan.V
        /// Modified Date : 2022-02-09
        /// Modified Abhinaya K
        /// Modified 2023-04-25 
        /// </summary>
        /// <param name="flDeprList"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_AssetDepFilter")]
        public IHttpActionResult AssetDeprecFilter([FromBody] FA_DepreciationList flDeprList)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (flDeprList.CorpId != "" && flDeprList.FinYear != "")
                {
                    sCondition = " WHERE A.CorpId = @CorpId AND A.DepPeriod = @DepPeriod ";

                    if (flDeprList.BranchCode != "")
                    {
                        sCondition += " AND A.BranchCode = @BranchCode ";
                    }
                    if (flDeprList.AssetGroupCode != "")
                    {
                        sCondition += " AND A.GroupCode = @AssetGroupCode ";
                    }

                    if (flDeprList.AssetType != "")
                    {
                        sCondition += " AND C.AssetType= @AssetType ";
                    }

                    if (flDeprList.BranchCode == "")
                    {
                        sCondition += " ORDER BY A.BranchCode ASC ";
                    }

                    sQuery = " SELECT A.CorpId, A.DepPeriod, A.BranchCode, A.BranchName, A.GroupCode, A.GroupName, C.AssetType, C.AssetTypeName, "
                            + " A.AssetCode, A.AssetName, CAST(A.DepRate AS DECIMAL(18,4)) 'DepRate',CONVERT(varchar, CAST(A.DepCurrent AS MONEY), 1) As 'DepCurrent', "
                            + " CONVERT(varchar, CAST(A.AssetResValue AS MONEY), 1) As 'AssetResValue' FROM YearlyDepreciation AS A "
                            + " INNER JOIN AssetMaster AS B ON A.GroupCode = B.AssetGroup AND A.AssetCode = B.AssetCode AND "
                            + " A.ActiveStatus = B.ActiveStatus AND A.CorpId = B.CorpId "
                            + " INNER JOIN AssetType AS C ON B.AssetType = C.AssetType AND A.GroupCode = C.GroupCode "
                            + " AND A.ActiveStatus = C.ActiveStatus AND A.CorpId = C.CorpId "
                            + " " + sCondition + " ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@AssetGroupCode", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@DepPeriod", System.Data.SqlDbType.VarChar, 7));


                    cmd.Parameters["@CorpId"].Value = flDeprList.CorpId.Trim();
                    cmd.Parameters["@AssetGroupCode"].Value = flDeprList.AssetGroupCode.Trim();
                    cmd.Parameters["@AssetType"].Value = flDeprList.AssetType.Trim();
                    cmd.Parameters["@BranchCode"].Value = flDeprList.BranchCode.Trim();
                    cmd.Parameters["@DepPeriod"].Value = flDeprList.FinYear.Trim();
                    
                    List<FA_DepreciationList> li = new List<FA_DepreciationList>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FA_DepreciationList lstAssetObFilter = new FA_DepreciationList();

                            lstAssetObFilter.CorpId = dt.Rows[i]["CorpId"].ToString();
                            lstAssetObFilter.FinYear = dt.Rows[i]["DepPeriod"].ToString();
                            lstAssetObFilter.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            lstAssetObFilter.BranchName = dt.Rows[i]["BranchName"].ToString();
                            lstAssetObFilter.AssetGroupCode = dt.Rows[i]["GroupCode"].ToString();
                            lstAssetObFilter.AssetGroupName = dt.Rows[i]["GroupName"].ToString();
                            lstAssetObFilter.AssetType = dt.Rows[i]["AssetType"].ToString();
                            lstAssetObFilter.AssetTypeName = dt.Rows[i]["AssetTypeName"].ToString();
                            lstAssetObFilter.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            lstAssetObFilter.AssetName = dt.Rows[i]["AssetName"].ToString();
                            lstAssetObFilter.DepRate = dt.Rows[i]["DepRate"].ToString();
                            lstAssetObFilter.DepCurrent = dt.Rows[i]["DepCurrent"].ToString();
                            lstAssetObFilter.AssetResValue = dt.Rows[i]["AssetResValue"].ToString();

                            li.Add(lstAssetObFilter);
                        }

                        FA_DepreciationListlst BoatRate = new FA_DepreciationListlst
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        FA_DepreciationListRes BoatRate = new FA_DepreciationListRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    FA_DepreciationListRes Vehicle = new FA_DepreciationListRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_DepreciationListRes ConfRes = new FA_DepreciationListRes
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

        /*DepreciationDelete*/
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_DepreciationCompleted")]
        public IHttpActionResult InsertDelete([FromBody] FA_DepreciationDelete DepreDelete)
        {
            try
            {
                if (DepreDelete.QueryType != "" && DepreDelete.UserId != "" && DepreDelete.BranchCode != "" && DepreDelete.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("spDepCalStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", DepreDelete.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UserId", DepreDelete.UserId.ToString());
                    cmd.Parameters.AddWithValue("@UserName", DepreDelete.UserName.ToString());
                    cmd.Parameters.AddWithValue("@DepPeriod", DepreDelete.FinancialYear.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", DepreDelete.CreatedBy.Trim());

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
                        FA_DepreciationDeleteString ConMstr = new FA_DepreciationDeleteString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_DepreciationDeleteString ConMstr = new FA_DepreciationDeleteString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_DepreciationDeleteString Vehicle = new FA_DepreciationDeleteString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_DepreciationDeleteString ConfRes = new FA_DepreciationDeleteString
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
        [Route("FA_DepreciationDelete")]
        public IHttpActionResult Delete([FromBody] FA_DepreciationDelete DepreDelete)
        {
            try
            {
                if (DepreDelete.QueryType != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("SP_DepreciationDeleteDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", DepreDelete.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UserId", DepreDelete.UserId.ToString());
                    cmd.Parameters.AddWithValue("@UserName", DepreDelete.UserName.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", DepreDelete.BranchCode.Trim());
                    cmd.Parameters.AddWithValue("@BranchName", DepreDelete.BranchName.Trim());
                    cmd.Parameters.AddWithValue("@DepPeriod", DepreDelete.FinancialYear.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", DepreDelete.CreatedBy.Trim());

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
                        FA_DepreciationDeleteString ConMstr = new FA_DepreciationDeleteString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_DepreciationDeleteString ConMstr = new FA_DepreciationDeleteString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_DepreciationDeleteString Vehicle = new FA_DepreciationDeleteString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_DepreciationDeleteString ConfRes = new FA_DepreciationDeleteString
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
        [Route("FA_ChangeAssetMaster")]
        public IHttpActionResult FA_ChangeAssetMaster([FromBody] FA_AssetMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.CorpId != "" && InsConfMstr.BranchCode != ""
                    && InsConfMstr.AssetGroup != "" && InsConfMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("ChangeAssetMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsConfMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@OldAssetCode", InsConfMstr.OldAssetCode.Trim());
                    cmd.Parameters.AddWithValue("@AssetCode", InsConfMstr.AssetCode.Trim());
                    cmd.Parameters.AddWithValue("@AssetSl", InsConfMstr.AssetSl.Trim());
                    cmd.Parameters.AddWithValue("@AssetName", InsConfMstr.AssetName.Trim());
                    cmd.Parameters.AddWithValue("@AssetShName", InsConfMstr.AssetShName.Trim());
                    cmd.Parameters.AddWithValue("@AssetDesc", InsConfMstr.AssetDesc.Trim());
                    cmd.Parameters.AddWithValue("@BranchCode", InsConfMstr.BranchCode.ToString());

                    cmd.Parameters.AddWithValue("@AssetTag", InsConfMstr.AssetTag.Trim());
                    cmd.Parameters.AddWithValue("@AssetType", InsConfMstr.AssetType.Trim());
                    cmd.Parameters.AddWithValue("@AssetGroup", InsConfMstr.AssetGroup.Trim());

                    cmd.Parameters.AddWithValue("@LocationId", InsConfMstr.LocationId.Trim());
                    cmd.Parameters.AddWithValue("@DeptId", InsConfMstr.DeptId.Trim());

                    if (InsConfMstr.VendorId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@VendorId", InsConfMstr.VendorId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@VendorId", System.DBNull.Value);
                    }

                    if (InsConfMstr.ManufId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@ManufId", InsConfMstr.ManufId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ManufId", System.DBNull.Value);
                    }

                    if (InsConfMstr.CustodianId.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@CustodianId", InsConfMstr.CustodianId.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CustodianId", System.DBNull.Value);
                    }


                    cmd.Parameters.AddWithValue("@InstallationDt", DateTime.Parse(InsConfMstr.InstallationDt.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@DepStartDt", DateTime.Parse(InsConfMstr.DepStartDt.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@ProductSlNo", InsConfMstr.ProductSlNo.Trim());
                    cmd.Parameters.AddWithValue("@ProductQty", InsConfMstr.ProductQty.Trim());

                    cmd.Parameters.AddWithValue("@PurchaseRefNo", InsConfMstr.PurchaseRefNo.Trim());

                    if (InsConfMstr.PurchaseRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRefDt", DateTime.Parse(InsConfMstr.PurchaseRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRefDt", System.DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@InvoiceRefNo", InsConfMstr.InvoiceRefNo.Trim());

                    if (InsConfMstr.InvoiceRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@InvoiceRefDt", DateTime.Parse(InsConfMstr.InvoiceRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceRefDt", System.DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@OtherRefNo", InsConfMstr.OtherRefNo.Trim());

                    if (InsConfMstr.OtherRefDt.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@OtherRefDt", DateTime.Parse(InsConfMstr.OtherRefDt.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OtherRefDt", System.DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@ProductCost", InsConfMstr.ProductCost.Trim());
                    cmd.Parameters.AddWithValue("@WarrantyRef", InsConfMstr.WarrantyRef.Trim());

                    if (InsConfMstr.WarrantyStart.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@WarrantyStart", DateTime.Parse(InsConfMstr.WarrantyStart.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WarrantyStart", System.DBNull.Value);
                    }
                    if (InsConfMstr.WarrantyEnd.Trim() != "")
                    {
                        cmd.Parameters.AddWithValue("@WarrantyEnd", DateTime.Parse(InsConfMstr.WarrantyEnd.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WarrantyEnd", System.DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@ResidualPer", InsConfMstr.ResidualPer.Trim());
                    cmd.Parameters.AddWithValue("@ResidualValue", InsConfMstr.ResidualValue.Trim());

                    cmd.Parameters.AddWithValue("@OrginalQty", InsConfMstr.OrginalQty.Trim());
                    cmd.Parameters.AddWithValue("@OtherInfo", InsConfMstr.OtherInfo.Trim());
                    cmd.Parameters.AddWithValue("@WriteOffFlag", InsConfMstr.WriteOffFlag.Trim());
                    cmd.Parameters.AddWithValue("@NoDepFlag", InsConfMstr.NoDepFlag.Trim());
                    cmd.Parameters.AddWithValue("@QtySlNo", InsConfMstr.QtySlNo.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsConfMstr.CreatedBy.Trim());

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
                        FA_LocationMasterRes ConMstr = new FA_LocationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_LocationMasterRes ConMstr = new FA_LocationMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_LocationMasterRes Vehicle = new FA_LocationMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_LocationMasterRes ConfRes = new FA_LocationMasterRes
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
        /// Modified Abhinaya K
        /// Modified 2023-04-25
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Removed 1=1 in where condition , seemed to be not used 
        /// </summary>
        /// <param name="assDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_NewAssetRequest")]
        public IHttpActionResult newAssetRequest([FromBody] AssetDtl assDtl)
        {
            try
            {
                string squery = string.Empty;

                if (assDtl.AssetGroup != "" && assDtl.BranchCode != "" && assDtl.AssetType != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.ActiveStatus IN ('A', 'D') AND A.AssetStatus = 'L' "
                                + " AND CAST(A.InstallationDt AS DATE) BETWEEN @FromDate AND @ToDate "
                                + " AND A.BranchCode = @BranchCode  "
                                + " AND A.ApprovalStatus='P'";



                    squery = " SELECT A.UniqueId,A.CorpId,A.AssetCode,A.AssetSl,A.AssetName,A.AssetShName,A.AssetDesc,A.BranchCode, "
                            + " BR.BranchName,A.AssetTag,A.AssetType,"
                            + " AST.AssetTypeName,A.AssetGroup, GR.GroupName,A.LocationId,A.DeptId,A.VendorId, "
                            + " A.ManufId,A.CustodianId, "
                            + " CONVERT(NVARCHAR,A.InstallationDt,103) AS 'InstallationDt', "
                            + " CONVERT(NVARCHAR,A.DepStartDt,103) AS 'DepStartDt', "
                            + " A.ProductSlNo,A.ProductQty,A.PurchaseRefNo, A.PurchaseRefDt,A.InvoiceRefNo, "
                            + " CONVERT(NVARCHAR,A.InvoiceRefDt,103) AS 'InvoiceRefDt',A.OtherRefNo, A.OtherRefDt, A.ProductCost,"
                            + " A.WarrantyRef,A.WarrantyStart,A.WarrantyEnd,A.ResidualPer, A.ResidualValue,A.ScrapDocNo, "
                            + " A.ScrapDocDt,A.DispDocNo,A.DispDocDt,A.DispValue,"
                            + " A.OrginalQty,A.QtySlNo,A.OtherInfo,A.WriteOffFlag,A.NoDepFlag,A.AssetStatus,A.ActiveStatus, "
                            + " A.CreatedBy,A.CreatedDate,A.UpdatedBy,A.UpdatedDate"
                            + " FROM AssetMaster AS A"
                            + " LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS BR ON A.BranchCode = BR.BranchCode AND A.CorpId = BR.CorpId"
                            + " LEFT JOIN GroupMaster AS GR ON A.AssetGroup = GR.GroupCode AND A.CorpId = GR.CorpId"
                            + " LEFT JOIN AssetType AS AST ON A.AssetType = AST.AssetType AND A.CorpId = AST.CorpId " + condition + "";

                    SqlCommand cmd = new SqlCommand(squery, con);
                  
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                 
                    cmd.Parameters["@BranchCode"].Value = assDtl.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(assDtl.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(assDtl.ToDate, objEnglishDate);
                    cmd.CommandTimeout = 10000000;
                    List<AssetDtl> li = new List<AssetDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AssetDtl assDt = new AssetDtl();

                            assDt.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            assDt.CorpId = dt.Rows[i]["CorpId"].ToString();
                            assDt.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            assDt.AssetSl = dt.Rows[i]["AssetSl"].ToString();
                            assDt.AssetName = dt.Rows[i]["AssetName"].ToString();
                            assDt.AssetShName = dt.Rows[i]["AssetShName"].ToString();

                            assDt.AssetDesc = dt.Rows[i]["AssetDesc"].ToString();
                            assDt.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            assDt.BranchName = dt.Rows[i]["BranchName"].ToString();
                            assDt.AssetTag = dt.Rows[i]["AssetTag"].ToString();
                            assDt.AssetType = dt.Rows[i]["AssetType"].ToString();
                            assDt.AssetTypeName = dt.Rows[i]["AssetTypeName"].ToString();
                            assDt.AssetGroup = dt.Rows[i]["AssetGroup"].ToString();
                            assDt.GroupName = dt.Rows[i]["GroupName"].ToString();

                            assDt.LocationId = dt.Rows[i]["LocationId"].ToString();
                            assDt.DeptId = dt.Rows[i]["DeptId"].ToString();
                            assDt.VendorId = dt.Rows[i]["VendorId"].ToString();
                            assDt.ManufId = dt.Rows[i]["ManufId"].ToString();
                            assDt.CustodianId = dt.Rows[i]["CustodianId"].ToString();

                            assDt.InstallationDt = dt.Rows[i]["InstallationDt"].ToString();
                            assDt.DepStartDt = dt.Rows[i]["DepStartDt"].ToString();
                            assDt.ProductSlNo = dt.Rows[i]["ProductSlNo"].ToString();
                            assDt.ProductQty = dt.Rows[i]["ProductQty"].ToString();
                            assDt.PurchaseRefNo = dt.Rows[i]["PurchaseRefNo"].ToString();

                            assDt.PurchaseRefDt = dt.Rows[i]["PurchaseRefDt"].ToString();
                            assDt.InvoiceRefNo = dt.Rows[i]["InvoiceRefNo"].ToString();
                            assDt.InvoiceRefDt = dt.Rows[i]["InvoiceRefDt"].ToString();
                            assDt.OtherRefNo = dt.Rows[i]["OtherRefNo"].ToString();
                            assDt.OtherRefDt = dt.Rows[i]["OtherRefDt"].ToString();

                            assDt.ProductCost = dt.Rows[i]["ProductCost"].ToString();
                            assDt.WarrantyRef = dt.Rows[i]["WarrantyRef"].ToString();
                            assDt.WarrantyStart = dt.Rows[i]["WarrantyStart"].ToString();
                            assDt.WarrantyEnd = dt.Rows[i]["WarrantyEnd"].ToString();
                            assDt.ResidualPer = dt.Rows[i]["ResidualPer"].ToString();

                            assDt.ResidualValue = dt.Rows[i]["ResidualValue"].ToString();
                            assDt.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            assDt.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            assDt.DispDocNo = dt.Rows[i]["DispDocNo"].ToString();
                            assDt.DispDocDt = dt.Rows[i]["DispDocDt"].ToString();

                            assDt.DispValue = dt.Rows[i]["DispValue"].ToString();
                            assDt.OrginalQty = dt.Rows[i]["OrginalQty"].ToString();
                            assDt.QtySlNo = dt.Rows[i]["QtySlNo"].ToString();
                            assDt.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
                            assDt.WriteOffFlag = dt.Rows[i]["WriteOffFlag"].ToString();

                            assDt.NoDepFlag = dt.Rows[i]["NoDepFlag"].ToString();
                            assDt.AssetStatus = dt.Rows[i]["AssetStatus"].ToString();
                            assDt.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            assDt.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                            assDt.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();

                            assDt.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                            assDt.UpdatedDate = dt.Rows[i]["UpdatedDate"].ToString();

                            li.Add(assDt);
                        }

                        AssetDtlList BoatRate = new AssetDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        AssetDtlString BoatRate = new AssetDtlString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    AssetDtlString Vehicle = new AssetDtlString
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
        /// Modified By Silambarasu
        /// modified date 01JUN2022
        /// Modified Abhinaya K
        /// Modified 2023-04-25
        /// Modified Imran K
        /// Modified 27-Apr-2023
        /// </summary>
        /// <param name="Asset"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_UpdateNewAssetRequest")]
        public IHttpActionResult GetReprintReason([FromBody] AssetDtl Asset)
        {
            try
            {
                string sQuery = string.Empty;
                string sQuery1 = string.Empty;
                int No = 0;
              
                string[] id;
                string Unique = string.Empty;
                id = Asset.UniqueId.Split(',');
                for (int i = 0; i < id.Count(); i++)
                {
                    int UniqueId = Convert.ToInt32(id[i].Replace("'", ""));
                    if (Asset.ApprovalStatus.ToString().Trim() == "R")
                    {
                        sQuery1 = "UPDATE AssetMaster SET ApprovalStatus =@ApprovalStatus, "
                          + " UpdatedBy=@UpdatedBy, UpdatedDate=GETDATE(), ActiveStatus = 'D' "
                          + " WHERE UniqueId =@UniqueId AND BranchCode=@BranchCode "
                          + " AND CAST(InstallationDt AS DATE) BETWEEN @FromDate AND @ToDate ";

                        SqlCommand cmd1 = new SqlCommand(sQuery1, con);
                        cmd1.Parameters.Add(new SqlParameter("@UniqueId", System.Data.SqlDbType.Int));
                        cmd1.Parameters.Add(new SqlParameter("@UpdatedBy", System.Data.SqlDbType.Int));
                        cmd1.Parameters.Add(new SqlParameter("@ApprovalStatus", System.Data.SqlDbType.Char, 1));
                        cmd1.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                        cmd1.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                        cmd1.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));


                        cmd1.Parameters["@UniqueId"].Value = UniqueId;
                        cmd1.Parameters["@UpdatedBy"].Value = Asset.UpdatedBy.Trim();
                        cmd1.Parameters["@ApprovalStatus"].Value = Asset.ApprovalStatus.Trim();
                        cmd1.Parameters["@BranchCode"].Value = Asset.BranchCode.Trim();
                        cmd1.Parameters["@FromDate"].Value = DateTime.Parse(Asset.FromDate, objEnglishDate);
                        cmd1.Parameters["@ToDate"].Value = DateTime.Parse(Asset.ToDate, objEnglishDate);
                        con.Open();
                        int No1 = cmd1.ExecuteNonQuery();
                        con.Close();

                        if (No1 > 0)
                        {
                            sQuery = "UPDATE AssetLocation SET ActiveStatus = 'D', UpdatedBy=@UpdatedBy, "
                          + " UpdatedDate=GETDATE() WHERE BranchCode=@BranchCode "
                          + " AND AssetCode IN (Select AssetCode from AssetMaster "
                          + " WHERE UniqueId =@UniqueId AND BranchCode=@BranchCode "
                          + " AND CAST(InstallationDt AS DATE) BETWEEN @FromDate AND @ToDate )";
                        }
                        else
                        {
                            AssetDtlString ConfRes = new AssetDtlString
                            {
                                Response = "Asset Details Was Not Rejected",
                                StatusCode = 0
                            };
                            return Ok(ConfRes);
                        }
                    }
                    else
                    {
                        sQuery = "UPDATE AssetMaster SET ApprovalStatus =@ApprovalStatus, "
                          + " UpdatedBy=@UpdatedBy, UpdatedDate=GETDATE() WHERE "
                          + " UniqueId =@UniqueId AND BranchCode=@BranchCode "
                          + " AND CAST(InstallationDt AS DATE) BETWEEN @FromDate AND @ToDate ";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@UniqueId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ApprovalStatus", System.Data.SqlDbType.Char, 1));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));


                    cmd.Parameters["@UniqueId"].Value = UniqueId;
                    cmd.Parameters["@UpdatedBy"].Value = Asset.UpdatedBy.Trim();
                    cmd.Parameters["@ApprovalStatus"].Value = Asset.ApprovalStatus.Trim();
                    cmd.Parameters["@BranchCode"].Value = Asset.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Asset.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Asset.ToDate, objEnglishDate);
                    con.Open();
                     No = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (No > 0)
                {
                    AssetDtlString ConfList = new AssetDtlString
                    {
                        Response = "Updated Successfully",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    AssetDtlString ConfRes = new AssetDtlString
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

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("FA_UpdateNewAssetRequest")]
        //public IHttpActionResult GetReprintReason([FromBody] AssetDtl Asset)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        sQuery = "UPDATE AssetMaster SET ApprovalStatus ='A', UpdatedBy='" + Asset.UpdatedBy.ToString() + "', UpdatedDate=GETDATE() WHERE "
        //               + "UniqueId IN (" + Asset.UniqueId.Trim() + ") AND "
        //               + "BranchCode='" + Asset.BranchCode.Trim() + "' "
        //               + " AND CAST(InstallationDt AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(Asset.FromDate, objEnglishDate) + "' AND "
        //               + " '" + DateTime.Parse(Asset.ToDate, objEnglishDate) + "' ";

        //        SqlCommand cmd = new SqlCommand(sQuery, con);

        //        con.Open();
        //        int No = cmd.ExecuteNonQuery();
        //        con.Close();

        //        if (No > 0)
        //        {
        //            AssetDtlString ConfList = new AssetDtlString
        //            {
        //                Response = "Updated Successfully",
        //                StatusCode = 1
        //            };
        //            return Ok(ConfList);
        //        }

        //        else
        //        {
        //            AssetDtlString ConfRes = new AssetDtlString
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
        /// Modified Abhinaya K
        /// Modified 2023-04-25
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Removed 1=1 in where condition , seemed to be not used 
        /// </summary>
        /// <param name="assDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_EditGetAssetDetails")]
        public IHttpActionResult EditGetAssetDetails([FromBody] AssetDtl assDtl)
        {
            try
            {
                string squery = string.Empty;

                if (assDtl.AssetGroup != "" && assDtl.BranchCode != "" && assDtl.AssetType != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.ActiveStatus IN ('A', 'D') AND A.AssetStatus = 'L' AND CAST(A.InstallationDt AS DATE) BETWEEN @FromDate AND @ToDate AND A.ApprovalStatus='P'";

                    if (assDtl.AssetGroup != "All")
                    {
                        condition += " AND A.AssetGroup= @AssetGroup ";
                    }
                    if (assDtl.BranchCode != "Select Branch")
                    {
                        condition += " AND A.BranchCode = @BranchCode ";
                    }

                    if (assDtl.AssetType != "All")
                    {
                        condition += " AND A.AssetType = @AssetType ";
                    }

                    squery = "SELECT A.CorpId,A.AssetCode,A.AssetSl,A.AssetName,A.AssetShName,A.AssetDesc,A.BranchCode, BR.BranchName,A.AssetTag,A.AssetType,"
                        + " AST.AssetTypeName,A.AssetGroup, GR.GroupName,A.LocationId,A.DeptId,A.VendorId, A.ManufId,A.CustodianId, "
                        + " CONVERT(NVARCHAR,A.InstallationDt,103) AS 'InstallationDt',CONVERT(NVARCHAR,A.DepStartDt,103) AS 'DepStartDt', "
                        + " A.ProductSlNo,A.ProductQty,A.PurchaseRefNo, A.PurchaseRefDt,A.InvoiceRefNo,CONVERT(NVARCHAR,A.InvoiceRefDt,103) AS 'InvoiceRefDt',A.OtherRefNo, A.OtherRefDt, A.ProductCost,"
                        + " A.WarrantyRef,A.WarrantyStart,A.WarrantyEnd,A.ResidualPer, A.ResidualValue,A.ScrapDocNo,A.ScrapDocDt,A.DispDocNo,A.DispDocDt,A.DispValue,"
                        + " A.OrginalQty,A.QtySlNo,A.OtherInfo,A.WriteOffFlag,A.NoDepFlag,A.AssetStatus,A.ActiveStatus,A.CreatedBy,A.CreatedDate,A.UpdatedBy,A.UpdatedDate, A.ApprovalStatus"
                        + " FROM AssetMaster AS A"
                        + " LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS BR ON A.BranchCode = BR.BranchCode AND A.CorpId = BR.CorpId"
                        + " LEFT JOIN GroupMaster AS GR ON A.AssetGroup = GR.GroupCode AND A.CorpId = GR.CorpId"
                        + " LEFT JOIN AssetType AS AST ON A.AssetType = AST.AssetType AND A.CorpId = AST.CorpId " + condition + "";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    
                    cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@AssetGroup"].Value = assDtl.AssetGroup.Trim();
                    cmd.Parameters["@AssetType"].Value = assDtl.AssetType.Trim();
                    cmd.Parameters["@BranchCode"].Value = assDtl.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(assDtl.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(assDtl.ToDate, objEnglishDate);
                    cmd.CommandTimeout = 900000;
                    List<AssetDtl> li = new List<AssetDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AssetDtl assDt = new AssetDtl();

                            assDt.CorpId = dt.Rows[i]["CorpId"].ToString();
                            assDt.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            assDt.AssetSl = dt.Rows[i]["AssetSl"].ToString();
                            assDt.AssetName = dt.Rows[i]["AssetName"].ToString();
                            assDt.AssetShName = dt.Rows[i]["AssetShName"].ToString();

                            assDt.AssetDesc = dt.Rows[i]["AssetDesc"].ToString();
                            assDt.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            assDt.BranchName = dt.Rows[i]["BranchName"].ToString();
                            assDt.AssetTag = dt.Rows[i]["AssetTag"].ToString();
                            assDt.AssetType = dt.Rows[i]["AssetType"].ToString();
                            assDt.AssetTypeName = dt.Rows[i]["AssetTypeName"].ToString();
                            assDt.AssetGroup = dt.Rows[i]["AssetGroup"].ToString();
                            assDt.GroupName = dt.Rows[i]["GroupName"].ToString();

                            assDt.LocationId = dt.Rows[i]["LocationId"].ToString();
                            assDt.DeptId = dt.Rows[i]["DeptId"].ToString();
                            assDt.VendorId = dt.Rows[i]["VendorId"].ToString();
                            assDt.ManufId = dt.Rows[i]["ManufId"].ToString();
                            assDt.CustodianId = dt.Rows[i]["CustodianId"].ToString();

                            assDt.InstallationDt = dt.Rows[i]["InstallationDt"].ToString();
                            assDt.DepStartDt = dt.Rows[i]["DepStartDt"].ToString();
                            assDt.ProductSlNo = dt.Rows[i]["ProductSlNo"].ToString();
                            assDt.ProductQty = dt.Rows[i]["ProductQty"].ToString();
                            assDt.PurchaseRefNo = dt.Rows[i]["PurchaseRefNo"].ToString();

                            assDt.PurchaseRefDt = dt.Rows[i]["PurchaseRefDt"].ToString();
                            assDt.InvoiceRefNo = dt.Rows[i]["InvoiceRefNo"].ToString();
                            assDt.InvoiceRefDt = dt.Rows[i]["InvoiceRefDt"].ToString();
                            assDt.OtherRefNo = dt.Rows[i]["OtherRefNo"].ToString();
                            assDt.OtherRefDt = dt.Rows[i]["OtherRefDt"].ToString();

                            assDt.ProductCost = dt.Rows[i]["ProductCost"].ToString();
                            assDt.WarrantyRef = dt.Rows[i]["WarrantyRef"].ToString();
                            assDt.WarrantyStart = dt.Rows[i]["WarrantyStart"].ToString();
                            assDt.WarrantyEnd = dt.Rows[i]["WarrantyEnd"].ToString();
                            assDt.ResidualPer = dt.Rows[i]["ResidualPer"].ToString();

                            assDt.ResidualValue = dt.Rows[i]["ResidualValue"].ToString();
                            assDt.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            assDt.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            assDt.DispDocNo = dt.Rows[i]["DispDocNo"].ToString();
                            assDt.DispDocDt = dt.Rows[i]["DispDocDt"].ToString();

                            assDt.DispValue = dt.Rows[i]["DispValue"].ToString();
                            assDt.OrginalQty = dt.Rows[i]["OrginalQty"].ToString();
                            assDt.QtySlNo = dt.Rows[i]["QtySlNo"].ToString();
                            assDt.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
                            assDt.WriteOffFlag = dt.Rows[i]["WriteOffFlag"].ToString();

                            assDt.NoDepFlag = dt.Rows[i]["NoDepFlag"].ToString();
                            assDt.AssetStatus = dt.Rows[i]["AssetStatus"].ToString();
                            assDt.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            assDt.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                            assDt.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();

                            assDt.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                            assDt.UpdatedDate = dt.Rows[i]["UpdatedDate"].ToString();
                            assDt.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();

                            li.Add(assDt);
                        }

                        AssetDtlList BoatRate = new AssetDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        AssetDtlString BoatRate = new AssetDtlString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    AssetDtlString Vehicle = new AssetDtlString
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
        /// Modified Abhinaya K
        /// Modified 2023-04-25
        /// Modified Imran K
        /// Modified 27-Apr-2023
        /// </summary>
        /// <param name="Asset"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_UpdateTransferHeader")]
        public IHttpActionResult UpdateTransferHeader([FromBody] AssetDtl Asset)
        {
            try
            {
                int No = 0;
                string sQuery = string.Empty;
                string[] id;
                string Unique = string.Empty;
                id = Asset.UniqueId.Split(',');
                for (int i = 0; i < id.Count(); i++)
                {
                    int UniqueId = Convert.ToInt32(id[i].Replace("'", ""));
                    if (Asset.ApprovalStatus.Trim() == "A")
                    {

                        sQuery = "UPDATE TransferHdr SET ApprovalStatus = @ApprovalStatus, UpdatedBy= @UpdatedBy, "
                               + "UpdatedDate=GETDATE(),ApprovedBy= @UpdatedBy ,ApprovedDate =GETDATE() WHERE "
                               + "UniqueId = @UniqueId AND "
                               + "BranchCode=@BranchCode "
                               + " AND CAST(TransfDocDt AS DATE) BETWEEN @FromDate AND @ToDate ";

                    }
                    else if (Asset.ApprovalStatus.Trim() == "R")
                    {
                        sQuery = "UPDATE TransferHdr SET ApprovalStatus = @ApprovalStatus, UpdatedBy= @UpdatedBy, "
                             + "UpdatedDate=GETDATE(),ApprovedBy=@UpdatedBy,ApprovedDate =GETDATE() ,ActiveStatus='D' WHERE "
                             + "UniqueId = @UniqueId AND "
                             + "BranchCode=@BranchCode "
                             + " AND CAST(TransfDocDt AS DATE) BETWEEN @FromDate AND @ToDate ";
                    }
                    else
                    {

                    }
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@UniqueId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ApprovalStatus", System.Data.SqlDbType.Char, 1));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));


                    cmd.Parameters["@UniqueId"].Value = UniqueId;
                    cmd.Parameters["@UpdatedBy"].Value = Asset.UpdatedBy.Trim();
                    cmd.Parameters["@ApprovalStatus"].Value = Asset.ApprovalStatus.Trim();
                    cmd.Parameters["@BranchCode"].Value = Asset.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Asset.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Asset.ToDate, objEnglishDate);
                    con.Open();
                     No = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (No > 0)
                {
                    AssetDtlString ConfList = new AssetDtlString
                    {
                        Response = "Updated Successfully",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    AssetDtlString ConfRes = new AssetDtlString
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


        ///// <summary>
        ///// Modified Abhinaya K
        ///// Modified 30-MAY-2022
        ///// </summary>
        ///// <param name="Asset"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("FA_UpdateTransferHeader")]
        //public IHttpActionResult UpdateTransferHeader([FromBody] AssetDtl Asset)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        sQuery = "UPDATE TransferHdr SET ApprovalStatus ='A', UpdatedBy='" + Asset.UpdatedBy.ToString() + "', "
        //               + "UpdatedDate=GETDATE(),ApprovedBy='" + Asset.UpdatedBy.ToString() + "',ApprovedDate=GETDATE() WHERE "
        //               + "UniqueId IN (" + Asset.UniqueId.Trim() + ") AND "
        //               + "BranchCode='" + Asset.BranchCode.Trim() + "' "
        //               + " AND CAST(TransfDocDt AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(Asset.FromDate, objEnglishDate) + "' AND "
        //               + " '" + DateTime.Parse(Asset.ToDate, objEnglishDate) + "' ";

        //        SqlCommand cmd = new SqlCommand(sQuery, con);

        //        con.Open();
        //        int No = cmd.ExecuteNonQuery();
        //        con.Close();

        //        if (No > 0)
        //        {
        //            AssetDtlString ConfList = new AssetDtlString
        //            {
        //                Response = "Updated Successfully",
        //                StatusCode = 1
        //            };
        //            return Ok(ConfList);
        //        }

        //        else
        //        {
        //            AssetDtlString ConfRes = new AssetDtlString
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
        /// Modified By Abhinaya K
        /// Modified Date 2023-04-25
        /// Modified Imran K
        /// Modified 2023-04-27
        /// </summary>
        /// <param name="Asset"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_UpdateScrapHeader")]
        public IHttpActionResult UpdateScrapHeader([FromBody] AssetDtl Asset)
        {
            try
            {
              
                int No = 0;
                string sQuery = string.Empty;
                string[] id;
                string Unique = string.Empty;
                id = Asset.UniqueId.Split(',');
                for (int i = 0; i < id.Count(); i++)
                {
                    int UniqueId = Convert.ToInt32(id[i].Replace("'", ""));
                    if (Asset.ApprovalStatus.Trim() == "A")
                    {
                        sQuery = "UPDATE ScrapHdr SET ApprovalStatus =@ApprovalStatus, UpdatedBy=@UpdatedBy, "
                         + "UpdatedDate=GETDATE(),ApprovedBy=@UpdatedBy,ApprovedDate=GETDATE()"
                         + " WHERE "
                         + "UniqueId = @UniqueId AND "
                         + "BranchCode = @BranchCode "
                         + " AND CAST(ScrapDocDt AS DATE) BETWEEN @FromDate AND @ToDate ";
                    }
                    else if (Asset.ApprovalStatus.Trim() == "R")
                    {
                        sQuery = "UPDATE ScrapHdr SET ApprovalStatus =@ApprovalStatus, UpdatedBy=@UpdatedBy, "
                         + "UpdatedDate=GETDATE(),ApprovedBy=@UpdatedBy,ApprovedDate=GETDATE(),ActiveStatus='D'"
                         + " WHERE "
                         + "UniqueId = @UniqueId AND "
                         + "BranchCode = @BranchCode "
                         + " AND CAST(ScrapDocDt AS DATE) BETWEEN @FromDate AND @ToDate ";
                    }
                    else
                    {

                    }
                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@UniqueId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ApprovalStatus", System.Data.SqlDbType.Char, 1));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));


                    cmd.Parameters["@UniqueId"].Value = UniqueId;
                    cmd.Parameters["@UpdatedBy"].Value = Asset.UpdatedBy.Trim();
                    cmd.Parameters["@ApprovalStatus"].Value = Asset.ApprovalStatus.Trim();
                    cmd.Parameters["@BranchCode"].Value = Asset.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Asset.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Asset.ToDate, objEnglishDate);
                    con.Open();
                     No = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (No > 0)
                {
                    AssetDtlString ConfList = new AssetDtlString
                    {
                        Response = "Updated Successfully",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    AssetDtlString ConfRes = new AssetDtlString
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

        ///// <summary>
        ///// Modified By Abhinaya K
        ///// Modified Date 25-MAY-2022
        ///// </summary>
        ///// <param name="Asset"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("FA_UpdateScrapHeader")]
        //public IHttpActionResult UpdateScrapHeader([FromBody] AssetDtl Asset)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        sQuery = "UPDATE ScrapHdr SET ApprovalStatus ='A', UpdatedBy='" + Asset.UpdatedBy.ToString() + "', "
        //               + "UpdatedDate=GETDATE(),ApprovedBy='" + Asset.UpdatedBy.ToString() + "',ApprovedDate=GETDATE()"
        //               + " WHERE "
        //               + "UniqueId IN (" + Asset.UniqueId.Trim() + ") AND "
        //               + "BranchCode='" + Asset.BranchCode.Trim() + "' "
        //               + " AND CAST(ScrapDocDt AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(Asset.FromDate, objEnglishDate) + "' AND "
        //               + " '" + DateTime.Parse(Asset.ToDate, objEnglishDate) + "' ";

        //        SqlCommand cmd = new SqlCommand(sQuery, con);

        //        con.Open();
        //        int No = cmd.ExecuteNonQuery();
        //        con.Close();

        //        if (No > 0)
        //        {
        //            AssetDtlString ConfList = new AssetDtlString
        //            {
        //                Response = "Updated Successfully",
        //                StatusCode = 1
        //            };
        //            return Ok(ConfList);
        //        }

        //        else
        //        {
        //            AssetDtlString ConfRes = new AssetDtlString
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
        /// Modified By Abhinaya K
        /// Modified Date 2023-04-25
        /// </summary>
        /// <param name="Asset"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_UpdateDisposeHeader")]
        public IHttpActionResult UpdateDisposeHeader([FromBody] AssetDtl Asset)
        {
            try
            {
                int No = 0;
                string sQuery = string.Empty;
                string[] id;
                string Unique = string.Empty;
                id = Asset.UniqueId.Split(',');
                for(int i=0;i<id.Count();i++)
                {
                    int UniqueId = Convert.ToInt32(id[i].Replace("'",""));
                    if (Asset.ApprovalStatus.ToString().Trim() == "R")
                    {
                        sQuery = "UPDATE DisposalHdr SET ApprovalStatus = @ApprovalStatus, "
                               + " UpdatedBy=@UpdatedBy, UpdatedDate=GETDATE(), ActiveStatus = 'D',"
                               + " ApprovedBy=@UpdatedBy, ApprovedDate=GETDATE() WHERE "
                               + " UniqueId = @UniqueId AND BranchCode=@BranchCode "
                               + " AND CAST(DispDocDt AS DATE) BETWEEN @FromDate AND "
                               + " @ToDate ";
                    }
                    else
                    {
                        sQuery = "UPDATE DisposalHdr SET ApprovalStatus = @ApprovalStatus, "
                              + " UpdatedBy= @UpdatedBy, UpdatedDate=GETDATE(),"
                              + " ApprovedBy= @UpdatedBy, ApprovedDate=GETDATE() WHERE "
                              + " UniqueId = @UniqueId AND BranchCode=@BranchCode "
                              + " AND CAST(DispDocDt AS DATE) BETWEEN @FromDate AND "
                              + " @ToDate ";
                    }

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@UniqueId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ApprovalStatus", System.Data.SqlDbType.Char, 1));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));


                    cmd.Parameters["@UniqueId"].Value = UniqueId;
                    cmd.Parameters["@UpdatedBy"].Value = Asset.UpdatedBy.Trim();
                    cmd.Parameters["@ApprovalStatus"].Value = Asset.ApprovalStatus.Trim();
                    cmd.Parameters["@BranchCode"].Value = Asset.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(Asset.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(Asset.ToDate, objEnglishDate);
                    con.Open();
                    No = cmd.ExecuteNonQuery();
                    con.Close();

                }

                if (No > 0)
                {
                    AssetDtlString ConfList = new AssetDtlString
                    {
                        Response = "Updated Successfully",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    AssetDtlString ConfRes = new AssetDtlString
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

        ///// <summary>
        ///// Modified By Abhinaya K
        ///// Modified Date 25-MAY-2022
        ///// </summary>
        ///// <param name="Asset"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("FA_UpdateDisposeHeader")]
        //public IHttpActionResult UpdateDisposeHeader([FromBody] AssetDtl Asset)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;
        //        sQuery = "UPDATE DisposalHdr SET ApprovalStatus ='A', UpdatedBy='" + Asset.UpdatedBy.ToString() + "', "
        //               + "UpdatedDate=GETDATE(),ApprovedBy='" + Asset.UpdatedBy.ToString() + "',ApprovedDate=GETDATE() WHERE "
        //               + "UniqueId IN (" + Asset.UniqueId.Trim() + ") AND "
        //               + "BranchCode='" + Asset.BranchCode.Trim() + "' "
        //               + " AND CAST(DispDocDt AS DATE) BETWEEN "
        //               + " '" + DateTime.Parse(Asset.FromDate, objEnglishDate) + "' AND "
        //               + " '" + DateTime.Parse(Asset.ToDate, objEnglishDate) + "' ";

        //        SqlCommand cmd = new SqlCommand(sQuery, con);

        //        con.Open();
        //        int No = cmd.ExecuteNonQuery();
        //        con.Close();

        //        if (No > 0)
        //        {
        //            AssetDtlString ConfList = new AssetDtlString
        //            {
        //                Response = "Updated Successfully",
        //                StatusCode = 1
        //            };
        //            return Ok(ConfList);
        //        }

        //        else
        //        {
        //            AssetDtlString ConfRes = new AssetDtlString
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
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// </summary>
        /// <param name="FilterAssetOB"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_AssetOBFilterNew")]
        public IHttpActionResult InsAssetOBFilterNew([FromBody] FA_AssetOB FilterAssetOB)
        {
            try
            {
                string squery = string.Empty;

                //if (Convert.ToString(FilterAssetOB.CorpId) != "" && FilterAssetOB.BranchCode != "" && FilterAssetOB.AssetGroup != ""
                //    && FilterAssetOB.AssetType != "" && FilterAssetOB.FinYear != "")
                //{

                string condition = string.Empty;

                condition = " WHERE A.CorpId = @CorpId AND A.DepPeriod = @DepPeriod ";

                if (FilterAssetOB.BranchCode != "0")
                {
                    condition += " AND A.BranchCode = @BranchCode ";
                }
                if (FilterAssetOB.AssetGroup != "0")
                {
                    condition += " AND B.AssetGroup = @AssetGroup ";
                }

                if (FilterAssetOB.AssetType != "0")
                {
                    condition += " AND B.AssetType = @AssetType ";
                }

                squery = " SELECT A.DepPeriod,A.CorpId,A.BranchCode,A.AssetCode,B.AssetName, "
                  + " CAST(A.GrossTotal As Decimal(10, 2)) as 'GrossBlockOB',CAST(A.DepTotal As Decimal(10, 2)) as 'DepBlockOB', "
                  + " CAST(Sum(A.GrossTotal) + Sum(A.DepTotal) AS  Decimal(10, 2)) as 'NetBlockOB' "
                  + " from YearlyDepreciation AS A   LEFT JOIN AssetMaster AS B "
                  + " ON A.BranchCode = B.BranchCode and A.AssetCode = B.AssetCode "
                  + " " + condition + " "
                  + " GROUP BY A.DepPeriod,A.CorpId,A.BranchCode,A.AssetCode,B.AssetName,A.GrossTotal,A.DepTotal ";



                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@DepPeriod", System.Data.SqlDbType.VarChar, 7));

                cmd.Parameters["@CorpId"].Value = FilterAssetOB.CorpId.Trim();
                cmd.Parameters["@AssetGroup"].Value = FilterAssetOB.AssetGroup.Trim();
                cmd.Parameters["@AssetType"].Value = FilterAssetOB.AssetType.Trim();
                cmd.Parameters["@BranchCode"].Value = FilterAssetOB.BranchCode.Trim();
                cmd.Parameters["@DepPeriod"].Value = FilterAssetOB.FinYear.Trim();

                List<FA_AssetOB> li = new List<FA_AssetOB>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FA_AssetOB lstAssetObFilter = new FA_AssetOB();

                        lstAssetObFilter.CorpId = dt.Rows[i]["CorpId"].ToString();
                        lstAssetObFilter.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                        lstAssetObFilter.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                        lstAssetObFilter.AssetName = dt.Rows[i]["AssetName"].ToString();
                        lstAssetObFilter.GrossBlockOB = dt.Rows[i]["GrossBlockOB"].ToString();
                        lstAssetObFilter.NetBlockOB = dt.Rows[i]["NetBlockOB"].ToString();
                        lstAssetObFilter.DepBlockOB = dt.Rows[i]["DepBlockOB"].ToString();
                        lstAssetObFilter.FinYear = dt.Rows[i]["DepPeriod"].ToString();

                        li.Add(lstAssetObFilter);
                    }

                    FA_AssetOBList BoatRate = new FA_AssetOBList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatRate);
                }
                else
                {
                    FA_AssetOBRes BoatRate = new FA_AssetOBRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(BoatRate);
                }
                // }
                //else
                //{
                //    FA_AssetOBRes Vehicle = new FA_AssetOBRes
                //    {
                //        Response = "Must Pass All Parameters",
                //        StatusCode = 0
                //    };
                //    return Ok(Vehicle);
                //}
            }
            catch (Exception ex)
            {
                FA_AssetOBRes ConfRes = new FA_AssetOBRes
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
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// </summary>
        /// <param name="assDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_GetNoDepFlag")]
        public IHttpActionResult GetNoDepFlag([FromBody] AssetDtl assDtl)
        {
            try
            {
                string squery = string.Empty;

                if (assDtl.AssetGroup != "" && assDtl.BranchCode != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.DepPeriod= @DepPeriod AND B.NoDepFlag= @NoDepFlag "
                              + " AND A.BranchCode = @BranchCode ";

                    if (assDtl.AssetGroup != "All")
                    {
                        condition += " AND A.GroupCode = @AssetGroup ";
                    }
                    squery = "SELECT  A.AssetCode,A.GroupCode, A.InstallationDt,A.OriginalCost,A.GrossTotal,A.DepTotal,A.AssetClgValue,A.AssetResValue, "
                        + " B.NoDepFlag FROM  YearlyDepreciation As A Inner Join AssetMaster AS B ON "
                        + " A.AssetCode = B.AssetCode And A.BranchCode = B.BranchCode AND A.GroupCode = B.AssetGroup " + condition + " ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@NoDepFlag", System.Data.SqlDbType.Char, 1));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@DepPeriod", System.Data.SqlDbType.VarChar, 7));

                    cmd.Parameters["@AssetGroup"].Value = assDtl.AssetGroup.Trim();
                    cmd.Parameters["@NoDepFlag"].Value = assDtl.NoDepFlag.Trim();
                    cmd.Parameters["@BranchCode"].Value = assDtl.BranchCode.Trim();
                    cmd.Parameters["@DepPeriod"].Value = assDtl.FromDate.Trim();
                    List<AssetDtl> li = new List<AssetDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AssetDtl assDt = new AssetDtl();
                            assDt.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            assDt.AssetGroup = dt.Rows[i]["GroupCode"].ToString();
                            assDt.InstallationDt = dt.Rows[i]["InstallationDt"].ToString();
                            assDt.ProductCost = dt.Rows[i]["OriginalCost"].ToString();
                            assDt.ResidualValue = dt.Rows[i]["AssetResValue"].ToString();

                            assDt.DepTotal = dt.Rows[i]["DepTotal"].ToString();
                            assDt.GrossTotal = dt.Rows[i]["GrossTotal"].ToString();
                            assDt.AssetClgValue = dt.Rows[i]["AssetClgValue"].ToString();
                            assDt.NoDepFlag = dt.Rows[i]["NoDepFlag"].ToString();

                            li.Add(assDt);
                        }

                        AssetDtlList BoatRate = new AssetDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        AssetDtlString BoatRate = new AssetDtlString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    AssetDtlString Vehicle = new AssetDtlString
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
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// </summary>
        /// <param name="assDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_GetWriteOffFlag")]
        public IHttpActionResult GetWriteOffFlag([FromBody] AssetDtl assDtl)
        {
            try
            {
                string squery = string.Empty;

                if (assDtl.AssetGroup != "" && assDtl.BranchCode != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.DepPeriod= @DepPeriod AND B.WriteOffFlag= @WriteOffFlag "
                              + " AND A.BranchCode = @BranchCode ";

                    if (assDtl.AssetGroup != "All")
                    {
                        condition += " AND A.GroupCode = @AssetGroup";
                    }
                    squery = "SELECT  A.AssetCode,A.GroupCode, A.InstallationDt,A.OriginalCost,A.GrossTotal,A.DepTotal,A.AssetClgValue,A.AssetResValue, "
                              + " B.WriteOffFlag FROM  YearlyDepreciation As A Inner Join AssetMaster AS B ON "
                               + " A.AssetCode = B.AssetCode And A.BranchCode = B.BranchCode AND A.GroupCode = B.AssetGroup "
                                + " " + condition + " ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@WriteOffFlag", System.Data.SqlDbType.Char, 1));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@DepPeriod", System.Data.SqlDbType.VarChar, 7));

                    cmd.Parameters["@AssetGroup"].Value = assDtl.AssetGroup.Trim();
                    cmd.Parameters["@WriteOffFlag"].Value = assDtl.WriteOffFlag.Trim();
                    cmd.Parameters["@BranchCode"].Value = assDtl.BranchCode.Trim();
                    cmd.Parameters["@DepPeriod"].Value = assDtl.FromDate.Trim();

                    List<AssetDtl> li = new List<AssetDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AssetDtl assDt = new AssetDtl();


                            assDt.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            assDt.AssetGroup = dt.Rows[i]["GroupCode"].ToString();
                            assDt.InstallationDt = dt.Rows[i]["InstallationDt"].ToString();
                            assDt.ProductCost = dt.Rows[i]["OriginalCost"].ToString();
                            assDt.ResidualValue = dt.Rows[i]["AssetResValue"].ToString();

                            assDt.DepTotal = dt.Rows[i]["DepTotal"].ToString();
                            assDt.GrossTotal = dt.Rows[i]["GrossTotal"].ToString();
                            assDt.AssetClgValue = dt.Rows[i]["AssetClgValue"].ToString();
                            assDt.WriteOffFlag = dt.Rows[i]["WriteOffFlag"].ToString();

                            li.Add(assDt);
                        }

                        AssetDtlList BoatRate = new AssetDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        AssetDtlString BoatRate = new AssetDtlString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    AssetDtlString Vehicle = new AssetDtlString
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
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// </summary>
        /// <param name="Asset"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_UpdateWriteOffFlag")]
        public IHttpActionResult UpdateWriteOffFlag([FromBody] AssetDtl Asset)
        {
            try
            {
                int No = 0;
                string sQuery = string.Empty;
                string[] SplitNoteDesp = Asset.AssetCode.Split(',');
                for (int i = 0; i < SplitNoteDesp.Count(); i++)
                {
                    sQuery = "UPDATE AssetMaster Set WriteOffFlag='T', WriteOffFlagDesc = @WriteOffFlagDesc  WHERE AssetCode = @AssetCode And  "
                    + "  BranchCode= @BranchCode ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@WriteOffFlagDesc", System.Data.SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@AssetCode", System.Data.SqlDbType.VarChar, 30));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters["@WriteOffFlagDesc"].Value = Asset.NoDepFlagDesc.Trim();
                    cmd.Parameters["@AssetCode"].Value = SplitNoteDesp[i].ToString().Trim();
                    cmd.Parameters["@BranchCode"].Value = Asset.BranchCode.Trim();

                    con.Open();
                    No = cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (No > 0)
                {
                    AssetDtlString ConfList = new AssetDtlString
                    {
                        Response = "Updated Successfully",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    AssetDtlString ConfRes = new AssetDtlString
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


        /// <summary>
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// </summary>
        /// <param name="Asset"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_UpdateNoDepFlag")]
        public IHttpActionResult UpdateNoDepFlag([FromBody] AssetDtl Asset)
        {
            try
            {
                int No = 0;
                string sQuery = string.Empty;
                string[] SplitNoteDesp = Asset.AssetCode.Split(',');
                for (int i = 0; i < SplitNoteDesp.Count(); i++)
                {
                    sQuery = "UPDATE AssetMaster Set NoDepFlag='T', NoDepFlagDesc= @NoDepFlagDesc  WHERE AssetCode= @AssetCode And  "
                        + " BranchCode= @BranchCode ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@NoDepFlagDesc", System.Data.SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@AssetCode", System.Data.SqlDbType.VarChar, 30));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters["@NoDepFlagDesc"].Value = Asset.NoDepFlagDesc.Trim();
                    cmd.Parameters["@AssetCode"].Value = SplitNoteDesp[i].ToString().Trim();
                    cmd.Parameters["@BranchCode"].Value = Asset.BranchCode.Trim();

                    con.Open();
                    No = cmd.ExecuteNonQuery();
                    con.Close();

                }
                if (No > 0)
                {
                    AssetDtlString ConfList = new AssetDtlString
                    {
                        Response = "Updated Successfully",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    AssetDtlString ConfRes = new AssetDtlString
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

        /// <summary>
        /// Newly Added By Abhinaya K
        /// Newly Added Date 25MAY2022
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// </summary>
        /// <param name="InsScrapHdr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsScrapFilterV2")]
        public IHttpActionResult InsScrapFilterV2([FromBody] FA_ScrapDtl InsScrapHdr)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(InsScrapHdr.CorpId) != "" && InsScrapHdr.BranchCode != "" && InsScrapHdr.Input1 != ""
                    && InsScrapHdr.Input2 != "" && InsScrapHdr.Input3 != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.CorpId = @CorpId AND cast(A.ScrapDocDt as DATE) BETWEEN  @FromDate AND @ToDate ";

                    if (InsScrapHdr.BranchCode != "0")
                    {
                        condition += " AND A.BranchCode = @BranchCode ";
                    }
                    if (InsScrapHdr.Input1 != "0")
                    {
                        condition += " AND B.AssetGroup = @AssetGroup ";
                    }

                    if (InsScrapHdr.Input2 != "0")
                    {
                        condition += " AND B.AssetType=  @AssetType ";
                    }


					squery = "SELECT DISTINCT A.CorpId,A.UniqueId,A.ApprovalStatus,A.BranchCode,A.ScrapTransNo, "
					+ " A.ScrapDocNo,CONVERT(NVARCHAR,A.ScrapDocDt,105) AS 'ScrapDocDt',A.Reason, A.ActiveStatus, "
					+ " C.ScrapValue,C.AssetCode,C.TransSlNo,C.ScrapTransNo,A.ScrapDocDt" 
					+ " FROM ScrapHdr AS A INNER JOIN AssetMaster AS B ON A.BranchCode = B.BranchCode "
					+ " INNER JOIN ScrapDtl AS C ON B.AssetCode = C.AssetCode AND A.ScrapTransNo = C.ScrapTransNo "
					+ " " + condition + " ORDER BY A.ScrapTransNo ASC,A.ApprovalStatus DESC ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                    cmd.Parameters["@CorpId"].Value = InsScrapHdr.CorpId.Trim();
                    cmd.Parameters["@AssetGroup"].Value = InsScrapHdr.Input1.Trim();
                    cmd.Parameters["@AssetType"].Value = InsScrapHdr.Input2.Trim();
                    cmd.Parameters["@BranchCode"].Value = InsScrapHdr.BranchCode.Trim();
                    cmd.Parameters["@FromDate"].Value = DateTime.Parse(InsScrapHdr.FromDate, objEnglishDate);
                    cmd.Parameters["@ToDate"].Value = DateTime.Parse(InsScrapHdr.ToDate, objEnglishDate);
                    List<FA_ScrapDtl> li = new List<FA_ScrapDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FA_ScrapDtl lstScrapFilter = new FA_ScrapDtl();

                            lstScrapFilter.CorpId = dt.Rows[i]["CorpId"].ToString();
                            lstScrapFilter.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            lstScrapFilter.ScrapTransNo = dt.Rows[i]["ScrapTransNo"].ToString();
                            lstScrapFilter.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            lstScrapFilter.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            lstScrapFilter.Reason = dt.Rows[i]["Reason"].ToString();
                            lstScrapFilter.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            lstScrapFilter.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            lstScrapFilter.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();
                            lstScrapFilter.ScrapValue = dt.Rows[i]["ScrapValue"].ToString();
                            lstScrapFilter.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            lstScrapFilter.TransSlNo = dt.Rows[i]["TransSlNo"].ToString();
                            lstScrapFilter.ScrapTransNo = dt.Rows[i]["ScrapTransNo"].ToString();
                            lstScrapFilter.ScrapDate = dt.Rows[i]["ScrapDocDt"].ToString();
                            li.Add(lstScrapFilter);
                        }

                        FA_ScrapDtlList BoatRate = new FA_ScrapDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        FA_ScrapDtlRes BoatRate = new FA_ScrapDtlRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    FA_ScrapDtlRes Vehicle = new FA_ScrapDtlRes
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
        /// Newly Added By Abhinaya K
        /// Newly Added Date 25MAY2022
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// <param name="DisposalFilter"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("DisposalDetailFilterV2")]
        public IHttpActionResult DisposalDetailFilterV2([FromBody] FA_DisposalDtl DisposalFilter)
        {
            try
            {
                string squery = string.Empty;

                string conditions = " WHERE A.CorpId = @CorpId ";

                if (DisposalFilter.BranchCode != "All" && DisposalFilter.BranchCode != "0")
                {
                    conditions = " WHERE A.CorpId = @CorpId  "
                             + " AND A.BranchCode = @BranchCode  ";
                }

                if (DisposalFilter.BranchCode != "All" && DisposalFilter.AssetGroup != "All" && DisposalFilter.BranchCode != "0" && DisposalFilter.AssetGroup != "0")
                {
                    conditions = " WHERE A.CorpId = @CorpId  AND CAST(A.CreatedDate AS date) = CAST(GetDate() As date) "
                        + " AND A.BranchCode = @BranchCode AND B.AssetGroup = @AssetGroup ";
                }

                if (DisposalFilter.AssetType != "All" && DisposalFilter.BranchCode != "All" && DisposalFilter.AssetGroup != "All" &&
                    DisposalFilter.AssetType != "0" && DisposalFilter.BranchCode != "0" && DisposalFilter.AssetGroup != "0")
                {
                    conditions = " WHERE A.CorpId = @CorpId  AND CAST(A.CreatedDate AS date) = CAST(GetDate() As date) "
                        + "AND A.BranchCode = @BranchCode AND B.AssetGroup = @AssetGroup "
                        + " AND  B.AssetType = @AssetType  ";

                }

                squery = " SELECT DISTINCT A.CorpId,A.UniqueId,A.ApprovalStatus,A.BranchCode,A.DispTransNo, "
                    + " A.DispDocNo,CONVERT(varchar(10), A.DispDocDt, 103) AS 'DispDocDt',A.ActiveStatus, C.DispTransNo,"
					+ " C.TransSlNo, C.AssetCode,  A.DispDocDt, C.DispValue "
					+ " FROM DisposalHdr AS A "
                    + " INNER JOIN AssetMaster AS B ON A.BranchCode = B.BranchCode "
                    + " INNER JOIN DisposalDtl AS C ON B.AssetCode = C.AssetCode AND A.DispTransNo = C.DispTransNo "
                    + " " + conditions + " AND cast(A.DispDocDt as DATE) BETWEEN  @FromDate "
                    + "  AND @ToDate ORDER BY A.DispTransNo ASC,A.ApprovalStatus DESC ";


                SqlCommand cmd = new SqlCommand(squery, con);
                cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));
                cmd.Parameters.Add(new SqlParameter("@FromDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@ToDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@CorpId"].Value = DisposalFilter.CorpId.Trim();
                cmd.Parameters["@AssetGroup"].Value = DisposalFilter.AssetGroup.Trim();
                cmd.Parameters["@AssetType"].Value = DisposalFilter.AssetType.Trim();
                cmd.Parameters["@BranchCode"].Value = DisposalFilter.BranchCode.Trim();
                cmd.Parameters["@FromDate"].Value = DateTime.Parse(DisposalFilter.FromDate, objEnglishDate);
                cmd.Parameters["@ToDate"].Value = DateTime.Parse(DisposalFilter.ToDate, objEnglishDate);


                List<FA_DisposalDtl> li = new List<FA_DisposalDtl>();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FA_DisposalDtl lstDisposalList = new FA_DisposalDtl();
                        lstDisposalList.CorpId = dt.Rows[i]["CorpId"].ToString();
                        lstDisposalList.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                        lstDisposalList.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        lstDisposalList.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();
                        lstDisposalList.DispTransNo = dt.Rows[i]["DispTransNo"].ToString();
                        lstDisposalList.DispDocNo = dt.Rows[i]["DispDocNo"].ToString();
                        lstDisposalList.DispDocDt = dt.Rows[i]["DispDocDt"].ToString();
                        lstDisposalList.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        lstDisposalList.DispTransNo = dt.Rows[i]["DispTransNo"].ToString();
                        lstDisposalList.TransSlNo = dt.Rows[i]["TransSlNo"].ToString();
                        lstDisposalList.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                        lstDisposalList.DispDate = dt.Rows[i]["DispDocDt"].ToString();
                        lstDisposalList.DispValue = dt.Rows[i]["DispValue"].ToString();

                        li.Add(lstDisposalList);
                    }

                    FA_DisposalDtlList DispLi = new FA_DisposalDtlList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(DispLi);
                }

                else
                {
                    FA_DisposalDtlRes DisRes = new FA_DisposalDtlRes
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(DisRes);
                }
            }
            catch (Exception ex)
            {
                FA_DisposalDtlRes DisRes = new FA_DisposalDtlRes
                {
                    Response = Convert.ToString(ex),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(DisRes);
            }

        }


        /// <summary>
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Removed 1=1 in where condition , seemed to be not used 
        /// </summary>
        /// <param name="assDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_GetAssetAllDetails")]
        public IHttpActionResult GetAssetAllDetails([FromBody] AssetDtl assDtl)
        {
            try
            {
                string squery = string.Empty;

                if (assDtl.AssetGroup != "" && assDtl.BranchCode != "" && assDtl.AssetType != "")
                {

                    string condition = string.Empty;

                    condition = " WHERE A.ActiveStatus IN ('A', 'D') AND A.AssetStatus = 'L' AND ApprovalStatus='A' ";

                    if (assDtl.AssetGroup != "All")
                    {
                        condition += " AND A.AssetGroup= @AssetGroup ";
                    }
                    if (assDtl.BranchCode != "Select Branch")
                    {
                        condition += " AND A.BranchCode = @BranchCode ";
                    }

                    if (assDtl.AssetType != "All")
                    {
                        condition += " AND A.AssetType = @AssetType ";
                    }

                    squery = "SELECT  A.ApprovalStatus, A.UniqueId,A.CorpId,A.AssetCode,A.AssetSl,A.AssetName,A.AssetShName,A.AssetDesc,A.BranchCode, BR.BranchName,A.AssetTag,A.AssetType,"
                        + " AST.AssetTypeName,A.AssetGroup, GR.GroupName,A.LocationId,A.DeptId,A.VendorId, A.ManufId,A.CustodianId, "
                        + " CONVERT(NVARCHAR,A.InstallationDt,103) AS 'InstallationDt',CONVERT(NVARCHAR,A.DepStartDt,103) AS 'DepStartDt', "
                        + " A.ProductSlNo,A.ProductQty,A.PurchaseRefNo, A.PurchaseRefDt,A.InvoiceRefNo,CONVERT(NVARCHAR,A.InvoiceRefDt,103) AS 'InvoiceRefDt',A.OtherRefNo, A.OtherRefDt, A.ProductCost,"
                        + " A.WarrantyRef,A.WarrantyStart,A.WarrantyEnd,A.ResidualPer, A.ResidualValue,A.ScrapDocNo,A.ScrapDocDt,A.DispDocNo,A.DispDocDt,A.DispValue,"
                        + " A.OrginalQty,A.QtySlNo,A.OtherInfo,A.WriteOffFlag,A.NoDepFlag,A.AssetStatus,A.ActiveStatus,A.CreatedBy,A.CreatedDate,A.UpdatedBy,A.UpdatedDate, A.ApprovalStatus"
                        + " FROM AssetMaster AS A"
                        + " LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS BR ON A.BranchCode = BR.BranchCode AND A.CorpId = BR.CorpId"
                        + " LEFT JOIN GroupMaster AS GR ON A.AssetGroup = GR.GroupCode AND A.CorpId = GR.CorpId"
                        + " LEFT JOIN AssetType AS AST ON A.AssetType = AST.AssetType AND A.CorpId = AST.CorpId " + condition + " "
                        + " ORDER BY A.ApprovalStatus DESC ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@AssetGroup", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@AssetType", System.Data.SqlDbType.VarChar, 15));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@AssetGroup"].Value = assDtl.AssetGroup.Trim();
                    cmd.Parameters["@AssetType"].Value = assDtl.AssetType.Trim();
                    cmd.Parameters["@BranchCode"].Value = assDtl.BranchCode.Trim();
                    List<AssetDtl> li = new List<AssetDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AssetDtl assDt = new AssetDtl();

                            assDt.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            assDt.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();

                            assDt.CorpId = dt.Rows[i]["CorpId"].ToString();
                            assDt.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            assDt.AssetSl = dt.Rows[i]["AssetSl"].ToString();
                            assDt.AssetName = dt.Rows[i]["AssetName"].ToString();
                            assDt.AssetShName = dt.Rows[i]["AssetShName"].ToString();

                            assDt.AssetDesc = dt.Rows[i]["AssetDesc"].ToString();
                            assDt.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            assDt.BranchName = dt.Rows[i]["BranchName"].ToString();
                            assDt.AssetTag = dt.Rows[i]["AssetTag"].ToString();
                            assDt.AssetType = dt.Rows[i]["AssetType"].ToString();
                            assDt.AssetTypeName = dt.Rows[i]["AssetTypeName"].ToString();
                            assDt.AssetGroup = dt.Rows[i]["AssetGroup"].ToString();
                            assDt.GroupName = dt.Rows[i]["GroupName"].ToString();

                            assDt.LocationId = dt.Rows[i]["LocationId"].ToString();
                            assDt.DeptId = dt.Rows[i]["DeptId"].ToString();
                            assDt.VendorId = dt.Rows[i]["VendorId"].ToString();
                            assDt.ManufId = dt.Rows[i]["ManufId"].ToString();
                            assDt.CustodianId = dt.Rows[i]["CustodianId"].ToString();

                            assDt.InstallationDt = dt.Rows[i]["InstallationDt"].ToString();
                            assDt.DepStartDt = dt.Rows[i]["DepStartDt"].ToString();
                            assDt.ProductSlNo = dt.Rows[i]["ProductSlNo"].ToString();
                            assDt.ProductQty = dt.Rows[i]["ProductQty"].ToString();
                            assDt.PurchaseRefNo = dt.Rows[i]["PurchaseRefNo"].ToString();

                            assDt.PurchaseRefDt = dt.Rows[i]["PurchaseRefDt"].ToString();
                            assDt.InvoiceRefNo = dt.Rows[i]["InvoiceRefNo"].ToString();
                            assDt.InvoiceRefDt = dt.Rows[i]["InvoiceRefDt"].ToString();
                            assDt.OtherRefNo = dt.Rows[i]["OtherRefNo"].ToString();
                            assDt.OtherRefDt = dt.Rows[i]["OtherRefDt"].ToString();

                            assDt.ProductCost = dt.Rows[i]["ProductCost"].ToString();
                            assDt.WarrantyRef = dt.Rows[i]["WarrantyRef"].ToString();
                            assDt.WarrantyStart = dt.Rows[i]["WarrantyStart"].ToString();
                            assDt.WarrantyEnd = dt.Rows[i]["WarrantyEnd"].ToString();
                            assDt.ResidualPer = dt.Rows[i]["ResidualPer"].ToString();

                            assDt.ResidualValue = dt.Rows[i]["ResidualValue"].ToString();
                            assDt.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            assDt.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            assDt.DispDocNo = dt.Rows[i]["DispDocNo"].ToString();
                            assDt.DispDocDt = dt.Rows[i]["DispDocDt"].ToString();

                            assDt.DispValue = dt.Rows[i]["DispValue"].ToString();
                            assDt.OrginalQty = dt.Rows[i]["OrginalQty"].ToString();
                            assDt.QtySlNo = dt.Rows[i]["QtySlNo"].ToString();
                            assDt.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
                            assDt.WriteOffFlag = dt.Rows[i]["WriteOffFlag"].ToString();

                            assDt.NoDepFlag = dt.Rows[i]["NoDepFlag"].ToString();
                            assDt.AssetStatus = dt.Rows[i]["AssetStatus"].ToString();
                            assDt.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            assDt.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                            assDt.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();

                            assDt.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                            assDt.UpdatedDate = dt.Rows[i]["UpdatedDate"].ToString();
                            assDt.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();

                            li.Add(assDt);
                        }

                        AssetDtlList BoatRate = new AssetDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        AssetDtlString BoatRate = new AssetDtlString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    AssetDtlString Vehicle = new AssetDtlString
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
        /// Created By : Abhinaya
        /// Created Date : 2023-02-20
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Removed 1=1 in where condition , seemed to be not used  
        /// </summary>
        /// <param name="assDtl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_RptGetAssetDetailsBasedOnFinYear")]
        public IHttpActionResult GetAssetDetailsBasedOnFinYear([FromBody] AssetDtl assDtl)
        {
            try
            {
                string squery = string.Empty;

                if (assDtl.AssetGroup != "" && assDtl.BranchCode != "" && assDtl.AssetType != "")
                {

                    string condition = string.Empty;
                    string sStardate = string.Empty;
                    string sEnddate = string.Empty;
                    string[] arrReportDate;
                    string sReportDate = string.Empty;

                    DataTable dtblMaxDate = new DataTable();
                    condition = " WHERE A.ActiveStatus IN ('A', 'D') AND A.AssetStatus = 'L' ";
                    if (assDtl.FromDate != "0")
                    {


                        sReportDate = assDtl.FromDate;

                        arrReportDate = sReportDate.Split("-".ToCharArray());

                        sStardate = "01-04-" + arrReportDate[0];
                        sEnddate = "31-03-" + arrReportDate[1];
                        condition += "AND CAST(A.InstallationDt AS DATE) BETWEEN @sStardate AND @sEnddate";

                    }

                    if (assDtl.BranchCode != "0")
                    {
                        condition += " AND A.BranchCode =  @BranchCode";
                    }

                    squery = "SELECT A.ApprovalStatus, A.UniqueId,A.CorpId,A.AssetCode,A.AssetSl,A.AssetName,A.AssetShName,A.AssetDesc,A.BranchCode, BR.BranchName,A.AssetTag,A.AssetType,"
                        + " AST.AssetTypeName,A.AssetGroup, GR.GroupName,A.LocationId,A.DeptId,A.VendorId, A.ManufId,A.CustodianId, "
                        + " CONVERT(NVARCHAR,A.InstallationDt,103) AS 'InstallationDt',CONVERT(NVARCHAR,A.DepStartDt,103) AS 'DepStartDt', "
                        + " A.ProductSlNo,A.ProductQty,A.PurchaseRefNo, A.PurchaseRefDt,A.InvoiceRefNo,CONVERT(NVARCHAR,A.InvoiceRefDt,103) AS 'InvoiceRefDt',A.OtherRefNo, A.OtherRefDt, A.ProductCost,"
                        + " A.WarrantyRef,A.WarrantyStart,A.WarrantyEnd,A.ResidualPer, A.ResidualValue,A.ScrapDocNo,A.ScrapDocDt,A.DispDocNo,A.DispDocDt,A.DispValue,"
                        + " A.OrginalQty,A.QtySlNo,A.OtherInfo,A.WriteOffFlag,A.NoDepFlag,A.AssetStatus,A.ActiveStatus,A.CreatedBy,A.CreatedDate,A.UpdatedBy,A.UpdatedDate, A.ApprovalStatus"
                        + " FROM AssetMaster AS A"
                        + " LEFT JOIN "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchMaster AS BR ON A.BranchCode = BR.BranchCode AND A.CorpId = BR.CorpId"
                        + " LEFT JOIN GroupMaster AS GR ON A.AssetGroup = GR.GroupCode AND A.CorpId = GR.CorpId"
                        + " LEFT JOIN AssetType AS AST ON A.AssetType = AST.AssetType AND A.CorpId = AST.CorpId " + condition + " "
                        + " ORDER BY A.ApprovalStatus DESC ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@sStardate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@sEnddate", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@sStardate"].Value = DateTime.Parse(sStardate, objEnglishDate);
                    cmd.Parameters["@sEnddate"].Value = DateTime.Parse(sEnddate, objEnglishDate);
                    cmd.Parameters["@BranchCode"].Value = assDtl.BranchCode.Trim();
                    List<AssetDtl> li = new List<AssetDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AssetDtl assDt = new AssetDtl();

                            assDt.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            assDt.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();

                            assDt.CorpId = dt.Rows[i]["CorpId"].ToString();
                            assDt.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            assDt.AssetSl = dt.Rows[i]["AssetSl"].ToString();
                            assDt.AssetName = dt.Rows[i]["AssetName"].ToString();
                            assDt.AssetShName = dt.Rows[i]["AssetShName"].ToString();

                            assDt.AssetDesc = dt.Rows[i]["AssetDesc"].ToString();
                            assDt.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            assDt.BranchName = dt.Rows[i]["BranchName"].ToString();
                            assDt.AssetTag = dt.Rows[i]["AssetTag"].ToString();
                            assDt.AssetType = dt.Rows[i]["AssetType"].ToString();
                            assDt.AssetTypeName = dt.Rows[i]["AssetTypeName"].ToString();
                            assDt.AssetGroup = dt.Rows[i]["AssetGroup"].ToString();
                            assDt.GroupName = dt.Rows[i]["GroupName"].ToString();

                            assDt.LocationId = dt.Rows[i]["LocationId"].ToString();
                            assDt.DeptId = dt.Rows[i]["DeptId"].ToString();
                            assDt.VendorId = dt.Rows[i]["VendorId"].ToString();
                            assDt.ManufId = dt.Rows[i]["ManufId"].ToString();
                            assDt.CustodianId = dt.Rows[i]["CustodianId"].ToString();

                            assDt.InstallationDt = dt.Rows[i]["InstallationDt"].ToString();
                            assDt.DepStartDt = dt.Rows[i]["DepStartDt"].ToString();
                            assDt.ProductSlNo = dt.Rows[i]["ProductSlNo"].ToString();
                            assDt.ProductQty = dt.Rows[i]["ProductQty"].ToString();
                            assDt.PurchaseRefNo = dt.Rows[i]["PurchaseRefNo"].ToString();

                            assDt.PurchaseRefDt = dt.Rows[i]["PurchaseRefDt"].ToString();
                            assDt.InvoiceRefNo = dt.Rows[i]["InvoiceRefNo"].ToString();
                            assDt.InvoiceRefDt = dt.Rows[i]["InvoiceRefDt"].ToString();
                            assDt.OtherRefNo = dt.Rows[i]["OtherRefNo"].ToString();
                            assDt.OtherRefDt = dt.Rows[i]["OtherRefDt"].ToString();

                            assDt.ProductCost = dt.Rows[i]["ProductCost"].ToString();
                            assDt.WarrantyRef = dt.Rows[i]["WarrantyRef"].ToString();
                            assDt.WarrantyStart = dt.Rows[i]["WarrantyStart"].ToString();
                            assDt.WarrantyEnd = dt.Rows[i]["WarrantyEnd"].ToString();
                            assDt.ResidualPer = dt.Rows[i]["ResidualPer"].ToString();

                            assDt.ResidualValue = dt.Rows[i]["ResidualValue"].ToString();
                            assDt.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            assDt.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            assDt.DispDocNo = dt.Rows[i]["DispDocNo"].ToString();
                            assDt.DispDocDt = dt.Rows[i]["DispDocDt"].ToString();

                            assDt.DispValue = dt.Rows[i]["DispValue"].ToString();
                            assDt.OrginalQty = dt.Rows[i]["OrginalQty"].ToString();
                            assDt.QtySlNo = dt.Rows[i]["QtySlNo"].ToString();
                            assDt.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
                            assDt.WriteOffFlag = dt.Rows[i]["WriteOffFlag"].ToString();

                            assDt.NoDepFlag = dt.Rows[i]["NoDepFlag"].ToString();
                            assDt.AssetStatus = dt.Rows[i]["AssetStatus"].ToString();
                            assDt.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            assDt.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                            assDt.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();

                            assDt.UpdatedBy = dt.Rows[i]["UpdatedBy"].ToString();
                            assDt.UpdatedDate = dt.Rows[i]["UpdatedDate"].ToString();
                            assDt.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();

                            li.Add(assDt);
                        }

                        AssetDtlList BoatRate = new AssetDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        AssetDtlString BoatRate = new AssetDtlString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    AssetDtlString Vehicle = new AssetDtlString
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
        /// Created By : Abhinaya
        /// Created Date : 2023-02-20
        /// Modified BY  : Abhinaya
        /// Modified Date : 2023-04-25
        /// </summary>
        /// <param name="InsScrapHdr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("FA_InsScrapFilterBasedOnFinYear")]
        public IHttpActionResult InsScrapBasedOnFinYear([FromBody] FA_ScrapDtl InsScrapHdr)
        {
            try
            {
                string squery = string.Empty;

                if (Convert.ToString(InsScrapHdr.CorpId) != "" && InsScrapHdr.BranchCode != "" && InsScrapHdr.Input1 != ""
                    && InsScrapHdr.Input2 != "" && InsScrapHdr.Input3 != "")
                {

                    string condition = string.Empty;
                    string sStardate = string.Empty;
                    string sEnddate = string.Empty;
                    string[] arrReportDate;
                    string sReportDate = string.Empty;

                    DataTable dtblMaxDate = new DataTable();
                    condition = " WHERE A.CorpId = @CorpId";

                    if (InsScrapHdr.FromDate != "0")
                    {


                        sReportDate = InsScrapHdr.FromDate;

                        arrReportDate = sReportDate.Split("-".ToCharArray());

                        sStardate = "01-04-" + arrReportDate[0];
                        sEnddate = "31-03-" + arrReportDate[1];
                        condition += " AND cast(A.ScrapDocDt as DATE) BETWEEN @sStardate AND @sEnddate";

                    }
                    if (InsScrapHdr.BranchCode != "0")
                    {
                        condition += " AND A.BranchCode = @BranchCode ";
                    }

                    squery = "SELECT DISTINCT A.CorpId,A.UniqueId,A.ApprovalStatus,A.BranchCode,A.ScrapTransNo, "
                        + " A.ScrapDocNo,CONVERT(NVARCHAR,A.ScrapDocDt,105) AS 'ScrapDocDt',A.Reason, "
                         + " A.ActiveStatus,A.ScrapTransNo,A.ScrapDocDt FROM ScrapHdr AS A "
                         + " INNER JOIN AssetMaster AS B ON A.BranchCode = B.BranchCode "
                         //+ " INNER JOIN ScrapDtl AS C ON B.AssetCode = C.AssetCode AND A.ScrapTransNo = C.ScrapTransNo "
                         + " " + condition + " ORDER BY A.ScrapTransNo ASC,A.ApprovalStatus DESC ";

                    SqlCommand cmd = new SqlCommand(squery, con);
                    cmd.Parameters.Add(new SqlParameter("@CorpId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@sStardate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@sEnddate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@BranchCode", System.Data.SqlDbType.VarChar, 10));

                    cmd.Parameters["@CorpId"].Value = InsScrapHdr.CorpId.Trim();
                    cmd.Parameters["@sStardate"].Value = DateTime.Parse(sStardate, objEnglishDate);
                    cmd.Parameters["@sEnddate"].Value = DateTime.Parse(sEnddate, objEnglishDate);
                    cmd.Parameters["@BranchCode"].Value = InsScrapHdr.BranchCode.Trim();
                    List<FA_ScrapDtl> li = new List<FA_ScrapDtl>();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            FA_ScrapDtl lstScrapFilter = new FA_ScrapDtl();

                            lstScrapFilter.CorpId = dt.Rows[i]["CorpId"].ToString();
                            lstScrapFilter.BranchCode = dt.Rows[i]["BranchCode"].ToString();
                            lstScrapFilter.ScrapTransNo = dt.Rows[i]["ScrapTransNo"].ToString();
                            lstScrapFilter.ScrapDocNo = dt.Rows[i]["ScrapDocNo"].ToString();
                            lstScrapFilter.ScrapDocDt = dt.Rows[i]["ScrapDocDt"].ToString();
                            lstScrapFilter.Reason = dt.Rows[i]["Reason"].ToString();
                            lstScrapFilter.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            lstScrapFilter.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            lstScrapFilter.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();
                            //lstScrapFilter.ScrapValue = dt.Rows[i]["ScrapValue"].ToString();
                            //lstScrapFilter.AssetCode = dt.Rows[i]["AssetCode"].ToString();
                            //lstScrapFilter.TransSlNo = dt.Rows[i]["TransSlNo"].ToString();
                            lstScrapFilter.ScrapTransNo = dt.Rows[i]["ScrapTransNo"].ToString();
                            lstScrapFilter.ScrapDate = dt.Rows[i]["ScrapDocDt"].ToString();
                            li.Add(lstScrapFilter);
                        }

                        FA_ScrapDtlList BoatRate = new FA_ScrapDtlList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }
                    else
                    {
                        FA_ScrapDtlRes BoatRate = new FA_ScrapDtlRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    FA_ScrapDtlRes Vehicle = new FA_ScrapDtlRes
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
    }
}

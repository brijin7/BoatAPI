using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using TTDCAPIv1.Models;

namespace TTDCAPIv1.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class MasterController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_Common"].ConnectionString);
        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

        /*Common Basic Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_CommonReport")]
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

        //Send SMS
        [HttpPost]
        [AllowAnonymous]
        [Route("SendSMSMsg")]
        public IHttpActionResult SendSMSMsg([FromBody] SMSService BtPin)
        {
            try
            {
                if (BtPin.MobileNo != "" && BtPin.ServiceType != "")
                {
                    string sReturn = string.Empty;
                    List<SMSService> li = new List<SMSService>();
                    SqlCommand cmd = new SqlCommand("SaveSMSServiceLogDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@SMSType", "Y");
                    cmd.Parameters.AddWithValue("@ServiceType", BtPin.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", BtPin.MobileNo.Trim());
                    cmd.Parameters.AddWithValue("@MediaType", BtPin.MediaType.Trim());

                    if (BtPin.ServiceType.Trim() != "SignUp" && BtPin.ServiceType.Trim() != "ForgotPwd")
                    {
                        cmd.Parameters.AddWithValue("@BookingId", BtPin.BookingId.Trim());
                        cmd.Parameters.AddWithValue("@BranchId", BtPin.BoatHouseId.Trim());
                        cmd.Parameters.AddWithValue("@BranchName", BtPin.BoatHouseName.Trim());
                        cmd.Parameters.AddWithValue("@Remarks", BtPin.Remarks.Trim());
                    }

                    cmd.Parameters.AddWithValue("@BookingPin", "");

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
                        SMSServiceRes InAppCar = new SMSServiceRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        SMSServiceRes InAppCar = new SMSServiceRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    SMSServiceRes Vehicle = new SMSServiceRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                SMSServiceRes ConfRes = new SMSServiceRes
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

        //Send SMS
        [HttpPost]
        [AllowAnonymous]
        [Route("BlockSMSMsg")]
        public IHttpActionResult BlockSMSMsg([FromBody] BlockSMSService BlckSMS)
        {
            try
            {
                if (BlckSMS.MessageBlock != "" && BlckSMS.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrSmsBlock", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@MessageBlock", BlckSMS.MessageBlock.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", BlckSMS.CreatedBy.Trim());                                    

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
                        BlockSMSServiceRes BlckSMSRes = new BlockSMSServiceRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(BlckSMSRes);
                    }
                    else
                    {
                        BlockSMSServiceRes BlckSMSRes = new BlockSMSServiceRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(BlckSMSRes);
                    }
                }
                else
                {
                    BlockSMSServiceRes BlckSMSRes = new BlockSMSServiceRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(BlckSMSRes);
                }
            }
            catch (Exception ex)
            {
                BlockSMSServiceRes ConfRes = new BlockSMSServiceRes
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


        //Image API
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_ImageAPI")]
        public IHttpActionResult ImageAPI()
        {
            try
            {
                var QueryType = HttpContext.Current.Request.Params["QueryType"];
                var ImageLink = HttpContext.Current.Request.Files["ImageLink"];
                var FormName = HttpContext.Current.Request.Params["FormName"];
                var PrevImageLink = HttpContext.Current.Request.Params["PrevImageLink"];

                if (ImageLink != null && ImageLink.ContentLength > 0)
                {
                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg" };
                    var ext = ImageLink.FileName.Substring(ImageLink.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        ImageUploadRes ImgData = new ImageUploadRes
                        {
                            Response = "Please Upload image of type .jpg, .png, .jpeg, .gif, .bmp, .svg ",
                            StatusCode = 0
                        };
                        return Ok(ImgData);
                    }
                    else
                    {
                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        if (ImageLink.ContentLength > MaxContentLength)
                        {
                            ImageUploadRes ImgData = new ImageUploadRes
                            {
                                Response = "Please Upload a file upto 1 mb.",
                                StatusCode = 0
                            };
                            return Ok(ImgData);
                        }
                        else
                        {

                            string tString = System.DateTime.Now.ToString("yyyyMMddHHmmssss");
                            Random generator = new Random();
                            String rString = generator.Next(0, 999999).ToString("D6");
                            string NewFileName = tString.Trim() + "" + rString.Trim() + extension;

                            var filePath = HttpContext.Current.Server.MapPath("~/Document/" + FormName + "_" + NewFileName);
                            var StorePath = ConfigurationManager.AppSettings["ImageUrl"] + FormName + "_" + NewFileName;


                            if (QueryType == "Insert")
                            {
                                ImageLink.SaveAs(filePath);
                            }
                            else
                            {
                                string filename = string.Empty;
                                filename = Path.GetFileName(PrevImageLink);

                                if (filename == "")
                                {
                                    ImageLink.SaveAs(filePath);
                                }
                                else
                                {
                                    if (File.Exists(HttpContext.Current.Server.MapPath("~/Document/" + filename)))
                                    {
                                        File.Delete(HttpContext.Current.Server.MapPath("~/Document/" + filename));
                                        ImageLink.SaveAs(filePath);
                                    }
                                    else
                                    {
                                        ImageUploadRes ImgData = new ImageUploadRes
                                        {
                                            Response = "Please pass Previous Image Link.",
                                            StatusCode = 0
                                        };
                                        return Ok(ImgData);

                                    }
                                }
                            }
                            ImageUploadRes ConMstr = new ImageUploadRes
                            {
                                Response = StorePath,
                                StatusCode = 1
                            };
                            return Ok(ConMstr);
                        }
                    }

                }

                else
                {
                    ImageUploadRes ImgData = new ImageUploadRes
                    {
                        Response = "Please Upload Image file.",
                        StatusCode = 0
                    };
                    return Ok(ImgData);
                }
            }
            catch (Exception ex)
            {
                ImageUploadRes ImgData = new ImageUploadRes
                {
                    Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ImgData);
            }

        }

        /*Configuration Type */
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_InsConfigType")]
        public IHttpActionResult AssertConfigType([FromBody] FA_ConfigurationType AssConfig)
        {
            try
            {
                if (
                    AssConfig.QueryType != ""
                    && AssConfig.ConfigurationName != ""
                    && AssConfig.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrConfigurationType", con);
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

        /*Configuration Master */
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_InsConfigMstr")]
        public IHttpActionResult MstrConfigurationMaster([FromBody] FA_ConfigurationMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.TypeId != ""
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


        /*Company Master*/
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_CompanyMaster")]
        public IHttpActionResult PostCompanyMaster([FromBody] CompanyMaster InsCompanyMstr)
        {
            try
            {
                if (InsCompanyMstr.QueryType != "" && InsCompanyMstr.CorpID != "" && InsCompanyMstr.CorpName != ""
                    && InsCompanyMstr.ShortName != ""
                    && InsCompanyMstr.AppName != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrCompanyMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsCompanyMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpID", InsCompanyMstr.CorpID.ToString());
                    cmd.Parameters.AddWithValue("@CorpName", InsCompanyMstr.CorpName.ToString());
                    cmd.Parameters.AddWithValue("@ShortName", InsCompanyMstr.ShortName.Trim());
                    cmd.Parameters.AddWithValue("@CorpLogo", InsCompanyMstr.CorpLogo.Trim());
                    cmd.Parameters.AddWithValue("@CorpLogo1", InsCompanyMstr.CorpLogo1.Trim());
                    cmd.Parameters.AddWithValue("@AppName", InsCompanyMstr.AppName.ToString());
                    cmd.Parameters.AddWithValue("@CorpPhoto", InsCompanyMstr.CorpPhoto.Trim());
                    cmd.Parameters.AddWithValue("@CorpPhoto1", InsCompanyMstr.CorpPhoto1.Trim());
                    cmd.Parameters.AddWithValue("@Address1", InsCompanyMstr.Address1.Trim());
                    cmd.Parameters.AddWithValue("@Address2", InsCompanyMstr.Address2.Trim());
                    cmd.Parameters.AddWithValue("@Zipcode", InsCompanyMstr.Zipcode.ToString());
                    cmd.Parameters.AddWithValue("@City", InsCompanyMstr.City.ToString());
                    cmd.Parameters.AddWithValue("@District", InsCompanyMstr.District.Trim());
                    cmd.Parameters.AddWithValue("@State", InsCompanyMstr.State.Trim());
                    cmd.Parameters.AddWithValue("@Country", InsCompanyMstr.Country.ToString());
                    cmd.Parameters.AddWithValue("@Phone1", InsCompanyMstr.Phone1.Trim());
                    cmd.Parameters.AddWithValue("@Phone2", InsCompanyMstr.Phone2.Trim());
                    cmd.Parameters.AddWithValue("@Fax", InsCompanyMstr.Fax.ToString());
                    cmd.Parameters.AddWithValue("@MailId", InsCompanyMstr.MailId.Trim());

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

        /*Branch Master */
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_InsBranchMstr")]
        public IHttpActionResult MstrBranchMaster([FromBody] FA_BranchMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.CorpId != "" && InsConfMstr.BranchCode != ""
                    && InsConfMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBranchMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsConfMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchId", InsConfMstr.BranchId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsConfMstr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@BranchName", InsConfMstr.BranchName.Trim());
                    cmd.Parameters.AddWithValue("@BranchType", InsConfMstr.BranchType.Trim());
                    cmd.Parameters.AddWithValue("@BranchRegion", InsConfMstr.BranchRegion.Trim());
                    cmd.Parameters.AddWithValue("@Address1", InsConfMstr.Address1.Trim());
                    cmd.Parameters.AddWithValue("@Address2", InsConfMstr.Address2.Trim());
                    cmd.Parameters.AddWithValue("@ZipCode", InsConfMstr.ZipCode.Trim());
                    cmd.Parameters.AddWithValue("@City", InsConfMstr.City.Trim());
                    cmd.Parameters.AddWithValue("@District", InsConfMstr.District.Trim());
                    cmd.Parameters.AddWithValue("@State", InsConfMstr.State.Trim());
                    cmd.Parameters.AddWithValue("@Country", InsConfMstr.Country.Trim());
                    cmd.Parameters.AddWithValue("@OperatingStatus", InsConfMstr.OperatingStatus.Trim());
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

        /*Department Master */
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_InsDeptMstr")]
        public IHttpActionResult DeptMstr([FromBody] FA_DepartmentMaster Dept)
        {
            try
            {
                if (Dept.QueryType != "" && Dept.CorpId != "" && Dept.BranchCode != ""
                    && Dept.DeptId != "" && Dept.DeptName != ""
                    && Dept.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrDeptmentMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", Dept.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", Dept.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", Dept.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@DeptId", Dept.DeptId.ToString());
                    cmd.Parameters.AddWithValue("@DeptName", Dept.DeptName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", Dept.CreatedBy.ToString());
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
                        FA_DepartmentMasterRes AssMstr = new FA_DepartmentMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(AssMstr);
                    }
                    else
                    {
                        FA_DepartmentMasterRes AssMstr = new FA_DepartmentMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(AssMstr);
                    }
                }
                else
                {
                    FA_DepartmentMasterRes AssMstr = new FA_DepartmentMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(AssMstr);
                }
            }
            catch (Exception ex)
            {
                FA_DepartmentMasterRes ConfRes = new FA_DepartmentMasterRes
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

        /*Location Master */
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_InsLocnMstr")]
        public IHttpActionResult MstrLocationMaster([FromBody] FA_LocationMaster InsConfMstr)
        {
            try
            {
                if (InsConfMstr.QueryType != "" && InsConfMstr.CorpId != "" && InsConfMstr.BranchCode != ""
                    && InsConfMstr.LocationId != "" && InsConfMstr.LocationName != "" && InsConfMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrLocationMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsConfMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsConfMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", InsConfMstr.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@LocationId", InsConfMstr.LocationId.Trim());
                    cmd.Parameters.AddWithValue("@LocationName", InsConfMstr.LocationName.Trim());
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

        /*Vendor Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_InsVendorMaster")]
        public IHttpActionResult InsVendorMaster([FromBody] FA_VendorMaster InsVendor)
        {
            try
            {
                if (InsVendor.QueryType != "" && InsVendor.CorpId != "" && InsVendor.VendorId != ""
                     && InsVendor.Country != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrVendorMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsVendor.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsVendor.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@VendorId", InsVendor.VendorId.ToString());
                    cmd.Parameters.AddWithValue("@Name", InsVendor.Name.ToString());
                    cmd.Parameters.AddWithValue("@Address1", InsVendor.Address1.ToString());
                    cmd.Parameters.AddWithValue("@Address2", InsVendor.Address2.ToString());
                    cmd.Parameters.AddWithValue("@Zipcode", InsVendor.Zipcode.ToString());
                    cmd.Parameters.AddWithValue("@City", InsVendor.City.ToString());
                    cmd.Parameters.AddWithValue("@District", InsVendor.District.ToString());
                    cmd.Parameters.AddWithValue("@State", InsVendor.State.ToString());
                    cmd.Parameters.AddWithValue("@Country", InsVendor.Country.ToString());
                    cmd.Parameters.AddWithValue("@Mobile", InsVendor.Mobile.ToString());
                    cmd.Parameters.AddWithValue("@Email", InsVendor.Email.ToString());
                    cmd.Parameters.AddWithValue("@PointOfContact", InsVendor.PointOfContact.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsVendor.CreatedBy.ToString());

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
                        FA_VendorMstrRes Invendor = new FA_VendorMstrRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Invendor);
                    }
                    else
                    {
                        FA_VendorMstrRes Invendor = new FA_VendorMstrRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Invendor);
                    }
                }
                else
                {
                    FA_VendorMstrRes Invendor = new FA_VendorMstrRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Invendor);
                }
            }
            catch (Exception ex)
            {
                FA_VendorMstrRes VendorRes = new FA_VendorMstrRes
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

        /*Manufacturer Master Details*/
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_InsManufMstr")]
        public IHttpActionResult MstrManufacturerMaster([FromBody] FA_ManufacturerMaster InsManufMstr)
        {
            try
            {
                if (InsManufMstr.QueryType != "" && InsManufMstr.ManufId != "" && InsManufMstr.ManufName != ""
                    && InsManufMstr.CorpId != "" && InsManufMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrManufacturerMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsManufMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@CorpId", InsManufMstr.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@ManufId", InsManufMstr.ManufId.ToString());
                    cmd.Parameters.AddWithValue("@ManufName", InsManufMstr.ManufName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsManufMstr.CreatedBy.Trim());

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
                        FA_ManufacturerMasterRes ConMstr = new FA_ManufacturerMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        FA_ManufacturerMasterRes ConMstr = new FA_ManufacturerMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    FA_ManufacturerMasterRes Vehicle = new FA_ManufacturerMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                FA_ManufacturerMasterRes ConfRes = new FA_ManufacturerMasterRes
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

        /*Employee Master*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("EmpMstr")]
        public IHttpActionResult MstrEmployeeMasterr([FromBody] EmployeeMaster InsEmpMstr)
        {
            try
            {
                if (InsEmpMstr.QueryType != "" && InsEmpMstr.EmpType != "" && InsEmpMstr.EmpId != "" && InsEmpMstr.EmpFirstName != ""
                    && InsEmpMstr.EmpLastName != "" && InsEmpMstr.EmpDesignation != "" && InsEmpMstr.EmpMobileNo != ""
                    && InsEmpMstr.EmpDOJ != "" && InsEmpMstr.ShiftId != "" && InsEmpMstr.RoleId != ""
                    && InsEmpMstr.UserName != "" && InsEmpMstr.Password != "" && InsEmpMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrEmployeeMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsEmpMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UserId", InsEmpMstr.UserId.ToString());
                    cmd.Parameters.AddWithValue("@EmpId", InsEmpMstr.EmpId.ToString());
                    cmd.Parameters.AddWithValue("@EmpType", InsEmpMstr.EmpType.ToString());
                    cmd.Parameters.AddWithValue("@EmpFirstName", InsEmpMstr.EmpFirstName.ToString());
                    cmd.Parameters.AddWithValue("@EmpLastName", InsEmpMstr.EmpLastName.Trim());
                    cmd.Parameters.AddWithValue("@EmpDesignation", InsEmpMstr.EmpDesignation.ToString());
                    cmd.Parameters.AddWithValue("@BranchId", InsEmpMstr.BranchId.ToString());
                    cmd.Parameters.AddWithValue("@BranchName", InsEmpMstr.BranchName.ToString());
                    cmd.Parameters.AddWithValue("@EmpMobileNo", InsEmpMstr.EmpMobileNo.ToString());

                    cmd.Parameters.AddWithValue("@Address1", InsEmpMstr.Address1.Trim());
                    cmd.Parameters.AddWithValue("@Address2", InsEmpMstr.Address2.Trim());
                    cmd.Parameters.AddWithValue("@City", InsEmpMstr.City.Trim());
                    cmd.Parameters.AddWithValue("@District", InsEmpMstr.District.Trim());
                    cmd.Parameters.AddWithValue("@State", InsEmpMstr.State.Trim());
                    cmd.Parameters.AddWithValue("@ZipCode", InsEmpMstr.ZipCode.Trim());

                    cmd.Parameters.AddWithValue("@EmpDOJ", DateTime.Parse(InsEmpMstr.EmpDOJ.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@EmpAadharId", InsEmpMstr.EmpAadharId.Trim());
                    cmd.Parameters.AddWithValue("@EmpMailId", InsEmpMstr.EmpMailId.Trim());
                    cmd.Parameters.AddWithValue("@EmpPhotoLink", InsEmpMstr.EmpPhotoLink.ToString());
                    cmd.Parameters.AddWithValue("@ShiftId", InsEmpMstr.ShiftId.ToString());
                    cmd.Parameters.AddWithValue("@RoleId", InsEmpMstr.RoleId.ToString());
                    cmd.Parameters.AddWithValue("@UserName", InsEmpMstr.UserName.ToString());
                    cmd.Parameters.AddWithValue("@Password", InsEmpMstr.Password.Trim());
                    cmd.Parameters.AddWithValue("@MobileAppAccess", InsEmpMstr.MobileAppAccess.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsEmpMstr.CreatedBy.Trim());

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


        /*Shift Master*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("ShiftMstr")]
        public IHttpActionResult MstrShiftMaster([FromBody] ShiftMaster InsShiftMstr)
        {
            try
            {
                if (InsShiftMstr.QueryType != ""
                    && InsShiftMstr.ShiftName != "" && InsShiftMstr.StartTime != "" && InsShiftMstr.EndTime != ""
                    && InsShiftMstr.BreakStartTime != "" && InsShiftMstr.BreakEndTime != ""
                    && InsShiftMstr.GracePeriod != "" && InsShiftMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrShiftMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsShiftMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ShiftId", InsShiftMstr.ShiftId.ToString());
                    cmd.Parameters.AddWithValue("@ShiftName", InsShiftMstr.ShiftName.ToString());
                    cmd.Parameters.AddWithValue("@StartTime", InsShiftMstr.StartTime.Trim());
                    cmd.Parameters.AddWithValue("@EndTime", InsShiftMstr.EndTime.Trim());
                    cmd.Parameters.AddWithValue("@BreakStartTime", InsShiftMstr.BreakStartTime.Trim());
                    cmd.Parameters.AddWithValue("@BreakEndTime", InsShiftMstr.BreakEndTime.Trim());
                    cmd.Parameters.AddWithValue("@GracePeriod", InsShiftMstr.GracePeriod.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsShiftMstr.CreatedBy.Trim());

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

        /*Branch Designation Mapping*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_BranchDesgMap")]
        public IHttpActionResult DeptDesgMap([FromBody] BranchDesgMap Department)
        {
            try
            {
                if (Department.QueryType != "" && Department.BranchId != ""
                    && Department.Designation != "" && Department.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBranchDesgMap", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", Department.QueryType.ToString().Trim());
                    cmd.Parameters.AddWithValue("@UniqueId", Department.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@BranchId", Department.BranchId.ToString());
                    cmd.Parameters.AddWithValue("@Designation", Department.Designation.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", Department.CreatedBy.Trim());

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
                        DeptDesgStr InsCE = new DeptDesgStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        DeptDesgStr InsCE = new DeptDesgStr
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    DeptDesgStr InsCE = new DeptDesgStr
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

        /*Module Access Rights*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("ModuleAccess")]
        public IHttpActionResult ModuleAccess([FromBody] ModuleAccess InsUserAcc)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("MstrModuleAccessRights", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", InsUserAcc.QueryType.ToString());
                cmd.Parameters.AddWithValue("@UserId", InsUserAcc.UserId.ToString());
                cmd.Parameters.AddWithValue("@UserName", InsUserAcc.UserName.ToString());
                cmd.Parameters.AddWithValue("@EmpId", InsUserAcc.EmpId.ToString());
                cmd.Parameters.AddWithValue("@EmpName", InsUserAcc.EmpName.ToString());
                cmd.Parameters.AddWithValue("@UserRole", InsUserAcc.UserRole.ToString());
                cmd.Parameters.AddWithValue("@MMaster", InsUserAcc.MMaster.ToString());
                cmd.Parameters.AddWithValue("@MBoating", InsUserAcc.MBoating.ToString());
                cmd.Parameters.AddWithValue("@MHotel", InsUserAcc.MHotel.ToString());
                cmd.Parameters.AddWithValue("@MTour", InsUserAcc.MTour.ToString());
                cmd.Parameters.AddWithValue("@MFixedAssets", InsUserAcc.MFixedAssets.ToString());

                cmd.Parameters.AddWithValue("@MRestaurant", InsUserAcc.MRestaurant.ToString());
                cmd.Parameters.AddWithValue("@MBar", InsUserAcc.MBar.ToString());

                cmd.Parameters.AddWithValue("@CreatedBy", InsUserAcc.CreatedBy.ToString());
                cmd.Parameters.AddWithValue("@BranchId", InsUserAcc.BranchId.ToString());
                cmd.Parameters.AddWithValue("@BranchName", InsUserAcc.BranchName.ToString());

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

        //Login
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("CM_UserLogin")]
        //public IHttpActionResult UserLogin([FromBody] UserLogin Login)
        //{
        //    try
        //    {
        //        string sQuery = string.Empty;

        //        if (Login.UserName != null)
        //        {
        //            if (Login.QueryType.Trim() == "CheckLogin")
        //            {
        //                sQuery = "EXECUTE UserLogin 'CheckLogin', '" + Login.UserName.Trim() + "', '" + Login.Password.Trim() + "' ";
        //            }
        //            else if (Login.QueryType.Trim() == "CheckPwdLogin")
        //            {
        //                sQuery = "EXECUTE UserLogin 'CheckPwdLogin', '" + Login.UserName.Trim() + "', '" + Login.Password.Trim() + "' ";
        //            }
        //            else
        //            {
        //                if (Login.UserName != null && Login.Password != null)
        //                {
        //                    sQuery = "EXECUTE UserLogin 'Login', '" + Login.UserName.Trim() + "', '" + Login.Password.Trim() + "' ";
        //                }
        //                else
        //                {
        //                    UserLoginRes Vehicle = new UserLoginRes
        //                    {
        //                        Response = "Must Pass All Parameters",
        //                        StatusCode = 0
        //                    };
        //                    return Ok(Vehicle);
        //                }
        //            }

        //            List<UserLogin> li = new List<UserLogin>();
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand(sQuery, con);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //            con.Close();
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (dt.Rows[0]["Status"].ToString().Trim() == "Success")
        //                {
        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        UserLogin ShowConfMstr = new UserLogin();
        //                        ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
        //                        ShowConfMstr.EmpId = dt.Rows[i]["EmpId"].ToString();
        //                        ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
        //                        ShowConfMstr.FirstName = dt.Rows[i]["FirstName"].ToString();
        //                        ShowConfMstr.LastName = dt.Rows[i]["LastName"].ToString();

        //                        ShowConfMstr.EmailId = dt.Rows[i]["MailId"].ToString();
        //                        ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
        //                        ShowConfMstr.BranchId = dt.Rows[i]["BranchId"].ToString();
        //                        ShowConfMstr.BranchName = dt.Rows[i]["BranchName"].ToString();
        //                        ShowConfMstr.MobAppAccess = dt.Rows[i]["MobileAppAccess"].ToString();

        //                        ShowConfMstr.UserType = dt.Rows[i]["UserType"].ToString();
        //                        ShowConfMstr.UserRoleId = dt.Rows[i]["UserRoleId"].ToString();
        //                        ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
        //                        ShowConfMstr.Status = dt.Rows[i]["Status"].ToString();
        //                        ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

        //                        ShowConfMstr.SupportUser = dt.Rows[i]["SupportUser"].ToString();
        //                        ShowConfMstr.Designation = dt.Rows[i]["Designation"].ToString();
        //                        ShowConfMstr.DesignationId = dt.Rows[i]["DesignationId"].ToString();

        //                        li.Add(ShowConfMstr);
        //                    }

        //                    UserLoginList ConfList = new UserLoginList
        //                    {
        //                        Response = li,
        //                        StatusCode = 1
        //                    };
        //                    return Ok(ConfList);
        //                }
        //                else
        //                {
        //                    //UserLoginRes ConfRes = new UserLoginRes
        //                    //{
        //                    //    Response = dt.Rows[0]["Status"].ToString().Trim(),
        //                    //    StatusCode = 0
        //                    //};
        //                    //return Ok(ConfRes);

        //                    for (int i = 0; i < dt.Rows.Count; i++)
        //                    {
        //                        UserLogin ShowConfMstr = new UserLogin();
        //                        ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
        //                        ShowConfMstr.EmpId = dt.Rows[i]["EmpId"].ToString();
        //                        ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
        //                        ShowConfMstr.FirstName = dt.Rows[i]["FirstName"].ToString();
        //                        ShowConfMstr.LastName = dt.Rows[i]["LastName"].ToString();

        //                        ShowConfMstr.EmailId = dt.Rows[i]["MailId"].ToString();
        //                        ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
        //                        ShowConfMstr.BranchId = dt.Rows[i]["BranchId"].ToString();
        //                        ShowConfMstr.BranchName = dt.Rows[i]["BranchName"].ToString();
        //                        ShowConfMstr.MobAppAccess = dt.Rows[i]["MobileAppAccess"].ToString();

        //                        ShowConfMstr.UserType = dt.Rows[i]["UserType"].ToString();
        //                        ShowConfMstr.UserRoleId = dt.Rows[i]["UserRoleId"].ToString();
        //                        ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
        //                        ShowConfMstr.Status = dt.Rows[i]["Status"].ToString();
        //                        ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

        //                        ShowConfMstr.SupportUser = dt.Rows[i]["SupportUser"].ToString();
        //                        ShowConfMstr.Designation = dt.Rows[i]["Designation"].ToString();
        //                        ShowConfMstr.DesignationId = dt.Rows[i]["DesignationId"].ToString();

        //                        li.Add(ShowConfMstr);
        //                    }

        //                    UserLoginList ConfList = new UserLoginList
        //                    {
        //                        Response = li,
        //                        StatusCode = 0
        //                    };
        //                    return Ok(ConfList);
        //                }
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
        //        UserLoginRes Vehicle = new UserLoginRes
        //        {
        //            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        //            StatusCode = 0
        //        };
        //        return Ok(Vehicle);
        //    }
        //}

        /// Created by : Vediyappan
        /// Created Date : 30-06-2021 
        ///   <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>


        [HttpPost]
        [AllowAnonymous]
        [Route("CM_UserLogin")]
        public IHttpActionResult UserLogin([FromBody] UserLogin Login)
        {
            try
            {
                string sQuery = string.Empty;

                if (Login.QueryType.Trim() != null && Login.UserName != null && Login.Password != null)
                {
                    if (Login.QueryType.Trim() == "CheckLogin")
                    {
                        sQuery = "EXECUTE UserLogin 'CheckLogin', @UserName, @Password ";
                    }
                    else if (Login.QueryType.Trim() == "CheckPwdLogin")
                    {
                        sQuery = "EXECUTE UserLogin 'CheckPwdLogin', @UserName, @Password ";
                    }
                    else
                    {
                        if (Login.UserName != null && Login.Password != null)
                        {
                            sQuery = "EXECUTE UserLogin 'Login', @UserName, @Password ";
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

                    List<UserLogin> li = new List<UserLogin>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar,50));
                    cmd.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.NVarChar,20));
                    cmd.Parameters["@UserName"].Value = Login.UserName.Trim();
                    cmd.Parameters["@Password"].Value = Login.Password.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Status"].ToString().Trim() == "Success")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                UserLogin ShowConfMstr = new UserLogin();
                                ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
                                ShowConfMstr.EmpId = dt.Rows[i]["EmpId"].ToString();
                                ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
                                ShowConfMstr.FirstName = dt.Rows[i]["FirstName"].ToString();
                                ShowConfMstr.LastName = dt.Rows[i]["LastName"].ToString();

                                ShowConfMstr.EmailId = dt.Rows[i]["MailId"].ToString();
                                ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                                ShowConfMstr.BranchId = dt.Rows[i]["BranchId"].ToString();
                                ShowConfMstr.BranchName = dt.Rows[i]["BranchName"].ToString();
                                ShowConfMstr.MobAppAccess = dt.Rows[i]["MobileAppAccess"].ToString();

                                ShowConfMstr.UserType = dt.Rows[i]["UserType"].ToString();
                                ShowConfMstr.UserRoleId = dt.Rows[i]["UserRoleId"].ToString();
                                ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
                                ShowConfMstr.Status = dt.Rows[i]["Status"].ToString();
                                ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

                                ShowConfMstr.SupportUser = dt.Rows[i]["SupportUser"].ToString();
                                ShowConfMstr.Designation = dt.Rows[i]["Designation"].ToString();
                                ShowConfMstr.DesignationId = dt.Rows[i]["DesignationId"].ToString();
                                ShowConfMstr.CorpId = dt.Rows[i]["CorpId"].ToString();

                                li.Add(ShowConfMstr);
                            }

                            UserLoginList ConfList = new UserLoginList
                            {
                                Response = li,
                                StatusCode = 1
                            };
                            return Ok(ConfList);
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[0]["Status"].ToString().Trim() != "UserName or Password Does Not Match !")
                                {

                                    UserLogin ShowConfMstr = new UserLogin();
                                    ShowConfMstr.UserId = dt.Rows[i]["UserId"].ToString();
                                    ShowConfMstr.EmpId = dt.Rows[i]["EmpId"].ToString();
                                    ShowConfMstr.UserName = dt.Rows[i]["UserName"].ToString();
                                    ShowConfMstr.FirstName = dt.Rows[i]["FirstName"].ToString();
                                    ShowConfMstr.LastName = dt.Rows[i]["LastName"].ToString();

                                    ShowConfMstr.EmailId = dt.Rows[i]["MailId"].ToString();
                                    ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                                    ShowConfMstr.BranchId = dt.Rows[i]["BranchId"].ToString();
                                    ShowConfMstr.BranchName = dt.Rows[i]["BranchName"].ToString();
                                    ShowConfMstr.MobAppAccess = dt.Rows[i]["MobileAppAccess"].ToString();

                                    ShowConfMstr.UserType = dt.Rows[i]["UserType"].ToString();
                                    ShowConfMstr.UserRoleId = dt.Rows[i]["UserRoleId"].ToString();
                                    ShowConfMstr.UserRole = dt.Rows[i]["UserRole"].ToString();
                                    ShowConfMstr.Status = dt.Rows[i]["Status"].ToString();
                                    ShowConfMstr.OfflineRights = dt.Rows[i]["OfflineRights"].ToString();

                                    ShowConfMstr.SupportUser = dt.Rows[i]["SupportUser"].ToString();
                                    ShowConfMstr.Designation = dt.Rows[i]["Designation"].ToString();
                                    ShowConfMstr.DesignationId = dt.Rows[i]["DesignationId"].ToString();
                                    ShowConfMstr.CorpId = dt.Rows[i]["CorpId"].ToString();

                                    li.Add(ShowConfMstr);
                                }
                                else
                                {
                                    UserLoginRes ConfRes = new UserLoginRes
                                    {
                                        Response = dt.Rows[0]["Status"].ToString().Trim(),
                                        StatusCode = 0
                                    };
                                    return Ok(ConfRes);
                                }
                            }

                            UserLoginList ConfList = new UserLoginList
                            {
                                Response = li,
                                StatusCode = 0
                            };
                            return Ok(ConfList);
                        }
                    }
                    else
                    {
                        UserLoginRes ConfRes = new UserLoginRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        if (Login.QueryType.Trim() == "CheckLogin")
                        {
                            return this.StatusCode(HttpStatusCode.NotFound);
                        }
                        else
                        {
                            return Ok(ConfRes);
                        }

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
                UserLoginRes Vehicle = new UserLoginRes
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

        //Send SMS
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_SendSMSMsg")]
        public IHttpActionResult CM_SendSMSMsg([FromBody] SMSService BtPin)
        {
            try
            {
                if (BtPin.MobileNo != "" && BtPin.ServiceType != "")
                {
                    string sReturn = string.Empty;
                    List<SMSService> li = new List<SMSService>();
                    SqlCommand cmd = new SqlCommand("SaveSMSServiceLogDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@SMSType", "Y");
                    cmd.Parameters.AddWithValue("@ServiceType", BtPin.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", BtPin.MobileNo.Trim());
                    cmd.Parameters.AddWithValue("@MediaType", BtPin.MediaType.Trim());

                    if (BtPin.ServiceType.Trim() != "SignUp" && BtPin.ServiceType.Trim() != "ForgotPwd")
                    {
                        cmd.Parameters.AddWithValue("@BookingId", BtPin.BookingId.Trim());
                        cmd.Parameters.AddWithValue("@BranchId", BtPin.BranchId.Trim());
                        cmd.Parameters.AddWithValue("@BranchName", BtPin.BranchName.Trim());
                        cmd.Parameters.AddWithValue("@Remarks", BtPin.Remarks.Trim());
                    }

                    cmd.Parameters.AddWithValue("@BookingPin", "");

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
                        SMSServiceRes InAppCar = new SMSServiceRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InAppCar);
                    }
                    else
                    {
                        SMSServiceRes InAppCar = new SMSServiceRes
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InAppCar);
                    }
                }
                else
                {
                    SMSServiceRes Vehicle = new SMSServiceRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                SMSServiceRes ConfRes = new SMSServiceRes
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


        /*User Registration*/
        //Response = sResult[1].Trim(), //--2021-066-30 commented by vediyappan for return more values.

        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_UserReg")]
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
                            //Response = sResult[1].Trim(), //--2021-066-30 commented by vediyappan for return more values.
                            Response = sResult[1].Trim() + "~" + sResult[2].Trim() + "~" + sResult[3].Trim() + "~" + sResult[4].Trim(),
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

        //Update Pwd
        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        [Route("CM_UpdateUserPwd")]
        public IHttpActionResult UpdateUserPwd([FromBody] UserLogin Login)
        {
            try
            {
                string regex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d!@#$%*_+$\\-]{8,20}$";
                Regex p = new Regex(regex);
                Match m = p.Match(Login.Password);
                if (m.Success)
                {
                    if (Login.UserType != null && Convert.ToString(Login.UserId) != null && Login.Password != null
                        && Login.Password != "")
                    {
                        string sQuery = string.Empty;
                        if (Login.UserType.Trim() == "Department")
                        {
                            sQuery = "Update EmpMaster SET Password = @Password WHERE UserId = @UserId AND ActiveStatus = 'A'";
                        }
                        else if (Login.UserType.Trim() == "Public")
                        {
                            sQuery = "Update PublicProfile SET Password = @Password WHERE UserId = @UserId AND ActiveStatus = 'A'";
                        }

                        string sReturn = string.Empty;
                        SqlCommand cmd = new SqlCommand(sQuery, con);
                        cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("@Password", System.Data.SqlDbType.NVarChar,20));
                        cmd.Parameters["@UserId"].Value = Login.UserId.Trim();
                        cmd.Parameters["@Password"].Value = Login.Password.Trim();

                        con.Open();
                        int iReturn = cmd.ExecuteNonQuery();
                        con.Close();

                        if (iReturn > 0)
                        {
                            UserLoginRes UserRes = new UserLoginRes
                            {
                                Response = "Success",
                                StatusCode = 1
                            };
                            return Ok(UserRes);
                        }
                        else
                        {
                            UserLoginRes UserRes = new UserLoginRes
                            {
                                Response = "Failure",
                                StatusCode = 0
                            };
                            return Ok(UserRes);
                        }
                    }
                    else
                    {
                        UserLoginRes UserRes = new UserLoginRes
                        {
                            Response = "Must Pass All Parameters",
                            StatusCode = 0
                        };
                        return Ok(UserRes);
                    }
                }
                else
                {
                    UserLoginRes UserRes = new UserLoginRes
                    {
                        Response = "Password Must have 8-20 characters Contains atleast 1 Uppercase[A-Z]," +
                        "1 Lowercase[a-z], 1 Number[0-9] and 1 Special character from [ !@#$%*_+- ]",
                        StatusCode = 0
                    };
                    return Ok(UserRes);
                }
            }
            catch (Exception ex)
            {
                UserLoginRes UserRes = new UserLoginRes
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

        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_GetUserIdPwd")]
        public IHttpActionResult GetUserPwsDetails([FromBody] UserLogin Login)
        {
            try
            {
                string sQuery = string.Empty;
                if (Login.UserType.Trim() != null && Login.UserId.Trim() != null)
                {
                    if (Login.UserType.Trim() == "Department")
                    {
                        sQuery = "SELECT UserId, Password, EmpMobileNo AS 'MobileNo' FROM EmpMaster WHERE UserId = @UserId AND ActiveStatus = 'A'";
                    }
                    else if (Login.UserType.Trim() == "Public")
                    {
                        sQuery = "SELECT UserId, Password, MobileNo FROM PublicProfile WHERE UserId = @UserId AND ActiveStatus = 'A'";
                    }

                    List<UserLogin> li = new List<UserLogin>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@UserId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@UserId"].Value = Login.UserId.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UserLogin UserD = new UserLogin();

                            UserD.UserId = dt.Rows[i]["UserId"].ToString();
                            UserD.Password = dt.Rows[i]["Password"].ToString();
                            UserD.MobileNo = dt.Rows[i]["MobileNo"].ToString();
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
                else
                {
                    UserLoginRes UserRes = new UserLoginRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(UserRes);
                }
            }
            catch (Exception ex)
            {
                UserLoginRes UserRes = new UserLoginRes
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

        //User Profile Update
        [HttpPost]
        [AllowAnonymous]
        [Route("UserProfile")]
        public IHttpActionResult UserProfileDetails([FromBody] UserProfile InsUserProf)
        {
            try
            {
                if (InsUserProf.QueryType != "" && InsUserProf.UserId != "" && InsUserProf.MobileNo != ""
                    && InsUserProf.EmailId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("UserProfile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsUserProf.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UserId", InsUserProf.UserId.ToString());
                    cmd.Parameters.AddWithValue("@FirstName", InsUserProf.FirstName.ToString());
                    cmd.Parameters.AddWithValue("@LastName", InsUserProf.LastName.Trim());
                    cmd.Parameters.AddWithValue("@MailId", InsUserProf.EmailId.Trim());
                    cmd.Parameters.AddWithValue("@MobileNo", InsUserProf.MobileNo.Trim());
                    cmd.Parameters.AddWithValue("@Address1", InsUserProf.Address1.Trim());
                    cmd.Parameters.AddWithValue("@Address2", InsUserProf.Address2.Trim());
                    cmd.Parameters.AddWithValue("@City", InsUserProf.City.Trim());
                    cmd.Parameters.AddWithValue("@District", InsUserProf.District.Trim());
                    cmd.Parameters.AddWithValue("@State", InsUserProf.State.Trim());
                    cmd.Parameters.AddWithValue("@ZipCode", InsUserProf.ZipCode.Trim());

                    if (InsUserProf.QueryType.Trim() == "Public")
                    {
                        cmd.Parameters.AddWithValue("@MiddleName", InsUserProf.MiddleName.Trim());
                        cmd.Parameters.AddWithValue("@Gender", InsUserProf.Gender.Trim());

                        if (InsUserProf.DOB.ToString().Trim() == "")
                        {
                            cmd.Parameters.AddWithValue("@DOB", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(InsUserProf.DOB.ToString(), objEnglishDate));
                        }

                        cmd.Parameters.AddWithValue("@MaritalStatus", InsUserProf.MaritalStatus.Trim());

                        if (InsUserProf.MaritalStatus.Trim() == "Single")
                        {
                            cmd.Parameters.AddWithValue("@DOM", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DOM", DateTime.Parse(InsUserProf.DOM.ToString(), objEnglishDate));
                        }

                        cmd.Parameters.AddWithValue("@Country", InsUserProf.Country.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@EmpDOJ", DateTime.Parse(InsUserProf.EmpDOJ.ToString(), objEnglishDate));
                    }

                    cmd.Parameters.AddWithValue("@Aadhaar", InsUserProf.Aadhaar.Trim());
                    cmd.Parameters.AddWithValue("@Photo", InsUserProf.Photo.Trim());
                    cmd.Parameters.AddWithValue("@PromoNotification", InsUserProf.PromoNotification.Trim());

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

        //User Profile by User Id
        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetUserProfile/UserId")]
        public IHttpActionResult GetUserProfile([FromBody] UserProfile bHMstr)
        {
            try
            {
                string sQuery = string.Empty;


                if (bHMstr.UserId != "" && bHMstr.QueryType != "")
                {
                    List<UserProfile> li = new List<UserProfile>();

                    if (bHMstr.QueryType.Trim() == "Public")
                    {
                        sQuery = "SELECT UserId AS 'UserId', UserName, FirstName, MiddleName, LastName, MailId, "
                            + " MobileNo, NULL AS 'MobileAppAccess', 'Public' AS 'UserType', Address1, Address2, Zipcode, City, District, State, Country, "
                            + " PromoNotification AS 'PromoNotification', '01/01/1900' AS 'EmpDOJ', NULL AS 'AadharNo', 0 AS 'UserRoleId', "
                            + " UserPhoto AS 'Photo', Gender, CONVERT(VARCHAR, DOB, 103) AS 'DOB', MaritalStatus, CONVERT(VARCHAR, DOM, 103) AS 'DOM', "
                            + " NULL AS 'SupportUser' FROM PublicProfile "
                            + " WHERE UserId = @UserId AND ActiveStatus = 'A'";
                    }
                    else if (bHMstr.QueryType.Trim() == "Department")
                    {
                        sQuery = "SELECT UserId AS 'UserId', UserName, EmpFirstName AS 'FirstName', NULL AS 'MiddleName', "
                            + " EmpLastName AS 'LastName', EmpMailId AS 'MailId', EmpMobileNo AS 'MobileNo', "
                            + " MobileAppAccess, 'Department' AS 'UserType', Address1, Address2, Zipcode, "
                            + " City, District, State, NULL AS 'Country', "
                            + " NULL AS 'PromoNotification', CONVERT(VARCHAR, EmpDOJ, 105) AS 'EmpDOJ', EmpAadharId AS 'AadharNo', RoleId AS 'UserRoleId', "
                            + " EmpPhotoLink AS 'Photo', NULL AS 'Gender', '01/01/1900' AS 'DOB', NULL AS 'MaritalStatus', '01/01/1900' AS 'DOM', "
                            + " UserType AS 'SupportUser' "
                            + " FROM EmpMaster WHERE UserId = @UserId AND ActiveStatus = 'A'";
                    }

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
                        User1.FirstName = dt.Rows[0]["FirstName"].ToString();
                        User1.MiddleName = dt.Rows[0]["MiddleName"].ToString();
                        User1.LastName = dt.Rows[0]["LastName"].ToString();
                        User1.MobileNo = dt.Rows[0]["MobileNo"].ToString();
                        User1.EmailId = dt.Rows[0]["MailId"].ToString();

                        User1.Address1 = dt.Rows[0]["Address1"].ToString();
                        User1.Address2 = dt.Rows[0]["Address2"].ToString();
                        User1.ZipCode = dt.Rows[0]["Zipcode"].ToString();
                        User1.City = dt.Rows[0]["City"].ToString();
                        User1.District = dt.Rows[0]["District"].ToString();
                        User1.State = dt.Rows[0]["State"].ToString();
                        User1.Country = dt.Rows[0]["Country"].ToString();

                        User1.EmpDOJ = dt.Rows[0]["EmpDOJ"].ToString();
                        User1.Aadhaar = dt.Rows[0]["AadharNo"].ToString();
                        User1.UserRoleId = dt.Rows[0]["UserRoleId"].ToString();
                        User1.Photo = dt.Rows[0]["Photo"].ToString();
                        User1.PromoNotification = dt.Rows[0]["PromoNotification"].ToString();

                        User1.Gender = dt.Rows[0]["Gender"].ToString();
                        User1.DOB = dt.Rows[0]["DOB"].ToString();

                        User1.MaritalStatus = dt.Rows[0]["MaritalStatus"].ToString();
                        User1.DOM = dt.Rows[0]["DOM"].ToString();
                        User1.SupportUser = dt.Rows[0]["SupportUser"].ToString();

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

        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("SupportUser")]
        public IHttpActionResult MstrSupportUser([FromBody] EmployeeMaster InsEmpMstr)
        {
            try
            {
                if (InsEmpMstr.QueryType != "" && InsEmpMstr.EmpId != "" && InsEmpMstr.EmpFirstName != ""
                    && InsEmpMstr.EmpLastName != "" && InsEmpMstr.EmpMobileNo != ""
                    && InsEmpMstr.UserName != "" && InsEmpMstr.Password != "" & InsEmpMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;

                    SqlCommand cmd = new SqlCommand("MstrSupportUserMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsEmpMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UserId", InsEmpMstr.UserId.ToString());
                    cmd.Parameters.AddWithValue("@EmpId", InsEmpMstr.EmpId.ToString());
                    cmd.Parameters.AddWithValue("@EmpFirstName", InsEmpMstr.EmpFirstName.ToString());
                    cmd.Parameters.AddWithValue("@EmpLastName", InsEmpMstr.EmpLastName.Trim());
                    cmd.Parameters.AddWithValue("@EmpMobileNo", InsEmpMstr.EmpMobileNo.ToString());

                    cmd.Parameters.AddWithValue("@EmpMailId", InsEmpMstr.EmpMailId.Trim());
                    cmd.Parameters.AddWithValue("@RoleId", InsEmpMstr.RoleId.ToString());
                    cmd.Parameters.AddWithValue("@UserName", InsEmpMstr.UserName.ToString());
                    cmd.Parameters.AddWithValue("@Password", InsEmpMstr.Password.Trim());
                    cmd.Parameters.AddWithValue("@UserType", InsEmpMstr.UserType.ToString());

                    cmd.Parameters.AddWithValue("@MMaster", InsEmpMstr.MMaster.Trim());
                    cmd.Parameters.AddWithValue("@MBoating", InsEmpMstr.MBoating.ToString());
                    cmd.Parameters.AddWithValue("@MTour", InsEmpMstr.MTour.ToString());
                    cmd.Parameters.AddWithValue("@MHotel", InsEmpMstr.MHotel.Trim());
                    cmd.Parameters.AddWithValue("@MFixedAssets", InsEmpMstr.MFixedAssets.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsEmpMstr.CreatedBy.Trim());

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

        /* Support User Branch Rights */
        [HttpPost]
        [AllowAnonymous]
        [Route("SupportUserBranchRights")]
        public IHttpActionResult MstrSupportUserBranchRights([FromBody] FA_BranchMaster InsMstrOther)
        {
            try
            {
                if (InsMstrOther.QueryType != "" && InsMstrOther.UserId != ""
                    && InsMstrOther.BranchType != "" && InsMstrOther.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrSupportUserBranchRights", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsMstrOther.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UserId", InsMstrOther.UserId.ToString());
                    cmd.Parameters.AddWithValue("@EmpName", InsMstrOther.EmpName.ToString());
                    cmd.Parameters.AddWithValue("@BranchType", InsMstrOther.BranchType.ToString());
                    cmd.Parameters.AddWithValue("@BranchId", InsMstrOther.BranchId.ToString());
                    cmd.Parameters.AddWithValue("@BranchName", InsMstrOther.BranchName.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsMstrOther.CreatedBy.Trim());

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

        /*App Version Details*/
        //App Version Details
        [HttpPost]
        [AllowAnonymous]
        [Route("CMAppVersionDetails")]
        public IHttpActionResult AppVersion([FromBody] AppVersion InsAppVer)
        {
            try
            {
                if (InsAppVer.AppType != "" && InsAppVer.VersionNo != "" && InsAppVer.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrAppVersions", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@AppType", InsAppVer.AppType.ToString());
                    cmd.Parameters.AddWithValue("@VersionNo", InsAppVer.VersionNo.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsAppVer.CreatedBy.Trim());

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
                        AppVersionRes EmMstr = new AppVersionRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(EmMstr);
                    }
                    else
                    {
                        AppVersionRes EmMstr = new AppVersionRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(EmMstr);
                    }
                }
                else
                {
                    AppVersionRes EmMstr = new AppVersionRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
                }
            }
            catch (Exception ex)
            {
                AppVersionRes ConfRes = new AppVersionRes
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
        [Route("CM_AppVersionBindA")]
        public IHttpActionResult AppVersionBindA([FromBody] AppVersion InsAppActive)
        {
            try
            {

                List<AppVersion> li = new List<AppVersion>();
                con.Open();
                string sQuery = "SELECT *," +
                    "convert(varchar(10), CreatedDate, 103) + right(convert(varchar(32), CreatedDate, 100), 8)as CreatedDate1, "
                    + " case when AppType ='A' THEN 'Android' when AppType ='AD' THEN 'Android Department' when AppType ='T' " +
                    " THEN 'TV' when AppType ='I' THEN 'IOS' END AS 'AppType1' "
                    + " from AppVersions where ActiveStatus='A' ORDER BY AppType";
                SqlCommand cmd = new SqlCommand(sQuery, con);
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
                AppVersionRes ConfRes = new AppVersionRes
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
        /// <param name="InsAppActive"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_AppVersionBindD")]
        public IHttpActionResult AppVersionBindD([FromBody] AppVersion InsAppActive)
        {
            try
            {
                List<AppVersion> li = new List<AppVersion>();
                con.Open();
                if (InsAppActive.AppType != "0")
                {
                    string sQuery = "select *, " +
                        "convert(varchar(10), CreatedDate, 103) + right(convert(varchar(32), CreatedDate, 100), 8)as CreatedDate1, "
                        + " convert(varchar(10), UpdatedDate, 103) + right(convert(varchar(32), UpdatedDate, 100), 8) as UpdatedDate1, " +
                        " case when AppType ='A' THEN 'Android' when AppType ='T' THEN 'TV' when AppType ='I' THEN 'IOS' END AS 'AppType1' "
                        + " from AppVersions where ActiveStatus = 'D' and AppType= @AppType ORDER BY AppType ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@AppType", System.Data.SqlDbType.NVarChar,2));
                    cmd.Parameters["@AppType"].Value = InsAppActive.AppType.Trim();

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
                    string sQuery = "select *, " +
                        "convert(varchar(10), CreatedDate, 103) + right(convert(varchar(32), CreatedDate, 100), 8)as CreatedDate1, "
                        + " convert(varchar(10), UpdatedDate, 103) + right(convert(varchar(32), UpdatedDate, 100), 8) as UpdatedDate1, " +
                        " case when AppType ='A' THEN 'Android' when AppType ='T' THEN 'TV' when AppType ='I' THEN 'IOS' END AS 'AppType1' "
                        + " from AppVersions where ActiveStatus = 'D' ORDER BY AppType ";
                    SqlCommand cmd = new SqlCommand(sQuery, con);
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

            }
            catch (Exception ex)
            {
                AppVersionRes ConfRes = new AppVersionRes
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

        //Gateway Master Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("GatewayNameMaster")]
        public IHttpActionResult GatewayNameMaster([FromBody] PaymentUPIDetails InsrPayUPI)
        {
            try
            {
                if (
                    InsrPayUPI.QueryType != "" && InsrPayUPI.GatewayName != "" && InsrPayUPI.MerchantId != ""
                    && InsrPayUPI.AccessCode != "" && InsrPayUPI.WorkingKey != "" && InsrPayUPI.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrPaymentGatewayDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsrPayUPI.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsrPayUPI.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@GatewayName", InsrPayUPI.GatewayName.ToString());

                    cmd.Parameters.AddWithValue("@MerchantId", InsrPayUPI.MerchantId.ToString());
                    cmd.Parameters.AddWithValue("@AccessCode", InsrPayUPI.AccessCode.Trim());
                    cmd.Parameters.AddWithValue("@WorkingKey", InsrPayUPI.WorkingKey.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsrPayUPI.CreatedBy.Trim());

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

        //Payment Rights Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_PaymentRights")]
        public IHttpActionResult PaymentRights([FromBody] PaymentRightsDetails InsrPayUPI)
        {
            try
            {
                if (InsrPayUPI.QueryType != "" && InsrPayUPI.BranchType != "" && InsrPayUPI.BranchName != ""
                    && InsrPayUPI.BlockType != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrOnlinePaymentRights", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsrPayUPI.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsrPayUPI.UniqueId.ToString());

                    cmd.Parameters.AddWithValue("@ApplicationType", InsrPayUPI.ApplicationType.ToString());
                    cmd.Parameters.AddWithValue("@BranchType", InsrPayUPI.BranchType.ToString());

                    cmd.Parameters.AddWithValue("@BranchId", InsrPayUPI.BranchId.ToString());
                    cmd.Parameters.AddWithValue("@BranchName", InsrPayUPI.BranchName.Trim());
                    cmd.Parameters.AddWithValue("@BlockType", InsrPayUPI.BlockType.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsrPayUPI.CreatedBy.Trim());

                    cmd.Parameters.AddWithValue("@ActiveStatus", InsrPayUPI.ActiveStatus.ToString());
                    cmd.Parameters.AddWithValue("@UnBlockReason", InsrPayUPI.UnBlockReason.Trim());
                    cmd.Parameters.AddWithValue("@BlockReason", InsrPayUPI.BlockReason.Trim());

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

        /// <summary>
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="InsAppVer"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_AppVersionChk")]
        public IHttpActionResult AppVersionChk([FromBody] AppVersion InsAppVer)
        {
            try
            {
                if (InsAppVer.AppType != "" && InsAppVer.VersionNo != "" &&
                    InsAppVer.AppType != null && InsAppVer.VersionNo != null)
                {
                    List<AppVersion> li = new List<AppVersion>();
                    con.Open();

                    string sQuery = "SELECT TOP 1 * FROM AppVersions WHERE VersionNo = @VersionNo AND AppType = @AppType" +
                        " AND ActiveStatus = 'A' ORDER BY VersionId DESC";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@VersionNo", System.Data.SqlDbType.NVarChar,25));
                    cmd.Parameters.Add(new SqlParameter("@AppType", System.Data.SqlDbType.NVarChar,2));
                    cmd.Parameters["@VersionNo"].Value = InsAppVer.VersionNo.Trim();
                    cmd.Parameters["@AppType"].Value = InsAppVer.AppType.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    con.Close();
                    if (ds.Rows.Count > 0)
                    {
                        AppVersionRes ResY = new AppVersionRes
                        {
                            Response = "YES",
                            StatusCode = 1
                        };
                        return Ok(ResY);
                    }
                    else
                    {
                        AppVersionRes ResN = new AppVersionRes
                        {
                            Response = "NO",
                            StatusCode = 0
                        };
                        return Ok(ResN);
                    }
                }
                else
                {
                    AppVersionRes EmMstr = new AppVersionRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(EmMstr);
                }
            }
            catch (Exception ex)
            {
                AppVersionRes ConfRes = new AppVersionRes
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
        /// Created Date : 30-09-2021 
        /// </summary>
        /// <param name="GenrtTckt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CM_GenerateManualTicket")]
        public IHttpActionResult GenerateManualTicket([FromBody] BoatTicketDtl GenrtTckt)
        {
            try
            {
                if (GenrtTckt.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("GenerateManualTicketAFTran", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@BoatHouseId", GenrtTckt.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@QueryType", GenrtTckt.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@CategoryValue", GenrtTckt.ServiceType.Trim());
                    if (GenrtTckt.BookingDate != "")
                    {
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Parse(GenrtTckt.BookingDate.Trim().ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CreatedDate", "");
                    }

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
        /// CReated By : Vinitha M
        /// Created Date : 2021-09-30
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Resend"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ReSendSMSMsg")]
        public IHttpActionResult ReSendSMSMsg([FromBody] SMSService Resend)
        {
            try
            {

                if (Resend.BookingId.Trim() != string.Empty && Resend.ReferenceNo.Trim() != string.Empty
                    && Resend.MobileNo.Trim() != string.Empty && Resend.BookingId.Trim() != null &&
                    Resend.ReferenceNo.Trim() != null && Resend.MobileNo.Trim() != null)
                {

                    string sQuery = "SELECT TOP 1 MobileNo,SMSMessage FROM SMSServiceLogDetails WHERE BookingId = @BookingId " +
                        " AND ReferenceNo = @ReferenceNo AND MobileNo = @MobileNo ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BookingId", System.Data.SqlDbType.NVarChar,50));
                    cmd.Parameters.Add(new SqlParameter("@ReferenceNo", System.Data.SqlDbType.NVarChar,50));
                    cmd.Parameters.Add(new SqlParameter("@MobileNo", System.Data.SqlDbType.NVarChar,10));

                    cmd.Parameters["@BookingId"].Value = Resend.BookingId.Trim();
                    cmd.Parameters["@ReferenceNo"].Value = Resend.ReferenceNo.Trim();
                    cmd.Parameters["@MobileNo"].Value = Resend.MobileNo.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        string ExeQuery = "EXECUTE "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.SendSMSServiceMessage @Message = @Msg, @MobileNo = @MobNo";

                        SqlCommand cmdExe = new SqlCommand(ExeQuery, con);

                        cmdExe.Parameters.Add(new SqlParameter("@MobNo", System.Data.SqlDbType.NVarChar,10));
                        cmdExe.Parameters.Add(new SqlParameter("@Msg", System.Data.SqlDbType.NVarChar,500));

                        cmdExe.Parameters["@MobNo"].Value = dt.Rows[0]["MobileNo"].ToString().Trim();
                        cmdExe.Parameters["@Msg"].Value = dt.Rows[0]["SMSMessage"].ToString().Trim();

                        con.Open();
                        cmdExe.ExecuteNonQuery();
                        con.Close();

                        SMSServiceRes ResendSMS = new SMSServiceRes
                        {
                            Response = "Success",
                            StatusCode = 1
                        };
                        return Ok(ResendSMS);
                    }
                    else
                    {
                        SMSServiceRes ResendSMS = new SMSServiceRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ResendSMS);
                    }
                }
                else
                {
                    SMSServiceRes ResendSMS = new SMSServiceRes
                    {
                        Response = "Must Pass All Parameters.",
                        StatusCode = 0
                    };
                    return Ok(ResendSMS);
                }
            }
            catch (Exception ex)
            {
                SMSServiceRes ResendSMS = new SMSServiceRes
                {
                    Response = ex.ToString().Trim(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ResendSMS);
            }
        }


        /// <summary>
        /// CReated By : JayaSuriya
        /// Created Date : 2021-10-04
        /// Modified BY  : Silambarasu
        /// Modified Date : 2023-04-25
        /// Modified into parameterised query
        /// </summary>
        /// <param name="Resend"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [AllowAnonymous]
        [Route("GetSMSMessage")]
        public IHttpActionResult GetSMSMessage([FromBody] SMSService Resend)
        {
            try
            {
                if (Resend.BoatHouseId.Trim() != string.Empty && Resend.ReferenceNo.Trim() != string.Empty
                   && Resend.MobileNo.Trim() != string.Empty && Resend.BoatHouseId.Trim() != null &&
                   Resend.ReferenceNo.Trim() != null && Resend.MobileNo.Trim() != null)
                {
                    string Message = string.Empty;
                    string sQuery = "SELECT TOP 1 SMSMessage FROM SMSServiceLogDetails WHERE ReferenceNo = @ReferenceNo " +
                        "AND BranchId = @BoatHouseId AND MobileNo= @MobileNo ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ReferenceNo", System.Data.SqlDbType.NVarChar,50));
                    cmd.Parameters.Add(new SqlParameter("@MobileNo", System.Data.SqlDbType.NVarChar,10));

                    cmd.Parameters["@BoatHouseId"].Value = Resend.BoatHouseId.Trim();
                    cmd.Parameters["@ReferenceNo"].Value = Resend.ReferenceNo.Trim();
                    cmd.Parameters["@MobileNo"].Value = Resend.MobileNo.Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        Message = dt.Rows[0]["SMSMessage"].ToString().Trim();

                        SMSServiceRes GetSMSMessage = new SMSServiceRes
                        {
                            Response = Message,
                            StatusCode = 1
                        };
                        return Ok(GetSMSMessage);
                    }
                    else
                    {
                        SMSServiceRes GetSMSMessage = new SMSServiceRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(GetSMSMessage);
                    }
                }
                else
                {
                    SMSServiceRes GetSMSMessage = new SMSServiceRes
                    {
                        Response = "Must Pass All Parameters.",
                        StatusCode = 0
                    };
                    return Ok(GetSMSMessage);
                }
            }
            catch (Exception ex)
            {
                SMSServiceRes GetSMSMessage = new SMSServiceRes
                {
                    Response = ex.ToString().Trim(),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(GetSMSMessage);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("CM_BranchOperatingHistory")]
        public IHttpActionResult BranchOpHstry([FromBody] FA_BranchMaster OpHstry)
        {
            try
            {
                if (OpHstry.QueryType != "" && OpHstry.CorpId != "" && OpHstry.BranchCode != "" && OpHstry.OperatingStatus != ""
                     && OpHstry.OperativeDate != "" && OpHstry.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBranchOperatingHistory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", OpHstry.QueryType.ToString().Trim());
                    cmd.Parameters.AddWithValue("@CorpId", OpHstry.CorpId.ToString());
                    cmd.Parameters.AddWithValue("@BranchCode", OpHstry.BranchCode.ToString());
                    cmd.Parameters.AddWithValue("@OperatingStatus", OpHstry.OperatingStatus.ToString());
                    cmd.Parameters.AddWithValue("@OperativeDate", DateTime.Parse(OpHstry.OperativeDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@CreatedBy", OpHstry.CreatedBy.Trim());

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
                        FA_BranchMasterRes InsCE = new FA_BranchMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(InsCE);
                    }
                    else
                    {
                        FA_BranchMasterRes InsCE = new FA_BranchMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(InsCE);
                    }
                }
                else
                {
                    FA_BranchMasterRes InsCE = new FA_BranchMasterRes
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

    }
}

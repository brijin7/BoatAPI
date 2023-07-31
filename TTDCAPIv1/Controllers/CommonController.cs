using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Http;
using TTDCAPIv1.Models;

namespace TTDCAPIv1.Controllers
{
    [RoutePrefix("api")]
    [AllowAnonymous]
    public class CommonController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr_BoatHouse"].ConnectionString);
        IFormatProvider objEnglishDate = new System.Globalization.CultureInfo("en-GB", true);

        //Image API
        [HttpPost]
        [AllowAnonymous]
        [Route("ImageAPI")]
        public IHttpActionResult ImageAPI()
        {
            try
            {
                var QueryType = HttpContext.Current.Request.Params["QueryType"];
                var BoatHouseId = HttpContext.Current.Request.Params["BoatHouseId"];
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

                            var filePath = HttpContext.Current.Server.MapPath("~/Document/" + BoatHouseId + "_" + FormName + "_" + NewFileName);
                            var StorePath = ConfigurationManager.AppSettings["ImageUrl"] + BoatHouseId + "_" + FormName + "_" + NewFileName;


                            if (QueryType == "Insert")
                            {
                                ImageLink.SaveAs(filePath);
                            }
                            else
                            {
                                string filename = string.Empty;
                                filename = Path.GetFileName(PrevImageLink);

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


        /// <summary>
        /// Modified by Brijin and Imran on 20-05-2022
        /// </summary>
        /// <param name="PinDet"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("CommonAPIMethod")]
        public IHttpActionResult CommonAPIMethod([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                if (PinDet.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@UserId", PinDet.UserId.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", PinDet.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(PinDet.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BookingId", PinDet.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();

                    //if (ds != null)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Ok(ds);
                    }
                    else
                    {

                        if (PinDet.QueryType.Trim() == "PrintAdditionalTicketAbstract")
                        {
                            PinDet.QueryType = "PrintAdditionalTicketAbstractOld";
                            return GetCommonPrints(PinDet);
                        }
                        else if (PinDet.QueryType.Trim() == "OtherPrintTicketBulkReceipts")
                        {
                            PinDet.QueryType = "OtherPrintTicketBulkReceiptsOld";
                            return GetCommonPrints(PinDet);
                        }
                        else if (PinDet.QueryType.Trim() == "BoatPrintTicketBulkReceipts")
                        {
                            PinDet.QueryType = "BoatPrintTicketBulkReceiptsHistory";
                            return GetCommonPrints(PinDet);

                        }
                        else if (PinDet.QueryType == "RestaurantTicketBulkReceipts")
                        {
                            PinDet.QueryType = "RestaurantTicketBulkReceiptsHistory";
                            return GetCommonPrints(PinDet);
                        }
                        else
                        {
                            return Ok("No Records Found.");
                        }

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
        //Newly added by Brijin and Imran on 09-05-2022
        public IHttpActionResult GetCommonPrints([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                if (PinDet.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("ShowBoatBookedDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@UserId", PinDet.UserId.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", PinDet.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(PinDet.BookingDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BookingId", PinDet.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());

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

        //Common Report for All purpose
        [HttpPost]
        [AllowAnonymous]
        [Route("CommonReport")]
        public IHttpActionResult CommonReport([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
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

        /*Payment Details*/
        //Payment Account Details Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("PaymentAccDetails")]
        public IHttpActionResult MstrPaymentAccountDetails([FromBody] PaymentAccDetails InsrPayUPI)
        {
            try
            {
                if (
                    InsrPayUPI.QueryType != "" && InsrPayUPI.AccountName != "" && InsrPayUPI.AccountNo != ""
                    && InsrPayUPI.BankIFSCCode != "" && InsrPayUPI.BankName != ""
                    && InsrPayUPI.BranchName != "" && InsrPayUPI.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrPaymentAccountDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsrPayUPI.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsrPayUPI.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@AccountName", InsrPayUPI.AccountName.ToString());
                    cmd.Parameters.AddWithValue("@AccountNo", InsrPayUPI.AccountNo.ToString());
                    cmd.Parameters.AddWithValue("@BankIFSCCode", InsrPayUPI.BankIFSCCode.Trim());

                    cmd.Parameters.AddWithValue("@BankName", InsrPayUPI.BankName.ToString());
                    cmd.Parameters.AddWithValue("@BranchName", InsrPayUPI.BranchName.Trim());
                    cmd.Parameters.AddWithValue("@MICRCode", InsrPayUPI.MICRCode.ToString());

                    cmd.Parameters.AddWithValue("@City", InsrPayUPI.City.ToString());
                    cmd.Parameters.AddWithValue("@District", InsrPayUPI.District.Trim());
                    cmd.Parameters.AddWithValue("@State", InsrPayUPI.State.ToString());
                    cmd.Parameters.AddWithValue("@EntityType", InsrPayUPI.EntityType.ToString());
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

        //Payment UPI Details
        /// <summary>
        /// Modified By : Silambarasu
        /// Modified Date : 27-09-2021
        /// </summary>
        /// <param name="InsrPayUPI"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("PaymentUPIDetails")]
        public IHttpActionResult MstrPaymentUPIDetails([FromBody] PaymentUPIDetails InsrPayUPI)
        {
            try
            {
                if (
                    InsrPayUPI.QueryType != "" && InsrPayUPI.Name != "" && InsrPayUPI.MobileNo != ""
                    && InsrPayUPI.UPIId != "" && InsrPayUPI.EntityType != "" && InsrPayUPI.EntityId != ""
                    && InsrPayUPI.EntityName != "" && InsrPayUPI.CreatedBy != "" && InsrPayUPI.MerchantCode != "" && InsrPayUPI.MerchantId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrPaymentUPIDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsrPayUPI.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsrPayUPI.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@Name", InsrPayUPI.Name.ToString());
                    cmd.Parameters.AddWithValue("@MobileNo", InsrPayUPI.MobileNo.ToString());
                    cmd.Parameters.AddWithValue("@UPIId", InsrPayUPI.UPIId.Trim());
                    cmd.Parameters.AddWithValue("@MerchantCode", InsrPayUPI.MerchantCode.ToString());
                    cmd.Parameters.AddWithValue("@MerchantId", InsrPayUPI.MerchantId.ToString());
                    cmd.Parameters.AddWithValue("@EntityId", InsrPayUPI.EntityId.ToString());
                    cmd.Parameters.AddWithValue("@EntityName", InsrPayUPI.EntityName.Trim());
                    cmd.Parameters.AddWithValue("@EntityType", InsrPayUPI.EntityType.ToString());
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

        //EmailId Password Details Insert
        /// <summary>
        /// Modified By :Subalakshmi
        /// Modified Date : 25-10-2021
        /// </summary>
        /// <param name="InsrEmlPwd"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("EmailIdPwdDetails")]
        public IHttpActionResult EmailIdPwdDetails([FromBody] EmailIdPwd InsrEmlPwd)
        {
            try
            {
                if (InsrEmlPwd.QueryType != "" && InsrEmlPwd.EmailId != "" && InsrEmlPwd.Password != "" && InsrEmlPwd.ServiceType != ""
                    && InsrEmlPwd.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrEmailIdPwdDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsrEmlPwd.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsrEmlPwd.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@EmailId", InsrEmlPwd.EmailId.ToString());
                    cmd.Parameters.AddWithValue("@Password", InsrEmlPwd.Password.ToString());
                    cmd.Parameters.AddWithValue("@ServiceType", InsrEmlPwd.ServiceType.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsrEmlPwd.CreatedBy.Trim());

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

        /*Module Access Rights*/
        //Get Employee Sadmin & Admin
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlEmpUser/Ad")]
        public IHttpActionResult ddlEmpUserAd([FromBody] EmployeeMaster EmpMstr)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (EmpMstr.RoleId != "0")
                {
                    sCondition = " AND RoleId = @RoleId ";
                }
                else
                {
                    sCondition = " AND RoleId IN (1,2) ";
                }

                sQuery = " SELECT EmpID, CONCAT(EmpFirstName, ' ', EmpLastName, '-', EmpID) AS Name  FROM EmpMaster "
                    + " WHERE ActiveStatus='A'";

                sQuery = sQuery + sCondition;

                List<EmployeeMaster> li = new List<EmployeeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);

                cmd.Parameters.Add(new SqlParameter("@RoleId", System.Data.SqlDbType.Int));
                cmd.Parameters["@RoleId"].Value = EmpMstr.RoleId.ToString();

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

        //Get Details based on Employee Id
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlEmpUser/Admin")]
        public IHttpActionResult ConfigMstrDetonTypeAdmin([FromBody] EmployeeMaster EmpMstr)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (EmpMstr.EmpId != "")
                {
                    sCondition = " AND EmpID = @EmpId ";
                }

                sQuery = " SELECT RoleId, BoatHouseId, BoatHouseName FROM EmpMaster WHERE ActiveStatus = 'A' ";

                sQuery = sQuery + sCondition;

                List<EmployeeMaster> li = new List<EmployeeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);

                cmd.Parameters.Add(new SqlParameter("@EmpId", System.Data.SqlDbType.Int));
                cmd.Parameters["@EmpId"].Value = EmpMstr.EmpId;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
        /// <summary>
        /// Modified By : M Vinitha
        /// Modified Date : 22-09-2021
        /// </summary>
        /// <param name="InsUserAcc"></param>
        /// <returns></returns> 
        [HttpPost]
        [AllowAnonymous]
        [Route("AdminUserAccess")]
        public IHttpActionResult AdminUserAccess([FromBody] AdminAccess InsUserAcc)
        {
            try
            {
                string sReturn = string.Empty;
                SqlCommand cmd = new SqlCommand("MstrUserAccessRights", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 10000000;
                cmd.Parameters.AddWithValue("@QueryType", InsUserAcc.QueryType.ToString());
                cmd.Parameters.AddWithValue("@UserId", InsUserAcc.UserId.ToString());
                cmd.Parameters.AddWithValue("@UserName", InsUserAcc.UserName.ToString());
                cmd.Parameters.AddWithValue("@UserRole", InsUserAcc.UserRole.ToString());
                cmd.Parameters.AddWithValue("@MMaster", InsUserAcc.MMaster.ToString());
                cmd.Parameters.AddWithValue("@MBS", InsUserAcc.MBS.ToString());
                cmd.Parameters.AddWithValue("@MTMS", InsUserAcc.MTMS.ToString());
                cmd.Parameters.AddWithValue("@MHMS", InsUserAcc.MHMS.ToString());
                cmd.Parameters.AddWithValue("@MAccounts", InsUserAcc.MAccounts.ToString());

                cmd.Parameters.AddWithValue("@MComMaster", InsUserAcc.MComMaster.ToString());
                cmd.Parameters.AddWithValue("@MBhMaster", InsUserAcc.MBhMaster.ToString());
                cmd.Parameters.AddWithValue("@MHotelMaster", InsUserAcc.MHotelMaster.ToString());
                cmd.Parameters.AddWithValue("@MTourMaster", InsUserAcc.MTourMaster.ToString());
                cmd.Parameters.AddWithValue("@MAccessRights", InsUserAcc.MAccessRights.ToString());
                cmd.Parameters.AddWithValue("@MOtherMaster", InsUserAcc.MOtherMaster.ToString());

                cmd.Parameters.AddWithValue("@BMaster", InsUserAcc.BMaster.ToString());
                cmd.Parameters.AddWithValue("@BTransaction", InsUserAcc.BTransaction.ToString());
                cmd.Parameters.AddWithValue("@BBooking", InsUserAcc.BBooking.ToString());
                cmd.Parameters.AddWithValue("@BReports", InsUserAcc.BReports.ToString());
                cmd.Parameters.AddWithValue("@BRestaurant", InsUserAcc.BRestaurant.ToString());
                cmd.Parameters.AddWithValue("@BGeneratingBoardingPass", InsUserAcc.BGeneratingBoardingPass.ToString());
                cmd.Parameters.AddWithValue("@BGenerateManualTicket", InsUserAcc.BGenerateManualTicket.ToString());

                if (InsUserAcc.BBoatingService.ToString() == "")
                {
                    cmd.Parameters.AddWithValue("@BBoatingService", "NULL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BBoatingService", InsUserAcc.BBoatingService.ToString());
                }

                if (InsUserAcc.BAdditionalService.ToString() == "")
                {
                    cmd.Parameters.AddWithValue("@BAdditionalService", "NULL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BAdditionalService", InsUserAcc.BAdditionalService.ToString());
                }

                if (InsUserAcc.BOtherService.ToString() == "")
                {
                    cmd.Parameters.AddWithValue("@BOtherService", "NULL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BOtherService", InsUserAcc.BOtherService.ToString());
                }

                cmd.Parameters.AddWithValue("@BMBooking", InsUserAcc.BMBooking.ToString());
                cmd.Parameters.AddWithValue("@BMBookingOthers", InsUserAcc.BMBookingOthers.ToString());
                cmd.Parameters.AddWithValue("@BMBulkBooking", InsUserAcc.BMBulkBooking.ToString());
                cmd.Parameters.AddWithValue("@BMAdditionalService", InsUserAcc.BMAdditionalService.ToString());
                cmd.Parameters.AddWithValue("@BMOtherService", InsUserAcc.BMOtherService.ToString());
                cmd.Parameters.AddWithValue("@BMBulkOtherService", InsUserAcc.BMBulkOtherService.ToString());
                cmd.Parameters.AddWithValue("@BMKioskBooking", InsUserAcc.BMKioskBooking.ToString());
                cmd.Parameters.AddWithValue("@BMTripSheet", InsUserAcc.BMTripSheet.ToString());

                //NEWLY ADDED

                cmd.Parameters.AddWithValue("@BMKioskOtherService", InsUserAcc.BMKioskOtherService.ToString());

                //NEWLY ADDED

                if (InsUserAcc.BTripSheetOptions.ToString() == "")
                {
                    cmd.Parameters.AddWithValue("@BTripSheetOptions", "NULL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BTripSheetOptions", InsUserAcc.BTripSheetOptions.ToString());
                }

                cmd.Parameters.AddWithValue("@BMChangeTripSheet", InsUserAcc.BMChangeTripSheet.ToString());
                cmd.Parameters.AddWithValue("@BMBoatReTripDetails", InsUserAcc.BMBoatReTripDetails.ToString());
                cmd.Parameters.AddWithValue("@BMChangeBoatDetails", InsUserAcc.BMChangeBoatDetails.ToString());
                cmd.Parameters.AddWithValue("@BMCancellation", InsUserAcc.BMCancellation.ToString());
                cmd.Parameters.AddWithValue("@BMReSchedule", InsUserAcc.BMReSchedule.ToString());

                cmd.Parameters.AddWithValue("@TMMaterialPur", InsUserAcc.TMMaterialPur.ToString());
                cmd.Parameters.AddWithValue("@TMMaterialIss", InsUserAcc.TMMaterialIss.ToString());
                cmd.Parameters.AddWithValue("@TMTripSheetSettle", InsUserAcc.TMTripSheetSettle.ToString());
                cmd.Parameters.AddWithValue("@TMRowerSettle", InsUserAcc.TMRowerSettle.ToString());
                cmd.Parameters.AddWithValue("@TMRefundCounter", InsUserAcc.TMRefundCounter.ToString());
                cmd.Parameters.AddWithValue("@TMStockEntryMaintance", InsUserAcc.TMStockEntryMaintance.ToString());

                //Newly Added 
                cmd.Parameters.AddWithValue("@TMReceiptBalanceRefund", InsUserAcc.TMReceiptBalanceRefund.ToString());
                //Newly Added 

                if (InsUserAcc.BDepositRefundOptions.ToString() == "")
                {
                    cmd.Parameters.AddWithValue("@BDepositRefundOptions", "NULL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BDepositRefundOptions", InsUserAcc.BDepositRefundOptions.ToString());
                }


                cmd.Parameters.AddWithValue("@RMBooking", InsUserAcc.RMBooking.ToString());
                cmd.Parameters.AddWithValue("@RMOtherSvc", InsUserAcc.RMOtherSvc.ToString());
                cmd.Parameters.AddWithValue("@RMRestaurantService", InsUserAcc.RMRestaurantService.ToString());


                //Newly Added 

                cmd.Parameters.AddWithValue("@RMAdditionalTicket", InsUserAcc.RMAdditionalTicket.ToString());
                cmd.Parameters.AddWithValue("@RMAbstractAdditionalTicket", InsUserAcc.RMAbstractAdditionalTicket.ToString());
                cmd.Parameters.AddWithValue("@RMDepositStatus", InsUserAcc.RMDepositStatus.ToString());
                cmd.Parameters.AddWithValue("@RMDiscountReport", InsUserAcc.RMDiscountReport.ToString());

                cmd.Parameters.AddWithValue("@RMCashinHands", InsUserAcc.RMCashinHands.ToString());
                cmd.Parameters.AddWithValue("@RMExtendedBoatHouse", InsUserAcc.RMExtendedBoatHouse.ToString());
                cmd.Parameters.AddWithValue("@RMPrintBoatBooking", InsUserAcc.RMPrintBoatBooking.ToString());

                cmd.Parameters.AddWithValue("@RMTripWiseDetails", InsUserAcc.RMTripWiseDetails.ToString());
                cmd.Parameters.AddWithValue("@RMReceiptBalance", InsUserAcc.RMReceiptBalance.ToString());
                //cmd.Parameters.AddWithValue("@RMAbstractBooking", InsUserAcc.RMAbstractBooking.ToString());
                cmd.Parameters.AddWithValue("@RMRePrintReport", InsUserAcc.RMRePrintReport.ToString());
                cmd.Parameters.AddWithValue("@RMQRCodeGeneration", InsUserAcc.RMQRCodeGeneration.ToString());



                //Newly Added 

                cmd.Parameters.AddWithValue("@RMAbstractBoatBook", InsUserAcc.RMAbstractBoatBook.ToString());
                cmd.Parameters.AddWithValue("@RMAbstractOthSvc", InsUserAcc.RMAbstractOthSvc.ToString());
                cmd.Parameters.AddWithValue("@RMAbstractResSvc", InsUserAcc.RMAbstractResSvc.ToString());

                cmd.Parameters.AddWithValue("@RMAvailBoatCapacity", InsUserAcc.RMAvailBoatCapacity.ToString());
                cmd.Parameters.AddWithValue("@RMBoatwiseTrip", InsUserAcc.RMBoatwiseTrip.ToString());
                cmd.Parameters.AddWithValue("@RMTripSheetSettle", InsUserAcc.RMTripSheetSettle.ToString());

                cmd.Parameters.AddWithValue("@RMRowerCharges", InsUserAcc.RMRowerCharges.ToString());
                cmd.Parameters.AddWithValue("@RMBoatCancellation", InsUserAcc.RMBoatCancellation.ToString());
                cmd.Parameters.AddWithValue("@RMRowerSettle", InsUserAcc.RMRowerSettle.ToString());

                cmd.Parameters.AddWithValue("@RMChallanRegister", InsUserAcc.RMChallanRegister.ToString());
                //cmd.Parameters.AddWithValue("@RMAbstractChallanRegister", InsUserAcc.RMAbstractChallanRegister.ToString());
                cmd.Parameters.AddWithValue("@RMServiceWiseCollection", InsUserAcc.RMServiceWiseCollection.ToString());

                cmd.Parameters.AddWithValue("@RMUserBookingReport", InsUserAcc.RMUserBookingReport.ToString());
                cmd.Parameters.AddWithValue("@RMTripWiseCollection", InsUserAcc.RMTripWiseCollection.ToString());
                cmd.Parameters.AddWithValue("@RMBoatTypeRowerList", InsUserAcc.RMBoatTypeRowerList.ToString());

                cmd.Parameters.AddWithValue("@OfflineRights", InsUserAcc.OfflineRights.ToString());
                cmd.Parameters.AddWithValue("@MBoatInfoDisplay", InsUserAcc.MBoatInfoDisplay.ToString());

                cmd.Parameters.AddWithValue("@CreatedBy", InsUserAcc.CreatedBy.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseId", InsUserAcc.BoatHouseId.ToString());
                cmd.Parameters.AddWithValue("@BoatHouseName", InsUserAcc.BoatHouseName.ToString());

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

        /*User Access Rights*/
        //Get User Access
        [HttpPost]
        [AllowAnonymous]
        [Route("GetUserAccess")]
        public IHttpActionResult GetUserAccess([FromBody] AdminAccess UserAcc)
        {
            try
            {
                if (UserAcc.QueryType != "" && UserAcc.UserId != "")
                {
                    SqlCommand cmd = new SqlCommand("GetUserAccessDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", UserAcc.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", UserAcc.ServiceType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", UserAcc.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", UserAcc.UserId.Trim());
                    cmd.Parameters.AddWithValue("@UserRole", UserAcc.UserRole.Trim());

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
        /// Created by :Imran
        /// Created Date : 2022-04-21
        /// Version : V2
        /// </summary>
        /// <param name="UserAcc"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetUserAccessV2")]
        public IHttpActionResult GetUserAccessV2([FromBody] AdminAccess UserAcc)
        {
            try
            {
                int endcount = Int32.Parse(UserAcc.CountStart.Trim()) + 9;

                if (UserAcc.QueryType != "" && UserAcc.UserId != "")
                {
                    SqlCommand cmd = new SqlCommand("GetUserAccessDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", UserAcc.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", UserAcc.ServiceType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", UserAcc.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@UserId", UserAcc.UserId.Trim());
                    cmd.Parameters.AddWithValue("@UserRole", UserAcc.UserRole.Trim());
                    cmd.Parameters.AddWithValue("@Input1", UserAcc.CountStart.Trim());
                    cmd.Parameters.AddWithValue("@Input2", endcount);

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

        /*Dropdowns*/
        //Get Configuration Type
        [HttpPost]
        [AllowAnonymous]
        [Route("ConfigMstrList/Type")]
        public IHttpActionResult getConfigMstrListType([FromBody] ConfigurationMaster type)
        {

            try
            {
                if (type.TypeId != null)
                {
                    List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId = @TypeId ", con);

                    cmd.Parameters.Add(new SqlParameter("@TypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@TypeId"].Value = type.TypeId;

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
                    Response = Convert.ToString(ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString()),
                    StatusCode = 0
                };
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        // Get Taxdetails by Tax Id
        [HttpPost]
        [AllowAnonymous]
        [Route("TaxMstr/IdDate")]
        public IHttpActionResult getBTMstrId([FromBody] TaxMaster bHMstr)
        {
            try
            {
                if (bHMstr.ServiceId != "" && bHMstr.ValidDate != "")
                {
                    List<TaxMaster> li = new List<TaxMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT [dbo].[GetTaxIdDetails]('Boat', @ServiceId, "
                        + " @ValidDate, '',@BoatHouseId) AS 'TaxName' ", con);

                    cmd.Parameters.Add(new SqlParameter("@ServiceId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ValidDate", System.Data.SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@ServiceId"].Value = bHMstr.ServiceId;
                    cmd.Parameters["@ValidDate"].Value = DateTime.Parse(bHMstr.ValidDate, objEnglishDate);
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TaxMaster BoatTypeMaster = new TaxMaster();
                            BoatTypeMaster.TaxName = dt.Rows[i]["TaxName"].ToString();

                            li.Add(BoatTypeMaster);
                        }

                        TaxMasterList BoatType = new TaxMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatType);
                    }

                    else
                    {
                        TaxMasterRes BoatType = new TaxMasterRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatType);
                    }
                }
                else
                {
                    TaxMasterRes Vehicle = new TaxMasterRes
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

        //Get Payment Type
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLPayType")]
        public IHttpActionResult getPayType()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId=20", con);
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

        //Get Country
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/ddlCountry")]
        public IHttpActionResult getCountry()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("Select ConfigID,ConfigName from ConfigurationMaster where TypeID='10' ANd ActiveStatus='A'", con);
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

        //Dropdown Config Type
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLConType")]
        public IHttpActionResult getConfigType()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TypeId,TypeName FROM ConfigurationType WHERE ActiveStatus = 'A'", con);
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

        //Get EmpDesignation
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLEmpDesg")]
        public IHttpActionResult getEmpDesig()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId = 18", con);
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

        //Get EmpDepartment
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLEmpDep")]
        public IHttpActionResult getEmpDep()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId=19", con);
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

        //Get LocationName
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlLocation")]
        public IHttpActionResult ddlLocation()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId=15", con);
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

        //Get Boat House
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlBoatHouse/ListAll")]
        public IHttpActionResult ddlBoatHouseAll([FromUri] BoatHouseMaster Boat)
        {
            try
            {
                List<getBoatHouseMaster> li = new List<getBoatHouseMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT BoatHouseId , BoatHouseName FROM BHMaster WHERE ActiveStatus IN('A','D') AND CorpId=@CorpId;", con);
                cmd.Parameters.AddWithValue("@CorpId", Boat.CorpId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        getBoatHouseMaster ShowBoathouseMstr = new getBoatHouseMaster();
                        ShowBoathouseMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowBoathouseMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();

                        li.Add(ShowBoathouseMstr);
                    }

                    getBoatHouseMasterList ConfList = new getBoatHouseMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    getBoatHouseMasterString ConfRes = new getBoatHouseMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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


        //Get Discount / Offer Category
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/OfferCategory")]
        public IHttpActionResult getOfferCat()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId = 30", con);
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

        //Get Offer based on BHId
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlDesig/DeptModules")]
        public IHttpActionResult ddlDesigDept([FromBody] DepartmentModules tx)
        {
            try
            {
                List<DepartmentModules> li = new List<DepartmentModules>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" SELECT A.Designation, B.ConfigName AS 'DesigName' FROM DeptDesgMap AS A "
                            + " INNER JOIN ConfigurationMaster AS B ON A.Designation = B.ConfigID AND A.ActiveStatus = B.ActiveStatus AND B.TypeID = '18' "
                            + " WHERE A.ActiveStatus = 'A' AND A.Department = @DeptId ", con);

                cmd.Parameters.Add(new SqlParameter("@DeptId", System.Data.SqlDbType.Int));
                cmd.Parameters["@DeptId"].Value = tx.DeptId.ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DepartmentModules ShowTaxMaster = new DepartmentModules();
                        ShowTaxMaster.Id = dt.Rows[i]["Designation"].ToString();
                        ShowTaxMaster.Name = dt.Rows[i]["DesigName"].ToString();

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

        //Get Department based on Modules
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlDeptModules")]
        public IHttpActionResult ddlDeptModules()
        {
            try
            {
                List<DepartmentModules> li = new List<DepartmentModules>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" SELECT DISTINCT (A.Department), B.ConfigName AS 'DeptName' FROM DeptDesgMap AS A "
                    + " INNER JOIN ConfigurationMaster AS B ON A.Department = B.ConfigID AND A.ActiveStatus = B.ActiveStatus AND B.TypeID = '19' "
                    + " WHERE A.ActiveStatus = 'A' AND Department = '1' ", con);
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

        //Get Employee Type
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/ddlEmployeeType")]
        public IHttpActionResult ddlEmployeeType()
        {
            try
            {

                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID,ConfigName FROM ConfigurationMaster WHERE TypeID='26' AND ActiveStatus='A';", con);
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

        //Get Shift
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLShift")]
        public IHttpActionResult getShift()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ShiftID, ShiftName FROM ShiftMaster WHERE ActiveStatus = 'A'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowConfMstr = new ConfigurationMaster();
                        ShowConfMstr.TypeId = dt.Rows[i]["ShiftID"].ToString();
                        ShowConfMstr.TypeName = dt.Rows[i]["ShiftName"].ToString();

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

        //Get Role
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLRole")]
        public IHttpActionResult getRole()
        {
            try
            {

                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId = 21", con);
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

        //Get Boat House
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlBoatHouse")]
        public IHttpActionResult ddlBoatHouse([FromUri] BoatHouseMaster BhMas)
        {
            try
            {

                List<getBoatHouseMaster> li = new List<getBoatHouseMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select BoatHouseId , BoatHouseName from BHMaster  where ActiveStatus='A' AND CorpId=@CorpId;", con);
                cmd.Parameters.AddWithValue("@CorpId", BhMas.CorpId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        getBoatHouseMaster ShowBoathouseMstr = new getBoatHouseMaster();
                        ShowBoathouseMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowBoathouseMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();

                        li.Add(ShowBoathouseMstr);
                    }

                    getBoatHouseMasterList ConfList = new getBoatHouseMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    getBoatHouseMasterString ConfRes = new getBoatHouseMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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


        [HttpGet]
        [AllowAnonymous]
        [Route("CheckddlBoatHouse")]
        public IHttpActionResult CheckddlBoatHouse()
        {
            try
            {
                string sQuery = string.Empty;

                //sQuery = "SELECT BoatHouseId, BoatHouseName FROM BHMaster WHERE ActiveStatus = 'A' AND BoatHouseId "
                //        + " NOT IN ("
                //        + " SELECT BranchId FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchOnlinePaymentBlock WHERE BranchType = 'Boating' "
                //        + " AND ApplicationType = 'Public' AND BlockType = 'Both' AND ActiveStatus = 'A' "
                //        + " UNION "
                //        + " SELECT BranchId FROM "+ ConfigurationManager.AppSettings["CommonDB"] +".dbo.BranchOnlinePaymentBlock WHERE BranchType = 'Boating' "
                //        + " AND ApplicationType = 'Public' AND BlockType IN ('Payment Gateway', 'UPI') AND ActiveStatus = 'A' "
                //        + " GROUP BY BranchId HAVING COUNT(*) > 1"
                //        + " )  ORDER BY BoatHouseId";

                sQuery = " SELECT A.BoatHouseId, A.BoatHouseName, ISNULL(B.ClassType,'S') AS 'ClassType' "
                + " FROM ( "
                + " SELECT BoatHouseId, BoatHouseName FROM BHMaster  WHERE ActiveStatus = 'A' AND BoatHouseId "
                + " NOT IN( "
                + " SELECT BranchId FROM " + ConfigurationManager.AppSettings["CommonDB"] + ".dbo.BranchOnlinePaymentBlock WHERE BranchType = 'Boating' "
                + " AND ApplicationType = 'Public' AND BlockType = 'Both' AND ActiveStatus = 'A' "
                + " UNION "
                + " SELECT BranchId FROM " + ConfigurationManager.AppSettings["CommonDB"] + ".dbo.BranchOnlinePaymentBlock WHERE BranchType = 'Boating' "
                + " AND ApplicationType = 'Public' AND BlockType IN('Payment Gateway', 'UPI') AND ActiveStatus = 'A' "
                + " GROUP BY BranchId HAVING COUNT(*) > 1 "
                + " )"
                + " ) AS A "
                + " LEFT JOIN( "
                + "  SELECT BoatHouseId, BoatHouseName, CASE WHEN SUM(BoatPremTotAmt) > 0 THEN 'M' ELSE 'S' END AS 'ClassType' FROM BoatRateMaster "
                + "  GROUP BY BoatHouseId, BoatHouseName "
                + " ) AS B ON A.BoatHouseId = B.BoatHouseId ";


                List<getBoatHouseMaster> li = new List<getBoatHouseMaster>();
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
                        getBoatHouseMaster ShowBoathouseMstr = new getBoatHouseMaster();
                        ShowBoathouseMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowBoathouseMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ShowBoathouseMstr.ClassType = dt.Rows[i]["ClassType"].ToString();

                        li.Add(ShowBoathouseMstr);
                    }

                    getBoatHouseMasterList ConfList = new getBoatHouseMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }

                else
                {
                    getBoatHouseMasterString ConfRes = new getBoatHouseMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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

        //Get Employee User
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlEmpUser")]
        public IHttpActionResult ddlEmpUser([FromBody] EmployeeMaster EmpMstr)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (EmpMstr.RoleId != "0")
                {
                    sCondition = " AND RoleId = @RoleId AND BoatHouseId = @BoatHouseId ";
                }
                else
                {
                    sCondition = " AND RoleId IN (1,2)";
                }

                sQuery = " SELECT EmpID, CONCAT(EmpFirstName, ' ', EmpLastName, '-', EmpID) AS Name  FROM EmpMaster "
                    + " WHERE ActiveStatus='A'";

                sQuery = sQuery + sCondition;

                List<EmployeeMaster> li = new List<EmployeeMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@RoleId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@RoleId"].Value = EmpMstr.RoleId.ToString();
                cmd.Parameters["@BoatHouseId"].Value = EmpMstr.BoatHouseId;
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

        //Get Boat House Based on BHId
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlBoatHouse/BHID")]
        public IHttpActionResult ddlBoatHouseId([FromBody] BoatSeatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatHouseMaster> li = new List<BoatHouseMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select BoatHouseId , BoatHouseName from BHMaster  where ActiveStatus='A' AND BoatHouseId = @BoatHouseId ;", con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatHouseMaster ShowBoathouseMstr = new BoatHouseMaster();
                            ShowBoathouseMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            ShowBoathouseMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();

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

        //Get Boat House/Boat Seater
        [HttpPost]
        [AllowAnonymous]
        [Route("BHMstr/ddlBoatSeater")]
        public IHttpActionResult BHMstrBs([FromBody] BoatSeatMaster boatSeat)
        {
            try
            {
                if (boatSeat.BoatHouseId != "")
                {
                    List<BoatSeatMaster> li = new List<BoatSeatMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select BoatSeaterId , SeaterType from BoatSeat where ActiveStatus='A' AND BoatHouseId = @BoatHouseId ", con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatSeat.BoatHouseId.Trim();
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
                BoatSeatMasterString Vehicle = new BoatSeatMasterString
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

        //Get Boat House/Boat Type
        [HttpPost]
        [AllowAnonymous]
        [Route("BHMstr/ddlBoatType")]
        public IHttpActionResult BHMstrBt([FromBody] BoatTypeMaster boatType)
        {
            try
            {
                if (boatType.BoatHouseId != "")
                {
                    List<BoatTypeMaster> li = new List<BoatTypeMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select BoatTypeId, BoatType from BoatTypes where ActiveStatus='A' AND BoatHouseId = @BoatHouseId", con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
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
                BoatTypeMasterString Vehicle = new BoatTypeMasterString
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

        //Get Payment Types
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLPayModel")]
        public IHttpActionResult getPayModel()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId=17", con);
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

        //Get Boat Status
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLBoatStatus")]
        public IHttpActionResult getBoatStatus()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster Where ActiveStatus='A' And TypeId=16", con);
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

        //Get Group Name
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/ddlGroupName")]
        public IHttpActionResult ddlGroupName()
        {
            try
            {

                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster WHERE TypeID='27' AND ActiveStatus='A';", con);
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
        [Route("ddlAddBoatTicketGroupName")]
        public IHttpActionResult ddlAddBoatTicketGroupName()
        {
            try
            {

                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM ConfigurationMaster WHERE TypeID='34' AND ActiveStatus='A';", con);
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

        //Get Tax Master List based on Service
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlTaxMasterList")]
        public IHttpActionResult ddlTaxMasterList([FromBody] TaxMaster tx)
        {
            try
            {
                List<TaxMaster> li = new List<TaxMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" SELECT TaxId, STUFF((SELECT ', ' + CAST(TaxDescription AS VARCHAR(20)) + '-' + CAST(TaxPercentage AS VARCHAR(20)) [text()] "
                            + " FROM TaxMaster WHERE TaxId = t.TaxId FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ' ') TaxDescription "
                            + " FROM TaxMaster t WHERE t.ActiveStatus = 'A' AND ServiceName = @ServiceName AND BoatHouseId=@BoatHouseId GROUP BY TaxId", con);

                cmd.Parameters.Add(new SqlParameter("@ServiceName", System.Data.SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@ServiceName"].Value = tx.ServiceId.Trim();
                cmd.Parameters["@BoatHouseId"].Value = tx.BoatHouseId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
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

        // Get ItemType
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlItemType")]
        public IHttpActionResult ddlItemType()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select ConfigID, ConfigName from ConfigurationMaster  where ActiveStatus='A' and TypeId=23", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowItemType = new ConfigurationMaster();
                        ShowItemType.ConfigId = dt.Rows[i]["ConfigID"].ToString();
                        ShowItemType.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        li.Add(ShowItemType);
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

        // Get UnitOfMeasure
        [HttpGet]
        [AllowAnonymous]
        [Route("ddlUOM")]
        public IHttpActionResult ddlUOM()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("select ConfigID, ConfigName from ConfigurationMaster  where ActiveStatus='A' and TypeId=22", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ConfigurationMaster ShowUOM = new ConfigurationMaster();
                        ShowUOM.ConfigId = dt.Rows[i]["ConfigID"].ToString();
                        ShowUOM.ConfigName = dt.Rows[i]["ConfigName"].ToString();
                        li.Add(ShowUOM);
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

        //Get Item Master
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlItemMstr/BHId")]
        public IHttpActionResult ddlItemMstrId([FromBody] ItemMaster bHMstr)
        {
            try
            {
                if (bHMstr.EntityId != "")
                {
                    List<ItemMaster> li = new List<ItemMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ItemId,ItemDescription from ItemMaster Where ActiveStatus='A' "
                        + " AND EntityId = @EntityId", con);
                    cmd.Parameters.Add(new SqlParameter("@EntityId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@EntityId"].Value = bHMstr.EntityId;
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
                else
                {
                    ItemMasterString Vehicle = new ItemMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                ItemMasterString ConfRes = new ItemMasterString
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

        //Get Rower Name
        [HttpPost]
        [AllowAnonymous]
        [Route("BHMstr/ddlRowerName")]
        public IHttpActionResult GetRowerName([FromBody] RowerMaster boatType)
        {
            try
            {
                if (boatType.BoatHouseId != "")
                {
                    List<RowerMaster> li = new List<RowerMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select RowerId, RowerName from RowerMaster "
                        + " where ActiveStatus='A' AND BoatHouseId = @BoatHouseId ", con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatType.BoatHouseId.Trim();
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
                BoatTypeMasterString Vehicle = new BoatTypeMasterString
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

        //Get Boat Master based on Boat House
        [HttpPost]
        [AllowAnonymous]
        [Route("ddlBoatMaster/BHId")]
        public IHttpActionResult ddlBoatMaster([FromBody] BoatMstr BM)
        {
            try
            {
                if (BM.BoatHouseId != "" && BM.BoatTypeId != "" && BM.BoatSeaterId != "")
                {
                    List<BoatMstr> li = new List<BoatMstr>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT BoatId, BoatName, BoatNum FROM BoatMaster WHERE BoatStatus = 1 "
                                + " AND BoatHouseId = @BoatHouseId AND BoatTypeId = @BoatTypeId "
                                + " AND BoatSeaterId = @BoatSeaterId;", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BM.BoatHouseId.ToString();
                    cmd.Parameters["@BoatTypeId"].Value = BM.BoatTypeId.ToString();
                    cmd.Parameters["@BoatSeaterId"].Value = BM.BoatSeaterId.ToString();
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

        /*Configuration Master*/
        //Display Configuration List based on Type
        [HttpPost]
        [AllowAnonymous]
        [Route("ConfigMstr/ListAll/Type")]
        public IHttpActionResult ConfigMstrDetonType([FromBody] ConfigurationMaster ConfMstr)
        {
            try
            {
                string sQuery = string.Empty;
                string sCondition = string.Empty;

                if (Convert.ToString(ConfMstr.TypeId) != "")
                {
                    sCondition = " AND A.TypeId = @TypeId ";
                }

                sQuery = " SELECT A.TypeId, B.TypeName, A.ConfigId, A.ConfigName, A.ActiveStatus, A.CreatedBy "
                        + " FROM ConfigurationMaster AS A "
                        + " INNER JOIN ConfigurationType AS B ON A.TypeId = B.TypeId AND B.ActiveStatus = 'A' "
                        + " WHERE A.ActiveStatus IN ('A','D') ";

                sQuery = sQuery + sCondition;

                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@TypeId", System.Data.SqlDbType.Int));
                cmd.Parameters["@TypeId"].Value = ConfMstr.TypeId.ToString();
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
        [Route("ConfigMstr")]
        public IHttpActionResult MstrConfigurationMaster([FromBody] ConfigurationMaster InsConfMstr)
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
                    cmd.Parameters.AddWithValue("@Configname", InsConfMstr.ConfigName.Trim());
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


        /*Tax Master*/
        //Display Tax Master List based on ACTIVE STATUS IN 'T'
        [HttpPost]
        [AllowAnonymous]
        [Route("TaxMstr/AddListAll")]
        public IHttpActionResult ShowAddDetails([FromBody] TaxMaster InsTaxMstr)
        {
            try
            {

                List<TaxMaster> li = new List<TaxMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT UniqueId,ServiceName AS 'ServiceId', CASE WHEN ServiceName = 1 THEN 'Boating' WHEN ServiceName = 2 THEN 'Boating Other Services' "
                + " WHEN ServiceName = 3 THEN 'Hotel' WHEN ServiceName = 4 THEN 'Tour' WHEN ServiceName = 5 THEN 'Restaurant' Else '-'  END as 'ServiceName', "
                + " TaxDescription, TaxPercentage FROM TaxMaster WHERE TaxId=0 AND ActiveStatus='T' AND BoatHouseId=@BoatHouseId", con);
                cmd.Parameters.AddWithValue("@BoatHouseId", InsTaxMstr.BoatHouseId.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TaxMaster amt = new TaxMaster();
                        amt.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                        amt.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                        amt.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        amt.TaxDescription = dt.Rows[i]["TaxDescription"].ToString();
                        amt.TaxPercentage = dt.Rows[i]["TaxPercentage"].ToString();
                        li.Add(amt);
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

        //Display Tax Master List based on ACTIVE STATUS IN 'A'
        [HttpPost]
        [AllowAnonymous]
        [Route("TaxMstr/InsertListAll")]
        public IHttpActionResult ShowTaxDetails([FromBody] TaxMaster InsTaxMstr)
        {
            try
            {
                List<TaxMaster> li = new List<TaxMaster>();
                con.Open();
                string Squery = string.Empty;
                if (InsTaxMstr.BoatHouseId == "")
                {
                    Squery = "select distinct t.[UniqueId],t.TaxId, B.BoatHouseName ,B.BoatHouseId, "
                      + " STUFF((SELECT distinct '  ' + CASE WHEN t1.ServiceName = 1 THEN 'Boating' WHEN t1.ServiceName = 2 THEN 'Boating Other Services' "
                      + " WHEN t1.ServiceName = 3 THEN 'Hotel' WHEN t1.ServiceName = 4 THEN 'Tour' WHEN t1.ServiceName = 5 THEN 'Restaurant' Else '-'  END as 'ServiceName', ' | ' + t1.TaxDescription, "
                      + " ' | ' + CAST(t1.TaxPercentage AS NVARCHAR) "
                      + " from TaxMaster t1 "
                      + " where t.[UniqueId] = t1.[UniqueId] "
                      + " FOR XML PATH(''), TYPE "
                      + " ).value('.', 'NVARCHAR(MAX)') "
                      + " , 1, 2, '') 'TaxDetails',convert(varchar(10), t.EffectiveFrom, 103) AS 'EffectiveFrom', "
                      + " ISNULL(convert(varchar(10), t.EffectiveTill, 103), '-') "
                      + "  AS 'EffectiveTill',t.RefNumber,convert(varchar(10), t.RefDate, 103) AS 'RefDate',t.RefDocumentLink,T.ActiveStatus "
                      + " from TaxMaster t "
                      + " INNER JOIN BHMaster AS B ON t.BoatHouseId = B.BoatHouseId "
                      + " WHERE t.ActiveStatus = 'A'  ";
                }
                else
                {
                    Squery = "select distinct t.[UniqueId],t.TaxId, B.BoatHouseName ,B.BoatHouseId, "
                   + " STUFF((SELECT distinct '  ' + CASE WHEN t1.ServiceName = 1 THEN 'Boating' WHEN t1.ServiceName = 2 THEN 'Boating Other Services' "
                   + " WHEN t1.ServiceName = 3 THEN 'Hotel' WHEN t1.ServiceName = 4 THEN 'Tour' WHEN t1.ServiceName = 5 THEN 'Restaurant' Else '-'  END as 'ServiceName', ' | ' + t1.TaxDescription, "
                   + " ' | ' + CAST(t1.TaxPercentage AS NVARCHAR) "
                   + " from TaxMaster t1 "
                   + " where t.[UniqueId] = t1.[UniqueId] "
                   + " FOR XML PATH(''), TYPE "
                   + " ).value('.', 'NVARCHAR(MAX)') "
                   + " , 1, 2, '') 'TaxDetails',convert(varchar(10), t.EffectiveFrom, 103) AS 'EffectiveFrom', "
                   + " ISNULL(convert(varchar(10), t.EffectiveTill, 103), '-') "
                   + "  AS 'EffectiveTill',t.RefNumber,convert(varchar(10), t.RefDate, 103) AS 'RefDate',t.RefDocumentLink,T.ActiveStatus "
                   + " from TaxMaster t "
                   + " INNER JOIN BHMaster AS B ON t.BoatHouseId = B.BoatHouseId "
                   + " WHERE t.ActiveStatus = 'A' AND t.BoatHouseId=@BoatHouseId ";


                }

                SqlCommand cmd = new SqlCommand(Squery, con);
                if (InsTaxMstr.BoatHouseId != "")
                {
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsTaxMstr.BoatHouseId.Trim());
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TaxMaster amt = new TaxMaster();
                        amt.TaxId = dt.Rows[i]["TaxId"].ToString();
                        amt.ServiceName = dt.Rows[i]["TaxDetails"].ToString();

                        amt.EffectiveFrom = dt.Rows[i]["EffectiveFrom"].ToString();
                        amt.EffectiveTill = dt.Rows[i]["EffectiveTill"].ToString();

                        amt.RefNum = dt.Rows[i]["RefNumber"].ToString();
                        amt.RefDate = dt.Rows[i]["RefDate"].ToString();
                        amt.RefDocLink = dt.Rows[i]["RefDocumentLink"].ToString();
                        amt.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();

                        amt.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        amt.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        li.Add(amt);
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

        //DELETE TEMP RECORDS
        [HttpDelete]
        [AllowAnonymous]
        [Route("TaxMstr/DeleteTemp")]
        public IHttpActionResult Deletetemp()
        {
            try
            {
                List<TaxMaster> li = new List<TaxMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(" DELETE FROM TaxMaster WHERE TaxId='0'  ", con);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {


                    TaxMasterRes ConfList = new TaxMasterRes
                    {
                        Response = "Temp Records Deleted.",
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

        //GET MAXIMUM TAX ID
        public int MaxTaxid()
        {
            int taxid = 0;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TaxId),0)+1 AS 'TaxID' FROM TaxMaster WHERE ActiveStatus='A'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    taxid = Convert.ToInt32(dt.Rows[i]["TaxID"].ToString());

                }

            }
            return taxid;
        }

        //UPDAING TAX ID 
        [HttpPost]
        [AllowAnonymous]
        [Route("TaxMaster/UpdateTaxId")]
        public IHttpActionResult UpdateTaxId([FromBody] TaxMaster taxMaster)
        {
            try
            {
                int Maxid = MaxTaxid();
                List<TaxMaster> li = new List<TaxMaster>();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE TaxMaster SET TaxId = @Maxid WHERE TaxId='0' AND ActiveStatus='A' " +
                    "AND BoatHouseId=@BoatHouseId", con);
                cmd.Parameters.Add(new SqlParameter("@Maxid", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@Maxid"].Value = Maxid;
                cmd.Parameters["@BoatHouseId"].Value = taxMaster.BoatHouseId;
                int i = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (i > 0)
                {
                    TaxMasterRes ConfList = new TaxMasterRes
                    {
                        Response = "Tax Id Updated.",
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    TaxMasterRes ConfRes = new TaxMasterRes
                    {
                        Response = "Tax Id Not Updated.",
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
        [Route("TaxMaster/Add")]
        public IHttpActionResult AddTaxMaster([FromBody] TaxMaster InsTaxMstr)
        {
            try
            {
                if (InsTaxMstr.QueryType != "" && InsTaxMstr.BoatHouseId != "" && InsTaxMstr.ServiceName != "" && InsTaxMstr.TaxDescription != ""
                    && InsTaxMstr.TaxPercentage != "" && InsTaxMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("sp_MstrTaxMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsTaxMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@TaxId", InsTaxMstr.TaxId.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsTaxMstr.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@ServiceName", InsTaxMstr.ServiceName.ToString());
                    cmd.Parameters.AddWithValue("@TaxDescription", InsTaxMstr.TaxDescription.Trim());
                    cmd.Parameters.AddWithValue("@TaxPercentage", InsTaxMstr.TaxPercentage.Trim());
                    cmd.Parameters.AddWithValue("@EffectiveFrom", "1900-01-01");
                    cmd.Parameters.AddWithValue("@EffectiveTill", "1900-01-01");
                    cmd.Parameters.AddWithValue("@RefNum", "0");
                    cmd.Parameters.AddWithValue("@RefDate", "1900-01-01");
                    cmd.Parameters.AddWithValue("@RefDocLink", "-");
                    cmd.Parameters.AddWithValue("@CreatedBy", InsTaxMstr.CreatedBy.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsTaxMstr.BoatHouseId.Trim());

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
                        TaxMasterRes TxMstr = new TaxMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(TxMstr);
                    }
                    else
                    {
                        TaxMasterRes TxMstr = new TaxMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(TxMstr);
                    }
                }
                else
                {
                    TaxMasterRes TxMstr = new TaxMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(TxMstr);
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

        //Tax Master Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("TaxMaster/Insert")]
        public IHttpActionResult InsTaxMaster()
        {
            try
            {
                var QueryType = HttpContext.Current.Request.Params["QueryType"];
                var TaxId = HttpContext.Current.Request.Params["TaxId"];
                var UniqueId = HttpContext.Current.Request.Params["UniqueId"];
                var ServiceName = HttpContext.Current.Request.Params["ServiceName"];
                var TaxDescription = HttpContext.Current.Request.Params["TaxDescription"];
                var EffectiveFrom = HttpContext.Current.Request.Params["EffectiveFrom"];
                var RefNum = HttpContext.Current.Request.Params["RefNum"];
                var RefDate = HttpContext.Current.Request.Params["RefDate"];
                var RefDocLink = HttpContext.Current.Request.Files["RefDocLink"];
                var CreatedBy = HttpContext.Current.Request.Params["CreatedBy"];
                var BoatHouseId = HttpContext.Current.Request.Params["BoatHouseId"];
                if (QueryType != "" && TaxId != "" && UniqueId != "" && ServiceName != ""
                    && TaxDescription != "" && EffectiveFrom != ""
                    && RefNum != "" && RefDate != "" && RefDocLink != null && CreatedBy != "")
                {
                    if (RefDocLink != null && RefDocLink.ContentLength > 0)
                    {
                        IList<string> AllowedFileExtensions = new List<string> { ".txt", ".pdf" };
                        var ext = RefDocLink.FileName.Substring(RefDocLink.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            TaxMasterRes UserData = new TaxMasterRes
                            {
                                Response = "Please Upload document of type .doc,.pdf.",
                                StatusCode = 0
                            };
                            return Ok(UserData);
                        }

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        if (RefDocLink.ContentLength > MaxContentLength)
                        {
                            var message = string.Format("Please Upload a file upto 1 mb.");

                            TaxMasterRes UserData = new TaxMasterRes
                            {
                                Response = "Please Upload a file upto 1 mb.",
                                StatusCode = 0
                            };
                            return Ok(UserData);
                        }
                        string tString = System.DateTime.Now.ToString("yyyyMMddHHmmssss");
                        Random generator = new Random();
                        String rString = generator.Next(0, 999999).ToString("D6");
                        string NewFileName = tString.Trim() + "" + rString.Trim() + extension;

                        string strMappath = "~/Document/";
                        string dirMapPath = HttpContext.Current.Server.MapPath(strMappath);
                        if (!Directory.Exists(dirMapPath))
                        {
                            Directory.CreateDirectory(dirMapPath);
                        }
                        string subFolder = Path.Combine(dirMapPath, "TaxMaster");
                        if (!Directory.Exists(subFolder))
                        {
                            Directory.CreateDirectory(subFolder);
                        }
                        var fileName1 = Path.GetFileNameWithoutExtension(RefDocLink.FileName) + Path.GetExtension(RefDocLink.FileName);


                        var filePath = subFolder + "\\TaxMstr" + NewFileName;
                        var StorePath = ConfigurationManager.AppSettings["TaxDoc"] + "TaxMstr" + NewFileName;



                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        RefDocLink.SaveAs(filePath);



                        string sReturn = string.Empty;
                        SqlCommand cmd = new SqlCommand("sp_MstrTaxMaster", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.CommandTimeout = 10000000;
                        cmd.Parameters.AddWithValue("@QueryType", QueryType.ToString());
                        cmd.Parameters.AddWithValue("@TaxId", "0");
                        cmd.Parameters.AddWithValue("@UniqueId", UniqueId.ToString());
                        cmd.Parameters.AddWithValue("@TaxPercentage", "0.00");

                        cmd.Parameters.AddWithValue("@ServiceName", ServiceName.ToString());
                        cmd.Parameters.AddWithValue("@TaxDescription", TaxDescription.ToString());

                        DateTime DT = DateTime.Parse(EffectiveFrom.Trim(), objEnglishDate);
                        string sEffFrom = DT.ToString("yyyy-MM-dd");

                        cmd.Parameters.AddWithValue("@EffectiveFrom", sEffFrom.ToString());
                        cmd.Parameters.AddWithValue("@EffectiveTill", "1900-01-01");
                        cmd.Parameters.AddWithValue("@RefNum", RefNum.Trim());
                        cmd.Parameters.AddWithValue("@RefDate", DateTime.Parse(RefDate.Trim(), objEnglishDate));
                        cmd.Parameters.AddWithValue("@RefDocLink", StorePath.Trim());
                        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy.Trim());
                        cmd.Parameters.AddWithValue("@BoatHouseId", BoatHouseId.Trim());

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
                            TaxMasterRes TxMstr = new TaxMasterRes
                            {
                                Response = sResult[1].Trim(),
                                StatusCode = 1
                            };
                            return Ok(TxMstr);
                        }
                        else
                        {
                            TaxMasterRes TxMstr = new TaxMasterRes
                            {
                                Response = sResult[1].Trim(),
                                StatusCode = 0
                            };
                            return Ok(TxMstr);
                        }
                    }
                    else
                    {
                        TaxMasterRes TxMstr = new TaxMasterRes
                        {
                            Response = "Upload File",
                            StatusCode = 0
                        };
                        return Ok(TxMstr);
                    }
                }
                else
                {
                    TaxMasterRes TxMstr = new TaxMasterRes
                    {
                        Response = "Must Pass All Parameters.",
                        StatusCode = 0
                    };
                    return Ok(TxMstr);
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

        /*Boat House Master*/
        //Boat House Master Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatHouseMstr")]
        public IHttpActionResult BoatHouseMstr([FromBody] BoatHouseMaster InsBoatHouseMstr)
        {
            try
            {
                if (InsBoatHouseMstr.QueryType != "" && InsBoatHouseMstr.BoatHouseId != ""
                    && InsBoatHouseMstr.BoatHouseName != "" && InsBoatHouseMstr.BoatLocnId != ""
                    && InsBoatHouseMstr.Address1 != "" && InsBoatHouseMstr.Address2 != ""
                    && InsBoatHouseMstr.City != "" && InsBoatHouseMstr.District != ""
                    && InsBoatHouseMstr.State != "" && InsBoatHouseMstr.ZipCode != ""
                    && InsBoatHouseMstr.BookingFrom != "" && InsBoatHouseMstr.BookingTo != ""
                    && InsBoatHouseMstr.WorkingDays != "" && InsBoatHouseMstr.BoatHouseManager != ""
                    && InsBoatHouseMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBoatHouseMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", InsBoatHouseMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBoatHouseMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBoatHouseMstr.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@BoatLocnId", InsBoatHouseMstr.BoatLocnId.Trim());
                    cmd.Parameters.AddWithValue("@Address1", InsBoatHouseMstr.Address1.Trim());

                    cmd.Parameters.AddWithValue("@Address2", InsBoatHouseMstr.Address2.Trim());
                    cmd.Parameters.AddWithValue("@City", InsBoatHouseMstr.City.Trim());
                    cmd.Parameters.AddWithValue("@District", InsBoatHouseMstr.District.Trim());
                    cmd.Parameters.AddWithValue("@State", InsBoatHouseMstr.State.Trim());
                    cmd.Parameters.AddWithValue("@ZipCode", InsBoatHouseMstr.ZipCode.Trim());

                    cmd.Parameters.AddWithValue("@BoatHouseManager", InsBoatHouseMstr.BoatHouseManager.Trim());
                    cmd.Parameters.AddWithValue("@BookingFrom", DateTime.Parse(InsBoatHouseMstr.BookingFrom.Trim()));
                    cmd.Parameters.AddWithValue("@BookingTo", DateTime.Parse(InsBoatHouseMstr.BookingTo.Trim()));
                    cmd.Parameters.AddWithValue("@WorkingDays", InsBoatHouseMstr.WorkingDays.Trim());
                    cmd.Parameters.AddWithValue("@MailId", InsBoatHouseMstr.MailId.Trim());

                    cmd.Parameters.AddWithValue("@MaxChildAge", InsBoatHouseMstr.MaxChildAge.Trim());
                    cmd.Parameters.AddWithValue("@MaxInfantAge", InsBoatHouseMstr.MaxInfantAge.Trim());
                    cmd.Parameters.AddWithValue("@GSTNumber", InsBoatHouseMstr.GSTNumber.Trim());
                    cmd.Parameters.AddWithValue("@TripStartAlertTime", InsBoatHouseMstr.TripStartAlertTime.Trim());
                    cmd.Parameters.AddWithValue("@TripEndAlertTime", InsBoatHouseMstr.TripEndAlertTime.Trim());

                    cmd.Parameters.AddWithValue("@RefundDuration", InsBoatHouseMstr.RefundDuration.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsBoatHouseMstr.CreatedBy.Trim());
                    cmd.Parameters.AddWithValue("@ReprintTime", InsBoatHouseMstr.ReprintTime.Trim());

                    if (InsBoatHouseMstr.AutoEndForNoDeposite != null)
                    {
                        cmd.Parameters.AddWithValue("@AutoEndForNoDeposite", InsBoatHouseMstr.AutoEndForNoDeposite.Trim());
                    }

                    if (InsBoatHouseMstr.QRcodeGenerate != null)
                    {
                        cmd.Parameters.AddWithValue("@QRcodeGenerate", InsBoatHouseMstr.QRcodeGenerate.Trim());
                    }
                    if (InsBoatHouseMstr.ExtensionPrint != null)
                    {
                        cmd.Parameters.AddWithValue("@ExtensionPrint", InsBoatHouseMstr.ExtensionPrint.Trim());
                    }
                    if (InsBoatHouseMstr.ExtnChargeStatus != null)
                    {
                        cmd.Parameters.AddWithValue("@ExtnChargeStatus", InsBoatHouseMstr.ExtnChargeStatus.Trim());
                    }

                    cmd.Parameters.AddWithValue("@BHShortCode", InsBoatHouseMstr.BHShortCode.Trim());
                    cmd.Parameters.AddWithValue("@CorpId", InsBoatHouseMstr.CorpId.Trim());

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
                        TaxMasterRes TxMstr = new TaxMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(TxMstr);
                    }
                    else
                    {
                        TaxMasterRes TxMstr = new TaxMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(TxMstr);
                    }
                }
                else
                {
                    TaxMasterRes TxMstr = new TaxMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(TxMstr);
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


        //Boat House Master List
        /// <summary>
        /// changed by Imran on 2021-11-10
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("BoatHouseMstr/ListAll")]
        public IHttpActionResult GetBoatHouseMaster()
        {
            try
            {
                List<BoatHouseMaster> li = new List<BoatHouseMaster>();

                SqlCommand cmd = new SqlCommand("SELECT A.CorpId, A.BoatHouseId, A.BoatHouseName, A.BoatLocnId, C.ConfigName, A.Address1, A.Address2, A.Zipcode, A.City, A.District, "
                       + " A.State, A.BoatHouseManager, NULL AS 'BoatHouseManagerUserName', A.BookingFrom, A.BookingTo, A.WorkingDays, A.MailId, "
                       + " A.MaxChildAge, A.MaxInfantAge, A.ActiveStatus, A.GSTNumber,A.ExtensionPrint,A.AutoEndForNoDeposite,A.QRcodeGenerate, "
                       + " A.TripStartAlertTime, A.TripEndAlertTime, A.RefundDuration, A.BHShortCode,A.ExtnChargeStatus, A.ReprintTime FROM BHMaster AS A "
                       + " INNER JOIN ConfigurationMaster AS C ON A.BoatLocnId = C.ConfigId AND C.TypeId = 15 AND  "
                       + " C.ActiveStatus = 'A' WHERE A.ActiveStatus IN ('A','D') ", con);

                //SqlCommand cmd = new SqlCommand("SELECT A.BoatHouseId, A.BoatHouseName, A.BoatLocnId, C.ConfigName, A.Address1, A.Address2, A.Zipcode, A.City, A.District, "
                //        + " A.State, A.BoatHouseManager, NULL AS 'BoatHouseManagerUserName', A.BookingFrom, A.BookingTo, A.WorkingDays, A.MailId, "
                //        + " A.MaxChildAge, A.MaxInfantAge, A.ActiveStatus, A.GSTNumber FROM BHMaster AS A "
                //        + " INNER JOIN ConfigurationMaster AS C ON A.BoatLocnId = C.ConfigId AND C.TypeId = 15 AND  "
                //        + " C.ActiveStatus = 'A' WHERE A.ActiveStatus IN ('A','D') ", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BoatHouseMaster BoatHouseMaster = new BoatHouseMaster();

                        BoatHouseMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        BoatHouseMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        BoatHouseMaster.BoatLocnId = dt.Rows[i]["BoatLocnId"].ToString();
                        BoatHouseMaster.BoatLocnName = dt.Rows[i]["ConfigName"].ToString();
                        BoatHouseMaster.Address1 = dt.Rows[i]["Address1"].ToString();
                        BoatHouseMaster.Address2 = dt.Rows[i]["Address2"].ToString();
                        BoatHouseMaster.City = dt.Rows[i]["City"].ToString();
                        BoatHouseMaster.State = dt.Rows[i]["State"].ToString();
                        BoatHouseMaster.District = dt.Rows[i]["District"].ToString();
                        BoatHouseMaster.ZipCode = dt.Rows[i]["Zipcode"].ToString();
                        BoatHouseMaster.BoatHouseManager = dt.Rows[i]["BoatHouseManager"].ToString();
                        //BoatHouseMaster.BoatHouseManagerUserName = dt.Rows[i]["BoatHouseManagerUserName"].ToString();
                        BoatHouseMaster.BookingFrom = dt.Rows[i]["BookingFrom"].ToString();
                        BoatHouseMaster.BookingTo = dt.Rows[i]["BookingTo"].ToString();
                        BoatHouseMaster.WorkingDays = dt.Rows[i]["WorkingDays"].ToString();
                        BoatHouseMaster.MailId = dt.Rows[i]["MailId"].ToString();
                        BoatHouseMaster.MaxChildAge = dt.Rows[i]["MaxChildAge"].ToString();
                        BoatHouseMaster.MaxInfantAge = dt.Rows[i]["MaxInfantAge"].ToString();
                        BoatHouseMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        BoatHouseMaster.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();
                        BoatHouseMaster.TripStartAlertTime = dt.Rows[i]["TripStartAlertTime"].ToString();
                        BoatHouseMaster.TripEndAlertTime = dt.Rows[i]["TripEndAlertTime"].ToString();
                        BoatHouseMaster.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                        BoatHouseMaster.ReprintTime = dt.Rows[i]["ReprintTime"].ToString();
                        BoatHouseMaster.AutoEndForNoDeposite = dt.Rows[i]["AutoEndForNoDeposite"].ToString();
                        BoatHouseMaster.QRcodeGenerate = dt.Rows[i]["QRcodeGenerate"].ToString();
                        BoatHouseMaster.ExtensionPrint = dt.Rows[i]["ExtensionPrint"].ToString();
                        BoatHouseMaster.ExtnChargeStatus = dt.Rows[i]["ExtnChargeStatus"].ToString();
                        BoatHouseMaster.BHShortCode = dt.Rows[i]["BHShortCode"].ToString();
                        BoatHouseMaster.CorpId = dt.Rows[i]["CorpId"].ToString();
                        li.Add(BoatHouseMaster);
                    }

                    BoatHouseMasterList BoatHouse = new BoatHouseMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(BoatHouse);
                }

                else
                {
                    BoatHouseMasterString BoatHouse = new BoatHouseMasterString
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

        //Get Boat House Master based on Boat House Id
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatHouseMstr/BHId")]
        public IHttpActionResult getBHMstrId([FromBody] BoatHouseMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatHouseMaster> li = new List<BoatHouseMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT A.ExtnChargeStatus,A.ExtensionPrint,A.AutoEndForNoDeposite,"
                        + " A.QRcodeGenerate, A.CorpId, A.BoatHouseId, A.BoatHouseName, A.BoatLocnId, C.ConfigName, A.Address1, "
                        + " A.Address2, A.Zipcode, A.City, A.District, A.State, A.BoatHouseManager, NULL AS 'BoatHouseManagerUserName',"
                        + " A.BookingFrom, A.BookingTo, A.WorkingDays, A.MailId, A.MaxChildAge, A.MaxInfantAge, A.GSTNumber, "
                        + " A.TripStartAlertTime, A.TripEndAlertTime, A.RefundDuration, A.BHShortCode,   A.ReprintTime "
                        + " FROM BHMaster AS A "
                        + " INNER JOIN ConfigurationMaster AS C ON A.BoatLocnId = C.ConfigId AND C.TypeId = 15 AND  "
                        + " A.ActiveStatus = C.ActiveStatus WHERE A.ActiveStatus = 'A' AND A.BoatHouseId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatHouseMaster BoatHouseMaster = new BoatHouseMaster();

                            BoatHouseMaster.CorpId = dt.Rows[i]["CorpId"].ToString();

                            BoatHouseMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            BoatHouseMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            BoatHouseMaster.BoatLocnId = dt.Rows[i]["BoatLocnId"].ToString();
                            BoatHouseMaster.BoatLocnName = dt.Rows[i]["ConfigName"].ToString();
                            BoatHouseMaster.Address1 = dt.Rows[i]["Address1"].ToString();
                            BoatHouseMaster.Address2 = dt.Rows[i]["Address2"].ToString();
                            BoatHouseMaster.City = dt.Rows[i]["City"].ToString();
                            BoatHouseMaster.State = dt.Rows[i]["State"].ToString();
                            BoatHouseMaster.District = dt.Rows[i]["District"].ToString();
                            BoatHouseMaster.ZipCode = dt.Rows[i]["Zipcode"].ToString();
                            BoatHouseMaster.BoatHouseManager = dt.Rows[i]["BoatHouseManager"].ToString();
                            //BoatHouseMaster.BoatHouseManagerUserName = dt.Rows[i]["BoatHouseManagerUserName"].ToString();
                            BoatHouseMaster.BookingFrom = dt.Rows[i]["BookingFrom"].ToString();
                            BoatHouseMaster.BookingTo = dt.Rows[i]["BookingTo"].ToString();
                            BoatHouseMaster.WorkingDays = dt.Rows[i]["WorkingDays"].ToString();
                            BoatHouseMaster.MailId = dt.Rows[i]["MailId"].ToString();
                            BoatHouseMaster.MaxChildAge = dt.Rows[i]["MaxChildAge"].ToString();
                            BoatHouseMaster.MaxInfantAge = dt.Rows[i]["MaxInfantAge"].ToString();
                            BoatHouseMaster.GSTNumber = dt.Rows[i]["GSTNumber"].ToString();
                            BoatHouseMaster.TripStartAlertTime = dt.Rows[i]["TripStartAlertTime"].ToString();
                            BoatHouseMaster.TripEndAlertTime = dt.Rows[i]["TripEndAlertTime"].ToString();
                            BoatHouseMaster.RefundDuration = dt.Rows[i]["RefundDuration"].ToString();
                            BoatHouseMaster.ReprintTime = dt.Rows[i]["ReprintTime"].ToString();
                            BoatHouseMaster.AutoEndForNoDeposite = dt.Rows[i]["AutoEndForNoDeposite"].ToString();
                            BoatHouseMaster.QRcodeGenerate = dt.Rows[i]["QRcodeGenerate"].ToString();
                            BoatHouseMaster.ExtensionPrint = dt.Rows[i]["ExtensionPrint"].ToString();
                            BoatHouseMaster.ExtnChargeStatus = dt.Rows[i]["ExtnChargeStatus"].ToString();
                            BoatHouseMaster.BHShortCode = dt.Rows[i]["BHShortCode"].ToString();

                            li.Add(BoatHouseMaster);
                        }

                        BoatHouseMasterList BoatHouse = new BoatHouseMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatHouse);
                    }

                    else
                    {
                        BoatHouseMasterString BoatHouse = new BoatHouseMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatHouse);
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


        /*Offer Master*/
        // Offer Master List
        [HttpGet]
        [AllowAnonymous]
        [Route("OfferMstr/ListAll")]
        public IHttpActionResult OfferMaster()
        {
            try
            {
                List<OfferMaster> li = new List<OfferMaster>();

                SqlCommand cmd = new SqlCommand(" SELECT A.OfferId, A.OfferType, A.OfferCategory, B.ConfigName AS 'OfferCategoryName', "
                        + " A.OfferName, A.AmountType, A.Offer, A.MinBillAmount, "
                        + " A.MinNoOfTickets, Convert(Nvarchar(20), A.EffectiveFrom,105) 'EffectiveFrom', "
                        + " Convert(Nvarchar(20), A.EffectiveTill, 105) 'EffectiveTill', A.ActiveStatus, "
                        + " A.BoatHouseId, A.BoatHouseName, A.Createdby, Convert(Nvarchar(20), A.CreatedDate, 105) 'CreatedDate', "
                        + " A.Updatedby, Convert(Nvarchar(20), A.UpdatedDate, 105) 'UpdatedDate',A.CorpId from OfferMaster AS A "
                        + " INNER JOIN ConfigurationMaster AS B ON A.OfferCategory = B.ConfigID AND B.TypeId = 30 "
                        + " Where A.ActiveStatus IN('A', 'D') ", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OfferMaster OfferMasters = new OfferMaster();

                        OfferMasters.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        OfferMasters.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        OfferMasters.OfferId = dt.Rows[i]["OfferId"].ToString();
                        OfferMasters.OfferType = dt.Rows[i]["OfferType"].ToString();
                        OfferMasters.OfferCategory = dt.Rows[i]["OfferCategory"].ToString();
                        OfferMasters.OfferCategoryName = dt.Rows[i]["OfferCategoryName"].ToString();
                        OfferMasters.OfferName = dt.Rows[i]["OfferName"].ToString();
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
                        OfferMasters.CorpId = dt.Rows[i]["CorpId"].ToString();

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
        [Route("OfferDiscount")]
        public IHttpActionResult MstrOfferDiscount([FromBody] OfferMaster InsOfferDiscount)
        {
            try
            {
                if (InsOfferDiscount.QueryType != "" && InsOfferDiscount.OfferId != "" && InsOfferDiscount.OfferName != ""
                    && InsOfferDiscount.BoatHouseId != "" && InsOfferDiscount.BoatHouseName != ""
                    && InsOfferDiscount.OfferType != "" && InsOfferDiscount.AmountType != ""
                    && InsOfferDiscount.OfferAmount != "" && InsOfferDiscount.MinBillAmount != ""
                    && InsOfferDiscount.MinNoOfTickets != "" && InsOfferDiscount.EffectiveFrom != ""
                    && InsOfferDiscount.EffectiveTill != "" && InsOfferDiscount.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrOfferDiscount", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsOfferDiscount.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@OfferId", InsOfferDiscount.OfferId.ToString());
                    cmd.Parameters.AddWithValue("@OfferType", InsOfferDiscount.OfferType.ToString());
                    cmd.Parameters.AddWithValue("@OfferCategory", InsOfferDiscount.OfferCategory.ToString());
                    cmd.Parameters.AddWithValue("@OfferName", InsOfferDiscount.OfferName.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsOfferDiscount.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsOfferDiscount.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@AmountType", InsOfferDiscount.AmountType.Trim());
                    cmd.Parameters.AddWithValue("@OfferAmount", InsOfferDiscount.OfferAmount.ToString());
                    cmd.Parameters.AddWithValue("@MinBillAmount", InsOfferDiscount.MinBillAmount.ToString());
                    cmd.Parameters.AddWithValue("@MinTicket", InsOfferDiscount.MinNoOfTickets.ToString());
                    cmd.Parameters.AddWithValue("@EffectiveFrom", DateTime.Parse(InsOfferDiscount.EffectiveFrom.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@EffectiveTill", DateTime.Parse(InsOfferDiscount.EffectiveTill.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@CreatedBy", InsOfferDiscount.Createdby.Trim());
                    cmd.Parameters.AddWithValue("@CorpId", InsOfferDiscount.CorpId.Trim());

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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        /*Boat Type Master*/
        //Get Boat Type based on Boat House Id
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatTypeMstr/BHId")]
        public IHttpActionResult getBTMstrId([FromBody] BoatTypeMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatTypeMaster> li = new List<BoatTypeMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT A.BoatTypeId,A.BoatType,A.BoatHouseId,B.BoatHouseName, A.ActiveStatus FROM BoatTypes AS A "
                        + " INNER JOIN BHMaster AS B ON A.BoatHouseId=B.BoatHouseId AND B.ActiveStatus = 'A' "
                        + " WHERE A.ActiveStatus IN ('A','D') AND A.BoatHouseId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
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
        [Route("BoatTypeMaster")]
        public IHttpActionResult mstrBoatTypeMaster([FromBody] BoatTypeMaster InsBoatTypeMaster)
        {
            try
            {
                if (InsBoatTypeMaster.QueryType != "" && InsBoatTypeMaster.BoatTypeId != ""
                    && InsBoatTypeMaster.BoatType != "" && InsBoatTypeMaster.BoatHouseId != ""
                    && InsBoatTypeMaster.BoatHouseName != "" && InsBoatTypeMaster.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBoatTypeMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBoatTypeMaster.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsBoatTypeMaster.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatType", InsBoatTypeMaster.BoatType.ToString());
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
                        BoatTypeMasterString Boattype = new BoatTypeMasterString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(Boattype);
                    }
                    else
                    {
                        BoatTypeMasterString Boattype = new BoatTypeMasterString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(Boattype);
                    }
                }
                else
                {
                    BoatTypeMasterString Boattype = new BoatTypeMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Boattype);
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

        /*Boat Seat Master*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatSeatMstr")]
        public IHttpActionResult MstrBoatSeaterMaster([FromBody] BoatSeatMaster InsBSMstr)
        {
            try
            {
                if (InsBSMstr.QueryType != "" && InsBSMstr.BoatSeaterId != ""
                    && InsBSMstr.SeaterType != "" && InsBSMstr.BoatHouseId != ""
                    && InsBSMstr.BoatHouseName != "" && InsBSMstr.NoOfSeats != ""
                    && InsBSMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBoatSeaterMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBSMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", InsBSMstr.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@SeaterType", InsBSMstr.SeaterType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBSMstr.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBSMstr.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@NoOfSeats", InsBSMstr.NoOfSeats.Trim());
                    cmd.Parameters.AddWithValue("@AllowedNoOfSeats", InsBSMstr.AllowedNoOfSeats.Trim());
                    cmd.Parameters.AddWithValue("@RestrictionReason", InsBSMstr.RestrictionReason.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsBSMstr.CreatedBy.Trim());



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

        //Get Boat Seat based on Boat House Id
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatSeaterMstr/BHId")]
        public IHttpActionResult getBSMastrId([FromBody] BoatSeatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatSeatMaster> li = new List<BoatSeatMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT A.BoatSeaterId,A.SeaterType,A.BoatHouseId,B.BoatHouseName,A.NoOfSeats,ISNULL(A.AllowedNoOfSeats,'-') AS 'AllowedNoOfSeats', "
                        + " ISNULL(A.RestrictionReason, '-') AS 'RestrictionReason',  A.ActiveStatus FROM BoatSeat AS A "
                        + " INNER JOIN BHMaster AS B ON A.BoatHouseId=B.BoatHouseId AND B.ActiveStatus = 'A' "
                        + " WHERE A.ActiveStatus IN('A','D') AND A.BoatHouseId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.Trim();
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

                            BoatSeatMaster.AllowedNoOfSeats = dt.Rows[i]["AllowedNoOfSeats"].ToString();
                            BoatSeatMaster.RestrictionReason = dt.Rows[i]["RestrictionReason"].ToString();
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
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
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

        /*Boat Rate Master*/
        //Get Boat Seat based on Boat Type
        //2021-07-01 Changes by vedi
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRate/Seat")]
        public IHttpActionResult Seat([FromBody] BoatSeatMaster tx)
        {
            try
            {
                List<BoatSeatMaster> li = new List<BoatSeatMaster>();
                con.Open();
                //SqlCommand cmd = new SqlCommand(" SELECT * FROM BoatSeat WHERE BoatHouseId = '" + tx.BoatHouseId.Trim() + "' AND ActiveStatus = 'A' AND BoatSeaterId NOT IN( "
                //    + " SELECT BoatSeaterId FROM BoatRateMaster WHERE BoatTypeId = '" + tx.BoatTypeId.Trim() + "' AND BoatHouseId = '" + tx.BoatHouseId.Trim() + "' AND ActiveStatus='A')", con);

                SqlCommand cmd = new SqlCommand(" SELECT * FROM BoatSeat WHERE BoatHouseId = @BoatHouseId AND ActiveStatus = 'A' AND BoatSeaterId NOT IN( "
                + " SELECT BoatSeaterId FROM BoatRateMaster WHERE BoatTypeId =@BoatTypeId AND BoatHouseId = @BoatHouseId)", con);

                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = tx.BoatHouseId.Trim();
                cmd.Parameters["@BoatTypeId"].Value = tx.BoatTypeId.Trim();

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
        /// Modified By : Abhiniya.
        /// Modified Date : 29-Jul-2021
        /// Note : 
        /// </summary>
        /// <param name="bHMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRateMstr/BHId")]
        public IHttpActionResult getBoatRateMstrId([FromBody] BoatRateMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatRateMaster> li = new List<BoatRateMaster>();
                    SqlCommand cmd = new SqlCommand("sp_CommonBindGridView", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "BoatRateMstr");
                    cmd.Parameters.AddWithValue("@BoatTypeId", "0");
                    cmd.Parameters.AddWithValue("@BoatSeaterId", "0");
                    cmd.Parameters.AddWithValue("@BoatHouseId", bHMstr.BoatHouseId.ToString());


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatRateMaster BoatRateMaster = new BoatRateMaster();

                            BoatRateMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatRateMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            BoatRateMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatRateMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            BoatRateMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();

                            BoatRateMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            BoatRateMaster.BoatImageLink = dt.Rows[i]["BoatImageLink"].ToString();
                            BoatRateMaster.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
                            BoatRateMaster.DepositType = dt.Rows[i]["DepositType"].ToString();
                            BoatRateMaster.DepositTypeName = dt.Rows[i]["DepositTypeName"].ToString();

                            BoatRateMaster.Deposit = dt.Rows[i]["Deposit"].ToString();
                            BoatRateMaster.TimeExtension = dt.Rows[i]["TimeExtension"].ToString();
                            BoatRateMaster.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();
                            BoatRateMaster.BoatGraceTime = dt.Rows[i]["BoatGraceTime"].ToString();
                            BoatRateMaster.BoatMinTotAmt = dt.Rows[i]["BoatMinTotAmt"].ToString();

                            BoatRateMaster.BoatMinCharge = dt.Rows[i]["BoatMinCharge"].ToString();
                            BoatRateMaster.RowerMinCharge = dt.Rows[i]["RowerMinCharge"].ToString();
                            BoatRateMaster.BoatMinTaxAmt = dt.Rows[i]["BoatMinTaxAmt"].ToString();
                            BoatRateMaster.PerHeadApplicable = dt.Rows[i]["PerHeadApplicable"].ToString();
                            BoatRateMaster.WEBoatMinTotAmt = dt.Rows[i]["WEBoatMinTotAmt"].ToString();

                            BoatRateMaster.WEBoatMinCharge = dt.Rows[i]["WEBoatMinCharge"].ToString();
                            BoatRateMaster.WERowerMinCharge = dt.Rows[i]["WERowerMinCharge"].ToString();
                            BoatRateMaster.WEBoatMinTaxAmt = dt.Rows[i]["WEBoatMinTaxAmt"].ToString();
                            BoatRateMaster.BoatPremTotAmt = dt.Rows[i]["BoatPremTotAmt"].ToString();
                            BoatRateMaster.BoatPremMinCharge = dt.Rows[i]["BoatPremMinCharge"].ToString();

                            BoatRateMaster.RowerPremMinCharge = dt.Rows[i]["RowerPremMinCharge"].ToString();
                            BoatRateMaster.BoatPremTaxAmt = dt.Rows[i]["BoatPremTaxAmt"].ToString();
                            BoatRateMaster.IWDBoatMinTotAmt = dt.Rows[i]["IWDBoatMinTotAmt"].ToString();
                            BoatRateMaster.IWDBoatMinCharge = dt.Rows[i]["IWDBoatMinCharge"].ToString();
                            BoatRateMaster.IWDRowerMinCharge = dt.Rows[i]["IWDRowerMinCharge"].ToString();

                            BoatRateMaster.IWDBoatMinTaxAmt = dt.Rows[i]["IWDBoatMinTaxAmt"].ToString();
                            BoatRateMaster.IWEBoatMinTotAmt = dt.Rows[i]["IWEBoatMinTotAmt"].ToString();
                            BoatRateMaster.IWEBoatMinCharge = dt.Rows[i]["IWEBoatMinCharge"].ToString();
                            BoatRateMaster.IWERowerMinCharge = dt.Rows[i]["IWERowerMinCharge"].ToString();
                            BoatRateMaster.IWEBoatMinTaxAmt = dt.Rows[i]["IWEBoatMinTaxAmt"].ToString();

                            BoatRateMaster.MaxTripsPerDay = dt.Rows[i]["MaxTripsPerDay"].ToString();
                            BoatRateMaster.ChildApplicable = dt.Rows[i]["ChildApplicable"].ToString();
                            BoatRateMaster.NoofChildApplicable = dt.Rows[i]["NoofChildApplicable"].ToString();
                            BoatRateMaster.ChargePerChild = dt.Rows[i]["ChargePerChild"].ToString();
                            BoatRateMaster.ChargePerChildTotAmt = dt.Rows[i]["ChargePerChildTotAmt"].ToString();

                            BoatRateMaster.ChargePerChildTaxAmt = dt.Rows[i]["ChargePerChildTaxAmt"].ToString();
                            BoatRateMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            BoatRateMaster.DisplayOrder = dt.Rows[i]["DisplayOrder"].ToString();

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

        /// <summary>
        /// Modified By : Abhiniya
        /// Modified Date : 29-Jul-2021
        /// Note : Query convert to Stored procedure
        /// </summary>
        /// <param name="BHId"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRateExtnChrg/BHId")]
        public IHttpActionResult getBoatRateExtnChrg([FromBody] BoatRateExtnCharge bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatRateExtnCharge> li = new List<BoatRateExtnCharge>();
                    SqlCommand cmd = new SqlCommand("sp_CommonBindGridView", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "BoatRateExtnChrg");
                    cmd.Parameters.AddWithValue("@BoatTypeId", bHMstr.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", bHMstr.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", bHMstr.BoatHouseId.ToString());


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatRateExtnCharge BoatRateMaster = new BoatRateExtnCharge();

                            BoatRateMaster.UniqueId = dt.Rows[i]["UniqueId"].ToString();
                            BoatRateMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatRateMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            BoatRateMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatRateMaster.BoatSeaterName = dt.Rows[i]["BoatSeater"].ToString();

                            BoatRateMaster.ExtensionType = dt.Rows[i]["ExtensionType"].ToString();
                            BoatRateMaster.ExtensionTypeName = dt.Rows[i]["ExtensionTypeName"].ToString();
                            BoatRateMaster.ExtFromTime = dt.Rows[i]["ExtFromTime"].ToString();
                            BoatRateMaster.ExtToTime = dt.Rows[i]["ExtToTime"].ToString();
                            BoatRateMaster.AmtType = dt.Rows[i]["AmtType"].ToString();
                            BoatRateMaster.AmtPer = dt.Rows[i]["Percentage"].ToString();

                            BoatRateMaster.BoatExtnTotAmt = dt.Rows[i]["BoatExtnTotAmt"].ToString();
                            BoatRateMaster.RowerExtnCharge = dt.Rows[i]["RowerExtnCharge"].ToString();
                            BoatRateMaster.BoatExtnCharge = dt.Rows[i]["BoatExtnCharge"].ToString();
                            BoatRateMaster.BoatExtnTaxAmt = dt.Rows[i]["BoatExtnTaxAmt"].ToString();

                            BoatRateMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            BoatRateMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            BoatRateMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            li.Add(BoatRateMaster);
                        }

                        BoatRateExtnChargeList BoatRate = new BoatRateExtnChargeList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(BoatRate);
                    }

                    else
                    {
                        BoatRateExtnChargeRes BoatRate = new BoatRateExtnChargeRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }
                else
                {
                    BoatRateExtnChargeRes Vehicle = new BoatRateExtnChargeRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BoatRateExtnChargeRes ConfRes = new BoatRateExtnChargeRes
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

        //Automatic Delete for Boat Rate
        [HttpPost]
        [AllowAnonymous]
        [Route("AutomaticDeleteBoatRate")]
        public IHttpActionResult AutomaticDeleteBoatRate([FromBody] BoatRateExtnCharge BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<BoatRateExtnCharge> li = new List<BoatRateExtnCharge>();
                    SqlCommand cmd = new SqlCommand(" DELETE FROM BoatRateExtnCharge "
                        + " WHERE BoatHouseId = @BoatHouseId AND ActiveStatus = 'T' ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.ToString();
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        BoatRateExtnChargeRes ItemMasters1 = new BoatRateExtnChargeRes
                        {
                            Response = "Record Delete Successfully.",
                            StatusCode = 1
                        };
                        return Ok(ItemMasters1);
                    }
                    else
                    {
                        BoatRateExtnChargeRes ItemMasters1 = new BoatRateExtnChargeRes
                        {
                            Response = "Record Delete Un Successfully.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                BoatRateExtnChargeRes ConfRes = new BoatRateExtnChargeRes
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
        /// Modified By : Abhinaya
        /// Modified Date : 29-Jul-2021
        /// Note: Add Extra field.
        /// </summary>
        /// <param name="InsBRMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRateMstr")]
        public IHttpActionResult MstrBoatRateMaster([FromBody] BoatRateMaster InsBRMstr)
        {
            try
            {
                if (InsBRMstr.QueryType != "" && InsBRMstr.BoatTypeId != ""
                    && InsBRMstr.BoatSeaterId != "" && InsBRMstr.BoatHouseId != ""
                    && InsBRMstr.BoatHouseName != "" && InsBRMstr.BoatMinTime != ""
                    && InsBRMstr.MaxTripsPerDay != "" && InsBRMstr.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBoatRateMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBRMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsBRMstr.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", InsBRMstr.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBRMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBRMstr.BoatHouseName.Trim());

                    cmd.Parameters.AddWithValue("@BoatImageLink", InsBRMstr.BoatImageLink.ToString());
                    cmd.Parameters.AddWithValue("@SelfDrive", InsBRMstr.SelfDrive.Trim());
                    cmd.Parameters.AddWithValue("@DepositType", InsBRMstr.DepositType.Trim());
                    cmd.Parameters.AddWithValue("@Deposit", InsBRMstr.Deposit.ToString());
                    cmd.Parameters.AddWithValue("@TimeExtension", InsBRMstr.TimeExtension.Trim());

                    cmd.Parameters.AddWithValue("@BoatMinTime", InsBRMstr.BoatMinTime.ToString());
                    cmd.Parameters.AddWithValue("@BoatGraceTime", InsBRMstr.BoatGraceTime.ToString());
                    cmd.Parameters.AddWithValue("@PerHeadApplicable", InsBRMstr.PerHeadApplicable.ToString());
                    cmd.Parameters.AddWithValue("@BoatMinTotAmt", InsBRMstr.BoatMinTotAmt.ToString());
                    cmd.Parameters.AddWithValue("@BoatMinCharge", InsBRMstr.BoatMinCharge.ToString());

                    cmd.Parameters.AddWithValue("@RowerMinCharge", InsBRMstr.RowerMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@BoatMinTaxAmt", InsBRMstr.BoatMinTaxAmt.ToString());
                    cmd.Parameters.AddWithValue("@WEBoatMinTotAmt", InsBRMstr.WEBoatMinTotAmt.ToString());
                    cmd.Parameters.AddWithValue("@WEBoatMinCharge", InsBRMstr.WEBoatMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@WERowerMinCharge", InsBRMstr.WERowerMinCharge.ToString());

                    cmd.Parameters.AddWithValue("@WEBoatMinTaxAmt", InsBRMstr.WEBoatMinTaxAmt.ToString());
                    cmd.Parameters.AddWithValue("@BoatPremTotAmt", InsBRMstr.BoatPremTotAmt.ToString());
                    cmd.Parameters.AddWithValue("@BoatPremMinCharge", InsBRMstr.BoatPremMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@RowerPremMinCharge", InsBRMstr.RowerPremMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@BoatPremTaxAmt", InsBRMstr.BoatPremTaxAmt.ToString());

                    cmd.Parameters.AddWithValue("@IWDBoatMinTotAmt", InsBRMstr.IWDBoatMinTotAmt.ToString());
                    cmd.Parameters.AddWithValue("@IWDBoatMinCharge", InsBRMstr.IWDBoatMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@IWDRowerMinCharge", InsBRMstr.IWDRowerMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@IWDBoatMinTaxAmt", InsBRMstr.IWDBoatMinTaxAmt.ToString());
                    cmd.Parameters.AddWithValue("@IWEBoatMinTotAmt", InsBRMstr.IWEBoatMinTotAmt.ToString());

                    cmd.Parameters.AddWithValue("@IWEBoatMinCharge", InsBRMstr.IWEBoatMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@IWERowerMinCharge", InsBRMstr.IWERowerMinCharge.ToString());
                    cmd.Parameters.AddWithValue("@IWEBoatMinTaxAmt", InsBRMstr.IWEBoatMinTaxAmt.ToString());
                    cmd.Parameters.AddWithValue("@MaxTripsPerDay", InsBRMstr.MaxTripsPerDay.ToString());
                    cmd.Parameters.AddWithValue("@ChildApplicable", InsBRMstr.ChildApplicable.ToString());

                    cmd.Parameters.AddWithValue("@NoofChildApplicable", InsBRMstr.NoofChildApplicable.ToString());
                    cmd.Parameters.AddWithValue("@ChargePerChild", InsBRMstr.ChargePerChild.ToString());
                    cmd.Parameters.AddWithValue("@ChargePerChildTotAmt", InsBRMstr.ChargePerChildTotAmt.ToString());
                    cmd.Parameters.AddWithValue("@ChargePerChildTaxAmt", InsBRMstr.ChargePerChildTaxAmt.ToString());
                    cmd.Parameters.AddWithValue("@Createdby", InsBRMstr.Createdby.ToString());

                    cmd.Parameters.AddWithValue("@DisplayOrder", InsBRMstr.DisplayOrder.ToString());
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
                        BoatRateMasterString ConMstr = new BoatRateMasterString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        BoatRateMasterString ConMstr = new BoatRateMasterString
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    BoatRateMasterString Vehicle = new BoatRateMasterString
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

        //Boat Rate Extension Charge Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatRateExtnCharge")]
        public IHttpActionResult BoatRateExtnCharge([FromBody] BoatRateExtnCharge InsBRMstr)
        {
            try
            {
                if (InsBRMstr.QueryType != "" && InsBRMstr.BoatTypeId != ""
                    && InsBRMstr.BoatSeaterId != "" && InsBRMstr.ExtensionType != ""
                    && InsBRMstr.ExtFromTime != "" && InsBRMstr.ExtToTime != ""
                    && InsBRMstr.AmtType != "" && InsBRMstr.BoatExtnTotAmt != ""
                    && InsBRMstr.RowerExtnCharge != "" && InsBRMstr.BoatExtnCharge != ""
                    && InsBRMstr.BoatExtnTaxAmt != "" && InsBRMstr.BoatHouseId != ""
                    && InsBRMstr.BoatHouseName != "" && InsBRMstr.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBoatRateExtnCharge", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBRMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@UniqueId", InsBRMstr.UniqueId.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsBRMstr.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatType", InsBRMstr.BoatTypeName.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", InsBRMstr.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeater", InsBRMstr.BoatSeaterName.ToString());

                    cmd.Parameters.AddWithValue("@ExtensionType", InsBRMstr.ExtensionType.ToString());
                    cmd.Parameters.AddWithValue("@ExtFromTime", InsBRMstr.ExtFromTime.ToString());
                    cmd.Parameters.AddWithValue("@ExtToTime", InsBRMstr.ExtToTime.ToString());
                    cmd.Parameters.AddWithValue("@AmtType", InsBRMstr.AmtType.ToString());
                    cmd.Parameters.AddWithValue("@Percentage", InsBRMstr.AmtPer.ToString());

                    cmd.Parameters.AddWithValue("@BoatExtnTotAmt", InsBRMstr.BoatExtnTotAmt.ToString());
                    cmd.Parameters.AddWithValue("@RowerExtnCharge", InsBRMstr.RowerExtnCharge.ToString());
                    cmd.Parameters.AddWithValue("@BoatExtnCharge", InsBRMstr.BoatExtnCharge.ToString());
                    cmd.Parameters.AddWithValue("@BoatExtnTaxAmt", InsBRMstr.BoatExtnTaxAmt.ToString());

                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBRMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBRMstr.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@Createdby", InsBRMstr.Createdby.ToString());
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
                        BoatRateMasterString ConMstr = new BoatRateMasterString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        BoatRateMasterString ConMstr = new BoatRateMasterString
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    BoatRateMasterString Vehicle = new BoatRateMasterString
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

        /*Boat Master*/
        //Get Boat Type from Boat Rate Master
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatType/BoatRateMstr")]
        public IHttpActionResult getBoatType([FromBody] BoatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatTypeMaster> li = new List<BoatTypeMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT A.BoatTypeId, B.BoatType from BoatRateMaster AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " where A.ActiveStatus='A' "
                        + " AND A.BoatHouseId = @BoatHouseId", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.ToString();
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
                BoatTypeMasterString Vehicle = new BoatTypeMasterString
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

        [HttpPost]
        [AllowAnonymous]
        [Route("GetBoatTypeId")]
        public IHttpActionResult GetBoatTypesId([FromBody] BoatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "" && bHMstr.BoatTypeId != "")
                {
                    string sQuery = string.Empty;

                    if (bHMstr.BoatTypeId.Trim() == "0")
                    {
                        sQuery = "SELECT DISTINCT A.BoatTypeId, B.BoatType FROM BoatRateMaster AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.ActiveStatus ='A' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId";
                    }
                    else
                    {
                        sQuery = "SELECT DISTINCT A.BoatTypeId, B.BoatType FROM BoatRateMaster AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.ActiveStatus = 'A' AND A.BoatHouseId = @BoatHouseId AND B.BoatHouseId = @BoatHouseId"
                        + " AND A.BoatTypeId = @BoatTypeId";
                    }


                    List<BoatTypeMaster> li = new List<BoatTypeMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.ToString();
                    cmd.Parameters["@BoatTypeId"].Value = bHMstr.BoatTypeId.ToString();
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
                BoatTypeMasterString Vehicle = new BoatTypeMasterString
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

        //Boat Master Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatMstr")]
        public IHttpActionResult MstrBoatMaster([FromBody] BoatMaster InsBoatMstr)
        {
            try
            {
                if (InsBoatMstr.QueryType != "" && InsBoatMstr.BoatId != ""
                    && InsBoatMstr.BoatHouseId != "" && InsBoatMstr.BoatHouseName != ""
                    && InsBoatMstr.BoatTypeId != "" && InsBoatMstr.BoatSeaterId != ""
                    && InsBoatMstr.BoatStatus != "" && InsBoatMstr.BoatNature != ""
                    && InsBoatMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBoatMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBoatMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatId", InsBoatMstr.BoatId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBoatMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBoatMstr.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@BoatNumber", InsBoatMstr.BoatNum.ToString());
                    cmd.Parameters.AddWithValue("@BoatName", InsBoatMstr.BoatName.Trim());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsBoatMstr.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", InsBoatMstr.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatStatus", InsBoatMstr.BoatStatus.ToString());
                    cmd.Parameters.AddWithValue("@BoatOwner", InsBoatMstr.BoatOwner.Trim());
                    cmd.Parameters.AddWithValue("@BoatNature", InsBoatMstr.BoatNature.Trim());
                    cmd.Parameters.AddWithValue("@PaymentModel", InsBoatMstr.PaymentModel.ToString());
                    cmd.Parameters.AddWithValue("@PaymentPercent", InsBoatMstr.PaymentPercent.ToString());
                    cmd.Parameters.AddWithValue("@PaymentAmount", InsBoatMstr.PaymentAmount.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsBoatMstr.CreatedBy.Trim());

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
                        BoatMasterString ConMstr = new BoatMasterString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(ConMstr);
                    }
                    else
                    {
                        BoatMasterString ConMstr = new BoatMasterString
                        {
                            Response = sResult[0].Trim(),
                            StatusCode = 0
                        };
                        return Ok(ConMstr);
                    }
                }
                else
                {
                    BoatMasterString Vehicle = new BoatMasterString
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

        //Get Boat Master Count
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatMstr/Count")]
        public IHttpActionResult BoatRateMasterCount([FromBody] BoatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatMaster> li = new List<BoatMaster>();
                    string sQuery = string.Empty;

                    sQuery = " SELECT BoatTypeId, BoatType, BoatSeaterId, SeaterType, SUM(Normal) AS 'Normal', "
                        + " SUM(Express) AS 'Express', SUM(Normal) + SUM(Express) AS 'Total' FROM ( "
                        + " SELECT A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, "
                        + " CASE WHEN A.BoatNature = 'N' THEN COUNT(*) ELSE 0 END AS 'Normal', "
                        + " CASE WHEN A.BoatNature = 'P' THEN COUNT(*) ELSE 0  END AS 'Express' "
                        + " FROM BoatMaster AS A "
                        + " INNER JOIN BoatTypes AS B ON A.BoatTypeId = B.BoatTypeId AND A.BoatHouseId = B.BoatHouseId AND B.ActiveStatus = 'A' "
                        + " INNER JOIN BoatSeat AS C ON A.BoatSeaterId = C.BoatSeaterId AND A.BoatHouseId = B.BoatHouseId AND C.ActiveStatus = 'A' "
                        + " WHERE A.BoatHouseId = @BoatHouseId "
                        + " GROUP BY A.BoatTypeId, B.BoatType, A.BoatSeaterId, C.SeaterType, A.BoatNature "
                        + " )  AS A GROUP BY BoatTypeId, BoatType, BoatSeaterId, SeaterType ORDER BY A.BoatType ASC ";

                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatMaster BoatRateMaster = new BoatMaster();

                            BoatRateMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatRateMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            BoatRateMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatRateMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            BoatRateMaster.Normal = dt.Rows[i]["Normal"].ToString();
                            BoatRateMaster.Express = dt.Rows[i]["Express"].ToString();
                            BoatRateMaster.Total = dt.Rows[i]["Total"].ToString();
                            li.Add(BoatRateMaster);
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
                        BoatMasterString BoatRate = new BoatMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(BoatRate);
                    }
                }

                else
                {
                    BoatMasterString Vehicle = new BoatMasterString
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

        //Get Boat Master List
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatMstr/BHId")]
        public IHttpActionResult GetBoatMasterId([FromBody] BoatMaster bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    List<BoatMaster> li = new List<BoatMaster>();
                    SqlCommand cmd = new SqlCommand("select A.BoatId, A.BoatName,A.BoatNum,CASE When A.BoatNature = 'N' THEN 'Normal' else 'Express' End As BoatNature,A.BoatNature,A.BoatHouseId,B.BoatHouseName ,A.BoatTypeId , C.BoatType, "
                    + "   A.BoatSeaterId, D.SeaterType, A.PaymentModel, E.ConfigName as PaymentModelName, F.ConfigID as BoatStatus, "
                    + "   F.ConfigName AS 'BoatStatusName', A.PaymentPercent, A.PaymentAmount, CASE When "
                    + "   A.BoatOwner = 'T' Then 'Own' else  'Private' End As BoatOwner from BoatMaster as A "
                    + "  Inner JOIN BHMaster AS B on A.BoatHouseId = B.BoatHouseId and  B.ActiveStatus = 'A'  "
                    + "   Inner JOIN BoatTypes As C on A.BoatTypeId = C.BoatTypeId AND C.ActiveStatus = 'A' AND C.BoatHouseId = @BoatHouseId "
                    + "   INNER JOIN BoatSeat AS D ON A.BoatSeaterId = D.BoatSeaterId AND D.ActiveStatus = 'A' AND D.BoatHouseId = @BoatHouseId "
                    + "  LEFT JOIN ConfigurationMaster AS E ON A.PaymentModel = E.ConfigID AND E.TypeID = '17' AND E.ActiveStatus = 'A' "
                    + "   LEFT JOIN ConfigurationMaster AS F ON A.BoatStatus = F.ConfigID AND F.TypeID = '16' AND F.ActiveStatus = 'A' "
                    + "   WHERE A.BoatHouseId =  @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = bHMstr.BoatHouseId.ToString().Trim();
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
                            BoatMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            BoatMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            BoatMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            BoatMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
                            BoatMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            BoatMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
                            BoatMaster.PaymentModel = dt.Rows[i]["PaymentModel"].ToString();
                            BoatMaster.PaymentModelName = dt.Rows[i]["PaymentModelName"].ToString();
                            BoatMaster.BoatStatus = dt.Rows[i]["BoatStatus"].ToString();
                            BoatMaster.BoatStatusName = dt.Rows[i]["BoatStatusName"].ToString();
                            BoatMaster.PaymentPercent = dt.Rows[i]["PaymentPercent"].ToString();
                            BoatMaster.PaymentAmount = dt.Rows[i]["PaymentAmount"].ToString();
                            BoatMaster.BoatOwner = dt.Rows[i]["BoatOwner"].ToString();
                            BoatMaster.BoatNature = dt.Rows[i]["BoatNature"].ToString();
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

        //Get Boat Seat from Boat Rate Master
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatSeat/BoatRateMstr")]
        public IHttpActionResult getBoatSeat([FromBody] BoatMaster boatSeat)
        {
            try
            {
                if (boatSeat.BoatHouseId != "")
                {
                    List<BoatSeatMaster> li = new List<BoatSeatMaster>();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT A.BoatSeaterId, B.SeaterType from BoatRateMaster AS A "
                        + " INNER JOIN BoatSeat AS B ON A.BoatSeaterId = B.BoatSeaterId AND A.BoatHouseId = B.BoatHouseId "
                        + " WHERE A.ActiveStatus='A' "
                        + " AND A.BoatHouseId = @BoatHouseId AND A.BoatTypeId = @BoatTypeId ", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = boatSeat.BoatHouseId;
                    cmd.Parameters["@BoatTypeId"].Value = boatSeat.BoatTypeId;
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
                BoatSeatMasterString Vehicle = new BoatSeatMasterString
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

        /*Other Service Master*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("OtherServices")]
        public IHttpActionResult MstrOtherServices([FromBody] OtherServices InsMstrOther)
        {
            try
            {
                if (InsMstrOther.QueryType != "" && InsMstrOther.Category != ""
                    && InsMstrOther.ServiceName != "" && InsMstrOther.BoatHouseId != ""
                    && InsMstrOther.BoatHouseName != "" && InsMstrOther.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrOtherServices", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsMstrOther.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@Category", InsMstrOther.Category.ToString());
                    cmd.Parameters.AddWithValue("@ServiceId", InsMstrOther.ServiceId.ToString());
                    cmd.Parameters.AddWithValue("@ServiceName", InsMstrOther.ServiceName.ToString());
                    cmd.Parameters.AddWithValue("@ShortName", InsMstrOther.ShortName.ToString());
                    cmd.Parameters.AddWithValue("@ServiceTotalAmount", InsMstrOther.ServiceTotalAmount.ToString());
                    cmd.Parameters.AddWithValue("@ChargePerItem", InsMstrOther.ChargePerItem.ToString());
                    cmd.Parameters.AddWithValue("@ChargePerItemTax", InsMstrOther.ChargePerItemTax.ToString());
                    cmd.Parameters.AddWithValue("@TaxID", InsMstrOther.TaxID.ToString());
                    cmd.Parameters.AddWithValue("@OtherServiceType", InsMstrOther.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsMstrOther.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsMstrOther.BoatHouseName.Trim());
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

        //Get Other Service List based on Boat House Id
        [HttpPost]
        [AllowAnonymous]
        [Route("OtherServicesMstr/BHId")]
        public IHttpActionResult OtherServicesBHId([FromBody] OtherServices bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    string sQuery = string.Empty;

                    sQuery = "SELECT A.Category, B.ConfigName AS 'CategoryName', A.ServiceId, A.ServiceName, A.ShortName, A.ServiceTotalAmount, "
                        + " A.BoatHouseId, A.BoatHouseName, A.ChargePerItem, A.ChargePerItemTax, A.OtherServiceType, "
                        + " A.TaxID, CASE WHEN A.TaxID > 0 THEN dbo.GetTaxIdDetails('Other', '2', '', A.TaxID,@BoatHouseId) ELSE 'Nil Tax' END AS 'TaxName', "
                        + " A.ActiveStatus "
                        + " FROM OtherServices AS A "
                        + " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND B.TypeID = '27' "
                        + " WHERE A.ActiveStatus IN ('A','D') AND A.OtherServiceType='OS' AND A.BoatHouseId = @BoatHouseId "
                        + " UNION ALL "
                        + " SELECT A.Category, B.ConfigName AS 'CategoryName', A.ServiceId, A.ServiceName, A.ShortName, A.ServiceTotalAmount, "
                        + " A.BoatHouseId, A.BoatHouseName, A.ChargePerItem, A.ChargePerItemTax, A.OtherServiceType, "
                        + " A.TaxID, CASE WHEN A.TaxID > 0 THEN dbo.GetTaxIdDetails('Other', '2', '', A.TaxID,@BoatHouseId) ELSE 'Nil Tax' END AS 'TaxName', "
                        + " A.ActiveStatus "
                        + " FROM OtherServices AS A "
                        + " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND B.TypeID = '34' "
                        + " WHERE A.ActiveStatus IN ('A','D') AND A.OtherServiceType='AB' AND A.BoatHouseId = @BoatHouseId ";

                    List<OtherServices> li = new List<OtherServices>();
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
                            OtherServices OtherServiceMstr = new OtherServices();

                            OtherServiceMstr.Category = dt.Rows[i]["Category"].ToString();
                            OtherServiceMstr.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OtherServiceMstr.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            OtherServiceMstr.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            OtherServiceMstr.ShortName = dt.Rows[i]["ShortName"].ToString();
                            OtherServiceMstr.ServiceTotalAmount = dt.Rows[i]["ServiceTotalAmount"].ToString();
                            OtherServiceMstr.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                            OtherServiceMstr.ChargePerItemTax = dt.Rows[i]["ChargePerItemTax"].ToString();
                            OtherServiceMstr.TaxID = dt.Rows[i]["TaxID"].ToString();
                            OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            OtherServiceMstr.TaxName = dt.Rows[i]["TaxName"].ToString();
                            OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OtherServiceMstr.ServiceType = dt.Rows[i]["OtherServiceType"].ToString();

                            li.Add(OtherServiceMstr);

                        }

                        OtherServicesList OtherService = new OtherServicesList
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
        [Route("PublicOtherServicesMstr/BHId")]
        public IHttpActionResult PublicOtherServicesBHId([FromBody] OtherServices bHMstr)
        {
            try
            {
                if (bHMstr.BoatHouseId != "")
                {
                    string sQuery = string.Empty;

                    sQuery = "SELECT A.Category, B.ConfigName AS 'CategoryName', A.ServiceId, A.ServiceName, A.ShortName, A.ServiceTotalAmount, "
                        + " A.BoatHouseId, A.BoatHouseName, A.ChargePerItem, A.ChargePerItemTax, A.OtherServiceType, "
                        + " A.TaxID, CASE WHEN A.TaxID > 0 THEN dbo.GetTaxIdDetails('Other', '2', '', A.TaxID,@BoatHouseId) ELSE 'Nil Tax' END AS 'TaxName', "
                        + " A.ActiveStatus "
                        + " FROM OtherServices AS A "
                        + " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND B.TypeID = '27' "
                        + " WHERE A.ActiveStatus IN ('A') AND A.OtherServiceType='OS' AND A.BoatHouseId = @BoatHouseId";

                    //+ " UNION ALL "
                    //+ " SELECT A.Category, B.ConfigName AS 'CategoryName', A.ServiceId, A.ServiceName, A.ShortName, A.ServiceTotalAmount, "
                    //+ " A.BoatHouseId, A.BoatHouseName, A.ChargePerItem, A.ChargePerItemTax, A.OtherServiceType, "
                    //+ " A.TaxID, CASE WHEN A.TaxID > 0 THEN dbo.GetTaxIdDetails('Other', '2', '', A.TaxID) ELSE 'Nil Tax' END AS 'TaxName', "
                    //+ " A.ActiveStatus "
                    //+ " FROM OtherServices AS A "
                    //+ " INNER JOIN ConfigurationMaster AS B ON A.Category = B.ConfigID AND B.TypeID = '34' "
                    //+ " WHERE A.ActiveStatus IN ('A','D') AND A.OtherServiceType='AB' AND A.BoatHouseId = '" + bHMstr.BoatHouseId.Trim() + "'";

                    List<OtherServices> li = new List<OtherServices>();
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
                            OtherServices OtherServiceMstr = new OtherServices();

                            OtherServiceMstr.Category = dt.Rows[i]["Category"].ToString();
                            OtherServiceMstr.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            OtherServiceMstr.ServiceId = dt.Rows[i]["ServiceId"].ToString();
                            OtherServiceMstr.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                            OtherServiceMstr.ShortName = dt.Rows[i]["ShortName"].ToString();
                            OtherServiceMstr.ServiceTotalAmount = dt.Rows[i]["ServiceTotalAmount"].ToString();
                            OtherServiceMstr.ChargePerItem = dt.Rows[i]["ChargePerItem"].ToString();
                            OtherServiceMstr.ChargePerItemTax = dt.Rows[i]["ChargePerItemTax"].ToString();
                            OtherServiceMstr.TaxID = dt.Rows[i]["TaxID"].ToString();
                            OtherServiceMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            OtherServiceMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            OtherServiceMstr.TaxName = dt.Rows[i]["TaxName"].ToString();
                            OtherServiceMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            OtherServiceMstr.ServiceType = dt.Rows[i]["OtherServiceType"].ToString();

                            li.Add(OtherServiceMstr);

                        }

                        OtherServicesList OtherService = new OtherServicesList
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

        /*Item Master*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("ItemMaster")]
        public IHttpActionResult MstrItemMaster([FromBody] ItemMaster InsItemMaster)
        {
            try
            {
                if (InsItemMaster.QueryType != "" && InsItemMaster.ItemId != ""
                    && InsItemMaster.ItemDescription != "" && InsItemMaster.ItemType != ""
                    && InsItemMaster.EntityFlag != "" && InsItemMaster.EntityId != ""
                    && InsItemMaster.EntityName != "" && InsItemMaster.UOM != ""
                    && InsItemMaster.ItemRate != ""
                    && InsItemMaster.OpeningQty != "" && InsItemMaster.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrItemMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsItemMaster.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ItemId", InsItemMaster.ItemId.ToString());
                    cmd.Parameters.AddWithValue("@ItemDescription", InsItemMaster.ItemDescription.ToString());
                    cmd.Parameters.AddWithValue("@ItemType", InsItemMaster.ItemType.ToString());
                    cmd.Parameters.AddWithValue("@EntityFlag", InsItemMaster.EntityFlag.Trim());
                    cmd.Parameters.AddWithValue("@EntityId", InsItemMaster.EntityId.ToString());
                    cmd.Parameters.AddWithValue("@EntityName", InsItemMaster.EntityName.Trim());
                    cmd.Parameters.AddWithValue("@UOM", InsItemMaster.UOM.ToString());
                    cmd.Parameters.AddWithValue("@ItemRate", InsItemMaster.ItemRate.ToString());
                    cmd.Parameters.AddWithValue("@OpeningQty", InsItemMaster.OpeningQty.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsItemMaster.CreatedBy.Trim());

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

        //Get Item Master List
        [HttpPost]
        [AllowAnonymous]
        [Route("ItemMaster/BhId")]
        public IHttpActionResult getItemMasterId([FromBody] ItemMaster BHId)
        {
            try
            {
                if (BHId.EntityId != "")
                {
                    List<ItemMaster> li = new List<ItemMaster>();

                    SqlCommand cmd = new SqlCommand(" SELECT A.ItemId , A.ItemDescription , A.ItemType , C.ConfigName As ItemName ,A.UOM , B.ConfigName as UOMName , "
                            + " A.ItemRate, A.OpeningQty, A.ActiveStatus, A.CreatedBy  From ItemMaster AS A "
                            + " INNER JOIN ConfigurationMaster AS B ON B.ConfigID = A.UOM  AND B.ActiveStatus = 'A' AND B.TypeID = 22 "
                            + " INNER JOIN ConfigurationMaster AS C ON C.ConfigID = A.ItemType AND C.ActiveStatus = 'A' AND C.TypeID = 23 "
                            + " where A.ActiveStatus IN('A', 'D') AND A.EntityId = @EntityId", con);

                    cmd.Parameters.Add(new SqlParameter("@EntityId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@EntityId"].Value = BHId.EntityId.Trim();
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
                else
                {
                    ItemMasterString CancelRe3 = new ItemMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(CancelRe3);
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


        /*Material Consumption*/
        // Edit
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatConsumpEditDet")]
        public IHttpActionResult BoatConsumpEditDet([FromBody] BoatConsumptionMaster BHId)
        {
            try
            {
                if (BHId.ConsumptionId != "" && BHId.BoatHouseId != "")
                {
                    List<BoatConsumptionMaster> li = new List<BoatConsumptionMaster>();
                    SqlCommand cmd = new SqlCommand(" SELECT A.ConsumptionId, CONVERT(NVARCHAR(50),B.ConsumptionDate,105) 'ConsumptionDate', A.ItemId, "
                            + " A.ItemQtyPerTrip, B.BoatTypeId, B.BoatSeaterId FROM BoatConsumptionDtl AS A  "
                            + " INNER JOIN BoatConsumptionHdr AS B ON A.ConsumptionId = B.ConsumptionId AND A.BoatHouseId = B.BoatHouseId AND B.ActiveStatus = 'A' "
                            + " INNER JOIN ItemMaster AS C On A.ItemId = C.ItemId AND A.BoatHouseId = C.EntityId AND C.ActiveStatus = 'A'  "
                            + " WHERE A.ConsumptionId = @ConsumptionId AND A.BoatHouseId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ConsumptionId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@ConsumptionId"].Value = BHId.ConsumptionId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatConsumptionMaster MaterialPurchase = new BoatConsumptionMaster();
                            MaterialPurchase.ConsumptionId = dt.Rows[i]["ConsumptionId"].ToString();
                            MaterialPurchase.ConsumptionDate = dt.Rows[i]["ConsumptionDate"].ToString();
                            MaterialPurchase.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            MaterialPurchase.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            MaterialPurchase.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialPurchase.ItemQtyPerTrip = dt.Rows[i]["ItemQtyPerTrip"].ToString();

                            li.Add(MaterialPurchase);
                        }
                        BoatConsumptionMasterList ItemMasters = new BoatConsumptionMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
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

        //List
        [HttpPost]
        [AllowAnonymous]
        [Route("GetBoatConsumpDetails")]
        public IHttpActionResult GetMaterialIssueDetails([FromBody] BoatConsumptionMaster BHId)
        {
            try
            {
                if (BHId.ConsumptionId != "" && BHId.BoatHouseId != "")
                {
                    List<BoatConsumptionMaster> li = new List<BoatConsumptionMaster>();

                    SqlCommand cmd = new SqlCommand(" SELECT A.ConsumptionId, A.ItemId, A.ItemQtyPerTrip, B.ItemDescription From BoatConsumptionDtl AS A "
                        + " INNER JOIN ItemMaster AS B ON A.ItemId = B.ItemId AND A.BoatHouseId = B.EntityId AND B.ActiveStatus = 'A'"
                        + " AND A.ConsumptionId = @ConsumptionId  AND A.BoatHouseId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ConsumptionId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@ConsumptionId"].Value = BHId.ConsumptionId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatConsumptionMaster MaterialIssues = new BoatConsumptionMaster();
                            MaterialIssues.ConsumptionId = dt.Rows[i]["ConsumptionId"].ToString();
                            MaterialIssues.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialIssues.ItemQtyPerTrip = dt.Rows[i]["ItemQtyPerTrip"].ToString();
                            MaterialIssues.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();

                            li.Add(MaterialIssues);
                        }
                        BoatConsumptionMasterList ItemMasters = new BoatConsumptionMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
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
        [Route("MstrBoatConsumption")]
        public IHttpActionResult MstrBoatConsumption([FromBody] BoatConsumptionMaster InsBoatCon)
        {
            try
            {
                if (InsBoatCon.QueryType != ""
                    && InsBoatCon.ConsumptionId != ""
                    && InsBoatCon.BoatHouseId != ""
                    )
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrBoatConsumption", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBoatCon.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ConsumptionId", InsBoatCon.ConsumptionId.ToString());
                    cmd.Parameters.AddWithValue("@ConsumptionDate", DateTime.Parse(InsBoatCon.ConsumptionDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsBoatCon.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", InsBoatCon.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBoatCon.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsBoatCon.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@ItemId", InsBoatCon.ItemId.ToString());
                    cmd.Parameters.AddWithValue("@ItemQtyPerTrip", InsBoatCon.ItemQtyPerTrip.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsBoatCon.CreatedBy.Trim());

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

        //Fetch Details
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatConsumpDet/ConsId")]
        public IHttpActionResult BoatConsumpDet([FromBody] BoatConsumptionMaster BHId)
        {
            try
            {
                if (BHId.ConsumptionId != "" && BHId.BoatHouseId != "")
                {
                    List<BoatConsumptionMaster> li = new List<BoatConsumptionMaster>();

                    SqlCommand cmd = new SqlCommand(" SELECT A.ConsumptionId, A.ItemId, B.ItemDescription, A.ItemQtyPerTrip FROM BoatConsumptionDtl AS A "
                        + " INNER JOIN ItemMaster AS B ON A.ItemId = B.ItemId AND A.BoatHouseId = B.EntityId "
                        + " INNER JOIN BoatConsumptionHdr AS C ON A.ConsumptionId = C.ConsumptionId AND C.ActiveStatus = 'A' "
                        + " WHERE A.ConsumptionId = @ConsumptionId AND A.BoatHouseId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ConsumptionId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@ConsumptionId"].Value = BHId.ConsumptionId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatConsumptionMaster MaterialPurchase = new BoatConsumptionMaster();
                            MaterialPurchase.ConsumptionId = dt.Rows[i]["ConsumptionId"].ToString();
                            MaterialPurchase.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialPurchase.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();
                            MaterialPurchase.ItemQtyPerTrip = dt.Rows[i]["ItemQtyPerTrip"].ToString();
                            li.Add(MaterialPurchase);
                        }
                        BoatConsumptionMasterList ItemMasters = new BoatConsumptionMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
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

        //Maximum Boat Consumption
        [HttpPost]
        [AllowAnonymous]
        [Route("MaxBoatConsumptionId")]
        public IHttpActionResult BoatConsumptionId([FromBody] BoatConsumptionMaster BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<BoatConsumptionMaster> li = new List<BoatConsumptionMaster>();

                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ConsumptionId), 0) + 1 'ConsumptionId' FROM BoatConsumptionHdr", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = dt.Rows[0]["ConsumptionId"].ToString(),
                            StatusCode = 1
                        };
                        return Ok(ItemMasters1);
                    }

                    else
                    {
                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = "0",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
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

        //List based on Boat House Id
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatConsumptionAll/BhId")]
        public IHttpActionResult BoatConsumptionAll([FromBody] BoatConsumptionMaster BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<BoatConsumptionMaster> li = new List<BoatConsumptionMaster>();

                    SqlCommand cmd = new SqlCommand(" SELECT A.ConsumptionId, CONVERT(NVARCHAR(50), A.ConsumptionDate,105) AS ConsumptionDate, "
                        + " A.BoatTypeId, D.BoatType, A.BoatSeaterId, E.SeaterType, ISNULL(SUM(B.ItemQtyPerTrip), 0) 'ItemQty', "
                        + " ISNULL(SUM(B.ItemQtyPerTrip), 0) 'ItemQty', COUNT(B.ConsumptionId) 'NoOfItems', A.ActiveStatus FROM BoatConsumptionHdr AS A "
                        + " INNER JOIN BoatConsumptionDtl AS B ON A.ConsumptionId = B.ConsumptionId AND A.BoatHouseId = B.BoatHouseId "
                        + " INNER JOIN ItemMaster AS C On B.ItemId = C.ItemId AND A.BoatHouseId = C.EntityId AND C.ActiveStatus = 'A'  "
                        + " INNER JOIN BoatTypes AS D ON A.BoatTypeId = D.BoatTypeId AND A.BoatHouseId = D.BoatHouseId AND D.ActiveStatus = 'A' "
                        + " INNER JOIN BoatSeat AS E ON A.BoatSeaterId = E.BoatSeaterId AND A.BoatHouseId = E.BoatHouseId AND E.ActiveStatus = 'A' "
                        + " WHERE A.ActiveStatus IN('A', 'D') AND A.BoatHouseId = @BoatHouseId "
                        + " GROUP BY A.ConsumptionId, A.ActiveStatus, A.ConsumptionDate, A.BoatTypeId, A.BoatSeaterId, D.BoatType, E.SeaterType ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatConsumptionMaster MaterialPurchase = new BoatConsumptionMaster();
                            MaterialPurchase.ConsumptionId = dt.Rows[i]["ConsumptionId"].ToString();
                            MaterialPurchase.ConsumptionDate = dt.Rows[i]["ConsumptionDate"].ToString();
                            MaterialPurchase.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
                            MaterialPurchase.BoatType = dt.Rows[i]["BoatType"].ToString();
                            MaterialPurchase.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
                            MaterialPurchase.SeaterType = dt.Rows[i]["SeaterType"].ToString();
                            MaterialPurchase.ItemQtyPerTrip = dt.Rows[i]["ItemQty"].ToString();
                            MaterialPurchase.NoofItems = dt.Rows[i]["NoOfItems"].ToString();
                            MaterialPurchase.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            li.Add(MaterialPurchase);
                        }
                        BoatConsumptionMasterList ItemMasters = new BoatConsumptionMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
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

        //Delete Temprorary Item
        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteBoatConsumptionItem")]
        public IHttpActionResult DeleteBoatConsumptionItem([FromBody] BoatConsumptionMaster BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand("DELETE FROM BoatConsumptionDtl WHERE BoatHouseId = @BoatHouseId AND ConsumptionId NOT IN "
                        + " ( SELECT ConsumptionId FROM BoatConsumptionHdr WHERE BoatHouseId = @BoatHouseId) ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = "Record Delete Successfully.",
                            StatusCode = 1
                        };
                        return Ok(ItemMasters1);
                    }
                    else
                    {
                        BoatConsumptionMasterString ItemMasters1 = new BoatConsumptionMasterString
                        {
                            Response = "Record Delete Un Successfully.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                return Ok();
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

        /*Cancellation Reschedule Rule*/
        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("MstrCancelReschedMaster")]
        public IHttpActionResult MstrCancelReschedMaster([FromBody] CancelReschedMstr InsCanResMstr)
        {
            try
            {
                if (InsCanResMstr.QueryType != "" && InsCanResMstr.ActivityId != "" && InsCanResMstr.Description != ""
                    && InsCanResMstr.BoatHouseId != "" && InsCanResMstr.BoatHouseName != ""
                    && InsCanResMstr.ActivityType != "" && InsCanResMstr.ChargeType != ""
                    && InsCanResMstr.Charges != "" && InsCanResMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrCancelReschedMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsCanResMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ActivityId", InsCanResMstr.ActivityId.ToString());
                    cmd.Parameters.AddWithValue("@Description", InsCanResMstr.Description.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsCanResMstr.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsCanResMstr.BoatHouseName.ToString());
                    cmd.Parameters.AddWithValue("@ActivityType", InsCanResMstr.ActivityType.ToString());
                    cmd.Parameters.AddWithValue("@ChargeType", InsCanResMstr.ChargeType.ToString());
                    cmd.Parameters.AddWithValue("@Charges", InsCanResMstr.Charges.ToString());
                    cmd.Parameters.AddWithValue("@ApplicableBefore", InsCanResMstr.ApplicableBefore.Trim());
                    cmd.Parameters.AddWithValue("@EffectiveFrom", DateTime.Parse(InsCanResMstr.EffectiveFrom.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@EffectiveTill", DateTime.Parse(InsCanResMstr.EffectiveTill.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@MaxNoOfResched", InsCanResMstr.MaxNoOfResched.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsCanResMstr.CreatedBy.Trim());

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

        //List
        [HttpPost]
        [AllowAnonymous]
        [Route("CancelReschedMstr/BHId")]
        public IHttpActionResult getCancellationReschedule([FromBody] CancelReschedMstr BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<CancelReschedMstr> li = new List<CancelReschedMstr>();

                    SqlCommand cmd = new SqlCommand("SElect A.ActivityId,A.Description,A.BoatHouseId,A.BoatHouseName,A.ActivityType,A.ChargeType, "
                                      + " CASE WHEN A.ActivityType='C' THEN 'Cancellation' ELSE 'Re-scheduling' END AS 'CategoryName' , "
                                      + " A.Charges, A.ApplicableBefore, CONVERT(NVARCHAR(50), A.EffectiveFrom, 105) AS EffectiveFrom, "
                                      + " CONVERT(NVARCHAR(50), A.EffectiveTill, 105) AS EffectiveTill, A.MaxNoOfResched, A.ActiveStatus From  CancelReschedMstr  AS A "
                                      + " Inner Join BHMaster AS B On A.BoatHouseId = B.BoatHouseId AND B.ActiveStatus = 'A' "
                                      + " Where A.ActiveStatus IN('A','D') And A.BoatHouseId = @BoatHouseId", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CancelReschedMstr CancelRe = new CancelReschedMstr();
                            CancelRe.ActivityId = dt.Rows[i]["ActivityId"].ToString();
                            CancelRe.Description = dt.Rows[i]["Description"].ToString();
                            CancelRe.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            CancelRe.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            CancelRe.ActivityType = dt.Rows[i]["ActivityType"].ToString();
                            CancelRe.ChargeType = dt.Rows[i]["ChargeType"].ToString();
                            CancelRe.Charges = dt.Rows[i]["Charges"].ToString();
                            CancelRe.ApplicableBefore = dt.Rows[i]["ApplicableBefore"].ToString();
                            CancelRe.EffectiveFrom = dt.Rows[i]["EffectiveFrom"].ToString();
                            CancelRe.EffectiveTill = dt.Rows[i]["EffectiveTill"].ToString();
                            CancelRe.MaxNoOfResched = dt.Rows[i]["MaxNoOfResched"].ToString();
                            CancelRe.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            CancelRe.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                            li.Add(CancelRe);
                        }
                        CancelReschedMstrList CancelRe1 = new CancelReschedMstrList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(CancelRe1);
                    }
                    else
                    {
                        CancelReschedMstrRes CancelRe2 = new CancelReschedMstrRes
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(CancelRe2);
                    }
                }
                else
                {
                    CancelReschedMstrRes CancelRe3 = new CancelReschedMstrRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(CancelRe3);
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

        /*Rower Master*/
        //List
        [HttpPost]
        [AllowAnonymous]
        [Route("ShowRowerMstrDetailsGrid")]
        public IHttpActionResult ShowRowerMasterDetailsGrid([FromBody] RowerMaster RowMstr)
        {
            try
            {
                string sQuery = string.Empty;

                sQuery = " SELECT A.RowerId, A.RowerName, A.MobileNo, A.AadharId,A.RowerType,B.ConfigName, A.MailId, A.Address1, "
                            + " A.Address2, A.Zipcode, A.City, A.District, A.State, A.PhotoLink, A.ActiveStatus, A.DriverCategory FROM RowerMaster AS A "
                            + " INNER JOIN ConfigurationMaster AS B ON A.RowerType = B.ConfigID AND B.TypeID = '26' AND B.ActiveStatus = 'A'  "
                            + " WHERE BoatHouseId = @BoatHouseId ORDER BY A.RowerName";

                List<RowerMaster> li = new List<RowerMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = RowMstr.BoatHouseId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RowerMaster ShowConfMstr = new RowerMaster();
                        ShowConfMstr.RowerId = dt.Rows[i]["RowerId"].ToString();
                        ShowConfMstr.RowerName = dt.Rows[i]["RowerName"].ToString();
                        ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        ShowConfMstr.AadharId = dt.Rows[i]["AadharId"].ToString();
                        ShowConfMstr.RowerType = dt.Rows[i]["RowerType"].ToString();
                        ShowConfMstr.MailId = dt.Rows[i]["MailId"].ToString();
                        ShowConfMstr.RowerTypeName = dt.Rows[i]["ConfigName"].ToString();
                        ShowConfMstr.Address1 = dt.Rows[i]["Address1"].ToString();
                        ShowConfMstr.Address2 = dt.Rows[i]["Address2"].ToString();
                        ShowConfMstr.ZipCode = dt.Rows[i]["Zipcode"].ToString();
                        ShowConfMstr.City = dt.Rows[i]["City"].ToString();
                        ShowConfMstr.District = dt.Rows[i]["District"].ToString();
                        ShowConfMstr.State = dt.Rows[i]["State"].ToString();
                        ShowConfMstr.PhotoLink = dt.Rows[i]["PhotoLink"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.DriverCategory = dt.Rows[i]["DriverCategory"].ToString();
                        li.Add(ShowConfMstr);
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
        /// Created By : Brijin
        /// Created Date : 2022-04-21
        /// Version : V2
        /// </summary>
        /// <param name="RowMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ShowRowerMstrDetailsGridV2")]
        public IHttpActionResult ShowRowerMasterDetailsGridV2([FromBody] RowerMaster RowMstr)
        {
            try
            {
                string sQuery = string.Empty;
                int endcount = Int32.Parse(RowMstr.CountStart.Trim()) + 9;
                sQuery = "  SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY A.RowerName) 'RowNumber', "
                            + " A.RowerId, A.RowerName, A.MobileNo, A.AadharId,A.RowerType,B.ConfigName, A.MailId, A.Address1, "
                            + " A.Address2, A.Zipcode, A.City, A.District, A.State, A.PhotoLink, A.ActiveStatus, A.DriverCategory FROM RowerMaster AS A "
                            + " INNER JOIN ConfigurationMaster AS B ON A.RowerType = B.ConfigID AND B.TypeID = '26' AND B.ActiveStatus = 'A'  "
                            + " WHERE BoatHouseId = @BoatHouseId ) AS A Where A.RowNumber BETWEEN @RowNumber AND @endcount ORDER BY A.RowerName ";

                List<RowerMaster> li = new List<RowerMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@RowNumber", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@endcount", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = RowMstr.BoatHouseId.Trim();
                cmd.Parameters["@RowNumber"].Value = RowMstr.CountStart.Trim();
                cmd.Parameters["@endcount"].Value = endcount;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RowerMaster ShowConfMstr = new RowerMaster();
                        ShowConfMstr.RowerId = dt.Rows[i]["RowerId"].ToString();
                        ShowConfMstr.RowerName = dt.Rows[i]["RowerName"].ToString();
                        ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        ShowConfMstr.AadharId = dt.Rows[i]["AadharId"].ToString();
                        ShowConfMstr.RowerType = dt.Rows[i]["RowerType"].ToString();
                        ShowConfMstr.MailId = dt.Rows[i]["MailId"].ToString();
                        ShowConfMstr.RowerTypeName = dt.Rows[i]["ConfigName"].ToString();
                        ShowConfMstr.Address1 = dt.Rows[i]["Address1"].ToString();
                        ShowConfMstr.Address2 = dt.Rows[i]["Address2"].ToString();
                        ShowConfMstr.ZipCode = dt.Rows[i]["Zipcode"].ToString();
                        ShowConfMstr.City = dt.Rows[i]["City"].ToString();
                        ShowConfMstr.District = dt.Rows[i]["District"].ToString();
                        ShowConfMstr.State = dt.Rows[i]["State"].ToString();
                        ShowConfMstr.PhotoLink = dt.Rows[i]["PhotoLink"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.DriverCategory = dt.Rows[i]["DriverCategory"].ToString();
                        ShowConfMstr.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                        li.Add(ShowConfMstr);
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
        /// Created By : Brijin
        /// Created Date : 2022-04-21
        /// Version : V2
        /// </summary>
        /// <param name="RowMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ShowRowerMstrDetailsGridSingleV2")]
        public IHttpActionResult ShowRowerMasterDetailsGridSingleV2([FromBody] RowerMaster RowMstr)
        {
            try
            {
                string sQuery = string.Empty;
                int endcount = Int32.Parse(RowMstr.CountStart.Trim()) + 9;
                sQuery = "  SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY A.RowerName) 'RowNumber', "
                            + " A.RowerId, A.RowerName, A.MobileNo, A.AadharId,A.RowerType,B.ConfigName, A.MailId, A.Address1, "
                            + " A.Address2, A.Zipcode, A.City, A.District, A.State, A.PhotoLink, A.ActiveStatus, A.DriverCategory FROM RowerMaster AS A "
                            + " INNER JOIN ConfigurationMaster AS B ON A.RowerType = B.ConfigID AND B.TypeID = '26' AND B.ActiveStatus = 'A'  "
                            + " WHERE BoatHouseId = @BoatHouseId And (A.RowerName like @Search or MobileNo Like @Search"
                            + "  or ConfigName like @Search or A.DriverCategory like @Search "
                            + " ) ) AS A Where A.RowNumber BETWEEN @RowNumber AND @endcount ORDER BY A.RowerName ";

                List<RowerMaster> li = new List<RowerMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@RowNumber", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@endcount", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Search", System.Data.SqlDbType.NVarChar));
                cmd.Parameters["@BoatHouseId"].Value = RowMstr.BoatHouseId.Trim();
                cmd.Parameters["@RowNumber"].Value = RowMstr.CountStart.Trim();
                cmd.Parameters["@endcount"].Value = endcount;
                cmd.Parameters["@Search"].Value = RowMstr.Search.ToString().Trim() + '%';
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RowerMaster ShowConfMstr = new RowerMaster();
                        ShowConfMstr.RowerId = dt.Rows[i]["RowerId"].ToString();
                        ShowConfMstr.RowerName = dt.Rows[i]["RowerName"].ToString();
                        ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        ShowConfMstr.AadharId = dt.Rows[i]["AadharId"].ToString();
                        ShowConfMstr.RowerType = dt.Rows[i]["RowerType"].ToString();
                        ShowConfMstr.MailId = dt.Rows[i]["MailId"].ToString();
                        ShowConfMstr.RowerTypeName = dt.Rows[i]["ConfigName"].ToString();
                        ShowConfMstr.Address1 = dt.Rows[i]["Address1"].ToString();
                        ShowConfMstr.Address2 = dt.Rows[i]["Address2"].ToString();
                        ShowConfMstr.ZipCode = dt.Rows[i]["Zipcode"].ToString();
                        ShowConfMstr.City = dt.Rows[i]["City"].ToString();
                        ShowConfMstr.District = dt.Rows[i]["District"].ToString();
                        ShowConfMstr.State = dt.Rows[i]["State"].ToString();
                        ShowConfMstr.PhotoLink = dt.Rows[i]["PhotoLink"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.DriverCategory = dt.Rows[i]["DriverCategory"].ToString();
                        ShowConfMstr.RowNumber = dt.Rows[i]["RowNumber"].ToString();
                        li.Add(ShowConfMstr);
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
        [Route("ShowRowerMstrDetails")]
        public IHttpActionResult ShowRowerMasterDetails([FromBody] RowerMaster RowMstr)
        {
            try
            {
                string sQuery = string.Empty;

                sQuery = " SELECT A.RowerId, A.RowerName, A.MobileNo, A.AadharId,A.RowerType,B.ConfigName, A.MailId, A.Address1, "
                            + " A.Address2, A.Zipcode, A.City, A.District, A.State, A.PhotoLink, A.ActiveStatus, A.DriverCategory FROM RowerMaster AS A "
                            + " INNER JOIN ConfigurationMaster AS B ON A.RowerType = B.ConfigID AND B.TypeID = '26' AND B.ActiveStatus = 'A'  "
                            + " WHERE BoatHouseId = @BoatHouseId AND A.ActiveStatus = 'A' ORDER BY A.RowerName";

                List<RowerMaster> li = new List<RowerMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = RowMstr.BoatHouseId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RowerMaster ShowConfMstr = new RowerMaster();
                        ShowConfMstr.RowerId = dt.Rows[i]["RowerId"].ToString();
                        ShowConfMstr.RowerName = dt.Rows[i]["RowerName"].ToString();
                        ShowConfMstr.MobileNo = dt.Rows[i]["MobileNo"].ToString();
                        ShowConfMstr.AadharId = dt.Rows[i]["AadharId"].ToString();
                        ShowConfMstr.RowerType = dt.Rows[i]["RowerType"].ToString();
                        ShowConfMstr.MailId = dt.Rows[i]["MailId"].ToString();
                        ShowConfMstr.RowerTypeName = dt.Rows[i]["ConfigName"].ToString();
                        ShowConfMstr.Address1 = dt.Rows[i]["Address1"].ToString();
                        ShowConfMstr.Address2 = dt.Rows[i]["Address2"].ToString();
                        ShowConfMstr.ZipCode = dt.Rows[i]["Zipcode"].ToString();
                        ShowConfMstr.City = dt.Rows[i]["City"].ToString();
                        ShowConfMstr.District = dt.Rows[i]["District"].ToString();
                        ShowConfMstr.State = dt.Rows[i]["State"].ToString();
                        ShowConfMstr.PhotoLink = dt.Rows[i]["PhotoLink"].ToString();
                        ShowConfMstr.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                        ShowConfMstr.DriverCategory = dt.Rows[i]["DriverCategory"].ToString();
                        li.Add(ShowConfMstr);
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
        [Route("RowerMaster")]
        public IHttpActionResult MstrRowerMaster([FromBody] RowerMaster InsrRowMstr)
        {
            try
            {
                if (InsrRowMstr.QueryType != "" && InsrRowMstr.RowerName != "" && InsrRowMstr.BoatHouseId != ""
                    && InsrRowMstr.BoatHouseName != "" && InsrRowMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrRowerMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsrRowMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@RowerId", InsrRowMstr.RowerId.ToString());
                    cmd.Parameters.AddWithValue("@RowerName", InsrRowMstr.RowerName.ToString());
                    cmd.Parameters.AddWithValue("@MobileNo", InsrRowMstr.MobileNo.ToString());
                    cmd.Parameters.AddWithValue("@MailId", InsrRowMstr.MailId.Trim());
                    cmd.Parameters.AddWithValue("@AadharId", InsrRowMstr.AadharId.Trim());
                    cmd.Parameters.AddWithValue("@RowerType", InsrRowMstr.RowerType.Trim());

                    cmd.Parameters.AddWithValue("@Address1", InsrRowMstr.Address1.Trim());
                    cmd.Parameters.AddWithValue("@Address2", InsrRowMstr.Address2.Trim());
                    cmd.Parameters.AddWithValue("@City", InsrRowMstr.City.Trim());
                    cmd.Parameters.AddWithValue("@District", InsrRowMstr.District.Trim());
                    cmd.Parameters.AddWithValue("@State", InsrRowMstr.State.Trim());
                    cmd.Parameters.AddWithValue("@ZipCode", InsrRowMstr.ZipCode.Trim());
                    cmd.Parameters.AddWithValue("@DriverCategory", InsrRowMstr.DriverCategory.Trim());

                    cmd.Parameters.AddWithValue("@PhotoLink", InsrRowMstr.PhotoLink.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsrRowMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseName", InsrRowMstr.BoatHouseName.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsrRowMstr.CreatedBy.Trim());

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

        /*View Offer Discount*/
        //Offer Master List
        [HttpPost]
        [AllowAnonymous]
        [Route("OfferMstrBasedOnBoatHouse")]
        public IHttpActionResult OfferMstrBasedOnBoatHouse(OfferMaster offer)
        {
            try
            {
                if (offer.BoatHouseId != "")
                {
                    List<OfferMaster> li = new List<OfferMaster>();

                    SqlCommand cmd = new SqlCommand(" SELECT A.OfferId,A.OfferName,Case When OfferType='O' THEN 'Offer' ELSE 'Discount' END AS OfferType, "
                    + " B.ConfigName 'OfferCategoryName', A.AmountType, A.Offer, MinBillAmount, A.MinNoOfTickets, Convert(NVARCHAR(50),EffectiveFrom,105) 'EffectiveFrom',"
                    + " Convert(NVARCHAR(50),EffectiveTill,105) 'EffectiveTill', "
                    + " convert(varchar(10), A.CreatedDate, 101) + right(convert(varchar(32), A.CreatedDate, 100), 8) AS 'CreatedDate' FROM OfferMaster AS A "
                    + " INNER JOIN ConfigurationMaster AS B ON A.OfferCategory = B.ConfigID AND B.TypeId = 30 AND B.ActiveStatus = 'A' "
                    + " WHERE A.BoatHouseId LIKE @BoatHouseId AND A.ActiveStatus = 'A' "
                    + " ORDER BY A.OfferId ASC ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@BoatHouseId"].Value = '%' + offer.BoatHouseId.Trim() + '%';
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            OfferMaster OfferMasters = new OfferMaster();

                            OfferMasters.OfferId = dt.Rows[i]["OfferId"].ToString();
                            OfferMasters.OfferType = dt.Rows[i]["OfferType"].ToString();
                            OfferMasters.OfferCategoryName = dt.Rows[i]["OfferCategoryName"].ToString();
                            OfferMasters.OfferName = dt.Rows[i]["OfferName"].ToString();
                            OfferMasters.AmountType = dt.Rows[i]["AmountType"].ToString();
                            OfferMasters.Offer = dt.Rows[i]["Offer"].ToString();
                            OfferMasters.MinBillAmount = dt.Rows[i]["MinBillAmount"].ToString();
                            OfferMasters.MinNoOfTickets = dt.Rows[i]["MinNoOfTickets"].ToString();
                            OfferMasters.EffectiveFrom = dt.Rows[i]["EffectiveFrom"].ToString();
                            OfferMasters.EffectiveTill = dt.Rows[i]["EffectiveTill"].ToString();
                            OfferMasters.CreatedDate = dt.Rows[i]["CreatedDate"].ToString();

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
                    OfferMasterString OfferMaster = new OfferMasterString
                    {
                        Response = "Must Pass Boat House Id.",
                        StatusCode = 0
                    };
                    return Ok(OfferMaster);
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

        /*Material Purchase*/
        //Get Maximum Purchase No
        [HttpPost]
        [AllowAnonymous]
        [Route("MaxPurchaseNo")]
        public IHttpActionResult MaxPurchaseNo([FromBody] MaterialPurchase BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(PurchaseId), 0) + 1 'PurchaseId' from MaterialPurHdr", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = dt.Rows[0]["PurchaseId"].ToString(),
                            StatusCode = 1
                        };
                        return Ok(ItemMasters1);
                    }

                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "0",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
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

        //Get Purchase Details List
        [HttpPost]
        [AllowAnonymous]
        [Route("GetMaterialPurchaseDetails")]
        public IHttpActionResult GetMaterialPurchaseDetails([FromBody] MaterialPurchase BHId)
        {
            try
            {
                if (BHId.PurchaseId != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand("SELECT A.PurchaseId, A.ItemId, A.RejectedQty, B.ItemDescription, A.ReceivedQty, A.AcceptedQty, "
                        + " A.ReceivedQty, A.RejectionReason, A.PurchaseRate From MaterialPurDtl AS A "
                        + " Inner Join ItemMaster AS B On A.ItemId = B.ItemId AND A.EntityId = B.EntityId and B.ActiveStatus = 'A' "
                        + " WHERE A.PurchaseId = @PurchaseId  AND A.EntityId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PurchaseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@PurchaseId"].Value = BHId.PurchaseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialPurchase MaterialPurchase = new MaterialPurchase();
                            MaterialPurchase.PurchaseId = dt.Rows[i]["PurchaseId"].ToString();
                            MaterialPurchase.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialPurchase.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();
                            MaterialPurchase.ReceivedQty = dt.Rows[i]["ReceivedQty"].ToString();
                            MaterialPurchase.AcceptedQty = dt.Rows[i]["AcceptedQty"].ToString();
                            MaterialPurchase.RejectionReason = dt.Rows[i]["RejectionReason"].ToString();
                            MaterialPurchase.RejectedQty = dt.Rows[i]["RejectedQty"].ToString();
                            MaterialPurchase.PurchaseRate = dt.Rows[i]["PurchaseRate"].ToString();

                            li.Add(MaterialPurchase);
                        }
                        MaterialPurchaseList ItemMasters = new MaterialPurchaseList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
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

        //All Material Purchase
        [HttpPost]
        [AllowAnonymous]
        [Route("AllMaterialPurchase")]
        public IHttpActionResult AllMaterialPurchase([FromBody] MaterialPurchase BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand("SELECT A.PurchaseId,CONVERT(NVARCHAR(50), A.PurchaseDate,105) AS PurchaseDate, "
                        + " A.ActiveStatus,A.VendorRef,"
                        + " ISNULL(SUM(B.PurchaseRate),0) 'PurchaseRate',Count(B.PurchaseId) 'NoOfItems' FROM MaterialPurHdr AS A "
                        + " INNER JOIN MaterialPurDtl AS B ON A.PurchaseId = B.PurchaseId AND A.EntityId=B.EntityId "
                        + " Inner Join ItemMaster AS C On B.ItemId = C.ItemId and C.ActiveStatus = 'A' "
                        + " WHERE A.ActiveStatus IN('A', 'D') AND A.EntityId = @BoatHouseId "
                        + " GROUP BY A.PurchaseId, A.ActiveStatus, A.PurchaseDate,A.VendorRef ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialPurchase MaterialPurchase = new MaterialPurchase();
                            MaterialPurchase.PurchaseId = dt.Rows[i]["PurchaseId"].ToString();
                            MaterialPurchase.PurchaseDate = dt.Rows[i]["PurchaseDate"].ToString();
                            MaterialPurchase.VendorRef = dt.Rows[i]["VendorRef"].ToString();
                            MaterialPurchase.PurchaseRate = dt.Rows[i]["PurchaseRate"].ToString();
                            MaterialPurchase.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            MaterialPurchase.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                            li.Add(MaterialPurchase);
                        }
                        MaterialPurchaseList ItemMasters = new MaterialPurchaseList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
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

        //Material Details based on Purchase
        [HttpPost]
        [AllowAnonymous]
        [Route("MaterialDetailsBasedOnPurchaseId")]
        public IHttpActionResult MaterialDetailsBasedOnPurchaseId([FromBody] MaterialPurchase BHId)
        {
            try
            {
                if (BHId.PurchaseId != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand("SELECT A.PurchaseId,A.ItemId,B.ItemDescription,A.ReceivedQty,A.AcceptedQty,A.RejectedQty, "
                        + " A.RejectionReason,A.PurchaseRate FROM MaterialPurDtl AS A "
                        + " INNER JOIN ItemMaster AS B ON A.ItemId = B.ItemId AND A.EntityId = B.EntityId "
                        + " where A.PurchaseId = @PurchaseId AND A.EntityId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PurchaseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@PurchaseId"].Value = BHId.PurchaseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialPurchase MaterialPurchase = new MaterialPurchase();
                            MaterialPurchase.PurchaseId = dt.Rows[i]["PurchaseId"].ToString();
                            MaterialPurchase.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialPurchase.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();
                            MaterialPurchase.ReceivedQty = dt.Rows[i]["ReceivedQty"].ToString();
                            MaterialPurchase.AcceptedQty = dt.Rows[i]["AcceptedQty"].ToString();
                            MaterialPurchase.RejectedQty = dt.Rows[i]["RejectedQty"].ToString();
                            MaterialPurchase.RejectionReason = dt.Rows[i]["RejectionReason"].ToString();
                            MaterialPurchase.PurchaseRate = dt.Rows[i]["PurchaseRate"].ToString();
                            li.Add(MaterialPurchase);
                        }
                        MaterialPurchaseList ItemMasters = new MaterialPurchaseList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
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

        //Automatic Delete Material Purchase
        [HttpPost]
        [AllowAnonymous]
        [Route("AutomaticDeleteMaterial")]
        public IHttpActionResult AutomaticDeleteMaterial([FromBody] MaterialPurchase BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand("DELETE from MaterialPurDtl where EntityId = @BoatHouseId "
                        + " AND PurchaseId NOT IN(Select PurchaseId From MaterialPurHdr Where EntityId = @BoatHouseId ) ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "Record Delete Successfully.",
                            StatusCode = 1
                        };
                        return Ok(ItemMasters1);
                    }
                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "Record Delete Un Successfully.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                return Ok();
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

        //Material Purchase Detail Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("MaterialPurchaseDetail")]
        public IHttpActionResult MaterialPurchaseDetail([FromBody] MaterialPurchase InsMatPur)
        {
            try
            {
                if (InsMatPur.QueryType != "" && InsMatPur.PurchaseId != "" && InsMatPur.PurchaseDate != ""
                    && InsMatPur.ItemId != "" && InsMatPur.EntityId != "" && InsMatPur.EntityName != ""
                    && InsMatPur.VendorRef != "" && InsMatPur.ReceivedQty != ""
                    && InsMatPur.AcceptedQty != "" && InsMatPur.RejectedQty != ""
                    && InsMatPur.PurchaseRate != "" && InsMatPur.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrMaterialPurchaseDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsMatPur.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@PurchaseId", InsMatPur.PurchaseId.ToString());
                    cmd.Parameters.AddWithValue("@ItemId", InsMatPur.ItemId.ToString());
                    cmd.Parameters.AddWithValue("@PurchaseDate", DateTime.Parse(InsMatPur.PurchaseDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@EntityId", InsMatPur.EntityId.ToString());
                    cmd.Parameters.AddWithValue("@EntityName", InsMatPur.EntityName.Trim());
                    cmd.Parameters.AddWithValue("@VendorRef", InsMatPur.VendorRef.ToString());
                    cmd.Parameters.AddWithValue("@ReceivedQty", InsMatPur.ReceivedQty.ToString());
                    cmd.Parameters.AddWithValue("@AcceptedQty", InsMatPur.AcceptedQty.ToString());
                    cmd.Parameters.AddWithValue("@RejectedQty", InsMatPur.RejectedQty.Trim());
                    cmd.Parameters.AddWithValue("@RejectionReason", InsMatPur.RejectionReason.Trim());
                    cmd.Parameters.AddWithValue("@PurchaseRate", InsMatPur.PurchaseRate.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsMatPur.Createdby.Trim());

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


        //Material Edit Details
        [HttpPost]
        [AllowAnonymous]
        [Route("MaterilEditDetails")]
        public IHttpActionResult MaterilEditDetails([FromBody] MaterialPurchase BHId)
        {
            try
            {
                if (BHId.PurchaseId != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand(" SELECT A.PurchaseId, CONVERT(NVARCHAR(50),B.PurchaseDate,105) 'PurchaseDate', "
                        + " B.VendorRef, A.ItemId, A.ReceivedQty, A.AcceptedQty, A.RejectedQty, "
                        + " A.RejectionReason, A.PurchaseRate from MaterialPurDtl AS A "
                        + " INNER JOIN MaterialPurHdr AS B ON A.PurchaseId = B.PurchaseId AND A.EntityId=B.EntityId AND B.ActiveStatus = 'A' "
                        + " Inner Join ItemMaster AS C On A.ItemId = C.ItemId and C.ActiveStatus = 'A' "
                        + " WHERE A.PurchaseId = @PurchaseId AND A.EntityId = @BoatHouseId", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PurchaseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@PurchaseId"].Value = BHId.PurchaseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialPurchase MaterialPurchase = new MaterialPurchase();
                            MaterialPurchase.PurchaseId = dt.Rows[i]["PurchaseId"].ToString();
                            MaterialPurchase.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialPurchase.PurchaseDate = dt.Rows[i]["PurchaseDate"].ToString();
                            MaterialPurchase.VendorRef = dt.Rows[i]["VendorRef"].ToString();
                            MaterialPurchase.ReceivedQty = dt.Rows[i]["ReceivedQty"].ToString();
                            MaterialPurchase.AcceptedQty = dt.Rows[i]["AcceptedQty"].ToString();
                            MaterialPurchase.RejectionReason = dt.Rows[i]["RejectionReason"].ToString();
                            MaterialPurchase.RejectedQty = dt.Rows[i]["RejectedQty"].ToString();
                            MaterialPurchase.PurchaseRate = dt.Rows[i]["PurchaseRate"].ToString();

                            li.Add(MaterialPurchase);
                        }
                        MaterialPurchaseList ItemMasters = new MaterialPurchaseList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
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

        //Activate Materials
        [HttpPost]
        [AllowAnonymous]
        [Route("ActiveInActiveMaterial")]
        public IHttpActionResult ActiveInActiveMaterial([FromBody] MaterialPurchase BHId)
        {
            try
            {
                if (BHId.PurchaseId != "" && BHId.QueryType != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();
                    if (BHId.QueryType.Trim() == "Delete")
                    {
                        SqlCommand cmd = new SqlCommand("Update MaterialPurHdr set ActiveStatus = 'D' "
                            + " Where PurchaseId = @PurchaseId  AND EntityId = @BoatHouseId", con);

                        cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("@PurchaseId", System.Data.SqlDbType.Int));
                        cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                        cmd.Parameters["@PurchaseId"].Value = BHId.PurchaseId.Trim();
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();

                        if (i > 0)
                        {
                            MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                            {
                                Response = "Record De Active Successfully.",
                                StatusCode = 1
                            };
                            return Ok(ItemMasters1);
                        }
                        else if (BHId.QueryType.Trim() == "ReActive")
                        {
                            MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                            {
                                Response = "Failure To De Active.",
                                StatusCode = 0
                            };
                            return Ok(ItemMasters1);
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Update MaterialPurHdr set ActiveStatus = 'A' Where PurchaseId = " + BHId.PurchaseId.Trim() + "  AND EntityId = '" + BHId.BoatHouseId.Trim() + "' ", con);
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i > 0)
                        {
                            MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                            {
                                Response = "Record  Active Successfully.",
                                StatusCode = 1
                            };
                            return Ok(ItemMasters1);
                        }

                        else
                        {
                            MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                            {
                                Response = "Failure To Active.",
                                StatusCode = 0
                            };
                            return Ok(ItemMasters1);

                        }
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
                }
                return Ok();
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return Ok(ConfRes);
            }
        }

        /*Material Issue*/
        //Maximum of Material Issue
        [HttpPost]
        [AllowAnonymous]
        [Route("MaxIssueNo")]
        public IHttpActionResult MaxIssueNo([FromBody] MaterialIssue BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<MaterialPurchase> li = new List<MaterialPurchase>();

                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(IssueId), 0) + 1 'IssueId' from MaterialIssHdr", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = dt.Rows[0]["IssueId"].ToString(),
                            StatusCode = 1
                        };
                        return Ok(ItemMasters1);
                    }

                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "0",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
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

        //Get Material Issue Details
        [HttpPost]
        [AllowAnonymous]
        [Route("GetMaterialIssueDetails")]
        public IHttpActionResult GetMaterialIssueDetails([FromBody] MaterialIssue BHId)
        {
            try
            {
                if (BHId.IssueId != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialIssue> li = new List<MaterialIssue>();

                    SqlCommand cmd = new SqlCommand("SELECT A.IssueId,A.ItemId,A.IssuedQty,B.ItemDescription,A.IssueRate From MaterialIssDtl AS A "
                                 + " Inner Join ItemMaster AS B On A.ItemId = B.ItemId AND A.EntityId = B.EntityId and B.ActiveStatus = 'A' "
                                + " AND A.IssueId = @PurchaseId  AND A.EntityId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@PurchaseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@PurchaseId"].Value = BHId.IssueId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialIssue MaterialIssues = new MaterialIssue();
                            MaterialIssues.IssueId = dt.Rows[i]["IssueId"].ToString();
                            MaterialIssues.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialIssues.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();
                            MaterialIssues.IssuedQty = dt.Rows[i]["IssuedQty"].ToString();
                            MaterialIssues.IssueRate = dt.Rows[i]["IssueRate"].ToString();

                            li.Add(MaterialIssues);
                        }
                        MaterialIssueList ItemMasters = new MaterialIssueList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialIssueString ItemMasters1 = new MaterialIssueString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialIssueString ItemMasters1 = new MaterialIssueString
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


        //Get Material Issue Details All
        [HttpPost]
        [AllowAnonymous]
        [Route("AllMaterialIssue")]
        public IHttpActionResult AllMaterialIssue([FromBody] MaterialIssue BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<MaterialIssue> li = new List<MaterialIssue>();

                    SqlCommand cmd = new SqlCommand("SELECT A.IssueId,CONVERT(NVARCHAR(50), A.IssueDate,105) AS IssueDate,A.ActiveStatus,A.IssueRef,"
                        + " ISNULL(SUM(B.IssueRate), 0) 'IssueRate', Count(B.IssueId) 'NoOfItems' FROM MaterialIssHdr AS A "
                        + "  INNER JOIN MaterialIssDtl AS B ON A.IssueId = B.IssueId AND A.EntityId = B.EntityId Inner Join ItemMaster AS C "
                        + " On B.ItemId = C.ItemId and C.ActiveStatus = 'A'  WHERE A.ActiveStatus IN('A', 'D') AND A.EntityId = @BoatHouseId "
                        + " GROUP BY A.IssueId, A.ActiveStatus, A.IssueDate, A.IssueRef", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialIssue MaterialPurchase = new MaterialIssue();
                            MaterialPurchase.IssueId = dt.Rows[i]["IssueId"].ToString();
                            MaterialPurchase.IssueDate = dt.Rows[i]["IssueDate"].ToString();
                            MaterialPurchase.IssueRef = dt.Rows[i]["IssueRef"].ToString();
                            MaterialPurchase.IssueRate = dt.Rows[i]["IssueRate"].ToString();
                            MaterialPurchase.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
                            MaterialPurchase.NoOfItems = dt.Rows[i]["NoOfItems"].ToString();
                            li.Add(MaterialPurchase);
                        }
                        MaterialIssueList ItemMasters = new MaterialIssueList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialIssueString ItemMasters1 = new MaterialIssueString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialIssueString ItemMasters1 = new MaterialIssueString
                    {
                        Response = "Must Pass Boat House Id",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
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

        //Automatic Material Issue Delete
        [HttpPost]
        [AllowAnonymous]
        [Route("AutomaticDeleteMatIssDtl")]
        public IHttpActionResult AutomaticDeleteMatIssDtl([FromBody] MaterialIssue BHId)
        {
            try
            {
                if (BHId.BoatHouseId != "")
                {
                    List<MaterialIssue> li = new List<MaterialIssue>();

                    SqlCommand cmd = new SqlCommand("DELETE from MaterialIssDtl where EntityId = @BoatHouseId "
                        + " AND IssueId NOT IN(Select IssueId From MaterialIssHdr Where EntityId = @BoatHouseId) ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        MaterialIssueString ItemMasters1 = new MaterialIssueString
                        {
                            Response = "Record Delete Successfully.",
                            StatusCode = 1
                        };
                        return Ok(ItemMasters1);
                    }
                    else
                    {
                        MaterialIssueString ItemMasters1 = new MaterialIssueString
                        {
                            Response = "Record Delete Un Successfully.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                return Ok();
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
        [Route("MaterialIssue")]
        public IHttpActionResult MstrMaterialIssue([FromBody] MaterialIssue InsMatPur)
        {
            try
            {
                if (InsMatPur.QueryType != "" && InsMatPur.IssueId != "" && InsMatPur.ItemId != ""
                    && InsMatPur.IssueDate != "" && InsMatPur.EntityId != "" && InsMatPur.EntityName != "" && InsMatPur.IssuedQty != ""
                    && InsMatPur.IssueRef != "" && InsMatPur.IssueRate != "" && InsMatPur.Createdby != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrMaterialIssueDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsMatPur.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@IssueId", InsMatPur.IssueId.ToString());
                    cmd.Parameters.AddWithValue("@ItemId", InsMatPur.ItemId.ToString());
                    cmd.Parameters.AddWithValue("@IssueDate", DateTime.Parse(InsMatPur.IssueDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@EntityId", InsMatPur.EntityId.ToString());
                    cmd.Parameters.AddWithValue("@EntityName", InsMatPur.EntityName.Trim());
                    cmd.Parameters.AddWithValue("@IssueRef", InsMatPur.IssueRef.ToString());
                    cmd.Parameters.AddWithValue("@IssuedQty", InsMatPur.IssuedQty.ToString());
                    cmd.Parameters.AddWithValue("@IssuedRate", InsMatPur.IssueRate.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsMatPur.Createdby.Trim());

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

        //Material Issue Details Edit
        [HttpPost]
        [AllowAnonymous]
        [Route("MaterilIssueEditDetails")]
        public IHttpActionResult MaterilIssueEditDetails([FromBody] MaterialIssue BHId)
        {
            try
            {
                if (BHId.IssueId != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialIssue> li = new List<MaterialIssue>();

                    SqlCommand cmd = new SqlCommand(" SELECT A.IssueId, CONVERT(NVARCHAR(50),B.IssueDate,105) 'IssueDate', B.IssueRef, A.ItemId, A.IssuedQty, "
                                + " A.IssueRate from MaterialIssDtl AS A "
                                + " INNER JOIN MaterialIssHdr AS B ON A.IssueId = B.IssueId AND A.EntityId = B.EntityId AND B.ActiveStatus = 'A' "
                                + " Inner Join ItemMaster AS C On A.ItemId = C.ItemId and C.ActiveStatus = 'A' "
                                + " WHERE A.IssueId = @IssueId AND A.EntityId = @BoatHouseId", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@IssueId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@IssueId"].Value = BHId.IssueId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialIssue MaterialPurchase = new MaterialIssue();
                            MaterialPurchase.IssueId = dt.Rows[i]["IssueId"].ToString();
                            MaterialPurchase.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialPurchase.IssueDate = dt.Rows[i]["IssueDate"].ToString();
                            MaterialPurchase.IssueRef = dt.Rows[i]["IssueRef"].ToString();
                            MaterialPurchase.IssuedQty = dt.Rows[i]["IssuedQty"].ToString();
                            MaterialPurchase.IssueRate = dt.Rows[i]["IssueRate"].ToString();

                            li.Add(MaterialPurchase);
                        }
                        MaterialIssueList ItemMasters = new MaterialIssueList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialPurchaseString ItemMasters1 = new MaterialPurchaseString
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

        //Active Material Issue Details
        [HttpPost]
        [AllowAnonymous]
        [Route("ActiveInActiveMatIss")]
        public IHttpActionResult ActiveInActiveMatIss([FromBody] MaterialIssue BHId)
        {
            try
            {
                if (BHId.IssueId != "" && BHId.QueryType != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialIssue> li = new List<MaterialIssue>();
                    if (BHId.QueryType.Trim() == "Delete")
                    {
                        SqlCommand cmd = new SqlCommand("Update MaterialIssHdr set ActiveStatus = 'D' "
                            + " Where IssueId = @IssueId  AND EntityId = @BoatHouseId ", con);

                        cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("@IssueId", System.Data.SqlDbType.Int));
                        cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                        cmd.Parameters["@IssueId"].Value = BHId.IssueId.Trim();
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();

                        if (i > 0)
                        {
                            MaterialIssueString ItemMasters1 = new MaterialIssueString
                            {
                                Response = "Material Issue details Inactive Successfully.",
                                StatusCode = 1
                            };
                            return Ok(ItemMasters1);
                        }
                        else if (BHId.QueryType.Trim() == "ReActive")
                        {
                            MaterialIssueString ItemMasters1 = new MaterialIssueString
                            {
                                Response = "Failure To De Active.",
                                StatusCode = 0
                            };
                            return Ok(ItemMasters1);
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Update MaterialIssHdr set ActiveStatus = 'A' Where IssueId = @IssueId  AND EntityId = @BoatHouseId ", con);
                        cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new SqlParameter("@IssueId", System.Data.SqlDbType.Int));
                        cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                        cmd.Parameters["@IssueId"].Value = BHId.IssueId.Trim();
                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();
                        if (i > 0)
                        {
                            MaterialIssueString ItemMasters1 = new MaterialIssueString
                            {
                                Response = "Material Issue details Active Successfully.",
                                StatusCode = 1
                            };
                            return Ok(ItemMasters1);
                        }

                        else
                        {
                            MaterialIssueString ItemMasters1 = new MaterialIssueString
                            {
                                Response = "Failure To Active.",
                                StatusCode = 0
                            };
                            return Ok(ItemMasters1);

                        }
                    }
                }
                else
                {
                    MaterialIssueString ItemMasters1 = new MaterialIssueString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(ItemMasters1);
                }
                return Ok();
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

        //Get Material Issue Details on Issue Id
        [HttpPost]
        [AllowAnonymous]
        [Route("MaterialIssueDetailsBasedOnIssueId")]
        public IHttpActionResult MaterialIssueDetailsBasedOnIssueId([FromBody] MaterialIssue BHId)
        {
            try
            {
                if (BHId.IssueId != "" && BHId.BoatHouseId != "")
                {
                    List<MaterialIssue> li = new List<MaterialIssue>();

                    SqlCommand cmd = new SqlCommand("SELECT A.IssueId, A.ItemId, B.ItemDescription, A.IssuedQty, A.IssueRate FROM MaterialIssDtl AS A"
                    + " INNER JOIN ItemMaster AS B ON A.ItemId = B.ItemId AND A.EntityId = B.EntityId "
                    + " where A.IssueId = @IssueId AND A.EntityId = @BoatHouseId ", con);

                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@IssueId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = BHId.BoatHouseId.Trim();
                    cmd.Parameters["@IssueId"].Value = BHId.IssueId.Trim();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MaterialIssue MaterialPurchase = new MaterialIssue();
                            MaterialPurchase.IssueId = dt.Rows[i]["IssueId"].ToString();
                            MaterialPurchase.ItemId = dt.Rows[i]["ItemId"].ToString();
                            MaterialPurchase.ItemDescription = dt.Rows[i]["ItemDescription"].ToString();
                            MaterialPurchase.IssuedQty = dt.Rows[i]["IssuedQty"].ToString();
                            MaterialPurchase.IssueRate = dt.Rows[i]["IssueRate"].ToString();
                            li.Add(MaterialPurchase);
                        }
                        MaterialIssueList ItemMasters = new MaterialIssueList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ItemMasters);
                    }
                    else
                    {
                        MaterialIssueString ItemMasters1 = new MaterialIssueString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(ItemMasters1);
                    }
                }
                else
                {
                    MaterialIssueString ItemMasters1 = new MaterialIssueString
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
        [Route("GetBoatHouseTiming")]
        public IHttpActionResult GetBoatHouseTiming([FromBody] getBoatHouseMaster BHTiming)
        {
            try
            {
                string sQuery = string.Empty;

                sQuery = "SELECT BoatHouseId, BoatHouseName, BookingFrom, BookingTo, ClosingBeforeTime FROM BHMaster "
                    + " WHERE ActiveStatus = 'A' AND BoatHouseId = @BoatHouseId";

                List<getBoatHouseMaster> li = new List<getBoatHouseMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = BHTiming.BoatHouseId.Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        getBoatHouseMaster ShowBoathouseMstr = new getBoatHouseMaster();
                        ShowBoathouseMstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                        ShowBoathouseMstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                        ShowBoathouseMstr.BookingFrom = dt.Rows[i]["BookingFrom"].ToString();
                        ShowBoathouseMstr.BookingTo = dt.Rows[i]["BookingTo"].ToString();
                        ShowBoathouseMstr.ClosingBeforeTime = dt.Rows[i]["ClosingBeforeTime"].ToString();
                        li.Add(ShowBoathouseMstr);
                    }

                    getBoatHouseMasterList ConfList = new getBoatHouseMasterList
                    {
                        Response = li,
                        StatusCode = 1
                    };
                    return Ok(ConfList);
                }
                else
                {
                    getBoatHouseMasterString ConfRes = new getBoatHouseMasterString
                    {
                        Response = "No Records Found.",
                        StatusCode = 0
                    };
                    return Ok(ConfRes);
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

        [HttpPost]
        [AllowAnonymous]
        [Route("GetRowerDetails")]
        public IHttpActionResult GetRowerDetails([FromBody] RowerMaster RowMstr)
        {
            try
            {
                string sQuery = string.Empty;

                sQuery = " SELECT CAST(A.RowerId AS NVARCHAR) AS 'RowerId',A.RowerName, A.MobileNo, A.AadharId,A.RowerType,B.ConfigName, A.MailId, A.Address1, "
                            + " A.Address2, A.Zipcode, A.City, A.District, A.State, A.PhotoLink, A.ActiveStatus,A.DriverCategory FROM RowerMaster AS A "
                            + " INNER JOIN ConfigurationMaster AS B ON A.RowerType = B.ConfigID AND B.TypeID = '26' AND B.ActiveStatus = 'A'  "
                            + " WHERE BoatHouseId = @BoatHouseId AND A.RowerId = @RowerId ";

                List<RowerMaster> li = new List<RowerMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@RowerId", System.Data.SqlDbType.Int));
                cmd.Parameters["@BoatHouseId"].Value = RowMstr.BoatHouseId.ToString().Trim();
                cmd.Parameters["@RowerId"].Value = RowMstr.RowerId.ToString().Trim();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "RowerDetails");
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
        [Route("CommonOperation")]
        public IHttpActionResult CommonCancelMethod([FromBody] CommonAPIMethod PinDet)
        {
            try
            {
                if (PinDet.QueryType != "")
                {
                    SqlCommand cmd = new SqlCommand("CommonOperation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", PinDet.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@ServiceType", PinDet.ServiceType.Trim());
                    cmd.Parameters.AddWithValue("@BookingId", PinDet.BookingId.Trim());
                    cmd.Parameters.AddWithValue("@BoatHouseId", PinDet.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@Input1", PinDet.Input1.ToString());
                    cmd.Parameters.AddWithValue("@Input2", PinDet.Input2.ToString());
                    cmd.Parameters.AddWithValue("@Input3", PinDet.Input3.ToString());
                    cmd.Parameters.AddWithValue("@Input4", PinDet.Input4.ToString());
                    cmd.Parameters.AddWithValue("@Input5", PinDet.Input5.ToString());

                    if (PinDet.FromDate.ToString() != "")
                    {
                        cmd.Parameters.AddWithValue("@FromDate", DateTime.Parse(PinDet.FromDate.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FromDate", "");
                    }

                    if (PinDet.ToDate.ToString() != "")
                    {
                        cmd.Parameters.AddWithValue("@ToDate", DateTime.Parse(PinDet.ToDate.ToString(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ToDate", "");
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
        /// Created By : Vediyappan
        /// Created Date : 23-07-2021
        /// Hosted Date : 23-07-2021
        /// Hosted By : Vediyappan
        /// Modified By : JayaSuriya
        /// Modified Date : 13-09-2021
        /// </summary>
        /// <param name="BoatSlot"></param>
        /// <returns></returns>        
        [HttpPost]
        [AllowAnonymous]
        [Route("GetAvailableBoatSlotTime")]
        public IHttpActionResult GetBoatSlot([FromBody] BoatSlotMaster BoatSlot)
        {
            try
            {
                if (BoatSlot.BoatHouseId != "")
                {
                    List<BoatSlotMaster> li = new List<BoatSlotMaster>();
                    SqlCommand cmd = new SqlCommand("GetAvailableBoatBookingSlot", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "GetAvailableSlot");
                    cmd.Parameters.AddWithValue("@BoatHouseId", BoatSlot.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@CheckInDate", DateTime.Parse(BoatSlot.CheckInDate.ToString(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatTypeId", BoatSlot.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", BoatSlot.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@SlotType", BoatSlot.SlotType.ToString());

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
                            BoatSlotMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            BoatSlotMaster.TotalTripCount = dt.Rows[i]["TotalTripCount"].ToString();
                            BoatSlotMaster.BookedTripCount = dt.Rows[i]["BookedTripCount"].ToString();
                            BoatSlotMaster.AvailableTripCount = dt.Rows[i]["AvailableTripCount"].ToString();
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
        /// Created By : Vediyappan.
        /// Created Date : 30-07-2021
        /// Hosted Date : 03-08-2021
        /// Hosted By : Vediyappan
        /// Modified Date : 24-08-2021
        /// Modified By : Abhi
        /// </summary>
        /// <param name="BoatSlot"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetAvailableBoatSlotTimeDept")]
        public IHttpActionResult GetAvailableBoatSlotTimeDept([FromBody] BoatSlotMaster BoatSlot)
        {
            try
            {
                if (BoatSlot.BoatHouseId != "")
                {
                    List<BoatSlotMaster> li = new List<BoatSlotMaster>();
                    SqlCommand cmd = new SqlCommand("GetAvailableBoatBookingSlotDept", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", BoatSlot.QueryType.ToString());
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
                            BoatSlotMaster.BlockId = dt.Rows[i]["BlockId"].ToString();

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
        /// Create Date : 31-07-2021
        /// Created By : Silambarasu
        /// Hosted Date : 03-08-2021
        /// Hosted By : Vediyappan
        /// </summary>
        /// <param name="WeekDay Tariff Master"></param>
        /// <returns></returns>      
        [HttpPost]
        [AllowAnonymous]
        [Route("GetWeekDayTariff")]
        public IHttpActionResult GetWeekDayTariffDtls([FromBody] WeekDaysMaster weekDays)
        {
            try
            {
                if (weekDays.BoatHouseId != "")
                {
                    List<WeekDaysMaster> li = new List<WeekDaysMaster>();

                    SqlCommand cmd = new SqlCommand("GetReportBasicDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", "GetWeekDayTariff");
                    cmd.Parameters.AddWithValue("@ServiceType", "");
                    cmd.Parameters.AddWithValue("@BoatHouseId", weekDays.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@Input1", "");
                    cmd.Parameters.AddWithValue("@Input2", "");
                    cmd.Parameters.AddWithValue("@Input3", "");
                    cmd.Parameters.AddWithValue("@Input4", "");
                    cmd.Parameters.AddWithValue("@Input5", "");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            WeekDaysMaster Wdmstr = new WeekDaysMaster();

                            Wdmstr.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
                            Wdmstr.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
                            Wdmstr.WeekDays = dt.Rows[i]["WeekDays"].ToString();
                            Wdmstr.WeekDaysDesc = dt.Rows[i]["WeekDayDesc"].ToString();
                            Wdmstr.HolidayDate = dt.Rows[i]["HolidayDate"].ToString();
                            Wdmstr.HolidayDesc = dt.Rows[i]["HolidayDesc"].ToString();

                            li.Add(Wdmstr);
                        }

                        WeekDaysMasterList wdmstrlst = new WeekDaysMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(wdmstrlst);
                    }

                    else
                    {
                        WeekDaysMasterString wdmstr = new WeekDaysMasterString
                        {
                            Response = "No Records Found.",
                            StatusCode = 0
                        };
                        return Ok(wdmstr);
                    }
                }
                else
                {
                    WeekDaysMasterString wdmstrres = new WeekDaysMasterString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(wdmstrres);
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
        /// Created Date : 31-07-2021
        /// Created By : Silambarasu
        /// Hosted Date : 03-08-2021
        /// Hosted By : Vediyappan
        /// </summary>
        /// <param name="WeekDay Tariff Master"></param>
        /// <returns></returns>        
        [HttpPost]
        [AllowAnonymous]
        [Route("InsWeekDayTariff")]
        public IHttpActionResult InsWeekDayTariff([FromBody] WeekDaysMaster InsweekDays)
        {
            try
            {
                if (InsweekDays.QueryType != "" && InsweekDays.BoatHouseId != "" && InsweekDays.WeekDays != ""
                    && InsweekDays.WeekDaysDesc != "" && InsweekDays.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("Mstr_WeekDays", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;

                    cmd.Parameters.AddWithValue("@QueryType", InsweekDays.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsweekDays.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@WeekDays", InsweekDays.WeekDays.ToString());
                    cmd.Parameters.AddWithValue("@WeekDayDesc", InsweekDays.WeekDaysDesc.Trim());
                    if (InsweekDays.HolidayDate != "")
                    {
                        cmd.Parameters.AddWithValue("@HolidayDate", DateTime.Parse(InsweekDays.HolidayDate.Trim(), objEnglishDate));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@HolidayDate", InsweekDays.HolidayDate.Trim());
                    }
                    cmd.Parameters.AddWithValue("@HolidayDesc", InsweekDays.HolidayDesc.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsweekDays.CreatedBy.Trim());

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
                        TaxMasterRes TxMstr = new TaxMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(TxMstr);
                    }
                    else
                    {
                        TaxMasterRes TxMstr = new TaxMasterRes
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(TxMstr);
                    }
                }
                else
                {
                    TaxMasterRes TxMstr = new TaxMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(TxMstr);
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
        /// Created By :Pretheka
        /// Created Date : 19-10-2021
        /// This Method Is used To Deactivate Boat Slot Master
        /// </summary>
        /// <param name="DaBoatSlotMstr"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("DeactiveBoatSlotMaster")]
        public IHttpActionResult DeactiveBoatSlotMaster([FromBody] BoatSlotMaster DaBoatSlotMstr)
        {
            try
            {
                if (DaBoatSlotMstr.BoatHouseId != "" && DaBoatSlotMstr.CreatedBy != "")
                {
                    string sReturn = string.Empty;
                    string sQuery = string.Empty;
                    string conditions = string.Empty;
                    if (DaBoatSlotMstr.BoatTypeId != "0")
                    {
                        conditions += " AND BoatTypeId = @BoatTypeId ";

                    }
                    if (DaBoatSlotMstr.BoatSeaterId != "0")
                    {
                        conditions += " AND BoatSeaterId = @BoatSeaterId ";

                    }

                    if (DaBoatSlotMstr.ServiceType != "0")
                    {
                        conditions += " AND SlotType = @SlotType ";

                    }

                    sQuery = " UPDATE BoatSlotMaster SET ActiveStatus = 'D' ,UpdatedBy = @CreatedBy,UpdatedDate = GETDATE() "
                             + " Where BoatHouseId = @BoatHouseId"
                             + " " + conditions + "  AND ActiveStatus='A'  ";


                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.Add(new SqlParameter("@BoatTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatSeaterId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@SlotType", System.Data.SqlDbType.Char));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = DaBoatSlotMstr.BoatHouseId.Trim();
                    cmd.Parameters["@BoatTypeId"].Value = DaBoatSlotMstr.BoatTypeId;
                    cmd.Parameters["@BoatSeaterId"].Value = DaBoatSlotMstr.BoatSeaterId;
                    cmd.Parameters["@CreatedBy"].Value = DaBoatSlotMstr.CreatedBy;
                    cmd.Parameters["@SlotType"].Value = DaBoatSlotMstr.ServiceType;
                    con.Open();
                    int sResult = cmd.ExecuteNonQuery();
                    con.Close();

                    if (sResult > 0)
                    {
                        BoatSlotMasterRes SlotMstr = new BoatSlotMasterRes
                        {
                            Response = "Boat Slot Master Deactivated Sucessfully",

                            StatusCode = 1
                        };
                        return Ok(SlotMstr);
                    }
                    else
                    {
                        BoatSlotMasterRes SlotMstr = new BoatSlotMasterRes
                        {
                            Response = "Slot Type Is Already Deactivated",
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
                BoatSlotMasterRes ConfRes = new BoatSlotMasterRes
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
        /// Created By :Pretheka
        /// Created Date : 20-10-2021
        /// This Method Is used To Check Slot Type
        /// </summary>
        /// <param name="Slottype"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatSlotTypeChk")]
        public IHttpActionResult getSlotTypeChk([FromBody] BoatSlotMaster Slottype)
        {

            try
            {
                if (Slottype.BoatHouseId != null)
                {
                    List<BoatSlotMaster> li = new List<BoatSlotMaster>();
                    con.Open();
                    string conditions = string.Empty;
                    if (Slottype.ServiceType != "0")
                    {
                        conditions += " AND SlotType = @SlotType";

                    }
                    SqlCommand cmd = new SqlCommand("select distinct SlotType from BoatSlotMaster where BoatHouseId = @BoatHouseId and ActiveStatus='A'"
                                    + " " + conditions + "", con);

                    cmd.Parameters.Add(new SqlParameter("@SlotType", System.Data.SqlDbType.Char));
                    cmd.Parameters.Add(new SqlParameter("@BoatHouseId", System.Data.SqlDbType.Int));
                    cmd.Parameters["@BoatHouseId"].Value = Slottype.BoatHouseId.Trim();
                    cmd.Parameters["@SlotType"].Value = Slottype.ServiceType;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            BoatSlotMaster ShowSlotMstr = new BoatSlotMaster();
                            ShowSlotMstr.SlotType = dt.Rows[i]["SlotType"].ToString();


                            li.Add(ShowSlotMstr);
                        }

                        BoatSlotMasterList ConfList = new BoatSlotMasterList
                        {
                            Response = li,
                            StatusCode = 1
                        };
                        return Ok(ConfList);
                    }

                    else
                    {
                        BoatSlotMasterRes ConfRes = new BoatSlotMasterRes
                        {
                            Response = "Slot Type Is Already Deactivated.",
                            StatusCode = 0
                        };
                        return Ok(ConfRes);
                    }
                }
                else
                {
                    BoatSlotMasterRes Vehicle = new BoatSlotMasterRes
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(Vehicle);
                }
            }
            catch (Exception ex)
            {
                BoatSlotMasterRes ConfRes = new BoatSlotMasterRes
                {
                    Response = Convert.ToString(ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString()),
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
        /// Created Date : 31-07-2021
        /// This Methid Is Used Insert Boat Slot Master
        /// </summary>
        /// <param name="InsBoatSlotMstr"></param>
        /// <returns></returns>  
        [HttpPost]
        [AllowAnonymous]
        [Route("BoatSlotMaster")]
        public IHttpActionResult MstrBoatSlotMaster([FromBody] BoatSlotMaster InsBoatSlotMstr)
        {
            try
            {
                if (InsBoatSlotMstr.BoatHouseId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("GetBoatSlotDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsBoatSlotMstr.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@ServiceType", InsBoatSlotMstr.ServiceType.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsBoatSlotMstr.BoatHouseId.ToString());
                    cmd.Parameters.AddWithValue("@SlotDuration", InsBoatSlotMstr.SlotDuration.ToString());
                    cmd.Parameters.AddWithValue("@BoatTypeId", InsBoatSlotMstr.BoatTypeId.ToString());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", InsBoatSlotMstr.BoatSeaterId.ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsBoatSlotMstr.CreatedBy.Trim());

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
        /// Created Date : 02-08-2021
        /// Created By : Silambarasu
        /// </summary>
        /// <param name="Delete Temporary Booked Slot"></param>
        /// <returns></returns>        
        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteTmpBookedSlotDept")]
        public IHttpActionResult DeleteTmpBookedSlot([FromBody] BoatSlotMaster DelTmpSlot)
        {
            try
            {
                if (DelTmpSlot.BoatHouseId != "" && DelTmpSlot.CheckInDate != "" && DelTmpSlot.BoatTypeId != "" && DelTmpSlot.BoatSeaterId != ""
                    && DelTmpSlot.SlotType != "" && DelTmpSlot.SlotId != "" && DelTmpSlot.UserId != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("DeleteTemporarySlotDept", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@BoatHouseId", DelTmpSlot.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@CheckInDate", DateTime.Parse(DelTmpSlot.CheckInDate.Trim(), objEnglishDate));
                    cmd.Parameters.AddWithValue("@BoatTypeId", DelTmpSlot.BoatTypeId.Trim());
                    cmd.Parameters.AddWithValue("@BoatSeaterId", DelTmpSlot.BoatSeaterId.Trim());
                    cmd.Parameters.AddWithValue("@BookingType", DelTmpSlot.SlotType.Trim());
                    cmd.Parameters.AddWithValue("@SlotId", DelTmpSlot.SlotId.Trim());
                    cmd.Parameters.AddWithValue("@UserId", DelTmpSlot.UserId.Trim());
                    cmd.Parameters.AddWithValue("@BlockId", DelTmpSlot.BlockId.Trim());

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
        /// Created By : Subalakshmi
        /// Created Date : 25-10-2021
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ConfigMstr/DDLEmailServiceType")]
        public IHttpActionResult getEmailServiceType()
        {
            try
            {
                List<ConfigurationMaster> li = new List<ConfigurationMaster>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ConfigID, ConfigName FROM " + ConfigurationManager.AppSettings["CommonDB"] + ".dbo.ConfigurationMaster Where ActiveStatus='A' And TypeId=11", con);
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
        /// Created BY:Jaya suriya A
        /// Created Date:20/12/2021
        /// This API Is Used To Insert and Update Sadmin Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("SadminAccessRights")]
        public IHttpActionResult SadminAccessRights([FromBody] UserProfile SadminLists)
        {
            try
            {
                if (SadminLists.QueryType != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("Sp_SadminAccessRights", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", SadminLists.QueryType.Trim());
                    cmd.Parameters.AddWithValue("@UserId", SadminLists.UserId.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", SadminLists.CreatedBy.ToString().Trim());
                    cmd.Parameters.AddWithValue("@UniqueId", SadminLists.UniqueId.ToString().Trim());
                    SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 100);
                    RuturnValue.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(RuturnValue);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
                    string[] sResult = sReturn.Split('~');
                    con.Close();

                    if (sResult[0].ToString().Trim() == "Success")
                    {
                        UserProfileRes SadminListsRes = new UserProfileRes()
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(SadminListsRes);
                    }
                    else
                    {
                        UserProfileRes SadminListsRes = new UserProfileRes()
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(SadminListsRes);
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

        //Insert
        [HttpPost]
        [AllowAnonymous]
        [Route("LateRefund")]
        public IHttpActionResult mstrLateRefund([FromBody] LateRefund InsLateRefund)
        {
            try
            {
                if (InsLateRefund.QueryType != "" && InsLateRefund.BookingPin != ""
                    && InsLateRefund.BookingId != "" && InsLateRefund.BoatHouseId != ""
                    && InsLateRefund.ComplaintNo != "" && InsLateRefund.CreatedBy != ""
                    && InsLateRefund.RefundDate != "")
                {
                    string sReturn = string.Empty;
                    SqlCommand cmd = new SqlCommand("MstrLateRefund", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.CommandTimeout = 10000000;
                    cmd.Parameters.AddWithValue("@QueryType", InsLateRefund.QueryType.ToString());
                    cmd.Parameters.AddWithValue("@BookingPin", InsLateRefund.BookingPin.ToString());
                    cmd.Parameters.AddWithValue("@BookingId", InsLateRefund.BookingId.ToString());
                    cmd.Parameters.AddWithValue("@BoatHouseId", InsLateRefund.BoatHouseId.Trim());
                    cmd.Parameters.AddWithValue("@ComplaintNo", InsLateRefund.ComplaintNo.Trim());
                    cmd.Parameters.AddWithValue("@RefundDate", InsLateRefund.RefundDate.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", InsLateRefund.CreatedBy.Trim());
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
                        LateRefundString refund = new LateRefundString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 1
                        };
                        return Ok(refund);
                    }
                    else
                    {
                        LateRefundString refund = new LateRefundString
                        {
                            Response = sResult[1].Trim(),
                            StatusCode = 0
                        };
                        return Ok(refund);
                    }
                }
                else
                {
                    LateRefundString refund = new LateRefundString
                    {
                        Response = "Must Pass All Parameters",
                        StatusCode = 0
                    };
                    return Ok(refund);
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

        ////[HttpPost]
        ////[AllowAnonymous]
        ////[Route("EmailIdPwdDetails")]
        ////public IHttpActionResult EmailIdPwdDetails([FromBody] EmailIdPwd InsrEmlPwd)
        ////{
        ////    try
        ////    {
        ////        if (InsrEmlPwd.QueryType != "" && InsrEmlPwd.EmailId != "" && InsrEmlPwd.Password != ""
        ////            && InsrEmlPwd.CreatedBy != "")
        ////        {
        ////            string sReturn = string.Empty;
        ////            SqlCommand cmd = new SqlCommand("MstrEmailIdPwdDetails", con);
        ////            cmd.CommandType = CommandType.StoredProcedure;
        ////            cmd.Parameters.Clear();
        ////            cmd.CommandTimeout = 10000000;
        ////            cmd.Parameters.AddWithValue("@QueryType", InsrEmlPwd.QueryType.ToString());
        ////            cmd.Parameters.AddWithValue("@UniqueId", InsrEmlPwd.UniqueId.ToString());
        ////            cmd.Parameters.AddWithValue("@EmailId", InsrEmlPwd.EmailId.ToString());

        ////            cmd.Parameters.AddWithValue("@Password", InsrEmlPwd.Password.ToString());
        ////            cmd.Parameters.AddWithValue("@CreatedBy", InsrEmlPwd.CreatedBy.Trim());

        ////            SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
        ////            RuturnValue.Direction = ParameterDirection.Output;
        ////            cmd.Parameters.Add(RuturnValue);
        ////            con.Open();
        ////            cmd.ExecuteNonQuery();
        ////            sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
        ////            string[] sResult = sReturn.Split('~');
        ////            con.Close();

        ////            if (sResult[0].Trim() == "Success")
        ////            {
        ////                EmployeeMasterRes EmMstr = new EmployeeMasterRes
        ////                {
        ////                    Response = sResult[1].Trim(),
        ////                    StatusCode = 1
        ////                };
        ////                return Ok(EmMstr);
        ////            }
        ////            else
        ////            {
        ////                EmployeeMasterRes EmMstr = new EmployeeMasterRes
        ////                {
        ////                    Response = sResult[1].Trim(),
        ////                    StatusCode = 0
        ////                };
        ////                return Ok(EmMstr);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            EmployeeMasterRes EmMstr = new EmployeeMasterRes
        ////            {
        ////                Response = "Must Pass All Parameters",
        ////                StatusCode = 0
        ////            };
        ////            return Ok(EmMstr);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        ////        {
        ////            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        ////            StatusCode = 0
        ////        };
        ////        if (con.State == ConnectionState.Open)
        ////        {
        ////            con.Close();
        ////        }
        ////        return Ok(ConfRes);
        ////    }
        ////}



        ////[HttpPost]
        ////[AllowAnonymous]
        ////[Route("AdminUserAccess")]
        ////public IHttpActionResult AdminUserAccess([FromBody] AdminAccess InsUserAcc)
        ////{
        ////    try
        ////    {
        ////        string sReturn = string.Empty;
        ////        SqlCommand cmd = new SqlCommand("MstrUserAccessRights", con);
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Parameters.Clear();
        ////        cmd.CommandTimeout = 10000000;
        ////        cmd.Parameters.AddWithValue("@QueryType", InsUserAcc.QueryType.ToString());
        ////        cmd.Parameters.AddWithValue("@UserId", InsUserAcc.UserId.ToString());
        ////        cmd.Parameters.AddWithValue("@UserName", InsUserAcc.UserName.ToString());
        ////        cmd.Parameters.AddWithValue("@UserRole", InsUserAcc.UserRole.ToString());
        ////        cmd.Parameters.AddWithValue("@MMaster", InsUserAcc.MMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MBS", InsUserAcc.MBS.ToString());
        ////        cmd.Parameters.AddWithValue("@MTMS", InsUserAcc.MTMS.ToString());
        ////        cmd.Parameters.AddWithValue("@MHMS", InsUserAcc.MHMS.ToString());
        ////        cmd.Parameters.AddWithValue("@MAccounts", InsUserAcc.MAccounts.ToString());

        ////        cmd.Parameters.AddWithValue("@MComMaster", InsUserAcc.MComMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MBhMaster", InsUserAcc.MBhMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MHotelMaster", InsUserAcc.MHotelMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MTourMaster", InsUserAcc.MTourMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MAccessRights", InsUserAcc.MAccessRights.ToString());
        ////        cmd.Parameters.AddWithValue("@MOtherMaster", InsUserAcc.MOtherMaster.ToString());

        ////        cmd.Parameters.AddWithValue("@BMaster", InsUserAcc.BMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@BTransaction", InsUserAcc.BTransaction.ToString());
        ////        cmd.Parameters.AddWithValue("@BBooking", InsUserAcc.BBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BReports", InsUserAcc.BReports.ToString());
        ////        cmd.Parameters.AddWithValue("@BRestaurant", InsUserAcc.BRestaurant.ToString());

        ////        if (InsUserAcc.BBoatingService.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BBoatingService", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BBoatingService", InsUserAcc.BBoatingService.ToString());
        ////        }

        ////        if (InsUserAcc.BAdditionalService.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BAdditionalService", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BAdditionalService", InsUserAcc.BAdditionalService.ToString());
        ////        }

        ////        if (InsUserAcc.BOtherService.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BOtherService", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BOtherService", InsUserAcc.BOtherService.ToString());
        ////        }

        ////        cmd.Parameters.AddWithValue("@BMBooking", InsUserAcc.BMBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBookingOthers", InsUserAcc.BMBookingOthers.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBulkBooking", InsUserAcc.BMBulkBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BMAdditionalService", InsUserAcc.BMAdditionalService.ToString());
        ////        cmd.Parameters.AddWithValue("@BMOtherService", InsUserAcc.BMOtherService.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBulkOtherService", InsUserAcc.BMBulkOtherService.ToString());
        ////        cmd.Parameters.AddWithValue("@BMKioskBooking", InsUserAcc.BMKioskBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BMTripSheet", InsUserAcc.BMTripSheet.ToString());

        ////        if (InsUserAcc.BTripSheetOptions.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BTripSheetOptions", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BTripSheetOptions", InsUserAcc.BTripSheetOptions.ToString());
        ////        }

        ////        cmd.Parameters.AddWithValue("@BMChangeTripSheet", InsUserAcc.BMChangeTripSheet.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBoatReTripDetails", InsUserAcc.BMBoatReTripDetails.ToString());
        ////        cmd.Parameters.AddWithValue("@BMChangeBoatDetails", InsUserAcc.BMChangeBoatDetails.ToString());
        ////        cmd.Parameters.AddWithValue("@BMCancellation", InsUserAcc.BMCancellation.ToString());
        ////        cmd.Parameters.AddWithValue("@BMReSchedule", InsUserAcc.BMReSchedule.ToString());

        ////        cmd.Parameters.AddWithValue("@TMMaterialPur", InsUserAcc.TMMaterialPur.ToString());
        ////        cmd.Parameters.AddWithValue("@TMMaterialIss", InsUserAcc.TMMaterialIss.ToString());
        ////        cmd.Parameters.AddWithValue("@TMTripSheetSettle", InsUserAcc.TMTripSheetSettle.ToString());
        ////        cmd.Parameters.AddWithValue("@TMRowerSettle", InsUserAcc.TMRowerSettle.ToString());
        ////        cmd.Parameters.AddWithValue("@TMRefundCounter", InsUserAcc.TMRefundCounter.ToString());
        ////        cmd.Parameters.AddWithValue("@TMStockEntryMaintance", InsUserAcc.TMStockEntryMaintance.ToString());

        ////        if (InsUserAcc.BDepositRefundOptions.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BDepositRefundOptions", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BDepositRefundOptions", InsUserAcc.BDepositRefundOptions.ToString());
        ////        }


        ////        cmd.Parameters.AddWithValue("@RMBooking", InsUserAcc.RMBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@RMOtherSvc", InsUserAcc.RMOtherSvc.ToString());
        ////        cmd.Parameters.AddWithValue("@RMRestaurantService", InsUserAcc.RMRestaurantService.ToString());

        ////        cmd.Parameters.AddWithValue("@RMAbstractBoatBook", InsUserAcc.RMAbstractBoatBook.ToString());
        ////        cmd.Parameters.AddWithValue("@RMAbstractOthSvc", InsUserAcc.RMAbstractOthSvc.ToString());
        ////        cmd.Parameters.AddWithValue("@RMAbstractResSvc", InsUserAcc.RMAbstractResSvc.ToString());

        ////        cmd.Parameters.AddWithValue("@RMAvailBoatCapacity", InsUserAcc.RMAvailBoatCapacity.ToString());
        ////        cmd.Parameters.AddWithValue("@RMBoatwiseTrip", InsUserAcc.RMBoatwiseTrip.ToString());
        ////        cmd.Parameters.AddWithValue("@RMTripSheetSettle", InsUserAcc.RMTripSheetSettle.ToString());

        ////        cmd.Parameters.AddWithValue("@RMRowerCharges", InsUserAcc.RMRowerCharges.ToString());
        ////        cmd.Parameters.AddWithValue("@RMBoatCancellation", InsUserAcc.RMBoatCancellation.ToString());
        ////        cmd.Parameters.AddWithValue("@RMRowerSettle", InsUserAcc.RMRowerSettle.ToString());

        ////        cmd.Parameters.AddWithValue("@RMChallanRegister", InsUserAcc.RMChallanRegister.ToString());
        ////        cmd.Parameters.AddWithValue("@RMAbstractChallanRegister", InsUserAcc.RMAbstractChallanRegister.ToString());
        ////        cmd.Parameters.AddWithValue("@RMServiceWiseCollection", InsUserAcc.RMServiceWiseCollection.ToString());

        ////        cmd.Parameters.AddWithValue("@RMUserBookingReport", InsUserAcc.RMUserBookingReport.ToString());
        ////        cmd.Parameters.AddWithValue("@RMTripWiseCollection", InsUserAcc.RMTripWiseCollection.ToString());
        ////        cmd.Parameters.AddWithValue("@RMBoatTypeRowerList", InsUserAcc.RMBoatTypeRowerList.ToString());

        ////        cmd.Parameters.AddWithValue("@OfflineRights", InsUserAcc.OfflineRights.ToString());
        ////        cmd.Parameters.AddWithValue("@MBoatInfoDisplay", InsUserAcc.MBoatInfoDisplay.ToString());

        ////        cmd.Parameters.AddWithValue("@CreatedBy", InsUserAcc.CreatedBy.ToString());
        ////        cmd.Parameters.AddWithValue("@BoatHouseId", InsUserAcc.BoatHouseId.ToString());
        ////        cmd.Parameters.AddWithValue("@BoatHouseName", InsUserAcc.BoatHouseName.ToString());

        ////        SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
        ////        RuturnValue.Direction = ParameterDirection.Output;
        ////        cmd.Parameters.Add(RuturnValue);
        ////        con.Open();
        ////        cmd.ExecuteNonQuery();
        ////        sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
        ////        string[] sResult = sReturn.Split('~');
        ////        con.Close();

        ////        if (sResult[0].Trim() == "Success")
        ////        {
        ////            UserRegistrationRes ConMstr = new UserRegistrationRes
        ////            {
        ////                Response = sResult[1].Trim(),
        ////                StatusCode = 1
        ////            };
        ////            return Ok(ConMstr);
        ////        }
        ////        else
        ////        {
        ////            UserRegistrationRes ConMstr = new UserRegistrationRes
        ////            {
        ////                Response = sResult[1].Trim(),
        ////                StatusCode = 0
        ////            };
        ////            return Ok(ConMstr);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        ////        {
        ////            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        ////            StatusCode = 0
        ////        };
        ////        if (con.State == ConnectionState.Open)
        ////        {
        ////            con.Close();
        ////        }
        ////        return Ok(ConfRes);
        ////    }
        ////}

        ////[HttpPost]
        ////[AllowAnonymous]
        ////[Route("AdminUserAccess")]
        ////public IHttpActionResult AdminUserAccess([FromBody] AdminAccess InsUserAcc)
        ////{
        ////    try
        ////    {
        ////        string sReturn = string.Empty;
        ////        SqlCommand cmd = new SqlCommand("MstrUserAccessRights", con);
        ////        cmd.CommandType = CommandType.StoredProcedure;
        ////        cmd.Parameters.Clear();
        ////        cmd.CommandTimeout = 10000000;
        ////        cmd.Parameters.AddWithValue("@QueryType", InsUserAcc.QueryType.ToString());
        ////        cmd.Parameters.AddWithValue("@UserId", InsUserAcc.UserId.ToString());
        ////        cmd.Parameters.AddWithValue("@UserName", InsUserAcc.UserName.ToString());
        ////        cmd.Parameters.AddWithValue("@UserRole", InsUserAcc.UserRole.ToString());
        ////        cmd.Parameters.AddWithValue("@MMaster", InsUserAcc.MMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MBS", InsUserAcc.MBS.ToString());
        ////        cmd.Parameters.AddWithValue("@MTMS", InsUserAcc.MTMS.ToString());
        ////        cmd.Parameters.AddWithValue("@MHMS", InsUserAcc.MHMS.ToString());
        ////        cmd.Parameters.AddWithValue("@MAccounts", InsUserAcc.MAccounts.ToString());

        ////        cmd.Parameters.AddWithValue("@MComMaster", InsUserAcc.MComMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MBhMaster", InsUserAcc.MBhMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MHotelMaster", InsUserAcc.MHotelMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MTourMaster", InsUserAcc.MTourMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@MAccessRights", InsUserAcc.MAccessRights.ToString());
        ////        cmd.Parameters.AddWithValue("@MOtherMaster", InsUserAcc.MOtherMaster.ToString());

        ////        cmd.Parameters.AddWithValue("@BMaster", InsUserAcc.BMaster.ToString());
        ////        cmd.Parameters.AddWithValue("@BTransaction", InsUserAcc.BTransaction.ToString());
        ////        cmd.Parameters.AddWithValue("@BBooking", InsUserAcc.BBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BReports", InsUserAcc.BReports.ToString());
        ////        cmd.Parameters.AddWithValue("@BRestaurant", InsUserAcc.BRestaurant.ToString());

        ////        if (InsUserAcc.BBoatingService.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BBoatingService", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BBoatingService", InsUserAcc.BBoatingService.ToString());
        ////        }

        ////        if (InsUserAcc.BAdditionalService.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BAdditionalService", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BAdditionalService", InsUserAcc.BAdditionalService.ToString());
        ////        }

        ////        if (InsUserAcc.BOtherService.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BOtherService", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BOtherService", InsUserAcc.BOtherService.ToString());
        ////        }

        ////        cmd.Parameters.AddWithValue("@BMBooking", InsUserAcc.BMBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBookingOthers", InsUserAcc.BMBookingOthers.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBulkBooking", InsUserAcc.BMBulkBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BMAdditionalService", InsUserAcc.BMAdditionalService.ToString());
        ////        cmd.Parameters.AddWithValue("@BMOtherService", InsUserAcc.BMOtherService.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBulkOtherService", InsUserAcc.BMBulkOtherService.ToString());
        ////        cmd.Parameters.AddWithValue("@BMKioskBooking", InsUserAcc.BMKioskBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@BMTripSheet", InsUserAcc.BMTripSheet.ToString());

        ////        if (InsUserAcc.BTripSheetOptions.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BTripSheetOptions", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BTripSheetOptions", InsUserAcc.BTripSheetOptions.ToString());
        ////        }

        ////        cmd.Parameters.AddWithValue("@BMChangeTripSheet", InsUserAcc.BMChangeTripSheet.ToString());
        ////        cmd.Parameters.AddWithValue("@BMBoatReTripDetails", InsUserAcc.BMBoatReTripDetails.ToString());
        ////        cmd.Parameters.AddWithValue("@BMChangeBoatDetails", InsUserAcc.BMChangeBoatDetails.ToString());
        ////        cmd.Parameters.AddWithValue("@BMCancellation", InsUserAcc.BMCancellation.ToString());
        ////        cmd.Parameters.AddWithValue("@BMReSchedule", InsUserAcc.BMReSchedule.ToString());

        ////        cmd.Parameters.AddWithValue("@TMMaterialPur", InsUserAcc.TMMaterialPur.ToString());
        ////        cmd.Parameters.AddWithValue("@TMMaterialIss", InsUserAcc.TMMaterialIss.ToString());
        ////        cmd.Parameters.AddWithValue("@TMTripSheetSettle", InsUserAcc.TMTripSheetSettle.ToString());
        ////        cmd.Parameters.AddWithValue("@TMRowerSettle", InsUserAcc.TMRowerSettle.ToString());
        ////        cmd.Parameters.AddWithValue("@TMRefundCounter", InsUserAcc.TMRefundCounter.ToString());

        ////        if (InsUserAcc.BDepositRefundOptions.ToString() == "")
        ////        {
        ////            cmd.Parameters.AddWithValue("@BDepositRefundOptions", "NULL");
        ////        }
        ////        else
        ////        {
        ////            cmd.Parameters.AddWithValue("@BDepositRefundOptions", InsUserAcc.BDepositRefundOptions.ToString());
        ////        }


        ////        cmd.Parameters.AddWithValue("@RMBooking", InsUserAcc.RMBooking.ToString());
        ////        cmd.Parameters.AddWithValue("@RMOtherSvc", InsUserAcc.RMOtherSvc.ToString());
        ////        cmd.Parameters.AddWithValue("@RMRestaurantService", InsUserAcc.RMRestaurantService.ToString());

        ////        cmd.Parameters.AddWithValue("@RMAbstractBoatBook", InsUserAcc.RMAbstractBoatBook.ToString());
        ////        cmd.Parameters.AddWithValue("@RMAbstractOthSvc", InsUserAcc.RMAbstractOthSvc.ToString());
        ////        cmd.Parameters.AddWithValue("@RMAbstractResSvc", InsUserAcc.RMAbstractResSvc.ToString());

        ////        cmd.Parameters.AddWithValue("@RMAvailBoatCapacity", InsUserAcc.RMAvailBoatCapacity.ToString());
        ////        cmd.Parameters.AddWithValue("@RMBoatwiseTrip", InsUserAcc.RMBoatwiseTrip.ToString());
        ////        cmd.Parameters.AddWithValue("@RMTripSheetSettle", InsUserAcc.RMTripSheetSettle.ToString());

        ////        cmd.Parameters.AddWithValue("@RMRowerCharges", InsUserAcc.RMRowerCharges.ToString());
        ////        cmd.Parameters.AddWithValue("@RMBoatCancellation", InsUserAcc.RMBoatCancellation.ToString());
        ////        cmd.Parameters.AddWithValue("@RMRowerSettle", InsUserAcc.RMRowerSettle.ToString());

        ////        cmd.Parameters.AddWithValue("@RMChallanRegister", InsUserAcc.RMChallanRegister.ToString());
        ////        cmd.Parameters.AddWithValue("@RMAbstractChallanRegister", InsUserAcc.RMAbstractChallanRegister.ToString());
        ////        cmd.Parameters.AddWithValue("@RMServiceWiseCollection", InsUserAcc.RMServiceWiseCollection.ToString());

        ////        cmd.Parameters.AddWithValue("@RMUserBookingReport", InsUserAcc.RMUserBookingReport.ToString());
        ////        cmd.Parameters.AddWithValue("@RMTripWiseCollection", InsUserAcc.RMTripWiseCollection.ToString());
        ////        cmd.Parameters.AddWithValue("@RMBoatTypeRowerList", InsUserAcc.RMBoatTypeRowerList.ToString());

        ////        cmd.Parameters.AddWithValue("@OfflineRights", InsUserAcc.OfflineRights.ToString());
        ////        cmd.Parameters.AddWithValue("@MBoatInfoDisplay", InsUserAcc.MBoatInfoDisplay.ToString());

        ////        cmd.Parameters.AddWithValue("@CreatedBy", InsUserAcc.CreatedBy.ToString());
        ////        cmd.Parameters.AddWithValue("@BoatHouseId", InsUserAcc.BoatHouseId.ToString());
        ////        cmd.Parameters.AddWithValue("@BoatHouseName", InsUserAcc.BoatHouseName.ToString());

        ////        SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
        ////        RuturnValue.Direction = ParameterDirection.Output;
        ////        cmd.Parameters.Add(RuturnValue);
        ////        con.Open();
        ////        cmd.ExecuteNonQuery();
        ////        sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
        ////        string[] sResult = sReturn.Split('~');
        ////        con.Close();

        ////        if (sResult[0].Trim() == "Success")
        ////        {
        ////            UserRegistrationRes ConMstr = new UserRegistrationRes
        ////            {
        ////                Response = sResult[1].Trim(),
        ////                StatusCode = 1
        ////            };
        ////            return Ok(ConMstr);
        ////        }
        ////        else
        ////        {
        ////            UserRegistrationRes ConMstr = new UserRegistrationRes
        ////            {
        ////                Response = sResult[1].Trim(),
        ////                StatusCode = 0
        ////            };
        ////            return Ok(ConMstr);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        ////        {
        ////            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        ////            StatusCode = 0
        ////        };
        ////        return Ok(ConfRes);
        ////    }
        ////}

        ////Get Boat Rate Master List
        ////[HttpPost]
        ////[AllowAnonymous]
        ////[Route("BoatRateMstr/BHId")]
        ////public IHttpActionResult getBoatRateMstrId([FromBody] BoatRateMaster bHMstr)
        ////{
        ////    try
        ////    {
        ////        if (bHMstr.BoatHouseId != "")
        ////        {
        ////            List<BoatRateMaster> li = new List<BoatRateMaster>();

        ////            //SqlCommand cmd = new SqlCommand("SELECT A.BoatTypeId,C.BoatType,A.BoatHouseId,B.BoatHouseName,A.BoatSeaterId,D.SeaterType,A.BoatImageLink,"
        ////            //   + " CASE When  A.SelfDrive='A' Then 'Allowed' else  'Not Allowed' End As SelfDrive,A.Deposit,CASE When  A.TimeExtension='A' Then 'Allowed' else  'Not Allowed' End As TimeExtension, "
        ////            //   + " A.BoatMinTime, A.BoatGraceTime, CASE When  A.DepositType='P' Then 'Percentage' else  'Fixed Amount' End As DepositTypeName, A.DepositType,"

        ////            //   + " A.BoatMinTotAmt, A.BoatMinCharge, A.RowerMinCharge, A.BoatMinTaxAmt, "
        ////            //   + " A.BoatPremTotAmt, A.BoatPremMinCharge, A.RowerPremMinCharge, A.BoatPremTaxAmt, A.MaxTripsPerDay,"

        ////            //   + " A.ChildApplicable, A.NoofChildApplicable, A.ChargePerChild, A.ChargePerChildTotAmt, A.ChargePerChildTaxAmt, A.ActiveStatus FROM BoatRateMaster AS A "

        ////            //   + " INNER JOIN BHMaster AS B ON A.BoatHouseId=B.BoatHouseId  "
        ////            //   + " INNER JOIN BoatTypes AS C ON A.BoatTypeId=C.BoatTypeId AND  A.BoatHouseId=C.BoatHouseId "
        ////            //   + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId=D.BoatSeaterId  AND A.BoatHouseId=D.BoatHouseId WHERE  "
        ////            //   + " A.BoatHouseId = '" + bHMstr.BoatHouseId.ToString().Trim() + "' ", con);

        ////            SqlCommand cmd = new SqlCommand("SELECT A.BoatTypeId,C.BoatType,A.BoatHouseId,B.BoatHouseName,A.BoatSeaterId,D.SeaterType,A.BoatImageLink,"
        ////               + " CASE When  A.SelfDrive='A' Then 'Allowed' else  'Not Allowed' End As SelfDrive,A.Deposit,CASE When  A.TimeExtension='A' Then 'Allowed' else  'Not Allowed' End As TimeExtension, "
        ////               + " A.BoatMinTime, A.BoatGraceTime, CASE When  A.DepositType='P' Then 'Percentage' else  'Fixed Amount' End As DepositTypeName, A.DepositType,"

        ////               + " A.BoatMinTotAmt, A.BoatMinCharge, A.RowerMinCharge, A.BoatMinTaxAmt, "
        ////               + " A.BoatPremTotAmt, A.BoatPremMinCharge, A.RowerPremMinCharge, A.BoatPremTaxAmt, A.MaxTripsPerDay,A.DisplayOrder,"

        ////               + " A.ChildApplicable, A.NoofChildApplicable, A.ChargePerChild, A.ChargePerChildTotAmt, A.ChargePerChildTaxAmt, A.ActiveStatus FROM BoatRateMaster AS A "

        ////               + " INNER JOIN BHMaster AS B ON A.BoatHouseId=B.BoatHouseId  "
        ////               + " INNER JOIN BoatTypes AS C ON A.BoatTypeId=C.BoatTypeId AND  A.BoatHouseId=C.BoatHouseId "
        ////               + " INNER JOIN BoatSeat AS D ON A.BoatSeaterId=D.BoatSeaterId  AND A.BoatHouseId=D.BoatHouseId WHERE  "
        ////               + " A.BoatHouseId = '" + bHMstr.BoatHouseId.ToString().Trim() + "'  ORDER BY A.DisplayOrder Asc ", con);

        ////            SqlDataAdapter da = new SqlDataAdapter(cmd);
        ////            DataTable dt = new DataTable();
        ////            da.Fill(dt);
        ////            if (dt.Rows.Count > 0)
        ////            {
        ////                for (int i = 0; i < dt.Rows.Count; i++)
        ////                {
        ////                    BoatRateMaster BoatRateMaster = new BoatRateMaster();

        ////                    BoatRateMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        ////                    BoatRateMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
        ////                    BoatRateMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        ////                    BoatRateMaster.BoatSeaterName = dt.Rows[i]["SeaterType"].ToString();
        ////                    BoatRateMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();

        ////                    BoatRateMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        ////                    BoatRateMaster.BoatImageLink = dt.Rows[i]["BoatImageLink"].ToString();
        ////                    BoatRateMaster.SelfDrive = dt.Rows[i]["SelfDrive"].ToString();
        ////                    BoatRateMaster.DepositType = dt.Rows[i]["DepositType"].ToString();
        ////                    BoatRateMaster.DepositTypeName = dt.Rows[i]["DepositTypeName"].ToString();

        ////                    BoatRateMaster.Deposit = dt.Rows[i]["Deposit"].ToString();
        ////                    BoatRateMaster.TimeExtension = dt.Rows[i]["TimeExtension"].ToString();
        ////                    BoatRateMaster.BoatMinTime = dt.Rows[i]["BoatMinTime"].ToString();
        ////                    BoatRateMaster.BoatGraceTime = dt.Rows[i]["BoatGraceTime"].ToString();
        ////                    BoatRateMaster.BoatMinTotAmt = dt.Rows[i]["BoatMinTotAmt"].ToString();

        ////                    BoatRateMaster.BoatMinCharge = dt.Rows[i]["BoatMinCharge"].ToString();
        ////                    BoatRateMaster.RowerMinCharge = dt.Rows[i]["RowerMinCharge"].ToString();
        ////                    BoatRateMaster.BoatMinTaxAmt = dt.Rows[i]["BoatMinTaxAmt"].ToString();
        ////                    BoatRateMaster.BoatPremTotAmt = dt.Rows[i]["BoatPremTotAmt"].ToString();
        ////                    BoatRateMaster.BoatPremMinCharge = dt.Rows[i]["BoatPremMinCharge"].ToString();

        ////                    BoatRateMaster.RowerPremMinCharge = dt.Rows[i]["RowerPremMinCharge"].ToString();
        ////                    BoatRateMaster.BoatPremTaxAmt = dt.Rows[i]["BoatPremTaxAmt"].ToString();
        ////                    BoatRateMaster.MaxTripsPerDay = dt.Rows[i]["MaxTripsPerDay"].ToString();
        ////                    BoatRateMaster.ChildApplicable = dt.Rows[i]["ChildApplicable"].ToString();
        ////                    BoatRateMaster.NoofChildApplicable = dt.Rows[i]["NoofChildApplicable"].ToString();

        ////                    BoatRateMaster.ChargePerChild = dt.Rows[i]["ChargePerChild"].ToString();
        ////                    BoatRateMaster.ChargePerChildTotAmt = dt.Rows[i]["ChargePerChildTotAmt"].ToString();
        ////                    BoatRateMaster.ChargePerChildTaxAmt = dt.Rows[i]["ChargePerChildTaxAmt"].ToString();
        ////                    BoatRateMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
        ////                    BoatRateMaster.DisplayOrder = dt.Rows[i]["DisplayOrder"].ToString();

        ////                    li.Add(BoatRateMaster);
        ////                }

        ////                BoatRateMasterList BoatRate = new BoatRateMasterList
        ////                {
        ////                    Response = li,
        ////                    StatusCode = 1
        ////                };
        ////                return Ok(BoatRate);
        ////            }

        ////            else
        ////            {
        ////                BoatRateMasterString BoatRate = new BoatRateMasterString
        ////                {
        ////                    Response = "No Records Found.",
        ////                    StatusCode = 0
        ////                };
        ////                return Ok(BoatRate);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            BoatHouseMasterString Vehicle = new BoatHouseMasterString
        ////            {
        ////                Response = "Must Pass All Parameters",
        ////                StatusCode = 0
        ////            };
        ////            return Ok(Vehicle);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        ////        {
        ////            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        ////            StatusCode = 0
        ////        };
        ////        return Ok(ConfRes);
        ////    }
        ////}

        ////Get Boat Rate Extension Charge List
        ///// <summary>
        ///// Blocked By : vediyappan
        ///// </summary>
        ///// <param name="BHId"></param>
        ///// <returns></returns>
        ////[HttpPost]
        ////[AllowAnonymous]
        ////[Route("BoatRateExtnChrg/BHId")]
        ////public IHttpActionResult getBoatRateExtnChrg([FromBody] BoatRateExtnCharge bHMstr)
        ////{
        ////    try
        ////    {
        ////        if (bHMstr.BoatHouseId != "")
        ////        {
        ////            List<BoatRateExtnCharge> li = new List<BoatRateExtnCharge>();

        ////            //--2021-07-02 BLocked by vediyappan Remove Deactivate Record

        ////            //SqlCommand cmd = new SqlCommand("SELECT UniqueId, BoatTypeId, BoatType, BoatSeaterId, BoatSeater, ExtensionType, "
        ////            //    + " CASE WHEN ExtensionType = 'N' THEN 'Normal Charge' ELSE 'Premium Charge' END AS ExtensionTypeName, "
        ////            //    + " ExtFromTime, ExtToTime, AmtType, "
        ////            //    + " Percentage, BoatExtnTotAmt, RowerExtnCharge, BoatExtnCharge, BoatExtnTaxAmt, "
        ////            //    + " BoatHouseId, BoatHouseName, ActiveStatus FROM BoatRateExtnCharge "
        ////            //    + " WHERE BoatHouseId = '" + bHMstr.BoatHouseId.ToString().Trim() + "'"
        ////            //    + " AND BoatTypeId = '" + bHMstr.BoatTypeId.ToString().Trim() + "' AND "
        ////            //    + " BoatSeaterId = '" + bHMstr.BoatSeaterId.ToString().Trim() + "' AND ActiveStatus IN ('T','A','D')", con);


        ////            SqlCommand cmd = new SqlCommand("SELECT UniqueId, BoatTypeId, BoatType, BoatSeaterId, BoatSeater, ExtensionType, "
        ////               + " CASE WHEN ExtensionType = 'N' THEN 'Normal Charge' ELSE 'Premium Charge' END AS ExtensionTypeName, "
        ////               + " ExtFromTime, ExtToTime, AmtType, "
        ////               + " Percentage, BoatExtnTotAmt, RowerExtnCharge, BoatExtnCharge, BoatExtnTaxAmt, "
        ////               + " BoatHouseId, BoatHouseName, ActiveStatus FROM BoatRateExtnCharge "
        ////               + " WHERE BoatHouseId = '" + bHMstr.BoatHouseId.ToString().Trim() + "'"
        ////               + " AND BoatTypeId = '" + bHMstr.BoatTypeId.ToString().Trim() + "' AND "
        ////               + " BoatSeaterId = '" + bHMstr.BoatSeaterId.ToString().Trim() + "' AND ActiveStatus IN ('T','A')", con);


        ////            SqlDataAdapter da = new SqlDataAdapter(cmd);
        ////            DataTable dt = new DataTable();
        ////            da.Fill(dt);
        ////            if (dt.Rows.Count > 0)
        ////            {
        ////                for (int i = 0; i < dt.Rows.Count; i++)
        ////                {
        ////                    BoatRateExtnCharge BoatRateMaster = new BoatRateExtnCharge();

        ////                    BoatRateMaster.UniqueId = dt.Rows[i]["UniqueId"].ToString();
        ////                    BoatRateMaster.BoatTypeId = dt.Rows[i]["BoatTypeId"].ToString();
        ////                    BoatRateMaster.BoatTypeName = dt.Rows[i]["BoatType"].ToString();
        ////                    BoatRateMaster.BoatSeaterId = dt.Rows[i]["BoatSeaterId"].ToString();
        ////                    BoatRateMaster.BoatSeaterName = dt.Rows[i]["BoatSeater"].ToString();

        ////                    BoatRateMaster.ExtensionType = dt.Rows[i]["ExtensionType"].ToString();
        ////                    BoatRateMaster.ExtensionTypeName = dt.Rows[i]["ExtensionTypeName"].ToString();
        ////                    BoatRateMaster.ExtFromTime = dt.Rows[i]["ExtFromTime"].ToString();
        ////                    BoatRateMaster.ExtToTime = dt.Rows[i]["ExtToTime"].ToString();
        ////                    BoatRateMaster.AmtType = dt.Rows[i]["AmtType"].ToString();
        ////                    BoatRateMaster.AmtPer = dt.Rows[i]["Percentage"].ToString();

        ////                    BoatRateMaster.BoatExtnTotAmt = dt.Rows[i]["BoatExtnTotAmt"].ToString();
        ////                    BoatRateMaster.RowerExtnCharge = dt.Rows[i]["RowerExtnCharge"].ToString();
        ////                    BoatRateMaster.BoatExtnCharge = dt.Rows[i]["BoatExtnCharge"].ToString();
        ////                    BoatRateMaster.BoatExtnTaxAmt = dt.Rows[i]["BoatExtnTaxAmt"].ToString();

        ////                    BoatRateMaster.BoatHouseId = dt.Rows[i]["BoatHouseId"].ToString();
        ////                    BoatRateMaster.BoatHouseName = dt.Rows[i]["BoatHouseName"].ToString();
        ////                    BoatRateMaster.ActiveStatus = dt.Rows[i]["ActiveStatus"].ToString();
        ////                    li.Add(BoatRateMaster);
        ////                }

        ////                BoatRateExtnChargeList BoatRate = new BoatRateExtnChargeList
        ////                {
        ////                    Response = li,
        ////                    StatusCode = 1
        ////                };
        ////                return Ok(BoatRate);
        ////            }

        ////            else
        ////            {
        ////                BoatRateExtnChargeRes BoatRate = new BoatRateExtnChargeRes
        ////                {
        ////                    Response = "No Records Found.",
        ////                    StatusCode = 0
        ////                };
        ////                return Ok(BoatRate);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            BoatRateExtnChargeRes Vehicle = new BoatRateExtnChargeRes
        ////            {
        ////                Response = "Must Pass All Parameters",
        ////                StatusCode = 0
        ////            };
        ////            return Ok(Vehicle);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        BoatRateExtnChargeRes ConfRes = new BoatRateExtnChargeRes
        ////        {
        ////            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        ////            StatusCode = 0
        ////        };
        ////        return Ok(ConfRes);
        ////    }
        ////}    

        ////Boat Rate Master Insert

        ////[HttpPost]
        ////[AllowAnonymous]
        ////[Route("BoatRateMstr")]
        ////public IHttpActionResult MstrBoatRateMaster([FromBody] BoatRateMaster InsBRMstr)
        ////{
        ////    try
        ////    {
        ////        if (InsBRMstr.QueryType != "" && InsBRMstr.BoatTypeId != ""
        ////            && InsBRMstr.BoatSeaterId != "" && InsBRMstr.BoatHouseId != ""
        ////            && InsBRMstr.BoatHouseName != "" && InsBRMstr.BoatMinTime != ""
        ////            && InsBRMstr.MaxTripsPerDay != "" && InsBRMstr.Createdby != "")
        ////        {
        ////            string sReturn = string.Empty;
        ////            SqlCommand cmd = new SqlCommand("MstrBoatRateMaster", con);
        ////            cmd.CommandType = CommandType.StoredProcedure;
        ////            cmd.Parameters.Clear();
        ////            cmd.CommandTimeout = 10000000;

        ////            cmd.Parameters.AddWithValue("@QueryType", InsBRMstr.QueryType.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatTypeId", InsBRMstr.BoatTypeId.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatSeaterId", InsBRMstr.BoatSeaterId.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatHouseId", InsBRMstr.BoatHouseId.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatHouseName", InsBRMstr.BoatHouseName.Trim());

        ////            cmd.Parameters.AddWithValue("@BoatImageLink", InsBRMstr.BoatImageLink.ToString());
        ////            cmd.Parameters.AddWithValue("@SelfDrive", InsBRMstr.SelfDrive.Trim());
        ////            cmd.Parameters.AddWithValue("@DepositType", InsBRMstr.DepositType.Trim());
        ////            cmd.Parameters.AddWithValue("@Deposit", InsBRMstr.Deposit.ToString());
        ////            cmd.Parameters.AddWithValue("@TimeExtension", InsBRMstr.TimeExtension.Trim());

        ////            cmd.Parameters.AddWithValue("@BoatMinTime", InsBRMstr.BoatMinTime.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatGraceTime", InsBRMstr.BoatGraceTime.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatMinTotAmt", InsBRMstr.BoatMinTotAmt.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatMinCharge", InsBRMstr.BoatMinCharge.ToString());
        ////            cmd.Parameters.AddWithValue("@RowerMinCharge", InsBRMstr.RowerMinCharge.ToString());

        ////            cmd.Parameters.AddWithValue("@BoatMinTaxAmt", InsBRMstr.BoatMinTaxAmt.ToString());

        ////            cmd.Parameters.AddWithValue("@BoatPremTotAmt", InsBRMstr.BoatPremTotAmt.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatPremMinCharge", InsBRMstr.BoatPremMinCharge.ToString());
        ////            cmd.Parameters.AddWithValue("@RowerPremMinCharge", InsBRMstr.RowerPremMinCharge.ToString());
        ////            cmd.Parameters.AddWithValue("@BoatPremTaxAmt", InsBRMstr.BoatPremTaxAmt.ToString());

        ////            cmd.Parameters.AddWithValue("@MaxTripsPerDay", InsBRMstr.MaxTripsPerDay.ToString());
        ////            cmd.Parameters.AddWithValue("@ChildApplicable", InsBRMstr.ChildApplicable.ToString());
        ////            cmd.Parameters.AddWithValue("@NoofChildApplicable", InsBRMstr.NoofChildApplicable.ToString());
        ////            cmd.Parameters.AddWithValue("@ChargePerChild", InsBRMstr.ChargePerChild.ToString());
        ////            cmd.Parameters.AddWithValue("@ChargePerChildTotAmt", InsBRMstr.ChargePerChildTotAmt.ToString());

        ////            cmd.Parameters.AddWithValue("@ChargePerChildTaxAmt", InsBRMstr.ChargePerChildTaxAmt.ToString());
        ////            cmd.Parameters.AddWithValue("@Createdby", InsBRMstr.Createdby.ToString());
        ////            cmd.Parameters.AddWithValue("@DisplayOrder", InsBRMstr.DisplayOrder.ToString());

        ////            SqlParameter RuturnValue = new SqlParameter("@SQLReturn", SqlDbType.VarChar, 500);
        ////            RuturnValue.Direction = ParameterDirection.Output;
        ////            cmd.Parameters.Add(RuturnValue);
        ////            con.Open();
        ////            cmd.ExecuteNonQuery();
        ////            sReturn = cmd.Parameters["@SQLReturn"].Value.ToString();
        ////            string[] sResult = sReturn.Split('~');
        ////            con.Close();

        ////            if (sResult[0].Trim() == "Success")
        ////            {
        ////                BoatRateMasterString ConMstr = new BoatRateMasterString
        ////                {
        ////                    Response = sResult[1].Trim(),
        ////                    StatusCode = 1
        ////                };
        ////                return Ok(ConMstr);
        ////            }
        ////            else
        ////            {
        ////                BoatRateMasterString ConMstr = new BoatRateMasterString
        ////                {
        ////                    Response = sResult[0].Trim(),
        ////                    StatusCode = 0
        ////                };
        ////                return Ok(ConMstr);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            BoatRateMasterString Vehicle = new BoatRateMasterString
        ////            {
        ////                Response = "Must Pass All Parameters",
        ////                StatusCode = 0
        ////            };
        ////            return Ok(Vehicle);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ConfigurationMasterRes ConfRes = new ConfigurationMasterRes
        ////        {
        ////            Response = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", "").ToString(),
        ////            StatusCode = 0
        ////        };
        ////        return Ok(ConfRes);
        ////    }
        ////}

        ////Boat Rate Extension Charge Insert
    }
}

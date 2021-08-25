using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TCAssociationTool.DAL
{
    public class Events
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Admin

        public Int64 InsertEvent(TCAssociationTool.Entities.Events objEvent, ref string imageurl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventId",objEvent.EventId),
                    new SqlParameter("@EventName",objEvent.EventName ),
                    new SqlParameter("@StartDate",(objEvent.StartDate==DateTime.MinValue?(object)DBNull.Value:objEvent.StartDate)),
                    new SqlParameter("@EndDate",(objEvent.EndDate==DateTime.MinValue?(object)DBNull.Value:objEvent.EndDate)),
                    new SqlParameter("@RegistrationStartDate",(objEvent.RegistrationStartDate==DateTime.MinValue?(object)DBNull.Value:objEvent.RegistrationStartDate)),
                    new SqlParameter("@RegistrationEndDate",(objEvent.RegistrationEndDate==DateTime.MinValue?(object)DBNull.Value:objEvent.RegistrationEndDate)),                
                    new SqlParameter("@BannerUrl",(imageurl==null?(object)DBNull.Value:imageurl)),
                    new SqlParameter("@EventCategoryId",(objEvent.EventCategoryId==0?(object)DBNull.Value:objEvent.EventCategoryId)),
                    new SqlParameter("@MemberCount",(objEvent.MemberCount==0?(object)DBNull.Value:objEvent.MemberCount)),
                    new SqlParameter("@Location",(objEvent.Location==null?(object)DBNull.Value:objEvent.Location)),
                    new SqlParameter("@City",(objEvent.City==null?(object)DBNull.Value:objEvent.City)),
                    new SqlParameter("@StateName",(objEvent.StateName==null?(object)DBNull.Value:objEvent.StateName)),
                    new SqlParameter("@ZipCode",(objEvent.ZipCode==null?(object)DBNull.Value:objEvent.ZipCode)),
                    new SqlParameter("@EventDetails",(objEvent.EventDetails==null?(object)DBNull.Value:objEvent.EventDetails)),
                    new SqlParameter("@ContactEmail",(objEvent.ContactEmail==null?(object)DBNull.Value:objEvent.ContactEmail)),                                      
                    new SqlParameter("@IsRegistration",(objEvent.IsRegistration==false?(object)DBNull.Value:objEvent.IsRegistration)),
                    new SqlParameter("@IsCulturalRegistration",(objEvent.IsCulturalRegistration==false?(object)DBNull.Value:objEvent.IsCulturalRegistration)), 
                    new SqlParameter("@PageTitle",(objEvent.PageTitle==null?(object)DBNull.Value:objEvent.PageTitle)),
                    new SqlParameter("@MetaDescription",(objEvent.MetaDescription==null?(object)DBNull.Value:objEvent.MetaDescription)),
                    new SqlParameter("@MetaKeywords",(objEvent.MetaKeywords==null?(object)DBNull.Value:objEvent.MetaKeywords)),
                    new SqlParameter("@TopLine",(objEvent.TopLine==null?(object)DBNull.Value:objEvent.TopLine)),
                    new SqlParameter("@UpdatedBy",objEvent.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@XMLRegistrations",(objEvent.XMLRegistrations== null ?DBNull.Value:(object)objEvent.XMLRegistrations)),
                    new SqlParameter("@QStatus",0)
                    };

                _sqlP[6].SqlDbType = SqlDbType.NVarChar;
                _sqlP[6].Size = 256;
                _sqlP[6].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[24].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[24].Value);

                imageurl = _sqlP[6].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataSet GetEventById(Int64 EventId, string EventName, ref int Status)
        {
            DataSet dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@EventName",EventName),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataSet("EventsGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

       

        public Int64 DeleteEventById(Int64 EventId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetEventsListByVariable(Int64 EventStatusId, string EventType, string StartDate, string EndDate, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventStatusId",EventStatusId),
                    new SqlParameter("@EventType",EventType),
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[8].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[8].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetEventsList(string Type, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Type",Type),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventsGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable FEGetEventsList(string Type, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Type",Type),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("FEEventsGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable RegistrationGetEventsList(ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("RegistrationEventsGetList", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateEventStatus(Int64 EventId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertEventUserInfo(TCAssociationTool.Entities.EventUserInfo objEventUserInfo, ref Int64 EventUserInfoId, ref string DocumentUrl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventId",objEventUserInfo.EventId),
                    new SqlParameter("@EventUserInfoId",EventUserInfoId),
                    new SqlParameter("@FirstName",objEventUserInfo.FirstName ),
                    new SqlParameter("@LastName",objEventUserInfo.LastName),
                    new SqlParameter("@Email",objEventUserInfo.Email),
                    new SqlParameter("@MemberId",(objEventUserInfo.MemberId==0?(object)DBNull.Value:objEventUserInfo.MemberId)),
                    new SqlParameter("@Gender",(objEventUserInfo.Gender==null?(object)DBNull.Value:objEventUserInfo.Gender)),
                    new SqlParameter("@Address",(objEventUserInfo.Address==null?(object)DBNull.Value:objEventUserInfo.Address)),                
                    new SqlParameter("@City",(objEventUserInfo.City==null?(object)DBNull.Value:objEventUserInfo.City)),
                    new SqlParameter("@State",(objEventUserInfo.State==null?(object)DBNull.Value:objEventUserInfo.State)),
                    new SqlParameter("@Mobile",(objEventUserInfo.Mobile==null?(object)DBNull.Value:objEventUserInfo.Mobile)),
                    new SqlParameter("@ItemName",(objEventUserInfo.ItemName==null?(object)DBNull.Value:objEventUserInfo.ItemName)),
                    new SqlParameter("@ItemCategory",(objEventUserInfo.ItemCategory==null?(object)DBNull.Value:objEventUserInfo.ItemCategory)),
                    new SqlParameter("@ItemDescription",(objEventUserInfo.ItemDescription==null?(object)DBNull.Value:objEventUserInfo.ItemDescription)),
                    new SqlParameter("@ItemDuration",(objEventUserInfo.ItemDuration==0?(object)DBNull.Value:objEventUserInfo.ItemDuration)),
                    new SqlParameter("@IsApproved",objEventUserInfo.IsApproved),
                    new SqlParameter("@ApprovedDate",DateTime.UtcNow),
                    new SqlParameter("@DocumentUrl",DocumentUrl),
                    new SqlParameter("@AmountPaid",(objEventUserInfo.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.AmountPaid)),
                    new SqlParameter("@ChildCount",(objEventUserInfo.ChildCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.ChildCount)),
                    new SqlParameter("@AdultCount",(objEventUserInfo.AdultCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.AdultCount)),
                    new SqlParameter("@CoupleCount",(objEventUserInfo.CoupleCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.CoupleCount)),
                    new SqlParameter("@FamilyCount",(objEventUserInfo.FamilyCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.FamilyCount)),
                    new SqlParameter("@VIPCount",(objEventUserInfo.VIPCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPCount)),
                    new SqlParameter("@VIPSingleAdultCount",(objEventUserInfo.VIPSingleAdultCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPSingleAdultCount)),
                    new SqlParameter("@VIPCoupleCount",(objEventUserInfo.VIPCoupleCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPCoupleCount)),
                    new SqlParameter("@VIPChildCount",(objEventUserInfo.VIPChildCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPChildCount)),
                    new SqlParameter("@PaymentStatusId",(objEventUserInfo.PaymentStatusId==0?(object)DBNull.Value:objEventUserInfo.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objEventUserInfo.PaymentMethodId==0?(object)DBNull.Value:objEventUserInfo.PaymentMethodId)),
                    new SqlParameter("@Comments",(objEventUserInfo.Comments==null?(object)DBNull.Value:objEventUserInfo.Comments)),
                    new SqlParameter("@OtherPaymentId",(objEventUserInfo.OtherPaymentId==null?(object)DBNull.Value:objEventUserInfo.OtherPaymentId)),
                    new SqlParameter("@PaymentMethod",(objEventUserInfo.PaymentMethod==null?(object)DBNull.Value:objEventUserInfo.PaymentMethod)),
                    new SqlParameter("@PaymentStatus",(objEventUserInfo.PaymentStatus==null?(object)DBNull.Value:objEventUserInfo.PaymentStatus)),
                    new SqlParameter("@InsertedBy",objEventUserInfo.InsertedBy),
                    new SqlParameter("@UpdatedBy",objEventUserInfo.UpdatedBy),
                    new SqlParameter("@InsertedTime",DateTime.UtcNow),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@XMLRegistrationsCounts",(objEventUserInfo.XMLRegistrationsCounts==null?(object)DBNull.Value:objEventUserInfo.XMLRegistrationsCounts))
                    };

                _sqlP[17].SqlDbType = SqlDbType.NVarChar;
                _sqlP[17].Size = 512;
                _sqlP[17].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[37].Direction = _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("FEInsertEventUserInfo", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[37].Value);
                EventUserInfoId = Convert.ToInt64(_sqlP[1].Value);

                DocumentUrl = _sqlP[16].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateEventUserPaymentInfo(TCAssociationTool.Entities.EventUserInfo objEventUserInfo)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventId",objEventUserInfo.EventId),
                    new SqlParameter("@EventUserInfoId",objEventUserInfo.EventUserInfoId),
                    new SqlParameter("@AmountPaid",(objEventUserInfo.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.AmountPaid)),
                    new SqlParameter("@PaymentStatusId",(objEventUserInfo.PaymentStatusId==0?(object)DBNull.Value:objEventUserInfo.PaymentStatusId)),
                    new SqlParameter("@PaymentMethodId",(objEventUserInfo.PaymentMethodId==0?(object)DBNull.Value:objEventUserInfo.PaymentMethodId)),
                    new SqlParameter("@PaymentMethod",(objEventUserInfo.PaymentMethod==null?(object)DBNull.Value:objEventUserInfo.PaymentMethod)),
                    new SqlParameter("@PaymentStatus",(objEventUserInfo.PaymentStatus==null?(object)DBNull.Value:objEventUserInfo.PaymentStatus)),
                    new SqlParameter("@TransactionId",(objEventUserInfo.TransactionId==null?(object)DBNull.Value:objEventUserInfo.TransactionId	)),
                    new SqlParameter("@UpdatedBy",objEventUserInfo.UpdatedBy),
                    new SqlParameter("@PaymentDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0),
                    };

               
                _sqlP[11].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("UpdateEventUserPaymentInfo", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[11].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateEventUserInfo(TCAssociationTool.Entities.EventUserInfo objEventUserInfo)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventUserInfoId",objEventUserInfo.EventUserInfoId),                   
                    new SqlParameter("@FirstName",objEventUserInfo.FirstName ),
                    new SqlParameter("@LastName",objEventUserInfo.LastName),
                    new SqlParameter("@Email",objEventUserInfo.Email),
                    new SqlParameter("@Gender",(objEventUserInfo.Gender==null?(object)DBNull.Value:objEventUserInfo.Gender)),
                    new SqlParameter("@Address",(objEventUserInfo.Address==null?(object)DBNull.Value:objEventUserInfo.Address)),                
                    new SqlParameter("@City",(objEventUserInfo.City==null?(object)DBNull.Value:objEventUserInfo.City)),
                    new SqlParameter("@State",(objEventUserInfo.State==null?(object)DBNull.Value:objEventUserInfo.State)),
                    new SqlParameter("@Mobile",(objEventUserInfo.Mobile==null?(object)DBNull.Value:objEventUserInfo.Mobile)),
                    new SqlParameter("@ItemName",(objEventUserInfo.ItemName==null?(object)DBNull.Value:objEventUserInfo.ItemName)),
                    new SqlParameter("@ItemCategory",(objEventUserInfo.ItemCategory==null?(object)DBNull.Value:objEventUserInfo.ItemCategory)),
                    new SqlParameter("@ItemDescription",(objEventUserInfo.ItemDescription==null?(object)DBNull.Value:objEventUserInfo.ItemDescription)),
                    new SqlParameter("@ItemDuration",(objEventUserInfo.ItemDuration==0?(object)DBNull.Value:objEventUserInfo.ItemDuration)),
                    new SqlParameter("@IsApproved",objEventUserInfo.IsApproved),
                    new SqlParameter("@DocumentUrl",(objEventUserInfo.DocumentUrl==null?(object)DBNull.Value:objEventUserInfo.DocumentUrl)),
                    new SqlParameter("@UpdatedBy",objEventUserInfo.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@EventOrderId",objEventUserInfo.EventOrderId),
                    new SqlParameter("@EventId",objEventUserInfo.EventId),                    
                    new SqlParameter("@PaymentStatusId",(objEventUserInfo.PaymentStatusId==0?(object)DBNull.Value:objEventUserInfo.PaymentStatusId)),
                    new SqlParameter("@PaymentDate",(objEventUserInfo.PaymentDate==DateTime.MinValue?(object)DBNull.Value:objEventUserInfo.PaymentDate)),
                    new SqlParameter("@PaymentMethodId",(objEventUserInfo.PaymentMethodId==0?(object)DBNull.Value:objEventUserInfo.PaymentMethodId)),
                    new SqlParameter("@TransactionId",(objEventUserInfo.TransactionId==null?(object)DBNull.Value:objEventUserInfo.TransactionId)),
                    new SqlParameter("@AmountPaid",(objEventUserInfo.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.AmountPaid)),
                    new SqlParameter("@ChildCount",(objEventUserInfo.ChildCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.ChildCount)),
                    new SqlParameter("@AdultCount",(objEventUserInfo.AdultCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.AdultCount)),
                    new SqlParameter("@CoupleCount",(objEventUserInfo.CoupleCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.CoupleCount)),
                    new SqlParameter("@FamilyCount",(objEventUserInfo.FamilyCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.FamilyCount)),
                    new SqlParameter("@VIPCount",(objEventUserInfo.VIPCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPCount)),
                     new SqlParameter("@VIPSingleAdultCount",(objEventUserInfo.VIPSingleAdultCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPSingleAdultCount)),
                    new SqlParameter("@VIPCoupleCount",(objEventUserInfo.VIPCoupleCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPCoupleCount)),
                    new SqlParameter("@VIPChildCount",(objEventUserInfo.VIPChildCount==Decimal.MinValue?(object)DBNull.Value:objEventUserInfo.VIPChildCount)),
                    new SqlParameter("@IsAttended",objEventUserInfo.IsAttended),                    
                    new SqlParameter("@InsertedBy",objEventUserInfo.InsertedBy),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[35].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventUserInfoUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[35].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertEventParticipant(TCAssociationTool.Entities.EventParticipants objEventParticipant)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventParticipantId",objEventParticipant.EventParticipantId),
                    new SqlParameter("@EventId",objEventParticipant.EventId),
                    new SqlParameter("@EventUserInfoId",objEventParticipant.EventUserInfoId ),
                    new SqlParameter("@FirstName",objEventParticipant.FirstName),
                    new SqlParameter("@LastName",objEventParticipant.LastName),
                    new SqlParameter("@Email",(objEventParticipant.Email==null?(object)DBNull.Value:objEventParticipant.Email)),
                    new SqlParameter("@Age",(objEventParticipant.Age==null?(object)DBNull.Value:objEventParticipant.Age)),
                    new SqlParameter("@Mobile",(objEventParticipant.Mobile==null?(object)DBNull.Value:objEventParticipant.Mobile)),
                    new SqlParameter("@IsApproved",objEventParticipant.IsApproved),
                    new SqlParameter("@UpdatedBy",objEventParticipant.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[11].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventParticipantsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[11].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateEventParticipant(TCAssociationTool.Entities.EventParticipants objEventParticipant)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventParticipantId",objEventParticipant.EventParticipantId),
                    new SqlParameter("@EventId",objEventParticipant.EventId),
                    new SqlParameter("@EventUserInfoId",objEventParticipant.EventUserInfoId ),
                    new SqlParameter("@FirstName",objEventParticipant.FirstName),
                    new SqlParameter("@LastName",objEventParticipant.LastName),
                    new SqlParameter("@Email",objEventParticipant.Email),
                    new SqlParameter("@Age",(objEventParticipant.Age==null?(object)DBNull.Value:objEventParticipant.Age)),
                    new SqlParameter("@Mobile",(objEventParticipant.Mobile==null?(object)DBNull.Value:objEventParticipant.Mobile)),
                    new SqlParameter("@IsApproved",objEventParticipant.IsApproved),
                    new SqlParameter("@UpdatedBy",objEventParticipant.UpdatedBy),
                    new SqlParameter("@UpdatedTime",objEventParticipant.UpdatedTime),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[11].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventParticipantsUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[11].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertEventOrderDetail(TCAssociationTool.Entities.EventOrderDetails objEventOrderDetail)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventOrderId",objEventOrderDetail.EventOrderId),
                    new SqlParameter("@EventUserInfoId",objEventOrderDetail.EventUserInfoId),
                    new SqlParameter("@EventId",objEventOrderDetail.EventId ),
                    new SqlParameter("@IsApproved",(objEventOrderDetail.IsApproved==false?(object)DBNull.Value:objEventOrderDetail.IsApproved)),
                    new SqlParameter("@ApprovedDate",(objEventOrderDetail.ApprovedDate==DateTime.MinValue?(object)DBNull.Value:objEventOrderDetail.ApprovedDate)),
                    new SqlParameter("@PaymentStatusId",(objEventOrderDetail.PaymentStatusId==0?(object)DBNull.Value:objEventOrderDetail.PaymentStatusId)),
                    new SqlParameter("@PaymentDate",(objEventOrderDetail.PaymentDate==DateTime.MinValue?(object)DBNull.Value:objEventOrderDetail.PaymentDate)),
                    new SqlParameter("@PaymentMethodId",(objEventOrderDetail.PaymentMethodId==0?(object)DBNull.Value:objEventOrderDetail.PaymentMethodId)),
                    new SqlParameter("@TransactionId",(objEventOrderDetail.TransactionId==null?(object)DBNull.Value:objEventOrderDetail.TransactionId)),
                    new SqlParameter("@PaymentMethod",(objEventOrderDetail.PaymentMethod==null?(object)DBNull.Value:objEventOrderDetail.PaymentMethod)),
                    new SqlParameter("@PaymentStatus",(objEventOrderDetail.PaymentStatus==null?(object)DBNull.Value:objEventOrderDetail.PaymentStatus)),
                    new SqlParameter("@AmountPaid",(objEventOrderDetail.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objEventOrderDetail.AmountPaid)),
                    new SqlParameter("@ChildCount",(objEventOrderDetail.ChildCount==0?(object)DBNull.Value:objEventOrderDetail.ChildCount)),
                    new SqlParameter("@AdultCount",(objEventOrderDetail.AdultCount==0?(object)DBNull.Value:objEventOrderDetail.AdultCount)),
                    new SqlParameter("@CoupleCount",(objEventOrderDetail.CoupleCount==0?(object)DBNull.Value:objEventOrderDetail.CoupleCount)),
                    new SqlParameter("@FamilyCount",(objEventOrderDetail.FamilyCount==0?(object)DBNull.Value:objEventOrderDetail.FamilyCount)),
                    new SqlParameter("@VIPCount",(objEventOrderDetail.VIPCount==0?(object)DBNull.Value:objEventOrderDetail.VIPCount)),
                     new SqlParameter("@VIPSingleAdultCount",(objEventOrderDetail.VIPSingleAdultCount==Decimal.MinValue?(object)DBNull.Value:objEventOrderDetail.VIPSingleAdultCount)),
                    new SqlParameter("@VIPCoupleCount",(objEventOrderDetail.VIPCoupleCount==Decimal.MinValue?(object)DBNull.Value:objEventOrderDetail.VIPCoupleCount)),
                    new SqlParameter("@VIPChildCount",(objEventOrderDetail.VIPChildCount==Decimal.MinValue?(object)DBNull.Value:objEventOrderDetail.VIPChildCount)),
                    new SqlParameter("@IsAttended",(objEventOrderDetail.IsAttended==false?(object)DBNull.Value:objEventOrderDetail.IsAttended)),
                    new SqlParameter("@InsertedBy",objEventOrderDetail.InsertedBy),
                    new SqlParameter("@InsertedDate",DateTime.UtcNow),
                    new SqlParameter("@UpdatedBy",objEventOrderDetail.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[25].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventOrderDetailsInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[25].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateEventOrderDetail(TCAssociationTool.Entities.EventOrderDetails objEventOrderDetail)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventOrderId",objEventOrderDetail.EventOrderId),
                    new SqlParameter("@EventUserInfoId",objEventOrderDetail.EventUserInfoId),
                    new SqlParameter("@EventId",objEventOrderDetail.EventId ),
                    new SqlParameter("@IsApproved",(objEventOrderDetail.IsApproved==false?(object)DBNull.Value:objEventOrderDetail.IsApproved)),
                    new SqlParameter("@ApprovedDate",(objEventOrderDetail.ApprovedDate==DateTime.MinValue?(object)DBNull.Value:objEventOrderDetail.ApprovedDate)),
                    new SqlParameter("@PaymentStatusId",(objEventOrderDetail.PaymentStatusId==0?(object)DBNull.Value:objEventOrderDetail.PaymentStatusId)),
                    new SqlParameter("@PaymentDate",(objEventOrderDetail.PaymentDate==DateTime.MinValue?(object)DBNull.Value:objEventOrderDetail.PaymentDate)),
                    new SqlParameter("@PaymentMethodId",(objEventOrderDetail.PaymentMethodId==0?(object)DBNull.Value:objEventOrderDetail.PaymentMethodId)),
                    new SqlParameter("@TransactionId",(objEventOrderDetail.TransactionId==null?(object)DBNull.Value:objEventOrderDetail.TransactionId)),
                    new SqlParameter("@AmountPaid",(objEventOrderDetail.AmountPaid==Decimal.MinValue?(object)DBNull.Value:objEventOrderDetail.AmountPaid)),
                    new SqlParameter("@IsAttended",(objEventOrderDetail.IsAttended==false?(object)DBNull.Value:objEventOrderDetail.IsAttended)),
                    new SqlParameter("@UpdatedBy",objEventOrderDetail.UpdatedBy),
                    new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                    new SqlParameter("@QStatus",0)
                    };


                _sqlP[13].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventOrderDetailsUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[13].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetEventsListall(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventGetListAll", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetEventsList(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventGetList", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetEnquiryEventsListall(ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EnquiryEventGetListAll", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 InsertRegistrationTypes(TCAssociationTool.Entities.EventRegistrationTypes objRegistrationTypes)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                    new SqlParameter("@EventRegistrationTypeId",(objRegistrationTypes.EventRegistrationTypeId!= 0?(object)objRegistrationTypes.EventRegistrationTypeId:DBNull.Value.ToString())),
                    new SqlParameter("@EventId",objRegistrationTypes.EventId),
                    new SqlParameter("@RegistrationTitle",objRegistrationTypes.RegistrationTitle),
                    new SqlParameter("@MemberAmount",objRegistrationTypes.MemberAmount),
                    new SqlParameter("@NonMemberAmount",objRegistrationTypes.NonMemberAmount),
                    new SqlParameter("@RegCount",(objRegistrationTypes.RegCount!= 0?(object)objRegistrationTypes.RegCount:DBNull.Value.ToString())),
                     new SqlParameter("@OrderNo",(objRegistrationTypes.OrderNo==0?(object)DBNull.Value:objRegistrationTypes.OrderNo)),
                     new SqlParameter("@IsActive",objRegistrationTypes.IsActive),
                     new SqlParameter("@UpdatedBy",objRegistrationTypes.UpdatedBy),
                     new SqlParameter("@UpdatedTime",DateTime.UtcNow),
                     new SqlParameter("@QStatus",0)
                    };
                _sqlP[10].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventRegistrationTypesInsert", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[10].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion

        #region EventUserInfo

        public Int64 DeleteEventUserInfoById(Int64 EventUserInfoId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventUserInfoId",EventUserInfoId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventUserInfoDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataSet GetEventUserInfoFullDetailsById(Int64 EventUserInfoId, Int64 EventId, ref int Status)
        {
            DataSet ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@EventUserInfoId",EventUserInfoId),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@EventId",EventId)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataSet("EventUserInfoFullDetailsGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable GetEventUserInfoById(Int64 EventUserInfoId, ref int Status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventUserInfoId",EventUserInfoId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventUserInfoGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetEventUserInfoListByVariable(Int64 EventId, string EventCategory, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@EventCategory",EventCategory),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventUserInfoGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable RegistrationGetEventUserInfoListByVariable(Int64 EventId, string EventCategory, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@EventCategory",EventCategory),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[6].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("RegistrationEventUserInfoGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[6].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable ExportToEventUserInfoList(Int64 EventId, string Search, string Sort)
        {
            DataTable dt = null;
            Int32 Total = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@Total",0)
                };
                _sqlP[3].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("ExporttoEventUserInfo", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[3].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdateEventUserInfoStatus(Int64 EventUserInfoId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventUserInfoId",EventUserInfoId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventUserInfoUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }
        
        #endregion

        #region  EventRegistration Types

        public DataTable GetEventRegistrationTypesList(Int64 EventId, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {    
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Qstatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventRegistrationTypesGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 DeleteEventRegistrationTypes(Int64 EventRegistrationTypeId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventRegistrationTypeId",EventRegistrationTypeId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventRegistrationTypesDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateEventRegistrationTypesStatus(Int64 EventRegistrationTypeId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventRegistrationTypeId",EventRegistrationTypeId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventRegistrationTypesUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 UpdateEventRegistrationTypesDisplayOrder(int OrderNo, Int64 EventRegistrationTypeId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@EventRegistrationTypeId",EventRegistrationTypeId),
                    new SqlParameter("@OrderNo",OrderNo),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventRegistrationTypesUpdateDisplayOrder", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion

        #region

        public DataSet FEGetEventById(Int64 EventId, string EventName, ref int Status)
        {
            DataSet dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@EventName",EventName),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataSet("FEEventsGetById", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        #endregion

        #region EventTicketMaster

        public DataTable EventTicketMasterGetListByVariable(Int64 EventId, string Mobile, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Mobile",Mobile)
                };
                _sqlP[4].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("EventTicketMasterGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[4].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 EventTicketMasterVisitCountUpdate(int VisitCount, Int64 EventRegCountId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@EventRegCountId",EventRegCountId),
                    new SqlParameter("@VisitCount",VisitCount),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("EventTicketMasterVisitCountUpdate", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[2].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetEventUserInfoFullDetailsByMobile(Int64 EventUserInfoId, Int64 EventId, string Mobile, string Search, ref int Status)
        {
            DataTable ds = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@EventUserInfoId",EventUserInfoId),
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@Mobile",Mobile),
                    new SqlParameter("@EventId",EventId),
                    new SqlParameter("@Search",Search)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                ds = _dbAccess.GetDataTable("EventUserInfoFullDetailsGetByMobile", ref _sqlP);
                Status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion
    }
}

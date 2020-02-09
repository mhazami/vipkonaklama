using Radyn.FileManager.BO;
using Radyn.Framework;
using Radyn.Framework.DbHelper;
using Radyn.Reservation.BO;
using Radyn.Reservation.DataStructure;
using Radyn.Reservation.Facade.Interface;
using System;
using System.Data;

namespace Radyn.Reservation.Facade
{
    internal sealed class RoomTypeFacade : ReservationBaseFacade<RoomType>, IRoomTypeFacade
    {
        internal RoomTypeFacade() { }

        internal RoomTypeFacade(IConnectionHandler connectionHandler)
            : base(connectionHandler) { }

        public override bool Insert(RoomType obj)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                if (obj.Picture != null)
                {
                    if (!new FileBO().Insert(ConnectionHandler, obj.Picture))
                    {
                        throw new Exception("");
                    }
                    obj.PicId = obj.Picture.Id;
                }
                if (!new RoomTypeBO().Insert(ConnectionHandler, obj))
                {
                    throw new Exception("");
                }

                ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Update(RoomType obj)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManager.DataStructure.File file = new FileBO().Get(ConnectionHandler, obj.PicId);
                if (obj.Picture != null)
                {
                    if (!new FileBO().Insert(ConnectionHandler, obj.Picture))
                    {
                        throw new Exception("");
                    }
                    obj.PicId = obj.Picture.Id;
                }
                if (!new RoomTypeBO().Update(ConnectionHandler, obj))
                {
                    throw new Exception("");
                }
                if (file != null)
                {
                    if (!new FileBO().Delete(ConnectionHandler, file))
                    {
                        throw new Exception("");
                    }
                }

                ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }

        public override bool Delete(RoomType obj)
        {
            try
            {
                ConnectionHandler.StartTransaction(IsolationLevel.ReadUncommitted);
                FileManager.DataStructure.File file = new FileBO().Get(ConnectionHandler, obj.PicId);
                if (!new RoomTypeBO().Delete(ConnectionHandler, obj))
                {
                    throw new Exception("");
                }
                if (file != null)
                {
                    if (!new FileBO().Delete(ConnectionHandler, file))
                    {
                        throw new Exception("");
                    }

                }

                ConnectionHandler.CommitTransaction();
                return true;
            }
            catch (KnownException ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                ConnectionHandler.RollBack();
                throw new KnownException(ex.Message, ex);
            }
        }
    }
}
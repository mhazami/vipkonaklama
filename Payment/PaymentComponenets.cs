using Radyn.Framework.DbHelper;
using Radyn.Payment.Facade;
using Radyn.Payment.Facade.Interface;

namespace Radyn.Payment
{
   public class PaymentComponenets
    {
       internal PaymentComponenets()
       {
           
       }

       private static PaymentComponenets _instance;
       public static PaymentComponenets Instance
       {
           get { return _instance ?? (_instance = new PaymentComponenets()); }
       }
       public IWebDesignDiscountTypeFacade WebDesignDiscountTypeFacade
        {
           get { return new WebDesignDiscountTypeFacade(); }
       }
        public IWebDesignAccountFacade WebDesignAccountFacade
        {
           get { return new WebDesignAccountFacade(); }
       }
        public IAccountFacade AccountFacade
       {
           get {return new AccountFacade();}
       }
       public IAccountFacade AccountTransactionalFacade(IConnectionHandler connectionHandler)
       {
           return new AccountFacade(connectionHandler);
       }
       public ITransactionFacade TransactionFacade
       {
           get {return new TransactionFacade();}
       }
       public IDiscountTypeFacade DiscountTypeFacade
       {
           get { return new DiscountTypeFacade(); }
       }
       public IDiscountTypeFacade DiscountTypeTransactionalFacade(IConnectionHandler connectionHandler)
       {
          return new DiscountTypeFacade(connectionHandler); 
       }
       public ITempFacade TempFacade
       {
           get { return new TempFacade(); }
       }
       public ITransactionDiscountFacade TransactionDiscountFacade
       {
           get { return new TransactionDiscountFacade(); }
       }
       public ITempDiscountFacade TempDiscountFacade
       {
           get { return new TempDiscountFacade(); }
       }
       public IDiscountTypeAutoCodeFacade DiscountTypeAutoCodeFacade
       {
           get { return new DiscountTypeAutoCodeFacade(); }
       }
       public IDiscountTypeSectionFacade DiscountTypeSectionFacade
       {
           get { return new DiscountTypeSectionFacade(); }
       }
       public IDiscountTypeSectionFacade DiscountTypeSectionTransactionalFacade(IConnectionHandler connectionHandler)
       {
           return new DiscountTypeSectionFacade(connectionHandler); 
       }
       public ITransactionFacade TransactionTransactionalFacade(IConnectionHandler connectionHandler)
       {
           return new TransactionFacade(connectionHandler); 
       }
       public ITransactionDiscountFacade TransactionDiscountFacadeTransactional(IConnectionHandler connectionHandler)
       {
           return new TransactionDiscountFacade(connectionHandler); 
       }
       public ITempFacade TempTransactionalFacade(IConnectionHandler connectionHandler)
       {
           return new TempFacade(connectionHandler);
       }
      

    }
}

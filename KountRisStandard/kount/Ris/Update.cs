//-----------------------------------------------------------------------
// <copyright file="Update.cs" company="Keynetics Inc">
//     Copyright Keynetics. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Kount.Ris
{
    using System;

    /// <summary>
    /// Update class. A bunch of setters for sending transaction update <br/>
    /// data to a Kount RIS server.
    /// <b>Author:</b> Kount <a>custserv@kount.com</a>;<br/>
    /// <b>Version:</b> 7.0.0. <br/>
    /// <b>Copyright:</b> 2020 Kount Inc <br/>
    /// </summary>
    public class Update : Kount.Ris.Request
    {
        /// <summary>
        /// Refund chargeback type refund
        /// </summary>
        private const char RfcbR = 'R';

        /// <summary>
        /// Refund chargeback type chargeback
        /// </summary>
        private const char RfcbC = 'C';

        /// <summary>
        /// The Logger to use.
        /// </summary>
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Update));

        /// <summary>
        /// Constructor. Sets the mode to 'U' by default.
        /// Use setMode(char) to change it.
        /// </summary>
        public Update() : base()
        {
            this.SetMode(Enums.UpdateTypes.ModeU);
        }

        /// <summary>
        /// Constructor. Sets the mode to 'U' by default.
        /// Use setMode(char) to change it.
        /// </summary>
        /// <param name="checkConfiguration">If is true: will check config file if 
        /// `Ris.Url`, 
        /// `Ris.MerchantId`, 
        /// `Ris.Config.Key` and `Ris.Connect.Timeout` are set.</param>
        public Update(bool checkConfiguration) : base(checkConfiguration)
        {
            this.SetMode(Enums.UpdateTypes.ModeU);
        }

        /// <summary>
        /// Constructor. Sets the mode to 'U' by default.
        /// Use setMode(char) to change it.
        /// </summary>
        /// <param name="checkConfiguration">If is true: will check config file if 
        /// `Ris.Url`, 
        /// `Ris.MerchantId`, 
        /// `Ris.Config.Key` and `Ris.Connect.Timeout` are set.</param>
        /// <param name="configuration">Configuration class with raw values</param>
        public Update(bool checkConfiguration, Configuration configuration) : base(checkConfiguration, configuration)
        {
            this.SetMode(Enums.UpdateTypes.ModeU);
        }

        /// <summary>
        /// Set the mode of the update.
        /// </summary>
        /// <param name="mode">Set U or X</param>
        /// <exception cref="Kount.Ris.IllegalArgumentException">Thrown if
        /// parameter is an invalid mode.</exception>
        protected override void SetMode(char mode)
        {
            if (((char)Enums.UpdateTypes.ModeU != mode)
                && ((char)Enums.UpdateTypes.ModeX != mode))
            {
                throw new Kount.Ris.IllegalArgumentException(
                    "Invalid RIS update mode " + mode);
            }

            this.Data["MODE"] = mode;
        }

        /// <summary>
        /// Set the original associated transaction id generated by Kount
        /// </summary>
        /// <param name="transactionId">Transaction id.</param>
        public void SetTransactionId(string transactionId)
        {
            this.Data["TRAN"] = this.SafeGet(transactionId);
        }

        /// <summary>
        /// Set if this transaction ended up being a refund or chargback.
        /// </summary>
        /// <param name="rfcb">Set R or C.</param>
        public void SetRefundChargeback(char rfcb)
        {
            if ((RfcbR != rfcb) && (RfcbC != rfcb))
            {
                throw new Kount.Ris.IllegalArgumentException(
                    "Invalid RIS refund charge back value " + rfcb);
            }

            this.Data["RFCB"] = rfcb;
        }

        /// <summary>
        /// Set the paypal Id
        /// </summary>
        /// <param name="paypalId">Set paypal Id</param>
        [Obsolete("version 4.1.0 - 2010. Use Kount.Ris.Update.SetPayPalPayment() instead")]
        public void SetPayPalId(string paypalId)
        {
            string message = "The method " +
                "Kount.Ris.Update.SetPaypalId() is obsolete. " +
                "Use Kount.Ris.Update.SetPaypalPayment(bool) instead.";
            logger.Info(message);
            this.SetPayment(Enums.PaymentTypes.Paypal, paypalId);
        }
    }
}
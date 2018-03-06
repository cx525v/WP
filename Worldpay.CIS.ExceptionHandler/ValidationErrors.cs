using System;
using System.Collections.Generic;
using System.Text;

namespace Worldpay.CIS.Utilities
{

    public class ValidationError
    {

        /// <summary>
        /// The error message for this validation error.
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// The name of the field that this error relates to.
        /// </summary>
        public string ControlId { get; set; } = "";

        /// <summary>
        /// An ID set for the Error. This ID can be used as a correlation between bus object and UI code.
        /// </summary>
        public string Id { get; set; } = "";

        public ValidationError() : base()
        {
        }

        public ValidationError(string message)
        {
            Message = message;
        }

        public ValidationError(string message, string fieldName)
        {
            Message = message;
            ControlId = fieldName;
        }

        public ValidationError(string message, string fieldName, string id)
        {
            Message = message;
            ControlId = fieldName;
            Id = id;
        }

    }
}



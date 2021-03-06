﻿namespace Protractor_Core
{
    /// <summary>
    ///     Wrapper around the NgBy.Model() static method to provide typed By selector for FindsByAttribute usage.
    /// </summary>
    public class NgByModel : JavaScriptBy
    {
        /// <summary>
        ///     Creates a new instance of <see cref="NgByModel" />.
        /// </summary>
        /// <param name="model">The model name.</param>
        public NgByModel(string model)
            : base(ClientSideScripts.FindModel, model)
        {
            Description = "NgBy.Model: " + model;
        }
    }
}
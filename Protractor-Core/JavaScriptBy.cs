using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Protractor_Core
{
    /// <summary>
    ///     Provides a mechanism by which to find elements within a document using JavaScript.
    /// </summary>
    public class JavaScriptBy : By
    {
        private readonly object[] args;
        private readonly string script;

        /// <summary>
        ///     Creates a new instance of <see cref="JavaScriptBy" />.
        /// </summary>
        /// <param name="script">
        ///     The JavaScript code to execute.
        /// </param>
        /// <param name="args">
        ///     The arguments to the script.
        /// </param>
        public JavaScriptBy(string script, params object[] args)
        {
            this.script = script;
            this.args = args;
            Description = "Protractor.JavaScriptBy";
        }

        /// <summary>
        ///     Gets or sets any additional arguments to the script.
        /// </summary>
        public object[] AdditionalScriptArguments { get; set; }

        /// <summary>
        ///     Finds the first element matching the criteria.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="ISearchContext" /> object to use to search for the elements.
        /// </param>
        /// <returns>
        ///     The first matching <see cref="IWebElement" /> on the current context.
        /// </returns>
        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);
            if (elements.Count == 0)
                throw new NoSuchElementException($"Unable to locate element: {{ {Description} }}.");
            return elements[0];
        }

        /// <summary>
        ///     Finds all elements matching the criteria.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="ISearchContext" /> object to use to search for the elements.
        /// </param>
        /// <returns>
        ///     A collection of all <see cref="OpenQA.Selenium.IWebElement" /> matching the current criteria,
        ///     or an empty list if nothing matches.
        /// </returns>
        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            // Create script arguments
            var scriptArgs = args;
            if (AdditionalScriptArguments != null && AdditionalScriptArguments.Length > 0)
            {
                // Add additionnal script arguments
                scriptArgs = new object[args.Length + AdditionalScriptArguments.Length];
                args.CopyTo(scriptArgs, 0);
                AdditionalScriptArguments.CopyTo(scriptArgs, args.Length);
            }

            // Get JS executor
            var jsExecutor = context as IJavaScriptExecutor;
            if (jsExecutor == null)
                if (context is IWrapsDriver wrapsDriver)
                    jsExecutor = wrapsDriver.WrappedDriver as IJavaScriptExecutor;
            if (jsExecutor == null)
                throw new NotSupportedException("Could not get an IJavaScriptExecutor instance from the context.");

            if (!(jsExecutor.ExecuteScript(script, scriptArgs) is ReadOnlyCollection<IWebElement> elements))
                elements = new ReadOnlyCollection<IWebElement>(new List<IWebElement>(0));
            return elements;
        }
    }
}
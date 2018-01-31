using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Protractor
{
    /// <summary>
    ///     Provides a mechanism to get elements off the page for test.
    /// </summary>
    public class NgWebElement : IWebElement, IWrapsElement
    {
        private readonly NgWebDriver ngDriver;

        /// <summary>
        ///     Creates a new instance of <see cref="NgWebElement" /> by wrapping a <see cref="IWebElement" /> instance.
        /// </summary>
        /// <param name="ngDriver">The <see cref="NgWebDriver" /> in use.</param>
        /// <param name="element">The existing <see cref="IWebElement" /> instance.</param>
        public NgWebElement(NgWebDriver ngDriver, IWebElement element)
        {
            this.ngDriver = ngDriver;
            WrappedElement = element;
        }

        /// <summary>
        /// Gets the <see cref="NgWebDriver"/> instance used to initialize the element.
        /// </summary>
        public NgWebDriver NgDriver
        {
            get { return this.ngDriver; }
        }

        #region IWrapsElement Members

        /// <summary>
        ///     Gets the wrapped <see cref="IWebElement" /> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        #endregion

        /// <summary>
        ///     Evaluates the expression as if it were on the scope of the current element.
        /// </summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>The expression evaluated by Angular.</returns>
        public object Evaluate(string expression)
        {
            ngDriver.WaitForAngular();
            return ngDriver.ExecuteScript(ClientSideScripts.Evaluate, WrappedElement, expression);
        }

        #region IWebElement Members

        /// <summary>
        ///     Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Displayed
        {
            get
            {
                ngDriver.WaitForAngular();
                return WrappedElement.Displayed;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether or not this element is enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                ngDriver.WaitForAngular();
                return WrappedElement.Enabled;
            }
        }

        /// <summary>
        ///     Gets a <see cref="Point" /> object containing the coordinates of the upper-left corner
        ///     of this element relative to the upper-left corner of the page.
        /// </summary>
        public Point Location
        {
            get
            {
                ngDriver.WaitForAngular();
                return WrappedElement.Location;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether or not this element is selected.
        /// </summary>
        public bool Selected
        {
            get
            {
                ngDriver.WaitForAngular();
                return WrappedElement.Selected;
            }
        }

        /// <summary>
        ///     Gets a <see cref="Size" /> object containing the height and width of this element.
        /// </summary>
        public Size Size
        {
            get
            {
                ngDriver.WaitForAngular();
                return WrappedElement.Size;
            }
        }

        /// <summary>
        ///     Gets the tag name of this element.
        /// </summary>
        public string TagName
        {
            get
            {
                ngDriver.WaitForAngular();
                return WrappedElement.TagName;
            }
        }

        /// <summary>
        ///     Gets the innerText of this element, without any leading or trailing whitespace,
        ///     and with other whitespace collapsed.
        /// </summary>
        public string Text
        {
            get
            {
                ngDriver.WaitForAngular();
                return WrappedElement.Text;
            }
        }

        /// <summary>
        ///     Clears the content of this element.
        /// </summary>
        public void Clear()
        {
            ngDriver.WaitForAngular();
            WrappedElement.Clear();
        }

        /// <summary>
        ///     Clicks this element.
        /// </summary>
        public void Click()
        {
            ngDriver.WaitForAngular();
            WrappedElement.Click();
        }

        /// <summary>
        ///     Gets the value of the specified attribute for this element.
        /// </summary>
        public string GetAttribute(string attributeName)
        {
            ngDriver.WaitForAngular();
            return WrappedElement.GetAttribute(attributeName);
        }

        /// <summary>
        ///     Gets the value of the specified property for this element.
        /// </summary>
        public string GetProperty(string propertyName)
        {
            ngDriver.WaitForAngular();
            return WrappedElement.GetProperty(propertyName);
        }

        /// <summary>
        ///     Gets the value of a CSS property of this element.
        /// </summary>
        public string GetCssValue(string propertyName)
        {
            ngDriver.WaitForAngular();
            return WrappedElement.GetCssValue(propertyName);
        }

        /// <summary>
        ///     Simulates typing text into the element.
        /// </summary>
        public void SendKeys(string text)
        {
            ngDriver.WaitForAngular();
            WrappedElement.SendKeys(text);
        }

        /// <summary>
        ///     Submits this element to the web server.
        /// </summary>
        public void Submit()
        {
            ngDriver.WaitForAngular();
            WrappedElement.Submit();
        }

        /// <summary>
        ///     Finds the first <see cref="NgWebElement" /> using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>The first matching <see cref="NgWebElement" /> on the current context.</returns>
        /// <exception cref="NoSuchElementException">If no element matches the criteria.</exception>
        public NgWebElement FindElement(By by)
        {
            if (by is JavaScriptBy scriptBy)
                scriptBy.AdditionalScriptArguments = new object[] {ngDriver.RootElement, WrappedElement};
            ngDriver.WaitForAngular();
            return new NgWebElement(ngDriver, WrappedElement.FindElement(by));
        }

        /// <summary>
        ///     Finds all <see cref="NgWebElement" />s within the current context
        ///     using the given mechanism.
        /// </summary>
        /// <param name="by">The locating mechanism to use.</param>
        /// <returns>
        ///     A <see cref="ReadOnlyCollection{T}" /> of all <see cref="NgWebElement" />s
        ///     matching the current criteria, or an empty list if nothing matches.
        /// </returns>
        public ReadOnlyCollection<NgWebElement> FindElements(By by)
        {
            if (by is JavaScriptBy scriptBy)
                scriptBy.AdditionalScriptArguments = new object[] {ngDriver.RootElement, WrappedElement};
            ngDriver.WaitForAngular();
            return new ReadOnlyCollection<NgWebElement>(WrappedElement.FindElements(by)
                .Select(e => new NgWebElement(ngDriver, e)).ToList());
        }

        IWebElement ISearchContext.FindElement(By by)
        {
            return FindElement(by);
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            if (by is JavaScriptBy scriptBy)
                scriptBy.AdditionalScriptArguments = new object[] {ngDriver.RootElement, WrappedElement};
            ngDriver.WaitForAngular();
            return new ReadOnlyCollection<IWebElement>(WrappedElement.FindElements(by)
                .Select(e => (IWebElement) new NgWebElement(ngDriver, e)).ToList());
        }

        #endregion
    }
}
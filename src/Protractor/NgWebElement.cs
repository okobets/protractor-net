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
        /// <summary>
        ///     Creates a new instance of <see cref="NgWebElement" /> by wrapping a <see cref="IWebElement" /> instance.
        /// </summary>
        /// <param name="ngDriver">The <see cref="NgWebDriver" /> in use.</param>
        /// <param name="element">The existing <see cref="IWebElement" /> instance.</param>
        public NgWebElement(NgWebDriver ngDriver, IWebElement element)
        {
            NgDriver = ngDriver;
            WrappedElement = element;
        }

        /// <summary>
        /// Gets the <see cref="NgWebDriver"/> instance used to initialize the element.
        /// </summary>
        public NgWebDriver NgDriver { get; }

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
            NgDriver.WaitForAngular();
            return NgDriver.ExecuteScript(ClientSideScripts.Evaluate, WrappedElement, expression);
        }

        #region IWebElement Members

        /// <summary>
        ///     Gets a value indicating whether or not this element is displayed.
        /// </summary>
        public bool Displayed
        {
            get
            {
                NgDriver.WaitForAngular();
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
                NgDriver.WaitForAngular();
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
                NgDriver.WaitForAngular();
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
                NgDriver.WaitForAngular();
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
                NgDriver.WaitForAngular();
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
                NgDriver.WaitForAngular();
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
                NgDriver.WaitForAngular();
                return WrappedElement.Text;
            }
        }

        /// <summary>
        ///     Clears the content of this element.
        /// </summary>
        public void Clear()
        {
            NgDriver.WaitForAngular();
            WrappedElement.Clear();
        }

        /// <summary>
        ///     Clicks this element.
        /// </summary>
        public void Click()
        {
            NgDriver.WaitForAngular();
            WrappedElement.Click();
        }

        /// <summary>
        ///     Gets the value of the specified attribute for this element.
        /// </summary>
        public string GetAttribute(string attributeName)
        {
            NgDriver.WaitForAngular();
            return WrappedElement.GetAttribute(attributeName);
        }

        /// <summary>
        ///     Gets the value of the specified property for this element.
        /// </summary>
        public string GetProperty(string propertyName)
        {
            NgDriver.WaitForAngular();
            return WrappedElement.GetProperty(propertyName);
        }

        /// <summary>
        ///     Gets the value of a CSS property of this element.
        /// </summary>
        public string GetCssValue(string propertyName)
        {
            NgDriver.WaitForAngular();
            return WrappedElement.GetCssValue(propertyName);
        }

        /// <summary>
        ///     Simulates typing text into the element.
        /// </summary>
        public void SendKeys(string text)
        {
            NgDriver.WaitForAngular();
            WrappedElement.SendKeys(text);
        }

        /// <summary>
        ///     Submits this element to the web server.
        /// </summary>
        public void Submit()
        {
            NgDriver.WaitForAngular();
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
                scriptBy.AdditionalScriptArguments = new object[] {NgDriver.RootElement, WrappedElement};
            NgDriver.WaitForAngular();
            return new NgWebElement(NgDriver, WrappedElement.FindElement(by));
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
                scriptBy.AdditionalScriptArguments = new object[] {NgDriver.RootElement, WrappedElement};
            NgDriver.WaitForAngular();
            return new ReadOnlyCollection<NgWebElement>(WrappedElement.FindElements(by)
                .Select(e => new NgWebElement(NgDriver, e)).ToList());
        }

        IWebElement ISearchContext.FindElement(By by)
        {
            return FindElement(by);
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            if (by is JavaScriptBy scriptBy)
                scriptBy.AdditionalScriptArguments = new object[] {NgDriver.RootElement, WrappedElement};
            NgDriver.WaitForAngular();
            return new ReadOnlyCollection<IWebElement>(WrappedElement.FindElements(by)
                .Select(e => (IWebElement) new NgWebElement(NgDriver, e)).ToList());
        }

        #endregion
    }
}
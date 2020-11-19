using System;
using OpenQA.Selenium;

namespace Protractor_Core
{
    /// <summary>
    ///     Provides a mechanism for navigating against an AngularJS application.
    /// </summary>
    public class NgNavigation : INavigation
    {
        /// <summary>
        ///     Creates a new instance of <see cref="NgNavigation" /> by wrapping a <see cref="INavigation" /> instance.
        /// </summary>
        /// <param name="ngDriver">The <see cref="NgWebDriver" /> in use.</param>
        /// <param name="navigation">The existing <see cref="INavigation" /> instance.</param>
        public NgNavigation(NgWebDriver ngDriver, INavigation navigation)
        {
            NgDriver = ngDriver;
            WrappedNavigation = navigation;
        }

        /// <summary>
        /// Gets the <see cref="NgWebDriver"/> instance used to initialize the instance.
        /// </summary>
        public NgWebDriver NgDriver { get; }

        /// <summary>
        /// Gets the wrapped <see cref="INavigation"/> instance.
        /// </summary>
        public INavigation WrappedNavigation { get; }

        #region INavigation Members

        /// <summary>
        ///     Move back a single entry in the browser's history.
        /// </summary>
        public void Back()
        {
            NgDriver.WaitForAngular();
            WrappedNavigation.Back();
        }

        /// <summary>
        ///     Move a single "item" forward in the browser's history.
        /// </summary>
        public void Forward()
        {
            NgDriver.WaitForAngular();
            WrappedNavigation.Forward();
        }

        void INavigation.GoToUrl(Uri url)
        {
            GoToUrl(url, true);
        }

        /// <summary>
        ///     Load a new web page in the current browser window.
        /// </summary>
        /// <param name="url">The URL to load.</param>
        public void GoToUrl(Uri url)
        {
            GoToUrl(url, true);
        }

        /// <summary>
        ///     Load a new web page in the current browser window.
        /// </summary>
        /// <param name="url">The URL to load.</param>
        /// <param name="ensureAngularApp">Ensure the page is an Angular page by throwing an exception.</param>
        public void GoToUrl(Uri url, bool ensureAngularApp)
        {
            if (ensureAngularApp)
            {
                if (url == null)
                    throw new ArgumentNullException(nameof(url), "URL cannot be null.");
                NgDriver.Url = url.ToString();
            }
            else
            {
                WrappedNavigation.GoToUrl(url);
            }
        }

        void INavigation.GoToUrl(string url)
        {
            GoToUrl(url, true);
        }

        /// <summary>
        ///     Load a new web page in the current browser window.
        /// </summary>
        /// <param name="url">The URL to load. It is best to use a fully qualified URL</param>
        public void GoToUrl(string url)
        {
            GoToUrl(url, true);
        }

        /// <summary>
        ///     Load a new web page in the current browser window.
        /// </summary>
        /// <param name="url">The URL to load. It is best to use a fully qualified URL</param>
        /// <param name="ensureAngularApp">Ensure the page is an Angular page by throwing an exception.</param>
        public void GoToUrl(string url, bool ensureAngularApp)
        {
            if (ensureAngularApp)
                NgDriver.Url = url;
            else
                WrappedNavigation.GoToUrl(url);
        }

        /// <summary>
        ///     Refreshes the current page.
        /// </summary>
        public void Refresh()
        {
            if (NgDriver.IgnoreSynchronization)
            {
                WrappedNavigation.Refresh();
            }
            else
            {
                var url = NgDriver.ExecuteScript("return window.location.href;") as string;
                NgDriver.Url = url;
            }
        }

        #endregion
    }
}
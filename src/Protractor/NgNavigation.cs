﻿using System;
using OpenQA.Selenium;

namespace Protractor
{
    /// <summary>
    ///     Provides a mechanism for navigating against an AngularJS application.
    /// </summary>
    public class NgNavigation : INavigation
    {
        private readonly NgWebDriver ngDriver;

        /// <summary>
        ///     Creates a new instance of <see cref="NgNavigation" /> by wrapping a <see cref="INavigation" /> instance.
        /// </summary>
        /// <param name="ngDriver">The <see cref="NgWebDriver" /> in use.</param>
        /// <param name="navigation">The existing <see cref="INavigation" /> instance.</param>
        public NgNavigation(NgWebDriver ngDriver, INavigation navigation)
        {
            this.ngDriver = ngDriver;
            WrappedNavigation = navigation;
        }

        /// <summary>
        ///     Gets the wrapped <see cref="INavigation" /> instance.
        /// </summary>
        public INavigation WrappedNavigation { get; }

        /// <summary>
        ///     Browse to another page using in-page navigation.
        /// </summary>
        /// <param name="path">The path to load using the same syntax as '$location.url()'.</param>
        public void GoToLocation(string path)
        {
            ngDriver.Location = path;
        }

        #region INavigation Members

        /// <summary>
        ///     Move back a single entry in the browser's history.
        /// </summary>
        public void Back()
        {
            ngDriver.WaitForAngular();
            WrappedNavigation.Back();
        }

        /// <summary>
        ///     Move a single "item" forward in the browser's history.
        /// </summary>
        public void Forward()
        {
            ngDriver.WaitForAngular();
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
                    throw new ArgumentNullException("url", "URL cannot be null.");
                ngDriver.Url = url.ToString();
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
                ngDriver.Url = url;
            else
                WrappedNavigation.GoToUrl(url);
        }

        /// <summary>
        ///     Refreshes the current page.
        /// </summary>
        public void Refresh()
        {
            if (ngDriver.IgnoreSynchronization)
            {
                WrappedNavigation.Refresh();
            }
            else
            {
                var url = ngDriver.ExecuteScript("return window.location.href;") as string;
                ngDriver.Url = url;
            }
        }

        #endregion
    }
}
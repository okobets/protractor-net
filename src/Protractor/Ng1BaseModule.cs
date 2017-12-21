namespace Protractor
{
    /// <summary>
    ///     Module automatically installed by Protractor when a page is loaded with Angular 1.
    /// </summary>
    internal class Ng1BaseModule : NgModule
    {
        private const string ModuleName = "protractorBaseModule_";

        private const string ModuleScript = "angular.module('" + ModuleName + @"', [])
.config([
  '$compileProvider',
  function($compileProvider) {
    if ($compileProvider.debugInfoEnabled) {
      $compileProvider.debugInfoEnabled(true);
    }
  }
]);
  if (trackOutstandingTimeouts) {
    ngMod.config([
      '$provide',
      function ($provide) {
        $provide.decorator('$timeout', [
          '$delegate',
          function ($delegate) {
            var $timeout = $delegate;

            var taskId = 0;

            if (!window['NG_PENDING_TIMEOUTS']) {
              window['NG_PENDING_TIMEOUTS'] = {};
            }

            var extendedTimeout= function() {
              var args = Array.prototype.slice.call(arguments);
              if (typeof(args[0]) !== 'function') {
                return $timeout.apply(null, args);
              }

              taskId++;
              var fn = args[0];
              window['NG_PENDING_TIMEOUTS'][taskId] =
                  fn.toString();
              var wrappedFn = (function(taskId_) {
                return function() {
                  delete window['NG_PENDING_TIMEOUTS'][taskId_];
                  return fn.apply(null, arguments);
                };
              })(taskId);
              args[0] = wrappedFn;

              var promise = $timeout.apply(null, args);
              promise.ptorTaskId_ = taskId;
              return promise;
            };

            extendedTimeout.cancel = function() {
              var taskId_ = arguments[0] && arguments[0].ptorTaskId_;
              if (taskId_) {
                delete window['NG_PENDING_TIMEOUTS'][taskId_];
              }
              return $timeout.cancel.apply($timeout, arguments);
            };

            return extendedTimeout;
          }
        ]);
      }
    ]);
}";

        public Ng1BaseModule()
            : base(ModuleName, ModuleScript)
        {
        }
    }
}
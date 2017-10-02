using Ninject;

namespace RM.Terminal
{
    public static class IoC
    {
        #region Public Properties

        public static IKernel Kernel { get; private set; } = new StandardKernel();
        public static ApplicationViewModel Application => IoC.Get<ApplicationViewModel>();

        #endregion Public Properties

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// </summary>
        public static void Setup()
        {
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModels()
        {
            // Bind to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        #endregion Construction

        /// <summary>
        /// Gets a service from the IoC of the specified type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

    }
}

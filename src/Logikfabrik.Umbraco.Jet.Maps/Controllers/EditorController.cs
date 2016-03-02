// <copyright file="EditorController.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Umbraco.Jet.Maps.Controllers
{
    using System.Web.Mvc;
    using global::Umbraco.Web.Mvc;

    /// <summary>
    /// The <see cref="EditorController" /> class.
    /// </summary>
    [PluginController("uJetMaps")]
    public class EditorController : SurfaceController
    {
        /// <summary>
        /// Handles the index action.
        /// </summary>
        /// <returns>A view.</returns>
        public ActionResult Index()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            return View();
        }
    }
}

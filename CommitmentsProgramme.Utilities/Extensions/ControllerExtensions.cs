using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Utilities.Extensions;


public static class ControllerExtensions
{
    public static async Task<string> RenderViewAsync<TModel>(
     this Controller controller,
     string viewName,
     TModel model,
     bool isPartial = true,
     ViewDataDictionary viewData = null)
    {
        controller.ViewData.Model = model;

        // 🔥 Inject extra ViewData
        if (viewData != null)
        {
            foreach (var item in viewData)
            {
                controller.ViewData[item.Key] = item.Value;
            }
        }

        using var writer = new StringWriter();

        var viewEngine = controller.HttpContext.RequestServices
            .GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

        var viewResult = viewEngine.FindView(
            controller.ControllerContext,
            viewName,
            !isPartial);

        if (!viewResult.Success)
        {
            throw new InvalidOperationException($"View {viewName} not found");
        }

        var viewContext = new ViewContext(
            controller.ControllerContext,
            viewResult.View,
            controller.ViewData,
            controller.TempData,
            writer,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);

        return writer.ToString();
    }   
}
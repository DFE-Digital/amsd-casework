﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ConcernsCaseWork.Extensions
{
	public static class PageModelExtension
	{
		/// <summary>
		/// Used to return an MVC view from a razor page
		/// </summary>
		/// <param name="pageModel"></param>
		/// <param name="viewName"></param>
		/// <param name="model"></param>
		/// <typeparam name="TModel"></typeparam>
		/// <returns></returns>
		public static ViewResult View<TModel>(this PageModel pageModel, string viewName, TModel model) {
			var viewDataDictionary = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary()) {
				Model = model
			};
			foreach (var kvp in pageModel.ViewData)
				viewDataDictionary.Add(kvp);

			return new ViewResult {
				ViewName = viewName,
				ViewData = viewDataDictionary,
				TempData = pageModel.TempData
			};
		}
	}
}
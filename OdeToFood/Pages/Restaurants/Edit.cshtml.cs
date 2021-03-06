﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Data;
using OdeToFood.Core;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = this.restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            Cuisines = this.htmlHelper.GetEnumSelectList<CuisineType>();

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = this.htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {

                this.restaurantData.Update(Restaurant);
            }   
            else
            {
                this.restaurantData.Add(Restaurant);
            }

            TempData["Message"] = "Restaurant saved";

            this.restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}

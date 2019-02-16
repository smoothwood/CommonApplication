using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Helpers;
using Core.Entities;
using Newtonsoft.Json.Linq;
using Core.DTOs;

namespace Web.Controllers
{
    /// <summary>
    /// Controller for article and category
    /// </summary>
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class ArticleCategoryController : Controller
    {
        private IArticleCategoryService _articleCategoryService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="articleCategoryService"></param>
        public ArticleCategoryController(IArticleCategoryService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }
        /// <summary>
        /// List all categories
        /// </summary>
        /// <returns>Category list</returns>
        [HttpGet]
        [Route("CategoryList")]
        public async Task<IReadOnlyList<Category>> ListCategories()
        {
            return await _articleCategoryService.ListCategories();
        }
        /// <summary>
        /// Get details of a single category
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>Category</returns>
        [HttpGet]
        [Route("Category")]
        public async Task<Category> GetCategory([FromQuery(Name = "categoryId")] int categoryId)
        {
            return await _articleCategoryService.GetCategory(categoryId);
        }
        /// <summary>
        /// List all subcategories based on parent category id
        /// </summary>
        /// <param name="parentCategoryId">Parent category id</param>
        /// <returns>Subcategory list</returns>
        [HttpGet]
        [Route("SubCategoryList")]
        public async Task<IReadOnlyList<Category>> ListSubCategories([FromQuery(Name = "parentCategoryId")] int parentCategoryId)
        {
            return await _articleCategoryService.ListSubCategories(parentCategoryId);
        }
        /// <summary>
        /// Add a new category
        /// Include categoryName and description in POST
        /// </summary>
        /// <returns>Json object with category id and category name</returns>
        [HttpPost]
        [Route("Category")]
        public async Task<IActionResult> AddCategory()
        {
            JObject jsonObj = Request.Body.GetJObject();
            string categoryName = jsonObj["categoryName"].ToString();
            string description = jsonObj["description"].ToString();
            Category category = new Category()
            {
                CategoryName = categoryName,
                Description = description
            };
            var result = await _articleCategoryService.AddCategory(category);
            if (result != null)
            {
                return Ok(new { status = "success", categoryId = result.CategoryId, categoryName= result.CategoryName});
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
        /// <summary>
        /// Update an existing category
        /// Include categoryId, categoryName and description in POST
        /// </summary>
        /// <returns>Json object with category id and category name</returns>
        [HttpPut]
        [Route("Category")]
        public async Task<IActionResult> UpdateCategory()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int categoryId = jsonObj.SelectToken("categoryId").Value<int>();
            string categoryName = jsonObj["categoryName"].ToString();
            string description = jsonObj["description"].ToString();
            Category category= new Category()
            {
                CategoryId= categoryId,
                CategoryName = categoryName,
                Description = description
            };
            var result = await _articleCategoryService.UpdateCategory(category);
            if (result != null)
            {
                return Ok(new { status = "success", categoryId = result.CategoryId, categoryName = result.CategoryName});
            }
            else
            {
                return Ok(new { status = "error" });
            }

        }
        /// <summary>
        /// Delete a existing cateogry
        /// Include categoryId in POST
        /// </summary>
        /// <returns>Json object with category id and category name</returns>
        [HttpDelete]
        [Route("Category")]
        public async Task<IActionResult> DeleteCategory()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int categoryId = jsonObj.SelectToken("categoryId").Value<int>();
            var result = await _articleCategoryService.DeleteCategory(categoryId);
            if (result != null)
            {
                return Ok(new { status = "success", categoryId = result.CategoryId, categoryName = result.CategoryName });
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
        /// <summary>
        /// List all articles
        /// Include skip, take in POST for server side pagination, categoryId is optional 
        /// </summary>
        /// <returns>Json object with article list and total record number</returns>
        [HttpPost]
        [Route("ArticleList")]
        public async Task<JsonResult> ListArticles()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int skip = jsonObj.SelectToken("skip").Value<int>();
            int take = jsonObj.SelectToken("take").Value<int>();
            int? categoryId = jsonObj["categoryId"] != null ? jsonObj.SelectToken("categoryId").Value<int>() : null as int?;
            IReadOnlyList<Article> articles = await _articleCategoryService.ListArticles(skip, take, categoryId);
            int totalRecord = await _articleCategoryService.CountArticle(categoryId);
            //Adding recordsTotal and recordsFiltered in order to be compatible with Jquery datatable
            return Json(new
            {
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = articles
            });

        }
        /// <summary>
        /// Get details of a single article
        /// </summary>
        /// <param name="articleId">Article id</param>
        /// <returns>ArticleDTO</returns>
        [HttpGet]
        [Route("Article")]
        public async Task<ArticleDTO> GetArticle([FromQuery(Name = "articleId")] int articleId)
        {
            Article article = await _articleCategoryService.GetArticle(articleId);
            return new ArticleDTO
            {
                ArticleId = article.ArticleId,
                Title = article.Title,
                Description = article.Description,
                ContentUrl = article.ContentUrl,
                ImageUrl = article.ImageUrl
            };
        }
        /// <summary>
        /// Add a new article
        /// Include title, decription, contentUrl, imageUrl and categoryId in POST
        /// </summary>
        /// <returns>Json object with article id and title</returns>
        [HttpPost]
        [Route("Article")]
        public async Task<IActionResult> AddArticle()
        {
            JObject jsonObj = Request.Body.GetJObject();
            Article article = new Article()
            {
                Title = jsonObj["title"].ToString(),
                Description = jsonObj["description"].ToString(),
                ContentUrl= jsonObj["contentUrl"].ToString(),
                ImageUrl= jsonObj["imageUrl"].ToString(),
                CategoryId=jsonObj["categoryId"].Value<int>()
            };
            var result = await _articleCategoryService.AddArticle(article);
            if (result != null)
            {
                return Ok(new { status = "success", articleId = result.ArticleId, title = result.Title });
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
        /// <summary>
        /// Update an existing article
        /// Include articleId, title, decription, contentUrl, imageUrl and categoryId in POST
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("Article")]
        public async Task<IActionResult> UpdateArticle()
        {
            JObject jsonObj = Request.Body.GetJObject();
            Article article = new Article()
            {
                ArticleId = jsonObj.SelectToken("articleId").Value<int>(),
                Title = jsonObj["title"].ToString(),
                Description= jsonObj["description"].ToString(),
                ContentUrl = jsonObj["contentUrl"].ToString(),
                ImageUrl= jsonObj["imageUrl"].ToString(),
                CategoryId = jsonObj["categoryId"].Value<int>()
            };


            var result = await _articleCategoryService.UpdateArticle(article);
            if (result != null)
            {
                return Ok(new { status = "success", articleId = result.ArticleId, title= result.Title});
            }
            else
            {
                return Ok(new { status = "error" });
            }
        }
        /// <summary>
        /// Delete an existing article
        /// Include articleId in POST
        /// </summary>
        /// <returns>Json object with article id and title</returns>
        [HttpDelete]
        [Route("Article")]
        public async Task<IActionResult> DeleteArticle()
        {
            JObject jsonObj = Request.Body.GetJObject();
            int articleId = jsonObj.SelectToken("articleId").Value<int>();
            var result = await _articleCategoryService.DeleteArticle(articleId);
            if (result != null)
            {
                return Ok(new { status = "success", articleId = result.ArticleId, title= result.Title});
            }
            else
            {
                return Ok(new { status = "error" });
            }

        }
    }
}
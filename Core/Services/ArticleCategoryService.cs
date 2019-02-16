using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private IAsyncRepository<Article> _articleRepository;
        private IAsyncRepository<Category> _categoryRepository;
        public ArticleCategoryService(IAsyncRepository<Article> articleRepository,
            IAsyncRepository<Category> categoryRepository
            )
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyList<Category>> ListCategories()
        {
            return await _categoryRepository.ListAllAsync();
        }

        public  async Task<Category> GetCategory(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }
        public async Task<IReadOnlyList<Category>> ListSubCategories(int parentCategoryId)
        {
            var spec = new SubCategoryFilterSpecification(parentCategoryId);
            return await _categoryRepository.ListAsync(spec);
        }

        public async Task<Category> AddCategory(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            Category _category= await _categoryRepository.GetByIdAsync(category.CategoryId);
            if (_category != null)
            {
                _category.CategoryName = category.CategoryName;
                _category.Description = category.Description;
                _category.UpdatedDate = DateTime.UtcNow;
                return await _categoryRepository.UpdateAsync(_category);
            }
            else
            {
                return null;
            }
        }

        public async Task<Category> DeleteCategory(int categoryId)
        {
            var spec = new ArticleFilterByCategoryIdSpecification(null, null,categoryId);
            var articles = await _articleRepository.ListAsync(spec);
            //there is article attached with this category
            if (articles.Count > 0)
            {
                return null;
            }
            Category _category = await _categoryRepository.GetByIdAsync(categoryId);
            if (_category != null)
            {
                return await _categoryRepository.DeleteAsync(_category);
            }
            else
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<Article>> ListArticles(int skip, int take, int? categoryId)
        {
            var spec = new ArticleFilterByCategoryIdSpecification(skip, take, categoryId);
            var articles = await _articleRepository.ListAsync(spec);
            return articles;          
        }

        public async Task<Article> GetArticle(int articleId)
        {
            return await _articleRepository.GetByIdAsync(articleId);
        }

        public async Task<Article> AddArticle(Article article)
        {
            Article _article = await _articleRepository.AddAsync(article);
            return _article;
        }

        public async Task<Article> UpdateArticle(Article article)
        {
            Article _article = await _articleRepository.GetByIdAsync(article.ArticleId);
            if (_article!= null)
            {
                _article.ContentUrl = article.ContentUrl;
                _article.Description= article.Description;
                _article.ImageUrl= article.ImageUrl;
                _article.Title= article.Title;
                _article.CategoryId = article.CategoryId;
                _article.UpdatedDate = DateTime.UtcNow;
                return await _articleRepository.UpdateAsync(_article);
            }
            else
            {
                return null;
            }
        }
        public async Task<Article> DeleteArticle(int articleId)
        {
            Article _article = await _articleRepository.GetByIdAsync(articleId);
            if (_article != null)
            {
                return await _articleRepository.DeleteAsync(_article);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> CountArticle(int? categoryId)
        {
            var spec = new ArticleFilterByCategoryIdSpecification(null, null, categoryId);
            return await _articleRepository.CountAsync(spec);
        }


    }


}

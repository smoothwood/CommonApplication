using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public class CommonApplicationContext : DbContext
    {
        public CommonApplicationContext(DbContextOptions<CommonApplicationContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Role role1 = new Role {RoleId=1, RoleName = "Admin", Description = "管理员" };
            Role role2 = new Role {RoleId=2, RoleName = "Employee", Description = "公司员工" };

            User user1 = new User {UserId=1, LoginId = "ycm@outlook.com", Email = "ycm@outlook.com", FirstName = "丞民", LastName = "杨", Password = "ycm119".GetMD5HashedValue() };
            User user2 = new User {UserId=2, LoginId = "12345929@qq.com", Email = "12345929@qq.com", FirstName = "悟空", LastName = "孙", Password = "!qazxsw2".GetMD5HashedValue() };

            UserRole userRole1 = new UserRole {Id=1, UserId = user1.UserId, RoleId = role1.RoleId };
            UserRole userRole2 = new UserRole {Id=2, UserId = user2.UserId, RoleId = role2.RoleId };
            UserRole userRole3 = new UserRole {Id=3, UserId = user1.UserId, RoleId = role2.RoleId };

            modelBuilder.Entity<Role>().HasData(role1);
            modelBuilder.Entity<Role>().HasData(role2);
            modelBuilder.Entity<User>().HasData(user1);
            modelBuilder.Entity<User>().HasData(user2);
            modelBuilder.Entity<UserRole>().HasData(userRole1);
            modelBuilder.Entity<UserRole>().HasData(userRole2);
            modelBuilder.Entity<UserRole>().HasData(userRole3);


            Category category1 = new Category { CategoryId = 1, CategoryName = "新人培训资料库", ParentCategoryId = 0 };
            Category category2 = new Category { CategoryId = 2, CategoryName = "基础营养知识库", ParentCategoryId = 0 };
            Category category3 = new Category { CategoryId = 3, CategoryName = "迷思与常用话术库", ParentCategoryId = 0 };
            Category category4 = new Category { CategoryId = 4, CategoryName = "基础营养知识库-子分类1", ParentCategoryId = 2 };
            Category category5 = new Category { CategoryId = 5, CategoryName = "基础营养知识库-子分类2", ParentCategoryId = 2 };


            Article article1 = new Article { ArticleId = 1, CategoryId = 1, Title = "article1", Description = "article1 description", ContentUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", ImageUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png"};
            Article article2 = new Article { ArticleId = 2, CategoryId = 4, Title = "article2", Description = "article2 description", ContentUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", ImageUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png"};
            Article article3 = new Article { ArticleId = 3, CategoryId = 4, Title = "article3", Description = "article3 description", ContentUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", ImageUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png"};
            Article article4 = new Article { ArticleId = 4, CategoryId = 4, Title = "article4", Description = "article4 description", ContentUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", ImageUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png"};
            Article article5 = new Article { ArticleId = 5, CategoryId = 5, Title = "article5", Description = "article5 description", ContentUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", ImageUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png"};
            Article article6 = new Article { ArticleId = 6, CategoryId = 2, Title = "article6", Description = "article6 description", ContentUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", ImageUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png"};
            Article article7 = new Article { ArticleId = 7, CategoryId = 3, Title = "article7", Description = "article7 description", ContentUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png", ImageUrl = "http://i1.haidii.com/v/1517537102/i1/images/dict_search_logo.png"};


            modelBuilder.Entity<Category>().HasData(category1);
            modelBuilder.Entity<Category>().HasData(category2);
            modelBuilder.Entity<Category>().HasData(category3);
            modelBuilder.Entity<Category>().HasData(category4);
            modelBuilder.Entity<Category>().HasData(category5);

            modelBuilder.Entity<Article>().HasData(article1);
            modelBuilder.Entity<Article>().HasData(article2);
            modelBuilder.Entity<Article>().HasData(article3);
            modelBuilder.Entity<Article>().HasData(article4);
            modelBuilder.Entity<Article>().HasData(article5);
            modelBuilder.Entity<Article>().HasData(article6);
            modelBuilder.Entity<Article>().HasData(article7);

        }
    }
}

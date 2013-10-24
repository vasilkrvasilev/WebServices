using BlogSystem.Data;
using BlogSystem.Models;
using BlogSystem.Services.Attributes;
using BlogSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace BlogSystem.Services.Controllers
{
    public class PostsController : BaseApiController
    {
        public IQueryable<PostModel> GetAll(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var context = new BlogSystemContext();

                var user = context.Users.FirstOrDefault(
                    usr => usr.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new InvalidOperationException("Invalid sessionKey");
                }

                var postEntities = context.Posts;
                var models =
                    (from postEntity in postEntities
                     select new PostModel()
                     {
                         Id = postEntity.Id,
                         Title = postEntity.Title,
                         PostDate = postEntity.PostDate,
                         Text = postEntity.Text,
                         PostedBy = postEntity.User.Nickname,
                         Tags = (from tagEntity in postEntity.Tags
                                 select tagEntity.Name),
                         Comments = (from commentEntity in postEntity.Comments
                                  select new CommentModel()
                                  {
                                      Text = commentEntity.Text,
                                      PostDate = commentEntity.PostDate,
                                      CommentedBy = commentEntity.User.Nickname
                                  })
                     });
                return models.OrderByDescending(p => p.PostDate);
            });

            return responseMsg;
        }

        //api/threads?sessionKey=.......&page=5&count=3
        public IQueryable<PostModel> GetPage(int page, int count,
           [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var models = this.GetAll(sessionKey)
                .Skip(page * count)
                .Take(count);
            return models;
        }

        public IQueryable<PostModel> GetByKeyword(string keyword,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var models = this.GetAll(sessionKey)
                .Where(p => p.Title.ToLower().Contains(keyword.ToLower()));
            return models;
        }

        public IQueryable<PostModel> GetByTags(string tags,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            string[] tagNames = tags.Split(',');
            var models = this.GetAll(sessionKey)
                .Where(p => !tagNames.Any(n => !p.Tags.Contains(n)));
            return models;
        }

        public HttpResponseMessage PostNewPost(PostNewModel model,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
              () =>
              {
                  var context = new BlogSystemContext();
                  using (context)
                  {
                      var user = context.Users.FirstOrDefault(
                          usr => usr.SessionKey == sessionKey);
                      if (user == null)
                      {
                          throw new InvalidOperationException("Invalid sessionKey");
                      }

                      if (model.Title == null || model.Text == null)
                      {
                          throw new ArgumentNullException("Post title or post text cannot be null");
                      }

                      string[] titleWords = model.Title.Split(
                          new char[] { ' ', ',', '.', '!', '?', '\'', '(', ')' }, 
                          StringSplitOptions.RemoveEmptyEntries);

                      IList<Tag> tags = new List<Tag>();
                      if (model.Tags != null)
                      {
                          foreach (var item in model.Tags)
                          {
                              var tag = context.Tags.FirstOrDefault(t => t.Name == item.ToLower());
                              if (tag == null)
                              {
                                  tag = new Tag()
                                  {
                                      Name = item.ToLower()
                                  };

                                  context.Tags.Add(tag);
                                  context.SaveChanges();
                              }

                              tags.Add(tag);
                          }
                      }

                      foreach (var item in titleWords)
                      {
                          var tag = context.Tags.FirstOrDefault(t => t.Name == item.ToLower());
                          if (tag == null)
                          {
                              tag = new Tag()
                              {
                                  Name = item.ToLower()
                              };

                              context.Tags.Add(tag);
                              context.SaveChanges();
                          }

                          tags.Add(tag);
                      }

                      var post = new Post()
                      {
                          Title = model.Title,
                          Text = model.Text,
                          PostDate = DateTime.Now,
                          User = user,
                          Tags = tags
                      };

                      context.Posts.Add(post);
                      context.SaveChanges();

                      var createdModel = new PostCreatedModel()
                      {
                          Id = post.Id,
                          Title = post.Title
                      };

                      var response =
                          this.Request.CreateResponse(HttpStatusCode.Created,
                                          createdModel);
                      return response;
                  }
              });

            return responseMsg;
        }

        [ActionName("comment")]
        public HttpResponseMessage PutComment(int postId, CommentNewModel model,
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
              () =>
              {
                  var context = new BlogSystemContext();
                  using (context)
                  {
                      var user = context.Users.FirstOrDefault(
                          usr => usr.SessionKey == sessionKey);

                      if (user == null)
                      {
                          throw new InvalidOperationException("Invalid sessionKey");
                      }

                      var post = context.Posts.FirstOrDefault(p => p.Id == postId);

                      if (post == null)
                      {
                          throw new InvalidOperationException("Invalid postId");
                      }

                      if (model.Text == null)
                      {
                          throw new ArgumentNullException("Comment text cannot be null");
                      }

                      var comment = new Comment()
                      {
                          Text = model.Text,
                          User = user,
                          PostDate = DateTime.Now,
                          Post = post
                      };

                      context.Comments.Add(comment);
                      context.SaveChanges();

                      var response =
                          this.Request.CreateResponse(HttpStatusCode.OK, "NULL");
                      return response;
                  }
              });

            return responseMsg;
        }
    }
}

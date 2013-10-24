using BlogSystem.Data;
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
    public class TagsController : BaseApiController
    {
        public IQueryable<TagModel> GetAll(
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

                var tagEntities = context.Tags;
                var models =
                    (from tagEntity in tagEntities
                     select new TagModel()
                     {
                         Id = tagEntity.Id,
                         Name = tagEntity.Name,
                         Posts = tagEntity.Posts.Count
                     });
                return models.OrderBy(t => t.Name);
            });

            return responseMsg;
        }

        [ActionName("posts")]
        public IQueryable<PostModel> GetPosts(int tagId,
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

                var tag = context.Tags.FirstOrDefault(t => t.Id == tagId);

                if (tag == null)
                {
                    throw new InvalidOperationException("Invalid tagId");
                }

                var postEntities = tag.Posts;
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

            return responseMsg.AsQueryable();
        }
    }
}

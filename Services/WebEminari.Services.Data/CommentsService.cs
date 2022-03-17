using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;
using WebEminari.Services.Mapping;
using WebEminari.Web.ViewModels;
using WebEminari.Web.ViewModels.Administration;
using WebEminari.Web.ViewModels.Comments;
using WebEminari.Web.ViewModels.WebEminars;

namespace WebEminari.Services.Data
{
    public class CommentsService : ICommentsService
    { 

    private readonly IDeletableEntityRepository<Comment> commentRepository;
    private readonly IDeletableEntityRepository<Report> reportRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository,
            IDeletableEntityRepository<Report> reportRepository)
        {
            this.commentRepository = commentRepository;
            this.reportRepository = reportRepository;
        }

    public bool Contains(int id)
        => this.commentRepository
            .All()
            .Any(x => x.Id == id);

    public int GetAllCommentsCount()
        => this.commentRepository
            .All()
            .Count();

    public async Task CreateCommentAsync(CommentInputModel commentInputModel, int? parentId = null)
    {
        Comment comment = new Comment
        {
            Content = commentInputModel.Content,
            ApplicationUserId = commentInputModel.ApplicationUserId,
            WebEminarId = commentInputModel.WebEminarId,
            ParentId = parentId,
        };

        await this.commentRepository.AddAsync(comment);
        await this.commentRepository.SaveChangesAsync();
    }

    public async Task EditCommentAsync(CommentEditModel commentEditModel)
    {
        Comment entity = this.commentRepository.All().FirstOrDefault(x => x.Id == commentEditModel.Id);
        if (entity != null)
        {
            entity.Content = commentEditModel.Content ?? entity.Content;
        }

        await this.commentRepository.SaveChangesAsync();
    }

    public async Task EditCommentByAdminAsync(AdminCommentEditModel commentEditModel)
    {
        Comment comment = this.commentRepository
            .AllWithDeleted()
            .FirstOrDefault(x => x.Id == commentEditModel.Id);

        if (comment != null)
        {
            comment.Content = commentEditModel.Content ?? comment.Content;

            if (commentEditModel.IsDeleted)
            {
                this.commentRepository.Delete(comment);
            }
            else
            {
                this.commentRepository.Undelete(comment);
            }
        }

        await this.commentRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteCommentByIdAsync(int commentId)
    {
        Comment comment = this.commentRepository.AllWithDeleted().FirstOrDefault(x => x.Id == commentId);
            var replyes = this.commentRepository.All().Where(x => x.ParentId == commentId);
            foreach(var reply in replyes)
            {
                this.commentRepository.Delete(reply);
            }
        if (comment != null)
        {
            this.commentRepository.Delete(comment);
        }

        await this.commentRepository.SaveChangesAsync();
    }

    public async Task HardDeleteCommentByIdAsync(int commentId)
    {
        Comment comment = this.commentRepository.AllWithDeleted().FirstOrDefault(x => x.Id == commentId);
        Report report = this.reportRepository.AllWithDeleted().FirstOrDefault(x => x.CommentId == commentId);
            var replyes = this.commentRepository.AllWithDeleted().Where(x => x.ParentId == commentId);
            foreach (var reply in replyes)
            {
                this.commentRepository.HardDelete(reply);
            }
            if (report != null)
            {
                this.reportRepository.HardDelete(report);
            }
            if (comment != null)
        {
            this.commentRepository.HardDelete(comment);
        }

        await this.commentRepository.SaveChangesAsync();
    }

    public async Task<T> GetCommentByIdWithDeletedAsync<T>(int id)
        => await this.commentRepository.AllWithDeleted()
            .Where(x => x.Id == id)
            .Include(x => x.WebEminar)
            .Include(x => x.WebEminar.Category)
            .Include(x => x.WebEminar.AddedByUser)
            .Include(x => x.ApplicationUser)
            .To<T>()
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<CommentByMeModel>> GetCommentsMadeByMeAsync(string userId)
        => await this.commentRepository.All()
            .Where(x => x.ApplicationUserId == userId)
             .Include(x => x.WebEminar)
            .Include(x => x.WebEminar.Category)
            .Include(x => x.WebEminar.AddedByUser)
            .Include(x => x.ApplicationUser)
            .To<CommentByMeModel>()
            .ToListAsync();

        public async Task<IEnumerable<CommentViewModel>> GetCommentsMadeInMyWebEminarAsync(string userId)
            => await this.commentRepository.All()
            .Where(x => x.WebEminar.AddedByUserId == userId)
             .Include(x => x.WebEminar)
            .Include(x => x.WebEminar.Category)
            .Include(x => x.WebEminar.AddedByUser)
            .Include(x => x.ApplicationUser)
            .To<CommentViewModel>()
            .ToListAsync();




        public async Task<IEnumerable<AdminCommentViewModel>> GetAllCommentsAsync()
    {
        IQueryable<Comment> allComments = this.commentRepository.AllWithDeleted();

        var comments = await allComments
            .Include(x => x.ApplicationUser)
            .Include(x => x.WebEminar)
            .Include(x => x.WebEminar.Category)
            .Include(x => x.WebEminar.AddedByUser)
            .Include(x => x.ApplicationUser)
            .OrderByDescending(x => x.CreatedOn)
            .To<AdminCommentViewModel>()
            .ToListAsync();

        return comments;
    }
    }
}

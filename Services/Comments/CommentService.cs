using AutoMapper;
using BridgeRTU.Domain;
using BridgeRTU.Interfaces;
using BridgeRTU.Persistance.Data;
using BridgeRTU.Services.Comments.Dto;

namespace BridgeRTU.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Comment> CreateComment(CommentDto commentDto)
        {
            var comment = new Comment
            {
                PostingDate = DateTime.Now,
                Description = commentDto.Description,
                StudentId = commentDto.StudentId,
                ProjectId = commentDto.ProjectId
            };
            var result=await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;

        }
    }
}

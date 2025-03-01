using BridgeRTU.Domain;
using BridgeRTU.Services.Comments.Dto;

namespace BridgeRTU.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> CreateComment(CommentDto commentDto);
    }
}

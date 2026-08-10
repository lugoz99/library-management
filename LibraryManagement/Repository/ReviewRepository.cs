using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository;

public class ReviewRepository(ApplicationDbContext context):IReviewRepository
{
    public async Task<IEnumerable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Reviews
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task AddAsync(Review review, CancellationToken cancellationToken = default)
    {
        
        await context.Reviews.AddAsync(review, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

 
    
    
    // listar revision de libro
}
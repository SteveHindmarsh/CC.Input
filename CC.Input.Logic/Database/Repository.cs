//namespace CC.Input.Logic.Database;

//public class Repository : IRepository<Model.Input>
//{
//    private InputDbContext _inputDbContext { get; set; }

//    public InputRepository(InputDbContext inputDbContext)
//    {
//        _inputDbContext = inputDbContext;
//    }
//    public async Task<IEnumerable<Model.Input>> GetAllAsync()
//    {
//        return await _inputDbContext.Inputs.ToListAsync();
//    }

//    public async Task<Model.Input?> GetByIdAsync(int id)
//    {
//        return await _inputDbContext.Inputs.FindAsync(id);
//    }

//    public async Task<int> CreateAsync(Model.Input entity)
//    {
//        await _inputDbContext.Inputs.AddAsync(entity);
//        await _inputDbContext.SaveChangesAsync();
//        return entity.Id;
//    }

//    public async Task UpdateAsync(Model.Input entity)
//    {
//        _inputDbContext.Inputs.Update(entity);
//        await _inputDbContext.SaveChangesAsync();
//    }

//    public async Task DeleteAsync(int id)
//    {
//        var entity = await _inputDbContext.Inputs.SingleOrDefaultAsync(e => e.Id == id);
//        if (entity != null)
//            _inputDbContext.Inputs.Remove(entity);

//        await _inputDbContext.SaveChangesAsync();
//    }
//}

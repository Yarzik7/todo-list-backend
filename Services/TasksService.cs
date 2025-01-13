using Models;
using MongoDB.Driver;

namespace Services;

public class TasksService
{
    private readonly IMongoCollection<TaskModel> _tasksCollection;

    public TasksService()
    {
        var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("DB_URI"));
        var mongoDatabase = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
        _tasksCollection = mongoDatabase.GetCollection<TaskModel>(Environment.GetEnvironmentVariable("TASKS_COLLECTION_NAME"));
    }

    public async Task<List<TaskModel>> GetAsync() => await _tasksCollection.Find(_ => true).ToListAsync();

    public async Task<TaskModel?> GetAsync(string id) => await _tasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TaskModel newTask) => await _tasksCollection.InsertOneAsync(newTask);

    public async Task UpdateAsync(string id, TaskModel updatedTask) => await _tasksCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

    public async Task RemoveAsync(string id) => await _tasksCollection.DeleteOneAsync(x => x.Id == id);
}
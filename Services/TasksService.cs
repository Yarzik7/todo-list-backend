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

    public async Task<TaskModel?> GetAsync(string id) => await _tasksCollection.Find(task => task.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TaskModel newTask) => await _tasksCollection.InsertOneAsync(newTask);

    public async Task RemoveAsync(string id) => await _tasksCollection.DeleteOneAsync(task => task.Id == id);

    public async Task UpdateAsync(string id, UpdateTaskModel updatedTask) {
        var filter = Builders<TaskModel>.Filter.Eq(task => task.Id, id);
        var updateBuilder = Builders<TaskModel>.Update;

        var updateDefinitions = new List<UpdateDefinition<TaskModel>>();

        if (updatedTask.Caption != null)
        {
           updateDefinitions.Add(updateBuilder.Set(task => task.Caption, updatedTask.Caption));
        }

        if (updatedTask.IsCompleted.HasValue)
        {
           updateDefinitions.Add(updateBuilder.Set(task => task.IsCompleted, updatedTask.IsCompleted));
        }

        if (updateDefinitions.Any())
        {
           var combinedUpdate = Builders<TaskModel>.Update.Combine(updateDefinitions);
           await _tasksCollection.UpdateOneAsync(filter, combinedUpdate);
        }
    }
}
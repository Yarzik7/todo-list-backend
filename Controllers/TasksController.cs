using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TasksService _tasksService;

    public TasksController(TasksService tasksService) => _tasksService = tasksService;

    [HttpGet]
    public async Task<List<TaskModel>> Get() => await _tasksService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TaskModel>> Get(string id)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Post(TaskModel newTask)
    {
        await _tasksService.CreateAsync(newTask);

        return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
    }

    [HttpPatch("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, UpdateTaskModel updatedTask)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await _tasksService.UpdateAsync(id, updatedTask);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await _tasksService.RemoveAsync(id);

        return NoContent();
    }
}
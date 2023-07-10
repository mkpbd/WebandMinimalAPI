using CollageApp.Model;
using CollageApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItems = await TodoItemRepository.TodoItem();

           var todoItem =   todoItems.Where(x => x.Id == id).FirstOrDefault();

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var todoItems = await TodoItemRepository.TodoItem();
             var lastId = todoItems.LastOrDefault();
            todoItem.Id = lastId.Id > 0 ? lastId.Id + 1 : 1;

            todoItems.Add(todoItem);

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            try
            {
                var todoItems = await TodoItemRepository.TodoItem();

                var exsistingItems = todoItems.Where(_ => _.Id == id).FirstOrDefault();
                exsistingItems.Name = todoItem.Name;
                exsistingItems.IsComplete = todoItem.IsComplete;

                return Ok(exsistingItems);
            }
            catch (Exception ex)
            {        
                    return NotFound();
            }

            //return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItems = await TodoItemRepository.TodoItem();
            var exsistingItems = todoItems.Where(_ => _.Id == id).FirstOrDefault();
            if (exsistingItems == null)
            {
                return NotFound();
            }

            todoItems.Remove(exsistingItems);
         

            return NoContent();
        }

    }
}

using CollageApp.DTO;
using CollageApp.Model;
using CollageApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CollageApp.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<TeacherDTO>> GetAllTeacher()
        {

            List<Teacher> teachers = TeacherRepository.Teachers.ToList();

            if (teachers == null && teachers?.Count <= 0) return BadRequest();

            List<TeacherDTO> teacherDTOs = new List<TeacherDTO>();

            foreach (var teacher in teachers)
            {
                var techDto = new TeacherDTO();

                techDto.Name = teacher.Name;
                techDto.Department = teacher.Department;
                techDto.Id = teacher.Id;
                techDto.Degnation = teacher.Degnation;
                teacherDTOs.Add(techDto);
            }



            // 200 ok then return  All Teacher
            return Ok(teacherDTOs);
        }

        [HttpPost("Create")]
        public ActionResult<TeacherDTO> CreateTeacher([FromBody] TeacherDTO model)
        {

            if (model == null) return BadRequest();

            List<Teacher> teachers = TeacherRepository.Teachers.ToList();
            var lastId = teachers?.LastOrDefault()?.Id + 1 ?? 1;

            Teacher teacher = new Teacher()
            {
                Name = model.Name,
                Department = model.Department,
                Degnation = model.Degnation,
                Id = lastId
            };

            teachers.Add(teacher);
            model.Id = lastId;
            return Ok(model);



        }

        [HttpPut("Update")]
        public ActionResult<TeacherDTO> UpdateData([FromBody] TeacherDTO model) {
            if(model == null) return BadRequest();

            List<Teacher> teachers = TeacherRepository.Teachers.ToList();
            Teacher teacher = teachers.Where(x => x.Id == model.Id).FirstOrDefault();
            if(teacher == null) return NotFound();


            teacher.Name = model.Name;
            teacher.Department = model.Department;
            teacher.Degnation = model.Degnation;

            

            return Ok(teacher);
        }

        
    }
}

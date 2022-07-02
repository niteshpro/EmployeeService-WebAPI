using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        //[ActionName("LoadAllEmployee")]
        [HttpGet]
        public IEnumerable<Employees> LoadAllEmployee() {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }
        //public Employees Get(int id)
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        return entities.Employees.FirstOrDefault(e => e.ID == id);
        //    }
        //}

        //GET(id) Method
        [HttpGet]
        public HttpResponseMessage LoadAllEmployeeById(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with Id " + id.ToString() + " not found");
                }
            }
        }

        /* public void Post([FromBody] Employees employees)
         {
             using (EmployeeDBEntities entities = new EmployeeDBEntities())
             {
                 entities.Employees.Add(employees);
                 entities.SaveChanges();
             }
         }*/


        //POST Method
        public HttpResponseMessage Post([FromBody] Employees employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        employee.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Delete Method

        //public void Delete(int id)
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.ID == id));
        //        entities.SaveChanges();
        //    }
        //}

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //PUT Method
        //public void Put(int id,[FromBody] Employees employees)
        //{
        //    using(EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        var entity=entities.Employees.FirstOrDefault(e => e.ID == id);
        //        entity.FirstName = employees.FirstName;
        //        entity.LastName = employees.LastName;   
        //        entity.Gender = employees.Gender;
        //        entity.Salary = employees.Salary;

        //        entities.SaveChanges();

        //    }
        //}

        public HttpResponseMessage Put(int id, [FromBody] Employees employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudDepartamentos2023.Models;
using MvcCoreApiCrudDepartamentos2023.Services;

namespace MvcCoreApiCrudDepartamentos2023.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceDepartamento service;

        public DepartamentosController(ServiceDepartamento service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Departamentos()
        {
            List<Departamento> departamentos = await this.service.GetDepartamentosAsync();
            return View(departamentos);
        }

        [HttpPost]
        public async Task<IActionResult> Departamentos(string localidad)
        {
            List<Departamento> departamentos = await this.service.DepartamentosLocalidadAsync(localidad);
            return View(departamentos);
        }
        public async Task<IActionResult> Details(int id)
        {
            Departamento departamento = await this.service.FindDepartamentoAsync(id);
            return View(departamento);
        }

        public IActionResult CreateDepartamento()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartamento(Departamento dept)
        {
            await this.service.InsertDepartamentoAsync(dept.IdDept,dept.Nombre,dept.Localidad);
            return RedirectToAction("Departamentos");
        }

        public async Task<IActionResult> UpdateDepartamento(int id)
        {
            Departamento departamento = await this.service.FindDepartamentoAsync(id);
            return View(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartamento(Departamento dept)
        {
            await this.service.UpdateDepartamentoAsync(dept.IdDept, dept.Nombre, dept.Localidad);
            return RedirectToAction("Departamentos");
        }

        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            await this.service.DeleteDepartamentoAsync(id);
            return RedirectToAction("Departamentos");
        }


    }
}

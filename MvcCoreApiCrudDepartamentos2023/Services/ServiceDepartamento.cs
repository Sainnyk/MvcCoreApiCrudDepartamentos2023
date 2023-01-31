
using MvcCoreApiCrudDepartamentos2023.Models;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace MvcCoreApiCrudDepartamentos2023.Services
{
    public class ServiceDepartamento
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceDepartamento(string url)
        {
            this.UrlApi = url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> GetDatosApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response = await client.GetAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    T datos = await response.Content.ReadAsAsync<T>();
                    return datos;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "/api/departamentos";
            List<Departamento> departamentos = await this.GetDatosApiAsync<List<Departamento>>(request);
            return departamentos;
        }
        
        public async Task<Departamento> FindDepartamentoAsync(int id)
        {
            string request = "/api/departamentos/" + id;
            Departamento departamento = await this.GetDatosApiAsync<Departamento>(request);
            return departamento;
        }

        public async Task<List<Departamento>> DepartamentosLocalidadAsync(string localidad)
        {
            string request = "/api/departamentos/departamentoslocalidad/" + localidad;
            List<Departamento> departamentos = await this.GetDatosApiAsync<List<Departamento>>(request);
            return departamentos;
        }
    }
}

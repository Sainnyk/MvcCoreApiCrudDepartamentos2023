
using MvcCoreApiCrudDepartamentos2023.Models;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;

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

        //Los metodos de accion no suelen tener un metodo generico debido a que cada uno recibe un valor distinto
        public async Task DeleteDepartamentoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //Como no vamos a recibir nada (objeto), simplemente se realiza la accion.
                await client.DeleteAsync(request);
            }
        }

        //Vamos a usar insertar objeto en el body
        public async Task InsertDepartamentoAsync(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //TENEMOS QUE ENVIAR UN OBJETO DEPARTAMENTO POR LO QUE CREAMOS UNA CLASE DEL MODEL DEPARTAMENTO CON LOS DATOS PROPORCIONADOS
                Departamento dept = new Departamento();
                dept.IdDept = id;
                dept.Nombre = nombre;
                dept.Localidad = localidad;
                //Convertimos el objeto departamento en un json
                string departamentoJson = JsonConvert.SerializeObject(dept);
                //Para enviar el objeto json en el body se realiza mediante
                //una clase llamanda StringContent donde debemos indicar el tipo de contenido que estamos enviando(JSON)
                StringContent content = new StringContent(departamentoJson, Encoding.UTF8, "application/json"); //(objeto,codificacion por si tiene ñ o acentos,tipo de contenido)
                //Realizamos la llamada al servicio enviando el objeto content
                await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();

                Departamento dept = new Departamento();
                dept.IdDept = id;
                dept.Nombre = nombre;
                dept.Localidad = localidad;

                string departamentoJson = JsonConvert.SerializeObject(dept);
                StringContent content = new StringContent(departamentoJson,Encoding.UTF8, "application/json");

                await client.PutAsync(request, content);
            }
        }
    }
}

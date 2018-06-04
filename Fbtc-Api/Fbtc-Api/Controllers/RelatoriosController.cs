using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System;

using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Reflection;
using Fbtc.Infra.Helpers;
using System.Net.Http.Headers;

namespace Fbtc.Api.Controllers
{
    [RoutePrefix("api/Relatorios")]
    public class RelatoriosController : ApiController
    {
        private readonly IRelatoriosApplication _relatoriosApplication;

        public RelatoriosController(IRelatoriosApplication relatoriosApplication)
        {
            _relatoriosApplication = relatoriosApplication;
        }

        // [Authorize]
        [Route("GetRptTotalAssociadosTipo")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptTotalAssociadosTipo()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptTotalAssociadosTipo();

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        [Route("GetRptTotalAssociadosTipoToExcel")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptTotalAssociadosTipoToExcel()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            string _fileInfo = "";

            try
            {
                var resultado = _relatoriosApplication.GetRptTotalAssociadosTipo();

                //Lendo a coleção:
                if (resultado != null)
                {
                    List<object[]> lst = new List<object[]>();

                    foreach (var associadoTipo in resultado)
                    {
                        lst.Add(new object[] { associadoTipo.NomeTipoAssociado, associadoTipo.Qtd });
                    }

                    IEnumerable<object[]> cellData = lst;

                    //Gerando a planilha de saída
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        excel.Workbook.Worksheets.Add("TotalDeAssociadosPorTipo");

                        // Creating the header:
                        var headerRow = new List<string[]>()
                        {
                            new string[] { "Tipo de Associação", "Quantidade" }
                        };

                        // Determine the header range (e.g. A1:E1)
                        string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                        // Target a worksheet
                        var worksheet = excel.Workbook.Worksheets["TotalDeAssociadosPorTipo"];

                        // Popular header row data
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                        worksheet.Cells[2, 1].LoadFromArrays(cellData);

                        string _dtCriacao = DateTime.Now.ToString();
                        _dtCriacao = _dtCriacao.Replace(" ", "-");
                        _dtCriacao = _dtCriacao.Replace("/", "_");
                        _dtCriacao = _dtCriacao.Replace(":", "");

                        _fileInfo = $"{System.Web.HttpContext.Current.Server.MapPath($"~/ExportFiles/")}TotalDeAssociadosPorTipo_{_dtCriacao}.xlsx";

                        FileInfo excelFile = new FileInfo(_fileInfo);
                        excel.SaveAs(excelFile);
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);

                var stream = new FileStream(_fileInfo, FileMode.Open);
                response.Content = new StreamContent(stream);

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(_fileInfo);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("GetRptRecebimentoStatus/{objetivoPagamento:int},{anoEventoPS:int},{statusPS:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptRecebimentoStatus(int objetivoPagamento, int anoEventoPS, int statusPS)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (objetivoPagamento == 0) throw new Exception("objetivoPagamento não informado!");
                if (anoEventoPS == 0) throw new Exception("anoEventoPS não informado!");

                var resultado = _relatoriosApplication.GetRptRecebimentoStatus(objetivoPagamento, anoEventoPS, statusPS);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        [Route("GetRptRecebimentoStatusToExcel/{objetivoPagamento:int},{anoEventoPS:int},{statusPS:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptRecebimentoStatusToExcel(int objetivoPagamento, int anoEventoPS, int statusPS)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            string _fileInfo = "";

            try
            {
                if (objetivoPagamento == 0) throw new Exception("objetivoPagamento não informado!");
                if (anoEventoPS == 0) throw new Exception("anoEventoPS não informado!");

                var resultado = _relatoriosApplication.GetRptRecebimentoStatus(objetivoPagamento, anoEventoPS, statusPS);

                //Lendo a coleção:
                if (resultado != null)
                {
                    List<object[]> lst = new List<object[]>();

                    foreach (var item in resultado)
                    {
                        lst.Add(new object[] { item.StatusPagamento, item.Qtd, item.ValorPorStatus });
                    }

                    IEnumerable<object[]> cellData = lst;

                    //Gerando a planilha de saída
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        excel.Workbook.Worksheets.Add("RecebimentosPorStatus");

                        // Creating the header:
                        var headerRow = new List<string[]>()
                        {
                            new string[] { "Status Pagamento", "Quantidade", "Valor" }
                        };

                        // Determine the header range (e.g. A1:E1)
                        string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                        // Target a worksheet
                        var worksheet = excel.Workbook.Worksheets["RecebimentosPorStatus"];

                        // Popular header row data
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                        worksheet.Cells[2, 1].LoadFromArrays(cellData);

                        string _dtCriacao = DateTime.Now.ToString();
                        _dtCriacao = _dtCriacao.Replace(" ", "-");
                        _dtCriacao = _dtCriacao.Replace("/", "_");
                        _dtCriacao = _dtCriacao.Replace(":", "");

                        _fileInfo = $"{System.Web.HttpContext.Current.Server.MapPath($"~/ExportFiles/")}RecebimentosPorStatus_{_dtCriacao}.xlsx";

                        FileInfo excelFile = new FileInfo(_fileInfo);
                        excel.SaveAs(excelFile);
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);

                var stream = new FileStream(_fileInfo, FileMode.Open);
                response.Content = new StreamContent(stream);

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(_fileInfo);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("GetRptAssociadosFaixa")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosFaixa()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptAssociadosFaixa();

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        [Route("GetRptAssociadosFaixaToExcel")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosFaixaToExcel()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            string _fileInfo = "";

            try
            {
                var resultado = _relatoriosApplication.GetRptAssociadosFaixa();

                //Lendo a coleção:
                if (resultado != null)
                {
                    List<object[]> lst = new List<object[]>();

                    foreach (var item in resultado)
                    {
                        lst.Add(new object[] { item.FaixaAte30, item.Faixa31a40, item.Faixa41a50, item.Faixa51a60, item.FaixaApos60 });
                    }

                    IEnumerable<object[]> cellData = lst;

                    //Gerando a planilha de saída
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        excel.Workbook.Worksheets.Add("UsuariosPorFaixaEtaria");

                        // Creating the header:
                        var headerRow = new List<string[]>()
                        {
                            new string[] { "Até 30 anos", "Entre 31 e 40 anos", "Entre 41 e 50 anos", "Entre 51 e 60 anos", "Maior que 60 anos" }
                        };

                        // Determine the header range (e.g. A1:E1)
                        string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                        // Target a worksheet
                        var worksheet = excel.Workbook.Worksheets["UsuariosPorFaixaEtaria"];

                        // Popular header row data
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                        worksheet.Cells[2, 1].LoadFromArrays(cellData);

                        string _dtCriacao = DateTime.Now.ToString();
                        _dtCriacao = _dtCriacao.Replace(" ", "-");
                        _dtCriacao = _dtCriacao.Replace("/", "_");
                        _dtCriacao = _dtCriacao.Replace(":", "");

                        _fileInfo = $"{System.Web.HttpContext.Current.Server.MapPath($"~/ExportFiles/")}UsuariosPorFaixaEtaria_{_dtCriacao}.xlsx";

                        FileInfo excelFile = new FileInfo(_fileInfo);
                        excel.SaveAs(excelFile);
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);

                var stream = new FileStream(_fileInfo, FileMode.Open);
                response.Content = new StreamContent(stream);

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(_fileInfo);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("GetRptAssociadosEstados")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosEstados()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptAssociadosEstados();

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        [Route("GetRptAssociadosEstadosToExcel")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosEstadosToExcel()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            string _fileInfo = "";

            try
            {
                var resultado = _relatoriosApplication.GetRptAssociadosEstados();

                //Lendo a coleção:
                if (resultado != null)
                {
                    List<object[]> lst = new List<object[]>();

                    foreach (var item in resultado)
                    {
                        lst.Add(new object[] { item.NomeUF, item.ProfissionalAssociado, item.ProfissionalNaoAssociado,
                        item.EstudantePosAssociado, item.EstudantePosNaoAssociado, item.EstudanteNaoAssociado, item.EstudanteNaoAssociado});
                    }

                    IEnumerable<object[]> cellData = lst;

                    //Gerando a planilha de saída
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        excel.Workbook.Worksheets.Add("UsuariosPorUFeTipoAssociacao");

                        // Creating the header:
                        var headerRow = new List<string[]>()
                        {
                            new string[] { "UF", "Profissional", "Profissional Não Associado", "Estudante de Pós", "Estudante de Pós Não Associado", "Estudante", "Estudante Não Associado" }
                        };

                        // Determine the header range (e.g. A1:E1)
                        string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                        // Target a worksheet
                        var worksheet = excel.Workbook.Worksheets["UsuariosPorUFeTipoAssociacao"];

                        // Popular header row data
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                        worksheet.Cells[2, 1].LoadFromArrays(cellData);

                        string _dtCriacao = DateTime.Now.ToString();
                        _dtCriacao = _dtCriacao.Replace(" ", "-");
                        _dtCriacao = _dtCriacao.Replace("/", "_");
                        _dtCriacao = _dtCriacao.Replace(":", "");

                        _fileInfo = $"{System.Web.HttpContext.Current.Server.MapPath($"~/ExportFiles/")}UsuariosPorUFeTipoAssociacao_{_dtCriacao}.xlsx";

                        FileInfo excelFile = new FileInfo(_fileInfo);
                        excel.SaveAs(excelFile);
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);

                var stream = new FileStream(_fileInfo, FileMode.Open);
                response.Content = new StreamContent(stream);

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(_fileInfo);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("GetRptAssociadosGenero/{genero}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosGenero(string genero)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (string.IsNullOrWhiteSpace(genero)) throw new Exception("genero não informado!");

                var resultado = _relatoriosApplication.GetRptAssociadosGenero(genero);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        [Route("GetRptAssociadosGeneroToExcel/{genero}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosGeneroToExcel(string genero)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            string _fileInfo = "";

            try
            {
                if (string.IsNullOrWhiteSpace(genero)) throw new Exception("genero não informado!");

                var resultado = _relatoriosApplication.GetRptAssociadosGenero(genero);

                //Lendo a coleção:
                if (resultado != null)
                {
                    List<object[]> lst = new List<object[]>();

                    foreach (var item in resultado)
                    {
                        lst.Add(new object[] { item.NomeTipoAssociado, item.Qtd });
                    }

                    IEnumerable<object[]> cellData = lst;

                    //Gerando a planilha de saída
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        excel.Workbook.Worksheets.Add($"UsuariosPorGenero - {genero}");

                        // Creating the header:
                        var headerRow = new List<string[]>()
                        {
                            new string[] { "Tipo de Associação", "Quantidade" }
                        };

                        // Determine the header range (e.g. A1:E1)
                        string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                        // Target a worksheet
                        var worksheet = excel.Workbook.Worksheets[$"UsuariosPorGenero - {genero}"];

                        // Popular header row data
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                        worksheet.Cells[2, 1].LoadFromArrays(cellData);

                        string _dtCriacao = DateTime.Now.ToString();
                        _dtCriacao = _dtCriacao.Replace(" ", "-");
                        _dtCriacao = _dtCriacao.Replace("/", "_");
                        _dtCriacao = _dtCriacao.Replace(":", "");

                        _fileInfo = $"{System.Web.HttpContext.Current.Server.MapPath($"~/ExportFiles/")}UsuariosPorGeneroeTipoAssociacao_{_dtCriacao}.xlsx";

                        FileInfo excelFile = new FileInfo(_fileInfo);
                        excel.SaveAs(excelFile);
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);

                var stream = new FileStream(_fileInfo, FileMode.Open);
                response.Content = new StreamContent(stream);

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(_fileInfo);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        // [Authorize]
        [Route("GetRptAssociadosAno/{ano:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosAno(int ano)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                if (ano == 0) throw new Exception("ano não informado!");

                var resultado = _relatoriosApplication.GetRptAssociadosAno(ano);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        [Route("GetRptAssociadosAnoToExcel/{ano:int}")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptAssociadosAnoToExcel(int ano)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            string _fileInfo = "";

            try
            {
                if (ano == 0) throw new Exception("ano não informado!");

                var resultado = _relatoriosApplication.GetRptAssociadosAno(ano);

                //Lendo a coleção:
                if (resultado != null)
                {
                    List<object[]> lst = new List<object[]>();

                    foreach (var item in resultado)
                    {
                        lst.Add(new object[] { item.NomeTipoAssociado, item.Qtd });
                    }

                    IEnumerable<object[]> cellData = lst;

                    //Gerando a planilha de saída
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        excel.Workbook.Worksheets.Add($"UsuariosPorAno - {ano}");

                        // Creating the header:
                        var headerRow = new List<string[]>()
                        {
                            new string[] { "Tipo de Associação", "Quantidade" }
                        };

                        // Determine the header range (e.g. A1:E1)
                        string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                        // Target a worksheet
                        var worksheet = excel.Workbook.Worksheets[$"UsuariosPorAno - {ano}"];

                        // Popular header row data
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                        worksheet.Cells[2, 1].LoadFromArrays(cellData);

                        string _dtCriacao = DateTime.Now.ToString();
                        _dtCriacao = _dtCriacao.Replace(" ", "-");
                        _dtCriacao = _dtCriacao.Replace("/", "_");
                        _dtCriacao = _dtCriacao.Replace(":", "");

                        _fileInfo = $"{System.Web.HttpContext.Current.Server.MapPath($"~/ExportFiles/")}UsuariosPorAnoeTipoAssociacao_{_dtCriacao}.xlsx";

                        FileInfo excelFile = new FileInfo(_fileInfo);
                        excel.SaveAs(excelFile);
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);

                var stream = new FileStream(_fileInfo, FileMode.Open);
                response.Content = new StreamContent(stream);

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(_fileInfo);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }
        
        // [Authorize]
        [Route("GetRptReceitaAnual")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptReceitaAnual()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                var resultado = _relatoriosApplication.GetRptReceitaAnual();

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }

        [Route("GetRptReceitaAnualToExcel")]
        [HttpGet]
        public Task<HttpResponseMessage> GetRptReceitaAnualToExcel()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            string _fileInfo = "";

            try
            {
                var resultado = _relatoriosApplication.GetRptReceitaAnual();

                //Lendo a coleção:
                if (resultado != null)
                {
                    List<object[]> lst = new List<object[]>();

                    foreach (var item in resultado)
                    {
                        lst.Add(new object[] { item.Ano, item.ValorPrevisto, item.ValorRealizado, item.QtdIsentos, item.NomeObjetivoPagamento });
                    }

                    IEnumerable<object[]> cellData = lst;

                    //Gerando a planilha de saída
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        excel.Workbook.Worksheets.Add("ReceitaAnual");

                        // Creating the header:
                        var headerRow = new List<string[]>()
                        {
                            new string[] { "Ano", "Valor Previsto", "Valor Realizado", "Nº de Isentos", "Objetivo" }
                        };

                        // Determine the header range (e.g. A1:E1)
                        string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                        // Target a worksheet
                        var worksheet = excel.Workbook.Worksheets["ReceitaAnual"];

                        // Popular header row data
                        worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                        worksheet.Cells[2, 1].LoadFromArrays(cellData);

                        string _dtCriacao = DateTime.Now.ToString();
                        _dtCriacao = _dtCriacao.Replace(" ", "-");
                        _dtCriacao = _dtCriacao.Replace("/", "_");
                        _dtCriacao = _dtCriacao.Replace(":", "");

                        _fileInfo = $"{System.Web.HttpContext.Current.Server.MapPath($"~/ExportFiles/")}ReceitaAnual_{_dtCriacao}.xlsx";

                        FileInfo excelFile = new FileInfo(_fileInfo);
                        excel.SaveAs(excelFile);
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK);

                var stream = new FileStream(_fileInfo, FileMode.Open);
                response.Content = new StreamContent(stream);

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(_fileInfo);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;

                tsc.SetResult(response);

                return tsc.Task;
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException" || ex.Source == "prmToolkit.Validation")
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                    response.ReasonPhrase = ex.Message;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
                tsc.SetResult(response);

                return tsc.Task;
            }
        }
    }
}

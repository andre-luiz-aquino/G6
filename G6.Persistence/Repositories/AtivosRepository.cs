using G6.Domain.Entities;
using G6.Domain.Interfaces;
using G6.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;



namespace G6.Persistence.Repositories
{
    public class AtivosRepository : IAtivosRepository
    {
        private readonly ApplicationDbContext _context;

        public AtivosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<bool> InsertAtivosList(IEnumerable<string> listastock)
        {
            List<Ativos> listaInsert = new List<Ativos>();
            try
            {
                foreach (var ativo in listastock)
                {
                    var model = new Ativos()
                    {
                        Symbol = ativo
                    };

                    listaInsert.Add(model);
                }

                await _context.AddRangeAsync(listaInsert);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task AtualizarAtivo(Ativos ativos)
        {
            var ativo = await GetAtivoByNome(ativos.Symbol);

            if (ativos is not null)
            {
                ativo.LogoUrl = ativos.LogoUrl;
                ativo.Symbol = ativos.Symbol;
                ativo.Currency = ativos.Currency;
                ativo.averageDailyVolume10Day = ativos.averageDailyVolume10Day;
                ativo.averageDailyVolume3Month = ativos.averageDailyVolume3Month;
                ativo.longName = ativos.longName;

                _context.Update(ativo);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("Dados invalidos...");
            }
        }
        public async Task AtualizarTodosAtivo(Ativos ativos)
        {

            var listaAtivos = await _context.Ativos.ToListAsync();

            // Verificar se a lista de ativos não está vazia
            if (listaAtivos != null && listaAtivos.Count > 0)
            {
                // Percorrer todos os ativos e atualizar suas informações
                foreach (var ativo in listaAtivos)
                {
                    ativo.LogoUrl = ativos.LogoUrl;
                    ativo.Symbol = ativos.Symbol;
                    ativo.Currency = ativos.Currency;
                    ativo.averageDailyVolume10Day = ativos.averageDailyVolume10Day;
                    ativo.averageDailyVolume3Month = ativos.averageDailyVolume3Month;
                    ativo.longName = ativos.longName;

                    _context.Update(ativo);
                }

                
                await _context.SaveChangesAsync();
            }
            else
            {
                
                throw new Exception("Não há ativos para atualizar.");
            }

        }

      
        public async Task<Ativos> GetAtivoByNome(string nome)
        {
            List<string> list = new List<string>();
            var ativo = await _context.Ativos.FirstOrDefaultAsync(x => x.Symbol == nome);

            if (ativo is not null)
            {
                return ativo;
            }

            return null;
        }

 
        public async Task<List<string>> GetAtivoCodigos()
        {
            var codigos = await _context.Ativos.Select(a => a.Symbol).ToListAsync();
            return codigos;
        }

        public async Task<ListaMelhoresAtivos> GetFuncaoMelhoresAtivos(string nome)
        {
            // String de conexão
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("SELECT \"Ativos\".calcular_media_movel_top10(@nomeCarteira);", conn))
                {
                    cmd.Parameters.AddWithValue("nomeCarteira", nome);

                    // Executar o comando e capturar o resultado
                    var result = await cmd.ExecuteScalarAsync();

                    // Converter o resultado para string JSON
                    string jsonResult = result.ToString();

                    // Opcional: deserializar o JSON para uma lista de objetos ListaMelhoresAtivos

                    var data = JsonConvert.DeserializeObject<ListaMelhoresAtivos>(jsonResult);

                    // Retornar os dados deserializados
                    return data;
                }
            }
        }

        public async Task<List<Ativos>> GetTodosAtivos()
        {
            return await _context.Ativos.ToListAsync();
        }

        public async Task<ListaTop10Ativos> GetTop10Ativos()
        {
            // String de conexão
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("SELECT \"Ativos\".get_top_10_ativos();", conn))
                {
                    // Executar o comando e capturar o resultado
                    var result = await cmd.ExecuteScalarAsync();

                    // Converter o resultado para string JSON
                    string jsonResult = result.ToString();

                    // Opcional: deserializar o JSON para uma lista de objetos ListaMelhoresAtivos

                    var data = JsonConvert.DeserializeObject<ListaTop10Ativos>(jsonResult);

                    // Retornar os dados deserializados
                    return data;
                }
            }
        }

        public async Task<RetornoRelatorioAtivo> GetRelatorioPorAtivo(int ativoId, bool paridadeRiscos)
        {
            // String de conexão
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("SELECT \"Ativos\".relatorio_graficos(@ativoIdFront, @paridadeRiscosFront);", conn))
                {
                    cmd.Parameters.AddWithValue("ativoIdFront", ativoId);
                    cmd.Parameters.AddWithValue("paridadeRiscosFront", paridadeRiscos);

                    // Executar o comando e capturar o resultado
                    var result = await cmd.ExecuteScalarAsync();

                    // Converter o resultado para string JSON
                    string jsonResult = result.ToString();

                    // Opcional: deserializar o JSON para uma lista de objetos ListaMelhoresAtivos

                    var data = JsonConvert.DeserializeObject<RetornoRelatorioAtivo>(jsonResult);

                    // Retornar os dados deserializados
                    return data;
                }
            }
        }

        public async Task NormalizarDadosTabela()
        {
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("CALL \"Ativos\".calcular_e_inserir_dados_normalizados();", conn))
                {
                    // Executar o comando
                    var result = await cmd.ExecuteNonQueryAsync();
                }
            }

        }

        public async Task<RetornoRendimentoTotalCarteira> GetRendimentoTotalCarteira(bool paridade)
        {
            // String de conexão
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("SELECT \"Ativos\".get_rendimento_total_carteira(@paridadeRiscosFront);", conn))
                {
                    cmd.Parameters.AddWithValue("paridadeRiscosFront", paridade);

                    // Executar o comando e capturar o resultado
                    var result = await cmd.ExecuteScalarAsync();

                    // Converter o resultado para string JSON
                    string jsonResult = result.ToString();

                    // Opcional: deserializar o JSON para uma lista de objetos ListaMelhoresAtivos

                    var data = JsonConvert.DeserializeObject<RetornoRendimentoTotalCarteira>(jsonResult);

                    // Retornar os dados deserializados
                    return data;
                }
            }
        }

        public async Task<RetornoRelatorioTodosAtivos> GetRelatorioAllAtivos(bool paridade)
        {
            // String de conexão
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("SELECT \"Ativos\".relatorio_todos_ativos(@paridadeRiscosFront);", conn))
                {
                    cmd.Parameters.AddWithValue("paridadeRiscosFront", paridade);

                    // Executar o comando e capturar o resultado
                    var result = await cmd.ExecuteScalarAsync();

                    // Converter o resultado para string JSON
                    string jsonResult = result.ToString();

                    // Opcional: deserializar o JSON para uma lista de objetos ListaMelhoresAtivos

                    var data = JsonConvert.DeserializeObject<RetornoRelatorioTodosAtivos>(jsonResult);

                    // Retornar os dados deserializados
                    return data;
                }
            }
        }

        public async Task<RetornoRelatorioRetornoDiarioCarteira> GetRelatorioDiarioCarteira(bool paridadeRiscos)
        {
            // String de conexão
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("SELECT \"Ativos\".relatorio_diario_carteira(@paridadeRiscosFront);", conn))
                {
                    cmd.Parameters.AddWithValue("paridadeRiscosFront", paridadeRiscos);

                    // Executar o comando e capturar o resultado
                    var result = await cmd.ExecuteScalarAsync();

                    // Converter o resultado para string JSON
                    string jsonResult = result.ToString();

                    // Opcional: deserializar o JSON para uma lista de objetos ListaMelhoresAtivos

                    var data = JsonConvert.DeserializeObject<RetornoRelatorioRetornoDiarioCarteira>(jsonResult);

                    // Retornar os dados deserializados
                    return data;
                }
            }
        }

        public async Task<RetornoTelaInicial> GetContextoTelaInicial(bool paridadeRiscos)
        {
            // String de conexão
            var connectionString = _context.Database.GetConnectionString();

            // Criar a conexão com o banco
            using (var conn = new NpgsqlConnection(connectionString))
            {
                // Abrir a conexão
                await conn.OpenAsync();

                // Criar o comando
                using (var cmd = new NpgsqlCommand("SELECT \"Ativos\".get_contexto_tela_inicial(@paridadeRiscosFront);", conn))
                {
                    cmd.Parameters.AddWithValue("paridadeRiscosFront", paridadeRiscos);

                    // Executar o comando e capturar o resultado
                    var result = await cmd.ExecuteScalarAsync();

                    // Converter o resultado para string JSON
                    string jsonResult = result.ToString();

                    // Opcional: deserializar o JSON para uma lista de objetos ListaMelhoresAtivos

                    var data = JsonConvert.DeserializeObject<RetornoTelaInicial>(jsonResult);

                    // Retornar os dados deserializados
                    return data;
                }
            }
        }
    }
}

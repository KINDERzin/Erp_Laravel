using Erp.Modelos;

namespace Controles;

public class Forn_MateriaPrimaControle : BaseControle
{
  //----------------------------------------------------------------------------

  public Forn_MateriaPrimaControle() : base()
  {
    NomeDaTabela = "Fornecedor_MateriaPrima";
  }

  //----------------------------------------------------------------------------

  public virtual Registro? Ler(int id)
  {
    var collection = liteDB.GetCollection<Forn_MateriaPrima>(NomeDaTabela);
    return collection.FindOne(d => d.Id == id);
  }

  //----------------------------------------------------------------------------

  public virtual List<Forn_MateriaPrima>? LerTodos()
  {
    var tabela = liteDB.GetCollection<Forn_MateriaPrima>(NomeDaTabela);
    return new List<Forn_MateriaPrima>(tabela.FindAll().OrderBy(d => d.Id));
  }

  //----------------------------------------------------------------------------

  public virtual void Apagar(int id)
  {
    var collection = liteDB.GetCollection<Forn_MateriaPrima>(NomeDaTabela);
    collection.Delete(id);
  }

  //----------------------------------------------------------------------------

  public virtual void CriarOuAtualizar(Forn_MateriaPrima forn_MateriaPrima)
  {
    var collection = liteDB.GetCollection<Forn_MateriaPrima>(NomeDaTabela);
    collection.Upsert(forn_MateriaPrima);
  }

  //----------------------------------------------------------------------------
}
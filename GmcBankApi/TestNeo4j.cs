using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GmcBankApi
{
    public class TestNeo4j : IDisposable
    {
        private readonly IDriver _driver;

        public TestNeo4j(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic(user, password));
        }

        public IStatementResult PrintGreeting()
        {
            using (var session = _driver.Session())
            {
                var greeting = session.WriteTransaction(tx =>
                {
                    var result = tx.Run("match (p:Person)-[r:ACTED_IN]->(:Movie)<-[d:DIRECTED]-(p:Person) return collect(p) as persons , collect(r) as acted_in, collect(d) as directed");
                    return result;
                });
                Console.WriteLine(greeting);
                return greeting;
            }
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
      
    }
    
}

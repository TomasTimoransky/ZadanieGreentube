using Microsoft.AspNetCore.Mvc;
using TransactionEngine;
using TransactionEngine.Interface;
using ZadanieGreentube.Model;

namespace ZadanieGreentube.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ITransactionEngine _transactionEngine;

        public PlayerController(ITransactionEngine transactionEngine)
        {
            _transactionEngine = transactionEngine;
        }

        [HttpPost("Register/{id}")]
        public ObjectResult PlayerRegister(Guid id)
        {
            try
            {
                _transactionEngine.RegisterPlayerWallet(id);
                return StatusCode(StatusCodes.Status200OK, "Player registered successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Balance/{id}")]
        public ObjectResult PlayerBalance(Guid id)
        {
            try
            {
                var balance = _transactionEngine.GetPlayerBalance(id);
                return StatusCode(StatusCodes.Status200OK, balance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Transactions/{id}")]
        public ObjectResult PlayerTransactions(Guid id)
        {
            try
            {
                var transactions = _transactionEngine.GetPlayerTransactions(id);
                return StatusCode(StatusCodes.Status200OK, transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Transaction")]
        public ObjectResult Transaction(TransactionRequest playerTransactionRequest)
        {
            try
            {
                var result = _transactionEngine.ProcessTransaction(playerTransactionRequest.PlayerId,
                    playerTransactionRequest.TransactionId, playerTransactionRequest.TransactionType, playerTransactionRequest.Amount);

                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, "Transaction approved.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "Transaction rejected.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
} 
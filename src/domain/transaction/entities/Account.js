import Entity from '../../commom/entities/Entity'
import Expense from './Expense'
import Revenue from './Revenue'

class Account extends Entity {
  constructor({ name, balance = 0, transactions = [], id = '' }) {
    super({ id })

    this._name = name
    this._balance = balance
    this._transactions = transactions
  }

  async name() {
    return this._name
  }

  async balance() {
    return this._balance
  }

  async transactions() {
    return this._transactions
  }

  async addRevenue({ value }) {
    const transaction = new Revenue({ value })
    await transaction.perform({ account: this })
    return transaction
  }

  async addExpense({ value }) {
    const transaction = new Expense({ value })
    await transaction.perform({ account: this })
    return transaction
  }
}

export default Account

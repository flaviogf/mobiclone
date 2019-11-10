import Transaction from './Transaction'

class Revenue extends Transaction {
  async perform({ account }) {
    account._balance += await this.value()
    account._transactions.push(this)
  }
}

export default Revenue

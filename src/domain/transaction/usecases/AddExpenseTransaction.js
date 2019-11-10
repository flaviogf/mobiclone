import UseCase from '../../commom/usecases/UseCase'

class AddExpenseTransaction extends UseCase {
  constructor({ userRepository, accountRepository }) {
    super()

    this._userRepository = userRepository
    this._accountRepository = accountRepository
  }

  async execute({ userId, accountId, value }) {
    const user = await this._userRepository.findById(userId)

    const account = await this._accountRepository.findById(accountId)

    await Promise.all([
      account.addExpense({ value }),
      user.addPoint({ description: 'new expense', value: 100 }),
    ])

    await Promise.all([
      this._accountRepository.persist(account),
      this._userRepository.persist(user),
    ])
  }
}

export default AddExpenseTransaction

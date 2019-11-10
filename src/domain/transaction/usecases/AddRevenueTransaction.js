import UseCase from '../../commom/usecases/UseCase'

class AddRevenueTransaction extends UseCase {
  constructor({ userRepository, accountRepository }) {
    super()

    this._userRepository = userRepository
    this._accountRepository = accountRepository
  }

  async execute({ userId, accountId, value }) {
    const user = await this._userRepository.findById(userId)

    const account = await this._accountRepository.findById(accountId)

    await Promise.all([
      account.addRevenue({ value }),
      user.addPoint({ description: 'new revenue', value: 100 }),
    ])

    await Promise.all([
      this._accountRepository.persist(account),
      this._userRepository.persist(user),
    ])
  }
}

export default AddRevenueTransaction

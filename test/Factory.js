import faker from 'faker'

import Account from '../src/domain/transaction/entities/Account'
import User from '../src/domain/transaction/entities/User'

class Factory {
  static async account(params) {
    return new Account({
      name: faker.name.findName(),
      ...params,
    })
  }

  static async user(params) {
    return new User({
      name: faker.name.findName(),
      ...params,
    })
  }
}

export default Factory

import { startOfMinute } from 'date-fns'

import Entity from '../../commom/entities/Entity'

class Transaction extends Entity {
  constructor({ value, date, id = '' }) {
    super({ id })

    this._value = value
    this._date = date || startOfMinute(new Date())
  }

  async value() {
    return this._value
  }

  async date() {
    return this._date
  }

  async perform({ account }) {
    throw new Error('it must be implemented')
  }
}

export default Transaction

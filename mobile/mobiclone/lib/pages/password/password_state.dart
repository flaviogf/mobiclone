import 'package:equatable/equatable.dart';
import 'package:mobiclone/models/user.dart';

abstract class PasswordState extends Equatable {}

class InitiatedPasswordState extends PasswordState {
  @override
  List<Object> get props => [];
}

class SubmittedPasswordState extends PasswordState {
  final String value;

  SubmittedPasswordState(this.value);

  @override
  List<Object> get props => [value];
}

class CreatedUserPasswordState extends PasswordState {
  final User value;

  CreatedUserPasswordState(this.value);

  @override
  List<Object> get props => [value];
}

class UnCreatedUserPasswordState extends PasswordState {
  final User value;

  UnCreatedUserPasswordState(this.value);

  @override
  List<Object> get props => [value];
}

class RequiredPasswordState extends PasswordState {
  final String value;
  final String error;

  RequiredPasswordState(this.value, this.error);

  @override
  List<Object> get props => [value, error];
}

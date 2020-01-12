import 'package:equatable/equatable.dart';

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

class ValidatedPasswordState extends PasswordState {
  final String value;

  ValidatedPasswordState(this.value);

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

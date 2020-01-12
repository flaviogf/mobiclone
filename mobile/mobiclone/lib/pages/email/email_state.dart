import 'package:equatable/equatable.dart';

abstract class EmailState extends Equatable {}

class InitiatedEmailState extends EmailState {
  @override
  List<Object> get props => [];
}

class SubmittedEmailState extends EmailState {
  final String value;

  SubmittedEmailState(this.value);

  @override
  List<Object> get props => [];
}

class ValidatedEmailState extends EmailState {
  final String value;

  ValidatedEmailState(this.value);

  @override
  List<Object> get props => [value];
}

class RequiredEmailState extends EmailState {
  final String value;
  final String error;

  RequiredEmailState(this.value, this.error);

  @override
  List<Object> get props => [value, error];
}
